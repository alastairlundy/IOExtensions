/*
    IOExtensions 
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

using AlastairLundy.Extensions.IO.Providers.KeyValueProviders.Abstractions;

namespace AlastairLundy.Extensions.IO.Providers.KeyValueProviders
{
    /// <summary>
    /// A class to read and write KeyValue Pairs to/from Text files.
    /// </summary>
    public class TextKeyValueFileProvider : IKeyValueFileProvider
    {
        /// <summary>
        /// Retrieves string Keys and Values stored in a .txt Text File.
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public KeyValuePair<string, string>[] Get(string pathToFile)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string text = File.ReadAllText(pathToFile);

#if NET6_0_OR_GREATER
            string[] lines = text.Split(Environment.NewLine);
#else
            string[] lines = text.Replace(" ", String.Empty).Split(Environment.NewLine.ToCharArray());
#endif
            foreach (string line in lines)
            {
                string[] lineSplit = line.Split('=');
                
                list.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Writes the specified data to a .txt Text file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pathToFile"></param>
        public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in data)
            {
                stringBuilder.AppendLine($"{pair.Key}={pair.Value}");
            }
            
            File.WriteAllText(pathToFile, stringBuilder.ToString());
        }
    }
}