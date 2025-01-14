using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
	public class Alarm:IComparable<Alarm>
	{
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public Week Weekdays { get; set; }
		public string Filename { get; set; }
		public string Message { get; set; }
		public Alarm() 
		{
			Weekdays = new Week();
		}
		public Alarm(DateTime date, TimeSpan time, Week week, string Filename, string Message)
		{
			this.Date = date;
			this.Time = time;
			this.Weekdays = week;
			this.Filename = Filename;
			this.Message = Message;
		}
		public Alarm(Alarm other)
		{
			this.Date = other.Date;
			this.Time = other.Time;
			this.Weekdays = other.Weekdays;
			this.Filename = other.Filename;
			this.Message = other.Message;
		}
		public override string ToString()
		{
			string info = "";
			if (Date != DateTime.MinValue) info += $"{Date}\t";
			DateTime time = DateTime.Today.Add(Time);
			info += time.ToString("hh:mm:ss tt");
			info += "\t";
			info += $"{Weekdays}\t";
			info += $"{Filename}\t";
			info += $"{Message}\t";
			return info;
		}
		public int CompareTo(Alarm other)
		{
			//Console.WriteLine($"CompareTo called: {this.Time} vs {other.Time}");
			if (other == null)
			{
				Console.WriteLine("CompareTo: other is null");
				return 1;
			}
			return this.Time.CompareTo(other.Time);
		}
	}
}
