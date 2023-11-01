using Moisture.Lib.Const;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Moisture.Login
{
    internal static class Program
    {
        private const string PROGRAM_LAUNCHER_NAME = "MoistureLancher";
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            FCSConst.IsRunmode = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if(args != null && args.Length !=0 && (args[0] ?? "")=="launcherok")
                {
                    Application.Run(new MAINaM000());
                }
                else
                {
                    // Process 클래스 : 로컬 및 원격 프로세스에 대한 액세스를 제공하고 로컬 시스템 프로세스를 시작하고 중지할 수 있습니다.
                    // using : 해당 객체를 어디서 부터 어디까지만 사용할 것인지 명시할때 사용
                    using (Process process = new Process())
                    {
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.FileName = Environment.CurrentDirectory + "\\MoistureLauncher.exe";
                        process.StartInfo.CreateNoWindow = true;
                        process.Start();
                    }
                    Application.Exit();                   
                }              
            }
            catch(Exception ex)
            {
                //string arg_C6_0 = ex.Message + ", [Source]=" + ex.Source + "\r\n";
                //string arg_C6_1 = ", [Method]=";
                //MethodBase expr_B5 = ex.TargetSite;
                //ErrorLogReport(arg_C6_0 + arg_C6_1 + ((expr_B5 !=null)? expr_B5.ToString(): null) + "\r\n" + ", [StackTrace]=" + ex.Message);

                string aMsg;
                aMsg = ex.Message;
                aMsg += ", [Source]=" + ex.Source + "\r\n";
                aMsg += ", [Method]=" + ex.TargetSite + "/r/n";
                aMsg += ", [StackTrace]=" + ex.StackTrace;
                ErrorLogReport(aMsg);

                Application.DoEvents();
                Application.ExitThread();
                Application.Exit();
            }                     
        }

        private static void ErrorLogReport(string LogMessage)
        {
            //StreamWriter expr_0F = new FileInfo("ErrorReport.txt").CreateText();
            //expr_0F.WriteLine("\n\r[" + DateTime.Now.ToString() + "]");
            //expr_0F.WriteLine(LogMessage);
            //expr_0F.WriteLine(expr_0F.NewLine);
            //expr_0F.Close();

            string fileName = "ErrorReport.txt";
            FileInfo fi = new FileInfo(fileName);
            StreamWriter sw = fi.CreateText();
            sw.WriteLine("\n\r[" + DateTime.Now.ToString() + "]");
            sw.WriteLine(LogMessage);
            sw.WriteLine(sw.NewLine);
            sw.Close();
        }
    }
}
