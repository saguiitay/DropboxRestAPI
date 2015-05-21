/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2014 Itay Sagui
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Models;
using DropboxRestAPI.Models.Exceptions;
using DropboxRestAPI.Utils;
using Newtonsoft.Json;

namespace DropboxRestAPI
{
    public class RequestExecuter : IRequestExecuter
    {
        private readonly HttpClient _clientContent;
        private readonly HttpClient _clientOAuth;

        public RequestExecuter(HttpClient clientContent, HttpClient clientOAuth)
        {
            _clientContent = clientContent;
            _clientOAuth = clientOAuth;
        }

        #region Request execution utilities methods

        public async Task<T> ExecuteAuthorization<T>(IRequest restRequest, CancellationToken cancellationToken = default(CancellationToken)) where T : new()
        {
            return await Execute<T>(() => restRequest, _clientOAuth, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Execute(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (restClient == null)
                restClient = _clientContent;

            using (IRequest request = restRequest())
            {
                HttpResponseMessage restResponse = null;
                try
                {
                    restResponse = await restClient.Execute(request, cancellationToken).ConfigureAwait(false);
                    await CheckForError(restResponse, false).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    if (restResponse != null)
                        restResponse.Dispose();

                    throw;
                }

                return restResponse;
            }
        }

        public async Task<T> Execute<T>(Func<IRequest> restRequest, HttpClient restClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (restClient == null)
                restClient = _clientContent;

            using (IRequest request = restRequest())
            using (HttpResponseMessage restResponse = await restClient.Execute(request, cancellationToken).ConfigureAwait(false))
            {
                string content = await CheckForError(restResponse).ConfigureAwait(false);

                var data = JsonConvert.DeserializeObject<T>(content);

                return data;
            }
        }

        public async Task<string> CheckForError(HttpResponseMessage httpResponse, bool readResponse = true)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            string content = null;

            if (readResponse)
                content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (statusCode == 0)
                throw new HttpException((int) statusCode, content) {Attempts = 1};

            if ((int)statusCode == 429 || statusCode == HttpStatusCode.ServiceUnavailable)
            {
                if (httpResponse.Headers != null)
                {
                    KeyValuePair<string, IEnumerable<string>> retryAfter = httpResponse.Headers.FirstOrDefault(x => x.Key == "Retry-After");
                    if (retryAfter.Value != null && retryAfter.Value.Any())
                    {
                        throw new RetryLaterException
                            {
                                RetryAfter = decimal.Parse(retryAfter.Value.First(), CultureInfo.InvariantCulture)
                            };
                    }
                }
                if ((int)statusCode == 429)
                    throw new RetryLaterException {RetryAfter = 10};
            }
            if ((int) statusCode == 507)
            {
                throw new NotEnoughQuotaException();
            }
            if (statusCode == HttpStatusCode.Unauthorized ||
                statusCode == HttpStatusCode.Forbidden ||
                statusCode == HttpStatusCode.BadRequest ||
                statusCode == HttpStatusCode.ServiceUnavailable)
            {
                Error errorInfo = null;

                // Force reading response content in order to retrieve the error message
                if (!readResponse)
                    content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!string.IsNullOrEmpty(content))
                    errorInfo = JsonConvert.DeserializeObject<Error>(content);
                if (errorInfo == null || errorInfo.error == null)
                    throw new HttpException((int) statusCode, content) {Attempts = 1};

                throw new ServiceErrorException((int) statusCode, errorInfo.error);
            }
            if (statusCode == HttpStatusCode.InternalServerError ||
                statusCode == HttpStatusCode.BadGateway)
            {
                throw new HttpException((int) statusCode, content) {Attempts = int.MaxValue};
            }

            return content;
        }

        #endregion
    }
}