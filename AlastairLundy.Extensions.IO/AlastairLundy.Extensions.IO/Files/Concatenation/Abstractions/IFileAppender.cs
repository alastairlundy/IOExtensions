﻿/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.Extensions.IO.Files.Concatenation.Abstractions
{
    public interface IFileAppender
    {
        void AppendFile(string fileToBeAppended);
        bool TryAppendFile(string fileToBeAppended);
        
        void AppendFiles(IOrderedEnumerable<string> filesToBeAppended);
        void AppendFiles(IEnumerable<string> filesToBeAppended);

        void AppendFileContents(IEnumerable<string> fileContents);
        
        bool TryAppendFileContents(IEnumerable<string> fileContents);
        bool TryAppendFiles(IEnumerable<string> filesToBeAppended);

        IEnumerable<string> ToEnumerable();
        string[] ToArray();
        List<string> ToList();

        void WriteToFile(string filePath);

    }
}