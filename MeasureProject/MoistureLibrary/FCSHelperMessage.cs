using InfoBox;
using Moisture.Lib.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moisture.Lib.Message
{
    public class FCS_CustomLocation : IDisposable
    {
        private int MTries { get; set; } = 0;
        private Form MOwner { get; set; } 

        public FCS_CustomLocation(Form owner)
        {
            MOwner = owner;
            owner.BeginInvoke(new MethodInvoker(FindDialog));
        }

        ~FCS_CustomLocation()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(MOwner !=null)
                {
                    MOwner.Dispose();
                    MOwner = null;
                }
            }

            MTries = -1;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void FindDialog()
        {
            if(MTries < 0)
            {
                return;
            }

            FCSWinApi.EnumThreadWndProc callback = new FCSWinApi.EnumThreadWndProc(CheckWindow);

            if(FCSWinApi.EnumThreadWindows(FCSWinApi.GetCurrentThreadId(), callback, IntPtr.Zero))
            {
                if (++MTries < 10) MOwner.BeginInvoke(new MethodInvoker(FindDialog));
            }
        }

        private bool CheckWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a dialog
            StringBuilder sb = new StringBuilder(260);
            FCSWinApi.GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;

            Rectangle frmRect = new Rectangle(MOwner.Location, MOwner.Size);

            ClientRect dlgRect = new ClientRect();

            FCSWinApi.GetWindowRect((int)hWnd, ref dlgRect);

            FCSWinApi.MoveWindow(hWnd,
                frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                dlgRect.Right - dlgRect.Left,
                dlgRect.Bottom - dlgRect.Top, true);

            return false;
        }
    }

    public static class Mhelper
    {
        private static readonly Dictionary<string, FCS_MESSAGE> msglist = new Dictionary<string, FCS_MESSAGE>();

        public static bool IsError { get; set; }
        public static string MessageId { get; set; }
        public static string MessageText { get; set; }
        public static string MessageType { get; set; }
        public static string MessageArg { get; set; }

        public static void SetMessageList(DataTable dt)
        {
            msglist.Clear();

            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i< dt.Rows.Count; i++)
                {
                    string msgid = dt.Rows[i]["MSGID"].TryConvertDBNull(Convert.ToString, string.Empty).Trim();
                    string msgnxid = dt.Rows[i]["NXTID"].TryConvertDBNull(Convert.ToString, string.Empty).Trim();
                    string msgtp = dt.Rows[i]["MSGTP"].TryConvertDBNull(Convert.ToString, string.Empty).Trim();
                    var msgkr = new StringBuilder(dt.Rows[i]["MSGKR"].TryConvertDBNull(Convert.ToString, string.Empty).Trim());


                    while(!string.IsNullOrEmpty(msgnxid))
                    {
                        DataRow dr = dt.Select($"MSGID = '{msgnxid}'")?[0];

                        if(dr !=null)
                        {
                            msgnxid = dr["NXTID"].TryConvertDBNull(Convert.ToString, string.Empty).Trim();
                            msgkr.Append($"\n{dr["MSGKR"].TryConvertDBNull(Convert.ToString, string.Empty).Trim()}");
                        }
                        else
                        {
                            msgnxid = "";
                        }
                    }

                    FCS_MESSAGE msg = new FCS_MESSAGE(msgid, msgtp, msgkr.ToString());

                    msglist.Add(msg.MessageId, msg);
                }

                
            }
        }

        private static FCS_MESSAGE GetMsgById(string mid, string arg)
        {
            if(msglist.ContainsKey(mid))
            {
                return msglist[mid];
            }

            return null;
        }

        public static InformationBoxResult ShowReplace(string msgId, string argument = null, string replaceOldStr = "", string replaceNewStr = "", int timeout = -1, InformationBoxDefaultButton defaultButton = InformationBoxDefaultButton.Button1, string[] customButtons = null)
        {
            string msgText = "";
            string msgType = "";
            bool IsError = true;

            FCS_MESSAGE msg = GetMsgById(msgId, argument);
            if (msg != null)
            {
                msgText = msg.MessageText;
                msgType = msg.MessageType;
                IsError = msg.IsError;
            }
            if (argument != null)
            {
                string[] aryStr = argument.Split(';');
                for (int i = 0; i < aryStr.Length; i++)
                {
                    string[] splitStr = aryStr[i].Split('=');

                    string pname = splitStr[0];
                    string pval = splitStr[1];

                    msgText = msgText.Replace(pname, pval);
                }
            }

            msgId = (replaceOldStr.Length == 0) ? msgId : msgId.Replace(replaceOldStr, replaceNewStr);
            msgText = (replaceOldStr.Length == 0) ? msgText : msgText.Replace(replaceOldStr, replaceNewStr);

            return Show(msgId: msgId, msgText: msgText, msgType: msgType, IsError: IsError, timeout: timeout, defaultButton: defaultButton, customButtons: customButtons);
        }

        public static InformationBoxResult Show(FCS_MESSAGE msg, int timeout = -1, InformationBoxDefaultButton defaultButton = InformationBoxDefaultButton.Button1, string[] customButtons = null)
        {
            return Show(msgId: msg.MessageId, msgText: msg.MessageText, msgType: msg.MessageType, IsError: msg.IsError, timeout: timeout, defaultButton: defaultButton, customButtons: customButtons);
        }
        public static InformationBoxResult Show(string msgId, string msgText, string msgType, bool IsError, int timeout = -1, InformationBoxDefaultButton defaultButton = InformationBoxDefaultButton.Button1, string[] customButtons = null)
        {
            string mText = msgId + "\n" + msgText.Replace("\\n", "\n").Replace("\\r", "\r");

            MessageBoxButtons legacy_msgBtn;
            InformationBoxButton msgBtn;

            switch(msgType)
            {
                case "Q":                 
                        msgBtn = InformationBoxButton.User1User2;
                        legacy_msgBtn = MessageBoxButtons.OKCancel;
                        break;                
                case "Q2":
                    msgBtn = InformationBoxButton.User1User2User3;
                    legacy_msgBtn = MessageBoxButtons.YesNoCancel;
                    break;
                default:
                    msgBtn = InformationBoxButton.User1;
                    legacy_msgBtn = MessageBoxButtons.OK;
                    break;
            }

            _ = legacy_msgBtn;

            InformationBoxIcon msgIcon = GetMessageTypeIcon(msgType);
            MessageBoxDefaultButton legacy_defaultButton = (MessageBoxDefaultButton)Enum.Parse(typeof(MessageBoxDefaultButton), defaultButton.ToString());
            
            if(customButtons == null)
            {
                customButtons = new string[] { "확인", "취소" };
            }

            var dr = default(InformationBoxResult);

            mText = mText.Replace("\\n", "\n");
            mText = mText.Replace("\\r", "\r");

            if (timeout == -1)
            {
                dr = InformationBox.Show(
                        parent: form,
                        text: mText,
                        title: "Moisture Measurement Message",
                        titleIcon: new InformationBoxTitleIcon(Properties.Resources.water),
                        icon: msgIcon,
                        buttons: msgBtn,
                        defaultButton: defaultButton,
                        customButtons: customButtons,
                        //legacyButtons: legacy_msgBtn,
                        //legacyDefaultButton: legacy_defaultButton,
                        order: InformationBoxOrder.TopMost,
                        design: new DesignParameters(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255)));
            }
            else
            {
                dr = InformationBox.Show(
                        parent: form,
                        text: mText,
                        title: "Moisture Measurement Message",
                        titleIcon: new InformationBoxTitleIcon(Properties.Resources.water),
                        icon: msgIcon,
                        buttons: msgBtn,
                        defaultButton: defaultButton,
                        customButtons: customButtons,
                        //legacyButtons: legacy_msgBtn,
                        //legacyDefaultButton: legacy_defaultButton,
                        order: InformationBoxOrder.TopMost,
                        design: new DesignParameters(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255)),
                        autoClose: new AutoCloseParameters(timeout));
            }

        }

        private static InformationBoxIcon GetMessageTypeIcon(string type)
        {
            InformationBoxIcon icon = InformationBoxIcon.None;
            
            return icon;
        }

    }
}
