
namespace AsyncBasicTask
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bugA = new System.Windows.Forms.Label();
            this.bugB = new System.Windows.Forms.Label();
            this.bugC = new System.Windows.Forms.Label();
            this.bugD = new System.Windows.Forms.Label();
            this.FinishLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 73);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(42, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 60);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 169);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 57);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(42, 252);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 48);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(51, 319);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(585, 94);
            this.listBox1.TabIndex = 4;
            // 
            // bugA
            // 
            this.bugA.AutoSize = true;
            this.bugA.Location = new System.Drawing.Point(125, 25);
            this.bugA.Name = "bugA";
            this.bugA.Size = new System.Drawing.Size(16, 15);
            this.bugA.TabIndex = 5;
            this.bugA.Text = "A";
            // 
            // bugB
            // 
            this.bugB.AutoSize = true;
            this.bugB.Location = new System.Drawing.Point(125, 91);
            this.bugB.Name = "bugB";
            this.bugB.Size = new System.Drawing.Size(17, 15);
            this.bugB.TabIndex = 6;
            this.bugB.Text = "B";
            // 
            // bugC
            // 
            this.bugC.AutoSize = true;
            this.bugC.Location = new System.Drawing.Point(125, 169);
            this.bugC.Name = "bugC";
            this.bugC.Size = new System.Drawing.Size(17, 15);
            this.bugC.TabIndex = 7;
            this.bugC.Text = "C";
            // 
            // bugD
            // 
            this.bugD.AutoSize = true;
            this.bugD.Location = new System.Drawing.Point(125, 252);
            this.bugD.Name = "bugD";
            this.bugD.Size = new System.Drawing.Size(17, 15);
            this.bugD.TabIndex = 8;
            this.bugD.Text = "D";
            // 
            // FinishLine
            // 
            this.FinishLine.Location = new System.Drawing.Point(703, 25);
            this.FinishLine.Name = "FinishLine";
            this.FinishLine.Size = new System.Drawing.Size(65, 256);
            this.FinishLine.TabIndex = 9;
            this.FinishLine.Text = "FinishLine";
            this.FinishLine.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FinishLine);
            this.Controls.Add(this.bugD);
            this.Controls.Add(this.bugC);
            this.Controls.Add(this.bugB);
            this.Controls.Add(this.bugA);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label bugA;
        private System.Windows.Forms.Label bugB;
        private System.Windows.Forms.Label bugC;
        private System.Windows.Forms.Label bugD;
        private System.Windows.Forms.Button FinishLine;
    }
}

