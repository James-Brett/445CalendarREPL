using _445CalendarREPL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _445CalendarREPL
{
    public static class CalendarService
    {
        private const DayOfWeek _firstDayOfTheWeek = DayOfWeek.Monday;
        private const int _startingMonth = 1; //denotes which month should be the first. NB january = 1
        private const int _quartersInAYear = 4;
        private const int _monthsInAQuarter = 3;
        private static readonly int[] _weeksInAMonthPattern = new[] { 4, 4, 5 }; // valid patterns are 445, 454, 544
        private const int _daysInAWeek = 7;

        public static CalendarFiscalYear FourFourFiveCalendarForYear(int year)
        {
            var firstOfGivenYear = new DateTime(year, _startingMonth, 1);
            var dayOfWeek = firstOfGivenYear.DayOfWeek;
            var daysToBacktrack = ((int)dayOfWeek - (int)_firstDayOfTheWeek + _daysInAWeek) % _daysInAWeek ; // this is the number of days we need to go back to get to the start of the week

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
                        Quarter = i + 1,
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

            if (YearRequiresAGapWeek(date))
            {
                var lastMonth = output.Months.Last();
                lastMonth.NumberOfWeeks++;
                
                var gapWeek = new CalendarFiscalWeek() { WeekNumber = lastMonth.NumberOfWeeks, Days = new() };
                for (int i = 0; i < _daysInAWeek; i++)
                {
                    gapWeek.Days.Add(date.ToShortDateString());
                    date += TimeSpan.FromDays(1);
                }

                lastMonth.Weeks.Add(gapWeek);
            }

            return output;
        }

        private static string FiscalMonthName(int quarterIndex, int monthInQuarterIndex)
        {
            var monthNumber = quarterIndex * _monthsInAQuarter + monthInQuarterIndex + 1;
            return CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(monthNumber);
        }

        private static bool YearRequiresAGapWeek(DateTime nextDateNotYetInCalendar)
        {
            //if advancing a week does not advance the year, we need a gap week
            return (nextDateNotYetInCalendar + TimeSpan.FromDays(_daysInAWeek - 1)).Year == nextDateNotYetInCalendar.Year;
        }
    }
}
