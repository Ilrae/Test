using System;
using System.Drawing;
using System.Windows.Forms;

namespace InfoBox
{
    

    public enum InformationBoxInitialization
    {
        FromParametersOnly = 0,
        FromScopeAndParameters = 1
    }

    public enum InformationBoxButtons
    {
        AbortRetryIgnore = 0,
        OK = 1,
        OKCancel = 2,
        RetryCancel = 3,
        YesNo = 4,
        YesNoCancel = 5,
        YesNoUser1 = 6,
        OKCancelUser1 = 7,
        User1User2User3 = 8,
        User1User2 = 9,
        User1 = 10
    }

    public enum InformationBoxIcon
    {
        Asterisk = 0,
        Error = 1,
        Exclamation = 2,
        Hand = 3,
        Information = 4,
        None = 5,
        Question = 6,
        Stop = 7,
        Warning = 8,
        Success = 9,
        Fax = 10,
        Gamepad = 11,
        Joystick = 12,
        Keys = 13,
        Locker = 14,
        Phone = 15,
        Printer = 16,
        Scanner = 17,
        Speakers = 18
    }

    public enum InformationBoxResult
    {
        Abort = 0,
        Cancel = 1,
        Ignore = 2,
        No = 3,
        None = 4,
        OK = 5,
        Retry = 6,
        Yes = 7,
        User1 = 8,
        User2 = 9,
        User3 = 10
    }

    public enum InformationBoxDefaultButton
    {
        Button1 = 0,
        Button2 = 1,
        Button3 = 2
    }

    public enum InformationBoxButtonsLayout
    {
        GroupLeft = 0,
        GroupMiddle = 1,
        GroupRight = 2,
        Separate = 3
    }

    public enum InformationBoxAutoSizeMode
    {
        MinimumWidth = 0,
        MinimumHeight = 1,
        None = 2
    }

    public enum InformationBoxPosition
    {
        CenterOnParent = 0,
        CenterOnScreen = 1
    }

    public enum InformationBoxStyle
    {
        Standard = 0,
        Modern = 1
    }

    public enum AutoCloseDefinedParameters
    {
        Button = 0,
        TimeOnly = 1,
        Result = 2
    }
    
    [Flags]
    public enum InformationBoxCheckBox
    {
        Show = 1,
        Checked = 2,
        RightAligned = 4
    }

    public enum InformationBoxTitleIconStyle
    {
        None = 0,
        SameAsBox = 1,
        Custom = 2
    }
    
}