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
using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Core;
using DropboxRestAPI.RequestsGenerators.Core;

namespace DropboxRestAPI.Services.Core
{
    public class OAuth2 : IOAuth2
    {
        private readonly IRequestExecuter _requestExecuter;
        private readonly IOAuth2RequestGenerator _requestGenerator;
        private readonly Options _options;

        public OAuth2(IRequestExecuter requestExecuter, IOAuth2RequestGenerator requestGenerator, Options options)
        {
            _requestExecuter = requestExecuter;
            _requestGenerator = requestGenerator;
            _options = options;
        }

        #region Implementation of IOAuth2

        public async Task<Uri> AuthorizeAsync(string response_type, string state = null, bool force_reapprove = false, bool disable_signup = false, string require_role = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = _requestGenerator.Authorize(response_type, _options.ClientId, _options.RedirectUri, state, force_reapprove, disable_signup, require_role);
            return request.BuildUri();
        }

        public async Task<Token> TokenAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
        {
            var token = await _requestExecuter.Execute<Token>(() => _requestGenerator.AccessToken(_options.ClientId, _options.ClientSecret, _options.RedirectUri, code), cancellationToken: cancellationToken).ConfigureAwait(false);

            _options.AccessToken = token.access_token;
            return token;
        }

        #endregion
    }
}