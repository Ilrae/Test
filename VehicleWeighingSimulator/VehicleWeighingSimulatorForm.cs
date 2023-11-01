using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using System;
using System.Configuration;

namespace VehicleWeighingSimulator
{
    public partial class VehicleWeighingSimulatorForm : Form
    {
        TcpClient mTcpClient;
        byte[] mRx;

        //서버에 연결되어 있는지 확인
        bool bConnected = false;
        private bool Connected { get => mTcpClient == null ? false : mTcpClient.Connected && bConnected; }

        //접속시 사용할 IP PORT
        int iPort;
        IPAddress iPAddress;
        string sServerIP = "";
        string sServerPort = "";

        //재접속 타이머
        System.Timers.Timer timer1 = new System.Timers.Timer();

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

        public VehicleWeighingSimulatorForm()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elasped);
        }
        private void TcpConnect()
        {
            //연결이 되어 있는데 또 연결 방지용, 서버쪽에 연결된 노드가 늘어남
            if (mTcpClient != null && Connected /*mTcpClient.Connected*/) return; 

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

                mTcpClient = new TcpClient();
                mTcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mTcpClient);

                iPAddress = ipa;
                iPort = nPort;
                bConnecting = true;
                timer1.Start();


            }
            catch (Exception exc)
            {
                bConnected = false;
                MessageBox.Show(exc.Message);
            }
        }

        bool bConnecting = false;
        void onCompleteConnect(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                //iar.AsyncWaitHandle.WaitOne(1000, false);

                tcpc = (TcpClient)iar.AsyncState;
                tcpc.EndConnect(iar);
                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
                bConnected = true; //연결 성공

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                bConnected = false; //연결 실패

            }
            bConnecting = false;
        }

        void onCompleteReadFromServerStream(IAsyncResult iar)
        {
            TcpClient tcpc;
            int nCountBytesReceivedFromServer;
            string strReceived;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                nCountBytesReceivedFromServer = tcpc.GetStream().EndRead(iar);

                if (nCountBytesReceivedFromServer == 0)
                {
                    MessageBox.Show("연결이 끊어졌습니다.");
                    return;
                }
                strReceived = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer);
                string s1 = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer - 1);
                if (mRx[nCountBytesReceivedFromServer - 1] != '\n') return;
                //printLine(strReceived);

                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);

                bConnected = true; //연결 성공

            }
            catch (Exception exc)
            {

                bConnected = false; //연결 실패
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void onCompleteWriteToServer(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
                //bCompleted = true;
            }
            catch (Exception exc)
            {
                //bCompleted = false;
                MessageBox.Show(exc.Message);
            }
        }
        private void timer1_Elasped(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            bConnected = mTcpClient.Connected;
            if (!mTcpClient.Connected && !bConnecting)
            {
                bConnecting = true;
                mTcpClient.Close();
                mTcpClient = new TcpClient();
                mTcpClient.BeginConnect(iPAddress, iPort, onCompleteConnect, mTcpClient);
            }
            timer1.Enabled = true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var _con  = ConfigurationManager.ConnectionStrings["Moisture.Login.Properties.Settings.moistureConnectionString"].ConnectionString;
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

                if (mTcpClient != null)
                {
                    if (mTcpClient.Client.Connected)
                    {
                        mTcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
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
