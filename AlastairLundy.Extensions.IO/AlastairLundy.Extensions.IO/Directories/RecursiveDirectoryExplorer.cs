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
using System.IO;
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
        List<string> files = new();
        List<string> directories = new();
        List<string> emptyDirectories = new();
        
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