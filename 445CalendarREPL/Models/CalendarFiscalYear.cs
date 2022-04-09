using System.Collections.Generic;

namespace _445CalendarREPL.Models
{
    public class CalendarFiscalYear
    {
        public int FiscalYear { get; set; }
        public List<CalendarFiscalMonth> Months { get; set; }
    }
}
