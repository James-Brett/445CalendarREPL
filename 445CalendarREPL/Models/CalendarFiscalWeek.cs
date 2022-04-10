using System.Collections.Generic;

namespace _445CalendarREPL.Models
{
    public class CalendarFiscalWeek
    {
        public int WeekNumber { get; set; }
        public List<string> Days { get; set; }

        public CalendarFiscalWeek(int weekNumber)
        {
            WeekNumber = weekNumber;
            Days = new();
        }
    }
}
