using FOCUS;
using Moisture.Lib;
using Moisture.Lib.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moisture.Login
{
    public partial class MAINaM000 : Form
    {
        private readonly TaskScheduler taskScheduler;

        // controls
        private FCSBox _MainBox;
        private FCSGradationBox _Header;
        private MenuDisplayer _MenuPath;
        private UserInfoBox _UserInfo;

        private FCSBox _MenuBox;
        private FcsDrawButton[] _aryButtons;
        private FCSGrid _Menu;

        private FCSBox _ContentBox;
        private FCSOpenTabBox _OpenTabBox;
        private FCSBox _OpenArea;
        private FCSRoundBox _HiddenTabBox;

        private MenuShowHideArrow _ShowHideArrow;

        // private PictureBox _Title;
        public MAINaM000()
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            InitializeComponent();

            Build();

            global::Moisture.Login.Properties.Settings.Default.Reload();

            var naType = $"{(global::Moisture.Login.Properties.Settings.Default.NA_TYPE)}".TryConvertDBNull(Convert.ToString, "TR1300");
        }

        private void Build()
        {
            BuildContorls();
            BuildMenuTree();
            BuildEvent();
        }

        private void BuildContorls()
        {
            WindowState = FormWindowState.Maximized;

            // Main Box
            _MainBox = new FCSBox
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };
            Controls.Add(_MainBox);

            // Header box
            _Header = new FCSGradationBox
            {
                GradientBackColor1 = FCSYStyle.UserInfoGradientColor1,
                GradientBackColor2 = FCSYStyle.UserInfoGradientColor2,
                BorderWidth = new Padding(0, 0, 0, 1),
                BorderColor = Color.FromArgb(183, 183, 183)
            };

            _MenuPath = new MenuDisplayer();

            _UserInfo = new UserInfoBox();

            _MainBox = new FCSBox
            {
                BorderWidth = new Padding(0, 0, 1, 0),
                BorderColor = ColorTranslator.FromHtml("#B7B7B7")
            };

            _aryButtons = new FcsDrawButton[4];
        }

        private void BuildMenuTree()
        {
            throw new NotImplementedException();
        }

        private void BuildEvent()
        {
            throw new NotImplementedException();
        }

        
        public void DoLogout()
        {
            _MainBox.Controls.Clear();
            _MainBox.Refresh();

            using(Container.Busy(this,"로그아웃"))
            {

            }
        }
        
    }
}
