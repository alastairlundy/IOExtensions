/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AlastairLundy.Extensions.IO.Directories.Abstractions;

namespace AlastairLundy.Extensions.IO.Directories;

public class DirectoryCreator : IDirectoryCreator
{
    /// <summary>
    /// Attempts to create a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    /// <returns>true if the directory was successfully created; returns false otherwise.</returns>
    public bool TryCreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths)
    {
        try
        {
            CreateDirectory(directoryPath, newDirectoryName, unixFileMode, createParentPaths);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="unixFileMode"></param>
    /// <returns></returns>
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode)
    {
        try
        {
            CreateParentDirectory(directoryPath, unixFileMode);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Recursively creates the parent directory as needed.
    /// </summary>
    /// <param name="parentDirectory">The parent directory to be created.</param>
    /// <param name="unixFileMode"></param>
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode)
    {
        string[] directories = parentDirectory.Split(Path.DirectorySeparatorChar);

        List<string> directoriesToCreate = new List<string>();
        
        for (int i = 0; i < directories.Length; i++)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int j = 0; j < i; j++)
            {
                stringBuilder.Append(directories[i][j]);
            }
            
            directoriesToCreate.Add(stringBuilder.ToString());
        }
        
        foreach (string directory in directoriesToCreate)
        {
            if (Directory.Exists(directory) == false)
            {
                CreateDirectory(directory, directory, unixFileMode, false);
            }
        }
    }
    
    /// <summary>
    /// Creates a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths)
    {
        if (createParentPaths)
        {
            if (directoryPath.EndsWith(newDirectoryName))
            {
                directoryPath = directoryPath.Remove(directoryPath.Length - newDirectoryName.Length, newDirectoryName.Length);
            }
            
            CreateParentDirectory(directoryPath, unixFileMode);
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath, unixFileMode);
            }
        }
    }
}