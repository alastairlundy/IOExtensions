/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.Extensions.IO.Files
{
    public interface IFileFinder
    {
#if NET6_0_OR_GREATER
        public bool IsAFile(string filePath);
#else
         bool IsAFile(string filePath);
#endif
    }
}