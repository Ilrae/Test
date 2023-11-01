using Moisture.Lib.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FOCUS
{
   

    public static partial class ConHelper
    {
        public static GmsSplashBusy SplashBusy;

        public static IDisposable Busy(Form form,string message,int width = 300, int height = 70)
        {
            if(SplashBusy == null)
            {
                SplashBusy = new GmsSplashBusy(form, message, width, height, i_am_dispose: SplashDisPose);
                return SplashBusy;
            }
            else
            {
                SplashBusy.ChangeMessage(message);
                return new GmsSplashBusyNothing();
            }
        }

        public static void SplashDisPose() => SplashBusy = null;

        public static void SetControlEnable(Control parent,List<string> disableControls)
        {
            foreach (Control c in parent.Controls)
            {
                Application.DoEvents();
                if (disableControls.FindIndex(delegate (string s) { return s == c.Name; }) >-1)
                {
                    Type t = c.GetType();
                    PropertyInfo pi = t.GetProperty("SetEditMode");
                    if(pi != null)
                    {
                        pi.SetValue(c, FCSEditModeEnum.Editable, null);
                    }
                    else
                    {
                        c.Enabled = true;
                    }
                }
            }           
        }

        public static List<string> SetControlDisable(Control parent)
        {
            List<string> disableControls = new List<string>();
            foreach (Control c in parent.Controls)
            {
                if (c.GetType().BaseType.Name == "Form")
                    continue;

                if (c.GetType().Name == "FCSLabel") continue;
                if (c.GetType().Name == "FCSXTab") continue;

                Application.DoEvents();

                Type t = c.GetType();
                PropertyInfo pi = t.GetProperty("SetEditMode");
                if (pi != null)
                {
                    object value = pi.GetValue(c, null);
                    if ((FCSEditModeEnum)value == FCSEditModeEnum.Editable)
                    {
                        pi.SetValue(c, FCSEditModeEnum.ReadOnly, null);
                        disableControls.Add(c.Name);
                    }                    
                }
                else
                {
                    if (c.Enabled)
                    {
                        c.Enabled = false;
                        disableControls.Add(c.Name);
                    }
                }
            }
            return disableControls;
        }

        public static void DisposeBusy(Form form)
        {
            if (SplashBusy == null || SplashBusy.IsDispose()) return;

            SplashBusy.Dispose();

            Application.DoEvents();

            SplashBusy = null;

            if(form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    FCSWinApi.SetWindowPos((int)form.Handle,
                                            0,
                                            form.Location.X,
                                            form.Location.Y,
                                            form.Size.Width,
                                            form.Size.Height,
                                            0x0010);
                }));
            }
            else
            {
                FCSWinApi.SetWindowPos((int)form.Handle,
                                       0,
                                       form.Location.X,
                                       form.Location.Y,
                                       form.Size.Width,
                                       form.Size.Height,
                                       0x0010);
            }
        }

        /// <summary>
        /// 작업중 splach 화면의 메세지를 변경한다.
        /// </summary>
        /// <param name="msg"></param>
        public static void ChangeBusyMessage(string msg)
        {
            if (SplashBusy != null)
                SplashBusy.ChangeMessage(msg);
        }

        public static void ShowAlarmForWeb(string title,string address,int width = 800, int height = 600,
            bool showScrollbar = false)
        {
            /*string url = address;
            string cookieName = "AlarmTodayEnd";

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            DateTime date = DateTime.Now;

            StringBuilder cookieData = new StringBuilder();
            int cookieSize = 0;
            FCSWinApi.InternetGetCookie(url, cookieName, cookieData, ref cookieSize);
            cookieData = new StringBuilder(cookieSize);
            if (FCSWinApi.InternetGetCookie(url, cookieName, cookieData, ref cookieSize))
                if (cookieData.ToString().Trim() == cookieName + "=1")
                    return;

            AlarmView view = new AlarmView(title, address, width, height, showScrollbar)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            if (view.ShowDialog() == DialogResult.OK)
            {
                // Sat,06-Jan-2008 00:00:00 GMT
                string expire_time = string.Format("{0},{1}-{2}-{3} 23:59:59 GMT", date.ToString("ddd", ci), date.ToString("dd", ci), date.ToString("MMM", ci), date.ToString("yyyy", ci));
                FCSWinApi.InternetSetCookie(url, null, cookieName + "=1;expires=" + expire_time);
            }*/
        }


        /// <summary>
        /// 메세지 show.
        /// 메세지 띄울 때, busy splash를 제거하기 위해 만듬.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="msgId"></param>
        /// <param name="argument"></param>
        /// <param name="justMoment">
        ///     splash가 계속 진행되어야 하는 경우의 DialogResult를 받아야 한다.
        ///     None 이면 Dispose splash...</param>
        /// <returns></returns>
        public static DialogResult ShowM(Form form, string msgId, string argument = null,
            DialogResult justMoment = DialogResult.None)
        {
            if (SplashBusy != null)
            {
                if (justMoment == DialogResult.None)
                {
                    DisposeBusy(form);
                }
                else
                {
                    SplashBusy.JustMomentMyThread();
                }
            }

            DialogResult result = MHelper.Show(form, msgId, argument).ToDialogResult();

            if (justMoment != DialogResult.None)
            {
                if (result == justMoment)
                {
                    SplashBusy.ContinueMyThread();
                }
                else
                {
                    DisposeBusy(form);
                }
            }

            return result;
        }
    }
}
