using _445CalendarREPL;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CalendarServiceTests
    {
        private const int _gapWeekYear = 2023;

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(_gapWeekYear)]
        [InlineData(2024)]
        public void ReturnsFiscalYear(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(2024)]
        public void ReturnsFiscalYearWith364Days(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            var days = result.Months
                .SelectMany(m => m.Weeks)
                .SelectMany(w => w.Days);
            Assert.Equal(364, days.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(2024)]
        public void ReturnsFiscalYearWith52Weeks(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            var weeks = result.Months
                .SelectMany(m => m.Weeks);
            Assert.Equal(52, weeks.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(_gapWeekYear)]
        [InlineData(2024)]
        public void ReturnsFiscalYearWith12Months(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            var months = result.Months;
            Assert.Equal(12, months.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(_gapWeekYear)]
        [InlineData(2024)]
        public void StartsWithJanuary(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            var monthName = result.Months.First().FiscalMonth;
            Assert.Equal("January", monthName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(_gapWeekYear)]
        [InlineData(2024)]
        public void EndsWithDecember(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
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

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(_gapWeekYear)]
        [InlineData(2024)]
        public void SevenDaysInAllTheWeeks(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
            var weeks = result.Months.SelectMany(m => m.Weeks);
            Assert.All(weeks, w => Assert.Equal(7, w.Days.Count));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2022)]
        [InlineData(2024)]
        public void Follows445Pattern(int year)
        {
            var result = CalendarService.FourFourFiveCalendarForYear(year);
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
            Assert.Equal("26/12/2022", firstDate);
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
            var result = CalendarService.FourFourFiveCalendarForYear(_gapWeekYear);
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