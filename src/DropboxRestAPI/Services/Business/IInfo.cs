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


using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Business;

namespace DropboxRestAPI.Services.Business
{
    public interface IInfo
    {
        /// <summary>
        /// Retrieves information about a team.
        /// </summary>
        /// <returns>Information about a team.</returns>
        Task<TeamInfo> GetInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Lists members of a team.
        /// </summary>
        /// <param name="limit">number of results to return per call (default 1000, maximum 1000)</param>
        /// <param name="cursor">encoded value indicating from what point to get the next limit members</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A list of team members.</returns>
        Task<Models.Business.Members> GetMembersAsync(int limit = 1000, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves information about a team member.
        /// </summary>
        /// <remarks>Exactly one of member_id, email, and external_id must be set.</remarks>
        /// <param name="member_id">ID of member</param>
        /// <param name="email">email of member</param>
        /// <param name="external_id">external ID of member</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Information about a team member.</returns>
        Task<MemberInfo> GetMemberInfoAsync(string member_id = null, string email = null, string external_id = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves information about multiple team members.
        /// </summary>
        /// <remarks>Exactly one of member_id, email, and external_id must be set.</remarks>
        /// <param name="member_ids">list of member IDs</param>
        /// <param name="emails">list of member emails</param>
        /// <param name="external_ids">list of external member IDs</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Information about multiple team members.</returns>
        Task<IEnumerable<MemberInfo>> GetMembersInfoBatchAsync(string[] member_ids = null, string[] emails = null, string[] external_ids = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}