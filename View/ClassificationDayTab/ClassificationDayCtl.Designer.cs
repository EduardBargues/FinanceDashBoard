namespace View.ClassificationDayTab
{
    partial class ClassificationDayCtl
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
            this.dailyClassificationMethodInputCtl = new View.ClassificationDayTab.SelectionDailyClassificationMethodInputCtl();
            this.classificationGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.startDateCtl = new System.Windows.Forms.DateTimePicker();
            this.endDayCtl = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.seriesIndicatorCtl = new View.Generic.SeriesIndicatorCtl();
            ((System.ComponentModel.ISupportInitialize)(this.classificationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dailyClassificationMethodInputCtl
            // 
            this.dailyClassificationMethodInputCtl.Location = new System.Drawing.Point(3, 3);
            this.dailyClassificationMethodInputCtl.Name = "dailyClassificationMethodInputCtl";
            this.dailyClassificationMethodInputCtl.Size = new System.Drawing.Size(900, 271);
            this.dailyClassificationMethodInputCtl.TabIndex = 0;
            // 
            // classificationGrid
            // 
            this.classificationGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.classificationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classificationGrid.Location = new System.Drawing.Point(3, 369);
            this.classificationGrid.Name = "classificationGrid";
            this.classificationGrid.RowTemplate.Height = 40;
            this.classificationGrid.Size = new System.Drawing.Size(900, 331);
            this.classificationGrid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "From:";
            // 
            // startDateCtl
            // 
            this.startDateCtl.Location = new System.Drawing.Point(99, 281);
            this.startDateCtl.Name = "startDateCtl";
            this.startDateCtl.Size = new System.Drawing.Size(804, 38);
            this.startDateCtl.TabIndex = 3;
            // 
            // endDayCtl
            // 
            this.endDayCtl.Location = new System.Drawing.Point(99, 325);
            this.endDayCtl.Name = "endDayCtl";
            this.endDayCtl.Size = new System.Drawing.Size(804, 38);
            this.endDayCtl.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "To:";
            // 
            // seriesIndicatorCtl
            // 
            this.seriesIndicatorCtl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seriesIndicatorCtl.Location = new System.Drawing.Point(909, 3);
            this.seriesIndicatorCtl.Name = "seriesIndicatorCtl";
            this.seriesIndicatorCtl.Size = new System.Drawing.Size(970, 697);
            this.seriesIndicatorCtl.TabIndex = 6;
            // 
            // ClassificationDayCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.seriesIndicatorCtl);
            this.Controls.Add(this.endDayCtl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startDateCtl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.classificationGrid);
            this.Controls.Add(this.dailyClassificationMethodInputCtl);
            this.Name = "ClassificationDayCtl";
            this.Size = new System.Drawing.Size(1882, 703);
            ((System.ComponentModel.ISupportInitialize)(this.classificationGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SelectionDailyClassificationMethodInputCtl dailyClassificationMethodInputCtl;
        private System.Windows.Forms.DataGridView classificationGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startDateCtl;
        private System.Windows.Forms.DateTimePicker endDayCtl;
        private System.Windows.Forms.Label label2;
        private Generic.SeriesIndicatorCtl seriesIndicatorCtl;
    }
}
