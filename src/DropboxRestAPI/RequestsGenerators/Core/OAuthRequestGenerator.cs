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
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.RequestsGenerators.Core
{
    public class OAuth2RequestGenerator : IOAuth2RequestGenerator
    {
        public IRequest Authorize(string response_type, string client_id, string redirect_uri, string state = null, bool force_reapprove = false, bool disable_signup = false)
        {
            var request = new Request
            {
                Method = HttpMethod.Get,
                BaseAddress = Consts.ApiBaseUrl,
                Resource = Consts.Version + "/oauth2/authorize"
            };

            request.AddParameter("response_type", response_type);
            request.AddParameter("client_id", client_id);
            request.AddParameter("redirect_uri", redirect_uri);
            request.AddParameter("state", state);
            if (force_reapprove)
                request.AddParameter("force_reapprove", "true");
            if (disable_signup)
                request.AddParameter("disable_signup", "true");

            return request;
        }

        public IRequest AccessToken(string clientId, string clientSecret, string callbackUrl, string authorizationCode)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/oauth2/token"
                };

            request.AddParameter("code", authorizationCode);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("redirect_uri", callbackUrl);

            return request;
        }
    }
}