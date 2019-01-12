using OtpLib;
using NUnit.Framework;

namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        [Test]
        public void IsValidTest()
        {
            var target = new AuthenticationService(new FakeProfileDao(),new FakeRsaTokenDao());
            var actual = target.IsValid("joey", "91000000");
            Assert.True(actual);
        }
    }


    internal class FakeRsaTokenDao : IRsaTokenDao
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }


    internal class FakeProfileDao : IProfileDao
    {
        public string GetPassword(string account)
        {
            if (account == "joey")
            {
                return "91";
            }

            return "";
        }
    }
}