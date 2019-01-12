using System;
using System.Collections.Generic;
using System.Text;
using OtpLib;
using NUnit.Framework;

namespace OtpTests
{
    public class HolidayTest
    {
        [Test]
        public void IsXmas()
        {
            var holiday = new FakeHoliday();
            holiday.SetDateTime(new DateTime(2019,12,25));
            var actual = holiday.AskIsTodayXmas();
            Assert.AreEqual(actual, "Merry Xmas");
        }

        [Test]
        public void IsNotXmas()
        {
            var holiday = new FakeHoliday();
            holiday.SetDateTime(new DateTime(2019, 12, 20));
            var actual = holiday.AskIsTodayXmas();
            Assert.AreEqual(actual, "Today is not Xmas");
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
