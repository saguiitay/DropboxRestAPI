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
using System.Net;
using System.Net.Http;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.Http
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private TimeSpanSemaphore _readTimeSpanSemaphore;
        private TimeSpanSemaphore _writeTimeSpanSemaphore;

        public HttpClient CreateHttpClient(HttpClientOptions options)
        {
            HttpMessageHandler handler = new HttpClientHandler {AllowAutoRedirect = options.AllowAutoRedirect};
            if (((HttpClientHandler) handler).SupportsAutomaticDecompression)
                ((HttpClientHandler) handler).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            if (options.AddTokenToRequests)
                handler = new AccessTokenAuthenticator(options.TokenRetriever, handler);

            TimeSpanSemaphore readTimeSpanSemaphore = null;
            TimeSpanSemaphore writeTimeSpanSemaphore = null;
            if (options.ReadRequestsPerSecond.HasValue)
            {
                double delay = (1/options.ReadRequestsPerSecond.Value);
                readTimeSpanSemaphore = (_readTimeSpanSemaphore ?? (_readTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay))));
            }
            if (options.WriteRequestsPerSecond.HasValue)
            {
                double delay = (1/options.WriteRequestsPerSecond.Value);
                writeTimeSpanSemaphore = (_writeTimeSpanSemaphore ?? (_writeTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay))));
            }

            if (readTimeSpanSemaphore != null || writeTimeSpanSemaphore != null)
            {
                handler = new ThrottlingMessageHandler(readTimeSpanSemaphore, writeTimeSpanSemaphore, handler);
            }
            return new HttpClient(handler);
        }
    }
}