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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

using AlastairLundy.Extensions.IO.Providers.KeyValueProviders.Abstractions;

namespace AlastairLundy.Extensions.IO.Providers.KeyValueProviders;

/// <summary>
/// A class to read and write KeyValue pairs to/from Resource files.
/// </summary>
public class ResourceKeyValueFileProvider : IKeyValueFileProvider
{
    /// <summary>
    /// Retrieves string Keys and Values stored in a Resource File.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public KeyValuePair<string, string>[] Get(string pathToFile)
    {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string baseName = pathToFile;

            if (pathToFile.EndsWith(".resw"))
            {
                baseName = pathToFile.Replace(".resw", string.Empty);
            }
            if (pathToFile.EndsWith(".resx"))
            {
                baseName = pathToFile.Replace(".resx", string.Empty);
            }
            if (pathToFile.EndsWith(".resources"))
            {
                baseName = pathToFile.Replace(".resources", string.Empty);
            }
        
            ResourceManager resourceManager = new ResourceManager(baseName, Assembly.GetEntryAssembly() ?? throw new NullReferenceException("Entry Assembly was null."));
            
            ResourceReader reader = new ResourceReader(resourceManager.GetStream(pathToFile) ?? throw new NullReferenceException());

            IDictionaryEnumerator readerEnumerator = reader.GetEnumerator();
            using var readerEnumerator1 = readerEnumerator as IDisposable;

            while (readerEnumerator.MoveNext())
            {
                list.Add(new KeyValuePair<string, string>((string)readerEnumerator.Key,
                    (string)readerEnumerator
                        .Value));
            }
            
            readerEnumerator.Reset();
            reader.Dispose();
            reader.Close();

            return list.ToArray();
    }

    /// <summary>
    /// Writes the specified data to a Resource file.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pathToFile"></param>
    public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
    {
            ResourceWriter resourceWriter = new ResourceWriter(pathToFile);

            foreach (KeyValuePair<string, string> pair in data)
            {
                resourceWriter.AddResource(pair.Key, pair.Value);
            }
            
            resourceWriter.Generate();
            
            resourceWriter.Close();
    }
}