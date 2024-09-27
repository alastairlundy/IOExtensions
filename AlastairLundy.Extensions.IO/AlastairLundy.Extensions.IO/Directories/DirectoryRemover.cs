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

namespace AlastairLundy.Extensions.IO.Directories;

public class DirectoryRemover : IDirectoryRemover, IRecursiveDirectoryRemover
{
    public DirectoryRemover()
    {
        
    }
    
    public event EventHandler<string> DirectoryDeleted; 
    public event EventHandler<string> FileDeleted;
    
    /// <summary>
    /// Attempts to delete the specified Directory.
    /// </summary>
    /// <param name="directory">The directory to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the directory if it is empty or not.</param>
    /// <param name="deleteParentDirectory"></param>
    /// <returns>true if the directory was successfully deleted; returns false otherwise.</returns>
    public bool TryDeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        try
        {
            DeleteDirectory(directory, deleteEmptyDirectory, deleteParentDirectory);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a specified directory.
    /// </summary>
    /// <param name="directory">The directory to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the directory or not if the directory is empty.</param>
    /// <param name="deleteParentDirectory"></param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        if (Directory.Exists(directory))
        {
            if ((directory.IsDirectoryEmpty() && deleteEmptyDirectory) || !deleteEmptyDirectory)
            {
                    Directory.Delete(directory);
                    DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));

                    if (deleteParentDirectory)
                    {
                       DeleteParentDirectory(directory, deleteEmptyDirectory);
                    }
            }
        }
        else
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
    }

    /// <summary>
    /// Attempts to delete a directory recursively by deleting sub-folders and files before deleting the directory.
    /// </summary>
    /// <param name="directory">The parent directory to be deleted.</param>
    /// <param name="deleteEmptyDirectories">Whether to delete empty sub-folders or not.</param>
    /// <returns>true if the directory was recursively deleted successfully; returns false otherwise.</returns>
    public bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories)
    {
        try
        {
            DeleteDirectoryRecursively(directory, deleteEmptyDirectories);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Deletes a parent directory of a directory.
    /// </summary>
    /// <param name="directory">The directory to get the parent directory of.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the parent directory if is empty or not.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteParentDirectory(string directory, bool deleteEmptyDirectory)
    {
        if (Directory.Exists(directory))
        {
            if (directory.IsDirectoryEmpty() && deleteEmptyDirectory || directory.IsDirectoryEmpty() == false)
            {
                string parentDirectory = Directory.GetParent(directory)!.FullName;
                
                Directory.Delete(parentDirectory);
                DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", parentDirectory));
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }
    
        /// <summary>
    /// Deletes a directory recursively by deleting 
    /// </summary>
    /// <param name="directory">The directory to be recursively deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete empty directories or not.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory)
    {
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
                            File.Delete(file);
                            FileDeleted?.Invoke(this, Resources.File_Deleted.Replace("{x}", file));
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (deleteEmptyDirectory == true && numberOfFiles == 0 || numberOfFiles > 0)
                    {
                        Directory.Delete(subDirectory);

                        if (deleteEmptyDirectory == true && numberOfFiles == 0)
                        {
                            DirectoryDeleted?.Invoke(this, Resources.EmptyDirectory_Deleted.Replace("{x}", subDirectory));
                        }
                        else
                        {
                            DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", subDirectory));
                        }
                    }
                }
            }
            else
            {
                if (deleteEmptyDirectory)
                {
                    Directory.Delete(directory);
                    DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));
                }
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// Deletes multiple specified directories.
    /// </summary>
    /// <param name="directories">The directories to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete empty directories or not.</param>
    /// <param name="deleteParentDirectory"></param>
    public void DeleteDirectories(IEnumerable<string> directories, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        foreach (string directory in directories)
        {
            DeleteDirectory(directory, deleteEmptyDirectory, deleteParentDirectory);
        }
    }
}