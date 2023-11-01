using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;

namespace TCPCLIENT
{
    public partial class Form1 : Form
    {
        TcpClient mtcpClient;
        byte[] mRx;
        byte[] tx;

        bool bConnected = false;
        bool bConnecting = false;
        private bool Connected { get => mtcpClient == null ? false : mtcpClient.Connected && bConnected; }

        int iport;
        IPAddress ipaddress;
        string sServerIP = "";
        string sServerPort = "";

        System.Timers.Timer timer = new System.Timers.Timer();

        public class Measure_interface
        {
            public int ID { get; set; }
            public string INTERFACE_ID { get; set; }
            public string MEASURE_DATE { get; set; }
            public string MEASURE_TIME { get; set; }
            public string CAR_NUMBER { get; set; }
        }

        //1 PM> install-package sqltabledependency 
        //2 Service Broker 활성화 : True
        //3 ALTER DATABASE Moist_SDY_BW SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;
        //4 ALTER AUTHORIZATION ON DATABASE::[Moist_SDY_BW] TO[moisture];
        //5 ALTER AUTHORIZATION on DATABASE::[Moist_SDY_BW] TO [sa]

        SqlTableDependency<Measure_interface> MeasureInterface;
        //6 MeasureInterface.Start(MS SQL Service Broker 설정) -> MeasureInterface_OnChange -> Send
        //7 --> WeightService_ReciveData(운영프로그램) 

        public Form1()
        {
            InitializeComponent();
            
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            bConnected = mtcpClient.Connected;
            if(!mtcpClient.Connected && !bConnecting)
            {
                bConnecting = true;
                mtcpClient.Close();
                mtcpClient = new TcpClient();
                mtcpClient.BeginConnect(ipaddress, iport, onCompleteConnect, mtcpClient);
            }
            timer.Enabled = true;
        }

        private void TcpConnect()
        {
            //연결이 되어 있는데 또 연결 방지용, 서버쪽에 연결된 노드가 늘어남
            if (mtcpClient != null && Connected /*mTcpClient.Connected*/) return;

            IPAddress ipa;
            int nPort;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            sServerIP = ConfigurationManager.ConnectionStrings["Moisture.connectIP"].ConnectionString;
            sServerPort = ConfigurationManager.ConnectionStrings["Moisture.connectPort"].ConnectionString;

            try
            {
                if (string.IsNullOrEmpty(sServerIP) || string.IsNullOrEmpty(sServerPort))
                    return;

                if (!IPAddress.TryParse(sServerIP, out ipa))
                {
                    MessageBox.Show("IP 주소를 입력하세요..");
                    return;
                }

                if (!int.TryParse(sServerPort, out nPort))
                {
                    nPort = 22245;
                }

                mtcpClient = new TcpClient();
                mtcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mtcpClient);

                ipaddress = ipa;
                iport = nPort;
                bConnecting = true;
                timer.Start();


            }
            catch (Exception exc)
            {
                bConnected = false;
                MessageBox.Show(exc.Message);
            }
        }

        private void onCompleteConnect(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.EndConnect(iar);
                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
                bConnected = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                bConnected = false;
            }
            bConnecting = false;
        }

        private void onCompleteReadFromServerStream(IAsyncResult iar)
        {
            TcpClient tcpc;
            int nCountBytesReceivedFromServer;
            string strReceived;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                nCountBytesReceivedFromServer = tcpc.GetStream().EndRead(iar);

                if(nCountBytesReceivedFromServer ==0)
                {
                    MessageBox.Show("Connection broken.");
                    return;
                }
                strReceived = Encoding.UTF8.GetString(mRx,0 ,nCountBytesReceivedFromServer);
                string s1 = Encoding.UTF8.GetString(mRx, 0, nCountBytesReceivedFromServer - 1);
                if (mRx[nCountBytesReceivedFromServer - 1] != '\n') 
                    return;
                printLine(strReceived);

                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);

                bConnected = true;

            }
            catch(Exception ex)
            {
                bConnected = false;
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void printLine(string _strPrint)
        {
            tbConsole.Invoke(new Action<string>(doInvoke), _strPrint);
        }
        public void doInvoke(string _strPrint)
        {
            tbConsole.Text = _strPrint + Environment.NewLine + tbConsole.Text;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress ipa;
            int nPort;

            try
            {
                if (mtcpClient != null && Connected) return;

                if (!IPAddress.TryParse(tbServerIP.Text, out ipa))
                {
                    MessageBox.Show("Please Supply an IP Address");
                    return;
                }

                if (!int.TryParse(tbServerPort.Text, out nPort))
                {
                    nPort = 23000;
                }

                mtcpClient = new TcpClient();
                mtcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mtcpClient);

                ipaddress = ipa;
                iport = nPort;
                bConnecting = true;
                timer.Start();
            }
            catch(Exception ex)
            {
                bConnected = false;
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPayload.Text + "\r\n")) return;

            try
            {
                if (!Connected) return;

                tx = Encoding.ASCII.GetBytes(tbPayload.Text + "\r\n");

                if(mtcpClient !=null)
                {
                    if(mtcpClient.Client.Connected)
                    {
                        mtcpClient.GetStream().BeginWrite(tx,0,tx.Length,onCompleteWriteToServer,mtcpClient);
                    }
                }
            }
            catch(Exception ex)
            {
                bConnected = false;
                MessageBox.Show(ex.Message);
            }

        }

        private void onCompleteWriteToServer(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var _con = ConfigurationManager.ConnectionStrings["Moisture.Login.Properties.Settings.moistureConnectionString"].ConnectionString;
            var tableName = "Measure_interface";
            var mapper = new ModelToTableMapper<Measure_interface>();
            mapper.AddMapping(c => c.INTERFACE_ID, "INTERFACE_ID");
            mapper.AddMapping(c => c.CAR_NUMBER, "CAR_NUMBER");

            MeasureInterface = new SqlTableDependency<Measure_interface>(_con, tableName: tableName, schemaName: "dbo", mapper: mapper);
            MeasureInterface.OnChanged += MeasureInterface_OnChange;
            MeasureInterface.Start();

        }

        private void MeasureInterface_OnChange(object sender, RecordChangedEventArgs<Measure_interface> e)
        {
            var changedEntity = e.Entity;
            string sSend = string.Format($"{Convert.ToChar(0x02)}{changedEntity.CAR_NUMBER}    01{Convert.ToChar(0x03)}");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + sSend);

            TcpConnect();
            TcpSend(sSend);
        }

        private void TcpSend(string sSendData)
        {
            if (string.IsNullOrEmpty(sSendData)) return;

            try
            {
                if (!Connected) return;

                System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);// EUC-KR
                byte[] tx = euckr.GetBytes(sSendData);

                if (mtcpClient != null)
                {
                    if (mtcpClient.Client.Connected)
                    {
                        mtcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
                    }
                }
            }
            catch (Exception exc)
            {
                bConnected = false;
                MessageBox.Show(exc.Message);
            }
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            MeasureInterface.Stop();
        }
    }
}
