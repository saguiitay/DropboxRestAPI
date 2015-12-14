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


using DropboxRestAPI.Http;
using DropboxRestAPI.RequestsGenerators;
using DropboxRestAPI.Services.Business;
using DropboxRestAPI.Services.Core;

namespace DropboxRestAPI
{
    public class Client : IClient
    {
        private readonly Options _options;

        public Client(Options options)
            : this(new HttpClientFactory(), new RequestGenerator(), options)
        {
        }

        public Client(IHttpClientFactory clientFactory, IRequestGenerator requestGenerator, Options options)
        {
            _options = options;
            IRequestExecuter requestExecuter = new RequestExecuter(
                clientFactory.CreateHttpClient(new HttpClientOptions
                    {
                        AddTokenToRequests = true,
                        TokenRetriever = () => _options.AccessToken,
                        ReadRequestsPerSecond = options.ReadRequestsPerSecond,
                        WriteRequestsPerSecond = options.WriteRequestsPerSecond,
                    }),
                clientFactory.CreateHttpClient(new HttpClientOptions()));

            if (options.AutoRetry)
            {
                requestExecuter = new AutoRetryRequestExecuter(requestExecuter)
                    {
                        NumberOfRetries = options.NumberOfRetries
                    };
            }

            RequestGenerator = requestGenerator;

            Core = new Core(requestExecuter, requestGenerator.Core, options);
            Business = new Business(requestExecuter, requestGenerator);
        }

        public IRequestGenerator RequestGenerator { get; private set; }

        public string UserAccessToken
        {
            get { return _options.AccessToken; }
            set { _options.AccessToken = value; }
        }

        public ICore Core { get; private set; }
        public IBusiness Business { get; private set; }
    }
}