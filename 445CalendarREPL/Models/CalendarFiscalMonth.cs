﻿using System.Collections.Generic;

namespace _445CalendarREPL.Models
{
    public class CalendarFiscalMonth
    {
        public string FiscalMonth { get; set; }
        public int NumberOfWeeks { get; set; }
        public List<CalendarFiscalWeek> Weeks { get; set; }
    }
}
