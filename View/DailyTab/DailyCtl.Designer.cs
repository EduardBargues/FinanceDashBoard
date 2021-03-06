﻿using View.Generic;

namespace View.DailyTab
{
    partial class DailyCtl
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.candlesDurationUpDown = new System.Windows.Forms.NumericUpDown();
            this.refreshButton = new System.Windows.Forms.Button();
            this.candlesVolumeUpDown = new System.Windows.Forms.NumericUpDown();
            this.candlesTicksUpDown = new System.Windows.Forms.NumericUpDown();
            this.candlesByVolumeRadioButton = new System.Windows.Forms.RadioButton();
            this.candlesByDurationRadioButton = new System.Windows.Forms.RadioButton();
            this.candlesByTicksRadioButton = new System.Windows.Forms.RadioButton();
            this.gridDays = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.periodIndicator = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.smoothingPeriod = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.seriesIndicatorCtl = new SeriesIndicatorCtl();
            this.strategiesCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlesDurationUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlesVolumeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlesTicksUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDays)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodIndicator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.candlesDurationUpDown);
            this.groupBox.Controls.Add(this.refreshButton);
            this.groupBox.Controls.Add(this.candlesVolumeUpDown);
            this.groupBox.Controls.Add(this.candlesTicksUpDown);
            this.groupBox.Controls.Add(this.candlesByVolumeRadioButton);
            this.groupBox.Controls.Add(this.candlesByDurationRadioButton);
            this.groupBox.Controls.Add(this.candlesByTicksRadioButton);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(520, 179);
            this.groupBox.TabIndex = 5;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Compute Candles By...";
            // 
            // candlesDurationUpDown
            // 
            this.candlesDurationUpDown.Location = new System.Drawing.Point(186, 35);
            this.candlesDurationUpDown.Name = "candlesDurationUpDown";
            this.candlesDurationUpDown.Size = new System.Drawing.Size(120, 38);
            this.candlesDurationUpDown.TabIndex = 8;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(312, 35);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(202, 127);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "REFRESH";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // candlesVolumeUpDown
            // 
            this.candlesVolumeUpDown.Location = new System.Drawing.Point(186, 124);
            this.candlesVolumeUpDown.Name = "candlesVolumeUpDown";
            this.candlesVolumeUpDown.Size = new System.Drawing.Size(120, 38);
            this.candlesVolumeUpDown.TabIndex = 7;
            // 
            // candlesTicksUpDown
            // 
            this.candlesTicksUpDown.Location = new System.Drawing.Point(186, 80);
            this.candlesTicksUpDown.Name = "candlesTicksUpDown";
            this.candlesTicksUpDown.Size = new System.Drawing.Size(120, 38);
            this.candlesTicksUpDown.TabIndex = 6;
            // 
            // candlesByVolumeRadioButton
            // 
            this.candlesByVolumeRadioButton.AutoSize = true;
            this.candlesByVolumeRadioButton.Location = new System.Drawing.Point(6, 121);
            this.candlesByVolumeRadioButton.Name = "candlesByVolumeRadioButton";
            this.candlesByVolumeRadioButton.Size = new System.Drawing.Size(149, 36);
            this.candlesByVolumeRadioButton.TabIndex = 5;
            this.candlesByVolumeRadioButton.TabStop = true;
            this.candlesByVolumeRadioButton.Text = "Volume";
            this.candlesByVolumeRadioButton.UseVisualStyleBackColor = true;
            // 
            // candlesByDurationRadioButton
            // 
            this.candlesByDurationRadioButton.AutoSize = true;
            this.candlesByDurationRadioButton.Location = new System.Drawing.Point(6, 37);
            this.candlesByDurationRadioButton.Name = "candlesByDurationRadioButton";
            this.candlesByDurationRadioButton.Size = new System.Drawing.Size(160, 36);
            this.candlesByDurationRadioButton.TabIndex = 3;
            this.candlesByDurationRadioButton.TabStop = true;
            this.candlesByDurationRadioButton.Text = "Duration";
            this.candlesByDurationRadioButton.UseVisualStyleBackColor = true;
            // 
            // candlesByTicksRadioButton
            // 
            this.candlesByTicksRadioButton.AutoSize = true;
            this.candlesByTicksRadioButton.Location = new System.Drawing.Point(6, 79);
            this.candlesByTicksRadioButton.Name = "candlesByTicksRadioButton";
            this.candlesByTicksRadioButton.Size = new System.Drawing.Size(118, 36);
            this.candlesByTicksRadioButton.TabIndex = 4;
            this.candlesByTicksRadioButton.TabStop = true;
            this.candlesByTicksRadioButton.Text = "Ticks";
            this.candlesByTicksRadioButton.UseVisualStyleBackColor = true;
            // 
            // gridDays
            // 
            this.gridDays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDays.Location = new System.Drawing.Point(0, 358);
            this.gridDays.Name = "gridDays";
            this.gridDays.RowTemplate.Height = 40;
            this.gridDays.Size = new System.Drawing.Size(523, 838);
            this.gridDays.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.strategiesCombobox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox);
            this.panel1.Controls.Add(this.gridDays);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 1196);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.periodIndicator);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.smoothingPeriod);
            this.panel2.Location = new System.Drawing.Point(0, 188);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 109);
            this.panel2.TabIndex = 6;
            // 
            // periodIndicator
            // 
            this.periodIndicator.Location = new System.Drawing.Point(259, 12);
            this.periodIndicator.Name = "periodIndicator";
            this.periodIndicator.Size = new System.Drawing.Size(120, 38);
            this.periodIndicator.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Smoothing Period:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Period:";
            // 
            // smoothingPeriod
            // 
            this.smoothingPeriod.Location = new System.Drawing.Point(259, 56);
            this.smoothingPeriod.Name = "smoothingPeriod";
            this.smoothingPeriod.Size = new System.Drawing.Size(120, 38);
            this.smoothingPeriod.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(535, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.seriesIndicatorCtl);
            this.splitContainer1.Size = new System.Drawing.Size(1453, 1190);
            this.splitContainer1.SplitterDistance = 594;
            this.splitContainer1.TabIndex = 11;
            // 
            // seriesIndicatorCtl
            // 
            this.seriesIndicatorCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seriesIndicatorCtl.Location = new System.Drawing.Point(0, 0);
            this.seriesIndicatorCtl.Name = "seriesIndicatorCtl";
            this.seriesIndicatorCtl.Size = new System.Drawing.Size(1453, 594);
            this.seriesIndicatorCtl.TabIndex = 9;
            // 
            // strategiesCombobox
            // 
            this.strategiesCombobox.FormattingEnabled = true;
            this.strategiesCombobox.Location = new System.Drawing.Point(161, 304);
            this.strategiesCombobox.Name = "strategiesCombobox";
            this.strategiesCombobox.Size = new System.Drawing.Size(362, 39);
            this.strategiesCombobox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "Strategies:";
            // 
            // DailyCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "DailyCtl";
            this.Size = new System.Drawing.Size(1991, 1196);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlesDurationUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlesVolumeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlesTicksUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDays)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodIndicator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingPeriod)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gridDays;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.RadioButton candlesByTicksRadioButton;
        private System.Windows.Forms.RadioButton candlesByDurationRadioButton;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton candlesByVolumeRadioButton;
        private System.Windows.Forms.NumericUpDown candlesDurationUpDown;
        private System.Windows.Forms.NumericUpDown candlesVolumeUpDown;
        private System.Windows.Forms.NumericUpDown candlesTicksUpDown;
        private System.Windows.Forms.Panel panel1;
        private SeriesIndicatorCtl seriesIndicatorCtl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown periodIndicator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown smoothingPeriod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox strategiesCombobox;
    }
}
