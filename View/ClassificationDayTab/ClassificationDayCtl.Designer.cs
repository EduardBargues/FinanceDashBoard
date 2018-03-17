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
            ((System.ComponentModel.ISupportInitialize)(this.classificationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dailyClassificationMethodInputCtl
            // 
            this.dailyClassificationMethodInputCtl.Location = new System.Drawing.Point(3, 3);
            this.dailyClassificationMethodInputCtl.Name = "dailyClassificationMethodInputCtl";
            this.dailyClassificationMethodInputCtl.Size = new System.Drawing.Size(708, 271);
            this.dailyClassificationMethodInputCtl.TabIndex = 0;
            // 
            // classificationGrid
            // 
            this.classificationGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.classificationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classificationGrid.Location = new System.Drawing.Point(3, 280);
            this.classificationGrid.Name = "classificationGrid";
            this.classificationGrid.RowTemplate.Height = 40;
            this.classificationGrid.Size = new System.Drawing.Size(708, 866);
            this.classificationGrid.TabIndex = 1;
            // 
            // ClassificationDayCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.classificationGrid);
            this.Controls.Add(this.dailyClassificationMethodInputCtl);
            this.Name = "ClassificationDayCtl";
            this.Size = new System.Drawing.Size(2535, 1149);
            ((System.ComponentModel.ISupportInitialize)(this.classificationGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SelectionDailyClassificationMethodInputCtl dailyClassificationMethodInputCtl;
        private System.Windows.Forms.DataGridView classificationGrid;
    }
}
