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
using DropboxRestAPI.Utils;

namespace DropboxRestAPI.RequestsGenerators.Core
{
    public interface IMetadataRequestGenerator
    {
        IRequest Files(string root, string path, string rev = null, string asTeamMember = null);
        IRequest FilesRange(string root, string path, long from, long to, string etag, string rev = null, string asTeamMember = null);

        IRequest FilesPut(string root, Stream content, string path, string locale = null, bool overwrite = true, string parent_rev = null, bool autorename = true, string asTeamMember = null);
        IRequest Metadata(string root, string path, int file_limit = 10000, string hash = null, bool list = true, bool include_deleted = false, string rev = null, string locale = null, bool include_media_info = false, bool include_membership = false, string asTeamMember = null);
        IRequest Delta(string cursor = null, string locale = null, string path_prefix = null, bool include_media_info = false, string asTeamMember = null);
        IRequest DeltaLatestCursor(string path_prefix = null, bool include_media_info = false, string asTeamMember = null);
        IRequest LongPollDelta(string cursor = null, int timeout = 30, string asTeamMember = null);
        IRequest Revisions(string root, string path, int rev_limit = 10, string locale = null, string asTeamMember = null);
        IRequest Restore(string root, string path, string rev = null, string locale = null, string asTeamMember = null);
        IRequest Search(string root, string path, string query, int file_limit = 1000, bool include_deleted = false, string locale = null, bool include_membership = false, string asTeamMember = null);
        IRequest Shares(string root, string path, string locale = null, bool short_url = true, string asTeamMember = null);
        IRequest Media(string root, string path, string locale = null, string asTeamMember = null);
        IRequest CopyRef(string root, string path, string asTeamMember = null);
        IRequest Thumbnails(string root, string path, string format = "jpeg", string size = "s", string asTeamMember = null);
        IRequest Previews(string root, string path, string rev = null, string asTeamMember = null);

        IRequest ChunkedUpload(byte[] content, int count, string uploadId = null, long? offset = null, string asTeamMember = null);
        IRequest CommitChunkedUpload(string root, string path, string uploadId, string locale = null, bool overwrite = true, string parent_rev = null, bool autorename = true, string asTeamMember = null);

        IRequest SharedFolders(string id = null, string asTeamMember = null);

    }
}