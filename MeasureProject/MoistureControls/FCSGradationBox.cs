using Moisture.Lib.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FOCUS
{
    public partial class FCSGradationBox : FCSBox
    {
        private Color _GradientColor1 = FCSYStyle.BaseGradientColor1;
        private Color _GradientColor2 = FCSYStyle.BaseGradientColor2;
        //private IContainer components;

        // 생성자      
        public FCSGradationBox()
        {
            DoubleBuffered = true;

            InitializeComponent();
        }

        public FCSGradationBox(IContainer container)
        {
            DoubleBuffered = true;

            container.Add(this);

            InitializeComponent();
        }

        [Category("FCS Properties")]
        [Description("Start color for Gradation.")]
        public Color GradientBackColor1
        {
            get
            {
                return this._GradientColor1;
            }
            set
            {
                this._GradientColor1 = value;

                // Repaint
                this.Refresh();
            }
        }

        [Category("FCS Properties")]
        [Description("End color for Gradation.")]
        public Color GradientBackColor2
        {
            get
            {
                return this._GradientColor2 ;
            }
            set
            {
                this._GradientColor2 = value;

                this.Refresh();
            }
        }

        [Category("FCS Properties")]
        [Description("User data")]
        public String UserData { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {
                using(var Lgb = new LinearGradientBrush(e.ClipRectangle,
                    _GradientColor1,_GradientColor2, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(Lgb, e.ClipRectangle);
                }
            }
            catch(Exception)
            {
                //Mhelper.Show(ex.Message);
            }

            base.OnPaint(e);
        }
    }
}
