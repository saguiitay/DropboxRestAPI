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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DropboxRestAPI.Models.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DropboxRestAPI.Utils
{
    public class TupleStringMetadataConverter : JsonConverter
    {
        #region Overrides of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<Tuple<string, JRaw>> jlist = ((List<Tuple<string, MetaData>>)value).Select(t => new Tuple<string, JRaw>(t.Item1, new JRaw(t.Item2))).ToList();
            serializer.Serialize(writer, jlist);

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray jarray = JArray.Load(reader);

            var result = new List<Tuple<string, MetaData>>(jarray.Count);
            foreach (JToken j in jarray)
            {
                string item1 = ((JArray)j)[0].ToString();
                var item2 = ((JArray)j)[1].ToObject<MetaData>();
                result.Add(new Tuple<string, MetaData>(item1, item2));
            }
            return result;

        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<Tuple<string, MetaData>>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        #endregion
    }
}