#region Copyright (C) 2009 Nate

/* 
 *	Copyright (C) 2009 Nate
 *	http://nate.dynalias.net
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IconExtractor
{
    internal class Shell32
    {
        [DllImport("shell32.dll")]
        internal static extern int SHGetDesktopFolder(ref IShellFolder ppshf);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void SHCreateItemFromParsingName(
            [In][MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            [In] IntPtr pbc,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv);

        [DllImport("shell32.dll", PreserveSig = false)]
        internal static extern void SHCreateItemFromIDList(
            [In] IntPtr pidl,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv);

        [DllImport("shell32.dll", PreserveSig = false)]
        internal static extern void SHCreateShellItem(
            [In]IntPtr pidlParent,
            [In, MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] IShellFolder psfParent,
            [In]IntPtr ppidl,
            [Out, MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppsi);

        [DllImport("shell32", CharSet = CharSet.Auto)]
        internal extern static int SHGetMalloc(out IMalloc ppMalloc);

        [DllImport("shell32", CharSet = CharSet.Auto)]
        internal extern static int SHGetPathFromIDList(
            IntPtr pidl,
            StringBuilder pszPath);

        [DllImport("shell32", CharSet = CharSet.Auto)]
        internal extern static IntPtr ExtractAssociatedIcon(
            IntPtr hInst,
            StringBuilder pszPath,
            ref int index);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            int cbFileInfo,
            SHGFIFLAGS uFlags);

        //[DllImport("shell32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

    }

    [ComImportAttribute()]
    [GuidAttribute("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItemImageFactory
    {
        void GetImage(
        [In, MarshalAs(UnmanagedType.Struct)] SIZE size,
        [In] SIIGBF flags,
        [Out] out IntPtr phbm);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;

        public SIZE(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }
    }

    [Flags]
    public enum SIIGBF
    {
        SIIGBF_RESIZETOFIT = 0x00,
        SIIGBF_BIGGERSIZEOK = 0x01,
        SIIGBF_MEMORYONLY = 0x02,
        SIIGBF_ICONONLY = 0x04,
        SIIGBF_THUMBNAILONLY = 0x08,
        SIIGBF_INCACHEONLY = 0x10,
    }

    public enum SIGDN : uint
    {
        NORMALDISPLAY = 0,
        PARENTRELATIVEPARSING = 0x80018001,
        PARENTRELATIVEFORADDRESSBAR = 0x8001c001,
        DESKTOPABSOLUTEPARSING = 0x80028000,
        PARENTRELATIVEEDITING = 0x80031001,
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        FILESYSPATH = 0x80058000,
        URL = 0x80068000
    }

    [ComImport, Guid("00000000-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IUnknown
    {
        [PreserveSig]
        IntPtr QueryInterface(ref Guid riid, out IntPtr pVoid);

        [PreserveSig]
        IntPtr AddRef();

        [PreserveSig]
        IntPtr Release();
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    public interface IShellItem
    {
        void BindToHandler(IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)]Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)]Guid riid,
            out IntPtr ppv);

        void GetParent(out IShellItem ppsi);

        void GetDisplayName(SIGDN sigdnName, out IntPtr ppszName);

        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

        void Compare(IShellItem psi, uint hint, out int piOrder);
    };


    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214E6-0000-0000-C000-000000000046")]
    public interface IShellFolder
    {
        [PreserveSig()]
        uint ParseDisplayName(
            IntPtr hwnd,
            IntPtr pbc,
            [In(), MarshalAs(UnmanagedType.LPWStr)]
            string pszDisplayName,
            out int pchEaten,
            out IntPtr ppidl,
            out int pdwAttributes);

        [PreserveSig()]
        uint EnumObjects(
            IntPtr hwnd,
            SHCONTF grfFlags,
            out IEnumIDList ppenumIDList);

        [PreserveSig()]
        uint BindToObject(
            IntPtr pidl,
            IntPtr pbc,
            [In()] ref Guid riid,
            out IShellFolder ppv);

        [PreserveSig()]
        uint BindToStorage(
            IntPtr pidl,
            IntPtr pbc,
            [In()] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv);

        [PreserveSig()]
        int CompareIDs(
            int lParam,
            IntPtr pidl1,
            IntPtr pidl2);

        [PreserveSig()]
        uint CreateViewObject(
            IntPtr hwndOwner,
            [In()] ref Guid riid,
           [MarshalAs(UnmanagedType.Interface)] ref IUnknown ppv);

        [PreserveSig()]
        uint GetAttributesOf(
            int cidl,
            [In(), MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
            [MarshalAs(UnmanagedType.LPArray)] SFGAOF[] rgfInOut);

        [PreserveSig()]
        uint GetUIObjectOf(
            IntPtr hwndOwner,
            int cidl,
            ref IntPtr apidl,
            [In()] ref Guid riid,
            out int rgfReserved,
            [MarshalAs(UnmanagedType.Interface)] ref IUnknown ppv);

        [PreserveSig()]
        uint GetDisplayNameOf(
            IntPtr pidl,
            SHGNO uFlags,
            out STRRET pName);

        [PreserveSig()]
        uint SetNameOf(
            IntPtr hwnd,
            IntPtr pidl,
            [In(), MarshalAs(UnmanagedType.LPWStr)] 
            string pszName,
            SHGNO uFlags,
            out IntPtr ppidlOut);
    }

    [ComImportAttribute()]
    [GuidAttribute("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IExtractImage
    {
        void GetLocation(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPathBuffer,
            int cch,
            ref int pdwPriority,
            ref SIZE prgSize,
            int dwRecClrDepth,
            ref int pdwFlags);

        void Extract(
            out IntPtr phBmpThumbnail);
    }

    [ComImport()]
    [Guid("000214eb-0000-0000-c000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IExtractIcon
    {
        [PreserveSig]
        int GetIconLocation(
            GILFLAGS1 uFlags,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder szIconFile,
            int cchMax,
            out int piIndex,
            out GILFLAGS2 pwFlags);

        [PreserveSig]
        int Extract(
            [In(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPathBuffer,
            int nIconIndex,
            out IntPtr phiconLarge,
            out IntPtr phiconSmall,
            uint nIconSize);
    }

    [Flags]
    public enum SHCONTF : uint
    {
        SHCONTF_FOLDERS = 0x0020,   // only want folders enumerated (SFGAO_FOLDER)
        SHCONTF_NONFOLDERS = 0x0040,   // include non folders
        SHCONTF_INCLUDEHIDDEN = 0x0080,   // show items normally hidden
        SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,   // allow EnumObject() to return before validating enum
        SHCONTF_NETPRINTERSRCH = 0x0200,   // hint that client is looking for printers
        SHCONTF_SHAREABLE = 0x0400,   // hint that client is looking sharable resources (remote shares)
        SHCONTF_STORAGE = 0x0800,   // include all items with accessible storage and their ancestors
    }

    [Flags]
    public enum SHGNO : uint
    {
        SHGDN_NORMAL = 0x0000,  // default (display purpose)
        SHGDN_INFOLDER = 0x0001,  // displayed under a folder (relative)
        SHGDN_FOREDITING = 0x1000,  // for in-place editing
        SHGDN_FORADDRESSBAR = 0x4000,  // UI friendly parsing name (remove ugly stuff)
        SHGDN_FORPARSING = 0x8000,  // parsing name for ParseDisplayName()
    }

    [Flags]
    public enum SFGAOF : uint
    {
        SFGAO_CANCOPY = 0x1,                // Objects can be copied    (DROPEFFECT_COPY)
        SFGAO_CANMOVE = 0x2,                // Objects can be moved     (DROPEFFECT_MOVE)
        SFGAO_CANLINK = 0x4,                // Objects can be linked    (DROPEFFECT_LINK)
        SFGAO_STORAGE = 0x00000008,         // supports BindToObject(IID_IStorage)
        SFGAO_CANRENAME = 0x00000010,         // Objects can be renamed
        SFGAO_CANDELETE = 0x00000020,         // Objects can be deleted
        SFGAO_HASPROPSHEET = 0x00000040,         // Objects have property sheets
        SFGAO_DROPTARGET = 0x00000100,         // Objects are drop target
        SFGAO_CAPABILITYMASK = 0x00000177,
        SFGAO_ENCRYPTED = 0x00002000,         // object is encrypted (use alt color)
        SFGAO_ISSLOW = 0x00004000,         // 'slow' object
        SFGAO_GHOSTED = 0x00008000,         // ghosted icon
        SFGAO_LINK = 0x00010000,         // Shortcut (link)
        SFGAO_SHARE = 0x00020000,         // shared
        SFGAO_READONLY = 0x00040000,         // read-only
        SFGAO_HIDDEN = 0x00080000,         // hidden object
        SFGAO_DISPLAYATTRMASK = 0x000FC000,
        SFGAO_FILESYSANCESTOR = 0x10000000,         // may contain children with SFGAO_FILESYSTEM
        SFGAO_FOLDER = 0x20000000,         // support BindToObject(IID_IShellFolder)
        SFGAO_FILESYSTEM = 0x40000000,         // is a win32 file system object (file/folder/root)
        SFGAO_HASSUBFOLDER = 0x80000000,         // may contain children with SFGAO_FOLDER
        SFGAO_CONTENTSMASK = 0x80000000,
        SFGAO_VALIDATE = 0x01000000,         // invalidate cached information
        SFGAO_REMOVABLE = 0x02000000,         // is this removeable media?
        SFGAO_COMPRESSED = 0x04000000,         // Object is compressed (use alt color)
        SFGAO_BROWSABLE = 0x08000000,         // supports IShellFolder, but only implements CreateViewObject() (non-folder view)
        SFGAO_NONENUMERATED = 0x00100000,         // is a non-enumerated object
        SFGAO_NEWCONTENT = 0x00200000,         // should show bold in explorer tree
        SFGAO_CANMONIKER = 0x00400000,         // defunct
        SFGAO_HASSTORAGE = 0x00400000,         // defunct
        SFGAO_STREAM = 0x00400000,         // supports BindToObject(IID_IStream)
        SFGAO_STORAGEANCESTOR = 0x00800000,         // may contain children with SFGAO_STORAGE or SFGAO_STREAM
        SFGAO_STORAGECAPMASK = 0x70C50008,         // for determining storage capabilities, ie for open/save semantics
    }

    public enum EIEIFLAG
    {
        IEIFLAG_ASYNC = 0x0001,      // ask the extractor if it supports ASYNC extract (free threaded)
        IEIFLAG_CACHE = 0x0002,      // returned from the extractor if it does NOT cache the thumbnail
        IEIFLAG_ASPECT = 0x0004,      // passed to the extractor to beg it to render to the aspect ratio of the supplied rect
        IEIFLAG_OFFLINE = 0x0008,      // if the extractor shouldn't hit the net to get any content neede for the rendering
        IEIFLAG_GLEAM = 0x0010,     // does the image have a gleam ? this will be returned if it does
        IEIFLAG_SCREEN = 0x0020,      // render as if for the screen  (this is exlusive with IEIFLAG_ASPECT )
        IEIFLAG_ORIGSIZE = 0x0040,      // render to the approx size passed, but crop if neccessary
        IEIFLAG_NOSTAMP = 0x0080,      // returned from the extractor if it does NOT want an icon stamp on the thumbnail
        IEIFLAG_NOBORDER = 0x0100,      // returned from the extractor if it does NOT want an a border around the thumbnail
        IEIFLAG_QUALITY = 0x0200      // passed to the Extract method to indicate that a slower, higher quality image is desired, re-compute the thumbnail
    }

    [Flags]
    public enum GILFLAGS1
    {
        GIL_OPENICON = 1,
        GIL_FORSHELL = 2,
        GIL_ASYNC = 32,
        GIL_DEFAULTICON = 64,
        GIL_FORSHORTCUT = 128
    }

    [Flags]
    public enum GILFLAGS2
    {
        GIL_None = 0,
        GIL_SIMULATEDOC = 1,
        GIL_PERINSTANCE = 2,
        GIL_PERCLASS = 4,
        GIL_NOTFILENAME = 8,
        GIL_DONTCACHE = 16
    }

    [Flags]
    public enum SHGFIFLAGS
    {
        SHGFI_ICON = 0x000000100,     // get icon
        SHGFI_DISPLAYNAME = 0x000000200,     // get display name
        SHGFI_TYPENAME = 0x000000400,     // get type name
        SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
        SHGFI_ICONLOCATION = 0x000001000,     // get icon location
        SHGFI_EXETYPE = 0x000002000,     // return exe type
        SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
        SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
        SHGFI_SELECTED = 0x000010000,     // show icon in selected state
        SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
        SHGFI_LARGEICON = 0x000000000,     // get large icon
        SHGFI_SMALLICON = 0x000000001,     // get small icon
        SHGFI_OPENICON = 0x000000002,     // get open icon
        SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
        SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
        SHGFI_USEFILEATTRIBUTES = 0x000000010,     // use passed dwFileAttribute

        SHGFI_ADDOVERLAYS = 0x000000020,     // apply the appropriate overlays
        SHGFI_OVERLAYINDEX = 0x000000040      // Get the index of the overlay
    }

    [StructLayout(LayoutKind.Explicit, Size = 264)]
    public struct STRRET
    {
        [FieldOffset(0)]
        public UInt32 uType;    // One of the STRRET_* values

        [FieldOffset(4)]
        public IntPtr pOleStr;    // must be freed by caller of GetDisplayNameOf

        [FieldOffset(4)]
        public IntPtr pStr;        // NOT USED

        [FieldOffset(4)]
        public UInt32 uOffset;    // Offset into SHITEMID

        [FieldOffset(4)]
        public IntPtr cStr;        // Buffer to fill in (ANSI)
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F2-0000-0000-C000-000000000046")]
    public interface IEnumIDList
    {
        [PreserveSig()]
        int Next(
            int celt,
            out IntPtr rgelt,
            out int pceltFetched);

        [PreserveSig()]
        uint Skip(
            uint celt);

        [PreserveSig()]
        uint Reset();

        [PreserveSig()]
        uint Clone(
            out IEnumIDList ppenum);
    }

    [ComImportAttribute()]
    [GuidAttribute("00000002-0000-0000-C000-000000000046")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMalloc
    {
        [PreserveSig]
        IntPtr Alloc(int cb);

        [PreserveSig]
        IntPtr Realloc(
            IntPtr pv,
            int cb);

        [PreserveSig]
        void Free(IntPtr pv);

        [PreserveSig]
        int GetSize(IntPtr pv);

        [PreserveSig]
        int DidAlloc(IntPtr pv);

        [PreserveSig]
        void HeapMinimize();
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };



}
