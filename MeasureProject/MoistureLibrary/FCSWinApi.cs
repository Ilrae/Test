using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows;

namespace Moisture.Lib.Win32
{
    public static class FCSWinApi
    {
        public const int CB_SETITEMHEIGHT = 0x0153;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int CS_DROPSHADOW = 0x00020000;
        public const int WM_NCPAINT = 0x0085;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
                  int hWnd,      // handle to destination window
                  int Msg,       // message
                  int wParam,  // first message parameter
                  int lParam   // second message parameter
        );

        public delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

        [DllImport("user32")]
        public static extern int GetClientRect(int hwnd, ref ClientRect lpRect);

        [DllImport("user32")]
        public static extern bool GetWindowRect(int hWnd, ref ClientRect lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int width, int height, int uFlags);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(int hWnd, out int id);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        [DllImport("gdi32.dll")]
        public static extern bool PaintRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, int crColor);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern int GetGraphicsMode(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern int SetGraphicsMode(IntPtr hdc, int iMode);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern bool InternetSetCookie([MarshalAs(UnmanagedType.LPWStr)] string lpszUrl, [MarshalAs(UnmanagedType.LPWStr)] string lpszCookieName, [MarshalAs(UnmanagedType.LPWStr)] string lpszCookieData);

        [DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern bool InternetGetCookie([MarshalAs(UnmanagedType.LPWStr)] string url, [MarshalAs(UnmanagedType.LPWStr)] string cookieName, StringBuilder cookieData, ref int size);

        [DllImport("kernel32dll")]
        public static extern void CloseHandle(IntPtr hProcess);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TRIVERTEX
    {
        public int x;
        public int y;
        public ushort Red;
        public ushort Green;
        public ushort Blue;
        public ushort Alpha;

        public TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
        {
            this.x = x;
            this.y = x;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = alpha;
        }
    }

    public enum PenStyle : int
    {
        PS_SOLID = 0, //The pen is solid.
        PS_DASH = 1, //The pen is dashed.
        PS_DOT = 2, //The pen is dotted.
        PS_DASHDOT = 3, //The pen has alternating dashes and dots.
        PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
        PS_NULL = 5, //The pen is invisible.
        PS_INSIDEFRAME = 6,// Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn
        // outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn 
        //completely inside the outer edge of the shape.
        PS_USERSTYLE = 7,
        PS_ALTERNATE = 8,
        PS_STYLE_MASK = 0x0000000F,

        PS_ENDCAP_ROUND = 0x00000000,
        PS_ENDCAP_SQUARE = 0x00000100,
        PS_ENDCAP_FLAT = 0x00000200,
        PS_ENDCAP_MASK = 0x00000F00,

        PS_JOIN_ROUND = 0x00000000,
        PS_JOIN_BEVEL = 0x00001000,
        PS_JOIN_MITER = 0x00002000,
        PS_JOIN_MASK = 0x0000F000,

        PS_COSMETIC = 0x00000000,
        PS_GEOMETRIC = 0x00010000,
        PS_TYPE_MASK = 0x000F0000
    };

    public struct ClientRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public struct Margins
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;

        public Margins(int top = 0, int bottom = 0, int left = 0, int right = 0)
        {
            topHeight = top;
            bottomHeight = bottom;
            leftWidth = left;
            rightWidth = right;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public bool fErase;
        public ClientRect rcPaint;
        public bool fRestore;
        public bool fIncUpdate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] rgbReserved;
    }
}
