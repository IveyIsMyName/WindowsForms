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
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;


namespace Clock
{
	public partial class MainForm : Form
	{
		Color foreground;
		Color background;
		//PrivateFontCollection fontCollection;
		ChooseFontForm fontDialog = null;
		AlarmsForm alarms = null;
		public MainForm()
		{
			InitializeComponent();
			//fontCollection = new PrivateFontCollection();
			//fontCollection.AddFontFile(@"C:\Users\iveyi\source\repos\WindowsForms\Clock\Fonts\digital-7.ttf");
			//labelTime.Font = new Font(fontCollection.Families[0], 42);
			labelTime.BackColor = Color.Black;
			labelTime.ForeColor = Color.Red;
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);
			SetVisibility(false);
			cmShowConsole.Checked = true;
			LoadSettings();
			//fontDialog = new ChooseFontForm();
			alarms = new AlarmsForm();
		}
		void SetVisibility(bool visible)
		{
			checkBoxShowDate.Visible = visible;
			cbShowWeekDay.Visible = visible;
			btnHideControls.Visible = visible;
			this.TransparencyKey = visible ? Color.Empty : this.BackColor;
			this.FormBorderStyle = visible ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
			this.ShowInTaskbar = visible;
		}
		void SaveSettings()
		{
			StreamWriter sw = new StreamWriter("Settings.ini");
			sw.WriteLine($"{cmTopmost.Checked}");
			sw.WriteLine($"{cmShowControls.Checked}");
			sw.WriteLine($"{cmShowDate.Checked}");
			sw.WriteLine($"{cmShowWeekday.Checked}");
			sw.WriteLine($"{cmShowConsole.Checked}");
			sw.WriteLine($"{labelTime.BackColor.ToArgb()}");
			sw.WriteLine($"{labelTime.ForeColor.ToArgb()}");
			sw.WriteLine($"{fontDialog.FontFileName}");
			sw.WriteLine($"{labelTime.Font.Size}");
			sw.Close();
			//Process.Start("notepad", "Settings.ini");
		}
		void LoadSettings()
		{
			string exePath = Path.GetDirectoryName(Application.ExecutablePath);
			Directory.SetCurrentDirectory($"{exePath}\\..\\..\\Fonts");
			StreamReader sr = new StreamReader("Settings.ini");
			cmTopmost.Checked = bool.Parse(sr.ReadLine());
			cmShowControls.Checked = bool.Parse(sr.ReadLine());
			cmShowDate.Checked = bool.Parse(sr.ReadLine());
			cmShowWeekday.Checked = bool.Parse(sr.ReadLine());
			cmShowConsole.Checked = bool.Parse(sr.ReadLine());
			labelTime.BackColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
			labelTime.ForeColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
			string fontName = sr.ReadLine();
			int fontSize = (int)Convert.ToDouble(sr.ReadLine());
			sr.Close();
			fontDialog = new ChooseFontForm(this, fontName, fontSize);
			labelTime.Font = fontDialog.Font;
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
			if (checkBoxShowDate.Checked)
			{
				labelTime.Text += "\n";
				labelTime.Text += DateTime.Now.ToString("dd.MM.yyyy");
			}
			if (cbShowWeekDay.Checked)
			{
				labelTime.Text += "\n";
				labelTime.Text += DateTime.Now.DayOfWeek;
			}
			notifyIcon.Text = labelTime.Text;
		}

		private void btnHideControls_Click(object sender, EventArgs e)
		{
			SetVisibility(cmShowControls.Checked = false);
		}

		private void labelTime_DoubleClick(object sender, EventArgs e)
		{
			SetVisibility(cmShowControls.Checked = true);
		}

		private void cmExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmTopmost_CheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = cmTopmost.Checked;
		}

		private void cmShowDate_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxShowDate.Checked = cmShowDate.Checked;
		}

		private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e)
		{
			cmShowDate.Checked = checkBoxShowDate.Checked;
		}

		private void cmShowWeekday_CheckedChanged(object sender, EventArgs e)
		{
			cbShowWeekDay.Checked = cmShowWeekday.Checked;
		}

		private void cbShowWeekDay_CheckedChanged(object sender, EventArgs e)
		{
			cmShowWeekday.Checked = cbShowWeekDay.Checked;
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!this.TopMost)
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}

		private void cmForeColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg1 = new ColorDialog();
			DialogResult result = dlg1.ShowDialog();
			if (result == DialogResult.OK)
			{
				foreground = dlg1.Color;
				labelTime.ForeColor = foreground;
			}
		}

		private void cmBackColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg2 = new ColorDialog();
			DialogResult result = dlg2.ShowDialog();
			if (result == DialogResult.OK)
			{
				background = dlg2.Color;
				labelTime.BackColor = background;
			}
		}

		private void cmShowControls_CheckedChanged(object sender, EventArgs e)
		{
			SetVisibility(cmShowControls.Checked);
		}

		private void cmChooseFonts_Click(object sender, EventArgs e)
		{
				if (fontDialog.ShowDialog() == DialogResult.OK)
				{
					labelTime.Font = fontDialog.Font;
				}
		}
		private void cmShowConsole_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as ToolStripMenuItem).Checked)
				AllocConsole();
			else
				FreeConsole();
		}
		[DllImport("kernel32.dll")]
		public static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		public static extern bool FreeConsole();
		//private void SaveSettings()
		//{
		//	Properties.Settings.Default.ForegroundColor = foreground;
		//	Properties.Settings.Default.BackgroundColor = background;
		//	Properties.Settings.Default.Font = labelTime.Font;
		//	Properties.Settings.Default.Save();
		//}
		//private void LoadSettings()
		//{
		//	foreground = Properties.Settings.Default.ForegroundColor;
		//	background = Properties.Settings.Default.BackgroundColor;
		//	labelTime.ForeColor = foreground;
		//	labelTime.BackColor = background;
		//	labelTime.Font = Properties.Settings.Default.Font;
		//}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
		}

		private void cmLoadOnWinStartup_CheckedChanged(object sender, EventArgs e)
		{
			string keyName = "ClockPV_319";
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (cmLoadOnWinStartup.Checked) rk.SetValue(keyName, Application.ExecutablePath);
			else rk.DeleteValue(keyName, false);
			rk.Dispose();
		}

		private void cmAlarm_Click(object sender, EventArgs e)
		{
			alarms.Location = new Point
				(
				this.Location.X - alarms.Width,
				this.Location.Y * 2
				);
			alarms.ShowDialog();
		}
	}
}
