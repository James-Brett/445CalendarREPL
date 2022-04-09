using _445CalendarREPL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace _445CalendarREPL
{
    public static class CalendarService
    {
        private const DayOfWeek _firstDayOfTheWeek = DayOfWeek.Monday;
        private const int _startingMonthIndex = 0; //denotes which month should be the first. NB january = 0 
        private const int _quartersInAYear = 4;
        private const int _monthsInAQuarter = 3;
        private static readonly int[] _weeksInAMonthPattern = new[] { 4, 4, 5 }; // valid patterns are 445, 454, 544
        private const int _daysInAWeek = 7;

        public static CalendarFiscalYear FourFourFiveCalendarForYear(int year)
        {
            var firstOfGivenYear = DateTime.ParseExact($"01/{_startingMonthIndex}/{year}", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var dayOfWeek = firstOfGivenYear.DayOfWeek;
            var daysToBacktrack = (int)dayOfWeek - (int)_firstDayOfTheWeek; // this is the number of days we need to go back to get to the start of the week

            var date = firstOfGivenYear - TimeSpan.FromDays(daysToBacktrack);

            var output = new CalendarFiscalYear
            {
                FiscalYear = year,
                Months = new List<CalendarFiscalMonth>()
            };

            for (var i = 0; i < _quartersInAYear; i++)
            {
                for (var j = 0; j < _monthsInAQuarter; j++)
                {
                    var weeksInMonth = _weeksInAMonthPattern[j % _weeksInAMonthPattern.Length];
                    var month = new CalendarFiscalMonth
                    {
                        FiscalMonth = FiscalMonthName(i, j),
                        NumberOfWeeks = weeksInMonth,
                        Weeks = new List<CalendarFiscalWeek>()
                    };
                    
                    for (int k = 0; k < weeksInMonth; k++)
                    {
                        var week = new CalendarFiscalWeek
                        {
                            WeekNumber = k + 1,
                            Days = new List<string>()
                        };
                        
                        for (int l = 0; l < _daysInAWeek; l++)
                        {
                            week.Days.Add(date.ToShortDateString());
                            date += TimeSpan.FromDays(1);
                        }

                        month.Weeks.Add(week);
                    }

                    output.Months.Add(month);
                }
            }

            return output;
        }

        private static string FiscalMonthName(int quarterIndex, int monthInQuarterIndex)
        {
            var monthIndex = quarterIndex * _monthsInAQuarter + monthInQuarterIndex + 1;
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthIndex);
        }
    }
}
