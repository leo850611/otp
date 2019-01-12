using NSubstitute;
using NUnit.Framework;
using OtpLib;


namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        [Test]
        public void IsValidTest()
        {
            var profileDao = Substitute.For<IProfile>();
            profileDao.GetPassword("joey").Returns("91");

            var rsaTokenDao = Substitute.For<IToken>();
            rsaTokenDao.GetRandom("").ReturnsForAnyArgs("000000");

            var target = new AuthenticationService(profileDao, rsaTokenDao);
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