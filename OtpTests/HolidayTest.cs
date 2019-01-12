using System;
using System.Collections.Generic;
using System.Text;
using OtpLib;
using Xunit;

namespace OtpTests
{
    public class HolidayTest
    {
        [Fact]
        public void IsXmas()
        {
            var holiday = new FakeHoliday();
            holiday.SetDateTime(new DateTime(2019,12,25));
            var actual = holiday.AskIsTodayXmas();
            Assert.Equal(actual, "Merry Xmas");
        }

        [Fact]
        public void IsNotXmas()
        {
            var holiday = new FakeHoliday();
            holiday.SetDateTime(new DateTime(2019, 12, 20));
            var actual = holiday.AskIsTodayXmas();
            Assert.Equal(actual, "Today is not Xmas");
        }
    }


    public class FakeHoliday : Holiday
    {
        DateTime date = new DateTime();
        public void SetDateTime(DateTime d)
        {
            date = d;
        }

        protected override DateTime GetDateTime()
        {
             return date;
        }
    }
}
