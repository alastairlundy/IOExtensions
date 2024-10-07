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

using System.IO;
using AlastairLundy.Extensions.IO.Files.Abstractions;

namespace AlastairLundy.Extensions.IO.Files
{
    public class FileFinder : IFileFinder
    {
        /// <summary>
        /// Determines whether a string is the name of a file.
        /// </summary>
        /// <param name="filePath">The string to be searched.</param>
        /// <returns>true if the string is a file; return false otherwise.</returns>
        public bool IsAFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return true;
                }
            
                if (filePath.Length > 1)
                {
                    if (filePath.Length - 4 >= 0 && filePath.Length - 4 < filePath.Length)
                    {
#if NET6_0_OR_GREATER
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[^4].Equals('.'))
                        {
                            return true;
                        }
#else
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[filePath.Length - 4].Equals('.'))
                        {
                            return true;
                        }
#endif
                    }
                    if (filePath.Length - 3 >= 0 && filePath.Length - 3 < filePath.Length)
                    {
#if NET6_0_OR_GREATER
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[^3].Equals('.') || filePath[^2].Equals('.'))
                        {
                            return true;
                        }
#else
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[filePath.Length - 3].Equals('.') || filePath[filePath.Length - 2].Equals('.'))
                        {
                            return true;
                        }
#endif
                    }

                    if (filePath.Length - 2 >= 0 && filePath.Length - 2 < filePath.Length)
                    {
#if NET6_0_OR_GREATER
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[^2].Equals('.'))
                        {
                            return true;
                        }
#else
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[filePath.Length - 2].Equals('.'))
                        {
                            return true;
                        }
#endif
                    }
                }
            
                return File.Exists(filePath);
            }
            catch
            {
                return false;
            }
        }
    }
}