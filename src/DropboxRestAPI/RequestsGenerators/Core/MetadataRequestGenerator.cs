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


using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.RequestsGenerators.Core
{
    public class MetadataRequestGenerator : IMetadataRequestGenerator
    {
        public IRequest Files(string root, string path, string rev = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/files/" + root + path,
                    Method = HttpMethod.Head
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            if (!string.IsNullOrEmpty(rev))
                request.AddParameter("rev", rev);

            return request;
        }

        public IRequest FilesRange(string root, string path, long @from, long to, string etag, string rev = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/files/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            if (to < 0)
                to = 0;

            request.AddHeader("Range", string.Format("bytes={0}-{1}", from, to));
            request.AddHeader("ETag", etag);

            if (!string.IsNullOrEmpty(rev))
                request.AddParameter("rev", rev);

            return request;
        }

        public IRequest FilesPut(string root, Stream content, string path, string locale = null, bool overwrite = true, string parent_rev = null, bool autorename = true, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/files_put/" + root + path,
                    Method = HttpMethod.Put
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddHeader("locale", locale);
            request.AddParameter("parent_rev", parent_rev);
            if (!overwrite)
                request.AddParameter("overwrite", false.ToString());
            if (!autorename)
                request.AddParameter("autorename", false.ToString());

            var httpContent = new StreamContent(content);
            request.Content = httpContent;


            return request;
        }

        public IRequest Metadata(string root, string path, int file_limit = 10000, string hash = null, bool list = true,
            bool include_deleted = false, string rev = null, string locale = null, bool include_media_info = false,
            bool include_membership = false, string asTeamMember = null)
        {
            var request = new Request
                {
                    Method = HttpMethod.Get,
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/metadata/" + root + path
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("hash", hash);
            request.AddParameter("rev", rev);
            request.AddParameter("locale", locale);

            if (file_limit != 10000)
                request.AddParameter("file_limit", file_limit.ToString());
            if (list != true)
                request.AddParameter("list", false.ToString());
            if (include_deleted)
                request.AddParameter("include_deleted", true.ToString());
            if (include_media_info)
                request.AddParameter("include_media_info", true.ToString());
            if (include_membership)
                request.AddParameter("include_membership", true.ToString());

            return request;
        }

        public IRequest Delta(string cursor = null, string locale = null, string path_prefix = null, bool include_media_info = false,
            string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/delta",
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("cursor", cursor);
            request.AddHeader("locale", locale);
            request.AddParameter("path_prefix", path_prefix);
            if (include_media_info)
                request.AddParameter("include_media_info", true.ToString());

            return request;
        }

        public IRequest DeltaLatestCursor(string path_prefix = null, bool include_media_info = false, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/delta/latest_cursor",
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("path_prefix", path_prefix);
            if (include_media_info)
                request.AddParameter("include_media_info", true.ToString());

            return request;
        }

        public IRequest LongPollDelta(string cursor = null, int timeout = 30, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiNotifyBaseUrl,
                    Resource = Consts.Version + "/longpoll_delta",
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("cursor", cursor);
            if (timeout != 30)
                request.AddParameter("timeout", timeout.ToString());

            return request;
        }

        public IRequest Revisions(string root, string path, int rev_limit = 10, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/revisions/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("locale", locale);
            if (rev_limit != 10)
                request.AddParameter("rev_limit", rev_limit.ToString());

            return request;
        }

        public IRequest Restore(string root, string path, string rev = null, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/restore/" + root + path,
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("rev", rev);
            request.AddParameter("locale", locale);

            return request;
        }

        public IRequest Search(string root, string path, string query, int file_limit = 1000, bool include_deleted = false,
            string locale = null, bool include_membership = false, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/search/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("query", query);
            request.AddParameter("locale", locale);
            if (include_deleted)
                request.AddParameter("include_deleted", true.ToString());
            if (include_membership)
                request.AddParameter("include_membership", true.ToString());
            if (file_limit != 1000)
                request.AddParameter("file_limit", file_limit.ToString());

            return request;
        }

        public IRequest Shares(string root, string path, string locale = null, bool short_url = true, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/shares/" + root + path,
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("locale", locale);
            if (!short_url)
                request.AddParameter("short_url", false.ToString());

            return request;
        }

        public IRequest Media(string root, string path, string locale = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/media/" + root + path,
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("locale", locale);

            return request;
        }

        public IRequest CopyRef(string root, string path, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/copy_ref/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            return request;
        }

        public IRequest Thumbnails(string root, string path, string format = "jpeg", string size = "s", string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/thumbnails/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("format", format);
            request.AddParameter("size", size);

            return request;
        }

        public IRequest Previews(string root, string path, string rev = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/previews/" + root + path,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("rev", rev);

            return request;
        }


        public IRequest ChunkedUpload(byte[] content, int count, string uploadId = null, long? offset = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/chunked_upload/",
                    Method = HttpMethod.Put
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("upload_id", uploadId);
            if (offset != null)
                request.AddParameter("offset", offset.ToString());

            HttpContent httpContent = new ByteArrayContent(content, 0, count);
            request.Content = httpContent;

            return request;
        }

        public IRequest CommitChunkedUpload(string root, string path, string uploadId, string locale = null, bool overwrite = true,
            string parent_rev = null, bool autorename = true, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiContentBaseUrl,
                    Resource = Consts.Version + "/commit_chunked_upload/" + root + path,
                    Method = HttpMethod.Post
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            request.AddParameter("upload_id", uploadId);
            request.AddParameter("locale", locale);
            request.AddParameter("overwrite", overwrite.ToString());
            request.AddParameter("parent_rev", parent_rev);
            if (!autorename)
                request.AddParameter("autorename", false.ToString());


            return request;
        }

        public IRequest SharedFolders(string id = null, string asTeamMember = null)
        {
            var request = new Request
                {
                    BaseAddress = Consts.ApiBaseUrl,
                    Resource = Consts.Version + "/shared_folders/" + id,
                    Method = HttpMethod.Get
                };
            request.AddHeader(Consts.AsTeamMemberHeader, asTeamMember);

            return request;
        }
    }
}