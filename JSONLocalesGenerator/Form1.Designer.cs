namespace JSONLocalesGenerator
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btBrowseInput = new System.Windows.Forms.Button();
            this.btBrowseOutput = new System.Windows.Forms.Button();
            this.lbInput = new System.Windows.Forms.Label();
            this.lbOutput = new System.Windows.Forms.Label();
            this.btGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(81, 6);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(204, 20);
            this.tbInput.TabIndex = 0;
            this.tbInput.Text = "d:\\localization.xlsx";
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(81, 37);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(204, 20);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "d:\\";
            // 
            // btBrowseInput
            // 
            this.btBrowseInput.Location = new System.Drawing.Point(291, 4);
            this.btBrowseInput.Name = "btBrowseInput";
            this.btBrowseInput.Size = new System.Drawing.Size(25, 23);
            this.btBrowseInput.TabIndex = 2;
            this.btBrowseInput.Text = "...";
            this.btBrowseInput.UseVisualStyleBackColor = true;
            this.btBrowseInput.Click += new System.EventHandler(this.btBrowseInput_Click);
            // 
            // btBrowseOutput
            // 
            this.btBrowseOutput.Location = new System.Drawing.Point(291, 34);
            this.btBrowseOutput.Name = "btBrowseOutput";
            this.btBrowseOutput.Size = new System.Drawing.Size(25, 23);
            this.btBrowseOutput.TabIndex = 3;
            this.btBrowseOutput.Text = "...";
            this.btBrowseOutput.UseVisualStyleBackColor = true;
            this.btBrowseOutput.Click += new System.EventHandler(this.btBrowseOutput_Click);
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Location = new System.Drawing.Point(25, 9);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(50, 13);
            this.lbInput.TabIndex = 4;
            this.lbInput.Text = "Input file:";
            // 
            // lbOutput
            // 
            this.lbOutput.AutoSize = true;
            this.lbOutput.Location = new System.Drawing.Point(4, 39);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(71, 13);
            this.lbOutput.TabIndex = 5;
            this.lbOutput.Text = "Output folder:";
            // 
            // btGenerate
            // 
            this.btGenerate.Location = new System.Drawing.Point(7, 63);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(309, 38);
            this.btGenerate.TabIndex = 6;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 113);
            this.Controls.Add(this.btGenerate);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.btBrowseOutput);
            this.Controls.Add(this.btBrowseInput);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.tbInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Locales Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button btBrowseInput;
        private System.Windows.Forms.Button btBrowseOutput;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Label lbOutput;
        private System.Windows.Forms.Button btGenerate;
    }
}

