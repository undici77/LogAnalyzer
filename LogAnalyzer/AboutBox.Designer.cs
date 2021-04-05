namespace LogAnalyzer
{
	partial class AboutBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.OkButton = new System.Windows.Forms.Button();
            this.CompanyNameLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ProductNameLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.BoxDescriptionText = new System.Windows.Forms.RichTextBox();
            this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(409, 321);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(171, 32);
            this.OkButton.TabIndex = 24;
            this.OkButton.Text = "&OK";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CompanyNameLabel
            // 
            this.TableLayoutPanel.SetColumnSpan(this.CompanyNameLabel, 2);
            this.CompanyNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompanyNameLabel.Location = new System.Drawing.Point(200, 105);
            this.CompanyNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.CompanyNameLabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.CompanyNameLabel.Name = "CompanyNameLabel";
            this.CompanyNameLabel.Size = new System.Drawing.Size(380, 17);
            this.CompanyNameLabel.TabIndex = 22;
            this.CompanyNameLabel.Text = "Company Name";
            this.CompanyNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopyrightLabel
            // 
            this.TableLayoutPanel.SetColumnSpan(this.CopyrightLabel, 2);
            this.CopyrightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CopyrightLabel.Location = new System.Drawing.Point(200, 70);
            this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.CopyrightLabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(380, 17);
            this.CopyrightLabel.TabIndex = 21;
            this.CopyrightLabel.Text = "Copyright";
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VersionLabel
            // 
            this.TableLayoutPanel.SetColumnSpan(this.VersionLabel, 2);
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Location = new System.Drawing.Point(200, 35);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.VersionLabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(380, 17);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "Version";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductNameLabel
            // 
            this.TableLayoutPanel.SetColumnSpan(this.ProductNameLabel, 2);
            this.ProductNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductNameLabel.Location = new System.Drawing.Point(200, 0);
            this.ProductNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.ProductNameLabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.ProductNameLabel.Name = "ProductNameLabel";
            this.ProductNameLabel.Size = new System.Drawing.Size(380, 17);
            this.ProductNameLabel.TabIndex = 19;
            this.ProductNameLabel.Text = "Product Name";
            this.ProductNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel.ColumnCount = 3;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30303F));
            this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.ProductNameLabel, 1, 0);
            this.TableLayoutPanel.Controls.Add(this.VersionLabel, 1, 1);
            this.TableLayoutPanel.Controls.Add(this.CopyrightLabel, 1, 2);
            this.TableLayoutPanel.Controls.Add(this.CompanyNameLabel, 1, 3);
            this.TableLayoutPanel.Controls.Add(this.BoxDescriptionText, 0, 4);
            this.TableLayoutPanel.Controls.Add(this.OkButton, 2, 5);
            this.TableLayoutPanel.Controls.Add(this.AboutLinkLabel, 0, 5);
            this.TableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 6;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(583, 356);
            this.TableLayoutPanel.TabIndex = 0;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogoPictureBox.Image = global::LogAnalyzer.Properties.Resources.logo;
            this.LogoPictureBox.InitialImage = null;
            this.LogoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.TableLayoutPanel.SetRowSpan(this.LogoPictureBox, 4);
            this.LogoPictureBox.Size = new System.Drawing.Size(188, 134);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 12;
            this.LogoPictureBox.TabStop = false;
            // 
            // BoxDescriptionText
            // 
            this.TableLayoutPanel.SetColumnSpan(this.BoxDescriptionText, 3);
            this.BoxDescriptionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoxDescriptionText.Location = new System.Drawing.Point(6, 143);
            this.BoxDescriptionText.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.BoxDescriptionText.Name = "BoxDescriptionText";
            this.BoxDescriptionText.ReadOnly = true;
            this.BoxDescriptionText.Size = new System.Drawing.Size(574, 172);
            this.BoxDescriptionText.TabIndex = 23;
            this.BoxDescriptionText.TabStop = false;
            this.BoxDescriptionText.Text = resources.GetString("BoxDescriptionText.Text");
            // 
            // AboutLinkLabel
            // 
            this.AboutLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutLinkLabel.AutoSize = true;
            this.TableLayoutPanel.SetColumnSpan(this.AboutLinkLabel, 2);
            this.AboutLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutLinkLabel.Location = new System.Drawing.Point(3, 318);
            this.AboutLinkLabel.Name = "AboutLinkLabel";
            this.AboutLinkLabel.Size = new System.Drawing.Size(400, 38);
            this.AboutLinkLabel.TabIndex = 27;
            this.AboutLinkLabel.TabStop = true;
            this.AboutLinkLabel.Text = "https://github.com/undici77/LogAnalyzer";
            this.AboutLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AboutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLinkLabel_LinkClicked);
            // 
            // AboutBox
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 374);
            this.Controls.Add(this.TableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.TopMost = true;
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.RichTextBox BoxDescriptionText;
		private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
		private System.Windows.Forms.PictureBox LogoPictureBox;
		private System.Windows.Forms.Label ProductNameLabel;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.Label CopyrightLabel;
		private System.Windows.Forms.Label CompanyNameLabel;
		private System.Windows.Forms.LinkLabel AboutLinkLabel;
	}
}
