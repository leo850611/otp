using System.Diagnostics;
using NSubstitute;
using NUnit.Framework;
using OtpLib;


namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        private static IToken _rsaTokenDao = Substitute.For<IToken>();
        private static IProfile _givenProfile = Substitute.For<IProfile>();

        [Test]
        public void IsValidTest()
        {
            GivenProfile("joey", "91");
            GivenToken("000000");
            ShouldValid("joey", "91000000");
        }

        [Test]
        public void IsInValidTest()
        {
            GivenProfile("joey", "91");
            GivenToken("123456");
            ShouldInValid("joey", "91000000");
        }


        private static void ShouldInValid(string account, string passcode)
        {
            var target = new AuthenticationService(_givenProfile, _rsaTokenDao);
            Assert.False(target.IsValid(account, passcode));
        }
        private static void ShouldValid(string account, string passcode)
        {
            var target = new AuthenticationService(_givenProfile, _rsaTokenDao);
            Assert.True(target.IsValid(account, passcode));
        }

        private static IToken GivenToken(string token)
        {
            _rsaTokenDao.GetRandom("").ReturnsForAnyArgs(token);
            return _rsaTokenDao;
        }

        private static IProfile GivenProfile(string account, string returnThis)
        {
            _givenProfile.GetPassword(account).Returns(returnThis);
            return _givenProfile;
        }

        [SetUp]
        public void SetUp()
        {

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