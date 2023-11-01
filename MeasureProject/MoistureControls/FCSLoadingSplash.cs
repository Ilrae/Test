using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FOCUS
{
    public delegate string DelegateAskSPlashSttus();

    public delegate void DelegateAnimationStop();

    public delegate string DelegateGiveMeMessage();

    public delegate void DelegateIamDispose();

    /// <summary>
    /// Name : GmsSplash
    /// 
    /// Description :
    ///     using 사용을 위한 Disposable Class
    /// </summary>
    public class GmsSplashBusy : IDisposable
    {
        private Thread _Thread;
        private readonly Form _Parent;
        private GmsSpashForm SplashForm;

        private Form _ParentMdi;
        private readonly int _width;
        private readonly int _height;

        private bool _dispose = false;
        private bool _animationStop = false;
        private bool _animationJustMoment = false;

        private readonly List<string> EnabledControls = new List<string>();

        private delegate void InvokeShowDialog();

        private readonly DelegateIamDispose IamDispose; 

        public string Message { get; set; }

        public GmsSplashBusy(Form parent, string message, int width = 300, int height = 70,
            DelegateIamDispose i_am_dispose = null)
        {
            _Parent = parent;
            Message = message;
            _width = width;
            _height = height;

            IamDispose = i_am_dispose;

            FindMidForm(_Parent);

            EventArgs e = new EventArgs();
            ParentMdi_Resize(_ParentMdi, e);

            StartThread();

        }

        private void ParentMdi_Resize(Form parentMdi, EventArgs e)
        {
            //_TPanel.Size =
            //    new Size(_ParentMdi.ClientRectangle.Width, _ParentMdi.ClientRectangle.Height);

            //SetLocationForTransparentForm();
        }

        ~GmsSplashBusy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispossing)
        {
            _dispose = true;

            if(_Thread != null)
            {
                while (!_animationStop)
                {
                    try
                    {
                        Application.DoEvents();
                    }
                    catch (Exception)
                    {

                    }
                }

                Thread.Sleep(100);

                _Thread.Join();
            }

            IamDispose();

            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);

            if(SplashForm != null)
            {
                SplashForm.Dispose();
            }

            SplashForm = null;
        }

        public bool IsDispose() => _dispose;

        private void StartThread()
        {
            ThreadStart ts = new ThreadStart(ShowLoading);
            _Thread = new Thread(ts)
            {
                IsBackground = true
            };
            _Thread.SetApartmentState(ApartmentState.STA);
            _Thread.Start();
        }

        private void ShowLoading()
        {
            try
            {
                SplashForm = new GmsSpashForm(this, Message, _width, _height, AskSplashStatus, AnimationStop, GiveMeMessage);

                int cw = _ParentMdi.ClientRectangle.Width;
                int ch = _ParentMdi.ClientRectangle.Height;
                int w = _width;
                int h = _height;
                int border_width = Convert.ToInt32((_ParentMdi.DesktopBounds.Width - w) / 2);
                int x = Convert.ToInt32(_ParentMdi.DesktopBounds.X + (cw / 2) - (w / 2));
                int y = _ParentMdi.DesktopBounds.Y + (ch / 2) - (h / 2);
                SplashForm.SetDesktopBounds(x, y, w, h);
                SplashForm.ShowDialog();

            }
            catch(Exception)
            {

            }
        }

        private string AskSplashStatus() => _dispose ? "END" : _animationJustMoment ? "STOP" : "RUN";

        private void AnimationStop() => _animationStop = true;

        private string GiveMeMessage() => Message;       

        private void FindMidForm(Control parent)
        {
            if (parent.GetType().Name.ToUpper() == "MAINAM000")
            {
                _ParentMdi = parent as Form;
                return;
            }
            else
            {
                if (parent.Parent == null)
                {
                    if ((parent as Form).Owner != null)
                    {
                        FindMidForm((parent as Form).Owner);
                    }

                    else
                    {
                        _ParentMdi = parent as Form;
                        return;
                    }
                }
                else
                {
                    FindMidForm(parent.Parent);
                }
            }
        }
        
        public void ChangeMessage(string message)
        {
            int count = 0;
            while (count < 5)
            {
                Application.DoEvents();
                if(SplashForm != null)
                {
                    SplashForm.ChangeMessage(message);
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                    count++;
                }
            }
        }

        public void JustMomentMyThread()
        {
            _animationJustMoment = true;
            Thread.Sleep(100);
            _Thread.Abort();
            _Thread = null;
        }
            
        public void ContinueMyThread()
        {
            _animationJustMoment = false;
            StartThread();
        }
    }

    public class GmsSplashBusyNothing : IDisposable
    {
        public GmsSplashBusyNothing()
        {
        }

        ~GmsSplashBusyNothing()
        {
            Dispose(false);
        }

        // Flag: Has Dispose already been called?
        private bool disposed = false;
        // Instantiate a SafeHandle instance.
        private readonly System.Runtime.InteropServices.SafeHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Thread.Sleep(100);

            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        
        
    }


    /// <summary>
    /// 
    /// </summary>
    public class GmsSpashForm : Form
    {
        public GmsSpashForm(GmsSplashBusy gsb,
            string message,
            int width,
            int height,
            DelegateAskSPlashSttus ask_splash_status,
            DelegateAnimationStop animation_stop,
            DelegateGiveMeMessage give_me_message)
        {

        }

        internal void ChangeMessage(string message)
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
