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
using System.Threading.Tasks;
using DropboxRestAPI.Models.Core;

namespace DropboxRestAPI.Services.Core
{
    public interface IOAuth2
    {
        /// <summary>
        /// This starts the OAuth 2.0 authorization flow. This isn't an API call — it returned the address to the web page that
        /// lets the user sign in to Dropbox and authorize your app. After the user decides whether or not to authorize your app,
        /// they will be redirected to the URI specified by redirect_uri.
        /// OAuth 2.0 supports two authorization flows:
        /// - The code flow returns a code via the redirect_uri callback which should then be converted into a bearer token using
        ///   the /oauth2/token call. This is the recommended flow for apps that are running on a server.
        /// - The token or implicit grant flow returns the bearer token via the redirect_uri callback, rather than requiring your
        ///   app to make a second call to a server. This is useful for pure client-side apps, such as mobile apps or JavaScript-based
        ///   apps.
        /// </summary>
        /// <param name="response_type">The grant type requested, either token or code.</param>
        /// <param name="state">
        /// Up to 200 bytes of arbitrary data that will be passed back to your redirect URI. This parameter should be used to
        /// protect against cross-site request forgery (CSRF). See Sections 4.4.1.8 and 4.4.2.5 of the OAuth 2.0 threat model spec.
        /// </param>
        /// <param name="force_reapprove">
        /// Whether or not to force the user to approve the app again if they've already done so. If false (default), a user who
        /// has already approved the application may be automatically redirected to the URI specified by redirect_uri. If true,
        /// the user will not be automatically redirected and will have to approve the app again.
        /// </param>
        /// <param name="disable_signup">
        /// When true (default is false) users will not be able to sign up for a Dropbox account via the authorization page. Instead,
        /// the authorization page will show a link to the Dropbox iOS app in the App Store. This is only intended for use when
        /// necessary for compliance with App Store policies.
        /// </param>
        /// <returns>
        /// Address to the web page that lets the user sign in to Dropbox and authorize your app
        /// </returns>
        Task<Uri> AuthorizeAsync(string response_type, string state = null, bool force_reapprove = false, bool disable_signup = false);

        /// <summary>
        /// This endpoint only applies to apps using the authorization code flow. An app calls this endpoint to acquire a bearer
        /// token once the user has authorized the app.
        /// </summary>
        /// <param name="code">The code acquired by directing users to /oauth2/authorize?response_type=code.</param>
        /// <returns>
        /// An access token (access_token), token type (token_type), and Dropbox user ID (uid). The token type will always be "bearer".
        /// </returns>
        Task<Token> TokenAsync(string code);
    }
}