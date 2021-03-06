﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GRBL;

namespace TestApp
{
    public partial class Form1 : Form, IGRBL
    {
        public GRBLManager GRBLFramework => GRBLManager.Instance;

        private PortData port;
        private OpenFileDialog ofd;
        private Form_ToolChange toolChangeWindow;

        private bool ToolChangeWindowVisible = false;

        public Form1()
        {
            InitializeComponent();

            toolChangeWindow = new Form_ToolChange();

            port = new PortData("COM3", 115200);

            ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "Select file";
            ofd.Filter = "NC (*.nc)|*.nc";


            GRBLFramework.ConsoleBox = richTextBox_console;
            GRBLFramework.CurrentForm = this;

            GRBLFramework.ShowQuery = false;

            GRBLFramework.SettingsScanFinished = new Action(ScannedSettings);
            GRBLFramework.VerOptScanFinished = new Action(ScannedVerOpt);

            GRBLFramework.joggingKnob = joggingKnob1;
            GRBLFramework.InitializeJoggingKnob();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GRBLFramework.CloseSerialPort();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            l_wpos_x.Text = GRBLFramework.WPos.X.ToString();
            l_wpos_y.Text = GRBLFramework.WPos.Y.ToString();
            l_wpos_z.Text = GRBLFramework.WPos.Z.ToString();

            l_mpos_x.Text = GRBLFramework.MPos.X.ToString();
            l_mpos_y.Text = GRBLFramework.MPos.Y.ToString();
            l_mpos_z.Text = GRBLFramework.MPos.Z.ToString();

            l_wcoX.Text = GRBLFramework.WCO.X.ToString();
            l_wcoY.Text = GRBLFramework.WCO.Y.ToString();
            l_wcoZ.Text = GRBLFramework.WCO.Z.ToString();

            labelPositioning.Text = GRBLFramework.Positioning == ePositioning.Absolute ? "Positioning: Absolute" : "Positioning: Incremental";

            l_currentFeedRate.Text = GRBLFramework.CurrentFeedRate.ToString();
            labelSpindleRPM.Text = GRBLFramework.currentSpindleSpeed.ToString();

            l_ovFeedRate.Text = GRBLFramework.OverrideFeedRate.ToString();
            l_ovRapid.Text = GRBLFramework.OverrideRapid.ToString();
            l_ovSpindle.Text = GRBLFramework.OverrideSpindle.ToString();

            label_machineStatus.Text = GRBLFramework.MachineState.ToString();

            visualizer1.UpdatePosition(new Point((int)GRBLFramework.WPos.X, (int)GRBLFramework.WPos.Y));

            if(GRBLFramework.SendingFile)
            {
                label_percentage.Text = string.Format("{0}%", GRBLFramework.FileSentPercentage);
                progressBar1.Value = (int)GRBLFramework.FileSentPercentage;
            }

            if(GRBLFramework.DoToolChange && GRBLFramework.MachineState == eMachineState.Idle && !ToolChangeWindowVisible)
            {
                ToolChangeWindowVisible = true;

                toolChangeWindow.ToolInfo = GRBLFramework.CurrentToolID.ToString();
                toolChangeWindow.ShowDialog();

                GRBLFramework.ToolChanged();

                ToolChangeWindowVisible = false;
            }
        }


        private void EnableButtons(bool enable)
        {
            btn_connect.Enabled = !enable;
            btn_disconnect.Enabled = enable;
            btn_reset.Enabled = enable;
            btn_send.Enabled = enable;
            btn_unlock.Enabled = enable;
            tb_sendCommand.Enabled = enable;
            btn_zeroAll.Enabled = enable;
            btn_zeroX.Enabled = enable;
            btn_zeroY.Enabled = enable;
            btn_zeroZ.Enabled = enable;
            cb_workSpaces.Enabled = enable;
            btn_scanSettings.Enabled = enable;
            groupBox1.Enabled = enable;
            joggingKnob1.Enabled = enable;
            btn_startResume.Enabled = enable;
            btn_hold.Enabled = enable;
            groupBox_moveButtons.Enabled = enable;

            btn_openFile.Enabled = enable;

            if(!enable)
            {
                btn_stopFile.Enabled = false;
                btn_sendFile.Enabled = false;
                btn_openFile.Enabled = false;
            }

            btn_touchThePlate.Enabled = enable;
        }

        #region Connection

        private void btn_connect_Click(object sender, EventArgs e)
        {
            comboBox_rapidAmmount.SelectedIndex = 0;
            //Open port
            GRBLFramework.OpenSerialPort(port);
            //Enable buttons
            EnableButtons(true);
            //Start timer to update position text
            timer1.Start();
            //Set work space
            cb_workSpaces.SelectedIndex = 0;

            //Reset override values
            GRBLFramework.FeedRateOverrideReset();
            GRBLFramework.SpindleOverrideReset();
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            timer1.Stop();

            GRBLFramework.CloseSerialPort();

            label_machineStatus.Text = "Disconnected";
        }

        #endregion

        #region Jogging

        private void checkBox_lockX_CheckedChanged(object sender, EventArgs e)
        {
            joggingKnob1.LockX = checkBox_lockX.Checked;
        }

        private void checkBox_lockY_CheckedChanged(object sender, EventArgs e)
        {
            joggingKnob1.LockY = checkBox_lockY.Checked;
        }

        #endregion

        #region Commands

        private void cb_workSpaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_workSpaces.SelectedIndex)
            {
                case 0:
                    GRBLFramework.SetCoordinateSystem(eP.P1);
                    break;
                case 1:
                    GRBLFramework.SetCoordinateSystem(eP.P2);
                    break;
                case 2:
                    GRBLFramework.SetCoordinateSystem(eP.P3);
                    break;
                case 3:
                    GRBLFramework.SetCoordinateSystem(eP.P4);
                    break;
                case 4:
                    GRBLFramework.SetCoordinateSystem(eP.P5);
                    break;
                case 5:
                    GRBLFramework.SetCoordinateSystem(eP.P6);
                    break;
            }
        }


        private void btn_startResume_Click(object sender, EventArgs e)
        {
            GRBLFramework.START_RESUME();
        }

        private void btn_hold_Click(object sender, EventArgs e)
        {
            GRBLFramework.HOLD();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            GRBLFramework.RESET();
        }

        private void btn_unlock_Click(object sender, EventArgs e)
        {
            GRBLFramework.UNLOCK();
        }


        private void btn_zeroX_Click(object sender, EventArgs e)
        {
            GRBLFramework.ZeroSingleAxis(eAxis.X);
        }

        private void btn_zeroY_Click(object sender, EventArgs e)
        {
            GRBLFramework.ZeroSingleAxis(eAxis.Y);
        }

        private void btn_zeroZ_Click(object sender, EventArgs e)
        {
            GRBLFramework.ZeroSingleAxis(eAxis.Z);
        }

        private void btn_zeroAll_Click(object sender, EventArgs e)
        {
            GRBLFramework.ZeroAllAxis();
        }


        private void btn_scanSettings_Click(object sender, EventArgs e)
        {
            listBox_settings.Items.Clear();
            GRBLFramework.ScanGRBLSettings();
            GRBLFramework.ScanVerOpt();
        }

        private void ScannedSettings()
        {
            foreach (GRBLSetting setting in GRBLFramework.ScannedGRBLSettings)
            {
                listBox_settings.Items.Add(setting.ToString());
            }
        }

        private void ScannedVerOpt()
        {
            l_ver.Text = string.Format("VER: {0}", GRBLFramework.ScannedVER);
            l_opt.Text = string.Format("OPT: {0}", GRBLFramework.ScannedOPT);
        }

        private void btn_touchThePlate_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Console

        private void checkBox_showQuery_CheckedChanged(object sender, EventArgs e)
        {
            GRBLFramework.ShowQuery = checkBox_showQuery.Checked;
        }

        private void tb_sendCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tb_sendCommand.Text.Length > 0)
            {
                GRBLFramework.SendLine(tb_sendCommand.Text, true);
                tb_sendCommand.Clear();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        #endregion

        #region Overrides

        private void btn_ovFeedRate_add_Click(object sender, EventArgs e)
        {
            GRBLFramework.FeedRateOverrideAdd(rb_one.Checked);
        }

        private void btn_ovFeedRate_remove_Click(object sender, EventArgs e)
        {
            GRBLFramework.FeedRateOverrideRemove(rb_one.Checked);
        }

        private void btn_ovSpindle_add_Click(object sender, EventArgs e)
        {
            GRBLFramework.SpindleOverrideAdd(rb_one.Checked);
        }

        private void btn_ovSpindle_remove_Click(object sender, EventArgs e)
        {
            GRBLFramework.SpindleOverrideRemove(rb_one.Checked);
        }

        private void comboBox_rapidAmmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            GRBLFramework.RapidOverride(comboBox_rapidAmmount.SelectedIndex);
        }

        #endregion

        #region Move

        private void btn_x0y0_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveToZero_TwoAxis();
        }

        private void btn_z0_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveToZero_SingleAxis(eAxis.Z);
        }


        private void btn_Xp_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.X, radioButton_g0.Checked, true,
                float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }

        private void btn_XpYp_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveTwoAxis(radioButton_g0.Checked, true,
                float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                int.Parse(textBox_feedRate.Text));
        }

        private void btn_Yp_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.Y, radioButton_g0.Checked, true,
                float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }

        private void btn_XmYp_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveTwoAxis(radioButton_g0.Checked, true,
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                int.Parse(textBox_feedRate.Text));
        }

        private void btn_Xm_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.X, radioButton_g0.Checked, true,
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }

        private void btn_XmYm_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveTwoAxis(radioButton_g0.Checked, true,
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                int.Parse(textBox_feedRate.Text));
        }

        private void btn_Ym_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.Y, radioButton_g0.Checked, true,
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }

        private void btn_XpYm_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveTwoAxis(radioButton_g0.Checked, true,
                float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)),
                int.Parse(textBox_feedRate.Text));
        }

        private void btn_Zp_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.Z, radioButton_g0.Checked, true,
                float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }

        private void btn_Zm_Click(object sender, EventArgs e)
        {
            GRBLFramework.MoveSingleAxis(eAxis.Z, radioButton_g0.Checked, true,
                -float.Parse(Converters.DotToFloat(textBox_distance.Text)), int.Parse(textBox_feedRate.Text));
        }


        private void textBox_feedRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_distance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }
        #endregion

        #region File

        private void btn_openFile_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                GRBLFramework.PrepareFile(ofd.FileName);

                List<string> lines = File.ReadLines(ofd.FileName).ToList();
                visualizer1.FileLines = lines;
                visualizer1.ReadLines();

                btn_sendFile.Enabled = true;
            }
        }

        private void btn_sendFile_Click(object sender, EventArgs e)
        {
            GRBLFramework.SendFile();

            btn_stopFile.Enabled = true;
            btn_sendFile.Enabled = false;
            btn_openFile.Enabled = false;
        }

        private void btn_stopFile_Click(object sender, EventArgs e)
        {
            GRBLFramework.StopFile();
            GRBLFramework.HOLD();

            btn_stopFile.Enabled = false;
            btn_sendFile.Enabled = true;
            btn_openFile.Enabled = true;
        }

        #endregion
    }
}
