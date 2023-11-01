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

namespace ChatServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpListener _listener;

        private void btnListen_Click(object sender, EventArgs e)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"),8080);
            //수신 대기 실행
            _listener.Start();

            TcpClient client = _listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            // 버퍼. 데이터의 임시 저장 공간
            byte[] buffer = new byte[1024];
            //stream.Read(buffer, 0, 1024);
            //string strMessage = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            // Read 반환값은 데이터의 길이
            int read = stream.Read(buffer, 0, buffer.Length);

            string message = Encoding.UTF8.GetString(buffer, 0, read);

            MessageBox.Show(message);
        }
    }
}
