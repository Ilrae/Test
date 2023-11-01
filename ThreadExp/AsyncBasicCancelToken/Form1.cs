using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncBasicCancelToken
{
    // 비동기 취소 : CancellationTokenSource
    // 비동기 취소 여부 추적 : CancellationToken
    

    public partial class Form1 : Form
    {
        CancellationTokenSource tokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        private async Task DoWorkAsnyc(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(100);
                int.TryParse(lb_index.Text, out int value);
                lb_index.Text = (++value).ToString();
            }
        }

        //private async Task DoWorkAsnyc(CancellationToken token)
        //{
        //    while (true)
        //    {
        //        //throw new Exception();
        //        token.ThrowIfCancellationRequested();
        //        await Task.Delay(100);
        //        int.TryParse(lb_index.Text, out int value);
        //        lb_index.Text = (++value).ToString();
        //    }
        //}

        private async void btn_start_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            try
            {
                await DoWorkAsnyc(token);
            }
            catch(OperationCanceledException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
