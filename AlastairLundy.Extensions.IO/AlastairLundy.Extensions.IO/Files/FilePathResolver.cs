﻿/*
    IOExtensions 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


#nullable enable

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

using System;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.IO.Files.Abstractions;

using AlastairLundy.Extensions.IO.Internal.Localizations;

namespace AlastairLundy.Extensions.IO.Files;

public class FilePathResolver : IFilePathResolver
{
    
    /// <summary>
    /// Resolves the file path to a specific file if the file path is part of a PATH environment variable.
    /// </summary>
    /// <param name="inputFilePath">The input file path to resolve.</param>
    /// <param name="outputFilePath">The resolved file path if the file path is in the PATH environment variable; the original input file path otherwise.</param>
    /// <exception cref="FileNotFoundException">Thrown if the input file path is null or empty.</exception>
    public void ResolveFilePath(string inputFilePath, out string outputFilePath)
    {
        if (string.IsNullOrEmpty(inputFilePath))
        {
            throw new ArgumentNullException(Resources.Exceptions_IO_TargetFile_NullOrEmpty);
        }

        if (File.Exists(inputFilePath))
        {
            outputFilePath = inputFilePath;
            return;
        }
        
        string? path = Environment.GetEnvironmentVariable("PATH");
            
        char pathSeparator = OperatingSystem.IsWindows() ? ';' : ':';

        if (path == null)
        {
            outputFilePath = inputFilePath;
            return;
        }
            
        string[] paths = path.Split(pathSeparator);

        foreach (string pathLine in paths)
        {
            string[] files = Directory.EnumerateFiles(pathLine,
                $"{Path.GetFileName(inputFilePath)}.*",
                SearchOption.TopDirectoryOnly).ToArray();
            
            if (files.Length > 0)
            {
                outputFilePath = files.First();
                return;
            }
        }
        
        outputFilePath = Path.GetFullPath(inputFilePath);
    }
}