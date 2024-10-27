/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
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