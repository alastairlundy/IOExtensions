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

namespace AlastairLundy.Extensions.IO.Files.Concatenation
{
    /// <summary>
    /// Syntactic sugar around the FileAppender class.
    /// </summary>
    public static class FileConcatenator
    {
        /// <summary>
        /// Concatenated the contents of files in the style of the Unix Cat command.
        /// </summary>
        /// <param name="files">The files to be concatenated.</param>
        /// <returns>the concatenated files as an IEnumerable of strings.</returns>
        public static IEnumerable<string> ConcatenateFilesToEnumerable(IEnumerable<string> files)
        {
            return ConcatenateFilesToArray(files);
        }

        /// <summary>
        /// Concatenated the contents of files in the style of the Unix Cat command.
        /// </summary>
        /// <param name="files">The files to be concatenated.</param>
        /// <returns>the concatenated files as an array of strings.</returns>
        public static string[] ConcatenateFilesToArray(IEnumerable<string> files)
        {
            return ConcatenateFilesToList(files).ToArray();
        }

        /// <summary>
        /// Concatenated the contents of files in the style of the Unix Cat command.
        /// </summary>
        /// <param name="files">The files to be concatenated.</param>
        /// <returns>the concatenated files as a list of strings.</returns>
        public static List<string> ConcatenateFilesToList(IEnumerable<string> files)
        {
            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendFiles(files);

            return fileAppender.ToList();
        }
        
        /// <summary>
        /// Concatenates the contents of specified files and saves it to a new file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFileName"></param>
        /// <param name="files">The files to be concatenated.</param>
        /// <exception cref="Exception">Thrown </exception>
        public static void ConcatenateFilesToNewFile(string filePath, string newFileName, IEnumerable<string> files)
        {
            try
            {
                string newFile = $"{filePath}{Path.DirectorySeparatorChar}{newFileName}";

                if (filePath.Contains(newFileName) == false)
                {
                    File.WriteAllLines(newFile, ConcatenateFilesToEnumerable(files));
                }
                else
                {
                    File.WriteAllLines(newFileName, ConcatenateFilesToEnumerable(files));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}