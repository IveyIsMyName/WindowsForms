using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class AddAlarmForm : Form
	{
		public AddAlarmForm()
		{
			InitializeComponent();
			System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
			System.Threading.Thread.CurrentThread.CurrentCulture = culture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
			dtpTime.Format = DateTimePickerFormat.Custom;
			dtpTime.CustomFormat = "hh:mm:ss tt";
			dtpDate.Enabled = false;
		}

		private void cbUseDate_CheckedChanged(object sender, EventArgs e)
		{
			dtpDate.Enabled = cbUseDate.Checked;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < clbWeekDays.CheckedItems.Count; i++)
			{
				Console.Write(clbWeekDays.GetItemChecked(i) + "\t");
			}
			Console.WriteLine();
		}
	}
}
