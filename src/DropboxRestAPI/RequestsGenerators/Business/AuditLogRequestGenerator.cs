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
    public class AuditLogRequestGenerator : IAuditLogRequestGenerator
    {
        public IRequest GetEvents(int limit = 1000, string cursor = null, string member_id = null, string user_id = null, string user_email = null, string category = null,
            DateTime? start_ts = null, DateTime? end_ts = null)
        {
            if (member_id != null && (user_id != null || user_email != null))
                throw new ArgumentException("Exactly one of member_id, user_id, and user_email must be set.", "member_id");
            if (user_id != null && (member_id != null || user_email != null))
                throw new ArgumentException("Exactly one of member_id, user_id, and user_email must be set.", "user_id");
            if (user_email != null && (user_id != null || member_id != null))
                throw new ArgumentException("Exactly one of member_id, user_id, and user_email must be set.", "user_email");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/members/set_profile"
                };

            var content = new JObject();

            if (limit != 1000)
                content["limit"] = limit;
            if (cursor != null)
                content["cursor"] = cursor;
            if (category != null)
                content["category"] = category;
            if (start_ts != null)
                content["start_ts"] = ToUnixTime(start_ts.Value.ToUniversalTime());
            if (end_ts != null)
                content["end_ts"] = ToUnixTime(end_ts.Value.ToUniversalTime());

            content["user"] = new JObject();
            if (member_id != null)
                content["user"]["member_id"] = member_id;
            else if (user_id != null)
                content["user"]["user_id"] = user_id;
            else if (user_email != null)
                content["user"]["email"] = user_email;

            request.Content = new JsonContent(content);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        private static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}