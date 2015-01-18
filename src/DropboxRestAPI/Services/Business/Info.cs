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
using System.Threading.Tasks;
using DropboxRestAPI.Models.Business;
using DropboxRestAPI.RequestsGenerators.Business;

namespace DropboxRestAPI.Services.Business
{
    public class Info : IInfo
    {
        private readonly Options _options;
        private readonly RequestExecuter _requestExecuter;
        private readonly ITeamInfoRequestGenerator _requestGenerator;

        public Info(RequestExecuter requestExecuter, ITeamInfoRequestGenerator requestGenerator, Options options)
        {
            _requestGenerator = requestGenerator;
            _options = options;
            _requestExecuter = requestExecuter;
        }

        public async Task<TeamInfo> GetInfoAsync()
        {
            return await _requestExecuter.Execute<TeamInfo>(() => _requestGenerator.GetInfo()).ConfigureAwait(false);
        }

        public async Task<Models.Business.Members> GetMembersAsync(int limit = 1000, string cursor = null)
        {
            return await _requestExecuter.Execute<Models.Business.Members>(() => _requestGenerator.GetMembers(limit, cursor)).ConfigureAwait(false);
        }

        public async Task<MemberInfo> GetMemberInfoAsync(string member_id = null, string email = null, string external_id = null)
        {
            return await _requestExecuter.Execute<MemberInfo>(() => _requestGenerator.GetMemberInfo(member_id, email, external_id)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<MemberInfo>> GetMembersInfoBatchAsync(string[] member_ids = null, string[] emails = null, string[] external_ids = null)
        {
            return await _requestExecuter.Execute<IEnumerable<MemberInfo>>(() => _requestGenerator.GetMembersInfoBatch(member_ids, emails, external_ids)).ConfigureAwait(false);
        }
    }
}