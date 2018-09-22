using System;
using OtpLib;
using Xunit;

namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void IsValidTest()
        {
            var target = new AuthenticationService(new StubProfile(), new StubToken());

            var actual = target.IsValid("joey", "91000000");
            Assert.True(actual);
        }
    }

    internal class StubProfile : IProfile
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

    internal class StubToken : IToken
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }
}