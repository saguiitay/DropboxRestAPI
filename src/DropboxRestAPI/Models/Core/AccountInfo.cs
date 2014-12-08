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


namespace DropboxRestAPI.Models.Core
{
    public class AccountInfo
    {

        /// <summary>
        /// The user's referral link.
        /// </summary>
        public string referral_link { get; set; }
        /// <summary>
        /// The user's two-letter country code, if available.
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// The user's display name.
        /// </summary>
        public string display_name { get; set; }
        /// <summary>
        /// The user's used quota details
        /// </summary>
        public QuotaInfo quota_info { get; set; }
        /// <summary>
        /// The user's unique Dropbox ID.
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// If the user belongs to a team, contains team information. Otherwise, null.
        /// </summary>
        public AccountTeam team { get; set; }
    }
}