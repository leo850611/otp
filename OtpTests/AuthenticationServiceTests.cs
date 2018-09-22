using System;
using NSubstitute;
using OtpLib;
using Xunit;

namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void IsValidTest()
        {
            var stubProfile = Substitute.For<IProfile>();
            stubProfile.GetPassword("joey").Returns("91");
            
            var stubToken = Substitute.For<IToken>();
            stubToken.GetRandom("").ReturnsForAnyArgs("000000");
            
            var target = new AuthenticationService(stubProfile,stubToken);
//            var target = new AuthenticationService(new StubProfile(), new StubToken());

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