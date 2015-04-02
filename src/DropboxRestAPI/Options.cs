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


namespace DropboxRestAPI
{
    public class Options
    {
        public const string AutoRoot = "auto";
        public const string SandboxRoot = "sandbox";
        public const string DropboxRoot = "dropbox";

        public Options()
        {
            ChunkSize = 4*1024*1024;
        }

        /// <summary>
        /// The app's key, found in the App Console.
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// The app's secret, found in the App Console.
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Where to redirect the user after authorization has completed. This must be the exact URI registered in the App Console;
        /// even 'localhost' must be listed if it is used for testing. A redirect URI is required for a token flow, but optional for
        /// code. If the redirect URI is omitted, the code will be presented directly to the user and they will be invited to enter
        /// the information in your app.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// A token which can be used to make calls to the Dropbox API.
        /// </summary>
        /// <remarks>This value is automatically updated when calling the Core.OAuth2.TokenAsync() method</remarks>
        public string AccessToken { get; set; }

        /// <summary>
        /// Maximum number of read requests to perform per second. Allows client-side throttling.
        /// </summary>
        public float? ReadRequestsPerSecond { get; set; }
        /// <summary>
        /// Maximum number of write requests to perform per second. Allows client-side throttling.
        /// </summary>
        public float? WriteRequestsPerSecond { get; set; }


       

        /// <summary>
        /// Chunks can be any size up to 150 MB. A typical chunk is 4 MB. Using large chunks will mean fewer calls to /chunked_upload
        /// and faster overall throughput. However, whenever a transfer is interrupted, you will have to resume at the beginning of
        /// the last chunk, so it is often safer to use smaller chunks.
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// To use Dropbox API in sandbox mode (app folder access) set to true
        /// </summary>
        public bool UseSandbox { get; set; }


        internal string Root
        {
            get { return UseSandbox ? SandboxRoot : DropboxRoot; }
        }
    }
}