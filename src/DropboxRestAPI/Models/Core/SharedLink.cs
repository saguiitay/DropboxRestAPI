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

namespace DropboxRestAPI.Models.Core
{
    public class SharedLink
    {
        /// <summary>
        /// The actual shared link URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// The link's expiration time
        /// </summary>
        public DateTime expires { get; set; }
        /// <summary>
        /// Indicates what (if any) restrictions are set on this particular link. Possible values include: "PUBLIC" (anyone can view),
        /// "TEAM_ONLY" (only the owner's team can view), "PASSWORD" (a password is required), "TEAM_AND_PASSWORD" (a combination of
        /// "TEAM_ONLY" and "PASSWORD" restrictions), or "SHARED_FOLDER_ONLY" (only members of the enclosing shared folder can view).
        /// <remarks>Note that other values may be added at any time.</remarks>
        /// </summary>
        public string visibility { get; set; }
    }
}