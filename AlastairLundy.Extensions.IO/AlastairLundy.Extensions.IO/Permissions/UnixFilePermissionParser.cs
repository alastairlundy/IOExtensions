﻿/*
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

using AlastairLundy.Extensions.IO.Localizations;

namespace AlastairLundy.Extensions.IO.Permissions;

public static class UnixFilePermissionParser
{
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>the UnixFileMode enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static UnixFileMode ParseNumericValue(string permissionNotation)
    {
        if (IsNumericNotation(permissionNotation) && int.TryParse(permissionNotation, out int result))
        {
            return result switch
            {
                0 => UnixFileMode.None,
                111 => UnixFileMode.UserExecute,
                222 => UnixFileMode.UserWrite,
                333 => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                444 => UnixFileMode.UserRead,
                555 => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                666 => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                700 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                740 => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                       UnixFileMode.GroupRead,
                770 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                777 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                       UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Octal notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission octal notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the octal notation if a valid octal notation was specified; null otherwise.</param>
    /// <returns>true if a valid Unix file permission octal notation was specified; returns false otherwise.</returns>
    public static bool TryParseNumericValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseNumericValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>the UnixFileMode enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static UnixFileMode ParseSymbolicValue(string permissionNotation)
    {
        if (IsSymbolicNotation(permissionNotation))
        {
            return permissionNotation.ToLower() switch
            {
                "----------" => UnixFileMode.None,
                "---x--x--x" => UnixFileMode.UserExecute,
                "--w--w--w-" => UnixFileMode.UserWrite,
                "--wx-wx-wx" => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-r--r--r--" => UnixFileMode.UserRead,
                "-r-xr-xr-x" => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                "-rw-rw-rw-" => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                "-rwx------" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-rwxr-----" => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                                UnixFileMode.GroupRead,
                "-rwxrwx---" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                "-rwxrwxrwx" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                                UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the symbolic notation if a valid symbolic notation was specified; null otherwise.</param>
    /// <returns>true if a valid Unix file permission symbolic notation was specified; returns false otherwise.</returns>
    public static bool TryParseSymbolicValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseSymbolicValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in either Numeric or Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// 
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The unix file mode if a valid permission notation was parsed; null otherwise.</param>
    /// <returns>true if the file mode notation was parsed successfully; returns false otherwise.</returns>
    public static bool TryParse(string permissionNotation, out UnixFileMode? fileMode)
    {
        bool isNumericNotation = IsNumericNotation(permissionNotation);
        bool isSymbolicNotation = IsSymbolicNotation(permissionNotation);

        try
        {
            if (isNumericNotation && !isSymbolicNotation)
            {
                fileMode = ParseNumericValue(permissionNotation);
            }
            else if (isSymbolicNotation && !isNumericNotation)
            {
                fileMode = ParseSymbolicValue(permissionNotation);
            }
            else
            {
                fileMode = UnixFileMode.UserRead & UnixFileMode.UserWrite;
            }

            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Detects whether a Unix Octal file permission notation is valid.
    /// </summary>
    /// <param name="notation">The numeric notation to be compared.</param>
    /// <returns>true if a valid unix file permission octal notation has been provided; returns false otherwise.</returns>
    public static bool IsNumericNotation(string notation)
    {
        if (notation.Length == 4 && int.TryParse(notation, out int result))
        {
            return result switch
            {
                0 or 111 or 222 or 333 or 444 or 555 or 666 or 700 or 740 or 777 => true,
                _ => false
            };
        }

        return false;
    }

    /// <summary>
    /// Detects whether a Unix symbolic file permission is valid.
    /// </summary>
    /// <param name="notation">The symbolic notation to be compared.</param>
    /// <returns>true if a valid unix file permission symbolic notation has been provided; returns false otherwise.</returns>
    public static bool IsSymbolicNotation(string notation)
    {
        if (notation.Length == 10)
        {
            return notation switch
            {
                "----------" or
                    "---x--x--x" or
                    "--w--w--w-" or
                    "--wx-wx-wx" or
                    "-r--r--r--" or
                    "-r-xr-xr-x" or
                    "-rw-rw-rw-" or
                    "-rwx------" or
                    "-rwxr-----" or
                    "-rwxrwx---" or
                    "-rwxrwxrwx" => true,
                _ => false
            };
        }

        return false;
    }
}