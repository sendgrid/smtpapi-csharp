using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

            var serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, objectToSerialize);
                string jsonData = Encoding.UTF8.GetString(stream.ToArray(), 0, (int) stream.Length);
                return jsonData; //return EncodeNonAsciiCharacters(jsonData);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="dictionaryToSerialize"></param>
        /// <returns></returns>
        public static string SerializeDictionary(IDictionary<String, String> dictionaryToSerialize)
        {
            return "{" +
                String.Join(",", dictionaryToSerialize.Select(kvp => Serialize(kvp.Key == null ? "" : kvp.Key ) + ":" + Serialize(kvp.Value == null ? "" : kvp.Value))) +
                   "}";
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
                    string encodedValue = "\\u" + ((int) c).ToString("x4");
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
        /// <param name="dateTIme">Date to convert to timestamp</param>
        /// <returns>Timestamp</returns>
        public static Int32 DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var span = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            return (Int32)span.TotalSeconds;
        }
    }
}