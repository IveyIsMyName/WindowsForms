﻿using System;
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
	public partial class AlarmsForm : Form
	{
		AddAlarmForm addAlarm = null;
		OpenFileDialog openFile	= null;
		public ListBox LB_Alarms
		{
			get => lbAlarms;
		}
		//Alarm newAlarm = new Alarm(Date, Time)
		//{
		//	Date = DateTime.Now,
		//	Time = TimeSpan.Zero,
		//};
		public AlarmsForm()
		{
			InitializeComponent();
			addAlarm = new AddAlarmForm();
			openFile = new OpenFileDialog();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			addAlarm.StartPosition = FormStartPosition.Manual;
			addAlarm.Location = new Point(this.Location.X + 25, this.Location.Y + 25);

			DialogResult result = addAlarm.ShowDialog();
			if(result == DialogResult.OK)
			{
				lbAlarms.Items.Add(addAlarm.Alarm);
			}
		}

		private void lbAlarms_DoubleClick(object sender, EventArgs e)
		{
			addAlarm.Alarm = lbAlarms.SelectedItem as Alarm;
			if(addAlarm.ShowDialog() == DialogResult.OK)
			{
				lbAlarms.Items[lbAlarms.SelectedIndex] = addAlarm.Alarm;
			}
		}
	}
}
