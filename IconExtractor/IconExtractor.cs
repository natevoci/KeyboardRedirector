using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace IconExtractor
{
    public class IconExtractor
    {
        private IMalloc _allocator = null;

        public IMalloc Allocator
        {
            get
            {
                if (_allocator == null)
                {
                    Shell32.SHGetMalloc(out _allocator);
                }
                return _allocator;
            }
        }

        public Icon ExtractAssociatedIcon(string filename)
        {
            int index = 0;
            StringBuilder sbFilename = new StringBuilder(260);
            sbFilename.Append(filename);
            IntPtr hico = Shell32.ExtractAssociatedIcon(IntPtr.Zero, sbFilename, ref index);
            return Icon.FromHandle(hico);
        }

        public enum LoadIconFlags
        {
            None = 0x0,
            LargeIcon = 0x1,
            SmallIcon = 0x2,
            OpenIcon = 0x4
        }

        public Icon LoadIcon(string filename, LoadIconFlags flags)
        {
            SHFILEINFO fileinfo = new SHFILEINFO();
            SHGFIFLAGS gfiflags = SHGFIFLAGS.SHGFI_ICON;// | SHGFIFLAGS.SHGFI_ADDOVERLAYS;
            if ((flags & LoadIconFlags.LargeIcon) != 0)
                gfiflags |= SHGFIFLAGS.SHGFI_LARGEICON;
            else if ((flags & LoadIconFlags.SmallIcon) != 0)
                gfiflags |= SHGFIFLAGS.SHGFI_SMALLICON;
            if ((flags & LoadIconFlags.OpenIcon) != 0)
                gfiflags |= SHGFIFLAGS.SHGFI_OPENICON;

            int hres = Shell32.SHGetFileInfo(filename, 0, ref fileinfo, Marshal.SizeOf(fileinfo), gfiflags);
            Icon ico = Icon.FromHandle(fileinfo.hIcon);

            Bitmap bm1 = ico.ToBitmap();
            //Debug.Write("Icon " + bm1.Size.ToString() + " : " + bm1.PixelFormat.ToString() + "\r\n");
            return ico;
        }

        public Bitmap LoadThumbnailFromImageFactory(string filename, Size requestedSize, bool onlyThumbnail)
        {
            Bitmap bm1 = null;
            IntPtr hbitmap = IntPtr.Zero;
            SIZE sz = new SIZE(requestedSize.Width, requestedSize.Height);

            IShellFolder desktop = null;
            IntPtr pidlMain = IntPtr.Zero;
            IShellItem shellItem = null;
            IShellItemImageFactory ppsiShellItemImageFactory = null;

            try
            {
                Shell32.SHGetDesktopFolder(ref desktop);

                int cParsed = 0;
                int pdwAttrib = 0;
                desktop.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, filename, out cParsed, out pidlMain, out pdwAttrib);

                //Shell32.SHCreateItemFromIDList(pidlMain, uuidIShellItem, out shellItem);
                Shell32.SHCreateShellItem(IntPtr.Zero, null, pidlMain, out shellItem);

                ppsiShellItemImageFactory = (IShellItemImageFactory)shellItem;
                if (onlyThumbnail)
                    ppsiShellItemImageFactory.GetImage(sz, SIIGBF.SIIGBF_BIGGERSIZEOK | SIIGBF.SIIGBF_THUMBNAILONLY, out hbitmap);
                else
                    ppsiShellItemImageFactory.GetImage(sz, SIIGBF.SIIGBF_BIGGERSIZEOK, out hbitmap);

                bm1 = GetBitmapFromHbitmap(hbitmap);
            }
            catch (InvalidCastException)
            {
                Debug.Write("    Did not support IShellItemImageFactory\r\n");
            }
            catch (Exception e)
            {
                Debug.Write("    Exception extracting image from IShellItemImageFactory: " + e.Message + "\r\n");
            }
            finally
            {
                if (ppsiShellItemImageFactory != null)
                {
                    Marshal.ReleaseComObject(ppsiShellItemImageFactory);
                    ppsiShellItemImageFactory = null;
                }
                if (shellItem != null)
                {
                    Marshal.ReleaseComObject(shellItem);
                    shellItem = null;
                }
                if (pidlMain != IntPtr.Zero)
                {
                    Allocator.Free(pidlMain);
                    pidlMain = IntPtr.Zero;
                }
                if (desktop != null)
                {
                    Marshal.ReleaseComObject(desktop);
                    desktop = null;
                }
                if (_allocator != null)
                {
                    Marshal.ReleaseComObject(_allocator);
                    _allocator = null;
                }
            }

            return bm1;
        }

        public Bitmap LoadThumbnailFromExtractImage(string filename, Size requestedSize)
        {
            Bitmap bm1 = null;
            IntPtr hbitmap = IntPtr.Zero;
            SIZE sz = new SIZE(requestedSize.Width, requestedSize.Height);

            IShellFolder desktop = null;
            IntPtr pidlParent = IntPtr.Zero;
            IShellFolder shellFolder = null;
            IUnknown iunk = null;
            IExtractImage extractImage = null;

            try
            {
                Debug.Write("Trying IExtractImage\r\n");

                Shell32.SHGetDesktopFolder(ref desktop);

                int cParsed = 0;
                int pdwAttrib = 0;
                string filePath = filename;
                if (System.IO.File.Exists(filename))
                    filePath = System.IO.Path.GetDirectoryName(filename);
                desktop.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, filePath, out cParsed, out pidlParent, out pdwAttrib);

                Guid uuidShellFolder = new Guid("000214E6-0000-0000-C000-000000000046");
                desktop.BindToObject(pidlParent, IntPtr.Zero, ref uuidShellFolder, out shellFolder);

                Guid uuidExtractImage = new Guid("BB2E617C-0920-11d1-9A0B-00C04FC2D6C1");
                iunk = GetUIObject(shellFolder, filename, uuidExtractImage);

                if (iunk == null)
                {
                    Debug.Write("    IExtractImage not supported\r\n");
                    return null;
                }

                extractImage = (IExtractImage)iunk;
                StringBuilder location = new StringBuilder(260, 260);
                int priority = 0;
                EIEIFLAG flags = EIEIFLAG.IEIFLAG_ORIGSIZE;
                int uFlags = (int)flags;
                extractImage.GetLocation(location, location.Capacity, ref priority, ref sz, 32, ref uFlags);
                extractImage.Extract(out hbitmap);
                bm1 = GetBitmapFromHbitmap(hbitmap);
            }
            catch (Exception e)
            {
                Debug.Write("    Exception extracting image from IExtractImage: " + e.Message + "\r\n");
            }
            finally
            {
                if (extractImage != null)
                {
                    Marshal.ReleaseComObject(extractImage);
                    extractImage = null;
                }
                if (iunk != null)
                {
                    Marshal.ReleaseComObject(iunk);
                    iunk = null;
                }
                if (shellFolder != null)
                {
                    Marshal.ReleaseComObject(shellFolder);
                    shellFolder = null;
                }
                if (pidlParent != IntPtr.Zero)
                {
                    Allocator.Free(pidlParent);
                    pidlParent = IntPtr.Zero;
                }
                if (desktop != null)
                {
                    Marshal.ReleaseComObject(desktop);
                    desktop = null;
                }
                if (_allocator != null)
                {
                    Marshal.ReleaseComObject(_allocator);
                    _allocator = null;
                }
            }
            return bm1;
        }

        public Bitmap LoadImageFromExtractIcon(string filename)
        {
            Bitmap bm1 = null;
            IntPtr hbitmap = IntPtr.Zero;

            IShellFolder desktop = null;
            IntPtr pidlParent = IntPtr.Zero;
            IShellFolder shellFolder = null;
            IUnknown iunk = null;
            IExtractIcon extractIcon = null;

            try
            {
                Debug.Write("Trying IExtractIcon\r\n");

                Shell32.SHGetDesktopFolder(ref desktop);

                int cParsed = 0;
                int pdwAttrib = 0;
                string filePath = filename;
                if (System.IO.File.Exists(filename))
                    filePath = System.IO.Path.GetDirectoryName(filename);
                desktop.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, filePath, out cParsed, out pidlParent, out pdwAttrib);

                Guid uuidShellFolder = new Guid("000214E6-0000-0000-C000-000000000046");
                desktop.BindToObject(pidlParent, IntPtr.Zero, ref uuidShellFolder, out shellFolder);

                Guid uuidExtractIcon = new Guid("000214eb-0000-0000-c000-000000000046");
                iunk = GetUIObject(shellFolder, filename, uuidExtractIcon);

                if (iunk == null)
                {
                    Debug.Write("    IExtractIcon not supported\r\n");
                    return null;
                }

                int hres = 0;
                extractIcon = (IExtractIcon)iunk;
                StringBuilder location = new StringBuilder(260, 260);
                int iconIndex = 0;
                GILFLAGS2 flags = GILFLAGS2.GIL_None;
                hres = extractIcon.GetIconLocation(0, location, location.Capacity, out iconIndex, out flags);
                IntPtr iconLarge = IntPtr.Zero;
                IntPtr iconSmall = IntPtr.Zero;
                uint iconSize = (16 | (48 << 16));
                hres = extractIcon.Extract(location, iconIndex, out iconLarge, out iconSmall, iconSize);
                Icon ico = Icon.FromHandle(iconLarge);
                bm1 = ico.ToBitmap();
            }
            catch (Exception e)
            {
                Debug.Write("    Exception extracting image from IExtractIcon: " + e.Message + "\r\n");
            }
            finally
            {
                if (extractIcon != null)
                {
                    Marshal.ReleaseComObject(extractIcon);
                    extractIcon = null;
                }
                if (iunk != null)
                {
                    Marshal.ReleaseComObject(iunk);
                    iunk = null;
                }
                if (shellFolder != null)
                {
                    Marshal.ReleaseComObject(shellFolder);
                    shellFolder = null;
                }
                if (pidlParent != IntPtr.Zero)
                {
                    Allocator.Free(pidlParent);
                    pidlParent = IntPtr.Zero;
                }
                if (desktop != null)
                {
                    Marshal.ReleaseComObject(desktop);
                    desktop = null;
                }
                if (_allocator != null)
                {
                    Marshal.ReleaseComObject(_allocator);
                    _allocator = null;
                }
            }
            return bm1;
        }


        private IUnknown GetUIObject(IShellFolder shellFolder, string filename, Guid uuid)
        {
            IUnknown iunk = null;
            if (System.IO.File.Exists(filename))
            {
                IntPtr pidl = IntPtr.Zero;
                int cParsed = 0;
                int pdwAttrib = 0;
                shellFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, System.IO.Path.GetFileName(filename), out cParsed, out pidl, out pdwAttrib);
                if (pidl != IntPtr.Zero)
                {
                    int prgf = 0;
                    shellFolder.GetUIObjectOf(IntPtr.Zero, 1, ref pidl, ref uuid, out prgf, ref iunk);
                }
            }
            else
            {
                shellFolder.CreateViewObject(IntPtr.Zero, ref uuid, ref iunk);
            }
            return iunk;
        }

        private System.Drawing.Bitmap GetBitmapFromHbitmap(IntPtr hbitmap)
        {
            Bitmap bm1 = Image.FromHbitmap(hbitmap);

            System.Drawing.Imaging.BitmapData[] bmData = new System.Drawing.Imaging.BitmapData[2];
            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(0, 0, bm1.Width, bm1.Height);
            bmData[0] = bm1.LockBits(bounds, System.Drawing.Imaging.ImageLockMode.ReadOnly, bm1.PixelFormat);

            //Debug.Write("Bitmap " + bounds.ToString() + " : Stride=" + bmData[0].Stride + " : " + bm1.PixelFormat.ToString() + "\r\n");

            System.Drawing.Bitmap bm2 = new System.Drawing.Bitmap(bmData[0].Width, bmData[0].Height, bmData[0].Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, bmData[0].Scan0);
            bmData[1] = bm2.LockBits(bounds, System.Drawing.Imaging.ImageLockMode.WriteOnly, bm2.PixelFormat);

            Marshal.StructureToPtr(bmData[0].Scan0, bmData[1].Scan0, true);
            bm2.UnlockBits(bmData[1]);
            bm1.UnlockBits(bmData[0]);

            // Test to make sure the entire image isn't transparent
            bool transparent = true;
            for (int y = 0; y < bm2.Height; y++)
            {
                for (int x = 0; x < bm2.Width; x++)
                {
                    // For some strange reason the pixel at 0,0 sometimes has an alpha value
                    // when the rest of the image doesn't
                    if ((x > 0) && (y > 0))
                    {
                        byte alpha = bm2.GetPixel(x, y).A;
                        //Debug.Write(alpha.ToString() + ",");
                        if (alpha > 0)
                        {
                            transparent = false;
                            break;
                        }
                    }
                }
                //Debug.Write("\r\n");
            }

            if (transparent == false)
            {
                //Debug.Write("Alpha channel found\r\n");
                return bm2;
            }

            // If the entire image is transparent we return the original
            //Debug.Write("Entire image transparent. Using original.\r\n");

            return bm1;
        }

    }
}
