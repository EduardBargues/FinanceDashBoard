namespace View
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
            this.selectionMethodInputCtl1 = new SelectionMethodInputCtl();
            this.SuspendLayout();
            // 
            // selectionMethodInputCtl1
            // 
            this.selectionMethodInputCtl1.Location = new System.Drawing.Point(3, 3);
            this.selectionMethodInputCtl1.Name = "selectionMethodInputCtl1";
            this.selectionMethodInputCtl1.Size = new System.Drawing.Size(936, 271);
            this.selectionMethodInputCtl1.TabIndex = 0;
            // 
            // ClassificationDayCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectionMethodInputCtl1);
            this.Name = "ClassificationDayCtl";
            this.Size = new System.Drawing.Size(2485, 1213);
            this.ResumeLayout(false);

        }

        #endregion

        private SelectionMethodInputCtl selectionMethodInputCtl1;
    }
}
