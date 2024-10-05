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
using System.Xml.Serialization;

using AlastairLundy.Extensions.IO.Providers.KeyValueProviders.Abstractions;

namespace AlastairLundy.Extensions.IO.Providers.KeyValueProviders;

/// <summary>
/// A class to read and write KeyValue Pairs to/from XML files.
/// </summary>
public class XmlKeyValueFileProvider : IKeyValueFileProvider
{
    /// <summary>
    /// Retrieves string Keys and Values stored in an XML File.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <returns></returns>
    public KeyValuePair<string, string>[] Get(string pathToFile)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KeyValuePair<string, string>>));

        KeyValuePair<string, string>[] array;

        using (Stream reader = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
        {
            try
            {
                // Call the Deserialize method to restore the object's state.
                array = xmlSerializer.Deserialize(reader) as KeyValuePair<string, string>[] ?? throw new NullReferenceException();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return array;
    }

    /// <summary>
    /// Writes the specified data to a XML file.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pathToFile"></param>
    public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
    {
            FileStream fileStream = new FileStream(pathToFile, FileMode.Create);
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KeyValuePair<string, string>>));
            xmlSerializer.Serialize(fileStream, data);
    }
}