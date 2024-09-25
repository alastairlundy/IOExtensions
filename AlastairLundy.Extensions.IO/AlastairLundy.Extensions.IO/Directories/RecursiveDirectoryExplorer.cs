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

using AlastairLundy.Extensions.IO.Directories.Abstractions;
using AlastairLundy.Extensions.IO.Directories.Extensions;

using AlastairLundy.Extensions.IO.Localizations;

using AlastairLundy.Extensions.System;

namespace AlastairLundy.Extensions.IO.Directories;

/// <summary>
/// 
/// </summary>
public class RecursiveDirectoryExplorer : IRecursiveDirectoryExplorer
{
    protected DirectoryCreator DirectoryCreator;

    public RecursiveDirectoryExplorer()
    {
        DirectoryCreator = new DirectoryCreator();
    }
    
    /// <summary>
    /// Determines whether subdirectories of a directory are empty.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>true if all subdirectories in a directory are empty; returns false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public bool AreSubdirectoriesEmpty(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
        
        string[] subDirectories = Directory.GetDirectories(directory);
                
        bool[] allowRecursiveEmptyDirectoryDeletion = new bool[subDirectories.Length];

        for (int i = 0; i < subDirectories.Length; i++)
        {
            string dir = subDirectories[i];
                    
            if (dir.IsDirectoryEmpty())
            {
                allowRecursiveEmptyDirectoryDeletion[i] = true;
            }
            else
            {
                allowRecursiveEmptyDirectoryDeletion[i] = false;
            }
        }

        return allowRecursiveEmptyDirectoryDeletion.IsAllTrue();
    }
    
    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    public (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory)
    {
        var output = GetRecursiveDirectoryContents(directory, true);
        return (output.files, output.directories);
    }

    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="includeEmptyDirectories">Whether to include empty directories or not.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories)
        GetRecursiveDirectoryContents(string directory,
            bool includeEmptyDirectories)
    {
        List<string> files = new List<string>();
        List<string> directories = new List<string>();
        List<string> emptyDirectories = new List<string>();
        
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            files.Add(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(subDirectory).Length;

                    if (numberOfFiles > 0)
                    {
                        directories.Add(subDirectory);
                    }
                    else if (includeEmptyDirectories == true && directory.IsDirectoryEmpty())
                    {
                        emptyDirectories.Add(subDirectory);
                    }
                }
            }
            else
            {
                if (includeEmptyDirectories)
                {
                    emptyDirectories.Add(directory);
                }
            }

            return (files, directories, emptyDirectories);
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetRecursiveEmptyDirectories(string directory)
    {
        return GetRecursiveDirectoryContents(directory, true).emptyDirectories;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFilesRecursively(string directory)
    {
       return GetRecursiveDirectoryContents(directory, false).files;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFolderRecursively(string directory)
    {
        return GetRecursiveDirectoryContents(directory, false).directories;
    }
}