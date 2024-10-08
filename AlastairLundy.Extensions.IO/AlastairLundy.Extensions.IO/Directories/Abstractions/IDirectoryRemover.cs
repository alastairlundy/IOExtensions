﻿/*
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

namespace AlastairLundy.Extensions.IO.Directories.Abstractions
{
    public interface IDirectoryRemover
    {
    #if NET6_0_OR_GREATER || NETSTANDARD2_1
        public event EventHandler<string> DirectoryDeleted; 
    
        public bool TryDeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory);
    
        public void DeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory);

        public void DeleteParentDirectory(string directory, bool deleteEmptyDirectory);

        public void DeleteDirectories(IEnumerable<string> directories, bool deleteEmptyDirectory,
            bool deleteParentDirectory);
#else
         event EventHandler<string> DirectoryDeleted; 
    
         bool TryDeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory);
    
         void DeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory);

         void DeleteParentDirectory(string directory, bool deleteEmptyDirectory);

         void DeleteDirectories(IEnumerable<string> directories, bool deleteEmptyDirectory,
            bool deleteParentDirectory);
#endif
    }
}