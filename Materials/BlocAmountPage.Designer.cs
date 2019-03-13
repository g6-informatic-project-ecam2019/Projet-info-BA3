namespace Materials
{
    partial class BlocAmountPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.homeBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.blocsTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.blocsTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of blocs";
            // 
            // homeBtn
            // 
            this.homeBtn.Location = new System.Drawing.Point(48, 93);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(75, 47);
            this.homeBtn.TabIndex = 2;
            this.homeBtn.Text = "HOME";
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(159, 93);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(75, 47);
            this.nextBtn.TabIndex = 3;
            this.nextBtn.Text = "Next";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // blocsTrackBar
            // 
            this.blocsTrackBar.Location = new System.Drawing.Point(138, 32);
            this.blocsTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.blocsTrackBar.Maximum = 7;
            this.blocsTrackBar.Minimum = 1;
            this.blocsTrackBar.Name = "blocsTrackBar";
            this.blocsTrackBar.Size = new System.Drawing.Size(96, 56);
            this.blocsTrackBar.TabIndex = 37;
            this.blocsTrackBar.Value = 1;
            this.blocsTrackBar.Scroll += new System.EventHandler(this.blocsTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 38;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // BlocAmountPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 163);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blocsTrackBar);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.homeBtn);
            this.Controls.Add(this.label1);
            this.Name = "BlocAmountPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blocs";
            ((System.ComponentModel.ISupportInitialize)(this.blocsTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.TrackBar blocsTrackBar;
        private System.Windows.Forms.Label label2;
    }
}