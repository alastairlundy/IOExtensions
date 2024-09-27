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

using AlastairLundy.Extensions.IO.Files.Abstractions;

using AlastairLundy.Extensions.IO.Localizations;

namespace AlastairLundy.Extensions.IO.Files;

public class FileRemover : IFileRemover
{
    public event EventHandler<string> FileDeleted;

    public FileRemover()
    {
        
    }

    /// <summary>
    /// Attempts to delete a file.
    /// </summary>
    /// <param name="file">The file to be deleted.</param>
    /// <returns>true if the file has been successfully deleted; returns false otherwise.</returns>
    public bool TryDeleteFile(string file)
    {
        try
        {
            DeleteFile(file);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Deletes a file.
    /// </summary>
    /// <param name="file">The file to be deleted.</param>
    /// <exception cref="FileNotFoundException">Thrown if the file doesn't exist.</exception>
    public void DeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
            FileDeleted?.Invoke(this, Resources.File_Deleted.Replace("{x}", file));
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{x}", file));
    }

    /// <summary>
    /// Deletes multiple files.
    /// </summary>
    /// <param name="files">The files to be deleted.</param>
    public void DeleteFiles(IEnumerable<string> files)
    {
        string[] enumerable = files as string[] ?? files.ToArray();
        for(int i = 0; i < enumerable.Count(); i++)
        {
            DeleteFile(enumerable[i]);
        }
    }
}