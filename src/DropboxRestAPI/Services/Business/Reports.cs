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
using DropboxRestAPI.RequestsGenerators.Business;

namespace DropboxRestAPI.Services.Business
{
    public class Reports : IReports
    {
        private readonly Options _options;
        private readonly IRequestExecuter _requestExecuter;
        private readonly IReportsRequestGenerator _requestGenerator;

        public Reports(IRequestExecuter requestExecuter, IReportsRequestGenerator requestGenerator, Options options)
        {
            _requestGenerator = requestGenerator;
            _options = options;
            _requestExecuter = requestExecuter;
        }

        public async Task<StorageInfo> GetStorageAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _requestExecuter.Execute<StorageInfo>(() => _requestGenerator.GetStorage(start_date, end_date), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ActivityInfo> GetActivityAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _requestExecuter.Execute<ActivityInfo>(() => _requestGenerator.GetActivity(start_date, end_date), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<MembershipInfo> GetMembershipAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _requestExecuter.Execute<MembershipInfo>(() => _requestGenerator.GetMembership(start_date, end_date), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<DevicesInfo> GetDevicesAsync(DateTime? start_date, DateTime? end_date, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _requestExecuter.Execute<DevicesInfo>(() => _requestGenerator.GetDevices(start_date, end_date), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}