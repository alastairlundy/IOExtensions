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

namespace AlastairLundy.Extensions.IO.Directories.Abstractions;

public interface IDirectoryCreator
{
#if NET8_OR_GREATER
    public bool TryCreateDirectory(string directoryPath,string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
    public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths);
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
#endif
    public bool TryCreateDirectory(string directoryPath,string newDirectoryName, bool createParentPaths);
    public void CreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths);
    public bool TryCreateParentDirectory(string directoryPath);
    public void CreateParentDirectory(string parentDirectory);
}