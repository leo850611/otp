using Moq;
using NSubstitute;
using NUnit.Framework;
using OtpLib;


namespace OtpTests
{
    public class AuthenticationServiceTests
    {
        private IProfile _profile;
        private IToken _token;

        [SetUp]
        public void SetUp()
        {
            _profile = Substitute.For<IProfile>();
            _token = Substitute.For<IToken>();
        }

        [Test]
        public void IsValidTest_只有驗證Authentication合法或非法()
        {
            _profile.GetPassword("Joey").Returns("91");
            _token.GetRandom("Joey").Returns("abc");

            ILog log = Substitute.For<ILog>();
            AuthenticationService target = new AuthenticationService(_profile, _token, log);
            string account = "Joey";
            string password = "wrong password";
            // 正確的 password 應為 "91abc"

            //act
            var actual = target.IsValid(account, password);

            // assert
            Assert.False(actual);
        }

        [Test]
        public void IsValidTest_如何驗證當非法登入時有正確紀錄log()
        {
            // 試著使用 stub object 的 ReturnsForAnyArgs() 方法
            //例如：profile.GetPassword("").ReturnsForAnyArgs("91"); // 不管GetPassword()傳入任何參數，都要回傳 "91"
            _profile.GetPassword("Joey").Returns("91");

            IToken token = Substitute.For<IToken>();
            token.GetRandom("Joey").Returns("abc");
            // step 1: arrange, 建立 mock object
            ILog log = Substitute.For<ILog>();
            AuthenticationService target = new AuthenticationService(_profile, _token, log);
            string account = "Joey";
            string password = "wrong password";
            // 正確的 password 應為 "91abc"

            //act
            var actual = target.IsValid(account, password);

            // step 2: act
            Assert.False(actual);
            // step 3: assert, mock object 是否有正確互動
            //log.Received(1).Save("account:Joey try to login failed"); //Received(1) 可以簡化成 Received()
            log.Received(1).Save(Arg.Is<string>(m => m.Contains(account) )); //Received(1) 可以簡化成 Received()
        }
    }
}