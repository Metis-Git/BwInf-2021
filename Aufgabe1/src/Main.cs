using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Task1
{

	enum CarPart { Null, X1, X2 };

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
				for (int i = 0; i < 70; i++) s[^1] += "--";
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

				this.Output("Ausgewählte Datei:\n-Dateipfad: \"" + path + "\"\n-Dateiname: \"" + name + "\"");
			}

		}

		private string[] ReadFile()
		{
			string[] content = new string[0];

			if (!File.Exists(this.path)) { MessageBox.Show("Datei exsitiert nicht (mehr)!"); return content; }

			StreamReader file = new StreamReader(this.path);
			string line = file.ReadLine();

			while (line != null)
			{
				Array.Resize(ref content, content.Length + 1);
				content[^1] = line;
				line = file.ReadLine();
			}

			file.Close();

			return content;
		}

		private void Clear(ref Tuple<char, CarPart>[] cars, int index)
		{
			cars[index] = new Tuple<char, CarPart>((char)0, CarPart.Null);	// leeren
		}

		private Tuple<char, CarPart>[] ConvertToTuple(ref string[] input)
		{
			int nCars = input[0].ToUpper()[2] - input[0].ToUpper()[0] + 1;
			Tuple<char, CarPart>[] parkingSpot = new Tuple<char, CarPart>[nCars];

			for (int i = 0; i < nCars; i++) parkingSpot[i] = new Tuple<char, CarPart>((char)0, CarPart.Null);

			nCars = Convert.ToInt32(input[1]);

			for (int i = 0; i < nCars; i++)
			{
				int pos = Convert.ToInt32(input[2 + i][2..]);
				char car = input[2 + i].ToUpper()[0];
				parkingSpot[pos] = new Tuple<char, CarPart>(car, CarPart.X1);       // vorne	-> X1
				parkingSpot[pos + 1] = new Tuple<char, CarPart>(car, CarPart.X2);   // hinten	-> X2
			}

			return parkingSpot;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns> 1 => left, -1 => right</returns>
		private int FindBestDirektion(ref Tuple<char, CarPart>[] parkingSpot, out int breakPos, ref int index)
		{
			bool isX1 = parkingSpot[index].Item2 == CarPart.X1;
			int isNullPos = 0;
			breakPos = 0;

			for (int t = index; t < parkingSpot.Length; t++)
			{
				if (parkingSpot[t].Item1 == 0)	// Parkplatz frei
				{
					if (isX1 || isNullPos == 1) { breakPos = t; break; };
					isNullPos = 1;
				}
			}

			for (int t = index, stepCounter = index; t >= 0; t--, stepCounter++)
			{
				if (parkingSpot[t].Item1 == 0)
				{
					if (!isX1 || isNullPos < 0)
					{
						if (breakPos == 0 || breakPos > (isX1 ? stepCounter : stepCounter - 1))
						{
							breakPos = t;
							return 1;
						}
						return -1;
					}
					isNullPos -= 2;
				}
				else if (breakPos != 0 && stepCounter > breakPos + 1) return -1;
			}

			return -1;
		}

		private void start_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				string[] content = ReadFile();
				if (content.Length == 0) return;    // siehe ReadFile();

				Tuple<char, CarPart>[] parkingSpot = ConvertToTuple(ref content);

				string output = "\n";

				// output des Parkplatzes
				for (int i = 0; i < parkingSpot.Length; i++) output += ((char)(i + 65)) + " ";
				output += "\n";
				for (int i = 0; i < parkingSpot.Length; i++) output += parkingSpot[i].Item1 == 0 ? "  " : parkingSpot[i].Item1 + " ";
				Output(output);

				for (int i = parkingSpot.Length - 1; i >= 0; i--)	// rückwärts, damit's vorwärts ausgegeben wird
				{
					output = ((char)(i + 65)) + ": ";

					if (parkingSpot[i].Item1 == 0) Output(output, false);		// Ja!!! Wir müssen es nur ausgeben!
					else
					{
						int break_pos;
						int add = FindBestDirektion(ref parkingSpot, out break_pos, ref i);

						string direktion_s = add == 1 ? "links" : "rechts";

						Tuple<char, CarPart>[] tempPS = new Tuple<char, CarPart>[parkingSpot.Length];
						parkingSpot.CopyTo(tempPS, 0);

						for (int t = break_pos; t != i; t += add /* 1/-1 */)	// von den angehaltenen Punkt auf-/abwärts
						{
							if (tempPS[t].Item1 == 0)
							{
								if (tempPS[t + add].Item1 == 0) // leer d.h. um zwei verschieben
								{
									tempPS[t] = tempPS[t + add * 2];		// fahre zwei Felder
									tempPS[t + add] = tempPS[t + add * 3];

									Clear(ref tempPS, t + add * 2);
									Clear(ref tempPS, t + add * 3);

									output += tempPS[t].Item1 + " zwei nach " + direktion_s + ", ";
								}
								else	// da steht was d.h. einmal verschieben
								{
									tempPS[t] = tempPS[t + add];			// fahre ein Feld
									tempPS[t + add] = tempPS[t + add * 2];

									Clear(ref tempPS, t + add * 2);

									output += tempPS[t].Item1 + " eins nach " + direktion_s + ", ";
								}

							}
						}
						Output(output[..^2] /* entfernt das letzte ", " */, false);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Während der Verarbeitung der angegebenen Datei ist ein unerwarteter Fehler aufgetreten: \"" + ex.Message +
					"\", möglicher Weise ist das Format der Datei falsch.", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Question);
			}
		}

		private void help_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("cmd", "/c start https://github.com/Metis-Git/BwInf-2021/blob/main/Aufgabe1.pdf");
		}

		private void info_Click(object sender, EventArgs e)
		{
			MessageBox.Show("BwInf 2021, Aufgabe 1 \"Schiebeparkplatz\"\nTeam-Name: \"SRZ info3 Gruppe 1\"\nTeam-ID: 00564\nErsteller: K. Jahn", "Aufgabe 1: \"Schiebeparkplatz\" - Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}

