namespace TCPClient01
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.tbPayload = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(7, 11);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.Size = new System.Drawing.Size(759, 285);
            this.tbConsole.TabIndex = 0;
            // 
            // tbPayload
            // 
            this.tbPayload.Location = new System.Drawing.Point(516, 303);
            this.tbPayload.Name = "tbPayload";
            this.tbPayload.Size = new System.Drawing.Size(250, 21);
            this.tbPayload.TabIndex = 1;
            this.tbPayload.Text = "TcpClient01";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(7, 330);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(250, 21);
            this.tbServerPort.TabIndex = 2;
            this.tbServerPort.Text = "5025";
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(7, 303);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(250, 21);
            this.tbServerIP.TabIndex = 3;
            this.tbServerIP.Text = "127.0.0.1";
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(516, 330);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(250, 21);
            this.tbSend.TabIndex = 4;
            this.tbSend.Text = "&Send";
            this.tbSend.UseVisualStyleBackColor = true;
            this.tbSend.Click += new System.EventHandler(this.tbSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(7, 357);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(250, 21);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 387);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.tbPayload);
            this.Controls.Add(this.tbConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "TCPClient 01";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.TextBox tbPayload;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.Button tbSend;
        private System.Windows.Forms.Button btnConnect;
    }
}

