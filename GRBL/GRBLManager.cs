using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

using GRBL.Controls;

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

            queryTimer = new Timer();
            queryTimer.Interval = TimerInterval;
            queryTimer.Tick += new EventHandler(queryTimer_Tick);

            JoggingTimer = new Timer();
            JoggingTimer.Interval = 1000;
            JoggingTimer.Tick += new EventHandler(JoggingTimer_Tick);

            ScannedGRBLSettings = new List<GRBLSetting>();
        }

        #region Console

        public RichTextBox ConsoleBox;

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
        private Timer queryTimer;

        private string RX_DATA;
        private bool Receiving = false;

        private int LockCounter = 0, LockTrigger = 3;
        private const int TimerInterval = 200;
        public bool ShowQuery = true;

        /// <summary>
        /// Check if serial is connected
        /// </summary>
        /// <returns>Connection status</returns>
        public bool IsSerialConnected()
        {
            return serialPort.IsOpen;
        }

        public void OpenSerialPort(PortData portData)
        {
            if (IsSerialConnected())
                CloseSerialPort();

            LockCounter = 0;
            RX_DATA = string.Empty;
            Receiving = false;

            portData.PortDataToSerialPort(serialPort);

            serialPort.Open();
            queryTimer.Start();
        }

        public void CloseSerialPort()
        {
            if(serialPort != null && IsSerialConnected())
            {
                queryTimer.Stop();
                serialPort.Close();
            }
        }

        public void SendLine(string line, bool sendToConsole)
        {
            if(IsSerialConnected() && line.Length > 0)
            {
                serialPort.Write(string.Format("{0}\r", FormattingLine(line)));

                if(sendToConsole)
                    LineToConsole(FormattingLine(line));
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

                Receiving = false;
                return;
            }

            //Errors
            if (RX_DATA.StartsWith("error"))
            {
                //Stop machine if error during file transfer


                Receiving = false;
                return;
            }

            //MSG, PRB, VER, OPT
            if (RX_DATA.Length > 0 && RX_DATA[0] == '[')
            {
                if (RX_DATA.Contains("MSG"))
                {

                }
                else if (RX_DATA.Contains("PRB"))
                {

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

        public void ScanGRBLSettings()
        {
            if(IsSerialConnected())
            {
                SettingsScan = true;
                ScannedGRBLSettings.Clear();
                SendLine("$$", false);
            }
        }

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

        #endregion

        #region Commands

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
            if (IsSerialConnected())
            {
                SendLine("$RST=$", true);
            }
        }

        /// <summary>
        /// Clears Work Coordinate Offsets G54-G59 and G28/30 from EEPROM
        /// </summary>
        public void RestoreGRBL_WCO()
        {
            if (IsSerialConnected())
            {
                SendLine("$RST=#", true);
            }
        }

        /// <summary>
        /// This will clear WCO coordinates G54-G59, G28/30 and all $$ settings, $# parameters, $N startup lines and also $I build info string.
        /// </summary>
        public void RestoreGRBL_EEPROM()
        {
            if (IsSerialConnected())
            {
                SendLine("$RST=*", true);
            }
        }

        #endregion

        #region Position

        public XYZ MPos;
        public XYZ WPos;
        public XYZ WCO;
        public float CurrentFeedRate = 0.0f;

        public float OverrideFeedRate = 100;
        public float OverrideRapid = 100;
        public float OverrideSpindle = 100;

        private void UpdatePosition(string rxData)
        {
            try
            {
                string rawPos = rxData.Split('|', '|')[1];
                string fs = "";
                if(rxData.Contains("Ov"))
                {
                    fs = rxData.Split('|', '|')[2];

                    string ov = rxData.Split('|', '>')[3];
                    OverrideFeedRate = float.Parse(ov.Split(':', ',')[1]);
                    OverrideRapid = float.Parse(ov.Split(',', ',')[1]);
                    OverrideSpindle = float.Parse(ov.Split(',').Last());
                }
                else if(rxData.Contains("FS"))
                {
                    fs = rxData.Split('|', '>')[2];
                }

                if(fs != "")
                {
                    fs = fs.Replace("FS:", string.Empty);
                    CurrentFeedRate = float.Parse(Converters.DotToFloat(fs));
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

        #endregion

        #region Jogging

        public JoggingKnob joggingKnob;
        public bool EnableJoggingKnob = true;
        private Timer JoggingTimer;
        private float JoggingAmmount = 1.5f;
        private float JoggingFeedRate = 500;
        private int JoggingInterval = 500;
        private bool Jogging = false, nextJog = false;

        public void InitializeJoggingKnob()
        {
            joggingKnob.StartJogging = new Action(StartJoggin);
            joggingKnob.StopJogging = new Action(StopJogging);
        }

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

        public void StopJogging()
        {
            JoggingTimer.Stop();
            Jogging = false;
            JogCancel();
        }

        private void Jog()
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

        private void JoggingTimer_Tick(object sender, EventArgs e)
        {
            Jog();
        }

        #endregion
    }
}
