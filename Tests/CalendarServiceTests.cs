using _445CalendarREPL;
using _445CalendarREPL.Models;
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
    }
}