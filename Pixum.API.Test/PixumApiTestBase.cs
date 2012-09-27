using System.Configuration;
using Pixum.API.Model.Auth;

namespace Pixum.API.Test
{
    public abstract class PixumApiTestBase
    {
        private bool _initialized = false;

        protected PixumApi _pixumApi;
        protected readonly string _authUser;
        protected readonly string _authPassword;
        protected readonly string _email;
        protected readonly string _password;

        public PixumApiTestBase()
        {
            _authUser = ConfigurationManager.AppSettings["AuthUser"];
            _authPassword = ConfigurationManager.AppSettings["AuthPassword"];
            _email = ConfigurationManager.AppSettings["LoginEmail"];
            _password = ConfigurationManager.AppSettings["LoginPassword"];
        }

        public PixumApi InitApi()
        {
            if (_initialized) return _pixumApi;

            var sessionId = PixumApi.GetSessionId(_authUser, _authPassword);
            var loginToken = PixumApi.GetLoginToken(sessionId);

            _pixumApi = new PixumApi(sessionId, loginToken);
            _initialized = true;

            return _pixumApi;
        }

        public PSIUserLogin Login()
        {
            return _pixumApi.Login(_email, _password);
        }
    }
}
