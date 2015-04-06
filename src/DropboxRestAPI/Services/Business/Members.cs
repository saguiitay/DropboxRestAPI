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


using System.Threading.Tasks;
using DropboxRestAPI.Models.Business;
using DropboxRestAPI.RequestsGenerators.Business;

namespace DropboxRestAPI.Services.Business
{
    public class Members : IMembers
    {
        private readonly Options _options;
        private readonly IRequestExecuter _requestExecuter;
        private readonly ITeamMembersRequestGenerator _requestGenerator;

        public Members(IRequestExecuter requestExecuter, ITeamMembersRequestGenerator requestGenerator, Options options)
        {
            _requestGenerator = requestGenerator;
            _options = options;
            _requestExecuter = requestExecuter;
        }

        public async Task<MemberInfo> AddAsync(string member_email, string member_given_name, string member_surname, string member_external_id = null,
            bool? send_welcome_email = null)
        {
            return await _requestExecuter.Execute<MemberInfo>(() => _requestGenerator.Add(member_email, member_given_name, member_surname, member_external_id, send_welcome_email)).ConfigureAwait(false);
        }

        public async Task<MemberInfo> SetProfileAsync(string member_id = null, string external_id = null, string new_email = null,
            string new_external_id = null)
        {
            return await _requestExecuter.Execute<MemberInfo>(() => _requestGenerator.SetProfile(member_id, external_id, new_email, new_external_id)).ConfigureAwait(false);
        }

        public async Task<PermissionInfo> SetPermissionsAsync(string member_id = null, string external_id = null, bool? new_is_admin = null)
        {
            return await _requestExecuter.Execute<PermissionInfo>(() => _requestGenerator.SetPermissions(member_id, external_id, new_is_admin)).ConfigureAwait(false);
        }

        public async Task RemoveAsync(string member_id = null, string external_id = null, string transfer_dest_member_id = null,
            string transfer_admin_member_id = null, bool delete_data = true)
        {
            using (
                var response =
                    await _requestExecuter.Execute(
                            () =>
                                _requestGenerator.Remove(member_id, external_id, transfer_dest_member_id,
                                    transfer_admin_member_id, delete_data)).ConfigureAwait(false))
            {
            }
        }
    }
}