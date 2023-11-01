
namespace ThreadExp
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
            this.btn_walk = new System.Windows.Forms.Button();
            this.btn_hand = new System.Windows.Forms.Button();
            this.btn_talk = new System.Windows.Forms.Button();
            this.lb_walk = new System.Windows.Forms.Label();
            this.lb_hand = new System.Windows.Forms.Label();
            this.lb_talk = new System.Windows.Forms.Label();
            this.lb_All = new System.Windows.Forms.ListBox();
            this.btn_All = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_walk
            // 
            this.btn_walk.Location = new System.Drawing.Point(37, 24);
            this.btn_walk.Name = "btn_walk";
            this.btn_walk.Size = new System.Drawing.Size(115, 53);
            this.btn_walk.TabIndex = 0;
            this.btn_walk.Text = "걷기";
            this.btn_walk.UseVisualStyleBackColor = true;
            this.btn_walk.Click += new System.EventHandler(this.btn_walk_Click);
            // 
            // btn_hand
            // 
            this.btn_hand.Location = new System.Drawing.Point(37, 113);
            this.btn_hand.Name = "btn_hand";
            this.btn_hand.Size = new System.Drawing.Size(115, 53);
            this.btn_hand.TabIndex = 1;
            this.btn_hand.Text = "핸드폰";
            this.btn_hand.UseVisualStyleBackColor = true;
            this.btn_hand.Click += new System.EventHandler(this.btn_hand_Click);
            // 
            // btn_talk
            // 
            this.btn_talk.Location = new System.Drawing.Point(37, 202);
            this.btn_talk.Name = "btn_talk";
            this.btn_talk.Size = new System.Drawing.Size(115, 53);
            this.btn_talk.TabIndex = 2;
            this.btn_talk.Text = "말하기";
            this.btn_talk.UseVisualStyleBackColor = true;
            this.btn_talk.Click += new System.EventHandler(this.btn_talk_Click);
            // 
            // lb_walk
            // 
            this.lb_walk.AutoSize = true;
            this.lb_walk.Location = new System.Drawing.Point(182, 53);
            this.lb_walk.Name = "lb_walk";
            this.lb_walk.Size = new System.Drawing.Size(45, 15);
            this.lb_walk.TabIndex = 3;
            this.lb_walk.Text = "label1";
            // 
            // lb_hand
            // 
            this.lb_hand.AutoSize = true;
            this.lb_hand.Location = new System.Drawing.Point(182, 142);
            this.lb_hand.Name = "lb_hand";
            this.lb_hand.Size = new System.Drawing.Size(45, 15);
            this.lb_hand.TabIndex = 4;
            this.lb_hand.Text = "label2";
            // 
            // lb_talk
            // 
            this.lb_talk.AutoSize = true;
            this.lb_talk.Location = new System.Drawing.Point(182, 231);
            this.lb_talk.Name = "lb_talk";
            this.lb_talk.Size = new System.Drawing.Size(45, 15);
            this.lb_talk.TabIndex = 5;
            this.lb_talk.Text = "label3";
            // 
            // lb_All
            // 
            this.lb_All.FormattingEnabled = true;
            this.lb_All.ItemHeight = 15;
            this.lb_All.Location = new System.Drawing.Point(431, 24);
            this.lb_All.Name = "lb_All";
            this.lb_All.Size = new System.Drawing.Size(289, 379);
            this.lb_All.TabIndex = 6;
            // 
            // btn_All
            // 
            this.btn_All.Location = new System.Drawing.Point(275, 24);
            this.btn_All.Name = "btn_All";
            this.btn_All.Size = new System.Drawing.Size(115, 53);
            this.btn_All.TabIndex = 7;
            this.btn_All.Text = "전체";
            this.btn_All.UseVisualStyleBackColor = true;
            this.btn_All.Click += new System.EventHandler(this.btn_All_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_All);
            this.Controls.Add(this.lb_All);
            this.Controls.Add(this.lb_talk);
            this.Controls.Add(this.lb_hand);
            this.Controls.Add(this.lb_walk);
            this.Controls.Add(this.btn_talk);
            this.Controls.Add(this.btn_hand);
            this.Controls.Add(this.btn_walk);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_walk;
        private System.Windows.Forms.Button btn_hand;
        private System.Windows.Forms.Button btn_talk;
        private System.Windows.Forms.Label lb_walk;
        private System.Windows.Forms.Label lb_hand;
        private System.Windows.Forms.Label lb_talk;
        private System.Windows.Forms.ListBox lb_All;
        private System.Windows.Forms.Button btn_All;
    }
}

