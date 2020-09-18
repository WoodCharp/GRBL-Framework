namespace TestApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_machineStatus = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.richTextBox_console = new System.Windows.Forms.RichTextBox();
            this.tb_sendCommand = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_unlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.l_wpos_x = new System.Windows.Forms.Label();
            this.l_mpos_x = new System.Windows.Forms.Label();
            this.l_mpos_y = new System.Windows.Forms.Label();
            this.l_wpos_y = new System.Windows.Forms.Label();
            this.l_mpos_z = new System.Windows.Forms.Label();
            this.l_wpos_z = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cb_workSpaces = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_zeroX = new System.Windows.Forms.Button();
            this.btn_zeroY = new System.Windows.Forms.Button();
            this.btn_zeroZ = new System.Windows.Forms.Button();
            this.btn_zeroAll = new System.Windows.Forms.Button();
            this.btn_scanSettings = new System.Windows.Forms.Button();
            this.listBox_settings = new System.Windows.Forms.ListBox();
            this.checkBox_showQuery = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.l_currentFeedRate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.l_wcoZ = new System.Windows.Forms.Label();
            this.l_wcoY = new System.Windows.Forms.Label();
            this.l_wcoX = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_ten = new System.Windows.Forms.RadioButton();
            this.comboBox_rapidAmmount = new System.Windows.Forms.ComboBox();
            this.rb_one = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_ovSpindle_remove = new System.Windows.Forms.Button();
            this.btn_ovSpindle_add = new System.Windows.Forms.Button();
            this.btn_ovFeedRate_remove = new System.Windows.Forms.Button();
            this.btn_ovFeedRate_add = new System.Windows.Forms.Button();
            this.l_ovSpindle = new System.Windows.Forms.Label();
            this.l_ovRapid = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.l_ovFeedRate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_startResume = new System.Windows.Forms.Button();
            this.btn_hold = new System.Windows.Forms.Button();
            this.l_ver = new System.Windows.Forms.Label();
            this.l_opt = new System.Windows.Forms.Label();
            this.checkBox_lockX = new System.Windows.Forms.CheckBox();
            this.checkBox_lockY = new System.Windows.Forms.CheckBox();
            this.btn_Xm = new System.Windows.Forms.Button();
            this.btn_Xp = new System.Windows.Forms.Button();
            this.btn_XmYp = new System.Windows.Forms.Button();
            this.btn_Yp = new System.Windows.Forms.Button();
            this.btn_XpYp = new System.Windows.Forms.Button();
            this.btn_XmYm = new System.Windows.Forms.Button();
            this.btn_Ym = new System.Windows.Forms.Button();
            this.btn_XpYm = new System.Windows.Forms.Button();
            this.btn_Zm = new System.Windows.Forms.Button();
            this.btn_Zp = new System.Windows.Forms.Button();
            this.textBox_distance = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton_g0 = new System.Windows.Forms.RadioButton();
            this.radioButton_g1 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_feedRate = new System.Windows.Forms.TextBox();
            this.groupBox_moveButtons = new System.Windows.Forms.GroupBox();
            this.btn_z0 = new System.Windows.Forms.Button();
            this.btn_x0y0 = new System.Windows.Forms.Button();
            this.btn_openFile = new System.Windows.Forms.Button();
            this.btn_sendFile = new System.Windows.Forms.Button();
            this.btn_stopFile = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_percentage = new System.Windows.Forms.Label();
            this.btn_touchThePlate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.labelSpindleRPM = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelPositioning = new System.Windows.Forms.Label();
            this.visualizer1 = new GRBL.Controls.Visualizer();
            this.joggingKnob1 = new GRBL.Controls.JoggingKnob();
            this.groupBox1.SuspendLayout();
            this.groupBox_moveButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_machineStatus
            // 
            this.label_machineStatus.AutoSize = true;
            this.label_machineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_machineStatus.Location = new System.Drawing.Point(12, 9);
            this.label_machineStatus.Name = "label_machineStatus";
            this.label_machineStatus.Size = new System.Drawing.Size(213, 33);
            this.label_machineStatus.TabIndex = 0;
            this.label_machineStatus.Text = "Machine State";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(18, 45);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Enabled = false;
            this.btn_disconnect.Location = new System.Drawing.Point(99, 45);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(75, 23);
            this.btn_disconnect.TabIndex = 2;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // richTextBox_console
            // 
            this.richTextBox_console.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_console.Location = new System.Drawing.Point(326, 466);
            this.richTextBox_console.Name = "richTextBox_console";
            this.richTextBox_console.ReadOnly = true;
            this.richTextBox_console.Size = new System.Drawing.Size(410, 198);
            this.richTextBox_console.TabIndex = 3;
            this.richTextBox_console.Text = "";
            // 
            // tb_sendCommand
            // 
            this.tb_sendCommand.Enabled = false;
            this.tb_sendCommand.Location = new System.Drawing.Point(326, 670);
            this.tb_sendCommand.Name = "tb_sendCommand";
            this.tb_sendCommand.Size = new System.Drawing.Size(329, 20);
            this.tb_sendCommand.TabIndex = 4;
            this.tb_sendCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_sendCommand_KeyDown);
            // 
            // btn_send
            // 
            this.btn_send.Enabled = false;
            this.btn_send.Location = new System.Drawing.Point(661, 670);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            // 
            // btn_reset
            // 
            this.btn_reset.Enabled = false;
            this.btn_reset.Location = new System.Drawing.Point(180, 45);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 6;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_unlock
            // 
            this.btn_unlock.Enabled = false;
            this.btn_unlock.Location = new System.Drawing.Point(261, 45);
            this.btn_unlock.Name = "btn_unlock";
            this.btn_unlock.Size = new System.Drawing.Size(75, 23);
            this.btn_unlock.TabIndex = 7;
            this.btn_unlock.Text = "Unlock";
            this.btn_unlock.UseVisualStyleBackColor = true;
            this.btn_unlock.Click += new System.EventHandler(this.btn_unlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 33);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 33);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z";
            // 
            // l_wpos_x
            // 
            this.l_wpos_x.AutoSize = true;
            this.l_wpos_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wpos_x.Location = new System.Drawing.Point(53, 121);
            this.l_wpos_x.Name = "l_wpos_x";
            this.l_wpos_x.Size = new System.Drawing.Size(109, 33);
            this.l_wpos_x.TabIndex = 11;
            this.l_wpos_x.Text = "00.000";
            // 
            // l_mpos_x
            // 
            this.l_mpos_x.AutoSize = true;
            this.l_mpos_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_mpos_x.Location = new System.Drawing.Point(174, 121);
            this.l_mpos_x.Name = "l_mpos_x";
            this.l_mpos_x.Size = new System.Drawing.Size(109, 33);
            this.l_mpos_x.TabIndex = 12;
            this.l_mpos_x.Text = "00.000";
            // 
            // l_mpos_y
            // 
            this.l_mpos_y.AutoSize = true;
            this.l_mpos_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_mpos_y.Location = new System.Drawing.Point(174, 154);
            this.l_mpos_y.Name = "l_mpos_y";
            this.l_mpos_y.Size = new System.Drawing.Size(109, 33);
            this.l_mpos_y.TabIndex = 14;
            this.l_mpos_y.Text = "00.000";
            // 
            // l_wpos_y
            // 
            this.l_wpos_y.AutoSize = true;
            this.l_wpos_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wpos_y.Location = new System.Drawing.Point(53, 154);
            this.l_wpos_y.Name = "l_wpos_y";
            this.l_wpos_y.Size = new System.Drawing.Size(109, 33);
            this.l_wpos_y.TabIndex = 13;
            this.l_wpos_y.Text = "00.000";
            // 
            // l_mpos_z
            // 
            this.l_mpos_z.AutoSize = true;
            this.l_mpos_z.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_mpos_z.Location = new System.Drawing.Point(174, 187);
            this.l_mpos_z.Name = "l_mpos_z";
            this.l_mpos_z.Size = new System.Drawing.Size(109, 33);
            this.l_mpos_z.TabIndex = 16;
            this.l_mpos_z.Text = "00.000";
            // 
            // l_wpos_z
            // 
            this.l_wpos_z.AutoSize = true;
            this.l_wpos_z.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wpos_z.Location = new System.Drawing.Point(53, 187);
            this.l_wpos_z.Name = "l_wpos_z";
            this.l_wpos_z.Size = new System.Drawing.Size(109, 33);
            this.l_wpos_z.TabIndex = 15;
            this.l_wpos_z.Text = "00.000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(174, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 33);
            this.label10.TabIndex = 18;
            this.label10.Text = "MPOS";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(53, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 33);
            this.label11.TabIndex = 17;
            this.label11.Text = "WPOS";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cb_workSpaces
            // 
            this.cb_workSpaces.Enabled = false;
            this.cb_workSpaces.FormattingEnabled = true;
            this.cb_workSpaces.Items.AddRange(new object[] {
            "G54 (P1)",
            "G55 (P2)",
            "G56 (P3)",
            "G57 (P4)",
            "G58 (P5)",
            "G59 (P6)"});
            this.cb_workSpaces.Location = new System.Drawing.Point(358, 47);
            this.cb_workSpaces.Name = "cb_workSpaces";
            this.cb_workSpaces.Size = new System.Drawing.Size(121, 21);
            this.cb_workSpaces.TabIndex = 19;
            this.cb_workSpaces.SelectedIndexChanged += new System.EventHandler(this.cb_workSpaces_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(374, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Work Space";
            // 
            // btn_zeroX
            // 
            this.btn_zeroX.Enabled = false;
            this.btn_zeroX.Location = new System.Drawing.Point(404, 121);
            this.btn_zeroX.Name = "btn_zeroX";
            this.btn_zeroX.Size = new System.Drawing.Size(75, 30);
            this.btn_zeroX.TabIndex = 21;
            this.btn_zeroX.Text = "Zero";
            this.btn_zeroX.UseVisualStyleBackColor = true;
            this.btn_zeroX.Click += new System.EventHandler(this.btn_zeroX_Click);
            // 
            // btn_zeroY
            // 
            this.btn_zeroY.Enabled = false;
            this.btn_zeroY.Location = new System.Drawing.Point(404, 154);
            this.btn_zeroY.Name = "btn_zeroY";
            this.btn_zeroY.Size = new System.Drawing.Size(75, 30);
            this.btn_zeroY.TabIndex = 22;
            this.btn_zeroY.Text = "Zero";
            this.btn_zeroY.UseVisualStyleBackColor = true;
            this.btn_zeroY.Click += new System.EventHandler(this.btn_zeroY_Click);
            // 
            // btn_zeroZ
            // 
            this.btn_zeroZ.Enabled = false;
            this.btn_zeroZ.Location = new System.Drawing.Point(404, 190);
            this.btn_zeroZ.Name = "btn_zeroZ";
            this.btn_zeroZ.Size = new System.Drawing.Size(75, 30);
            this.btn_zeroZ.TabIndex = 23;
            this.btn_zeroZ.Text = "Zero";
            this.btn_zeroZ.UseVisualStyleBackColor = true;
            this.btn_zeroZ.Click += new System.EventHandler(this.btn_zeroZ_Click);
            // 
            // btn_zeroAll
            // 
            this.btn_zeroAll.Enabled = false;
            this.btn_zeroAll.Location = new System.Drawing.Point(404, 85);
            this.btn_zeroAll.Name = "btn_zeroAll";
            this.btn_zeroAll.Size = new System.Drawing.Size(75, 30);
            this.btn_zeroAll.TabIndex = 24;
            this.btn_zeroAll.Text = "Zero All";
            this.btn_zeroAll.UseVisualStyleBackColor = true;
            this.btn_zeroAll.Click += new System.EventHandler(this.btn_zeroAll_Click);
            // 
            // btn_scanSettings
            // 
            this.btn_scanSettings.Enabled = false;
            this.btn_scanSettings.Location = new System.Drawing.Point(5, 631);
            this.btn_scanSettings.Name = "btn_scanSettings";
            this.btn_scanSettings.Size = new System.Drawing.Size(175, 26);
            this.btn_scanSettings.TabIndex = 25;
            this.btn_scanSettings.Text = "Scan Settings";
            this.btn_scanSettings.UseVisualStyleBackColor = true;
            this.btn_scanSettings.Click += new System.EventHandler(this.btn_scanSettings_Click);
            // 
            // listBox_settings
            // 
            this.listBox_settings.FormattingEnabled = true;
            this.listBox_settings.Location = new System.Drawing.Point(5, 466);
            this.listBox_settings.Name = "listBox_settings";
            this.listBox_settings.Size = new System.Drawing.Size(175, 160);
            this.listBox_settings.TabIndex = 26;
            // 
            // checkBox_showQuery
            // 
            this.checkBox_showQuery.AutoSize = true;
            this.checkBox_showQuery.Location = new System.Drawing.Point(326, 440);
            this.checkBox_showQuery.Name = "checkBox_showQuery";
            this.checkBox_showQuery.Size = new System.Drawing.Size(84, 17);
            this.checkBox_showQuery.TabIndex = 27;
            this.checkBox_showQuery.Text = "Show Query";
            this.checkBox_showQuery.UseVisualStyleBackColor = true;
            this.checkBox_showQuery.CheckedChanged += new System.EventHandler(this.checkBox_showQuery_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-1, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 33);
            this.label5.TabIndex = 28;
            this.label5.Text = "FS";
            // 
            // l_currentFeedRate
            // 
            this.l_currentFeedRate.AutoSize = true;
            this.l_currentFeedRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_currentFeedRate.Location = new System.Drawing.Point(53, 220);
            this.l_currentFeedRate.Name = "l_currentFeedRate";
            this.l_currentFeedRate.Size = new System.Drawing.Size(109, 33);
            this.l_currentFeedRate.TabIndex = 29;
            this.l_currentFeedRate.Text = "00.000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(289, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 33);
            this.label6.TabIndex = 33;
            this.label6.Text = "WCO";
            // 
            // l_wcoZ
            // 
            this.l_wcoZ.AutoSize = true;
            this.l_wcoZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wcoZ.Location = new System.Drawing.Point(289, 187);
            this.l_wcoZ.Name = "l_wcoZ";
            this.l_wcoZ.Size = new System.Drawing.Size(109, 33);
            this.l_wcoZ.TabIndex = 32;
            this.l_wcoZ.Text = "00.000";
            // 
            // l_wcoY
            // 
            this.l_wcoY.AutoSize = true;
            this.l_wcoY.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wcoY.Location = new System.Drawing.Point(289, 154);
            this.l_wcoY.Name = "l_wcoY";
            this.l_wcoY.Size = new System.Drawing.Size(109, 33);
            this.l_wcoY.TabIndex = 31;
            this.l_wcoY.Text = "00.000";
            // 
            // l_wcoX
            // 
            this.l_wcoX.AutoSize = true;
            this.l_wcoX.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_wcoX.Location = new System.Drawing.Point(289, 121);
            this.l_wcoX.Name = "l_wcoX";
            this.l_wcoX.Size = new System.Drawing.Size(109, 33);
            this.l_wcoX.TabIndex = 30;
            this.l_wcoX.Text = "00.000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_ten);
            this.groupBox1.Controls.Add(this.comboBox_rapidAmmount);
            this.groupBox1.Controls.Add(this.rb_one);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.btn_ovSpindle_remove);
            this.groupBox1.Controls.Add(this.btn_ovSpindle_add);
            this.groupBox1.Controls.Add(this.btn_ovFeedRate_remove);
            this.groupBox1.Controls.Add(this.btn_ovFeedRate_add);
            this.groupBox1.Controls.Add(this.l_ovSpindle);
            this.groupBox1.Controls.Add(this.l_ovRapid);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.l_ovFeedRate);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 256);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 204);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Overrides";
            // 
            // rb_ten
            // 
            this.rb_ten.AutoSize = true;
            this.rb_ten.Location = new System.Drawing.Point(237, 92);
            this.rb_ten.Name = "rb_ten";
            this.rb_ten.Size = new System.Drawing.Size(40, 20);
            this.rb_ten.TabIndex = 36;
            this.rb_ten.Text = "10";
            this.rb_ten.UseVisualStyleBackColor = true;
            // 
            // comboBox_rapidAmmount
            // 
            this.comboBox_rapidAmmount.FormattingEnabled = true;
            this.comboBox_rapidAmmount.Items.AddRange(new object[] {
            "100",
            "50",
            "25"});
            this.comboBox_rapidAmmount.Location = new System.Drawing.Point(198, 158);
            this.comboBox_rapidAmmount.Name = "comboBox_rapidAmmount";
            this.comboBox_rapidAmmount.Size = new System.Drawing.Size(79, 24);
            this.comboBox_rapidAmmount.TabIndex = 37;
            this.comboBox_rapidAmmount.SelectedIndexChanged += new System.EventHandler(this.comboBox_rapidAmmount_SelectedIndexChanged);
            // 
            // rb_one
            // 
            this.rb_one.AutoSize = true;
            this.rb_one.Checked = true;
            this.rb_one.Location = new System.Drawing.Point(198, 92);
            this.rb_one.Name = "rb_one";
            this.rb_one.Size = new System.Drawing.Size(33, 20);
            this.rb_one.TabIndex = 35;
            this.rb_one.TabStop = true;
            this.rb_one.Text = "1";
            this.rb_one.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(66, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 20);
            this.label18.TabIndex = 30;
            this.label18.Text = "Ammount";
            // 
            // btn_ovSpindle_remove
            // 
            this.btn_ovSpindle_remove.Location = new System.Drawing.Point(250, 61);
            this.btn_ovSpindle_remove.Name = "btn_ovSpindle_remove";
            this.btn_ovSpindle_remove.Size = new System.Drawing.Size(25, 25);
            this.btn_ovSpindle_remove.TabIndex = 29;
            this.btn_ovSpindle_remove.Text = "-";
            this.btn_ovSpindle_remove.UseVisualStyleBackColor = true;
            this.btn_ovSpindle_remove.Click += new System.EventHandler(this.btn_ovSpindle_remove_Click);
            // 
            // btn_ovSpindle_add
            // 
            this.btn_ovSpindle_add.Location = new System.Drawing.Point(219, 61);
            this.btn_ovSpindle_add.Name = "btn_ovSpindle_add";
            this.btn_ovSpindle_add.Size = new System.Drawing.Size(25, 25);
            this.btn_ovSpindle_add.TabIndex = 28;
            this.btn_ovSpindle_add.Text = "+";
            this.btn_ovSpindle_add.UseVisualStyleBackColor = true;
            this.btn_ovSpindle_add.Click += new System.EventHandler(this.btn_ovSpindle_add_Click);
            // 
            // btn_ovFeedRate_remove
            // 
            this.btn_ovFeedRate_remove.Location = new System.Drawing.Point(250, 30);
            this.btn_ovFeedRate_remove.Name = "btn_ovFeedRate_remove";
            this.btn_ovFeedRate_remove.Size = new System.Drawing.Size(25, 25);
            this.btn_ovFeedRate_remove.TabIndex = 25;
            this.btn_ovFeedRate_remove.Text = "-";
            this.btn_ovFeedRate_remove.UseVisualStyleBackColor = true;
            this.btn_ovFeedRate_remove.Click += new System.EventHandler(this.btn_ovFeedRate_remove_Click);
            // 
            // btn_ovFeedRate_add
            // 
            this.btn_ovFeedRate_add.Location = new System.Drawing.Point(219, 30);
            this.btn_ovFeedRate_add.Name = "btn_ovFeedRate_add";
            this.btn_ovFeedRate_add.Size = new System.Drawing.Size(25, 25);
            this.btn_ovFeedRate_add.TabIndex = 24;
            this.btn_ovFeedRate_add.Text = "+";
            this.btn_ovFeedRate_add.UseVisualStyleBackColor = true;
            this.btn_ovFeedRate_add.Click += new System.EventHandler(this.btn_ovFeedRate_add_Click);
            // 
            // l_ovSpindle
            // 
            this.l_ovSpindle.AutoSize = true;
            this.l_ovSpindle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_ovSpindle.Location = new System.Drawing.Point(135, 61);
            this.l_ovSpindle.Name = "l_ovSpindle";
            this.l_ovSpindle.Size = new System.Drawing.Size(21, 25);
            this.l_ovSpindle.TabIndex = 17;
            this.l_ovSpindle.Text = "*";
            // 
            // l_ovRapid
            // 
            this.l_ovRapid.AutoSize = true;
            this.l_ovRapid.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_ovRapid.Location = new System.Drawing.Point(135, 158);
            this.l_ovRapid.Name = "l_ovRapid";
            this.l_ovRapid.Size = new System.Drawing.Size(21, 25);
            this.l_ovRapid.TabIndex = 16;
            this.l_ovRapid.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(7, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 25);
            this.label15.TabIndex = 15;
            this.label15.Text = "Spindle";
            // 
            // l_ovFeedRate
            // 
            this.l_ovFeedRate.AutoSize = true;
            this.l_ovFeedRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_ovFeedRate.Location = new System.Drawing.Point(135, 30);
            this.l_ovFeedRate.Name = "l_ovFeedRate";
            this.l_ovFeedRate.Size = new System.Drawing.Size(21, 25);
            this.l_ovFeedRate.TabIndex = 13;
            this.l_ovFeedRate.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 25);
            this.label14.TabIndex = 14;
            this.label14.Text = "Rapid";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 25);
            this.label13.TabIndex = 12;
            this.label13.Text = "Feed Rate";
            // 
            // btn_startResume
            // 
            this.btn_startResume.Enabled = false;
            this.btn_startResume.Location = new System.Drawing.Point(186, 466);
            this.btn_startResume.Name = "btn_startResume";
            this.btn_startResume.Size = new System.Drawing.Size(134, 30);
            this.btn_startResume.TabIndex = 37;
            this.btn_startResume.Text = "Start/Resume";
            this.btn_startResume.UseVisualStyleBackColor = true;
            this.btn_startResume.Click += new System.EventHandler(this.btn_startResume_Click);
            // 
            // btn_hold
            // 
            this.btn_hold.Enabled = false;
            this.btn_hold.Location = new System.Drawing.Point(186, 502);
            this.btn_hold.Name = "btn_hold";
            this.btn_hold.Size = new System.Drawing.Size(134, 30);
            this.btn_hold.TabIndex = 38;
            this.btn_hold.Text = "Hold";
            this.btn_hold.UseVisualStyleBackColor = true;
            this.btn_hold.Click += new System.EventHandler(this.btn_hold_Click);
            // 
            // l_ver
            // 
            this.l_ver.AutoSize = true;
            this.l_ver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_ver.Location = new System.Drawing.Point(3, 660);
            this.l_ver.Name = "l_ver";
            this.l_ver.Size = new System.Drawing.Size(49, 16);
            this.l_ver.TabIndex = 39;
            this.l_ver.Text = "VER:*";
            // 
            // l_opt
            // 
            this.l_opt.AutoSize = true;
            this.l_opt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_opt.Location = new System.Drawing.Point(3, 680);
            this.l_opt.Name = "l_opt";
            this.l_opt.Size = new System.Drawing.Size(49, 16);
            this.l_opt.TabIndex = 40;
            this.l_opt.Text = "OPT:*";
            // 
            // checkBox_lockX
            // 
            this.checkBox_lockX.AutoSize = true;
            this.checkBox_lockX.Location = new System.Drawing.Point(510, 420);
            this.checkBox_lockX.Name = "checkBox_lockX";
            this.checkBox_lockX.Size = new System.Drawing.Size(60, 17);
            this.checkBox_lockX.TabIndex = 42;
            this.checkBox_lockX.Text = "Lock X";
            this.checkBox_lockX.UseVisualStyleBackColor = true;
            this.checkBox_lockX.CheckedChanged += new System.EventHandler(this.checkBox_lockX_CheckedChanged);
            // 
            // checkBox_lockY
            // 
            this.checkBox_lockY.AutoSize = true;
            this.checkBox_lockY.Location = new System.Drawing.Point(510, 443);
            this.checkBox_lockY.Name = "checkBox_lockY";
            this.checkBox_lockY.Size = new System.Drawing.Size(60, 17);
            this.checkBox_lockY.TabIndex = 43;
            this.checkBox_lockY.Text = "Lock Y";
            this.checkBox_lockY.UseVisualStyleBackColor = true;
            this.checkBox_lockY.CheckedChanged += new System.EventHandler(this.checkBox_lockY_CheckedChanged);
            // 
            // btn_Xm
            // 
            this.btn_Xm.Location = new System.Drawing.Point(6, 86);
            this.btn_Xm.Name = "btn_Xm";
            this.btn_Xm.Size = new System.Drawing.Size(50, 50);
            this.btn_Xm.TabIndex = 45;
            this.btn_Xm.Text = "X-";
            this.btn_Xm.UseVisualStyleBackColor = true;
            this.btn_Xm.Click += new System.EventHandler(this.btn_Xm_Click);
            // 
            // btn_Xp
            // 
            this.btn_Xp.Location = new System.Drawing.Point(119, 86);
            this.btn_Xp.Name = "btn_Xp";
            this.btn_Xp.Size = new System.Drawing.Size(50, 50);
            this.btn_Xp.TabIndex = 46;
            this.btn_Xp.Text = "X+";
            this.btn_Xp.UseVisualStyleBackColor = true;
            this.btn_Xp.Click += new System.EventHandler(this.btn_Xp_Click);
            // 
            // btn_XmYp
            // 
            this.btn_XmYp.Location = new System.Drawing.Point(6, 30);
            this.btn_XmYp.Name = "btn_XmYp";
            this.btn_XmYp.Size = new System.Drawing.Size(50, 50);
            this.btn_XmYp.TabIndex = 47;
            this.btn_XmYp.Text = "X-Y+";
            this.btn_XmYp.UseVisualStyleBackColor = true;
            this.btn_XmYp.Click += new System.EventHandler(this.btn_XmYp_Click);
            // 
            // btn_Yp
            // 
            this.btn_Yp.Location = new System.Drawing.Point(62, 30);
            this.btn_Yp.Name = "btn_Yp";
            this.btn_Yp.Size = new System.Drawing.Size(50, 50);
            this.btn_Yp.TabIndex = 48;
            this.btn_Yp.Text = "Y+";
            this.btn_Yp.UseVisualStyleBackColor = true;
            this.btn_Yp.Click += new System.EventHandler(this.btn_Yp_Click);
            // 
            // btn_XpYp
            // 
            this.btn_XpYp.Location = new System.Drawing.Point(119, 30);
            this.btn_XpYp.Name = "btn_XpYp";
            this.btn_XpYp.Size = new System.Drawing.Size(50, 50);
            this.btn_XpYp.TabIndex = 49;
            this.btn_XpYp.Text = "X+Y+";
            this.btn_XpYp.UseVisualStyleBackColor = true;
            this.btn_XpYp.Click += new System.EventHandler(this.btn_XpYp_Click);
            // 
            // btn_XmYm
            // 
            this.btn_XmYm.Location = new System.Drawing.Point(6, 142);
            this.btn_XmYm.Name = "btn_XmYm";
            this.btn_XmYm.Size = new System.Drawing.Size(50, 50);
            this.btn_XmYm.TabIndex = 50;
            this.btn_XmYm.Text = "X-Y-";
            this.btn_XmYm.UseVisualStyleBackColor = true;
            this.btn_XmYm.Click += new System.EventHandler(this.btn_XmYm_Click);
            // 
            // btn_Ym
            // 
            this.btn_Ym.Location = new System.Drawing.Point(62, 142);
            this.btn_Ym.Name = "btn_Ym";
            this.btn_Ym.Size = new System.Drawing.Size(50, 50);
            this.btn_Ym.TabIndex = 51;
            this.btn_Ym.Text = "Y-";
            this.btn_Ym.UseVisualStyleBackColor = true;
            this.btn_Ym.Click += new System.EventHandler(this.btn_Ym_Click);
            // 
            // btn_XpYm
            // 
            this.btn_XpYm.Location = new System.Drawing.Point(119, 142);
            this.btn_XpYm.Name = "btn_XpYm";
            this.btn_XpYm.Size = new System.Drawing.Size(50, 50);
            this.btn_XpYm.TabIndex = 52;
            this.btn_XpYm.Text = "X+Y-";
            this.btn_XpYm.UseVisualStyleBackColor = true;
            this.btn_XpYm.Click += new System.EventHandler(this.btn_XpYm_Click);
            // 
            // btn_Zm
            // 
            this.btn_Zm.Location = new System.Drawing.Point(175, 142);
            this.btn_Zm.Name = "btn_Zm";
            this.btn_Zm.Size = new System.Drawing.Size(50, 50);
            this.btn_Zm.TabIndex = 54;
            this.btn_Zm.Text = "Z-";
            this.btn_Zm.UseVisualStyleBackColor = true;
            this.btn_Zm.Click += new System.EventHandler(this.btn_Zm_Click);
            // 
            // btn_Zp
            // 
            this.btn_Zp.Location = new System.Drawing.Point(175, 30);
            this.btn_Zp.Name = "btn_Zp";
            this.btn_Zp.Size = new System.Drawing.Size(50, 50);
            this.btn_Zp.TabIndex = 53;
            this.btn_Zp.Text = "Z+";
            this.btn_Zp.UseVisualStyleBackColor = true;
            this.btn_Zp.Click += new System.EventHandler(this.btn_Zp_Click);
            // 
            // textBox_distance
            // 
            this.textBox_distance.Location = new System.Drawing.Point(125, 198);
            this.textBox_distance.Name = "textBox_distance";
            this.textBox_distance.Size = new System.Drawing.Size(100, 20);
            this.textBox_distance.TabIndex = 55;
            this.textBox_distance.Text = "10";
            this.textBox_distance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_distance_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 56;
            this.label7.Text = "Distance";
            // 
            // radioButton_g0
            // 
            this.radioButton_g0.AutoSize = true;
            this.radioButton_g0.Checked = true;
            this.radioButton_g0.Location = new System.Drawing.Point(25, 201);
            this.radioButton_g0.Name = "radioButton_g0";
            this.radioButton_g0.Size = new System.Drawing.Size(39, 17);
            this.radioButton_g0.TabIndex = 57;
            this.radioButton_g0.TabStop = true;
            this.radioButton_g0.Text = "G0";
            this.radioButton_g0.UseVisualStyleBackColor = true;
            // 
            // radioButton_g1
            // 
            this.radioButton_g1.AutoSize = true;
            this.radioButton_g1.Location = new System.Drawing.Point(25, 225);
            this.radioButton_g1.Name = "radioButton_g1";
            this.radioButton_g1.Size = new System.Drawing.Size(39, 17);
            this.radioButton_g1.TabIndex = 58;
            this.radioButton_g1.Text = "G1";
            this.radioButton_g1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "Feed rate";
            // 
            // textBox_feedRate
            // 
            this.textBox_feedRate.Location = new System.Drawing.Point(125, 224);
            this.textBox_feedRate.Name = "textBox_feedRate";
            this.textBox_feedRate.Size = new System.Drawing.Size(100, 20);
            this.textBox_feedRate.TabIndex = 59;
            this.textBox_feedRate.Text = "250";
            this.textBox_feedRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_feedRate_KeyPress);
            // 
            // groupBox_moveButtons
            // 
            this.groupBox_moveButtons.Controls.Add(this.btn_z0);
            this.groupBox_moveButtons.Controls.Add(this.btn_x0y0);
            this.groupBox_moveButtons.Controls.Add(this.btn_XmYp);
            this.groupBox_moveButtons.Controls.Add(this.label8);
            this.groupBox_moveButtons.Controls.Add(this.btn_Xm);
            this.groupBox_moveButtons.Controls.Add(this.textBox_feedRate);
            this.groupBox_moveButtons.Controls.Add(this.btn_Xp);
            this.groupBox_moveButtons.Controls.Add(this.radioButton_g1);
            this.groupBox_moveButtons.Controls.Add(this.btn_Yp);
            this.groupBox_moveButtons.Controls.Add(this.radioButton_g0);
            this.groupBox_moveButtons.Controls.Add(this.btn_XpYp);
            this.groupBox_moveButtons.Controls.Add(this.label7);
            this.groupBox_moveButtons.Controls.Add(this.btn_XmYm);
            this.groupBox_moveButtons.Controls.Add(this.textBox_distance);
            this.groupBox_moveButtons.Controls.Add(this.btn_Ym);
            this.groupBox_moveButtons.Controls.Add(this.btn_Zm);
            this.groupBox_moveButtons.Controls.Add(this.btn_XpYm);
            this.groupBox_moveButtons.Controls.Add(this.btn_Zp);
            this.groupBox_moveButtons.Location = new System.Drawing.Point(485, 26);
            this.groupBox_moveButtons.Name = "groupBox_moveButtons";
            this.groupBox_moveButtons.Size = new System.Drawing.Size(251, 265);
            this.groupBox_moveButtons.TabIndex = 61;
            this.groupBox_moveButtons.TabStop = false;
            this.groupBox_moveButtons.Text = "Move";
            // 
            // btn_z0
            // 
            this.btn_z0.Location = new System.Drawing.Point(175, 86);
            this.btn_z0.Name = "btn_z0";
            this.btn_z0.Size = new System.Drawing.Size(50, 50);
            this.btn_z0.TabIndex = 62;
            this.btn_z0.Text = "G0 Z0";
            this.btn_z0.UseVisualStyleBackColor = true;
            this.btn_z0.Click += new System.EventHandler(this.btn_z0_Click);
            // 
            // btn_x0y0
            // 
            this.btn_x0y0.Location = new System.Drawing.Point(62, 85);
            this.btn_x0y0.Name = "btn_x0y0";
            this.btn_x0y0.Size = new System.Drawing.Size(50, 50);
            this.btn_x0y0.TabIndex = 61;
            this.btn_x0y0.Text = "G0 X0 Y0";
            this.btn_x0y0.UseVisualStyleBackColor = true;
            this.btn_x0y0.Click += new System.EventHandler(this.btn_x0y0_Click);
            // 
            // btn_openFile
            // 
            this.btn_openFile.Enabled = false;
            this.btn_openFile.Location = new System.Drawing.Point(326, 373);
            this.btn_openFile.Name = "btn_openFile";
            this.btn_openFile.Size = new System.Drawing.Size(75, 30);
            this.btn_openFile.TabIndex = 62;
            this.btn_openFile.Text = "Open File";
            this.btn_openFile.UseVisualStyleBackColor = true;
            this.btn_openFile.Click += new System.EventHandler(this.btn_openFile_Click);
            // 
            // btn_sendFile
            // 
            this.btn_sendFile.Enabled = false;
            this.btn_sendFile.Location = new System.Drawing.Point(411, 373);
            this.btn_sendFile.Name = "btn_sendFile";
            this.btn_sendFile.Size = new System.Drawing.Size(75, 30);
            this.btn_sendFile.TabIndex = 63;
            this.btn_sendFile.Text = "Send File";
            this.btn_sendFile.UseVisualStyleBackColor = true;
            this.btn_sendFile.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // btn_stopFile
            // 
            this.btn_stopFile.Enabled = false;
            this.btn_stopFile.Location = new System.Drawing.Point(495, 373);
            this.btn_stopFile.Name = "btn_stopFile";
            this.btn_stopFile.Size = new System.Drawing.Size(75, 30);
            this.btn_stopFile.TabIndex = 64;
            this.btn_stopFile.Text = "Stop File";
            this.btn_stopFile.UseVisualStyleBackColor = true;
            this.btn_stopFile.Click += new System.EventHandler(this.btn_stopFile_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(326, 348);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(244, 23);
            this.progressBar1.TabIndex = 65;
            // 
            // label_percentage
            // 
            this.label_percentage.AutoSize = true;
            this.label_percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_percentage.Location = new System.Drawing.Point(326, 329);
            this.label_percentage.Name = "label_percentage";
            this.label_percentage.Size = new System.Drawing.Size(21, 16);
            this.label_percentage.TabIndex = 66;
            this.label_percentage.Text = "%";
            // 
            // btn_touchThePlate
            // 
            this.btn_touchThePlate.Enabled = false;
            this.btn_touchThePlate.Location = new System.Drawing.Point(326, 261);
            this.btn_touchThePlate.Name = "btn_touchThePlate";
            this.btn_touchThePlate.Size = new System.Drawing.Size(153, 30);
            this.btn_touchThePlate.TabIndex = 67;
            this.btn_touchThePlate.Text = "Touch The Plate";
            this.btn_touchThePlate.UseVisualStyleBackColor = true;
            this.btn_touchThePlate.Click += new System.EventHandler(this.btn_touchThePlate_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(182, 630);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 36);
            this.label9.TabIndex = 69;
            this.label9.Text = "<- Do this before\r\nsending file";
            // 
            // labelSpindleRPM
            // 
            this.labelSpindleRPM.AutoSize = true;
            this.labelSpindleRPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpindleRPM.Location = new System.Drawing.Point(289, 220);
            this.labelSpindleRPM.Name = "labelSpindleRPM";
            this.labelSpindleRPM.Size = new System.Drawing.Size(109, 33);
            this.labelSpindleRPM.TabIndex = 71;
            this.labelSpindleRPM.Text = "00.000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(255, 220);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 33);
            this.label16.TabIndex = 70;
            this.label16.Text = "S";
            // 
            // labelPositioning
            // 
            this.labelPositioning.AutoSize = true;
            this.labelPositioning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPositioning.Location = new System.Drawing.Point(326, 297);
            this.labelPositioning.Name = "labelPositioning";
            this.labelPositioning.Size = new System.Drawing.Size(114, 20);
            this.labelPositioning.TabIndex = 38;
            this.labelPositioning.Text = "Positioning: *";
            // 
            // visualizer1
            // 
            this.visualizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualizer1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.visualizer1.DimensionColor = System.Drawing.Color.Gray;
            this.visualizer1.DrawColor = System.Drawing.Color.Black;
            this.visualizer1.FileLines = null;
            this.visualizer1.Location = new System.Drawing.Point(742, 9);
            this.visualizer1.Name = "visualizer1";
            this.visualizer1.PathColor = System.Drawing.Color.Blue;
            this.visualizer1.Size = new System.Drawing.Size(701, 684);
            this.visualizer1.TabIndex = 72;
            this.visualizer1.Text = "visualizer1";
            this.visualizer1.ToolColor = System.Drawing.Color.DarkRed;
            this.visualizer1.ToolDiameter = 6;
            this.visualizer1.VisualizerScale = 10;
            // 
            // joggingKnob1
            // 
            this.joggingKnob1.Enabled = false;
            this.joggingKnob1.Location = new System.Drawing.Point(576, 297);
            this.joggingKnob1.Name = "joggingKnob1";
            this.joggingKnob1.Size = new System.Drawing.Size(160, 160);
            this.joggingKnob1.TabIndex = 35;
            this.joggingKnob1.Text = "joggingKnob1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 705);
            this.Controls.Add(this.visualizer1);
            this.Controls.Add(this.labelPositioning);
            this.Controls.Add(this.labelSpindleRPM);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btn_touchThePlate);
            this.Controls.Add(this.label_percentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_stopFile);
            this.Controls.Add(this.btn_sendFile);
            this.Controls.Add(this.btn_openFile);
            this.Controls.Add(this.groupBox_moveButtons);
            this.Controls.Add(this.checkBox_lockY);
            this.Controls.Add(this.checkBox_lockX);
            this.Controls.Add(this.l_opt);
            this.Controls.Add(this.l_ver);
            this.Controls.Add(this.btn_hold);
            this.Controls.Add(this.btn_startResume);
            this.Controls.Add(this.joggingKnob1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.l_wcoZ);
            this.Controls.Add(this.l_wcoY);
            this.Controls.Add(this.l_wcoX);
            this.Controls.Add(this.l_currentFeedRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox_showQuery);
            this.Controls.Add(this.listBox_settings);
            this.Controls.Add(this.btn_scanSettings);
            this.Controls.Add(this.btn_zeroAll);
            this.Controls.Add(this.btn_zeroZ);
            this.Controls.Add(this.btn_zeroY);
            this.Controls.Add(this.btn_zeroX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_workSpaces);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.l_mpos_z);
            this.Controls.Add(this.l_wpos_z);
            this.Controls.Add(this.l_mpos_y);
            this.Controls.Add(this.l_wpos_y);
            this.Controls.Add(this.l_mpos_x);
            this.Controls.Add(this.l_wpos_x);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_unlock);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_sendCommand);
            this.Controls.Add(this.richTextBox_console);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.label_machineStatus);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_moveButtons.ResumeLayout(false);
            this.groupBox_moveButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_machineStatus;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.RichTextBox richTextBox_console;
        private System.Windows.Forms.TextBox tb_sendCommand;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_unlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label l_wpos_x;
        private System.Windows.Forms.Label l_mpos_x;
        private System.Windows.Forms.Label l_mpos_y;
        private System.Windows.Forms.Label l_wpos_y;
        private System.Windows.Forms.Label l_mpos_z;
        private System.Windows.Forms.Label l_wpos_z;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cb_workSpaces;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_zeroX;
        private System.Windows.Forms.Button btn_zeroY;
        private System.Windows.Forms.Button btn_zeroZ;
        private System.Windows.Forms.Button btn_zeroAll;
        private System.Windows.Forms.Button btn_scanSettings;
        private System.Windows.Forms.ListBox listBox_settings;
        private System.Windows.Forms.CheckBox checkBox_showQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label l_currentFeedRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label l_wcoZ;
        private System.Windows.Forms.Label l_wcoY;
        private System.Windows.Forms.Label l_wcoX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label l_ovFeedRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label l_ovSpindle;
        private System.Windows.Forms.Label l_ovRapid;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_ovSpindle_remove;
        private System.Windows.Forms.Button btn_ovSpindle_add;
        private System.Windows.Forms.Button btn_ovFeedRate_remove;
        private System.Windows.Forms.Button btn_ovFeedRate_add;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rb_ten;
        private System.Windows.Forms.RadioButton rb_one;
        private System.Windows.Forms.ComboBox comboBox_rapidAmmount;
        private GRBL.Controls.JoggingKnob joggingKnob1;
        private System.Windows.Forms.Button btn_startResume;
        private System.Windows.Forms.Button btn_hold;
        private System.Windows.Forms.Label l_ver;
        private System.Windows.Forms.Label l_opt;
        private System.Windows.Forms.CheckBox checkBox_lockX;
        private System.Windows.Forms.CheckBox checkBox_lockY;
        private System.Windows.Forms.Button btn_Xm;
        private System.Windows.Forms.Button btn_Xp;
        private System.Windows.Forms.Button btn_XmYp;
        private System.Windows.Forms.Button btn_Yp;
        private System.Windows.Forms.Button btn_XpYp;
        private System.Windows.Forms.Button btn_XmYm;
        private System.Windows.Forms.Button btn_Ym;
        private System.Windows.Forms.Button btn_XpYm;
        private System.Windows.Forms.Button btn_Zm;
        private System.Windows.Forms.Button btn_Zp;
        private System.Windows.Forms.TextBox textBox_distance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton_g0;
        private System.Windows.Forms.RadioButton radioButton_g1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_feedRate;
        private System.Windows.Forms.GroupBox groupBox_moveButtons;
        private System.Windows.Forms.Button btn_z0;
        private System.Windows.Forms.Button btn_x0y0;
        private System.Windows.Forms.Button btn_openFile;
        private System.Windows.Forms.Button btn_sendFile;
        private System.Windows.Forms.Button btn_stopFile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_percentage;
        private System.Windows.Forms.Button btn_touchThePlate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelSpindleRPM;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelPositioning;
        private GRBL.Controls.Visualizer visualizer1;
    }
}

