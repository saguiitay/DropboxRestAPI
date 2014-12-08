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
using System.Net.Http;
using DropboxRestAPI.Utils;
using Newtonsoft.Json.Linq;

namespace DropboxRestAPI.RequestsGenerators.Business
{
    public class TeamMembersRequestGenerator : ITeamMembersRequestGenerator
    {
        public IRequest Add(string member_email, string member_given_name, string member_surname,
            string member_external_id = null, bool? send_welcome_email = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/add"
                };

            var content = new
                {
                    member_email,
                    member_given_name,
                    member_surname,
                    member_external_id,
                    send_welcome_email
                };
            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest SetProfile(string member_id = null, string external_id = null, string new_email = null,
            string new_external_id = null)
        {
            if (member_id != null && external_id != null)
                throw new ArgumentException("Must specify either a member_id or external_id.");
            if (new_email != null && new_external_id != null)
                throw new ArgumentException("Must specify either a new_email or new_external_id.");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/set_profile"
                };

            var content = new JObject();

            if (member_id != null)
                content["member_id"] = member_id;
            else if (external_id != null)
                content["external_id"] = external_id;
            if (new_email != null)
                content["new_email"] = new_email;
            else if (new_external_id != null)
                content["new_external_id"] = new_external_id;

            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest SetPermissions(string member_id = null, string external_id = null, bool? new_is_admin = null)
        {
            if (member_id != null && external_id != null)
                throw new ArgumentException("Must specify either a member_id or external_id.");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/set_permissions"
                };

            var content = new JObject();

            if (member_id != null)
                content["member_id"] = member_id;
            else if (external_id != null)
                content["external_id"] = external_id;
            if (new_is_admin != null)
                content["new_is_admin"] = new_is_admin;

            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest Remove(string member_id = null, string external_id = null, string transfer_dest_member_id = null,
            string transfer_admin_member_id = null, bool delete_data = true)
        {
            if (member_id != null && external_id != null)
                throw new ArgumentException("Must specify either a member_id or external_id.");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/remove"
                };

            var content = new JObject();

            if (member_id != null)
                content["member_id"] = member_id;
            else if (external_id != null)
                content["external_id"] = external_id;
            if (transfer_dest_member_id != null)
                content["transfer_dest_member_id"] = transfer_dest_member_id;
            if (transfer_admin_member_id != null)
                content["transfer_admin_member_id"] = transfer_admin_member_id;
            if (delete_data != true)
                content["delete_data"] = false;

            request.Content = new JsonContent(content);

            return request;
        }
    }
}