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
using static System.Net.WebRequestMethods;
using System.Drawing.Text;


namespace Clock
{
	public partial class ChooseFontForm : Form
	{
		override public Font Font {  get; set; }
		public int selectedFontSize;
		public string FontFileName { get; set; }
		public ChooseFontForm()
		{
			InitializeComponent();
			LoadFonts();
			cbFonts.SelectedIndex = 0;
			selectedFontSize = Convert.ToInt32(nudFontSize.Value);
		}
		void LoadFonts()
		{
			Directory.SetCurrentDirectory("..\\..\\Fonts");
			Console.WriteLine(Directory.GetCurrentDirectory());
			cbFonts.Items.AddRange(GetFontsFormat("*.ttf"));
			cbFonts.Items.AddRange(GetFontsFormat("*.otf"));
		}
		static string[] GetFontsFormat(string format)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), format);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Split('\\').Last();
			}
			return files;
		}
		private void cbFonts_SelectedIndexChanged(object sender, EventArgs e)
		{
			//selectedFontFileName = cbFonts.SelectedItem.ToString();
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile($"{Directory.GetCurrentDirectory()}\\{cbFonts.SelectedItem}");
			labelExample.Font = new Font(pfc.Families[0], Convert.ToInt32(nudFontSize.Value));
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			Font = labelExample.Font;
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile($"{Directory.GetCurrentDirectory()}\\{cbFonts.SelectedItem}");
			labelExample.Font = new Font(pfc.Families[0], selectedFontSize);

		}

		private void nudFontSize_ValueChanged(object sender, EventArgs e)
		{
			selectedFontSize = Convert.ToInt32(nudFontSize.Value);
		}
	}
}
