/*
    IOExtensions 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

using AlastairLundy.Extensions.IO.Files;

using AlastairLundy.Extensions.IO.Paths.Abstractions;

namespace AlastairLundy.Extensions.IO.Paths;

public class PathResolver : IPathResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public bool DoesPathHaveExtension(string path)
    {
        if (DoesPathExist(path))
        {
            int lastDotIndex = path.LastIndexOf('.');

            return path.Length - lastDotIndex > 0;
        }
        
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public PathType GetPathType(string path)
    {
        if (DoesPathHaveExtension(path))
        {
            return PathType.File;
        }
        else
        {
            if (path.EndsWith(Path.DirectorySeparatorChar))
            {
                return PathType.Directory;
            }
            else
            {
                return Directory.Exists(path) ? PathType.Directory : PathType.File;
            }
        }
    }

    public bool DoesPathExist(string path)
    {
        try
        {
            
        }
        catch
        {
            return false;
        }
    }

    public bool IsPathFullyQualified(string path)
    {
        if (DoesPathExist(path))
        {
            bool output = false;
            
            string absolutePath = ToAbsolutePath(path);
            
            PathType pathType = GetPathType(absolutePath);

            if (pathType == PathType.Directory)
            {
                if (Directory.Exists(absolutePath))
                {
                    
                }
                else
                {
                    output = false;
                }
            }
            else
            {
                
            }

            return output;
        }

        throw new ArgumentException("String provided, with value {x}, is not a path".Replace("{x}", path));
    }

    public string ToRelativePath(string path)
    {
        
    }

    public string ToAbsolutePath(string path)
    {
        
    }

    public string NormalizePath(string path)
    {
        string output = path.Replace("\\", Path.DirectorySeparatorChar.ToString());
        output = 
        
    }

    public FileModel GetFile(string path)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetFileNameWithoutExtension(string path)
    {
        return path.Replace(GetPathExtension(path), string.Empty);
    }
    

    public string GetFileName(string path)
    {
        int indexOfLastDirectorySeparator = path.LastIndexOf(Path.DirectorySeparatorChar);

        if (indexOfLastDirectorySeparator != -1)
        {
            
        }
        else
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetPathExtension(string path)
    {
        if (DoesPathHaveExtension(path))
        {
            int lastDotIndex = path.LastIndexOf('.');

            if (path.Length - lastDotIndex > 0)
            {
                int extensionLength = path.Length - lastDotIndex;
                    
                return path.Substring(lastDotIndex, extensionLength);
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }

    
    public FileModel ResolvePathToFile(string path)
    {
        
    }

    public string CombinePaths(string path1, string path2)
    {
        if (path2.StartsWith(path1))
        {
            return path2;
        }
        else
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.Append(path1);
            stringBuilder.Append(Path.DirectorySeparatorChar);
            stringBuilder.Append(path2);
            stringBuilder.Append(Path.DirectorySeparatorChar);
            
            return stringBuilder.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string ExpandEnvironmentVariablesInPath(string path)
    {
        string[] pathComponents = NormalizePath(path).Split(Path.DirectorySeparatorChar).SkipLast(1).ToArray();
        
        StringBuilder stringBuilder = new StringBuilder();
        
        IDictionary environmentVariables = Environment.GetEnvironmentVariables();

        foreach (string pathComponent in pathComponents)
        {
            foreach (DictionaryEntry environmentVariable in environmentVariables)
            {
                if (pathComponent.Equals(environmentVariable.Key.ToString()))
                {
                    stringBuilder.Append(environmentVariable.Value);
                }
                else
                {
                    stringBuilder.Append(pathComponent);
                }
            }
            
            stringBuilder.Append(Path.DirectorySeparatorChar);
        }
        
        return stringBuilder.ToString();
    }
}