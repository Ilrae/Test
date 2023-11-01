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

namespace ThreadExp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RunAnithing(Label label)
        {
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(100);
                label.Text = i.ToString();
                label.Refresh();
            }
        }

        private async void RunAnithingAsync(Label label)
        {
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(100);
                label.Text = i.ToString();
                label.Refresh();
            }
        }

        private void btn_walk_Click(object sender, EventArgs e)
        {
            RunAnithingAsync(lb_walk);
        }

        private void btn_hand_Click(object sender, EventArgs e)
        {
            RunAnithingAsync(lb_hand);
        }

        private void btn_talk_Click(object sender, EventArgs e)
        {
            RunAnithingAsync(lb_talk);
        }

        // Task를 반환해야 await 키워드를 사용할 수 있다.
        private async Task RunAllAsync(Label label)
        {
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(100);
                lb_All.Items.Add($"{label.Name} - {i}");
            }
        }

        private async void btn_All_Click(object sender, EventArgs e)
        {
            await RunAllAsync(lb_walk);
            await RunAllAsync(lb_hand);
            await RunAllAsync(lb_talk);
        }
    }
}
