using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

using GRBL.Controls;
using GRBL.Wiki;

namespace GRBL
{
    public class GRBLManager
    {
        private static GRBLManager _instance;
        public static GRBLManager Instance => _instance ?? (_instance = new GRBLManager());

        public Form CurrentForm;

        public eMachineState MachineState = eMachineState.Unknown;

        public GRBLManager()
        {
            serialPort = new SerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            queryTimer = new System.Windows.Forms.Timer();
            queryTimer.Interval = TimerInterval;
            queryTimer.Tick += new EventHandler(queryTimer_Tick);

            JoggingTimer = new System.Windows.Forms.Timer();
            JoggingTimer.Interval = 1000;
            JoggingTimer.Tick += new EventHandler(JoggingTimer_Tick);

            ScannedGRBLSettings = new List<GRBLSetting>();
            MessagesList = new List<MessageInfo>();
            ProbeSteps = new List<string>();
        }

        #region Console

        public RichTextBox ConsoleBox;

        /// <summary>
        /// Adds line to console(rich text box)
        /// </summary>
        /// <param name="line"></param>
        public void LineToConsole(string line)
        {
            if (ConsoleBox != null)
            {
                ConsoleBox.Invoke(() => {
                    ConsoleBox.AppendText(string.Format("{0}\n", line));
                    ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
                    ConsoleBox.ScrollToCaret();
                });
            }
        }

        #endregion

        #region Serial

        private SerialPort serialPort;
        private System.Windows.Forms.Timer queryTimer;

        private string RX_DATA;
        private bool Receiving = false;

        private int LockCounter = 0, LockTrigger = 3;
        private const int TimerInterval = 200;
        public bool ShowQuery = true;

        #region Check mode

        public List<CheckItem> CheckItemsList;
        public Action CheckEndAction;
        public bool CheckInProgress = false;

        public void StartCheck()
        {
            if (CheckItemsList == null)
                CheckItemsList = new List<CheckItem>();

            CheckItemsList.Clear();

            CheckInProgress = true;
            SendFile();
        }

        /// <summary>
        /// Enable check mode by sending $C and disable sending it again. $C
        /// </summary>
        public void CheckMode()
        {
            SendLine("$C", false);
        }

        #endregion

        #region Messages List

        public Action MessageReceived;
        public List<MessageInfo> MessagesList;
        public bool ShowMessagesInConsole_ALARM = true;
        public bool ShowMessagesInConsole_ERROR = true;
        public bool ShowMessagesInConsole_MSG = true;

        public void AddMessage(string type, string id, string message)
        {
            MessagesList.Add(new MessageInfo() { ID = id, Message = message, Type = type });
        }

        public void RemoveMessage(int index)
        {
            MessagesList.RemoveAt(index);
        }

        #endregion


        /// <summary>
        /// Check if serial is connected
        /// </summary>
        /// <returns>Connection status</returns>
        public bool IsSerialConnected()
        {
            return serialPort.IsOpen;
        }

        /// <summary>
        /// Open serial connection to GRBL
        /// </summary>
        /// <param name="portData">Serial port information</param>
        public void OpenSerialPort(PortData portData)
        {
            if (IsSerialConnected())
                CloseSerialPort();

            try
            {
                LockCounter = 0;
                RX_DATA = string.Empty;
                Receiving = false;

                portData.PortDataToSerialPort(serialPort);

                serialPort.Open();
                queryTimer.Start();

                SetCoordinateSystem(eP.P1);
                AbsoluteMode();
            }
            catch(Exception ex)
            {
                queryTimer.Stop();
                MachineState = eMachineState.Unknown;
                Receiving = false;
                RX_DATA = string.Empty;
                MessageBox.Show(ex.Message, "Serial Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Close serial connection to GRBL
        /// </summary>
        public void CloseSerialPort()
        {
            if(serialPort != null && IsSerialConnected())
            {
                queryTimer.Stop();
                serialPort.Close();
            }
        }

        /// <summary>
        /// send line to GRBL
        /// </summary>
        /// <param name="line">line to be sent</param>
        /// <param name="sendToConsole">if true add line to console(rich text box)</param>
        public void SendLine(string line, bool sendToConsole)
        {
            if(IsSerialConnected() && line.Length > 0)
            {
                serialPort.Write(string.Format("{0}\r", FormattingLine(line)));

                if(sendToConsole && !CheckInProgress)
                    LineToConsole(FormattingLine(line));

                //Update current positioning mode
                if (line.Contains("G90"))
                    Positioning = ePositioning.Absolute;
                else if(line.Contains("G91"))
                    Positioning = ePositioning.Incremental;
            }
        }

        private string FormattingLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return string.Empty;

            //No reason to sent comments
            if (line[0] == '(')
                return string.Empty;

            //GRBL dislikes values like 1,5 but likes values like 1.5
            line = Converters.DotToGRBL(line);
            line = line.Replace(" ", "");
            line = line.ToUpper();
            line = line.Replace("\r", ""); //remove CR
            line = line.Replace("\n", ""); //remove LF
            return line.Trim();
        }


        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (IsSerialConnected() && serialPort.BytesToRead > 0)
                {
                    RX_DATA = string.Empty;
                    RX_DATA = serialPort.ReadTo("\r\n");
                    Receiving = true;

                    CurrentForm.Invoke(new EventHandler(DataReceived));
                    while (IsSerialConnected() && Receiving);
                }
            }
            catch { }
        }

        private void DataReceived(object sender, EventArgs e)
        {
            if (RX_DATA.StartsWith("[PRB") && ProbeInProgress)
            {
                ProbeNextStep();
            }

            if (CheckInProgress && MachineState == eMachineState.Check && SendingFile)
            {
                if (RX_DATA[0] != '<')
                {
                    CheckItemsList.Add(new CheckItem() { Line = FileLines[linesConfirmed], OK = RX_DATA, lineIndex = linesConfirmed });
                }
            }

            if (SendingFile)
            {
                if (RX_DATA.Contains("ok"))
                {
                    buffer += FileLines[linesConfirmed].Length + 1;

                    linesConfirmed++;

                    if (linesConfirmed >= linesCount)
                    {
                        SendingFile = false;

                        UpdatePercentage();

                        if (SendingSettings)
                        {
                            SendingSettings = false;
                            LineToConsole("Settings has been sent.");
                        }
                    }
                    else
                    {
                        if (linesSent < linesCount)
                            SendNextLine();
                    }
                }
            }

            //Query data
            if (RX_DATA.Length > 0 && RX_DATA[0] == '<')
            {
                //GRBL gives data, reset lock counter
                LockCounter = 0;

                //Update position values
                UpdatePosition(RX_DATA);

                //Update current status
                MachineState = GetMachineStatus(RX_DATA);

                //Show full line in console
                if (ShowQuery)
                    LineToConsole(RX_DATA);
            }

            //If jogging, and returned ok, send next jog
            if(Jogging && !nextJog)
            {
                if (RX_DATA.Contains("ok"))
                {
                    //LineToConsole(RX_DATA);
                    nextJog = true;
                }
            }

            //Add lines to settings list if scanning them

            if(RX_DATA.StartsWith("$"))
                LineToConsole(RX_DATA);

            if (SettingsScan)
            {
                if (RX_DATA.StartsWith("$"))
                {
                    ScannedGRBLSettings.Add(GetValues.GetSetting(RX_DATA));
                }
                else
                {
                    SettingsScan = false;

                    if(SettingsScanFinished != null)
                        SettingsScanFinished.Invoke();
                }
            }

            //Add VER and OPT info when scanning them
            if(VerOptScan)
            {
                if (RX_DATA.Contains("VER"))
                {
                    ScannedVER = RX_DATA;
                }
                else if (RX_DATA.Contains("OPT"))
                {
                    ScannedOPT = RX_DATA;
                    VerOptScan = false;

                    if (VerOptScanFinished != null)
                        VerOptScanFinished.Invoke();
                }
            }

            //Alarm errors
            if (RX_DATA.Contains("ALARM"))
            {
                MachineState = eMachineState.Alarm;

                if(ShowMessagesInConsole_ALARM)
                {
                    LineToConsole("-------------------");
                    LineToConsole(RX_DATA);
                    LineToConsole(WikiAlarm.GetAlarmDescription(int.Parse(RX_DATA.Split(':')[1])));
                    LineToConsole("-------------------");
                }

                if (MessageReceived != null)
                {
                    AddMessage("ALARM",
                        int.Parse(RX_DATA.Split(':')[1]).ToString(),
                        WikiAlarm.GetAlarmDescription(int.Parse(RX_DATA.Split(':')[1])));

                    MessageReceived.Invoke();
                }

                Receiving = false;
                return;
            }

            //Errors
            if (RX_DATA.StartsWith("error") && !CheckInProgress)
            {
                if(ShowMessagesInConsole_ERROR)
                {
                    LineToConsole("-------------------");
                    LineToConsole(RX_DATA);
                    LineToConsole(WikiError.GetErrorDescription(int.Parse(RX_DATA.Split(':')[1])));
                    LineToConsole("-------------------");
                }



                //Stop machine if error during file transfer
                if(SendingFile)
                    HOLD();

                if (MessageReceived != null)
                {
                    AddMessage("ERROR",
                        int.Parse(RX_DATA.Split(':')[1]).ToString(),
                        WikiError.GetErrorDescription(int.Parse(RX_DATA.Split(':')[1])));

                    MessageReceived.Invoke();
                }

                Receiving = false;
                return;
            }

            //MSG
            if (RX_DATA.Length > 0 && RX_DATA[0] == '[' && !ProbeInProgress)
            {
                if (RX_DATA.Contains("MSG"))
                {
                    if (RX_DATA.Contains("Pgm End"))
                    {
                        if(FileHasBeenSent != null && !CheckInProgress)
                            FileHasBeenSent.Invoke();

                        if (CheckInProgress)
                        {
                            if (CheckEndAction != null)
                                CheckEndAction.Invoke();

                            CheckInProgress = false;
                        }

                        LineToConsole(RX_DATA);
                    }
                }

                if (ShowMessagesInConsole_MSG)
                {
                    LineToConsole("-------------------");
                    LineToConsole("MESSAGE");
                    LineToConsole(RX_DATA);
                    LineToConsole("-------------------");
                }

                if (MessageReceived != null)
                {
                    AddMessage("MESSAGE",
                        RX_DATA.Split(':', ']')[1],
                        WikiMessages.GetMessageDescription(RX_DATA.Split(':', ']')[1]));

                    MessageReceived.Invoke();
                }

                Receiving = false;
                return;
            }

            Receiving = false;
        }

        private void queryTimer_Tick(object sender, EventArgs e)
        {
            //Tool Change thing also here


            if (IsSerialConnected())
            {
                serialPort.Write(new byte[] { Convert.ToByte('?') }, 0, 1);

                if (LockCounter >= LockTrigger)
                    MachineState = eMachineState.Locked;
                else
                {
                    LockCounter++;
                }
            }
        }

        #endregion

        #region Scan Settings

        public Action SettingsScanFinished;
        private bool SettingsScan = false;
        public readonly List<GRBLSetting> ScannedGRBLSettings;

        private bool VerOptScan = false;
        public Action VerOptScanFinished;
        public string ScannedVER, ScannedOPT;

        /// <summary>
        /// Starts settings scan
        /// </summary>
        public void ScanGRBLSettings()
        {
            if(IsSerialConnected())
            {
                SettingsScan = true;
                ScannedGRBLSettings.Clear();
                SendLine("$$", false);
            }
        }

        /// <summary>
        /// Starts VER and OPT scan
        /// </summary>
        public void ScanVerOpt()
        {
            VerOptScan = true;
            SendLine("$I", false);
        }

        /// <summary>
        /// Returns only version from VER string
        /// </summary>
        /// <returns>VERSION</returns>
        public string ParseVersionFromScanned()
        {
            if (string.IsNullOrEmpty(ScannedVER) || ScannedVER.Length <= 0)
                return string.Empty;

            return ScannedVER.Split(':', ':')[1];
        }

        /// <summary>
        /// Returns name witch is set with I$ command
        /// </summary>
        /// <returns>Name</returns>
        public string ParseStringFromScanned()
        {
            if (string.IsNullOrEmpty(ScannedVER) || ScannedVER.Length <= 0)
                return string.Empty;

            return ScannedVER.Split(':', ']')[2];
        }

        /// <summary>
        /// Returns block buffer size from scanned OPT
        /// </summary>
        /// <returns>block buffer size</returns>
        public int ParseBlockBufferSizeFromScanned()
        {
            if (string.IsNullOrEmpty(ScannedOPT) || ScannedOPT.Length <= 0)
                return -1;

            return int.Parse(ScannedOPT.Split(',', ',')[1]);
        }

        /// <summary>
        /// Returns RX buffer size from scanned OPT
        /// </summary>
        /// <returns>rx buffer size</returns>
        public int ParseRXBufferSizeFromScanned()
        {
            if (string.IsNullOrEmpty(ScannedOPT) || ScannedOPT.Length <= 0)
                return -1;

            return int.Parse(ScannedOPT.Split(',', ']')[2]);
        }

        /// <summary>
        /// Returns OPT code letters in char array
        /// </summary>
        /// <returns>OPT codes</returns>
        public char[] ParseOPTCodesFromScanned()
        {
            if (string.IsNullOrEmpty(ScannedOPT) || ScannedOPT.Length <= 0)
                return null;

            return ScannedOPT.Split(':', ',')[1].ToCharArray();
        }

        private bool SendingSettings = false;

        public void SendSettingsToGRBL(List<string> settings)
        {
            if (settings == null || settings.Count <= 0)
                return;

            if (FileLines == null)
                FileLines = new List<string>();

            FileLines.Clear();
            FileLines = settings;

            SendingSettings = true;
            bufferSize = ParseRXBufferSizeFromScanned() - 1;

            linesCount = settings.Count;

            LineToConsole("sending settings...");

            SendFile();
        }

        #endregion

        #region WCS

        public eP CurrentWCS = eP.P1;

        /// <summary>
        /// Set current WCS space (G54-G59)
        /// </summary>
        /// <param name="P">WCS space</param>
        public void SetCoordinateSystem(eP P)
        {
            switch (P)
            {
                case eP.P1:
                    SendLine("G54", true);
                    CurrentWCS = eP.P1;
                    break;
                case eP.P2:
                    SendLine("G55", true);
                    CurrentWCS = eP.P2;
                    break;
                case eP.P3:
                    SendLine("G56", true);
                    CurrentWCS = eP.P3;
                    break;
                case eP.P4:
                    SendLine("G57", true);
                    CurrentWCS = eP.P4;
                    break;
                case eP.P5:
                    SendLine("G58", true);
                    CurrentWCS = eP.P5;
                    break;
                case eP.P6:
                    SendLine("G59", true);
                    CurrentWCS = eP.P6;
                    break;
            }
        }


        /// <summary>
        /// G10 L20 is similar to G10 L2 except that instead of setting the offset/entry to the given value,
        /// it is set to a calculated value that makes the current coordinates become the given value.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="Pos"></param>
        public void SetCoordinateSystem_L20(eP P, XYZ Pos)
        {
            SendLine(string.Format("G10 L20 {0} {1}", P.ToString(), Pos.ToString()), true);
        }

        /// <summary>
        /// G10 L2 offsets the origin of the axes in the coordinate system specified to the value of the axis word.
        /// The offset is from the machine origin established during homing.
        /// The offset value will replace any current offsets in effect for the coordinate system specified.
        /// Axis words not used will not be changed.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="Pos"></param>
        public void SetCoordinateSystem_L2(eP P, XYZ Pos)
        {
            SendLine(string.Format("G10 L2 {0} {1}", P.ToString(), Pos.ToString()), true);
        }

        /// <summary>
        /// Zero single axis in current WCS
        /// </summary>
        /// <param name="axis"></param>
        public void ZeroSingleAxis(eAxis axis)
        {
            SendLine(string.Format("G10 L20 {0} {1}0", CurrentWCS.ToString(), axis.ToString()), true);
        }

        /// <summary>
        /// Zero all axis in current WCS
        /// </summary>
        public void ZeroAllAxis()
        {
            SendLine(string.Format("G10 L20 {0} X0 Y0 Z0", CurrentWCS.ToString()), true);
        }

        /// <summary>
        /// Returns WCS G version of P
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string GetWorkCoordinateSpace(eP p)
        {
            switch(p)
            {
                case eP.P1:
                    return "G54";
                case eP.P2:
                    return "G55";
                case eP.P3:
                    return "G56";
                case eP.P4:
                    return "G57";
                case eP.P5:
                    return "G58";
                case eP.P6:
                    return "G59";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Start spindle with RPM (M3 S0000)
        /// </summary>
        /// <param name="RPM">RPM</param>
        public void PowerSpindle(int RPM)
        {
            SendLine(string.Format("M3 S{0}", RPM), true);
        }

        /// <summary>
        /// Start spindle (M3)
        /// </summary>
        public void PowerSpidle()
        {
            SendLine("M3", true);
        }

        /// <summary>
        /// Stop spindle (M5)
        /// </summary>
        public void PowerOffSpindle()
        {
            SendLine("M5", true);
        }

        public void SetSpindleRPM(int RPM)
        {
            SendLine(string.Format("S{0}", RPM), true);
        }


        /// <summary>
        /// Unlock GRBL ($X)
        /// </summary>
        public void UNLOCK()
        {
            SendLine("$X", true);
        }

        /// <summary>
        /// Cycle Start/Resume (~)
        /// </summary>
        public void START_RESUME()
        {
            SendLine("~", true);
        }

        /// <summary>
        /// Feed hold (!)
        /// </summary>
        public void HOLD()
        {
            SendLine("!", true);
        }

        /// <summary>
        /// Soft reset (0x18 (ctrl-x))
        /// </summary>
        public void RESET()
        {
            if (IsSerialConnected())
            {
                var reset = new byte[] { 0x18 };
                serialPort.Write(reset, 0, 1);
                LineToConsole("ctrl-x (0x18)");
            }
        }

        /// <summary>
        /// Homing sequence with $H
        /// </summary>
        public void HOME()
        {
            SendLine("$H", true);
        }


        /// <summary>
        /// Send $I command to get VER and OPT
        /// </summary>
        public void ShowVerOpt()
        {
            SendLine("$I", true);
        }


        /// <summary>
        /// GRBL enters to sleep mode. To exit sleep mode use soft-reset or power-cycle
        /// </summary>
        public void EnableSleepMode()
        {
            if (IsSerialConnected())
            {
                SendLine("$SLP", true);
            }
        }

        /// <summary>
        /// Triggers safety door warning
        /// </summary>
        public void TriggerSafetyDoorWarning()
        {
            if (IsSerialConnected())
            {
                var value = new byte[] { 0x84 };
                serialPort.Write(value, 0, 1);
            }
        }


        /// <summary>
        /// Increase spindle override value
        /// </summary>
        /// <param name="one">Add one or ten</param>
        public void SpindleOverrideAdd(bool one)
        {
            if (IsSerialConnected())
            {
                if (one)
                {
                    var value = new byte[] { 0x9C };
                    serialPort.Write(value, 0, 1);
                }
                else
                {
                    var value = new byte[] { 0x9A };
                    serialPort.Write(value, 0, 1);
                }
            }
        }

        /// <summary>
        /// Decrease spindle override value
        /// </summary>
        /// <param name="one">Remove one or ten</param>
        public void SpindleOverrideRemove(bool one)
        {
            if (IsSerialConnected())
            {
                if (one)
                {
                    var value = new byte[] { 0x9D };
                    serialPort.Write(value, 0, 1);
                }
                else
                {
                    var value = new byte[] { 0x9B };
                    serialPort.Write(value, 0, 1);
                }
            }
        }

        /// <summary>
        /// Reset spindle override value
        /// </summary>
        public void SpindleOverrideReset()
        {
            if (IsSerialConnected())
            {
                var value = new byte[] { 0x99 };
                serialPort.Write(value, 0, 1);
            }
        }


        /// <summary>
        /// Increase feed rate override value
        /// </summary>
        /// <param name="one">Add one or ten</param>
        public void FeedRateOverrideAdd(bool one)
        {
            if (IsSerialConnected())
            {
                if (one)
                {
                    var value = new byte[] { 0x93 };
                    serialPort.Write(value, 0, 1);
                }
                else
                {
                    var value = new byte[] { 0x91 };
                    serialPort.Write(value, 0, 1);
                }
            }
        }

        /// <summary>
        /// Decrease feed rate override value
        /// </summary>
        /// <param name="one">Remove one or ten</param>
        public void FeedRateOverrideRemove(bool one)
        {
            if (IsSerialConnected())
            {
                if (one)
                {
                    var value = new byte[] { 0x94 };
                    serialPort.Write(value, 0, 1);
                }
                else
                {
                    var value = new byte[] { 0x92 };
                    serialPort.Write(value, 0, 1);
                }
            }
        }

        /// <summary>
        /// Reset feed rate override value
        /// </summary>
        public void FeedRateOverrideReset()
        {
            if(IsSerialConnected())
            {
                var value = new byte[] { 0x90 };
                serialPort.Write(value, 0, 1);
            }
        }

        /// <summary>
        /// Set rapid override value, 0 = Full, 1 = Half, 2 = Quarter
        /// </summary>
        /// <param name="amount">Override amount</param>
        public void RapidOverride(int amount)
        {
            if (!IsSerialConnected())
                return;

            switch(amount)
            {
                case 0:
                    var full = new byte[] { 0x95 };
                    serialPort.Write(full, 0, 1);
                    break;
                case 1:
                    var half = new byte[] { 0x96 };
                    serialPort.Write(half, 0, 1);
                    break;
                case 2:
                    var quarter = new byte[] { 0x97 };
                    serialPort.Write(quarter, 0, 1);
                    break;
            }
        }


        /// <summary>
        /// Rename arduino board
        /// </summary>
        /// <param name="name">New name</param>
        public void RenameGRBLBoard(string name)
        {
            SendLine(string.Format("$I={0}", name), true);
        }

        /// <summary>
        /// Restore GRBL settings to default values
        /// </summary>
        public void RestoreGRBL_Settings()
        {
            SendLine("$RST=$", true);
        }

        /// <summary>
        /// Clears Work Coordinate Offsets G54-G59 and G28/30 from EEPROM
        /// </summary>
        public void RestoreGRBL_WCO()
        {
            SendLine("$RST=#", true);
        }

        /// <summary>
        /// This will clear WCO coordinates G54-G59, G28/30 and all $$ settings, $# parameters, $N startup lines and also $I build info string.
        /// </summary>
        public void RestoreGRBL_EEPROM()
        {
            SendLine("$RST=*", true);
        }

        #endregion

        #region Position

        public XYZ MPos;
        public XYZ WPos;
        public XYZ WCO;
        public float CurrentFeedRate = 0.0f;
        public float currentSpindleSpeed = 0.0f;

        public ePositioning Positioning = ePositioning.Absolute;

        public float OverrideFeedRate = 100;
        public float OverrideRapid = 100;
        public float OverrideSpindle = 100;

        private string FS = "", OV = "", F, S;

        /// <summary>
        /// Updates position
        /// </summary>
        /// <param name="rxData"></param>
        private void UpdatePosition(string rxData)
        {
            try
            {
                string rawPos = rxData.Split('|', '|')[1];
                if(rxData.Contains("Ov"))
                {
                    FS = rxData.Split('|', '|')[2];

                    string ov = rxData.Split('|', '>')[3];
                    OverrideFeedRate = float.Parse(ov.Split(':', ',')[1]);
                    OverrideRapid = float.Parse(ov.Split(',', ',')[1]);
                    OverrideSpindle = float.Parse(ov.Split(',').Last());
                }
                else if(rxData.Contains("FS") || rxData.Contains("F"))
                {
                    FS = rxData.Split('|', '>')[2];
                }

                if(FS != "")
                {
                    if(FS.StartsWith("FS:"))
                        F = FS.Split(':', ',')[1];
                    else //GRBL reports only F
                        F = FS.Split(':', '>')[1];

                    CurrentFeedRate = float.Parse(Converters.DotToFloat(F));

                    if (FS.Contains("FS"))
                    {
                        S = FS.Split(',', '>')[1];
                        currentSpindleSpeed = float.Parse(Converters.DotToFloat(S));
                    }
                }

                if (rxData.Contains("WCO"))
                {
                    int wcoIndex = rxData.IndexOf("WCO");
                    string wco = rxData.Substring(wcoIndex);
                    wco = wco.Replace("WCO", string.Empty);

                    WCO.X = float.Parse(Converters.DotToFloat(wco.Split(':', ',')[1]));
                    WCO.Y = float.Parse(Converters.DotToFloat(wco.Split(',', ',')[1]));
                    WCO.Z = float.Parse(Converters.DotToFloat(wco.Split(',', '>')[2]));
                }

                if (rxData.Contains("MPos"))
                {
                    rawPos = rawPos.Remove(0, 5);
                    MPos.X = float.Parse(Converters.DotToFloat(rawPos.Split(',')[0]));
                    MPos.Y = float.Parse(Converters.DotToFloat(rawPos.Split(',')[1]));
                    MPos.Z = float.Parse(Converters.DotToFloat(rawPos.Split(',')[2]));

                    WPos = MPos - WCO;
                }

                if (rxData.Contains("WPos"))
                {
                    rawPos = rawPos.Remove(0, 5);
                    WPos.X = float.Parse(Converters.DotToFloat(rawPos.Split(',')[0]));
                    WPos.Y = float.Parse(Converters.DotToFloat(rawPos.Split(',')[1]));
                    WPos.Z = float.Parse(Converters.DotToFloat(rawPos.Split(',')[2]));

                    MPos = WPos + WCO;
                }
            }
            catch { }
        }

        /// <summary>
        /// Returns machine state from rx data
        /// </summary>
        /// <param name="RX_DATA"></param>
        /// <returns></returns>
        public eMachineState GetMachineStatus(string RX_DATA)
        {
            RX_DATA = RX_DATA.Split('<', '|')[1];

            switch (RX_DATA)
            {
                case "Idle":
                    return eMachineState.Idle;
                case "Run":
                    return eMachineState.Run;
                case "Hold:0":
                    return eMachineState.Hold_0;
                case "Hold:1":
                    return eMachineState.Hold_1;
                case "Jog":
                    return eMachineState.Jog;
                case "Alarm":
                    return eMachineState.Alarm;
                case "Door:0":
                    return eMachineState.Door_0;
                case "Door:1":
                    return eMachineState.Door_1;
                case "Door:2":
                    return eMachineState.Door_2;
                case "Door:3":
                    return eMachineState.Door_3;
                case "Check":
                    return eMachineState.Check;
                case "Home":
                    return eMachineState.Home;
                case "Sleep":
                    return eMachineState.Sleep;
                default:
                    return eMachineState.Unknown;
            }
        }

        /// <summary>
        /// Moves one axis with incremental command
        /// </summary>
        /// <param name="axis">axis to move</param>
        /// <param name="G0">move with G0 or G1 ?</param>
        /// <param name="distance">distance to be moved, can also be negative value</param>
        /// <param name="feedRate">specify feed rate if G1 is used</param>
        public void MoveSingleAxis(eAxis axis, bool G0, bool incremental, float distance, int feedRate)
        {
            if(incremental)
            {
                if (G0)
                    SendLine(string.Format("G0G91{0}{1}", axis.ToString(), Converters.DotToGRBL(distance)), true);
                else
                    SendLine(string.Format("G1G91{0}{1}F{2}", axis.ToString(), Converters.DotToGRBL(distance), feedRate), true);
            }
            else
            {
                if (G0)
                    SendLine(string.Format("G0G90{0}{1}", axis.ToString(), Converters.DotToGRBL(distance)), true);
                else
                    SendLine(string.Format("G1G90{0}{1}F{2}", axis.ToString(), Converters.DotToGRBL(distance), feedRate), true);
            }
        }

        /// <summary>
        /// Moves X and Y axis with incremental command
        /// </summary>
        /// <param name="G0">move with G0 or G1 ?</param>
        /// <param name="distanceX">distance to be moved along X axis, can also be negative value</param>
        /// <param name="distanceY">distance to be moved along Y axis, can also be negative value</param>
        /// <param name="feedRate">specify feed rate if G1 is used</param>
        public void MoveTwoAxis(bool G0, bool incremental, float distanceX, float distanceY, int feedRate)
        {
            if(incremental)
            {
                if (G0)
                    SendLine(string.Format("G0G91X{0}Y{1}", Converters.DotToGRBL(distanceX), Converters.DotToGRBL(distanceY)), true);
                else
                    SendLine(string.Format("G1G91X{0}Y{1}F{2}", Converters.DotToGRBL(distanceX), Converters.DotToGRBL(distanceY), feedRate), true);
            }
            else
            {
                if (G0)
                    SendLine(string.Format("G0G90X{0}Y{1}", Converters.DotToGRBL(distanceX), Converters.DotToGRBL(distanceY)), true);
                else
                    SendLine(string.Format("G1G90X{0}Y{1}F{2}", Converters.DotToGRBL(distanceX), Converters.DotToGRBL(distanceY), feedRate), true);
            }
        }

        /// <summary>
        /// Moves chosen axis to zero point with G0 command
        /// </summary>
        /// <param name="axis">axis to move</param>
        public void MoveToZero_SingleAxis(eAxis axis)
        {
            SendLine(string.Format("G0G90{0}0", axis), true);
        }

        /// <summary>
        /// Moves X and Y axis to zero point with G0 command
        /// </summary>
        public void MoveToZero_TwoAxis()
        {
            SendLine("G0X0Y0", true);
        }

        /// <summary>
        /// G90
        /// </summary>
        public void AbsoluteMode()
        {
            SendLine("G90", true);
        }

        /// <summary>
        /// G91
        /// </summary>
        public void IncrementalMode()
        {
            SendLine("G91", true);
        }

        #endregion

        #region Jogging

        public JoggingKnob joggingKnob;
        public bool EnableJoggingKnob = true;
        private System.Windows.Forms.Timer JoggingTimer;
        public float JoggingAmmount = 1.5f;
        public int JoggingFeedRate = 500;
        public int JoggingInterval = 500;
        private bool Jogging = false, nextJog = false;

        /// <summary>
        /// Sets up start and end actions
        /// </summary>
        public void InitializeJoggingKnob()
        {
            joggingKnob.StartJogging = new Action(StartJoggin);
            joggingKnob.StopJogging = new Action(StopJogging);
        }

        /// <summary>
        /// Start jogging
        /// </summary>
        public void StartJoggin()
        {
            Jogging = true;
            nextJog = true;
            JoggingTimer.Interval = JoggingInterval;
            JoggingTimer.Start();
        }

        /// <summary>
        /// Cancel jogging
        /// </summary>
        public void JogCancel()
        {
            if (IsSerialConnected())
            {
                var value = new byte[] { 0x85 };
                serialPort.Write(value, 0, 1);
                serialPort.Write("G4P0\r");
            }
        }

        /// <summary>
        /// Stop jogging
        /// </summary>
        public void StopJogging()
        {
            JoggingTimer.Stop();
            Jogging = false;
            JogCancel();
        }

        private void JoggingTimer_Tick(object sender, EventArgs e)
        {
            if (EnableJoggingKnob && nextJog)
            {
                float xjog = (JoggingAmmount * joggingKnob.ValX);
                float yjog = (JoggingAmmount * joggingKnob.ValY);

                if (xjog == 0 && yjog == 0)
                    return;

                SendLine(string.Format("$J=G91G21X{0}Y{1}F{2}", xjog, yjog, JoggingFeedRate), false);

                nextJog = false;
            }
        }

        #endregion

        #region File

        public List<string> FileLines;
        private int bufferSize, buffer, linesSent, linesConfirmed, linesCount;
        public bool SendingFile = false;

        public float FileSentPercentage = 0.0f;

        /// <summary>
        /// When file has been sent and pgm end msg received
        /// </summary>
        public Action FileHasBeenSent;

        /// <summary>
        /// Stop sending file
        /// </summary>
        public void StopFile()
        {
            SendingFile = false;
            DoToolChange = false;
        }

        /// <summary>
        /// Start sending file
        /// </summary>
        public void SendFile()
        {
            linesSent = 0;
            linesConfirmed = 0;
            buffer = bufferSize;

            SendingFile = true;
            SendNextLine();
        }

        /// <summary>
        /// Prepare opened file to list
        /// </summary>
        /// <param name="file"></param>
        public void PrepareFile(string file)
        {
            if (!File.Exists(file))
                return;

            bufferSize = ParseRXBufferSizeFromScanned() - 1;

            if (FileLines == null)
                FileLines = new List<string>();

            FileLines.Clear();
            linesCount = 0;

            using (StreamReader sr = new StreamReader(file))
            {
                string line = sr.ReadLine();
                while(line != null)
                {
                    if ((!string.IsNullOrEmpty(line)) && (line[0] != '(') && line[0] != '%')
                    {
                        FileLines.Add(line);
                        linesCount++;
                    }

                    line = sr.ReadLine();
                }

                sr.Close();
            }

            LineToConsole(string.Format("Lines prepared {0}", FileLines.Count));
        }

        /// <summary>
        /// Update progress percentage
        /// </summary>
        private void UpdatePercentage()
        {
            FileSentPercentage = ((float)linesSent / (float)FileLines.Count) * 100;
        }

        /// <summary>
        /// Send next line to GRBL
        /// </summary>
        private void SendNextLine()
        {
            if (DoToolChange)
                return;

            if(linesSent < linesCount && buffer >= FileLines[linesSent].Length + 1)
            {
                if (LineIsToolChange(FileLines[linesSent]) && !CheckInProgress)
                {
                    CurrentToolID = GetToolIDFromLine(FileLines[linesSent]);
                    DoToolChange = true;
                    linesSent++;
                    LineToConsole(string.Format("Change Tool to: {0}", CurrentToolID));
                    return;
                }

                SendLine(FileLines[linesSent], true);
                buffer -= FileLines[linesSent].Length + 1;
                linesSent++;

                UpdatePercentage();
            }
        }

        #endregion

        #region Tool Change

        public int CurrentToolID = 0;
        public bool DoToolChange = false;

        /// <summary>
        /// If file has been opened, this will return all tool ID numbers.
        /// </summary>
        /// <returns>array of tool ID's</returns>
        public int[] GetToolIDsFromFile()
        {
            if(FileLines != null && FileLines.Count > 0)
            {
                List<int> IDs = new List<int>();

                foreach(string line in FileLines)
                {
                    if(line[0] == 'T')
                        IDs.Add(int.Parse(line.Split('T', ' ')[1]));
                }

                return IDs.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Get tool ID from line
        /// </summary>
        /// <param name="line">T1 M6</param>
        /// <returns>Tool ID</returns>
        public int GetToolIDFromLine(string line)
        {
            return int.Parse(line.Split('T', ' ')[1]);
        }

        /// <summary>
        /// Inform that tool has been changed to continue
        /// </summary>
        public void ToolChanged()
        {
            DoToolChange = false;
            SendNextLine();
        }

        /// <summary>
        /// Check if line is tool change command
        /// </summary>
        /// <param name="line">line to be checked</param>
        /// <returns></returns>
        private bool LineIsToolChange(string line)
        {
            if (line.Contains("T"))
                return true;

            return false;
        }

        #endregion

        #region Probe

        private float PlateHeight = 0.0f, ProbeMoveUpAfterDistance = 10.0f;
        private int TouchCounter = 0;
        public bool ProbeInProgress { get; private set; } = false;

        public List<string> ProbeSteps;

        /// <summary>
        /// Probe command. Find workpiece top surface.
        /// </summary>
        /// <param name="distance">How far down Z axis will move ?</param>
        /// <param name="feedRate">How fast Z axis will move ?</param>
        /// <param name="plateHeight">Plate height is needed to set correct Z height.</param>
        public void ToutchThePlate(float plateHeight, float moveUpDistance)
        {
            PlateHeight = plateHeight;
            ProbeMoveUpAfterDistance = moveUpDistance;
            TouchCounter = 0;
            ProbeInProgress = true;

            SendLine(ProbeSteps[0], true);
        }

        private void ProbeNextStep()
        {
            TouchCounter++;

            if (TouchCounter >= ProbeSteps.Count)
                SetZHeight();
            else
                SendLine(ProbeSteps[TouchCounter], true);
        }

        private void SetZHeight()
        {
            ProbeInProgress = false;
            SendLine(string.Format("G10{0}L20Z{1}", GetWorkCoordinateSpace(CurrentWCS), PlateHeight), true);

            if (ProbeMoveUpAfterDistance < 0)
                ProbeMoveUpAfterDistance = -ProbeMoveUpAfterDistance;

            SendLine(string.Format("G1Z{0}F200", ProbeMoveUpAfterDistance), true);
        }

        /// <summary>
        /// Returns probe value from: [PRB:0.000,0.000,0.000:0]
        /// </summary>
        /// <param name="rxData"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        private float GetProbeValue(string rxData, eAxis axis)
        {
            switch(axis)
            {
                case eAxis.X:
                    return float.Parse(rxData.Split(':', ',')[1].Replace('.', ','));
                case eAxis.Y:
                    return float.Parse(rxData.Split(',', ',')[1].Replace('.', ','));
                case eAxis.Z:
                    return float.Parse(rxData.Split(',', ':')[3].Replace('.', ','));
                default:
                    return 0.0f;
            }
        }

        /// <summary>
        /// Check probe result
        /// </summary>
        /// <param name="rxData"></param>
        /// <returns></returns>
        private bool WasProbeSuccessful(string rxData)
        {
            return bool.Parse(rxData.Split(':', ']')[2]);
        }

        #endregion
    }
}
