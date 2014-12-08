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
using System.Net.Http;

namespace DropboxRestAPI.Utils
{
    public interface IRequest
    {
        HttpMethod Method { get; }
        HttpContent Content { get; set; }
        Dictionary<string, string> Headers { get; }

        Uri BuildUri();
    }

    public class Request : IRequest
    {
        private readonly HttpValueCollection _query = new HttpValueCollection();

        public string BaseAddress { get; set; }
        public string Resource { get; set; }
        public HttpMethod Method { get; set; }

        public HttpContent Content { get; set; }
        public Dictionary<string, string> Headers { get; private set; }

        public Uri BuildUri()
        {
            var uriBuilder = new UriBuilder(BaseAddress);
            if (string.IsNullOrEmpty(uriBuilder.Path))
                uriBuilder.Path = Resource;
            else
            {
                if (uriBuilder.Path.EndsWith("/"))
                    uriBuilder.Path += Resource.TrimStart('/');
                else
                    uriBuilder.Path += Resource;
            }
            uriBuilder.Query = _query.ToQueryString(false);

            return uriBuilder.Uri;
        }


        public void AddParameter(string key, string value)
        {
            if (value == null)
                return;

            _query[key] = value;
        }

        public void AddHeader(string name, string value)
        {
            if (value == null)
                return;
            if (Headers == null)
                Headers = new Dictionary<string, string>();

            Headers.Add(name, value);
        }
    }
}