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

namespace DropboxRestAPI.RequestsGenerators.Core
{
    public class FileOperationsRequestGenerator : IFileOperationsRequestGenerator
    {
        public IRequest Copy(string root, string from_path, string to_path, string from_copy_ref = null, string locale = null, string asTeamMember = null)
        {
            if (from_path != null && from_copy_ref != null)
                throw new ArgumentException("Must specify either a from_path or from_copy_ref.");

            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/fileops/copy"
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("root", root);
            request.AddParameter("from_path", from_path);
            request.AddParameter("to_path", to_path);
            request.AddParameter("from_copy_ref", from_copy_ref);
            request.AddParameter("locale", locale);

            return request;
        }

        public IRequest CreateFolder(string root, string path, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Get,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/fileops/create_folder"
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("path", path);
            request.AddParameter("root", root);
            request.AddParameter("locale", locale);

            return request;
        }

        public IRequest Delete(string root, string path, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/fileops/delete"
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("root", root);
            request.AddParameter("path", path);
            request.AddParameter("locale", locale);

            return request;
        }

        public IRequest Move(string root, string from_path, string to_path, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Post,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/fileops/move"
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("root", root);
            request.AddParameter("from_path", from_path);
            request.AddParameter("to_path", to_path);
            request.AddParameter("locale", locale);

            return request;
        }
    }
}