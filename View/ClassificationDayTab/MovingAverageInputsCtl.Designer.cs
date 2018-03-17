namespace View.ClassificationDayTab
{
    partial class MovingAverageInputsCtl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.mediumMovingAverageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fastMovingAverageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.slowMovingAverageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.doAnalysisButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mediumMovingAverageNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastMovingAverageNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slowMovingAverageNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Slow M.A. Period:";
            // 
            // mediumMovingAverageNumericUpDown
            // 
            this.mediumMovingAverageNumericUpDown.Location = new System.Drawing.Point(288, 49);
            this.mediumMovingAverageNumericUpDown.Name = "mediumMovingAverageNumericUpDown";
            this.mediumMovingAverageNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.mediumMovingAverageNumericUpDown.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Medium M.A. Period:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fast M.A. Period:";
            // 
            // fastMovingAverageNumericUpDown
            // 
            this.fastMovingAverageNumericUpDown.Location = new System.Drawing.Point(288, 93);
            this.fastMovingAverageNumericUpDown.Name = "fastMovingAverageNumericUpDown";
            this.fastMovingAverageNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.fastMovingAverageNumericUpDown.TabIndex = 4;
            // 
            // slowMovingAverageNumericUpDown
            // 
            this.slowMovingAverageNumericUpDown.Location = new System.Drawing.Point(288, 5);
            this.slowMovingAverageNumericUpDown.Name = "slowMovingAverageNumericUpDown";
            this.slowMovingAverageNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.slowMovingAverageNumericUpDown.TabIndex = 5;
            // 
            // doAnalysisButton
            // 
            this.doAnalysisButton.Location = new System.Drawing.Point(9, 137);
            this.doAnalysisButton.Name = "doAnalysisButton";
            this.doAnalysisButton.Size = new System.Drawing.Size(399, 60);
            this.doAnalysisButton.TabIndex = 6;
            this.doAnalysisButton.Text = "Classify";
            this.doAnalysisButton.UseVisualStyleBackColor = true;
            // 
            // MovingAverageInputsCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.doAnalysisButton);
            this.Controls.Add(this.slowMovingAverageNumericUpDown);
            this.Controls.Add(this.fastMovingAverageNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mediumMovingAverageNumericUpDown);
            this.Controls.Add(this.label1);
            this.Name = "MovingAverageInputsCtl";
            this.Size = new System.Drawing.Size(417, 208);
            ((System.ComponentModel.ISupportInitialize)(this.mediumMovingAverageNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastMovingAverageNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slowMovingAverageNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown mediumMovingAverageNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fastMovingAverageNumericUpDown;
        private System.Windows.Forms.NumericUpDown slowMovingAverageNumericUpDown;
        private System.Windows.Forms.Button doAnalysisButton;
    }
}
