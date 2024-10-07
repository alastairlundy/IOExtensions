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

using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Directories.Abstractions
{
    public interface IRecursiveDirectoryExplorer
    {
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        public bool AreSubdirectoriesEmpty(string directory);

        public (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory);

        public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetRecursiveDirectoryContents(string directory, bool includeEmptyDirectories);

        public IEnumerable<string> GetRecursiveEmptyDirectories(string directory);

        public IEnumerable<string> GetFilesRecursively(string directory);
        public IEnumerable<string> GetFolderRecursively(string directory);
#else
        bool AreSubdirectoriesEmpty(string directory);

        (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory);

        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetRecursiveDirectoryContents(string directory, bool includeEmptyDirectories);

        IEnumerable<string> GetRecursiveEmptyDirectories(string directory);

        IEnumerable<string> GetFilesRecursively(string directory);
        IEnumerable<string> GetFolderRecursively(string directory);
#endif
    }
}