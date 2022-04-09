using System.Collections.Generic;

namespace _445CalendarREPL.Models
{
    public class FiscalYear
    {
        public int FiscalYear { get; set; }
        public List<FiscalMonth> Months { get; set; }
    }
}
