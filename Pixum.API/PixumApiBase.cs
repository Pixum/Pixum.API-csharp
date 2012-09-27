using System;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Pixum.API.Model;

namespace Pixum.API
{
    /// <summary>
    /// Base class for Pixum Api Service classes.
    /// </summary>
    public abstract class PixumApiBase
    {
        protected const string BaseURL = "https://psi.pixum.com/";
        protected IRestClient _client;

        public PixumApiBase(IRestClient client)
        {
            _client = client;
        }

        protected T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<PSIResult<T>>(request);
            var responseException = CheckForException<T>(response);

            if (responseException != null)
            {
                throw responseException;
            }

            return response.Data.response.data;
        }

        protected PSIEmptyResponse Execute(RestRequest request)
        {
            var response = _client.Execute<PSIResult<PSIEmptyResponse>>(request);
            var responseException = CheckForException<PSIEmptyResponse>(response);

            if (responseException != null)
            {
                throw responseException;
            }

            return response.Data.response.data;
        }

        /// <summary>
        /// Executes given request asynchronously.
        /// </summary>
        /// <typeparam name="T">The response type.</typeparam>
        /// <param name="request">The api request.</param>
        /// <returns>The requests result as a Task.</returns>
        protected Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var tcs = new TaskCompletionSource<T>();

            _client.ExecuteAsync<PSIResult<T>>(request, (response) =>
            {
                var responseException = CheckForException<T>(response);

                if (responseException == null)
                {
                    tcs.SetResult(response.Data.response.data);
                }
                else
                {
                    tcs.SetException(responseException);
                }
            });

            return tcs.Task;
        }

        /// <summary>
        /// Checks if an Exception happened during the request and returns it.
        /// </summary>
        /// <typeparam name="T">Response type holded in PSIResult</typeparam>
        /// <param name="response">The request response</param>
        /// <returns>PSIException, RestSharp Exception or null if no exception was found.</returns>
        protected Exception CheckForException<T>(IRestResponse<PSIResult<T>> response)
        {
            if (response.ErrorException != null)
            {
                return response.ErrorException;
            }
            else if (response.Data.response.code != 0)
            {
                return new PSIException(response.Data.response.code, response.Data.response.message);
            }

            return null;
        }

        protected RestRequest CreateNonRestPSIRequest(string service, string module, string action, int version = 1)
        {
            var request = new RestRequest();

            request.DateFormat = @"yyyy-MM-dd HH:mm:ss";
            request.AddParameter(new
            {
                service = service,
                module = module,
                action = action,
                version = version
            });

            return request;
        }

        protected RestRequest CreateRestPSIRequest(string service, string module, int version = 1)
        {
            var request = new RestRequest("/{service}/{module}");

            request.DateFormat = @"yyyy-MM-dd HH:mm:ss";
            request.AddUrlSegment("service", service);
            request.AddUrlSegment("module", module);
            request.AddParameter("version", version);

            return request;
        }
    }
}
