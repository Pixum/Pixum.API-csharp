using Xunit;

namespace Pixum.API.Test
{
    public class PixumApiTest : PixumApiTestBase
    {
        [Fact]
        public void Test_GetSessionId()
        {
            // Arrange
            var initResult = InitApi();

            // Assert
            Assert.NotNull(initResult.SessionId); //, "Session Id should have a value");
        }

        [Fact]
        public void Test_Login_And_Logout()
        {
            // Arrange, Act
            InitApi();
            var login = Login();
            
            // Assert
            Assert.NotNull(login);
            Assert.Equal(_email, login.email);

            Assert.DoesNotThrow(() =>
            {
                var logoutResult = _pixumApi.Logout().Result;
            });
        }
    }
}
