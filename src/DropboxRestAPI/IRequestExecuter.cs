using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI
{
    public interface IRequestExecuter
    {
        Task<T> ExecuteAuthorization<T>(IRequest restRequest, CancellationToken cancellationToken = default(CancellationToken)) where T : new();

        Task<HttpResponseMessage> Execute(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> Execute<T>(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> CheckForError(HttpResponseMessage httpResponse, bool readResponse = true);
    }
}