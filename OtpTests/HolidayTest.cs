using System;
using System.Collections.Generic;
using System.Text;
using OtpLib;
using NUnit.Framework;

namespace OtpTests
{
    public class HolidayTest
    {
        private FakeHoliday _fakeHoliday = new FakeHoliday();

        [Test]
        public void IsXmas()
        {
            GivenToday(2019, 12, 25);
            ResponseShouldEqual("Merry Xmas");
        }

        [Test]
        public void IsNotXmas()
        {
            GivenToday(2019, 12, 01);
            ResponseShouldEqual("Today is not Xmas");
        }

        private void ResponseShouldEqual(string merryXmas)
        {
            var actual = _fakeHoliday.AskIsTodayXmas();
            Assert.AreEqual(actual, merryXmas);
        }

        private void GivenToday(int year, int month, int day)
        {
            _fakeHoliday.SetDateTime(new DateTime(year, month, day));
        }
    }


    public class FakeHoliday : Holiday
    {
        DateTime _date;
        public void SetDateTime(DateTime d)
        {
            _date = d;
        }

        protected override DateTime GetDateTime()
        {
             return _date;
        }
    }
}
