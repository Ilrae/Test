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

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient _client; 

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _client = new TcpClient();
            _client.Connect(IPAddress.Parse("127.0.0.1"), 8080);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            NetworkStream stream = _client.GetStream();

            string text = "안녕 하세요.";
            var buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer,0,buffer.Length);     
        }
    }
}
