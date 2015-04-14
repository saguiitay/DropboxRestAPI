using System;
using System.Net.Http;
using System.Threading.Tasks;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI
{
    public interface IRequestExecuter
    {
        Task<T> ExecuteAuthorization<T>(IRequest restRequest) where T : new();

        Task<HttpResponseMessage> Execute(Func<IRequest> restRequest, HttpClient restClient = null);

        Task<T> Execute<T>(Func<IRequest> restRequest, HttpClient restClient = null);

        Task<string> CheckForError(HttpResponseMessage httpResponse, bool readResponse = true);
    }
}