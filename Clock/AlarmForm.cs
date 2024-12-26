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
	public partial class AlarmForm : Form
	{
		private MainForm mainForm;
		public AlarmForm(MainForm callingForm)
		{
			InitializeComponent();
			this.mainForm = callingForm;
			dateTimePicker1.Format = DateTimePickerFormat.Custom;
			dateTimePicker1.CustomFormat = "dd.MM.yyyy hh:mm:ss";
			dateTimePicker1.ShowUpDown = true;
			dateTimePicker1.MinDate = DateTime.Today;
		}

		private void btnAddAlarm_Click(object sender, EventArgs e)
		{
			DateTime selectedTime = dateTimePicker1.Value;
			string message = txtMessage.Text;
			mainForm.AddAlarm(selectedTime, message);
			listBoxAlarms.Items.Add($"{selectedTime.ToString("dd.MM.yyyy hh:mm:ss")} - {message}");
			txtMessage.Clear();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			// Перебираем каждый элемент в listBoxAlarms
			foreach (var item in listBoxAlarms.Items)
			{
				// Извлекаем строковое представление времени и сообщения будильника
				string alarmText = item.ToString();
				string[] parts = alarmText.Split(new[] { " - " }, StringSplitOptions.None);

				if (parts.Length > 0 && DateTime.TryParse(parts[0], out DateTime alarmTime))
				{
					// Удаляем будильник из основной формы
					mainForm.RemoveAlarm(alarmTime);
				}
			}
			this.Close();
		}
	}
}
