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


using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Core;

namespace DropboxRestAPI.Services.Core
{
    public interface IAccounts
    {
        /// <summary>
        /// Retrieves information about the user's account.
        /// <remarks>https://www.dropbox.com/developers/core/docs#account-info</remarks>
        /// </summary>
        /// <param name="locale">Use to specify language settings for user error messages and other language specific text.</param>
        /// <param name="asTeamMember">Specify the member_id of the user that the app wants to act on.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        /// User account information.
        /// </returns>
        Task<AccountInfo> AccountInfoAsync(string locale = null, string asTeamMember = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}