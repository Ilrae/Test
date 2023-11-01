
namespace TCPCLIENT
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbPayload = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(51, 330);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(161, 25);
            this.tbServerIP.TabIndex = 0;
            this.tbServerIP.Text = "127.0.0..1";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(51, 374);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(161, 25);
            this.tbServerPort.TabIndex = 1;
            this.tbServerPort.Text = "5025";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(51, 414);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(180, 24);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbPayload
            // 
            this.tbPayload.Location = new System.Drawing.Point(433, 330);
            this.tbPayload.Name = "tbPayload";
            this.tbPayload.Size = new System.Drawing.Size(217, 25);
            this.tbPayload.TabIndex = 3;
            this.tbPayload.Text = "Message";
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(433, 414);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(183, 23);
            this.btSend.TabIndex = 4;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(51, 33);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.Size = new System.Drawing.Size(627, 278);
            this.tbConsole.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.tbPayload);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.tbServerIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbPayload;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.TextBox tbConsole;
    }
}

