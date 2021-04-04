/*
 * This file is part of the LogAnalyzer distribution (https://github.com/undici77/PlugnPutty.git).
 * Copyright (c) 2021 Alessandro Barbieri.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

namespace LogAnalyzer
{
	partial class InputForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
			this.MessageLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancButton = new System.Windows.Forms.Button();
			this.InputTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			//
			// MessageLabel
			//
			this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.MessageLabel.Location = new System.Drawing.Point(12, 9);
			this.MessageLabel.Name = "MessageLabel";
			this.MessageLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.MessageLabel.Size = new System.Drawing.Size(398, 57);
			this.MessageLabel.TabIndex = 0;
			this.MessageLabel.Text = "Message";
			this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			// OkButton
			//
			this.OkButton.Location = new System.Drawing.Point(12, 93);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			this.OkButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Buttons_KeyDown);
			//
			// CancButton
			//
			this.CancButton.Location = new System.Drawing.Point(93, 93);
			this.CancButton.Name = "CancButton";
			this.CancButton.Size = new System.Drawing.Size(75, 23);
			this.CancButton.TabIndex = 2;
			this.CancButton.Text = "Cancel";
			this.CancButton.UseVisualStyleBackColor = true;
			this.CancButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.CancButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Buttons_KeyDown);
			//
			// InputTextBox
			//
			this.InputTextBox.Location = new System.Drawing.Point(12, 70);
			this.InputTextBox.Name = "InputTextBox";
			this.InputTextBox.Size = new System.Drawing.Size(398, 20);
			this.InputTextBox.TabIndex = 3;
			this.InputTextBox.TextChanged += new System.EventHandler(this.InputTextBox_TextChanged);
			this.InputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputTextBox_KeyDown);
			//
			// InputForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 121);
			this.Controls.Add(this.InputTextBox);
			this.Controls.Add(this.CancButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.MessageLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(440, 160);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(440, 160);
			this.Name = "InputForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputForm_FormClosing);
			this.Load += new System.EventHandler(this.InputForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label MessageLabel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancButton;
		private System.Windows.Forms.TextBox InputTextBox;
	}
}