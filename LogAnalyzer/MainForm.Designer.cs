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
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterTextBoxMenuItem = new ToolStripTextBoxEx();
            this.TextRegexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FollowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterPatternMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainStatusStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.LogListView = new System.Windows.Forms.ListViewEx();
            this.LogContentText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilterPatternListView = new System.Windows.Forms.ListView();
            this.PatternText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainFormMenuStrip.AutoSize = false;
            this.MainFormMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainFormMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.ExportMenuItem,
            this.FilterTextBoxMenuItem,
            this.TextRegexMenuItem,
            this.FollowMenuItem,
            this.FilterPatternMenuItem});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.ShowItemToolTips = true;
            this.MainFormMenuStrip.Size = new System.Drawing.Size(584, 42);
            this.MainFormMenuStrip.TabIndex = 0;
            this.MainFormMenuStrip.Text = "Menu";
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.AutoSize = false;
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(36, 36);
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // ExportMenuItem
            // 
            this.ExportMenuItem.AutoSize = false;
            this.ExportMenuItem.Name = "ExportMenuItem";
            this.ExportMenuItem.Size = new System.Drawing.Size(36, 36);
            this.ExportMenuItem.Click += new System.EventHandler(this.ExportMenuItem_Click);
            // 
            // FilterTextBoxMenuItem
            // 
            this.FilterTextBoxMenuItem.CueText = "";
            this.FilterTextBoxMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterTextBoxMenuItem.Name = "FilterTextBoxMenuItem";
            this.FilterTextBoxMenuItem.ShowCueTextWithFocus = false;
            this.FilterTextBoxMenuItem.Size = new System.Drawing.Size(264, 38);
            this.FilterTextBoxMenuItem.TextChanged += new System.EventHandler(this.FilterTextBoxMenuItem_TextChanged);
            // 
            // TextRegexMenuItem
            // 
            this.TextRegexMenuItem.AutoSize = false;
            this.TextRegexMenuItem.Name = "TextRegexMenuItem";
            this.TextRegexMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.TextRegexMenuItem.Size = new System.Drawing.Size(36, 36);
            this.TextRegexMenuItem.Click += new System.EventHandler(this.TextRegexMenuItem_Click);
            // 
            // FollowMenuItem
            // 
            this.FollowMenuItem.AutoSize = false;
            this.FollowMenuItem.Name = "FollowMenuItem";
            this.FollowMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.FollowMenuItem.Size = new System.Drawing.Size(36, 36);
            this.FollowMenuItem.Click += new System.EventHandler(this.FollowMenuItem_Click);
            // 
            // FilterPatternMenuItem
            // 
            this.FilterPatternMenuItem.AutoSize = false;
            this.FilterPatternMenuItem.Name = "FilterPatternMenuItem";
            this.FilterPatternMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.FilterPatternMenuItem.Size = new System.Drawing.Size(36, 36);
            this.FilterPatternMenuItem.Click += new System.EventHandler(this.FilterPatternMenuItem_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainStatusStrip.AutoSize = false;
            this.MainStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStatusStripLabel,
            this.MainStatusStripProgressBar});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 339);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(584, 22);
            this.MainStatusStrip.TabIndex = 4;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // MainStatusStripLabel
            // 
            this.MainStatusStripLabel.AutoSize = false;
            this.MainStatusStripLabel.Name = "MainStatusStripLabel";
            this.MainStatusStripLabel.Size = new System.Drawing.Size(100, 17);
            this.MainStatusStripLabel.Text = "MainStatusStripLabel";
            this.MainStatusStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainStatusStripProgressBar
            // 
            this.MainStatusStripProgressBar.AutoSize = false;
            this.MainStatusStripProgressBar.Name = "MainStatusStripProgressBar";
            this.MainStatusStripProgressBar.Size = new System.Drawing.Size(260, 16);
            this.MainStatusStripProgressBar.Visible = false;
            // 
            // LogListView
            // 
            this.LogListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogListView.AutoArrange = false;
            this.LogListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LogContentText});
            this.LogListView.FullRowSelect = true;
            this.LogListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.LogListView.HideSelection = false;
            this.LogListView.Location = new System.Drawing.Point(0, 3);
            this.LogListView.Margin = new System.Windows.Forms.Padding(0);
            this.LogListView.Name = "LogListView";
            this.LogListView.Size = new System.Drawing.Size(403, 283);
            this.LogListView.TabIndex = 3;
            this.LogListView.UseCompatibleStateImageBehavior = false;
            this.LogListView.View = System.Windows.Forms.View.Details;
            this.LogListView.VirtualMode = true;
            this.LogListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LogListView_RetrieveVirtualItem);
            this.LogListView.SelectedIndexChanged += new System.EventHandler(this.LogListView_SelectedIndexChanged);
            this.LogListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.LogListView_DragDrop);
            this.LogListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.LogListView_DragEnter);
            this.LogListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogListView_KeyDown);
            this.LogListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LogListView_MouseClick);
            // 
            // LogContentText
            // 
            this.LogContentText.Width = 0;
            // 
            // FilterPatternListView
            // 
            this.FilterPatternListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPatternListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PatternText});
            this.FilterPatternListView.FullRowSelect = true;
            this.FilterPatternListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.FilterPatternListView.HideSelection = false;
            this.FilterPatternListView.Location = new System.Drawing.Point(3, 3);
            this.FilterPatternListView.MultiSelect = false;
            this.FilterPatternListView.Name = "FilterPatternListView";
            this.FilterPatternListView.Size = new System.Drawing.Size(147, 283);
            this.FilterPatternListView.TabIndex = 5;
            this.FilterPatternListView.UseCompatibleStateImageBehavior = false;
            this.FilterPatternListView.View = System.Windows.Forms.View.Details;
            this.FilterPatternListView.DoubleClick += new System.EventHandler(this.PatternListView_DoubleClick);
            // 
            // PatternText
            // 
            this.PatternText.Width = 25;
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplitContainer.Location = new System.Drawing.Point(12, 45);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.LogListView);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.FilterPatternListView);
            this.MainSplitContainer.Size = new System.Drawing.Size(560, 291);
            this.MainSplitContainer.SplitterDistance = 403;
            this.MainSplitContainer.TabIndex = 6;
            this.MainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainSplitContainer_SplitterMoved);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainFormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.MaximumSizeChanged += new System.EventHandler(this.MainForm_MaximumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainFormMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExportMenuItem;
		private System.Windows.Forms.ListViewEx LogListView;
		private System.Windows.Forms.ColumnHeader LogContentText;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel MainStatusStripLabel;
		private System.Windows.Forms.ToolStripProgressBar MainStatusStripProgressBar;
		private ToolStripTextBoxEx FilterTextBoxMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TextRegexMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FollowMenuItem;
		private System.Windows.Forms.ListView FilterPatternListView;
		private System.Windows.Forms.ColumnHeader PatternText;
		private System.Windows.Forms.ToolStripMenuItem FilterPatternMenuItem;
		private System.Windows.Forms.SplitContainer MainSplitContainer;
    }
}

