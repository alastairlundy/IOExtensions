/*
    KeyValueProvider IO Extensions
    Copyright (c) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace AlastairLundy.Extensions.IO.KeyValueProviders.Providers.Strings
{
    /// <summary>
    /// A class to read and write KeyValue pairs to/from JSON files.
    /// </summary>
    public class JsonKeyValueFileProvider : IStringKeyValueFileProvider
    {
        /// <summary>
        /// Retrieves string Keys and Values stored in a Json File.
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public KeyValuePair<string, string>[] Get(string pathToFile)
        {
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllText(pathToFile));

#if NETSTANDARD2_1 || NET6_0_OR_GREATER
                KeyValuePair<string, string>[]? data = jsonDocument.Deserialize<KeyValuePair<string, string>[]>();
#elif NETSTANDARD2_0
                KeyValuePair<string, string>[] data = jsonDocument.Deserialize<KeyValuePair<string, string>[]>();
#endif

                if (data == null)
                {
                    throw new NullReferenceException($"{nameof(data)} is null");
                }

                return data;  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Writes the specified data to a Json file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pathToFile"></param>
        public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
        {
            string json = JsonSerializer.Serialize(data);
            
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append('{');
            stringBuilder.AppendLine();
            
            foreach (KeyValuePair<string, string> pair in data)
            {
                stringBuilder.Append('"');
                stringBuilder.Append(pair.Key);
                stringBuilder.Append('"');
                
                stringBuilder.Append(' ');
                stringBuilder.Append(':');
                stringBuilder.Append(' ');

                stringBuilder.Append('"');
                stringBuilder.Append(pair.Value);
                stringBuilder.Append('"');
                stringBuilder.Append(',');
                stringBuilder.AppendLine();
            }

            stringBuilder.Append('}');
            stringBuilder.AppendLine();
            
            File.WriteAllText(pathToFile, stringBuilder.ToString());
        }
    }
}
