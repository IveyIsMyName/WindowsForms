﻿namespace Clock
{
	partial class ChooseFontForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseFontForm));
			this.cbFonts = new System.Windows.Forms.ComboBox();
			this.nudFontSize = new System.Windows.Forms.NumericUpDown();
			this.labelChooseFont = new System.Windows.Forms.Label();
			this.labelFontSize = new System.Windows.Forms.Label();
			this.labelExample = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
			this.SuspendLayout();
			// 
			// cbFonts
			// 
			this.cbFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFonts.FormattingEnabled = true;
			this.cbFonts.Location = new System.Drawing.Point(11, 25);
			this.cbFonts.Name = "cbFonts";
			this.cbFonts.Size = new System.Drawing.Size(345, 24);
			this.cbFonts.TabIndex = 0;
			this.cbFonts.SelectedIndexChanged += new System.EventHandler(this.cbFonts_SelectedIndexChanged);
			// 
			// nudFontSize
			// 
			this.nudFontSize.Location = new System.Drawing.Point(362, 27);
			this.nudFontSize.Name = "nudFontSize";
			this.nudFontSize.Size = new System.Drawing.Size(99, 22);
			this.nudFontSize.TabIndex = 1;
			this.nudFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudFontSize.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
			// 
			// labelChooseFont
			// 
			this.labelChooseFont.AutoSize = true;
			this.labelChooseFont.Location = new System.Drawing.Point(11, 6);
			this.labelChooseFont.Name = "labelChooseFont";
			this.labelChooseFont.Size = new System.Drawing.Size(81, 16);
			this.labelChooseFont.TabIndex = 2;
			this.labelChooseFont.Text = "Choose font:";
			// 
			// labelFontSize
			// 
			this.labelFontSize.AutoSize = true;
			this.labelFontSize.Location = new System.Drawing.Point(382, 6);
			this.labelFontSize.Name = "labelFontSize";
			this.labelFontSize.Size = new System.Drawing.Size(63, 16);
			this.labelFontSize.TabIndex = 3;
			this.labelFontSize.Text = "Font size:";
			// 
			// labelExample
			// 
			this.labelExample.AutoSize = true;
			this.labelExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelExample.Location = new System.Drawing.Point(11, 75);
			this.labelExample.Name = "labelExample";
			this.labelExample.Size = new System.Drawing.Size(176, 46);
			this.labelExample.TabIndex = 4;
			this.labelExample.Text = "Example";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(304, 170);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(385, 170);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnApply
			// 
			this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnApply.Location = new System.Drawing.Point(362, 55);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(99, 25);
			this.btnApply.TabIndex = 7;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// ChooseFontForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(473, 205);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.labelExample);
			this.Controls.Add(this.labelFontSize);
			this.Controls.Add(this.labelChooseFont);
			this.Controls.Add(this.nudFontSize);
			this.Controls.Add(this.cbFonts);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ChooseFontForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ChooseFont";
			((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbFonts;
		private System.Windows.Forms.NumericUpDown nudFontSize;
		private System.Windows.Forms.Label labelChooseFont;
		private System.Windows.Forms.Label labelFontSize;
		private System.Windows.Forms.Label labelExample;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnApply;
	}
}