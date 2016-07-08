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
            this.txtBubblesheetLocation = new System.Windows.Forms.TextBox();
            this.txtFilterLocation = new System.Windows.Forms.TextBox();
            this.btnRunAndMark = new System.Windows.Forms.Button();
            this.txtAnswers = new System.Windows.Forms.TextBox();
            this.btnRunAndSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBubblesheetLocation
            // 
            this.txtBubblesheetLocation.Location = new System.Drawing.Point(794, 25);
            this.txtBubblesheetLocation.Multiline = true;
            this.txtBubblesheetLocation.Name = "txtBubblesheetLocation";
            this.txtBubblesheetLocation.Size = new System.Drawing.Size(200, 32);
            this.txtBubblesheetLocation.TabIndex = 1;
            this.txtBubblesheetLocation.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\scantron.png";
            // 
            // txtFilterLocation
            // 
            this.txtFilterLocation.Location = new System.Drawing.Point(794, 76);
            this.txtFilterLocation.Multiline = true;
            this.txtFilterLocation.Name = "txtFilterLocation";
            this.txtFilterLocation.Size = new System.Drawing.Size(200, 32);
            this.txtFilterLocation.TabIndex = 3;
            this.txtFilterLocation.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\filter10.txt";
            // 
            // btnRunAndMark
            // 
            this.btnRunAndMark.Location = new System.Drawing.Point(794, 165);
            this.btnRunAndMark.Name = "btnRunAndMark";
            this.btnRunAndMark.Size = new System.Drawing.Size(97, 47);
            this.btnRunAndMark.TabIndex = 9;
            this.btnRunAndMark.Text = "Run and Mark";
            this.btnRunAndMark.UseVisualStyleBackColor = true;
            this.btnRunAndMark.Click += new System.EventHandler(this.btnRunAndMark_Click);
            // 
            // txtAnswers
            // 
            this.txtAnswers.Location = new System.Drawing.Point(794, 127);
            this.txtAnswers.Multiline = true;
            this.txtAnswers.Name = "txtAnswers";
            this.txtAnswers.Size = new System.Drawing.Size(200, 32);
            this.txtAnswers.TabIndex = 10;
            this.txtAnswers.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\answers.txt";
            // 
            // btnRunAndSave
            // 
            this.btnRunAndSave.Location = new System.Drawing.Point(897, 165);
            this.btnRunAndSave.Name = "btnRunAndSave";
            this.btnRunAndSave.Size = new System.Drawing.Size(97, 47);
            this.btnRunAndSave.TabIndex = 11;
            this.btnRunAndSave.Text = "Run as AnswerKey";
            this.btnRunAndSave.UseVisualStyleBackColor = true;
            this.btnRunAndSave.Click += new System.EventHandler(this.btnRunAndSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(791, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bubblesheet Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(791, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(791, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Answer Key (Load/Save)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 667);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRunAndSave);
            this.Controls.Add(this.txtAnswers);
            this.Controls.Add(this.btnRunAndMark);
            this.Controls.Add(this.txtFilterLocation);
            this.Controls.Add(this.txtBubblesheetLocation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBubblesheetLocation;
        private System.Windows.Forms.TextBox txtFilterLocation;
        private System.Windows.Forms.Button btnRunAndMark;
        private System.Windows.Forms.TextBox txtAnswers;
        private System.Windows.Forms.Button btnRunAndSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

