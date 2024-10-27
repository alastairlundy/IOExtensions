/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;

namespace AlastairLundy.Extensions.IO.Directories.Abstractions
{
    public interface IDirectoryCreator
    {
#if NET8_OR_GREATER
        public bool TryCreateDirectory(string directoryPath,string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
        public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
        public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);
        public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
#endif
        
#if NET6_0_OR_GREATER || NETSTANDARD2_1
        public bool TryCreateDirectory(string directoryPath,string newDirectoryName, bool createParentPaths);
        public void CreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths);
        public bool TryCreateParentDirectory(string directoryPath);
        public void CreateParentDirectory(string parentDirectory);
#else
        bool TryCreateDirectory(string directoryPath,string newDirectoryName, bool createParentPaths);
        void CreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths);
        bool TryCreateParentDirectory(string directoryPath);
        void CreateParentDirectory(string parentDirectory);
#endif
    }
}