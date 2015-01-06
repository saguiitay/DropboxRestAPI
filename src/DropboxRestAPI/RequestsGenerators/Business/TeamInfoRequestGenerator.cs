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
using System.Net.Http.Headers;
using DropboxRestAPI.Utils;
using Newtonsoft.Json.Linq;

namespace DropboxRestAPI.RequestsGenerators.Business
{
    public class TeamInfoRequestGenerator : ITeamInfoRequestGenerator
    {
        public IRequest GetInfo()
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/get_info"
                };
            request.Content = new JsonContent(new {});
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        public IRequest GetMembers(int limit = 1000, string cursor = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/list"
                };

            var content = new JObject();

            if (limit != 1000)
                content["limit"] = limit;
            if (cursor != null)
                content["cursor"] = cursor;

            request.Content = new JsonContent(content);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            return request;
        }

        public IRequest GetMemberInfo(string member_id = null, string email = null, string external_id = null)
        {
            if (member_id != null && (email != null || external_id != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "member_id");
            if (email != null && (member_id != null || external_id != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "email");
            if (external_id != null && (email != null || member_id != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "external_id");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/get_info"
                };

            if (member_id != null)
                request.Content = new JsonContent(new {member_id});
            else if (email != null)
                request.Content = new JsonContent(new {email});
            else if (external_id != null)
                request.Content = new JsonContent(new {external_id});

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        public IRequest GetMembersInfoBatch(string[] member_ids = null, string[] emails = null, string[] external_ids = null)
        {
            if (member_ids != null && (emails != null || external_ids != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "member_ids");
            if (emails != null && (member_ids != null || external_ids != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "emails");
            if (external_ids != null && (emails != null || member_ids != null))
                throw new ArgumentException("Exactly one of member_id, email, and external_id must be set.", "external_ids");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/get_info_batch"
                };

            if (member_ids != null)
                request.Content = new JsonContent(new {member_ids});
            else if (emails != null)
                request.Content = new JsonContent(new {emails});
            else if (external_ids != null)
                request.Content = new JsonContent(new {external_ids});

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }
    }
}