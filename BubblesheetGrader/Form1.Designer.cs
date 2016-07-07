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
            this.txtScale = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.btnFilterValue = new System.Windows.Forms.Button();
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
            this.txtFilterLocation.Text = "C:\\Users\\Josh\\Desktop\\Projects\\BubblesheetGrader\\filter.txt";
            // 
            // btnRunFilter
            // 
            this.btnRunFilter.Location = new System.Drawing.Point(865, 198);
            this.btnRunFilter.Name = "btnRunFilter";
            this.btnRunFilter.Size = new System.Drawing.Size(129, 51);
            this.btnRunFilter.TabIndex = 4;
            this.btnRunFilter.Text = "Run Filter";
            this.btnRunFilter.UseVisualStyleBackColor = true;
            this.btnRunFilter.Click += new System.EventHandler(this.btnRunFilter_Click);
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(794, 255);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(67, 20);
            this.txtScale.TabIndex = 5;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(867, 255);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(59, 20);
            this.txtX.TabIndex = 6;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(932, 255);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(62, 20);
            this.txtY.TabIndex = 7;
            // 
            // btnFilterValue
            // 
            this.btnFilterValue.Location = new System.Drawing.Point(865, 281);
            this.btnFilterValue.Name = "btnFilterValue";
            this.btnFilterValue.Size = new System.Drawing.Size(129, 61);
            this.btnFilterValue.TabIndex = 8;
            this.btnFilterValue.Text = "Get Value";
            this.btnFilterValue.UseVisualStyleBackColor = true;
            this.btnFilterValue.Click += new System.EventHandler(this.btnFilterValue_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 667);
            this.Controls.Add(this.btnFilterValue);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtScale);
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
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnFilterValue;
    }
}

