using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TCPClient01
{
    public partial class Form1 : Form
    {
        TcpClient mTcpClient;
        byte[] mRx;

        //서버에 연결되어 있는지 확인
        bool bConnected = false;
        private bool Connected { get => mTcpClient == null ? false : mTcpClient.Connected && bConnected; }

        //접속시 사용한 IP PORT
        int iPort;
        IPAddress iPAddress;

        //재접속 타이머
        System.Timers.Timer timer1 = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elasped);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (mTcpClient != null && Connected /*mTcpClient.Connected*/) return ; //연결이 되어 있는데 또 연결 방지용, 서버쪽에 연결된 노드가 늘어남

            IPAddress ipa;
            int nPort;

            try
            {
                if (string.IsNullOrEmpty(tbServerIP.Text) || string.IsNullOrEmpty(tbServerPort.Text))
                    return;

                if (!IPAddress.TryParse(tbServerIP.Text, out ipa))
                {
                    MessageBox.Show("Please supply an IP Address.");
                    return;
                }

                if (!int.TryParse(tbServerPort.Text, out nPort))
                {
                    nPort = 23000;
                }

                mTcpClient = new TcpClient();
                mTcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mTcpClient);

                iPAddress   = ipa  ;
                iPort       = nPort;
                bConnecting = true ;
                timer1.Start();


            }
            catch (Exception exc)
            {
                bConnected = false;
                MessageBox.Show(exc.Message);
            }
        }

        bool bFirstConnect = false;
        bool bConnecting = false ;
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
                    MessageBox.Show("Connection broken.");
                    return;
                }
                strReceived = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer);
                string s1 = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer-1);
                if(mRx[nCountBytesReceivedFromServer-1] != '\n') return;
                printLine(strReceived);

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

        public void printLine(string _strPrint)
        {
            tbConsole.Invoke(new Action<string>(doInvoke), _strPrint);
        }

        public void doInvoke(string _strPrint)
        {
            tbConsole.Text = _strPrint + Environment.NewLine + tbConsole.Text;
        }

        byte[] tx;
        private void tbSend_Click(object sender, EventArgs e)
        {
            //byte[] tx;
            
            if (string.IsNullOrEmpty(tbPayload.Text)) return;

            try
            {
                if (!Connected) return;

                tx = Encoding.ASCII.GetBytes(tbPayload.Text + "\r\n");

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


    }
}
