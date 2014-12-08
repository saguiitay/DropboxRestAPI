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
using DropboxRestAPI.Models.Business;

namespace DropboxRestAPI.Services.Business
{
    public interface IAuditLog
    {
        /// <summary>
        /// Accesses the Dropbox for Business audit log for a team.
        /// </summary>
        /// <remarks>
        /// If no parameters are specified, the endpoint will return the first 1000 events, along with cursor and has_more, indicating
        /// if there are more events in the log.
        /// If a cursor is provided, only the limit parameter may be (optionally) changed; all other parameters will be based on the
        /// cursor settings.
        /// When a user parameter is included, events will only be returned where that user is the actor. Users can be specified using
        /// an member_id, user_id or email.
        /// When a category parameter is included, only events matching the given category will be returned. Allowed category values
        /// are: logins, passwords, apps, members, devices, team_admin_actions, and sharing.
        /// The name, email, and user values for an event type, along with any entries in an event's info_dict, reflect the state of
        /// those values at the time the event occurred and may not reflect the current state of the Dropbox for Business account.
        /// </remarks>
        /// <remarks>Only only of member_id, user_id, user_email may be specified</remarks>
        /// <param name="limit">number of results to return per call (default 1000, maximum 1000)</param>
        /// <param name="cursor">encoded value indicating from what point to get the next limit logs</param>
        /// <param name="member_id">member ID of a user for filtering events</param>
        /// <param name="user_id">user ID of a user for filtering events</param>
        /// <param name="user_email">email of a user for filtering events</param>
        /// <param name="category">filter the returned events to a single category (see Event types below)</param>
        /// <param name="start_ts">start time </param>
        /// <param name="end_ts">end time </param>
        /// <returns>Audit log for a team.</returns>
        Task<Events> GetEventsAsync(int limit = 1000, string cursor = null, string member_id = null, string user_id = null, string user_email = null, string category = null,
            DateTime? start_ts = null, DateTime? end_ts = null);
    }
}