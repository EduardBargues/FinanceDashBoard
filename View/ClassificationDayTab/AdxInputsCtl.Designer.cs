namespace View.ClassificationDayTab
{
    partial class AdxInputsCtl
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
            this.label2 = new System.Windows.Forms.Label();
            this.dxPeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.averagePeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DoClassificationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dxPeriodNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagePeriodNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dx Period:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Average Period:";
            // 
            // dxPeriodNumericUpDown
            // 
            this.dxPeriodNumericUpDown.Location = new System.Drawing.Point(229, 6);
            this.dxPeriodNumericUpDown.Name = "dxPeriodNumericUpDown";
            this.dxPeriodNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.dxPeriodNumericUpDown.TabIndex = 3;
            // 
            // averagePeriodNumericUpDown
            // 
            this.averagePeriodNumericUpDown.Location = new System.Drawing.Point(229, 48);
            this.averagePeriodNumericUpDown.Name = "averagePeriodNumericUpDown";
            this.averagePeriodNumericUpDown.Size = new System.Drawing.Size(120, 38);
            this.averagePeriodNumericUpDown.TabIndex = 4;
            // 
            // DoClassificationButton
            // 
            this.DoClassificationButton.Location = new System.Drawing.Point(10, 92);
            this.DoClassificationButton.Name = "DoClassificationButton";
            this.DoClassificationButton.Size = new System.Drawing.Size(339, 60);
            this.DoClassificationButton.TabIndex = 5;
            this.DoClassificationButton.Text = "Classify";
            this.DoClassificationButton.UseVisualStyleBackColor = true;
            // 
            // AdxInputsCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DoClassificationButton);
            this.Controls.Add(this.averagePeriodNumericUpDown);
            this.Controls.Add(this.dxPeriodNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AdxInputsCtl";
            this.Size = new System.Drawing.Size(360, 166);
            ((System.ComponentModel.ISupportInitialize)(this.dxPeriodNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagePeriodNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown dxPeriodNumericUpDown;
        private System.Windows.Forms.NumericUpDown averagePeriodNumericUpDown;
        private System.Windows.Forms.Button DoClassificationButton;
    }
}
