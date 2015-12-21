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
using System.Net.Http;
using System.Net.Http.Headers;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.RequestsGenerators.Business
{
    public class GroupsRequestGenerator : IGroupsRequestGenerator
    {
        public IRequest GetInfoAsync(IEnumerable<string> group_ids)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/groups/get_info"
                };
            request.Content = new JsonContent(new { group_ids });
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }

        public IRequest ListAsync()
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/groups/list"
                };
            request.Content = new JsonContent(new { });
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return request;
        }
    }
}