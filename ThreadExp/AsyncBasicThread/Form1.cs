using AsyncBasicThread.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncBasicThread
{
    public partial class Form1 : Form
    {
        private const string SOURCE_FILE = "C:/Users/dnctm/OneDrive/바탕 화면/새 폴더/SourceDir/231010.xlsx";
        private const string DESTINATION_FILE = "C:/Users/dnctm/OneDrive/바탕 화면/새 폴더/SourceDir/231010.xlsx";

        public Form1()
        {
            InitializeComponent();
        }

        private void FileProgress(string currentUnits, string totalUnits, int percentage)
        {
            if(this.InvokeRequired)
            {
                BeginInvoke(new FileProgressDelegate(FileProgress),currentUnits, totalUnits, percentage);
            }
            else
            {
                label1.Text = $"{currentUnits} / {totalUnits} ({percentage}%)";
                progressBar1.Value = percentage;
            }
            
        }

        #region Main
        private async Task CopyFileAsync()
        {
            FileUtils.Copy(SOURCE_FILE, DESTINATION_FILE, fileProgress: FileProgress);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await CopyFileAsync();
        }

        
        #endregion

        #region Background
        private async Task CopyFileBackAsync()
        {
            await Task.Run(() =>
            {
                FileUtils.Copy(SOURCE_FILE, DESTINATION_FILE, fileProgress: FileProgress);
            });       
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await CopyFileBackAsync();
        }
        #endregion
    }
}
