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
using DropboxRestAPI.Models.Core;
using DropboxRestAPI.RequestsGenerators.Core;

namespace DropboxRestAPI.Services.Core
{
    public class FileOperations : IFileOperations
    {
        private readonly IRequestExecuter _requestExecuter;
        private readonly IFileOperationsRequestGenerator _requestGenerator;
        private readonly Options _options;

        public FileOperations(IRequestExecuter requestExecuter, IFileOperationsRequestGenerator requestGenerator, Options options)
        {
            _requestExecuter = requestExecuter;
            _requestGenerator = requestGenerator;
            _options = options;
        }


        #region Implementation of IFileOperations

        public async Task<MetaData> CopyAsync(string from_path, string to_path, string from_copy_ref = null, string locale = null, string asTeamMember = null)
        {
            return await _requestExecuter.Execute<MetaData>(() => _requestGenerator.Copy(_options.Root, from_path, to_path, from_copy_ref, locale, asTeamMember)).ConfigureAwait(false);
        }

        public async Task<MetaData> CreateFolderAsync(string path, string locale = null, string asTeamMember = null)
        {
            return await _requestExecuter.Execute<MetaData>(() => _requestGenerator.CreateFolder(_options.Root, path, locale, asTeamMember)).ConfigureAwait(false);
        }

        public async Task<MetaData> DeleteAsync(string path, string locale = null, string asTeamMember = null)
        {
            return await _requestExecuter.Execute<MetaData>(() => _requestGenerator.Delete(_options.Root, path, locale, asTeamMember)).ConfigureAwait(false);
        }

        public async Task<MetaData> MoveAsync(string from_path, string to_path, string locale = null, string asTeamMember = null)
        {
            return await _requestExecuter.Execute<MetaData>(() => _requestGenerator.Move(_options.Root, from_path, to_path, locale, asTeamMember)).ConfigureAwait(false);
        }

        #endregion
    }
}