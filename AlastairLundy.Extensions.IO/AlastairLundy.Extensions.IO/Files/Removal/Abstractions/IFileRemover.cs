/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Files.Removal.Abstractions
{
    public interface IFileRemover
    {
#if NET6_0_OR_GREATER
        public event EventHandler<string> FileDeleted;
    
        public bool TryDeleteFile(string file);

        public void DeleteFile(string file);

        public void DeleteFiles(IEnumerable<string> files);
#else
         event EventHandler<string> FileDeleted;
    
         bool TryDeleteFile(string file);

         void DeleteFile(string file);

         void DeleteFiles(IEnumerable<string> files);        
#endif
    }
}