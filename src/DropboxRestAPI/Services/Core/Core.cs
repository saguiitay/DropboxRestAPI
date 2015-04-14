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


using DropboxRestAPI.RequestsGenerators.Core;

namespace DropboxRestAPI.Services.Core
{
    public class Core : ICore
    {
        public IOAuth2 OAuth2 { get; private set; }
        public IAccounts Accounts { get; private set; }
        public IMetadata Metadata { get; private set; }
        public IFileOperations FileOperations { get; private set; }

        public Core(IRequestExecuter requestExecuter, ICoreRequestGenerator requestGenerator, Options options)
        {
            OAuth2 = new OAuth2(requestExecuter, requestGenerator.OAuth2, options);
            Accounts = new Accounts(requestExecuter, requestGenerator.Accounts);
            Metadata = new Metadata(requestExecuter, requestGenerator.Metadata, options);
            FileOperations = new FileOperations(requestExecuter, requestGenerator.FileOperations, options);
        }
    }
}