﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Backup;
using WInterop.FileManagement;
using WInterop.Handles;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static class Backup
        {
            /// <summary>
            /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
            /// </summary>
            public static class Desktop
            {
                /// <summary>
                /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
                /// </summary>
                /// <remarks>
                /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
                /// </remarks>
#if DESKTOP
                [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
                public static class Direct
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa362509.aspx
                    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                    [return: MarshalAs(UnmanagedType.Bool)]
                    public static extern bool BackupRead(
                        SafeFileHandle hFile,
                        SafeHandle lpBuffer,
                        uint nNumberOfBytesToRead,
                        out uint lpNumberOfBytesRead,
                        [MarshalAs(UnmanagedType.Bool)] bool bAbort,
                        [MarshalAs(UnmanagedType.Bool)] bool bProcessSecurity,
                        ref IntPtr context);

                    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa362510.aspx
                    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                    [return: MarshalAs(UnmanagedType.Bool)]
                    public static extern bool BackupSeek(
                        SafeFileHandle hFile,
                        uint dwLowBytesToSeek,
                        uint dwHighBytesToSeek,
                        out uint lpdwLowByteSeeked,
                        out uint lpdwHighByteSeeked,
                        ref IntPtr context);
                }

                public static IEnumerable<WInterop.Backup.StreamInformation> GetAlternateStreamInformation(string path)
                {
                    List<WInterop.Backup.StreamInformation> streams = new List<WInterop.Backup.StreamInformation>();
                    using (var fileHandle = FileManagement.CreateFile(
                        path: path,
                        // To look at metadata we don't need read or write access
                        fileAccess: 0,
                        fileShare: System.IO.FileShare.ReadWrite,
                        creationDisposition: System.IO.FileMode.Open,
                        fileAttributes: FileAttributes.NONE,
                        fileFlags: FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                    {
                        using (BackupReader reader = new BackupReader(fileHandle))
                        {
                            WInterop.Backup.StreamInformation? info;
                            while ((info = reader.GetNextInfo()).HasValue)
                            {
                                if (info.Value.StreamType == BackupStreamType.BACKUP_ALTERNATE_DATA)
                                {
                                    streams.Add(new WInterop.Backup.StreamInformation { Name = info.Value.Name, Size = info.Value.Size });
                                }
                            }
                        }
                    }

                    return streams;
                }
            }
        }
    }
}