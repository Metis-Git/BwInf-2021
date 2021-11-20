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

namespace bwinfTest1
{
	public partial class Main : Form
	{
		private string path = "";
		private string name = "";
		private static Random rand = new Random();
		private static bool[,] filled;

		public Main()
		{
			filled = new bool[0, 0];
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.label1.Text = "";
		}
	
	private void startLvl1_Click(object sender, EventArgs e) { DistributeLetters(1); }
	private void startLvl2_Click(object sender, EventArgs e) { DistributeLetters(2); }
	private void startLvl3_Click(object sender, EventArgs e) { DistributeLetters(3); }
	
		private void openFile_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
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
				this.startLvl1.Visible = true;
				this.startLvl2.Visible = true;
				this.startLvl3.Visible = true;
			}
		}

		private string[] readFile()
		{
			if (!File.Exists(this.path)) { MessageBox.Show("Datei exsitiert nicht mehr!"); return null; }

			string[] content = new string[0];

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
		
		private void DistributeLetters(int level) {
			
			string[] data = readFile();
			
			try {
				//Speichern der Höhe und Breite des Feldes
				string firstLine = data[0];
				string[] heigthWidth = firstLine.Split(" ");
				int height = int.Parse(heigthWidth[0]);
				int width = int.Parse(heigthWidth[1]);
				filled = new bool[height, width]; //speichert alle besetzten Plätze

				//Speichern der Worte
				string[] words = new string[data.Length];
				data.CopyTo(words, 0);
				
				//die ersten zwei Elemente werden gelöscht
				for (int i = 0; i < data.Length - 2; i++)
					words[i] = words[i + 2];
				
				//Auswählen aller Buchstaben, die eingesetzt werden können
				string possibleLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				
				if(level == 1) DelWordsLetter(ref possibleLetters, ref words);
				
				string[] field = new string[height];
				string[] changedField = new string[height];
				
				for(int i = 0; i < height; i++) {
					for(int k = 0; k < width; k++) {
						field[i] += generateGridLetters(possibleLetters);
					}
				}
				
				if(level == 3) TurnRandomWords(ref words);
				
				for(int i = 0; i < words.Length - 2;) {
					switch((level == 1 ? rand.Next(0, 2) : rand.Next(0, 3))) {
						case 0: changedField = putWordHorizontal(height, width, words[i], possibleLetters, field); break;
						case 1: changedField = putWordVertical(height, width, words[i], possibleLetters, field); break;
						case 2: changedField = putWordDiagonal(height, width, words[i], possibleLetters, field); break;
					}
					
					if(!Equals(field, changedField)) {
						Array.Copy(changedField, 0, field, 0, changedField.Length);
						i++;
					}
				}     

				output(field);
				this.copy.Visible = true;
			
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		
		private void DelWordsLetter(ref string possibleLetters, ref string[] words) {
			foreach(string word in words) {
				for(int i = 0; i < word.Length; i++) {
					for (int k = 0; k < possibleLetters.Length; k++) {
						if(word[i] == possibleLetters[k]) ReplaceAt(ref possibleLetters, '#', k);
					}
				}
			}
		}
	
		private void TurnRandomWords(ref string[] words) {
			
			for (int i = 0; i < words.Length - 2; i++)
			{
				if (rand.Next(0, 2) == 0)
				{
					string reverseWord = "";
					for (int j = (words[i].Length) - 1; j >= 0; j--)
						reverseWord += words[i][j];
					
					words[i] = reverseWord;
				}
			}
		}
		
		private static string[] putWordHorizontal(int height, int width, string word, string possibleLetters, string[] field)
		{
			string[] functionField = new string[height];
			Array.Copy(field, 0, functionField, 0, field.Length);
			int locationY = rand.Next(0, height);
			int locationX = rand.Next(0, (width - word.Length) + 1);

			bool locationFree = true;
			for (int j = 0; j < word.Length; j++)
			{
				if (filled[locationY, locationX + j] == true)
				{
					if (functionField[locationY][locationX + j] == word[j])
					{

					}

					else
					{
						locationFree = false;
						break;
					}
				}
			}

			//Einsetzen des Wortes
			if (locationFree == true)
			{
				for (int j = 0; j < word.Length; j++)
				{
					ReplaceAt(ref functionField[locationY], word[j], locationX + j);
					filled[locationY, locationX + j] = true;
				}
			}


			return functionField;
		}

		private static string[] putWordVertical(int height, int width, string word, string possibleLetters, string[] field)
		{
			string[] functionField = new string[height];
			Array.Copy(field, 0, functionField, 0, field.Length);
			int locationY = rand.Next(0, (height - word.Length) + 1);
			int locationX = rand.Next(0, (width));

			bool locationFree = true;
			for (int j = 0; j < word.Length; j++)
			{
				if (filled[locationY + j, locationX] == true)
				{
					if (functionField[locationY + j][locationX] == word[j])
					{

					}

					else
					{
						locationFree = false;
						break;
					}
				}
			}

			//Einsetzen des Wortes
			if (locationFree == true)
			{
				for (int j = 0; j < word.Length; j++)
				{
					ReplaceAt(ref functionField[locationY + j], word[j], locationX);
					filled[locationY + j, locationX] = true;
				}
			}

			return functionField;
		}

		private static string[] putWordDiagonal(int height, int width, string word, string possibleLetters, string[] field)
		{
			string[] functionField = new string[height];
			Array.Copy(field, 0, functionField, 0, field.Length);
			int locationY = rand.Next(0, height - word.Length + 1);
			int locationX = rand.Next(0, width - word.Length + 1);

			bool locationFree = true;
			for (int j = 0; j < word.Length; j++)
			{
				if (filled[locationY + j, locationX + j] == true)
				{
					if (functionField[locationY + j][locationX + j] == word[j])
					{

					}

					else
					{
						locationFree = false;
						break;
					}
				}
			}

			//Einsetzen des Wortes
			if (locationFree == true)
			{
				for (int j = 0; j < word.Length; j++)
				{
					ReplaceAt(ref functionField[locationY + j], word[j], locationX + j);
					filled[locationY + j, locationX + j] = true;
				}
			}
			return functionField;
		}

		//Methode zum Generiern vom Buchstaben im Feld
		private char generateGridLetters(string possibleLetters)
		{
			{
				char letter = '#';
				
				while (letter == '#') {
					int letterNumber = rand.Next(0, 26);

					letter = possibleLetters[letterNumber];                    
				}
				
				return letter;
			}
		}

		//überprüft ob 2 Arrays die gleichen Werte enthalten
		private static bool Equals(string[] e, string[] f)
		{
			for (int i = 0; i < e.Length; i++)
			{
				if (!e[i].Equals(f[i])) return false;
			}

			return true;
		}

		private void output(string[] field)
		{
			label1.Text = "";
			for (int i = 0; i < field.Length; i++)
			{
				for(int k = 0; k < field[i].Length; k++) label1.Text += field[i][k] + " ";
				label1.Text += "\n";
			}
		}

		static void ReplaceAt(ref string s, char c, int index)
		{
			char[] cArr = s.ToCharArray();
			cArr[index] = c;
			s = new string(cArr);
		}

		private void copy_Click(object sender, EventArgs e)
		{
			if(label1.Text != null) Clipboard.SetText(label1.Text);
		}
	}
}
