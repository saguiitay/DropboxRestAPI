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


using DropboxRestAPI.RequestsGenerators;
using DropboxRestAPI.Services.Business;
using DropboxRestAPI.Services.Core;

namespace DropboxRestAPI
{
    public interface IClient
    {
        /// <summary>
        /// A token which can be used to make calls to the Dropbox API.
        /// </summary>
        /// <remarks>This value is automatically updated when calling the Core.OAuth2.TokenAsync() method</remarks>
        string UserAccessToken { get; set; }

        /// <summary>
        /// Helper class that generated requests to the Dropbox service
        /// </summary>
        IRequestGenerator RequestGenerator { get; }

        /// <summary>
        /// The Core API is the underlying interface for all of our official Dropbox mobile apps and our SDKs. It's the most
        /// direct way to access the API.
        /// </summary>
        ICore Core { get; }

        /// <summary>
        /// 
        /// </summary>
        IBusiness Business { get; }
    }
}