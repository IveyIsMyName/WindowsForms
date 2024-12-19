using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

namespace Clock
{
	public partial class ChooseFontForm : Form
	{
		private static PrivateFontCollection privateFonts = new PrivateFontCollection();
		int fontSize = 24;
		public Font SelectedFont {  get; private set; }
		public ChooseFontForm()
		{
			InitializeComponent();
			LoadFonts();
		}

		private void LoadFonts()
		{
			string fontFolder = @"C:\Users\iveyi\source\repos\WindowsForms\Clock\Fonts";
			List<string> fontFiles = new List<string>();
			fontFiles.AddRange(Directory.GetFiles(fontFolder, "*.ttf", SearchOption.AllDirectories));
			fontFiles.AddRange(Directory.GetFiles(fontFolder, "*.otf", SearchOption.AllDirectories));
			foreach (string fontFile in fontFiles)
			{
				try
				{
					privateFonts.AddFontFile(fontFile);
				}
				catch (Exception ex) 
				{
					MessageBox.Show($"Ошибка при загрузке файла шрифта '{fontFile}': {ex.Message}");
				}
			}
			foreach (FontFamily fontFamily in privateFonts.Families)
			{
				cbFonts.Items.Add(fontFamily.Name);
			}
		}
		private void cbFonts_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedFontName = cbFonts.SelectedItem as string;
			if (!string.IsNullOrEmpty(selectedFontName))
			{
				FontFamily selectedFontFamily = null;
				foreach (FontFamily fontFamily in privateFonts.Families)
				{
					if (fontFamily.Name.Equals(selectedFontName, StringComparison.OrdinalIgnoreCase))
					{
						selectedFontFamily = fontFamily;
						break;
					}
				}
				if (selectedFontFamily != null)
				{
					Font font = new Font(selectedFontFamily, fontSize);
					labelExample.Font = font; 
					SelectedFont = font;
				}
			}
		}

		private void nudFontSize_ValueChanged(object sender, EventArgs e)
		{
			fontSize = (int)nudFontSize.Value;
		}
	}
}
