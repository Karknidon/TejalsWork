namespace GUISocket
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            backWorkerTreatment = new System.ComponentModel.BackgroundWorker();
            tbRecieved = new TextBox();
            groupBox1 = new GroupBox();
            indCmdProgress = new WinFormsControlLibrary1.Indicator();
            label5 = new Label();
            lblStatServer = new Label();
            label4 = new Label();
            menuStrip1 = new MenuStrip();
            loginToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            tbSrcEnc = new TextBox();
            tbDmyEnc = new TextBox();
            indIndexLock = new WinFormsControlLibrary1.Indicator();
            indAdapterSensor = new WinFormsControlLibrary1.Indicator();
            indiIndexHome = new WinFormsControlLibrary1.Indicator();
            indiSourceOut = new WinFormsControlLibrary1.Indicator();
            indicSOver = new WinFormsControlLibrary1.Indicator();
            indiSHome = new WinFormsControlLibrary1.Indicator();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            indiDOverShoot = new WinFormsControlLibrary1.Indicator();
            indiDHome = new WinFormsControlLibrary1.Indicator();
            label16 = new Label();
            label17 = new Label();
            label1 = new Label();
            label2 = new Label();
            tbIP = new TextBox();
            tbPort = new TextBox();
            btnConnect = new Button();
            btnSend = new Button();
            tbSend = new TextBox();
            label3 = new Label();
            panel1 = new Panel();
            btnDisconnect = new Button();
            timerClock1 = new TimerClock();
            progressBarD = new ProgressBar();
            tbIndxCount = new TextBox();
            btnIndxCW = new Button();
            btnIndxCCW = new Button();
            btnHome = new Button();
            Indexer = new GroupBox();
            btnI_EReset = new Button();
            label6 = new Label();
            gpSDrive = new GroupBox();
            btnS_Ereset = new Button();
            label18 = new Label();
            tbScount = new TextBox();
            btnS_home = new Button();
            btnS_in = new Button();
            btnS_out = new Button();
            gpDDrive = new GroupBox();
            btnD_Ereset = new Button();
            label19 = new Label();
            tbDcount = new TextBox();
            btnD_home = new Button();
            btnD_in = new Button();
            btnD_out = new Button();
            calibrate = new Button();
            cbIndexer = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            indexTo = new Button();
            progressBarS = new ProgressBar();
            label21 = new Label();
            label22 = new Label();
            button1 = new Button();
            lblIndexSlotNo = new Label();
            dataGridView1 = new DataGridView();
            label7 = new Label();
            label8 = new Label();
            indSourcOut = new WinFormsControlLibrary1.Indicator();
            label9 = new Label();
            tmrSourceOUT = new System.Windows.Forms.Timer(components);
            tcDwell = new TimerClock();
            btAs = new Button();
            btAE = new Button();
            label20 = new Label();
            lblIndexerCount = new Label();
            indIndexCalibrated = new WinFormsControlLibrary1.Indicator();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            Indexer.SuspendLayout();
            gpSDrive.SuspendLayout();
            gpDDrive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // backWorkerTreatment
            // 
            backWorkerTreatment.DoWork += backWorkerTreatment_DoWork;
            // 
            // tbRecieved
            // 
            tbRecieved.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            tbRecieved.Cursor = Cursors.No;
            tbRecieved.Location = new Point(1167, 55);
            tbRecieved.MaxLength = 25;
            tbRecieved.Multiline = true;
            tbRecieved.Name = "tbRecieved";
            tbRecieved.ReadOnly = true;
            tbRecieved.Size = new Size(300, 725);
            tbRecieved.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(indCmdProgress);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(lblStatServer);
            groupBox1.Location = new Point(0, 786);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1472, 54);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Status";
            // 
            // indCmdProgress
            // 
            indCmdProgress.Location = new Point(1431, 13);
            indCmdProgress.Name = "indCmdProgress";
            indCmdProgress.Size = new Size(35, 35);
            indCmdProgress.TabIndex = 76;
            indCmdProgress.Value = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(344, 19);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 1;
            label5.Text = "User: ";
            // 
            // lblStatServer
            // 
            lblStatServer.AutoSize = true;
            lblStatServer.ForeColor = Color.Red;
            lblStatServer.Location = new Point(9, 19);
            lblStatServer.Name = "lblStatServer";
            lblStatServer.Size = new Size(110, 15);
            lblStatServer.TabIndex = 0;
            lblStatServer.Text = "Server: Disconected";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(1166, 37);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 11;
            label4.Text = "Data Recieved";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { loginToolStripMenuItem, aboutToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1484, 24);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new Size(49, 20);
            loginToolStripMenuItem.Text = "Login";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // tbSrcEnc
            // 
            tbSrcEnc.Location = new Point(140, 531);
            tbSrcEnc.Name = "tbSrcEnc";
            tbSrcEnc.Size = new Size(100, 23);
            tbSrcEnc.TabIndex = 27;
            // 
            // tbDmyEnc
            // 
            tbDmyEnc.Location = new Point(138, 600);
            tbDmyEnc.Name = "tbDmyEnc";
            tbDmyEnc.Size = new Size(100, 23);
            tbDmyEnc.TabIndex = 28;
            // 
            // indIndexLock
            // 
            indIndexLock.Location = new Point(607, 658);
            indIndexLock.Name = "indIndexLock";
            indIndexLock.Size = new Size(47, 47);
            indIndexLock.TabIndex = 29;
            indIndexLock.Value = false;
            // 
            // indAdapterSensor
            // 
            indAdapterSensor.Location = new Point(193, 658);
            indAdapterSensor.Name = "indAdapterSensor";
            indAdapterSensor.Size = new Size(47, 47);
            indAdapterSensor.TabIndex = 30;
            indAdapterSensor.Value = false;
            // 
            // indiIndexHome
            // 
            indiIndexHome.Location = new Point(266, 658);
            indiIndexHome.Name = "indiIndexHome";
            indiIndexHome.Size = new Size(47, 47);
            indiIndexHome.TabIndex = 31;
            indiIndexHome.Value = false;
            // 
            // indiSourceOut
            // 
            indiSourceOut.Location = new Point(333, 658);
            indiSourceOut.Name = "indiSourceOut";
            indiSourceOut.Size = new Size(47, 47);
            indiSourceOut.TabIndex = 32;
            indiSourceOut.Value = false;
            // 
            // indicSOver
            // 
            indicSOver.Location = new Point(526, 658);
            indicSOver.Name = "indicSOver";
            indicSOver.Size = new Size(47, 47);
            indicSOver.TabIndex = 33;
            indicSOver.Value = false;
            // 
            // indiSHome
            // 
            indiSHome.Location = new Point(432, 658);
            indiSHome.Name = "indiSHome";
            indiSHome.Size = new Size(47, 47);
            indiSHome.TabIndex = 34;
            indiSHome.Value = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(607, 708);
            label10.Name = "label10";
            label10.Size = new Size(64, 15);
            label10.TabIndex = 35;
            label10.Text = "Index Lock";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(200, 708);
            label11.Name = "label11";
            label11.Size = new Size(38, 15);
            label11.TabIndex = 36;
            label11.Text = "Rot_C";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(255, 708);
            label12.Name = "label12";
            label12.Size = new Size(69, 15);
            label12.TabIndex = 38;
            label12.Text = "IndexHome";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(344, 708);
            label13.Name = "label13";
            label13.Size = new Size(36, 15);
            label13.TabIndex = 39;
            label13.Text = "S Out";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(512, 708);
            label14.Name = "label14";
            label14.Size = new Size(71, 15);
            label14.TabIndex = 40;
            label14.Text = "S Overshoot";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(441, 708);
            label15.Name = "label15";
            label15.Size = new Size(49, 15);
            label15.TabIndex = 41;
            label15.Text = "S Home";
            // 
            // indiDOverShoot
            // 
            indiDOverShoot.Location = new Point(93, 658);
            indiDOverShoot.Name = "indiDOverShoot";
            indiDOverShoot.Size = new Size(47, 47);
            indiDOverShoot.TabIndex = 42;
            indiDOverShoot.Value = false;
            // 
            // indiDHome
            // 
            indiDHome.Location = new Point(22, 658);
            indiDHome.Name = "indiDHome";
            indiDHome.Size = new Size(47, 47);
            indiDHome.TabIndex = 43;
            indiDHome.Value = false;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(72, 708);
            label16.Name = "label16";
            label16.Size = new Size(73, 15);
            label16.TabIndex = 44;
            label16.Text = "D Overshoot";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(18, 708);
            label17.Name = "label17";
            label17.Size = new Size(51, 15);
            label17.TabIndex = 45;
            label17.Text = "D Home";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 4);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 0;
            label1.Text = "ServerIP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(161, 4);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 1;
            label2.Text = "Port";
            // 
            // tbIP
            // 
            tbIP.Location = new Point(8, 22);
            tbIP.Name = "tbIP";
            tbIP.Size = new Size(147, 23);
            tbIP.TabIndex = 2;
            tbIP.Text = "200.100.2.4";
            // 
            // tbPort
            // 
            tbPort.Location = new Point(161, 22);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(130, 23);
            tbPort.TabIndex = 3;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(161, 66);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(130, 35);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(579, 91);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(204, 28);
            btnSend.TabIndex = 6;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // tbSend
            // 
            tbSend.Location = new Point(579, 62);
            tbSend.Name = "tbSend";
            tbSend.Size = new Size(204, 23);
            tbSend.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(579, 42);
            label3.Name = "label3";
            label3.Size = new Size(143, 15);
            label3.TabIndex = 10;
            label3.Text = "Command To send (RAW)";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDisconnect);
            panel1.Controls.Add(btnConnect);
            panel1.Controls.Add(tbPort);
            panel1.Controls.Add(tbIP);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(819, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(318, 110);
            panel1.TabIndex = 49;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(9, 66);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(130, 35);
            btnDisconnect.TabIndex = 5;
            btnDisconnect.Text = "Dis-Connect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += Form1_Closing;
            // 
            // timerClock1
            // 
            timerClock1.BackColor = Color.LightSteelBlue;
            timerClock1.BrushThickness = 6;
            timerClock1.CtValue = 0;
            timerClock1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            timerClock1.Location = new Point(928, 311);
            timerClock1.Margin = new Padding(7, 7, 7, 7);
            timerClock1.MaxValue1 = 360;
            timerClock1.Name = "timerClock1";
            timerClock1.Size = new Size(209, 209);
            timerClock1.Size1 = 209;
            timerClock1.TabIndex = 50;
            // 
            // progressBarD
            // 
            progressBarD.Location = new Point(24, 629);
            progressBarD.Maximum = 40000;
            progressBarD.Name = "progressBarD";
            progressBarD.Size = new Size(870, 23);
            progressBarD.Step = 1;
            progressBarD.TabIndex = 51;
            // 
            // tbIndxCount
            // 
            tbIndxCount.Location = new Point(9, 42);
            tbIndxCount.MaxLength = 10;
            tbIndxCount.Name = "tbIndxCount";
            tbIndxCount.Size = new Size(136, 23);
            tbIndxCount.TabIndex = 52;
            tbIndxCount.TabStop = false;
            tbIndxCount.Text = "1000";
            tbIndxCount.TextAlign = HorizontalAlignment.Center;
            tbIndxCount.WordWrap = false;
            // 
            // btnIndxCW
            // 
            btnIndxCW.Location = new Point(6, 132);
            btnIndxCW.Name = "btnIndxCW";
            btnIndxCW.Size = new Size(136, 36);
            btnIndxCW.TabIndex = 55;
            btnIndxCW.Text = "Move CW";
            btnIndxCW.UseVisualStyleBackColor = true;
            btnIndxCW.Click += btnIndxCW_Click;
            // 
            // btnIndxCCW
            // 
            btnIndxCCW.Location = new Point(9, 80);
            btnIndxCCW.Name = "btnIndxCCW";
            btnIndxCCW.Size = new Size(136, 36);
            btnIndxCCW.TabIndex = 56;
            btnIndxCCW.Text = "Move CCW";
            btnIndxCCW.UseVisualStyleBackColor = true;
            btnIndxCCW.Click += btnIndxCCW_Click;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(5, 188);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(140, 56);
            btnHome.TabIndex = 57;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // Indexer
            // 
            Indexer.Controls.Add(btnI_EReset);
            Indexer.Controls.Add(label6);
            Indexer.Controls.Add(tbIndxCount);
            Indexer.Controls.Add(btnHome);
            Indexer.Controls.Add(btnIndxCCW);
            Indexer.Controls.Add(btnIndxCW);
            Indexer.Location = new Point(18, 110);
            Indexer.Name = "Indexer";
            Indexer.Size = new Size(152, 301);
            Indexer.TabIndex = 58;
            Indexer.TabStop = false;
            Indexer.Text = "Indexer Motion";
            // 
            // btnI_EReset
            // 
            btnI_EReset.Location = new Point(5, 262);
            btnI_EReset.Name = "btnI_EReset";
            btnI_EReset.Size = new Size(140, 33);
            btnI_EReset.TabIndex = 60;
            btnI_EReset.Text = "E Reset";
            btnI_EReset.UseVisualStyleBackColor = true;
            btnI_EReset.Click += btn_EReset_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 21);
            label6.Name = "label6";
            label6.Size = new Size(81, 15);
            label6.TabIndex = 58;
            label6.Text = "Motor Counts";
            // 
            // gpSDrive
            // 
            gpSDrive.Controls.Add(btnS_Ereset);
            gpSDrive.Controls.Add(label18);
            gpSDrive.Controls.Add(tbScount);
            gpSDrive.Controls.Add(btnS_home);
            gpSDrive.Controls.Add(btnS_in);
            gpSDrive.Controls.Add(btnS_out);
            gpSDrive.Location = new Point(206, 110);
            gpSDrive.Name = "gpSDrive";
            gpSDrive.Size = new Size(152, 301);
            gpSDrive.TabIndex = 60;
            gpSDrive.TabStop = false;
            gpSDrive.Text = "Source Drive";
            // 
            // btnS_Ereset
            // 
            btnS_Ereset.Location = new Point(5, 262);
            btnS_Ereset.Name = "btnS_Ereset";
            btnS_Ereset.Size = new Size(140, 33);
            btnS_Ereset.TabIndex = 60;
            btnS_Ereset.Text = "E Reset";
            btnS_Ereset.UseVisualStyleBackColor = true;
            btnS_Ereset.Click += btnS_Ereset_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(12, 21);
            label18.Name = "label18";
            label18.Size = new Size(81, 15);
            label18.TabIndex = 58;
            label18.Text = "Motor Counts";
            // 
            // tbScount
            // 
            tbScount.Location = new Point(9, 42);
            tbScount.MaxLength = 10;
            tbScount.Name = "tbScount";
            tbScount.Size = new Size(136, 23);
            tbScount.TabIndex = 52;
            tbScount.TabStop = false;
            tbScount.Text = "100";
            tbScount.TextAlign = HorizontalAlignment.Center;
            tbScount.WordWrap = false;
            // 
            // btnS_home
            // 
            btnS_home.Location = new Point(5, 188);
            btnS_home.Name = "btnS_home";
            btnS_home.Size = new Size(140, 56);
            btnS_home.TabIndex = 57;
            btnS_home.Text = "Home";
            btnS_home.UseVisualStyleBackColor = true;
            btnS_home.Click += btnS_home_Click;
            // 
            // btnS_in
            // 
            btnS_in.Location = new Point(9, 80);
            btnS_in.Name = "btnS_in";
            btnS_in.Size = new Size(136, 36);
            btnS_in.TabIndex = 56;
            btnS_in.Text = "Move IN";
            btnS_in.UseVisualStyleBackColor = true;
            btnS_in.Click += btnS_in_Click;
            // 
            // btnS_out
            // 
            btnS_out.Location = new Point(6, 132);
            btnS_out.Name = "btnS_out";
            btnS_out.Size = new Size(136, 36);
            btnS_out.TabIndex = 55;
            btnS_out.Text = "Move OUT";
            btnS_out.UseVisualStyleBackColor = true;
            btnS_out.Click += btnS_out_Click;
            // 
            // gpDDrive
            // 
            gpDDrive.Controls.Add(btnD_Ereset);
            gpDDrive.Controls.Add(label19);
            gpDDrive.Controls.Add(tbDcount);
            gpDDrive.Controls.Add(btnD_home);
            gpDDrive.Controls.Add(btnD_in);
            gpDDrive.Controls.Add(btnD_out);
            gpDDrive.Location = new Point(375, 110);
            gpDDrive.Name = "gpDDrive";
            gpDDrive.Size = new Size(152, 301);
            gpDDrive.TabIndex = 61;
            gpDDrive.TabStop = false;
            gpDDrive.Text = "Dummy Drive";
            // 
            // btnD_Ereset
            // 
            btnD_Ereset.Location = new Point(5, 262);
            btnD_Ereset.Name = "btnD_Ereset";
            btnD_Ereset.Size = new Size(140, 33);
            btnD_Ereset.TabIndex = 60;
            btnD_Ereset.Text = "E Reset";
            btnD_Ereset.UseVisualStyleBackColor = true;
            btnD_Ereset.Click += btnD_Ereset_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(12, 21);
            label19.Name = "label19";
            label19.Size = new Size(81, 15);
            label19.TabIndex = 58;
            label19.Text = "Motor Counts";
            // 
            // tbDcount
            // 
            tbDcount.Location = new Point(9, 42);
            tbDcount.MaxLength = 10;
            tbDcount.Name = "tbDcount";
            tbDcount.Size = new Size(136, 23);
            tbDcount.TabIndex = 52;
            tbDcount.TabStop = false;
            tbDcount.Text = "100";
            tbDcount.TextAlign = HorizontalAlignment.Center;
            tbDcount.WordWrap = false;
            tbDcount.TextChanged += tbDcount_TextChanged;
            // 
            // btnD_home
            // 
            btnD_home.Location = new Point(5, 188);
            btnD_home.Name = "btnD_home";
            btnD_home.Size = new Size(140, 56);
            btnD_home.TabIndex = 57;
            btnD_home.Text = "Home";
            btnD_home.UseVisualStyleBackColor = true;
            btnD_home.Click += btnD_home_Click;
            // 
            // btnD_in
            // 
            btnD_in.Location = new Point(9, 80);
            btnD_in.Name = "btnD_in";
            btnD_in.Size = new Size(136, 36);
            btnD_in.TabIndex = 56;
            btnD_in.Text = "Move IN";
            btnD_in.UseVisualStyleBackColor = true;
            btnD_in.Click += btnD_in_Click;
            // 
            // btnD_out
            // 
            btnD_out.Location = new Point(6, 132);
            btnD_out.Name = "btnD_out";
            btnD_out.Size = new Size(136, 36);
            btnD_out.TabIndex = 55;
            btnD_out.Text = "Move OUT";
            btnD_out.UseVisualStyleBackColor = true;
            btnD_out.Click += btnD_out_Click;
            // 
            // calibrate
            // 
            calibrate.Location = new Point(900, 175);
            calibrate.Name = "calibrate";
            calibrate.Size = new Size(107, 32);
            calibrate.TabIndex = 62;
            calibrate.Text = "Calibrate ";
            calibrate.UseVisualStyleBackColor = true;
            calibrate.Visible = false;
            calibrate.Click += calibrate_Click;
            // 
            // cbIndexer
            // 
            cbIndexer.FormattingEnabled = true;
            cbIndexer.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
            cbIndexer.Location = new Point(1016, 175);
            cbIndexer.Name = "cbIndexer";
            cbIndexer.Size = new Size(121, 23);
            cbIndexer.TabIndex = 64;
            cbIndexer.Text = "1";
            cbIndexer.Visible = false;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(1068, 278);
            numericUpDown1.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(69, 23);
            numericUpDown1.TabIndex = 66;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // indexTo
            // 
            indexTo.Location = new Point(928, 270);
            indexTo.Name = "indexTo";
            indexTo.Size = new Size(91, 35);
            indexTo.TabIndex = 68;
            indexTo.Text = "Indexer To";
            indexTo.UseVisualStyleBackColor = true;
            indexTo.Click += indexTo_Click;
            // 
            // progressBarS
            // 
            progressBarS.Location = new Point(24, 565);
            progressBarS.Maximum = 40000;
            progressBarS.Name = "progressBarS";
            progressBarS.Size = new Size(870, 23);
            progressBarS.Step = 1;
            progressBarS.TabIndex = 70;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(24, 606);
            label21.Name = "label21";
            label21.Size = new Size(89, 15);
            label21.TabIndex = 71;
            label21.Text = "Source Position";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(30, 534);
            label22.Name = "label22";
            label22.Size = new Size(96, 15);
            label22.TabIndex = 72;
            label22.Text = "Dummy Position";
            // 
            // button1
            // 
            button1.Location = new Point(18, 417);
            button1.Name = "button1";
            button1.Size = new Size(175, 33);
            button1.TabIndex = 73;
            button1.Text = "Start Treatment";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblIndexSlotNo
            // 
            lblIndexSlotNo.AutoSize = true;
            lblIndexSlotNo.Location = new Point(1027, 534);
            lblIndexSlotNo.Name = "lblIndexSlotNo";
            lblIndexSlotNo.Size = new Size(55, 15);
            lblIndexSlotNo.TabIndex = 74;
            lblIndexSlotNo.Text = "Initilizing";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(533, 175);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(361, 150);
            dataGridView1.TabIndex = 75;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(94, 466);
            label7.Name = "label7";
            label7.Size = new Size(46, 54);
            label7.TabIndex = 76;
            label7.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(30, 487);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 77;
            label8.Text = "Dwel Time";
            // 
            // indSourcOut
            // 
            indSourcOut.Location = new Point(633, 361);
            indSourcOut.Name = "indSourcOut";
            indSourcOut.Size = new Size(89, 89);
            indSourcOut.TabIndex = 78;
            indSourcOut.Value = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(633, 443);
            label9.Name = "label9";
            label9.Size = new Size(87, 32);
            label9.TabIndex = 79;
            label9.Text = "Source";
            // 
            // tmrSourceOUT
            // 
            tmrSourceOUT.Interval = 1500;
            tmrSourceOUT.Tick += tmrSourceOUT_Tick;
            // 
            // tcDwell
            // 
            tcDwell.BackColor = Color.LightSteelBlue;
            tcDwell.BrushThickness = 6;
            tcDwell.CtValue = 25;
            tcDwell.Location = new Point(960, 573);
            tcDwell.MaxValue1 = 100;
            tcDwell.Name = "tcDwell";
            tcDwell.Size = new Size(150, 150);
            tcDwell.Size1 = 150;
            tcDwell.TabIndex = 80;
            // 
            // btAs
            // 
            btAs.Location = new Point(51, 47);
            btAs.Name = "btAs";
            btAs.Size = new Size(75, 23);
            btAs.TabIndex = 81;
            btAs.Text = "AS";
            btAs.UseVisualStyleBackColor = true;
            btAs.Click += btAs_Click;
            // 
            // btAE
            // 
            btAE.Location = new Point(165, 47);
            btAE.Name = "btAE";
            btAE.Size = new Size(75, 23);
            btAE.TabIndex = 82;
            btAE.Text = "AE";
            btAE.UseVisualStyleBackColor = true;
            btAE.Click += btAE_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(51, 81);
            label20.Name = "label20";
            label20.Size = new Size(82, 15);
            label20.TabIndex = 83;
            label20.Text = "Indexer Count";
            // 
            // lblIndexerCount
            // 
            lblIndexerCount.AutoSize = true;
            lblIndexerCount.Location = new Point(149, 81);
            lblIndexerCount.Name = "lblIndexerCount";
            lblIndexerCount.Size = new Size(44, 15);
            lblIndexerCount.TabIndex = 84;
            lblIndexerCount.Text = "label23";
            // 
            // indIndexCalibrated
            // 
            indIndexCalibrated.Location = new Point(0, 41);
            indIndexCalibrated.Name = "indIndexCalibrated";
            indIndexCalibrated.Size = new Size(40, 40);
            indIndexCalibrated.TabIndex = 85;
            indIndexCalibrated.Value = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(1484, 841);
            Controls.Add(indIndexCalibrated);
            Controls.Add(lblIndexerCount);
            Controls.Add(label20);
            Controls.Add(btAE);
            Controls.Add(btAs);
            Controls.Add(tcDwell);
            Controls.Add(label9);
            Controls.Add(indSourcOut);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(dataGridView1);
            Controls.Add(lblIndexSlotNo);
            Controls.Add(button1);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(progressBarS);
            Controls.Add(indexTo);
            Controls.Add(numericUpDown1);
            Controls.Add(cbIndexer);
            Controls.Add(calibrate);
            Controls.Add(gpDDrive);
            Controls.Add(gpSDrive);
            Controls.Add(label3);
            Controls.Add(Indexer);
            Controls.Add(btnSend);
            Controls.Add(tbSend);
            Controls.Add(progressBarD);
            Controls.Add(timerClock1);
            Controls.Add(panel1);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(indiDHome);
            Controls.Add(indiDOverShoot);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(indiSHome);
            Controls.Add(indicSOver);
            Controls.Add(indiSourceOut);
            Controls.Add(indiIndexHome);
            Controls.Add(indAdapterSensor);
            Controls.Add(indIndexLock);
            Controls.Add(tbDmyEnc);
            Controls.Add(tbSrcEnc);
            Controls.Add(label4);
            Controls.Add(menuStrip1);
            Controls.Add(groupBox1);
            Controls.Add(tbRecieved);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "My Application Client";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Indexer.ResumeLayout(false);
            Indexer.PerformLayout();
            gpSDrive.ResumeLayout(false);
            gpSDrive.PerformLayout();
            gpDDrive.ResumeLayout(false);
            gpDDrive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backWorkerTreatment;
        private TextBox tbRecieved;
        private GroupBox groupBox1;
        private Label lblStatServer;
        private Label label5;
        private Label label4;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TextBox tbSrcEnc;
        private TextBox tbDmyEnc;
        private WinFormsControlLibrary1.Indicator indIndexLock;
        private WinFormsControlLibrary1.Indicator indAdapterSensor;
        private WinFormsControlLibrary1.Indicator indiIndexHome;
        private WinFormsControlLibrary1.Indicator indiSourceOut;
        private WinFormsControlLibrary1.Indicator indicSOver;
        private WinFormsControlLibrary1.Indicator indiSHome;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private WinFormsControlLibrary1.Indicator indiDOverShoot;
        private WinFormsControlLibrary1.Indicator indiDHome;
        private Label label16;
        private Label label17;
        private Label label1;
        private Label label2;
        private TextBox tbIP;
        private TextBox tbPort;
        private Button btnConnect;
        private Button btnSend;
        private TextBox tbSend;
        private Label label3;
        private Panel panel1;

        private TimerClock timerClock1;
        private ProgressBar progressBarD;
        private Button btnIndxCW;
        private Button btnIndxCCW;
        public TextBox tbIndxCount;
        private Button btnHome;
        private GroupBox Indexer;
        private Label label6;
        private Button btnI_EReset;
        private GroupBox gpSDrive;
        private Button btnS_Ereset;
        private Label label18;
        public TextBox tbScount;
        private Button btnS_home;
        private Button btnS_in;
        private Button btnS_out;
        private GroupBox gpDDrive;
        private Button btnD_Ereset;
        private Label label19;
        public TextBox tbDcount;
        private Button btnD_home;
        private Button btnD_in;
        private Button btnD_out;
        private Button btnDisconnect;
        private Button calibrate;
        private ComboBox cbIndexer;
        private NumericUpDown numericUpDown1;
        private Button indexTo;
        private ProgressBar progressBarS;
        private Label label21;
        private Label label22;
        private Button button1;
        private Label lblIndexSlotNo;
        private DataGridView dataGridView1;
        private WinFormsControlLibrary1.Indicator indCmdProgress;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label label7;
        private Label label8;
        private WinFormsControlLibrary1.Indicator indSourcOut;
        private Label label9;
        private System.Windows.Forms.Timer tmrSourceOUT;
        private TimerClock tcDwell;
        private Button btAs;
        private Button btAE;
        private Label label20;
        private Label lblIndexerCount;
        private WinFormsControlLibrary1.Indicator indIndexCalibrated;
    }
}