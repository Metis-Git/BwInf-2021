
namespace Task1
{
	partial class Main
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.help = new System.Windows.Forms.Button();
			this.txtlbl = new System.Windows.Forms.Label();
			this.filename = new System.Windows.Forms.Label();
			this.openFile = new System.Windows.Forms.LinkLabel();
			this.output = new System.Windows.Forms.TextBox();
			this.start = new System.Windows.Forms.LinkLabel();
			this.lbltxtInfo = new System.Windows.Forms.Label();
			this.info = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// help
			// 
			this.help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.help.BackColor = System.Drawing.Color.Transparent;
			this.help.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("help.BackgroundImage")));
			this.help.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.help.Cursor = System.Windows.Forms.Cursors.Hand;
			this.help.FlatAppearance.BorderSize = 0;
			this.help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.help.ForeColor = System.Drawing.Color.Transparent;
			this.help.Location = new System.Drawing.Point(699, 1);
			this.help.Margin = new System.Windows.Forms.Padding(1);
			this.help.Name = "help";
			this.help.Size = new System.Drawing.Size(48, 46);
			this.help.TabIndex = 2;
			this.help.TabStop = false;
			this.help.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.help.UseMnemonic = false;
			this.help.UseVisualStyleBackColor = false;
			this.help.Click += new System.EventHandler(this.help_Click);
			// 
			// txtlbl
			// 
			this.txtlbl.AutoSize = true;
			this.txtlbl.Location = new System.Drawing.Point(12, 32);
			this.txtlbl.Name = "txtlbl";
			this.txtlbl.Size = new System.Drawing.Size(108, 15);
			this.txtlbl.TabIndex = 3;
			this.txtlbl.Text = "Ausgewählte Datei:";
			// 
			// filename
			// 
			this.filename.AutoSize = true;
			this.filename.Location = new System.Drawing.Point(120, 32);
			this.filename.Name = "filename";
			this.filename.Size = new System.Drawing.Size(100, 15);
			this.filename.TabIndex = 4;
			this.filename.Text = "keine Ausgewählt";
			// 
			// openFile
			// 
			this.openFile.AutoSize = true;
			this.openFile.Location = new System.Drawing.Point(220, 32);
			this.openFile.Name = "openFile";
			this.openFile.Size = new System.Drawing.Size(65, 15);
			this.openFile.TabIndex = 5;
			this.openFile.TabStop = true;
			this.openFile.Text = "Auswählen";
			this.openFile.Click += new System.EventHandler(this.openFile_Click);
			// 
			// output
			// 
			this.output.AcceptsReturn = true;
			this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.output.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.output.HideSelection = false;
			this.output.Location = new System.Drawing.Point(12, 288);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.PlaceholderText = "Ausgabe";
			this.output.ReadOnly = true;
			this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.output.Size = new System.Drawing.Size(776, 158);
			this.output.TabIndex = 6;
			this.output.TabStop = false;
			this.output.WordWrap = false;
			// 
			// start
			// 
			this.start.AutoSize = true;
			this.start.Location = new System.Drawing.Point(288, 32);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(44, 15);
			this.start.TabIndex = 7;
			this.start.TabStop = true;
			this.start.Text = "Starten";
			this.start.Visible = false;
			this.start.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.start_LinkClicked);
			// 
			// lbltxtInfo
			// 
			this.lbltxtInfo.AutoSize = true;
			this.lbltxtInfo.Location = new System.Drawing.Point(12, 9);
			this.lbltxtInfo.Name = "lbltxtInfo";
			this.lbltxtInfo.Size = new System.Drawing.Size(196, 15);
			this.lbltxtInfo.TabIndex = 8;
			this.lbltxtInfo.Text = "Team-ID: TODO, Team-Name: TODO";
			// 
			// info
			// 
			this.info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.info.BackColor = System.Drawing.Color.Transparent;
			this.info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("info.BackgroundImage")));
			this.info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.info.FlatAppearance.BorderSize = 0;
			this.info.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.info.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.info.ForeColor = System.Drawing.Color.Transparent;
			this.info.Location = new System.Drawing.Point(744, 1);
			this.info.Margin = new System.Windows.Forms.Padding(1);
			this.info.Name = "info";
			this.info.Size = new System.Drawing.Size(48, 46);
			this.info.TabIndex = 9;
			this.info.TabStop = false;
			this.info.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.info.UseMnemonic = false;
			this.info.UseVisualStyleBackColor = false;
			this.info.Click += new System.EventHandler(this.info_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.info);
			this.Controls.Add(this.lbltxtInfo);
			this.Controls.Add(this.start);
			this.Controls.Add(this.output);
			this.Controls.Add(this.openFile);
			this.Controls.Add(this.filename);
			this.Controls.Add(this.txtlbl);
			this.Controls.Add(this.help);
			this.MinimumSize = new System.Drawing.Size(816, 489);
			this.Name = "Main";
			this.Text = "BWINF 2021 - Aufgabe 1: \"Schiebeparkplatz\"";
			this.TransparencyKey = System.Drawing.SystemColors.ControlLight;
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button help;
		private System.Windows.Forms.Label txtlbl;
		private System.Windows.Forms.Label filename;
		private System.Windows.Forms.LinkLabel openFile;
		private System.Windows.Forms.TextBox output;
		private System.Windows.Forms.LinkLabel start;
		private System.Windows.Forms.Label lbltxtInfo;
		private System.Windows.Forms.Button info;
	}
}

