using NSubstitute;
using OtpLib;
using Xunit;

namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        private readonly IProfile _stubProfile = Substitute.For<IProfile>();
        private readonly IToken _stubToken = Substitute.For<IToken>();
        private readonly AuthenticationService _target;

        public AuthenticationServiceTests()
        {
            _target = new AuthenticationService(_stubProfile, _stubToken);
        }

        [Fact]
        public void is_valid()
        {
            GivenProfile("joey", "91");
            GivenToken("000000");

            ShouldBeValid("joey", "91000000");
        }

        [Fact]
        public void is_invalid()
        {
            GivenProfile("joey", "91");
            GivenToken("000000");

            ShouldBeInvalid("joey", "wrong passcode");
        }

        private void ShouldBeInvalid(string account, string passcode)
        {
            Assert.False(_target.IsValid(account, passcode));
        }

        private void ShouldBeValid(string account, string passcode)
        {
            Assert.True(_target.IsValid(account, passcode));
        }

        private void GivenToken(string token)
        {
            _stubToken.GetRandom("").ReturnsForAnyArgs(token);
        }

        private void GivenProfile(string account, string password)
        {
            _stubProfile.GetPassword(account).Returns(password);
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