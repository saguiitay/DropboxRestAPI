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
using DropboxRestAPI.Models.Business;

namespace DropboxRestAPI.Services.Business
{
    public interface IReports
    {
        /// <summary>
        /// Retrieves reporting data about a team's storage usage.
        /// </summary>
        /// <remarks>
        /// start_date defaults to the earliest date tracked (current maximum is 6 months in the past), and end_date defaults to
        /// the current date.
        /// Data is not available for the current day (partitioned on midnight UTC); there is an indeterminate delay as data is
        /// compiled before it is available for the previous day.
        /// Arrays may terminate with null if data is not yet available for the last day.
        /// </remarks>
        /// <param name="start_date">starting date (inclusive)</param>
        /// <param name="end_date">ending date (exclusive)</param>
        /// <returns>
        /// Data about a team's storage usage. Each result is represented as an array ordered by date, where the first value
        /// corresponds to the data on the start_date. Each subsequent array value represents one day's data in that series.
        /// </returns>
        Task<StorageInfo> GetStorageAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves reporting data about a team's user activity.
        /// </summary>
        /// <remarks>
        /// start_date defaults to the earliest date tracked (current maximum is 6 months in the past), and end_date defaults to the
        /// current date.
        /// Data is not available for the current day (partitioned on midnight UTC); there is an indeterminate delay as data is
        /// compiled before it is available for the previous day.
        /// Arrays may terminate with null if data is not yet available for the last day.
        /// </remarks>
        /// <param name="start_date">starting date (inclusive)</param>
        /// <param name="end_date">ending date (exclusive)</param>
        /// <returns>
        /// Data about a team's user activity. Each result is represented as an array ordered by date, where the first value
        /// corresponds to the data on the start_date. Each subsequent array value represents one day's data in that series.
        /// </returns>
        Task<ActivityInfo> GetActivityAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves reporting data about a team's membership.
        /// </summary>
        /// <remarks>
        /// start_date defaults to the earliest date tracked (current maximum is 6 months in the past), and end_date defaults to the
        /// current date.
        /// Data is not available for the current day (partitioned on midnight UTC); there is an indeterminate delay as data is
        /// compiled before it is available for the previous day.
        /// Arrays may terminate with null if data is not yet available for the last day.
        /// </remarks>
        /// <param name="start_date">starting date (inclusive)</param>
        /// <param name="end_date">ending date (exclusive)</param>
        /// <returns>
        /// Data about a team's membership. Each result is represented as an array ordered by date, where the first value corresponds
        /// to the data on the start_date. Each subsequent array value represents one day's data in that series.
        /// </returns>
        Task<MembershipInfo> GetMembershipAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves reporting data about a team's linked devices.
        /// </summary>
        /// <remarks>
        /// start_date defaults to the earliest date tracked (current maximum is 6 months in the past), and end_date defaults to the
        /// current date.
        /// Data is not available for the current day (partitioned on midnight UTC); there is an indeterminate delay as data is compiled
        /// before it is available for the previous day.
        /// Arrays may terminate with null if data is not yet available for the last day.
        /// </remarks>
        /// <param name="start_date">starting date (inclusive)</param>
        /// <param name="end_date">ending date (exclusive)</param>
        /// <returns>
        /// Data about a team's linked devices. Linked devices are those with a Dropbox client or mobile app installed and linked to
        /// a team member's Dropbox for Business account. Each result is represented as an array ordered by date, where the first
        /// value corresponds to the data on the start_date. Each subsequent array value represents one day's data in that series.
        /// </returns>
        Task<DevicesInfo> GetDevicesAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken));
    }
}