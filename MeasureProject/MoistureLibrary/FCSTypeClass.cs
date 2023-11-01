using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using System.Data;

namespace Moisture.Lib
{
    /* Delegate */
    public delegate void TCallBackButtonClick();
    public delegate void TCallbackAfterPopup(Dictionary<string, dynamic> values);
    public delegate object TCallbackAfterPopup2(Dictionary<string, object> resultValue);
    public delegate void CallBackAfterSelectForPopup(Dictionary<string, dynamic> values);
    public delegate void CallBackAfterSelectForPopup2(Dictionary<string, dynamic> master,
                                            Dictionary<string, dynamic> detail);
    public delegate object TCallbackDialog(Form dlg, string button, Dictionary<string, object> result);


    public class FCSFormType
    {
        public string AssemblyName;
        public string ModuleName;
        public string Namespace;
        public string FormName;
    }

    public class FCSOpenTab
    {
        public int TabId;
        public string TabKey;
        public FCSRunInfo runInfo;
        public Control TabObj;
        public Control RunSpace;
        public Form FormObj;
        public bool OverArea;
    }

    public class FCSOpenForm
    {
        public string PgmId;
        public string AssemblyName;
        public string Namespace;
        public string FormName;
        public string FormText;
        public object Param;
        public object LoadCallBack;
        public object CloseCallBack;
    }
    public class FCSPgmItem
    {
        public string Callee;
        public string PgmCd;
        public string Group;
        public string Title;
        public string Argument;
        public bool Executable;
        public bool Editable;
        public bool Exportable;
        public string AuthOrgList;
    }

    public class FCSAutoNum
    {
        public bool IsError;        // 자동채번 생성 에러.
        public string AutoNumber;   // 자동채번.
        public bool IsInput;        // 수기입력 여부.
    }

    public class FCSRunParam
    {
        private readonly Dictionary<string, dynamic> Param;

        public int Count => Param.Count;

        public FCSRunParam()
        {
            Param = new Dictionary<string, dynamic>();
        }

        public void SetValue(string pkey, dynamic pval)
        {
            if (!Param.ContainsKey(pkey))
            {
                Param.Add(pkey, pval);
            }
            else
            {
                Param[pkey] = pval;
            }
        }

        public dynamic GetValue(string pkey)
        {
            if (Param.ContainsKey(pkey))
            {
                return Param[pkey];
            }
            else
            {
                MessageBox.Show("Not found parameter [" + pkey + "].", "ERROR:RunItemType.GetParaVal()", MessageBoxButtons.OK);
                return null;
            }
        }

        public string GetKeyByIndex(int index)
        {
            int no = 0;
            foreach (string key in Param.Keys)
            {
                if (no == index)
                {
                    return key;
                }

                no++;
            }

            return "";
        }

        public bool Exists(string key) => Param.ContainsKey(key);
    }

    public delegate void EventHandlerTranslateParam();
    public class FCSRunInfo
    {
        public int RunID { get; set; }
        public string RunMode { get; set; }
        public string RunModeText { get; set; }
        public string PgmCode { get; set; }

        public string RunVr { get; set; }
        public string RunTc { get; set; }

        public string FormName { get; set; }
        public string FormTitle { get; set; }
        public Control TabIcon { get; set; }
        public Control TitleCtl { get; set; }
        public Control ImagePanelLeft { get; set; }
        public Control ImagePanelRight { get; set; }
        public Image DisplayIcon { get; set; }
        public Form AllocatedForm { get; set; }
        public Form OwnerForm { get; set; }
        public int ParamCnt { get; set; }
        public FCSPgmItem PgmInfo { get; set; }
        public string PgmPath { get; set; }
        public bool IsTranslate { get; set; }

        private readonly FCSRunParam Param = new FCSRunParam();

        public event EventHandlerTranslateParam OnTranslateParam;

        // 복사 생성자
        public FCSRunInfo(FCSRunInfo preRunInfo)
        {
            RunID = preRunInfo.RunID;
            RunMode = preRunInfo.RunMode;
            RunModeText = preRunInfo.RunModeText;
            PgmCode = preRunInfo.PgmCode;
            RunVr = preRunInfo.RunVr;
            RunTc = preRunInfo.RunTc;
            FormName = preRunInfo.FormName;
            FormTitle = preRunInfo.FormTitle;
            TabIcon = preRunInfo.TabIcon;
            TitleCtl = preRunInfo.TitleCtl;
            ImagePanelLeft = preRunInfo.ImagePanelLeft;
            ImagePanelRight = preRunInfo.ImagePanelRight;
            DisplayIcon = preRunInfo.DisplayIcon;
            AllocatedForm = preRunInfo.AllocatedForm;
            OwnerForm = preRunInfo.OwnerForm;
            ParamCnt = preRunInfo.ParamCnt;
            PgmInfo = preRunInfo.PgmInfo;
            PgmPath = preRunInfo.PgmPath;
            IsTranslate = preRunInfo.IsTranslate;
            Param = preRunInfo.Param;
        }

        public FCSRunInfo(FCSPgmItem pi, string pRunMode = "")
        {
            PgmCode = pi.PgmCd;
            FormName = pi.Callee; ;
            RunMode = pRunMode;
            PgmInfo = pi;
            ParamCnt = 0;
        }

        public FCSRunInfo(string PgmCode, string FormName, string pRunMode = "")
        {
            this.PgmCode = PgmCode;
            this.FormName = FormName;
            RunMode = pRunMode;
            ParamCnt = 0;
        }

        public FCSRunInfo(string FormName, string pRunMode = "")
        {
            PgmCode = FormName;
            this.FormName = FormName;
            RunMode = pRunMode;
            ParamCnt = 0;
        }

        public void SetParam(string pKey, dynamic pValue)
        {
            Param.SetValue(pKey, pValue);
            ParamCnt++;
        }

        public dynamic GetParam(string pKey)
        {
            if (Param.Exists(pKey))
            {
                return Param.GetValue(pKey);
            }
            else
            {
                return "*null*";
            }
        }

        public string GetRunValue(string key)
        {
            string value = "";
            string[] arys = this.RunVr.Split(';');
            foreach (string s in arys)
            {
                if (s.IndexOf(key + "=") > -1)
                {
                    value = s.Replace(key + "=", "");
                }
            }
            return value;
        }

        public void RaiseEvent() => OnTranslateParam?.Invoke();

        public bool HasParam(string key) => Param.Exists(key);
    }

    public class FCS_MESSAGE
    {
        public bool IsError { get; set; }
        public string MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public string MessageArg { get; set; }

        public FCS_MESSAGE() { }

        public FCS_MESSAGE(string messageKeyedString)
        {
            MessageId = FCSFunc.GetKeyVal(messageKeyedString, "MSGID");
            MessageType = FCSFunc.GetKeyVal(messageKeyedString, "MSGTP");
            //MessageText = FCSFunc.GetKeyVal(messageKeyedString, "MSGKR");
            MessageText = FCSFunc.GetKeyVal(messageKeyedString, "MSGTX");
            MessageArg = FCSFunc.GetKeyVal(messageKeyedString, "MSGAG");
            IsError = MessageType.Equals("E");
        }

        public FCS_MESSAGE(string messageId, string messageType, string messageText, string messageArg = null)
        {
            MessageId = messageId;
            MessageType = messageType;
            MessageText = messageText;
            MessageArg = messageArg;
            IsError = MessageType.Equals("E");
        }
    }
    //서비스에서 사용
    public class FCS_DALRESULT
    {
        public bool IsError { get; set; }
        public string ConnectionString { get; set; }
        public string MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public DataTable ResultSet { get; set; }
        public DataSet ResultDataSet { get; set; }
        public DataContractClass OutputSet { get; set; }
    }

    //서비스에서 사용
    public class FCS_SVCRESULT
    {
        public bool IsError { get; set; }
        public string ConnectionString { get; set; }
        public string MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public string ResultSet { get; set; }
        public string ResultDataSet { get; set; }
        public string OutputSet { get; set; }
    }

    //클라이언트용
    public class FCS_SVCRCV
    {
        public bool IsError { get; set; }
        public string ConnectionString { get; set; }
        public string MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public DataTable ResultSet { get; set; }
        public DataSet ResultDataSet { get; set; }
        public DataContractClass OutputSet { get; set; }

        public dynamic Get(string colName)
        {
            if (ResultSet.Rows.Count > 0)
            {
                switch (ResultSet.Rows[0][colName].GetType().Name)
                {
                    case "System.String":
                        return ResultSet.Rows[0][colName].ToString().Trim();

                    case "System.Double":
                    case "System.Decimal":
                    case "System.Int32":
                        return ResultSet.Rows[0][colName].ToNumber();

                    default:
                        return ResultSet.Rows[0][colName];
                }
            }
            return null;
        }
    }

    public class FCS_SYSCOLINFO
    {
        public string TableName { get; set; }
        public string ColName { get; set; }
        public string ColType { get; set; }
        public int ColLength { get; set; }
        public int ColNumScale { get; set; }
    }


    public enum FCSRUNSTAT
    {
        OK, ERROR
    }

    [Serializable()]
    public class FCSException : System.Exception
    {
        public FCSException() : base() { }
        public FCSException(string message) : base(message) { }
        public FCSException(string messageid, string message) : base(message) { _ = messageid; }
        public FCSException(string message, System.Exception inner) : base(message, inner) { }

        protected FCSException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class ExcelBE
    {
        public int Row = 0;
        public int Col = 0;
        public string Text = string.Empty;
        public string StartCell = string.Empty;
        public string EndCell = string.Empty;
        public string InteriorColor = string.Empty;
        public bool IsMerge = false;
        public int Size = 0;
        public string FontColor = string.Empty;
        public string Format = string.Empty;

        public ExcelBE(int row, int col, string text,
            string startCell = "", string endCell = "", string interiorColor = "",
            bool isMerge = false, int size = 10, string fontColor = "", string format = null)
        {
            Row = row;
            Col = col;
            Text = text;
            StartCell = startCell;
            EndCell = endCell;
            InteriorColor = interiorColor;
            IsMerge = isMerge;
            Size = size;
            FontColor = fontColor;
            Format = format;
        }

        public ExcelBE()
        { }
    }

    public class FCS_MLANGUAGE
    {
        public string CAPTX { get; set; }
        public string MNLVL { get; set; }

        public FCS_MLANGUAGE() { }

        public FCS_MLANGUAGE(string CAPTX, string MNLVL)
        {
            this.CAPTX = CAPTX;
            this.MNLVL = MNLVL;
        }
    }

    public class FCS_MCAPTION
    {
        public string LNGCD { get; set; }
        public string MNLVL { get; set; }

        public FCS_MCAPTION() { }

        public FCS_MCAPTION(string LNGCD, string MNLVL)
        {
            this.LNGCD = LNGCD;
            this.MNLVL = MNLVL;
        }
    }

}
