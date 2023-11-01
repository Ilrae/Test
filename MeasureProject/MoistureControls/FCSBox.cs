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

namespace FOCUS
{
    public partial class FCSBox : Control
    {
        protected Color _BorderColor = FCSYStyle.BaseLineColor;
        protected Padding _BorderWidth = new Padding(0);

        // 생성자      
        public FCSBox()
        {
            InitializeComponent();
        }

        public FCSBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [Category("FCS Properties")]
        [Description("Border color")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;

                PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
                this.OnPaint(e);
            }
        }

        [Category("FCS Properties")]
        [Description("Border width")]
        public Padding BorderWidth
        {
            get
            {
                return this._BorderWidth;
            }
            set
            {
                this._BorderWidth = value;

                PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
                this.OnPaint(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if(this.BorderWidth.Top > 0)
            {
                using(Pen p = new Pen(this._BorderColor,this._BorderWidth.Top))
                {
                    Point p1 = new Point(0, 0);
                    Point p2 = new Point(this.Width, 0);
                }
            }

            if(this.BorderWidth.Bottom >0)
            {
                using(Pen p = new Pen(this._BorderColor,this._BorderWidth.Bottom))
                {
                    Point p1 = new Point(0, this.Height - this.BorderWidth.Bottom);
                    Point p2 = new Point(this.Width, this.Height - this.BorderWidth.Bottom);
                }
            }

            if (this.BorderWidth.Left > 0)
            {
                using (Pen p = new Pen(this._BorderColor, this.BorderWidth.Left))
                {
                    Point p1 = new Point(0, 0);
                    Point p2 = new Point(0, this.Height);
                    g.DrawLine(p, p1, p2);
                }
            }

            if (this.BorderWidth.Right > 0)
            {
                using (Pen p = new Pen(this._BorderColor, this.BorderWidth.Right))
                {
                    Point p1 = new Point(this.Width - this.BorderWidth.Right, 0);
                    Point p2 = new Point(this.Width - this.BorderWidth.Right, this.Height);
                    g.DrawLine(p, p1, p2);
                }
            }


            base.OnPaint(e);
        }
    }
}
