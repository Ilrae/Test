using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moisture.Lib.Drawing
{
    public static class FCSYStyle
    {
        // Defalut 
        public static Color BaseLineColor = Color.FromArgb(255, 183, 183, 183);

        // Main Form
        // Background color
        public static Color BaseGradientColor1 = Color.FromArgb(255, 254, 254, 254);
        public static Color BaseGradientColor2 = Color.FromArgb(255, 243, 243, 243);

        // Background color for user informaion box
        public static Color UserInfoGradientColor1 = Color.FromArgb(255, 188, 226, 242);
        public static Color UserInfoGradientColor2 = Color.FromArgb(255, 160, 208, 230);

        // Font
        public static Font FontBase = new Font("Arial", 9);
        public static Font FontBaseBold = new Font("Arial", 9, FontStyle.Bold);
        public static Font FontGridBase = new Font("Arial", 8);
        public static Font FontGridBaseBold = new Font("Arial", 8, FontStyle.Bold);
        public static Font FontGridHeader = new Font("Arial", 8);
        public static Font FontControlBase = new Font("Arial", 9);
        public static Font FontArial12Base = new Font("Arial", 12);
        public static Font FontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
        public static Font FontUserInfo = new Font("맑은 고딕", 8);
        public static Font FontHanNormal = new Font("맑은 고딕", 9);
        public static Font FontHan8 = new Font("맑은 고딕", 8);
        public static Font FontHan8b = new Font("맑은 고딕", 8, FontStyle.Bold);
        public static Font FontHan9b = new Font("맑은 고딕", 9, FontStyle.Bold);
        public static Font FontHanSmall = new Font("맑은 고딕", 7);
        public static Font FontConsolasR = new Font("Consolas", 9);
        public static Font FontConsolas8 = new Font("Consolas", 8);
        public static Font FontConsolas8b = new Font("Consolas", 8, FontStyle.Bold);
        public static Font FontConsolas7 = new Font("Consolas", 7);
        public static Font FontConsolas7b = new Font("Consolas", 7, FontStyle.Bold);
        public static Font FontGridSmall = new Font("Arial", 7);

        // Image
        public static Image SplitArrowLeft = Properties.Resources.ShowHideButtonLeft;
        public static Image SplitArrowRight = Properties.Resources.ShowHideButtonRight;


        // 자동체번 color
        public static Color ColorAutoNumbering = Color.FromArgb(240, 240, 180);
    }
}
