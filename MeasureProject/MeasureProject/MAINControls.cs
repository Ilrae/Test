using Moisture.Lib;
using Moisture.Lib.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moisture.Login
{
    public delegate void EventShowHideArrowClick(string direct);

    /// <summary>
    /// 메뉴 경로 표시 컨트롤
    /// </summary>
    internal class MenuDisplayer : Panel
    {
        private Panel _Icon;
        private Label _Path;

        private int _size = 16;

        public MenuDisplayer()
        {
            this.BackColor = Color.Transparent;

            this._Icon = new Panel();
            this._Icon.BackgroundImage = Properties.Resources.MenuPathArrow2;
            this._Icon.Size = new Size(this._size, this._size);
            this._Icon.Visible = false;
            this.Controls.Add(this._Icon);

            this._Path = new Label();
            this._Path.Height = this._size;
            this._Path.Font = FCSYStyle.FontBase;
            this._Path.ForeColor = ColorTranslator.FromHtml("#483D8B");
            this._Path.TextAlign = ContentAlignment.MiddleLeft;
            this._Path.Text = "";
            this._Path.BackColor = Color.Transparent;
            this._Path.Visible = false;
            this.Controls.Add(this._Path);
        }

        public void DoSetMenuPath(string path)
        {
            this._Path.Text = path;
            this._Icon.Visible = path != "";
            this._Path.Visible = path != "";
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            this._Icon.Location = new Point(0, 13);
            this._Path.Location = new Point(this._size, 13);
            this._Path.Width = Math.Abs(this.Width - this._size);
        }
    }

    /// <summary>
    /// 로그인 사용자 정보를 표시하는 컨트롤
    /// </summary>
    public class UserInfoBox : Panel
    {
        private int _size = 16;

        public UserInfoBox()
        {
            this.BackColor = Color.Transparent;
        }

        public void DoTurnOn()
        {
            this.Controls.Clear();

            string usr_name = "";
            if (SHelper.UserID != null)
                usr_name = SHelper.UserName.Trim() + " (" + SHelper.UserID.Trim() + ")";
            else
                return;

            PictureBox UserIcon = new PictureBox();
            UserIcon.Size = new Size(this._size, this._size);
            UserIcon.Location = new Point(2, 10);
            UserIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            UserIcon.Image = Properties.Resources.UserMale;
            this.Controls.Add(UserIcon);

            Label UserName = new Label();
            UserName.Font = FCSYStyle.FontUserInfo;
            UserName.ForeColor = Color.FromArgb(29, 40, 60);
            UserName.BackColor = Color.Transparent;
            UserName.AutoSize = true;
            UserName.Text = usr_name;
            UserName.Location = new Point(UserIcon.Left + UserIcon.Width + 2, 10);
            this.Controls.Add(UserName);

            UserName.ContextMenu = new ContextMenu();
            UserName.ContextMenu.MenuItems.Add(new MenuItem("로그아웃",DoLogout));


        }

        private void DoLogout(object sender, EventArgs e) => (FCSFunc.GetParent(this, "Form") as MAINaM000).DoLogout();


    }

   
}
