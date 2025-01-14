﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors
{
    // https://msdn.microsoft.com/en-us/library/cc231198.aspx
    public enum HResult : int
    {
        S_OK = 0,
        S_FALSE = 1,
        E_NOINTERFACE = unchecked((int)0x80004002),
        E_POINTER = unchecked((int)0x80004003),
        E_FAIL = unchecked((int)0x80004005),
        STG_E_INVALIDFUNCTION = unchecked((int)0x80030001L),
        STG_E_FILENOTFOUND = unchecked((int)0x80030002),
        STG_E_ACCESSDENIED = unchecked((int)0x80030005),
        STG_E_INVALIDPARAMETER = unchecked((int)0x80030057),
        STG_E_INVALIDFLAG = unchecked((int)0x800300FF),
        E_ACCESSDENIED = unchecked((int)0x80070005L),
        E_INVALIDARG = unchecked((int)0x80070057),
        D2DERR_WRONG_STATE = unchecked((int)0x88990001),
        D2DERR_RECREATE_TARGET = unchecked((int)0x8899000C),
        D2DERR_INVALID_PROPERTY = unchecked((int)0x88990029),
        WINCODEC_ERR_UNSUPPORTEDPIXELFORMAT = unchecked((int)0x88982F80)
    }
}
