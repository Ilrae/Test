namespace TCPServer01
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
            this.tbConsoleOutput = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.btnStartListening = new System.Windows.Forms.Button();
            this.tbPayload = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnFindIPv4IP = new System.Windows.Forms.Button();
            this.lbClients = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbConsoleOutput
            // 
            this.tbConsoleOutput.Location = new System.Drawing.Point(7, 6);
            this.tbConsoleOutput.Multiline = true;
            this.tbConsoleOutput.Name = "tbConsoleOutput";
            this.tbConsoleOutput.Size = new System.Drawing.Size(724, 212);
            this.tbConsoleOutput.TabIndex = 0;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(7, 253);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(178, 21);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "5025";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(7, 225);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(178, 21);
            this.tbIPAddress.TabIndex = 2;
            this.tbIPAddress.Text = "127.0.0.1";
            // 
            // btnStartListening
            // 
            this.btnStartListening.Location = new System.Drawing.Point(191, 225);
            this.btnStartListening.Name = "btnStartListening";
            this.btnStartListening.Size = new System.Drawing.Size(178, 21);
            this.btnStartListening.TabIndex = 4;
            this.btnStartListening.Text = "Start Listening";
            this.btnStartListening.UseVisualStyleBackColor = true;
            this.btnStartListening.Click += new System.EventHandler(this.btnStartListening_Click);
            // 
            // tbPayload
            // 
            this.tbPayload.Location = new System.Drawing.Point(737, 225);
            this.tbPayload.Name = "tbPayload";
            this.tbPayload.Size = new System.Drawing.Size(208, 21);
            this.tbPayload.TabIndex = 5;
            this.tbPayload.Text = "TcpServer01";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(737, 252);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(208, 21);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnFindIPv4IP
            // 
            this.btnFindIPv4IP.Location = new System.Drawing.Point(191, 253);
            this.btnFindIPv4IP.Name = "btnFindIPv4IP";
            this.btnFindIPv4IP.Size = new System.Drawing.Size(178, 21);
            this.btnFindIPv4IP.TabIndex = 8;
            this.btnFindIPv4IP.Text = "Find IP";
            this.btnFindIPv4IP.UseVisualStyleBackColor = true;
            this.btnFindIPv4IP.Click += new System.EventHandler(this.btnFindIPv4IP_Click_1);
            // 
            // lbClients
            // 
            this.lbClients.FormattingEnabled = true;
            this.lbClients.ItemHeight = 12;
            this.lbClients.Location = new System.Drawing.Point(738, 23);
            this.lbClients.Name = "lbClients";
            this.lbClients.Size = new System.Drawing.Size(207, 196);
            this.lbClients.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(735, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Clients";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 284);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbClients);
            this.Controls.Add(this.btnFindIPv4IP);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbPayload);
            this.Controls.Add(this.btnStartListening);
            this.Controls.Add(this.tbIPAddress);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbConsoleOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "TCP Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbConsoleOutput;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Button btnStartListening;
        private System.Windows.Forms.TextBox tbPayload;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnFindIPv4IP;
        private System.Windows.Forms.ListBox lbClients;
        private System.Windows.Forms.Label label3;
    }
}

