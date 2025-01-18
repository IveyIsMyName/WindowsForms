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
using System.Security.Claims;
using AxWMPLib;


namespace Clock
{
	public partial class MainForm : Form
	{
		Color foreground;
		Color background;
		ChooseFontForm fontDialog = null;
		AlarmsForm alarms = null;
		Alarm nextAlarm = null;
		public MainForm()
		{
			InitializeComponent();
			labelTime.BackColor = Color.Black;
			labelTime.ForeColor = Color.Red;
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);
			SetVisibility(false);
			cmShowConsole.Checked = true;
			LoadSettings();
			alarms = new AlarmsForm();
			LoadAlarms();
			axWindowsMediaPlayer.Visible = false;
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
		void SaveAlarms()
		{
			string executation_path = Path.GetDirectoryName(Application.ExecutablePath);
			string fileName = $"{executation_path}\\..\\..\\Fonts\\Alarms.ini";
			StreamWriter sw = new StreamWriter(fileName);
			for( int i = 0; i < alarms.LB_Alarms.Items.Count; i++)
			{
				sw.WriteLine((alarms.LB_Alarms.Items[i] as Alarm).ToFileString());
			}
			sw.Close();
			//Process.Start("notepad", fileName);
		}
		void LoadAlarms()
		{
			string execution_path = Path.GetDirectoryName(Application.ExecutablePath);
			string fileName = $"{execution_path}\\..\\..\\Fonts\\Alarms.ini";
			try
			{
				StreamReader sr = new StreamReader(fileName);
				while (!sr.EndOfStream)
				{
					string s_alarm = sr.ReadLine();
					string[] s_alarm_parts = s_alarm.Split(',');
					for (int i = 0; i < s_alarm_parts.Length; i++)
						Console.Write(s_alarm_parts[i] + ' ');
					Console.WriteLine();
					Alarm alarm = new Alarm
						(
						s_alarm_parts[0] == "" ? new DateTime() : new DateTime(Convert.ToInt64(s_alarm_parts[0])),
						new TimeSpan(Convert.ToInt64(s_alarm_parts[1])),
						new Week(Convert.ToByte(s_alarm_parts[2])),
						s_alarm_parts[3],
						s_alarm_parts[4]
						);
					alarms.LB_Alarms.Items.Add(alarm);
				}
				sr.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Alarms not found");
			}
			
		}
		Alarm FindNextAlarm()
		{
			Alarm[] actualAlarms = alarms.LB_Alarms.Items
				.Cast<Alarm>()
				//.Where(a => a.Time > DateTime.Now.TimeOfDay)
				.Where(a => a != null && a.Time > DateTime.Now.TimeOfDay)
				.ToArray();
			
			return actualAlarms.Min();
		}
		bool CompareDates(DateTime date1, DateTime date2)
		{
			return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
		}
		void PlayAlarm()
		{
			axWindowsMediaPlayer.URL = nextAlarm.Filename;
			axWindowsMediaPlayer.settings.volume = 100;
			axWindowsMediaPlayer.Ctlcontrols.play();
			axWindowsMediaPlayer.Visible = true;
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
			if (
				nextAlarm != null && 
				(
				nextAlarm.Date == DateTime.MinValue ? 
				nextAlarm.Weekdays.Contains(DateTime.Now.DayOfWeek) : 
				CompareDates(nextAlarm.Date, DateTime.Now)
				) &&
				nextAlarm.Time.Hours == DateTime.Now.Hour && 
				nextAlarm.Time.Minutes == DateTime.Now.Minute && 
				nextAlarm.Time.Seconds == DateTime.Now.Second
				//nextAlarm.Weekdays.IsSet(DateTime.Now.DayOfWeek)
				)
			{
				System.Threading.Thread.Sleep(1000);
				//Console.WriteLine("ALARM!!!");
				PlayAlarm();
				MessageBox.Show(!string.IsNullOrEmpty(nextAlarm.Message) ? nextAlarm.Message : "Alarm!!!!", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				nextAlarm = FindNextAlarm();
				//alarms.LB_Alarms.Items.Remove(nextAlarm);
			}
			if (alarms.LB_Alarms.Items.Count > 0) nextAlarm = FindNextAlarm(); //nextAlarm = alarms.LB_Alarms.Items.Cast<Alarm>().ToArray().Min();
			if (nextAlarm != null) Console.WriteLine(nextAlarm);
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
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
			SaveAlarms();
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
		
		void SetPlayerInvisible(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
		{
			if (axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsMediaEnded || 
				axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
				axWindowsMediaPlayer.Visible = false;
		}
	}
}
