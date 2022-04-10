using _445CalendarREPL;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CalendarServiceTests
    {
        private const int _gapWeekYear = 2023;

        [Fact]
        public void ReturnsFiscalYear()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsFiscalYearWith364Days()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var days = result.Months
                .SelectMany(m => m.Weeks)
                .SelectMany(w => w.Days);
            Assert.Equal(364, days.Count());
        }

        [Fact]
        public void ReturnsFiscalYearWith52Weeks()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var weeks = result.Months
                .SelectMany(m => m.Weeks);
            Assert.Equal(52, weeks.Count());
        }

        [Fact]
        public void ReturnsFiscalYearWith12Months()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var months = result.Months;
            Assert.Equal(12, months.Count());
        }

        [Fact]
        public void StartsWithJanuary()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var monthName = result.Months.First().FiscalMonth;
            Assert.Equal("January", monthName);
        }

        [Fact]
        public void EndsWithDecember()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var monthName = result.Months.Last().FiscalMonth;
            Assert.Equal("December", monthName);
        }

        [Fact]
        public void StartsWithThisYearsCorrectDate()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(2022);
            var firstDate = result.Months.First().Weeks.First().Days.First();
            Assert.Equal("27/12/2021", firstDate);
        }

        [Fact]
        public void SevenDaysInAllTheWeeks()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var weeks = result.Months.SelectMany(m => m.Weeks);
            Assert.All(weeks, w => Assert.Equal(7, w.Days.Count));
        }

        [Fact]
        public void Follows445Pattern()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var pattern = "";
            foreach (var month in result.Months)
            {
                var weeks = month.Weeks;
                Assert.Equal(month.NumberOfWeeks, weeks.Count);
                pattern += weeks.Count.ToString();
            }

            Assert.Equal("445445445445", pattern);
        }

        [Fact]
        public void ReturnsFiscalYearWith371Days_GapWeek()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(_gapWeekYear);
            var days = result.Months
                .SelectMany(m => m.Weeks)
                .SelectMany(w => w.Days);
            Assert.Equal(371, days.Count());
        }

        [Fact]
        public void StartsWithThisYearsCorrectDate_GapWeek()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(_gapWeekYear);
            var firstDate = result.Months.First().Weeks.First().Days.First();
            Assert.Equal("27/12/2021", firstDate);
        }

        [Fact]
        public void SevenDaysInAllTheWeeks_GapWeek()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(_gapWeekYear);
            var weeks = result.Months.SelectMany(m => m.Weeks);
            Assert.All(weeks, w => Assert.Equal(7, w.Days.Count));
        }

        [Fact]
        public void Follows445Pattern_GapWeek()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var pattern = "";
            foreach (var month in result.Months)
            {
                var weeks = month.Weeks;
                Assert.Equal(month.NumberOfWeeks, weeks.Count);
                pattern += weeks.Count.ToString();
            }

            Assert.Equal("445445445446", pattern);
        }
    }
}