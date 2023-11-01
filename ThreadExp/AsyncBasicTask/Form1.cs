using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncBasicTask
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();
        private double ArriveTime => Math.Round(sw.ElapsedMilliseconds / 1000d, 2);

        public Form1()
        {
            InitializeComponent();
        }

        private void Start()
        {
            sw.Start();
            ClearLogs();
            ResetBugs();
        }

        private void End()
        {
            AddLogs($"총 소요시간은 : {ArriveTime}초");
            sw.Stop();
            sw.Reset();
        }
        private void AddLogs(params string[] texts) => listBox1.Items.AddRange(texts);

        private void ClearLogs() => listBox1.Items.Clear();

        private void ResetBugs()
        {
            var bugs = new List<Label> { bugA, bugB, bugC, bugD };
            bugs.ForEach(x => x.Left = 125);

        }

        // 이동 메서드
        private async Task<string> MoveLabel(Label lbl,int speed)
        {
            int endX = FinishLine.Left - lbl.Width;

            while (lbl.Left < endX)
            {
                lbl.Location = new Point(lbl.Left + speed * 4, lbl.Top);
                await Task.Delay(1);
            }

            lbl.Location = new Point(lbl.Left, lbl.Top);

            return $"{lbl.Text} 도착시간: {ArriveTime}초";
        }

        // 하나씩 출발
        private async void button1_Click(object sender, EventArgs e)
        {
            Start();

            string logA = await MoveLabel(bugA, 1);
            string logB = await MoveLabel(bugB, 2);
            string logC = await MoveLabel(bugC, 3);
            string logD = await MoveLabel(bugD, 4);

            AddLogs(logA, logB, logC, logD);
            End();
        }

        // A와 B가 먼저 출발
        private async void button2_Click(object sender, EventArgs e)
        {
            Start();

            Task<string> aTask = MoveLabel(bugA, 1);
            Task<string> bTask = MoveLabel(bugB, 2);

            //string LogA = await aTask;
            //string LogB = await bTask;

            string[] results =await Task.WhenAll(aTask, bTask);
            AddLogs(results);

            Task<string> cTask = MoveLabel(bugC, 3);
            Task<string> dTask = MoveLabel(bugD, 4);

            results = await Task.WhenAll(cTask, dTask);
            AddLogs(results);
            //await cTask;
            //await dTask;

            End();
        }

        // 다 같이 출발
        private async void button3_Click(object sender, EventArgs e)
        {
            Start();

            Task<string> aTask = MoveLabel(bugA, 1);
            Task<string> bTask = MoveLabel(bugB, 2);
            Task<string> cTask = MoveLabel(bugC, 3);
            Task<string> dTask = MoveLabel(bugD, 4);

            await aTask;
            await bTask;
            await cTask;
            await dTask;

            End();
        }
        // 다 같이 출발(WhenAny)
        private async void button4_Click(object sender, EventArgs e)
        {
            Start();

            Task<string> aTask = MoveLabel(bugA, 1);
            Task<string> bTask = MoveLabel(bugB, 2);
            Task<string> cTask = MoveLabel(bugC, 3);
            Task<string> dTask = MoveLabel(bugD, 4);

            var tasks = new List<Task>() { aTask, bTask, cTask, dTask };

            while(tasks.Count >0)
            {
                Task task = await Task.WhenAny(tasks);

                //if(task == cTask)
                //{
                //    string logC = await cTask;
                //    AddLogs(logC);
                //}

                string log = await (Task<string>)task;
                AddLogs(log);
                tasks.Remove(task);
            }

            End();
        }

        

    }
}
