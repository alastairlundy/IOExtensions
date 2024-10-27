/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.IO.Files.Concatenation.Abstractions;
using AlastairLundy.Extensions.IO.Localizations;

// ReSharper disable RedundantIfElseBlock

namespace AlastairLundy.Extensions.IO.Files.Concatenation
{
    /// <summary>
    /// A class to append the contents of files.
    /// </summary>
    public class FileAppender : IFileAppender
    {
        protected List<string> _appendedFileContents;
        
        protected IFileFinder _fileFinder;
    
        public FileAppender()
        {
            _appendedFileContents = new List<string>();
            _fileFinder = new FileFinder();
        }

        public FileAppender(IFileFinder fileFinder)
        {
            _fileFinder = fileFinder;
            _appendedFileContents = new List<string>();
        }
        
        /// <summary>
        /// Attempts to append the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to have its contents appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>true if the file was successfully appended; returns false otherwise.</returns>
        public bool TryAppendFile(string fileToBeAppended)
        {
            try
            {
                AppendFile(fileToBeAppended);

                return true;
            }
            catch
            {
                return false;
            }
        }
    
        /// <summary>
        /// Appends the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        public void AppendFile(string fileToBeAppended)
        {
            if (_fileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
            {
                try
                {
                    string[] fileContents = File.ReadAllLines(fileToBeAppended);

                    AppendFileContents(fileContents);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
            }
        }

        /// <summary>
        /// Append the contents of an ordered enumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        public void AppendFiles(IOrderedEnumerable<string> filesToBeAppended)
        {
            AppendFiles(filesToBeAppended.ToArray());
        }

        /// <summary>
        /// Append the contents of the IEnumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        public void AppendFiles(IEnumerable<string> filesToBeAppended)
        {
            foreach (string file in filesToBeAppended)
            {
                AppendFile(file);
            }
        }

        /// <summary>
        /// Append the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileContents">The file contents to be appended.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        public void AppendFileContents(IEnumerable<string> fileContents)
        {
            try
            {
                string[] fileContentsArray = fileContents as string[] ?? fileContents.ToArray();
                
                foreach (string str in fileContentsArray)
                {
                    _appendedFileContents.Add(str);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// Attempts to append file contents to the existing file contents.
        /// </summary>
        /// <param name="fileContents">The file contents to be appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>true if the file contents was successfully appended; returns false otherwise.</returns>
        public bool TryAppendFileContents(IEnumerable<string> fileContents)
        {
            try
            {
                AppendFileContents(fileContents);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to append the contents of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>true if the files were successfully appended; returns false otherwise.</returns>
        public bool TryAppendFiles(IEnumerable<string> filesToBeAppended)
        {
            try
            {
                AppendFiles(filesToBeAppended);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the appended contents as an IEnumerable.
        /// </summary>
        /// <returns>the list of appended strings as an enumerable.</returns>
        public IEnumerable<string> ToEnumerable()
        {
            return _appendedFileContents;
        }

        /// <summary>
        /// Returns the appended contents as an array of strings.
        /// </summary>
        /// <returns>the array of appended strings.</returns>
        public string[] ToArray()
        {
            return ToList().ToArray();
        }

        /// <summary>
        /// Returns the appended contents as a list of strings.
        /// </summary>
        /// <returns>the list of appended strings.</returns>
        public List<string> ToList()
        {
            return _appendedFileContents;
        }

        /// <summary>
        /// Writes the appended strings to a file.
        /// </summary>
        /// <param name="filePath">The path to save the file to.</param>
        /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
        public void WriteToFile(string filePath)
        {
            if (_fileFinder.IsAFile(filePath))
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.WriteAllLines(filePath, _appendedFileContents);
                }
                catch (FileNotFoundException exception)
                {
                    throw new FileNotFoundException(exception.Message, filePath, exception);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new ArgumentException(Resources.Exceptions_NoFileProvided, filePath);
            }
        }
    }
}