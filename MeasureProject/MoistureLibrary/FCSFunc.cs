using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moisture.Lib
{
    public static class FCSFunc
    {
        private static readonly int WM_NCPAINT = 0x0085;
        private static readonly int WM_ERASEBKGND = 0x0014;
        private static readonly int WM_PAINT = 0x000F;

        private static readonly ILog _log = LogManager.GetLogger(typeof(FCSFunc));

        public static Control GetParent(Control my_object, string type_name)
        {
            try
            {
                Control parent = my_object.Parent;

                while (parent != null)
                {
                    if (parent.GetType().BaseType.Name == type_name)
                    {
                        break;
                    }

                    parent = parent.Parent;
                }

                return parent;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetKeyVal(string keyValueString, string keyString, char delimiter = ';')
        {
            if (string.IsNullOrEmpty(keyValueString)) return ("");

            string rtnValue = "";

            string[] keyVals = keyString.Split(new char[] { delimiter });

            for (int i = 0; i < keyVals.Length; i++)
            {
                if (!string.IsNullOrEmpty(keyVals[i]))
                {
                    string[] kv = keyVals[i].Split('=');
                    string key = kv[0];
                    string val = kv[1];

                    if (key.Equals(keyString, StringComparison.OrdinalIgnoreCase))
                    {
                        rtnValue = val;
                        break;
                    }
                }
            }
            return rtnValue;
        }

        public static string SetKeyVal(string keyValueString, string keyString, object keyValue, char delimiter = ';')
        {
            keyValueString += (string.IsNullOrEmpty(keyValueString) ? "" : delimiter.ToString());
            keyValueString += keyString + "=" + keyValue as string;

            return keyValueString;
        }

        public static string TraceExceptionMessage(Exception ex)
        {
            string trcmsg = "";
            string trcstk = "";

            while (ex != null)
            {
                trcmsg += ex.Message + "\n";
                trcstk += ex.StackTrace + "\n";
                ex = ex.InnerException;
            }

            return trcmsg + "\n" + trcstk;
        }

        /// <summary>
        /// Kill Process
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static void KillProcess(string process_name, string main_window_title = "")
        {
            try
            {
                Process[] ps = Process.GetProcessesByName(process_name);
                foreach (Process p in ps)
                {
                    if ((p.MainWindowTitle.ToString().Contains(main_window_title)) || (p.MainWindowTitle.ToString() == ""))
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
