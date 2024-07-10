using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms.VisualStyles;
using static GUISocket.Form2;


namespace GUISocket
{

    public partial class Form1 : Form
    {
        class TreatmentState
        {
            bool active;
            int state;
            int subState;
            string? lastCommandSent;

            public TreatmentState()
            {
                Active = false;
                State = 0;
                SubState = 0;
                LastCommandSent = "";
            }

            public bool Active { get => active; set => active = value; }
            public int State { get => state; set => state = value; }
            public int SubState { get => subState; set => subState = value; }
            public String LastCommandSent { get => lastCommandSent; set => lastCommandSent = value; }
        }
        Stopwatch stopTime = new Stopwatch();
        Stopwatch totalTime = new Stopwatch();
        int currentSourcPos = 0;
        bool SourceOut = false;

        System.Data.DataSet dataSet;
        System.Data.DataTable ChannelTable = new DataTable("ChannelData");
        int[] indexerPosition = { 0, 515, 3741, 6955, 10142, 13352, 16506, 19677, 22856, 26036, 29231, 32424, 35708, 38914, 42120, 45326, 48532, 51738, 54944, 58148, 61353 };
        int[] indexerEncPosition = { 0, 880, 5879, 10873, 15869, 20822, 25834, 30792,35808,40729,
        45740,50740,55728,60751,65744, 70725,75705, 80794,85871, 90870,95870};
        TreatmentState ts = new TreatmentState();

        IPAddress ip;
        IPEndPoint remoteEP;
        Socket clientSocket;
        int port = 2321;
        string ipno = "192.168.99.253";
        byte[] buffer = new byte[1024];
        int ZeroSize_count = 0;
        Int32 mean;

        private void MakeChannelTable()
        {

            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType,
            // ColumnName and add to DataTable.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = false;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            ChannelTable.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Position";
            column.AutoIncrement = false;
            column.Caption = "Position";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            ChannelTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "TreatmentTime";
            column.AutoIncrement = false;
            column.Caption = "Treatment Time";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            ChannelTable.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = ChannelTable.Columns["id"];
            ChannelTable.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(ChannelTable);

            // Create three new DataRow objects and add
            // them to the DataTable
            for (int i = 0; i <= 5; i++)
            {
                row = ChannelTable.NewRow();
                row["id"] = i;
                row["Position"] = Convert.ToInt32(30 * 2.8);
                row["TreatmentTime"] = 5000;
                ChannelTable.Rows.Add(row);
            }
        }


        public Form1()
        {
            InitializeComponent();
            ip = IPAddress.Parse(ipno);
            remoteEP = new IPEndPoint(ip, port);
            MakeChannelTable();
            dataGridView1.DataSource = ChannelTable;

        }

        private string Send_to_client(string Data2send)
        {
            try
            {
                string toSend = "#," + Data2send + ",?";
                byte[] msg1 = Encoding.ASCII.GetBytes(toSend);
                int bytesSent = clientSocket.Send(msg1);
                int bytesReceived = clientSocket.Receive(buffer);
                string dtRecievd = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                String timeStamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");
                tbRecieved.AppendText($"{timeStamp}:< {dtRecievd}> \n");

                //Debug.WriteLine($"{timeStamp}: <{dtRecievd}>: {bytesReceived}: byte [0]={(int)buffer[0]}, byte[1]= {(int)buffer[1]}");
                return dtRecievd;
            }
            catch (ArgumentNullException e1)
            {
                MessageBox.Show(e1.Message, "Buffer empty ");
                return "E";
                throw;
            }
            catch (Exception e1)
            {

                timer1.Enabled = false;
                lblStatServer.ForeColor = System.Drawing.Color.Red;
                lblStatServer.Text = "Server:Disconnected";
                timer1.Enabled = false;
                clientSocket.Close();
                btnConnect.Enabled = true;
                btnSend.Enabled = false;
                MessageBox.Show(e1.Message, "All Send button Other Exception");
                return "E";


            }

        }

        private string[] CommandParser(string dtRecievd)
        {
            //Parsing of command
            string[] words = dtRecievd.Split(",");


            //Update GUI -------
            UpdateGUI(words);



            return words;

        }

        private void UpdateGUI(string[] words)
        {

            //Debug.WriteLine(words.Length);
            //Debug.WriteLine("DSensor :" + Convert.ToString(dSensor, 2));
            SensorUdate(byte.Parse(words[1]), byte.Parse(words[2])); // update the sensor display
            //double srcEncoder = double.Parse(words[3]);
            progressBarS.Value = (-1 * int.Parse(words[5])) < 0 ? 0 : -1 * (int.Parse(words[5]));
            progressBarD.Value = int.Parse(words[4]) < 0 ? 0 : int.Parse(words[4]);
            tbSrcEnc.Text = Convert.ToString(float.Parse(words[5]) * -0.0706);//Convert.ToString(float.Parse(words[]) * 360 / 100000);//For Indexer Encoder
            tbDmyEnc.Text = Convert.ToString(float.Parse(words[4]) * 0.0706);
            timerClock1.CtValue = int.Parse(words[3]) * 360 / 100000;
            indSourcOut.Value = SourceOut;
            lblIndexSlotNo.Text = ((int.Parse(words[3]) - 656) / 5000 + 1).ToString();
            label7.Text = (stopTime.Elapsed.TotalSeconds).ToString("0.00");
            if (!indCmdProgress.Value)
            {
                lblIndexerCount.Text = words[3];
            }
            else
                lblIndexerCount.Text = "-- NA --";
        }

        void SensorUdate(byte dSensor, byte dSensor2)
        {
            byte mask = 0x02;
            indicSOver.Value = (dSensor & mask) != 0;

            mask = 0x80;
            indIndexLock.Value = (dSensor & mask) != 0;

            mask = 0x04;
            indiSHome.Value = (dSensor & mask) != 0;

            mask = 0x01;
            indiSourceOut.Value = (dSensor & mask) != 0;

            mask = 0x10;
            indiIndexHome.Value = (dSensor & mask) != 0;

            mask = 0x08;
            indAdapterSensor.Value = (dSensor & mask) != 0;

            mask = 0x40;
            indiDHome.Value = (dSensor & mask) != 0;

            mask = 0x20;
            indiDOverShoot.Value = (dSensor & mask) != 0;

            // ----------------------
            mask = 0x02;
            indCmdProgress.Value = (dSensor2 & mask) != 0;
            mask = 0x08;
            indIndexCalibrated.Value = (dSensor2 & mask) == 0;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tbIP.Text = ip.ToString();
            tbPort.Text = port.ToString();
            btnSend.Enabled = false;
            btnSend.Text = "Send";
            tcDwell.MaxValue1 = 0;


        }
        private void Form1_Closing(object sender, EventArgs e)
        {
            //release socket
            timer1.Enabled = false;
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            lblStatServer.ForeColor = System.Drawing.Color.Red;
            lblStatServer.Text = "Server:Disconnected";
            btnConnect.Enabled = true;
            btnSend.Enabled = false;
            Indexer.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                ip = IPAddress.Parse(tbIP.Text);
                port = int.Parse(tbPort.Text);
                remoteEP = new IPEndPoint(ip, port);
                clientSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // Connect to the server

                try
                {
                    clientSocket.Connect(remoteEP);
                    btnSend.Enabled = true;
                    btnConnect.Enabled = false;
                    Indexer.Enabled = true;
                    lblStatServer.Text = "Server:Connected (" + ipno + ":" + port.ToString() + ")";
                    lblStatServer.ForeColor = System.Drawing.Color.Green;
                    MessageBox.Show("Socket Connected to"+ip, "Success ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer1.Enabled = true;


                }
                catch (ArgumentNullException e1)
                {

                    MessageBox.Show(e1.Message, "Argument Null ");
                    throw;
                }
                catch (SocketException e1)
                {

                    MessageBox.Show(e1.Message, "Socket Connection Exception");

                }
                catch (Exception e1)
                {

                    MessageBox.Show(e1.Message, "All Other Exception");

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Socket Creation Exception");

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            string data = Send_to_client(tbSend.Text);
            CommandParser(data);
            Debug.WriteLine(tbSend.Text);

        }


        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 childForm = new Form2();
            childForm.DataSent += ParentForm_DataSent;
            childForm.ShowDialog();


        }
        private void ParentForm_DataSent(object sender, DataEventArgs e)
        {
            string receivedData = e.Data;
            // Process the received data from the child form
            label5.Text = "User: " + receivedData;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Send heart beat HB pulse here.
            string cmdStr = "HB";
            string data = Send_to_client(cmdStr);
            string[] parList = CommandParser(data); // UpdateGui
            if (indiSHome.Value == false)
            {
                tmrSourceOUT.Enabled = true;
                SourceOut = true;
            }
            else
            {
                tmrSourceOUT.Enabled = false;
                SourceOut = false;
            }
            Treament();

        }

        private void Treament()
        {
            string cmdStr;
            string data;
            int tipPosition = 4200; // steps to reach tip of appliacator

            if (ts.Active)
            {


                switch (ts.State)
                {
                    case 0: //Send Dummy out
                        if (!indCmdProgress.Value)
                        {  // if previous command is completed
                            ts.State = ts.State + 1;
                        }
                        break;
                    case 1: //Send Dummy out
                        if (!indCmdProgress.Value)
                        {  // if previous command is completed

                            ts.State = ts.State + 1;
                        }
                        break;
                    case 2: //Send Dummy out
                        if (!indCmdProgress.Value)
                        {  // if previous command is completed
                            cmdStr = "MDF" + tipPosition + ",";
                            data = Send_to_client(cmdStr);
                            ts.State = ts.State + 1;
                        }
                        break;


                    case 3: // Bring home the dummy
                        if (!indCmdProgress.Value)
                        {
                            // Send Dummy home ie to origin
                            data = Send_to_client("OD");
                            ts.State = ts.State + 1;
                        }
                        break;

                    case 4: //Dymmy Reached Home
                        if (!indCmdProgress.Value)
                        {
                            stopTime.Start();
                            ts.State = ts.State + 1;
                        }

                        break;
                    case 5:
                        if (stopTime.ElapsedMilliseconds > 100)
                        {
                            stopTime.Reset();
                            ts.State = ts.State + 1;
                        }
                        break;
                    case 6: // Send Source Forward 

                        if (!indCmdProgress.Value)
                        {  // if previous command is completed
                            Debug.WriteLine(ts.SubState);
                            currentSourcPos = tipPosition - Convert.ToInt32(ChannelTable.Rows[ts.SubState]["Position"]);
                            cmdStr = "MSF" + currentSourcPos + ",";
                            data = Send_to_client(cmdStr);
                            ts.State = ts.State + 1;
                            Console.Beep();
                        }
                        break;
                    case 7:
                        if (!indCmdProgress.Value)
                        {
                            tcDwell.MaxValue1 = Convert.ToInt32(ChannelTable.Rows[ts.SubState]["TreatmentTime"]);
                            stopTime.Restart();
                            //stopTime.Start();
                            ts.State = ts.State + 1;
                        }

                        break;
                    case 8:
                        if (stopTime.Elapsed.TotalMilliseconds > Convert.ToInt32(ChannelTable.Rows[ts.SubState]["TreatmentTime"]))
                        {
                            //stopTime.Stop();
                            tcDwell.CtValue = 0;
                            stopTime.Reset();
                            ts.SubState = ts.SubState + 1;
                            ts.State = ts.State + 1;
                        }
                        tcDwell.CtValue = Convert.ToInt32(stopTime.Elapsed.TotalMilliseconds);
                        break;
                    case 9: // Send Source reverse in step
                        if (!indCmdProgress.Value)
                        {  // if previous command is completed
                            Debug.WriteLine(ts.SubState);
                            currentSourcPos = Convert.ToInt32(ChannelTable.Rows[ts.SubState]["Position"]);

                            cmdStr = "MSR" + currentSourcPos + ",";
                            data = Send_to_client(cmdStr);
                            ts.State = ts.State + 1;
                        }
                        break;
                    case 10:
                        if (!indCmdProgress.Value)
                        {
                            tcDwell.MaxValue1 = Convert.ToInt32(ChannelTable.Rows[ts.SubState]["TreatmentTime"]);
                            stopTime.Restart();
                            //stopTime.Start();
                            ts.State = ts.State + 1;
                        }

                        break;
                    case 11:
                        if (stopTime.Elapsed.TotalMilliseconds > Convert.ToInt32(ChannelTable.Rows[ts.SubState]["TreatmentTime"]))
                        {
                            //stopTime.Stop();
                            stopTime.Reset();
                            ts.SubState = ts.SubState + 1;
                            if (ts.SubState < ChannelTable.Rows.Count - 1)
                            {
                                ts.State = 9; // Loop is more controle points are therr.
                                tcDwell.CtValue = 0;
                            }
                            else
                            {
                                ts.State = ts.State + 1;
                                tcDwell.CtValue = 0;
                            }
                        }
                        tcDwell.CtValue = Convert.ToInt32(stopTime.Elapsed.TotalMilliseconds);
                        break;

                    case 12:
                        if (!indCmdProgress.Value)
                        {
                            // Send Source home ie to origin
                            data = Send_to_client("OS");
                            ts.State = ts.State + 1;
                            currentSourcPos = 0;
                        }
                        break;

                    case 13:// Command Completed
                        if (!indCmdProgress.Value)
                        {
                            ts.State = 0;
                            ts.SubState = 0;
                            ts.Active = false;
                        }
                        break;


                }

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 abtForm = new Form3();
            abtForm.FormBorderStyle = FormBorderStyle.Fixed3D;
            abtForm.Show();
        }


        private void btnIndxCCW_Click(object sender, EventArgs e)
        {

            string countStr = tbIndxCount.Text;
            string cmdStr = "MIF" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);


        }

        private void btnIndxCW_Click(object sender, EventArgs e)
        {
            string countStr = tbIndxCount.Text;
            string cmdStr = "MIR" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }
        private void btnD_out_Click(object sender, EventArgs e)
        {
            string countStr = tbDcount.Text;
            string cmdStr = "MDF" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void btnD_in_Click(object sender, EventArgs e)
        {
            string countStr = tbDcount.Text;
            string cmdStr = "MDR" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);



        }
        private void btnS_out_Click(object sender, EventArgs e)
        {
            string countStr = tbScount.Text;
            string cmdStr = "MSF" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }
        private void btnS_in_Click(object sender, EventArgs e)
        {
            string countStr = tbScount.Text;
            string cmdStr = "MSR" + countStr + ",";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);

        }




        private void btnHome_Click(object sender, EventArgs e)
        {
            string cmdStr = "O" + "I"; // Send Indexer to origin
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }
        private void btnS_home_Click(object sender, EventArgs e)
        {
            string cmdStr = "O" + "S"; // Send Indexer to origin
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void btnD_home_Click(object sender, EventArgs e)
        {
            string cmdStr = "O" + "D"; // Send Indexer to origin
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }



        private void btn_EReset_Click(object sender, EventArgs e) //Indexer encoder Reset
        {
            string cmdStr = "E" + "I"; //
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void btnS_Ereset_Click(object sender, EventArgs e)
        {
            string cmdStr = "E" + "S";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void btnD_Ereset_Click(object sender, EventArgs e)// Dummy Encoder Reset to Zero
        {
            string cmdStr = "E" + "D";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tbIndexEnc_TextChanged(object sender, EventArgs e)
        {

        }

        private void calibrate_Click(object sender, EventArgs e)
        {
            double avg = 0;
            double sum = 0;


            string path = @"D:\CalibData" + $"{cbIndexer.Text}" + "_" + DateTime.Now.ToString("ddMMy_hhmmss") + ".txt";
            MessageBox.Show(path);

            string data;
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {

                    timer1.Enabled = false;
                    for (int i = 0; i < 20; i++)
                    {
                        Debug.WriteLine($"Count: {i}");
                        string cmdStr = "O" + "I"; // Send Indexer to origin
                        Send_to_client(cmdStr);
                        data = Send_to_client("HB");
                        CommandParser(data);
                        Debug.WriteLine("Homing Completed");
                        cmdStr = "E" + "I"; // erase the encoder register set to zero
                        Send_to_client(cmdStr);
                        data = Send_to_client("HB");
                        CommandParser(data);
                        Debug.WriteLine("Reset Encoder");
                        cmdStr = "A" + "S";
                        Send_to_client(cmdStr);

                        data = Send_to_client("HB");
                        string[] replystringArray = CommandParser(data);
                        cmdStr = "A" + "E";
                        Send_to_client(cmdStr);
                        data = Send_to_client("HB");
                        mean = Convert.ToInt32(replystringArray[2]) + Convert.ToInt32(CommandParser(data)[2]);
                        mean = mean / 2;
                        sw.WriteLine(replystringArray[2] + "," + CommandParser(data)[2] + "," + mean);
                        sum += Convert.ToDouble(mean);
                        //MessageBox.Show(Convert.ToString(sum));
                    }

                    timer1.Enabled = true;
                    sum = sum / 20;
                    sum = Convert.ToInt64(sum);
                    sw.WriteLine(sum);
                    MessageBox.Show(" Calibration completed ");

                }
            }
        }

        private void tbDcount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTreatment_Click(object sender, EventArgs e)
        {
            //backWorkerTreatment.RunWorkerAsync();
        }

        private void backWorkerTreatment_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }

        private void lblHelpCmd_Click(object sender, EventArgs e)
        {

        }

        private void indexTo_Click(object sender, EventArgs e)
        {
            int v = indexerEncPosition[(int)numericUpDown1.Value];
            string data = Send_to_client("P" + v.ToString());


        }

        private void button1_Click(object sender, EventArgs e)
        {


            ts.Active = true;


        }

        private void listOfCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpSheet helpSheet = new HelpSheet();
            helpSheet.Show();
        }

        private void tmrSourceOUT_Tick(object sender, EventArgs e)
        {

            Console.Beep();
        }

        private void btAs_Click(object sender, EventArgs e)
        {
            string countStr = tbIndxCount.Text;
            string cmdStr = "AS";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }

        private void btAE_Click(object sender, EventArgs e)
        {
            string countStr = tbIndxCount.Text;
            string cmdStr = "AE";
            string data = Send_to_client(cmdStr);
            CommandParser(data);
            Debug.WriteLine(cmdStr);
        }
    }
}