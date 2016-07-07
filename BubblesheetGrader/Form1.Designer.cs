namespace BubblesheetGrader
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
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.txtBubblesheetLocation = new System.Windows.Forms.TextBox();
            this.btnLoadFilter = new System.Windows.Forms.Button();
            this.txtFilterLocation = new System.Windows.Forms.TextBox();
            this.btnRunFilter = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtAnswers = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(865, 50);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(129, 47);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // txtBubblesheetLocation
            // 
            this.txtBubblesheetLocation.Location = new System.Drawing.Point(794, 12);
            this.txtBubblesheetLocation.Multiline = true;
            this.txtBubblesheetLocation.Name = "txtBubblesheetLocation";
            this.txtBubblesheetLocation.Size = new System.Drawing.Size(200, 32);
            this.txtBubblesheetLocation.TabIndex = 1;
            this.txtBubblesheetLocation.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\scantron.png";
            // 
            // btnLoadFilter
            // 
            this.btnLoadFilter.Location = new System.Drawing.Point(865, 141);
            this.btnLoadFilter.Name = "btnLoadFilter";
            this.btnLoadFilter.Size = new System.Drawing.Size(129, 51);
            this.btnLoadFilter.TabIndex = 2;
            this.btnLoadFilter.Text = "Load Filter";
            this.btnLoadFilter.UseVisualStyleBackColor = true;
            this.btnLoadFilter.Click += new System.EventHandler(this.btnLoadFilter_Click);
            // 
            // txtFilterLocation
            // 
            this.txtFilterLocation.Location = new System.Drawing.Point(794, 103);
            this.txtFilterLocation.Multiline = true;
            this.txtFilterLocation.Name = "txtFilterLocation";
            this.txtFilterLocation.Size = new System.Drawing.Size(200, 32);
            this.txtFilterLocation.TabIndex = 3;
            this.txtFilterLocation.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\filter10.txt";
            // 
            // btnRunFilter
            // 
            this.btnRunFilter.Location = new System.Drawing.Point(865, 305);
            this.btnRunFilter.Name = "btnRunFilter";
            this.btnRunFilter.Size = new System.Drawing.Size(129, 51);
            this.btnRunFilter.TabIndex = 4;
            this.btnRunFilter.Text = "Run Filter";
            this.btnRunFilter.UseVisualStyleBackColor = true;
            this.btnRunFilter.Click += new System.EventHandler(this.btnRunFilter_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(645, 305);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(129, 47);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtAnswers
            // 
            this.txtAnswers.Location = new System.Drawing.Point(794, 198);
            this.txtAnswers.Multiline = true;
            this.txtAnswers.Name = "txtAnswers";
            this.txtAnswers.Size = new System.Drawing.Size(200, 32);
            this.txtAnswers.TabIndex = 10;
            this.txtAnswers.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\answers.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 667);
            this.Controls.Add(this.txtAnswers);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnRunFilter);
            this.Controls.Add(this.txtFilterLocation);
            this.Controls.Add(this.btnLoadFilter);
            this.Controls.Add(this.txtBubblesheetLocation);
            this.Controls.Add(this.btnLoadImage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.TextBox txtBubblesheetLocation;
        private System.Windows.Forms.Button btnLoadFilter;
        private System.Windows.Forms.TextBox txtFilterLocation;
        private System.Windows.Forms.Button btnRunFilter;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtAnswers;
    }
}

