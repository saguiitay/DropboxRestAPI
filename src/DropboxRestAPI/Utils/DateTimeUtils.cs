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
using System.Globalization;

namespace DropboxRestAPI.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetDateTimeFromString(string dateTimeStr)
        {
            //cast to datetime and return
            return dateTimeStr == null ? DateTime.MinValue : DateTime.Parse(dateTimeStr); //RFC1123 format date codes are returned by API
        }

        public static DateTime GetUTCDateTimeFromString(string dateTimeStr)
        {
            string str = dateTimeStr;
            if (str == null)
                return DateTime.MinValue;
            if (str.EndsWith(" +0000")) str = str.Substring(0, str.Length - 6);
            if (!str.EndsWith(" UTC")) str += " UTC";
            return DateTime.ParseExact(str, "ddd, d MMM yyyy HH:mm:ss UTC", CultureInfo.InvariantCulture);
        }

        public static string GetStringFromDateTime(DateTime dateTime)
        {
            return dateTime.ToString("ddd, d MMM yyyy HH:mm:ss UTC");
        }
    }
}