using System;
using System.Net.Http;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Exceptions;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI
{
    public class AutoRetryRequestExecuter : IRequestExecuter
    {
        private readonly IRequestExecuter _requestExecuter;

        public int NumberOfRetries { get; set; }

        public AutoRetryRequestExecuter(IRequestExecuter requestExecuter)
        {
            _requestExecuter = requestExecuter;

            NumberOfRetries = 3;
        }

        #region Implementation of IRequestExecuter

        public Task<T> ExecuteAuthorization<T>(IRequest restRequest) where T : new()
        {
            return _requestExecuter.ExecuteAuthorization<T>(restRequest);
        }

        public async Task<HttpResponseMessage> Execute(Func<IRequest> restRequest, HttpClient restClient = null)
        {
            for (int i = 0; i < NumberOfRetries; i++)
            {
                try
                {
                    return await _requestExecuter.Execute(restRequest, restClient).ConfigureAwait(false);
                }
                catch (RetryLaterException exception)
                {
                    if (exception.RetryAfter != null)
                        Task.Delay(TimeSpan.FromSeconds((int)exception.RetryAfter.Value)).Wait();
                }
            }

            throw new RetryLaterException();
        }

        public async Task<T> Execute<T>(Func<IRequest> restRequest, HttpClient restClient = null)
        {
            for (int i = 0; i < NumberOfRetries; i++)
            {
                try
                {
                    return await _requestExecuter.Execute<T>(restRequest, restClient).ConfigureAwait(false);
                }
                catch (RetryLaterException exception)
                {
                    if (exception.RetryAfter != null)
                        Task.Delay(TimeSpan.FromSeconds((int)exception.RetryAfter.Value)).Wait();
                }
            }

            throw new RetryLaterException();
        }

        public Task<string> CheckForError(HttpResponseMessage httpResponse, bool readResponse = true)
        {
            return _requestExecuter.CheckForError(httpResponse, readResponse);
            
        }

        #endregion
    }
}