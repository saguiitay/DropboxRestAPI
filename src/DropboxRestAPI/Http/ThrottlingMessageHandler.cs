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


using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.Http
{
    public class ThrottlingMessageHandler : DelegatingHandler
    {
        private readonly TimeSpanSemaphore _readTimeSpanSemaphore;
        private readonly TimeSpanSemaphore _writeTimeSpanSemaphore;

        public ThrottlingMessageHandler(TimeSpanSemaphore readTimeSpanSemaphore, TimeSpanSemaphore writeTimeSpanSemaphore)
            : this(readTimeSpanSemaphore, writeTimeSpanSemaphore, null)
        {
        }

        public ThrottlingMessageHandler(TimeSpanSemaphore readTimeSpanSemaphore, TimeSpanSemaphore writeTimeSpanSemaphore, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            _readTimeSpanSemaphore = readTimeSpanSemaphore;
            _writeTimeSpanSemaphore = writeTimeSpanSemaphore;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get ||
                request.Method == HttpMethod.Head ||
                request.Method == HttpMethod.Options)
            {
                if (_readTimeSpanSemaphore != null)
                    return _readTimeSpanSemaphore.RunAsync(base.SendAsync, request, cancellationToken);

                return base.SendAsync(request, cancellationToken);
            }

            if (_writeTimeSpanSemaphore != null)
                return _writeTimeSpanSemaphore.RunAsync(base.SendAsync, request, cancellationToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}