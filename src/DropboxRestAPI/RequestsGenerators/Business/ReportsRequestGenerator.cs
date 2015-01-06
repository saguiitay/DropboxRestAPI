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
    public class ReportsRequestGenerator : IReportsRequestGenerator
    {
        public IRequest GetStorage(DateTime? start_date, DateTime? end_date)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/reports/get_storage"
                };

            var content = new JObject();

            if (start_date != null)
                content["start_date"] = start_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            if (end_date != null)
                content["end_date"] = end_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest GetActivity(DateTime? start_date, DateTime? end_date)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/reports/get_activity"
                };

            var content = new JObject();

            if (start_date != null)
                content["start_date"] = start_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            if (end_date != null)
                content["end_date"] = end_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest GetMembership(DateTime? start_date, DateTime? end_date)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/reports/get_membership"
                };

            var content = new JObject();

            if (start_date != null)
                content["start_date"] = start_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            if (end_date != null)
                content["end_date"] = end_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            request.Content = new JsonContent(content);

            return request;
        }

        public IRequest GetDevices(DateTime? start_date, DateTime? end_date)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/team/reports/get_devices"
                };

            var content = new JObject();

            if (start_date != null)
                content["start_date"] = start_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            if (end_date != null)
                content["end_date"] = end_date.Value.ToUniversalTime().ToString("yyyy-MM-dd");
            request.Content = new JsonContent(content);

            return request;
        }
    }
}