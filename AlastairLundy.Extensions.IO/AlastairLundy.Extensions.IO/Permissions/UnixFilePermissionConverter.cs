/*
    IOExtensions 
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.Extensions.IO.Localizations;

namespace AlastairLundy.Extensions.IO.Permissions
{
    public static class UnixFilePermissionConverter
    {
        /// <summary>
        /// Converts a Unix file permission in symbolic notation to Octal notation.
        /// </summary>
        /// <param name="symbolicNotation">The symbolic notation to be converted to octal notation.</param>
        /// <returns>the octal notation equivalent of the specified symbolic notation.</returns>
        /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
        public static string ToNumericNotation(string symbolicNotation)
        {
            if (symbolicNotation.Length == 10)
            {
#if NET6_0_OR_GREATER
                return symbolicNotation switch
                {
                    "----------" => "0000",
                    "---x--x--x" => "0111",
                    "--w--w--w-" => "0222",
                    "--wx-wx-wx" => "0333",
                    "-r--r--r--" => "0444",
                    "-r-xr-xr-x" => "0555",
                    "-rw-rw-rw-" => "0666",
                    "-rwx------" => "0700",
                    "-rwxr-----" => "0740",
                    "-rwxrwx---" => "0770",
                    "-rwxrwxrwx" => "0777",
                    _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
                };
#else
                switch (symbolicNotation)
                {
                    case "----------":
                        return "0000";
                    case "---x--x--x":
                        return "0111";
                    case "--w--w--w-":
                        return "0222";
                    case "--wx-wx-wx":
                        return "0333";
                    case "-r--r--r--":
                        return "0444";
                    case "-r-xr-xr-x":
                        return "0555";
                    case "-rw-rw-rw-":
                        return "0666";
                    case "-rwx------":
                        return "0700";
                    case "-rwxr-----":
                        return "0740";
                    case "-rwxrwx---":
                        return "0770";
                    case "-rwxrwxrwx":
                        return "0777";
                    default:
                        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
                }
#endif
            }

            throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
        }

        /// <summary>
        /// Converts a Unix file permission in symbolic notation to Octal notation.
        /// </summary>
        /// <param name="numericNotation">The octal notation to be converted to symbolic notation.</param>
        /// <returns>the symbolic notation equivalent of the specified octal notation.</returns>
        /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
        public static string ToSymbolicNotation(string numericNotation)
        {
            if (numericNotation.Length == 4 && int.TryParse(numericNotation, out int result))
            {
#if NET6_0_OR_GREATER
             return result switch
                {
                    0 => "----------",
                    111 => "---x--x--x",
                    222 => "--w--w--w-",
                    333 => "--wx-wx-wx",
                    444 => "-r--r--r--",
                    555 => "-r-xr-xr-x",
                    666 => "-rw-rw-rw-",
                    700 => "-rwx------",
                    740 => "-rwxr-----",
                    770 => "-rwxrwx---",
                    777 => "-rwxrwxrwx",
                    _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
                };
#else
                switch (result)
                {
                    case 0:
                        return "----------";
                    case 111:
                        return "---x--x--x";
                    case 222:
                        return "--w--w--w-";
                    case 333:
                        return "--wx-wx-wx";
                    case 444:
                        return "-r--r--r--";
                    case 555:
                        return "-r-xr-xr-x";
                    case 666:
                        return "-rw-rw-rw-";
                    case 700:
                        return "-rwx------";
                    case 740:
                        return "-rwxr-----";
                    case 770:
                        return "-rwxrwx---";
                    case 777:
                        return "-rwxrwxrwx";
                    default:
                        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
                }
#endif
            }

            throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
        }
    }
}