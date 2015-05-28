using System;
using System.IO;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Core;

namespace DropboxRestAPI
{
    public class LargeFileUploader
    {
        public static long BufferSize = 4 * 1024 * 1024;

        private readonly Client _client;
        private readonly Stream _dataSource;

        public LargeFileUploader(Client client, Stream dataSource)
        {
            _client = client;
            _dataSource = dataSource;
        }


        public async Task<MetaData> UploadFileStream(string path, string locale = null, bool overwrite = true, string parent_rev = null, bool autorename = true, string asTeamMember = null)
        {
            string uploadId = null;
            long currentPosition = 0;

            var fragmentBuffer = new byte[BufferSize];
            do
            {
                long endPosition = currentPosition + BufferSize;
                if (endPosition > _dataSource.Length)
                {
                    endPosition = _dataSource.Length;
                }
                var count = await _dataSource.ReadAsync(fragmentBuffer, 0, (int) Math.Max(0, endPosition - currentPosition));

                var response = await _client.Core.Metadata.ChunkedUploadAsync(fragmentBuffer, count, uploadId, currentPosition, asTeamMember);
                uploadId = response.upload_id;

                currentPosition = endPosition;
            } while (currentPosition < _dataSource.Length);

            if (uploadId == null)
                return null;

            var uploadedItem = await _client.Core.Metadata.CommitChunkedUploadAsync(path, uploadId, locale, overwrite, parent_rev, autorename, asTeamMember);
            return uploadedItem;
        }
    }
}