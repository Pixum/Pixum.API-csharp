using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestSharp;
using Pixum.API.Model;
using Pixum.API.Model.Auth;
using Pixum.API.Services;

namespace Pixum.API
{
    public class PixumApi : PixumApiBase
    {
        readonly string _sessionId;
        readonly string _loginToken;

        public Image Image { get; set; }
        public Album Album { get; set; }
        public string SessionId { get { return _sessionId; } }
        public string LoginToken { get { return _loginToken; } }

        /// <summary>
        /// Creates new Pixum Service Api instance.
        /// </summary>
        /// <param name="sessionId">A valid sessionId created by PixumApi.GetSessionId</param>
        /// <param name="loginToken">A valid login token created by PixumApi.GetLoginToken</param>
        public PixumApi(string sessionId, string loginToken) : base(new RestClient(PixumApiBase.BaseURL))
        {
            _sessionId = sessionId;
            _loginToken = loginToken;

            _client.Authenticator = new SimpleAuthenticator("sessionId", _sessionId, "", "");

            Image = new Image(this._client);
            Album = new Album(this._client);
        }

        /// <summary>
        /// Get a new session id for given user and password.
        /// </summary>
        /// <param name="authUser">The username</param>
        /// <param name="authPassword">The password</param>
        /// <returns>A new session id.</returns>
        /// <remarks>Uses version 1</remarks>
        public static string GetSessionId(string authUser, string authPassword)
        {
            var client = new RestClient(PixumApiBase.BaseURL);
            var request = new RestRequest();

            request.AddParameter(new
            {
                service = "auth",
                module = "session",
                action = "sessionOpen",
                version = "1",
                authUser = authUser,
                authPassword = authPassword
            });
            
            var response = client.Execute<PSIResult<PSISessionId>>(request);
            return response.Data.response.data.sessionId;
        }

        /// <summary>
        /// Creates a login token used for PixumApi.
        /// </summary>
        /// <param name="sessionId">A valid session id created by GetSessionId.</param>
        /// <returns>A new login token</returns>
        public static string GetLoginToken(string sessionId)
        {
            var client = new RestClient(PixumApiBase.BaseURL);
            var request = new RestRequest();

            request.AddParameter(new
            {
                service = "auth",
                module = "session",
                action = "createLoginToken",
                sessionId = sessionId
            });
            
            var response = client.Execute<PSIResult<PSILoginToken>>(request);
            return response.Data.response.data.token;
        }

        /// <summary>
        /// Login the given Pixum user account.
        /// </summary>
        /// <param name="username">The username of the Pixum user.</param>
        /// <param name="password">The password of the Pixum user.</param>
        public PSIUserLogin Login(string username, string password)
        {
            var request = CreateNonRestPSIRequest("auth", "session", "userLogin", 2);

            request.AddParameter(new
            {
                email = username,
                password = (password + _loginToken).GetMd5()
            });

            return Execute<PSIUserLogin>(request);
        }

        /// <summary>
        /// Logout the current Pixum user account.
        /// </summary>
        public Task<List<dynamic>> Logout()
        {
            var request = CreateNonRestPSIRequest("auth", "session", "userLogout");

            return ExecuteAsync<List<dynamic>>(request);
        }
    }
}