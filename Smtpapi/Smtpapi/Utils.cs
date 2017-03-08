using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendGrid.SmtpApi
{
    /// <summary>
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string Serialize<T>(T objectToSerialize)
        {
            if (objectToSerialize == null)
                throw new ArgumentNullException("A key or value in your X-SMTPAPI header is null.");

            return JsonConvert.SerializeObject(objectToSerialize);
        }

        /// <summary>
        /// </summary>
        /// <param name="dictionaryToSerialize"></param>
        /// <returns></returns>
        public static string SerializeDictionary(IDictionary<string, string> dictionaryToSerialize)
        {
            return JsonConvert.SerializeObject(dictionaryToSerialize);
        }

        /// <summary>
        /// ASCII escapes non-ASCII characters
        /// </summary>
        /// <param name="value">The string to escape</param>
        /// <returns>Escaped string</returns>
        public static string EncodeNonAsciiCharacters(string value)
        {
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert a DateTime to a UNIX Epoch Timestamp
        /// </summary>
        /// <param name="dateTime">Date to convert to timestamp</param>
        /// <returns>Timestamp</returns>
        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var span = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            return (int)span.TotalSeconds;
        }
    }
}