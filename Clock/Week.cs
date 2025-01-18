using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
	public class Week
	{
		public static readonly string[] Weekdays = new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вск" };

		byte week;
		public Week(byte week = 0) 
		{ 
			this.week = week; 
		}
		public Week(bool[] days)
		{
			CompressWeekDays(days);
		}
		
		
		public void CompressWeekDays(bool[] days)
		{
			for (byte i = 0; i < days.Length; i++)
			{
				if (days[i]) week |= (byte)(1 << i);
			}
		}
		public bool[] ExtractWeekDays()
		{
			bool[] weekDays = new bool[7];
			for (byte i = 0; i < 7; i++)
			{
				weekDays[i] = (week & (byte)(1 << i)) != 0;
			}

			return weekDays;
		}
		public bool Contains(DayOfWeek day)
		{
			int iDay = (int)day;
			iDay -= 1;
			if (iDay == -1) iDay = 6;
			return (week & (1 <<iDay)) != 0;	
		}
		public override string ToString()
		{
			string weekdays = "";
			for (byte i = 0; i < Weekdays.Length; i++)
			{
				if (((1 << i) & week) != 0)
					weekdays += $"{Weekdays[i]},";
			}
			return weekdays;
		}
		public bool IsSet (DayOfWeek day)
		{
			int dayIndex = ((int)day + 6) % 7;
			return (week & (1 << dayIndex)) != 0;
		}
		public string ToFileString()
		{
			return week.ToString();
		}
	}
}
