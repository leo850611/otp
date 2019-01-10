using NSubstitute;
using OtpLib;
using Xunit;

namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        private readonly IProfile _profile = Substitute.For<IProfile>();
        private readonly IToken _token = Substitute.For<IToken>();
        private readonly ILog _log = Substitute.For<ILog>();
        private readonly AuthenticationService _target;

        public AuthenticationServiceTests()
        {
            _target = new AuthenticationService(_profile, _token, _log);
        }

        [Fact]
        public void IsValidTest_只有驗證Authentication合法或非法()
        {
            //arrange
            IProfile profile = Substitute.For<IProfile>();
            profile.GetPassword("Joey").Returns("91");

            IToken token = Substitute.For<IToken>();
            token.GetRandom("Joey").Returns("abc");

            ILog log = Substitute.For<ILog>();
            AuthenticationService target = new AuthenticationService(profile, token, log);
            string account = "Joey";
            string password = "wrong password";
            // 正確的 password 應為 "91abc"

            //act
            bool actual;
            actual = target.IsValid(account, password);

            // assert
            Assert.False(actual);
        }

        [Fact]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄log()
        {
            //arrange
            GivenProfile("Joey", "91");

            GivenToken("abc");

            // 正確的 password 應為 "91abc"
            //act
            WhenInvalid("Joey", "wrong password");

            // assert
            //ShouldLogMessage("account:Joey try to login failed");
            ShouldLogMessageWith("Joey", "login failed");

            // 試著使用 stub object 的 ReturnsForAnyArgs() 方法
            //例如：profile.GetPassword("").ReturnsForAnyArgs("91"); // 不管GetPassword()傳入任何參數，都要回傳 "91"

            // step 1: arrange, 建立 mock object
            // ILog log = Substitute.For<ILog>();

            // step 2: act

            // step 3: assert, mock object 是否有正確互動
            //log.Received(1).Save("account:Joey try to login failed"); //Received(1) 可以簡化成 Received()
        }

        private void ShouldLogMessageWith(string account, string status)
        {
            _log.Received(1).Save(Arg.Is<string>(m => m.Contains(account) && m.Contains(status)));
        }

        private void ShouldLogMessage(string message)
        {
            _log.Received(1).Save(message);
        }

        private void WhenInvalid(string account, string password)
        {
            _target.IsValid(account, password);
        }

        private void GivenToken(string token)
        {
            _token.GetRandom("Joey").Returns(token);
        }

        private void GivenProfile(string account, string password)
        {
            _profile.GetPassword(account).Returns(password);
        }
    }
}