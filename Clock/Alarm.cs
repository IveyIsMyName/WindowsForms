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
			Console.WriteLine($"CompareTo called: {this.Time} vs {other.Time}");
			return this.Time.CompareTo(other.Time);
		}
	}
}
