using System;
using System.Net.Http;
using System.Threading;
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

        public Task<T> ExecuteAuthorization<T>(IRequest restRequest, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            return _requestExecuter.ExecuteAuthorization<T>(restRequest, cancellationToken);
        }

        public async Task<HttpResponseMessage> Execute(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            for (int i = 0; i < NumberOfRetries; i++)
            {
                try
                {
                    return await _requestExecuter.Execute(restRequest, restClient, cancellationToken).ConfigureAwait(false);
                }
                catch (RetryLaterException exception)
                {
                    if (exception.RetryAfter != null)
                        Task.Delay(TimeSpan.FromSeconds((int)exception.RetryAfter.Value), cancellationToken).Wait(cancellationToken);
                }
            }

            throw new RetryLaterException();
        }

        public async Task<T> Execute<T>(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            for (int i = 0; i < NumberOfRetries; i++)
            {
                try
                {
                    return await _requestExecuter.Execute<T>(restRequest, restClient, cancellationToken).ConfigureAwait(false);
                }
                catch (RetryLaterException exception)
                {
                    if (exception.RetryAfter != null)
                        Task.Delay(TimeSpan.FromSeconds((int)exception.RetryAfter.Value), cancellationToken).Wait(cancellationToken);
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