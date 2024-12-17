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

namespace Clock
{
	public partial class MainForm : Form
	{
		Color foreground;
		Color background;
		PrivateFontCollection fontCollection;
		public MainForm()
		{
			InitializeComponent();
			fontCollection = new PrivateFontCollection();
			fontCollection.AddFontFile(@"C:\Users\iveyi\source\repos\WindowsForms\Clock\Fonts\digital-7.ttf");
			labelTime.Font = new Font(fontCollection.Families[0], 42);
			labelTime.BackColor = Color.AliceBlue;
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);
			SetVisibility(false);
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
	}
}
