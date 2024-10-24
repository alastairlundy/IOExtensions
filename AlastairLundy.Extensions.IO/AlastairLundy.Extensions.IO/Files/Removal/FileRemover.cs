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
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.IO.Files.Removal.Abstractions;

using AlastairLundy.Extensions.IO.Localizations;

namespace AlastairLundy.Extensions.IO.Files.Removal
{
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
}