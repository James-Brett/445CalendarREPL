using _445CalendarREPL;
using _445CalendarREPL.Models;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CalendarServiceTests
    {
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
        public void StartsWithJanuary()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var monthName = result.Months.First().FiscalMonth;
            Assert.Equal("January", monthName);
        }

        [Fact]
        public void StartsWithThisYearsCorrectDate()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(2022);
            var firstDate = result.Months.First().Weeks.First().Days.First();
            Assert.Equal("27/12/2021", firstDate);
        }
    }
}