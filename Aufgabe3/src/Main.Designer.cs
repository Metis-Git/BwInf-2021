
namespace bwinfTest1
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
			this.txtlbl = new System.Windows.Forms.Label();
			this.openFile = new System.Windows.Forms.LinkLabel();
			this.filename = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.startLvl1 = new System.Windows.Forms.Button();
			this.startLvl2 = new System.Windows.Forms.Button();
			this.startLvl3 = new System.Windows.Forms.Button();
			this.lbltxtInfo = new System.Windows.Forms.Label();
			this.copy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtlbl
			// 
			this.txtlbl.AutoSize = true;
			this.txtlbl.Location = new System.Drawing.Point(12, 27);
			this.txtlbl.Name = "txtlbl";
			this.txtlbl.Size = new System.Drawing.Size(108, 15);
			this.txtlbl.TabIndex = 1;
			this.txtlbl.Text = "Ausgewählte Datei:";
			// 
			// openFile
			// 
			this.openFile.AutoSize = true;
			this.openFile.Location = new System.Drawing.Point(230, 27);
			this.openFile.Name = "openFile";
			this.openFile.Size = new System.Drawing.Size(65, 15);
			this.openFile.TabIndex = 2;
			this.openFile.TabStop = true;
			this.openFile.Text = "Auswählen";
			this.openFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openFile_Click);
			// 
			// filename
			// 
			this.filename.AutoSize = true;
			this.filename.Location = new System.Drawing.Point(126, 27);
			this.filename.Name = "filename";
			this.filename.Size = new System.Drawing.Size(98, 15);
			this.filename.TabIndex = 3;
			this.filename.Text = "keine ausgewählt";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(310, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// startLvl1
			// 
			this.startLvl1.Location = new System.Drawing.Point(12, 64);
			this.startLvl1.Name = "startLvl1";
			this.startLvl1.Size = new System.Drawing.Size(212, 23);
			this.startLvl1.TabIndex = 6;
			this.startLvl1.Text = "Schwierigkeitsgrad 1 generieren";
			this.startLvl1.UseVisualStyleBackColor = true;
			this.startLvl1.Visible = false;
			this.startLvl1.Click += new System.EventHandler(this.startLvl1_Click);
			// 
			// startLvl2
			// 
			this.startLvl2.Location = new System.Drawing.Point(12, 93);
			this.startLvl2.Name = "startLvl2";
			this.startLvl2.Size = new System.Drawing.Size(212, 23);
			this.startLvl2.TabIndex = 7;
			this.startLvl2.Text = "Schwierigkeitsgrad 2 generieren";
			this.startLvl2.UseVisualStyleBackColor = true;
			this.startLvl2.Visible = false;
			this.startLvl2.Click += new System.EventHandler(this.startLvl2_Click);
			// 
			// startLvl3
			// 
			this.startLvl3.Location = new System.Drawing.Point(12, 122);
			this.startLvl3.Name = "startLvl3";
			this.startLvl3.Size = new System.Drawing.Size(212, 23);
			this.startLvl3.TabIndex = 8;
			this.startLvl3.Text = "Schwierigkeitsgrad 3 generieren";
			this.startLvl3.UseVisualStyleBackColor = true;
			this.startLvl3.Visible = false;
			this.startLvl3.Click += new System.EventHandler(this.startLvl3_Click);
			// 
			// lbltxtInfo
			// 
			this.lbltxtInfo.AutoSize = true;
			this.lbltxtInfo.Location = new System.Drawing.Point(12, 9);
			this.lbltxtInfo.Name = "lbltxtInfo";
			this.lbltxtInfo.Size = new System.Drawing.Size(275, 15);
			this.lbltxtInfo.TabIndex = 9;
			this.lbltxtInfo.Text = "Team-ID: 00564, Team-Name: \"SRZ info3 Gruppe 1\"";
			// 
			// copy
			// 
			this.copy.Location = new System.Drawing.Point(12, 151);
			this.copy.Name = "copy";
			this.copy.Size = new System.Drawing.Size(212, 23);
			this.copy.TabIndex = 10;
			this.copy.Text = "Kopieren";
			this.copy.UseVisualStyleBackColor = true;
			this.copy.Visible = false;
			this.copy.Click += new System.EventHandler(this.copy_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.copy);
			this.Controls.Add(this.lbltxtInfo);
			this.Controls.Add(this.startLvl3);
			this.Controls.Add(this.startLvl2);
			this.Controls.Add(this.startLvl1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.filename);
			this.Controls.Add(this.openFile);
			this.Controls.Add(this.txtlbl);
			this.Name = "Main";
			this.Text = "BwInf 2021 Aufgabe 3: \"Wortsuche\"";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label txtlbl;
        private System.Windows.Forms.LinkLabel openFile;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startLvl1;
        private System.Windows.Forms.Button startLvl2;
        private System.Windows.Forms.Button startLvl3;
		private System.Windows.Forms.Label lbltxtInfo;
		private System.Windows.Forms.Button copy;
	}
}

