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

namespace AlastairLundy.Extensions.IO.Directories.Abstractions
{
    public interface IRecursiveDirectoryRemover
    {
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        public event EventHandler<string> DirectoryDeleted; 
        public event EventHandler<string> FileDeleted;
        public void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory);
    
        public bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories);

#else
        event EventHandler<string> DirectoryDeleted; 
        event EventHandler<string> FileDeleted;
        void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory);
    
        bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories);
#endif
    }
}