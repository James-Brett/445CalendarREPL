using System.Collections.Generic;

namespace _445CalendarREPL.Models
{
    public class FiscalMonth
    {
        public string Month { get; set; }
        public int NumberOfWeeks { get; set; }
        public List<FiscalWeek> Weeks { get; set; }
    }
}
