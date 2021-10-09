using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Task1
{

	enum CarPart {Null, X1, X2};

	public partial class Main : Form
	{

		private string path = "";
		private string name = "keine Ausgewählt";

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{

		}

		private void Output(string text, bool splitter = true)
		{
			string[] s = text.Split('\n');
			if (splitter)
			{
				Array.Resize(ref s, s.Length + 1);
				s[^1] = "----------------------------------------------------------------------------------------------------";
			}

			int s_size = s.Length;
			int o_size = this.output.Lines.Length;

			string[] t = new string[o_size + s_size];

			Array.Copy(s, 0, t, 0, s.Length);
			Array.Copy(this.output.Lines, 0, t, s.Length, o_size);

			this.output.Lines = t;
		}

		private void openFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog {
				InitialDirectory = default,
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 2,
				RestoreDirectory = true
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				path = dialog.FileName;
				name = path[(path.LastIndexOf('\\') + 1)..];

				this.filename.Text = name;
				this.openFile.Text = "Ändern";
				this.openFile.Location = new Point(this.filename.Location.X + this.filename.Size.Width, this.filename.Location.Y);
				this.start.Visible = true;
				this.start.Location = new Point(this.openFile.Location.X + this.openFile.Size.Width, this.openFile.Location.Y);

				this.Output("file selected:\n-path: \"" + path + "\"\n-file name: \"" + name + "\"");
			}

		}

		private void start_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!File.Exists(this.path)) { MessageBox.Show("Datei exsitiert nicht mehr!"); return; }

			string[] content = new string[0];

			StreamReader file = new StreamReader(this.path);
			string line = file.ReadLine();

			while(line != null)
			{
				Array.Resize(ref content, content.Length + 1);
				content[^1] = line;
				line = file.ReadLine();
			}

			file.Close();

			Output("");

			try
			{

				int nCars = (content[0].ToUpper())[2] - (content[0].ToUpper())[0] + 1;
				Tuple<char, CarPart>[] parkingSpot = new Tuple<char, CarPart>[nCars];

				for (int i = 0; i < nCars; i++) parkingSpot[i] = new Tuple<char, CarPart>((char)0, CarPart.Null);
				int max = Convert.ToInt32(content[1]);

				for (int i = 0; i < max; i++)
				{
					int pos = Convert.ToInt32(content[2 + i][2..]);
					char car = (content[2 + i].ToUpper())[0];
					parkingSpot[pos] = new Tuple<char, CarPart>(car, CarPart.X1);       // vorne	-> X1
					parkingSpot[pos + 1] = new Tuple<char, CarPart>(car, CarPart.X2);   // hinten	-> X2
				}

				for (int i = parkingSpot.Length - 1; i >= 0; i--)
				{
					if (parkingSpot[i].Item1 == 0)
					{
						Output(((char)(i + 65)) + ": ", false);
					}
					else
					{
						bool direktion = false; // true => left, false => rigth
						bool x1 = parkingSpot[i].Item2 == CarPart.X1;
						bool null_pos_r = false, null_pos_l = false;
						int break_pos = 0;
						int sec_break_pos_r = 0;
						int sec_break_pos_l = 0;

						for (int t = i; t < parkingSpot.Length; t++)
						{
							if (parkingSpot[t].Item1 == 0)
							{
								if (x1 || null_pos_r) { break_pos = t; break; };
								null_pos_r = true;
								sec_break_pos_r = t;
							}
						}

						for (int t = i, x = i; t >= 0; t--, x++)
						{
							if (parkingSpot[t].Item1 == 0)
							{
								if (!x1 || null_pos_l)
								{
									if (break_pos == 0 || break_pos > (x1 ? x : x - 1))
									{
										break_pos = t;
										sec_break_pos_r = sec_break_pos_l;
										direktion = true;
									}
									break;
								}
								null_pos_l = true;
								sec_break_pos_l = t;
							}
							else if (break_pos != 0 && x > break_pos + 1) break;
						}

						int add = direktion ? 1 : -1;
						string direktion_s = direktion ? "left" : "rigth";
						string output = ((char)(i + 65)) + ": ";
						Tuple<char, CarPart>[] tempPS = new Tuple<char, CarPart>[parkingSpot.Length];
						parkingSpot.CopyTo(tempPS, 0);
						Tuple<char, CarPart> clear = new Tuple<char, CarPart>((char)0, CarPart.Null);

						for (int t = break_pos; t != i; t += add)
						{
							if (tempPS[t].Item1 == 0)
							{
								if (tempPS[t + add].Item1 == 0)
								{
									tempPS[t] = tempPS[t + add * 2];
									tempPS[t + add] = tempPS[t + add * 3];
									tempPS[t + add * 2] = clear;
									tempPS[t + add * 3] = clear;
									output += tempPS[t].Item1 + " 2 to the " + direktion_s + ", ";
								}
								else
								{
									tempPS[t] = tempPS[t + add];
									tempPS[t + add] = tempPS[t + add * 2];
									tempPS[t + add * 2] = clear;
									output += tempPS[t].Item1 + " 1 to the " + direktion_s + ", ";
								}
							}
						}
						Output(output[..^2], false);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Wärend der Verarbeitung der angegebenen Datei ist ein Unerwarteter Fehler aufgetreten: \"" + ex.Message +
					"\", möglicher Weise ist das Format der Datei invalide.", "Unerwarteter Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Question);
			}
		}

		private void help_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("cmd", "/c start https://github.com/BW-INF/Aufgabe-1/blob/master/Dokumentation%20Aufgabe%201.pdf");
		}

		private void info_Click(object sender, EventArgs e)
		{
			MessageBox.Show("", "Aufgabe 1: \"Schiebeparkplatz\" - Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}

