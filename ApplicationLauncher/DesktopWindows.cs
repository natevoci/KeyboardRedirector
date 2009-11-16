using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using MS;

namespace ApplicationLauncher
{
    class DesktopWindows
    {
        List<Window> _windows = new List<Window>();

        public DesktopWindows()
        {
            Refresh();
        }

        public List<Window> Windows
        {
            get { return _windows; }
        }

        public void Refresh()
        {
            _windows.Clear();

            Win32.EnumWindowsProc proc = new Win32.EnumWindowsProc(EnumWindowsCallback);
            IntPtr hDesktop = IntPtr.Zero; // Current Desktop
            bool success = Win32.EnumDesktopWindows(hDesktop, proc, IntPtr.Zero);

            if (!success)
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private bool EnumWindowsCallback(IntPtr hwnd, int lParam)
        {
            string title = Win32.GetWindowText(hwnd);

            if (Win32.IsWindowVisible(hwnd) == false)
                return true;

            IntPtr parentHwnd = Win32.GetAncestor(hwnd, Win32.GA.ROOTOWNER);
            if (hwnd != parentHwnd)
                return true;

            IntPtr stylePtr = Win32.GetWindowLongPtr(hwnd, Win32.GWL.STYLE);
            Win32.WindowStyles style = (Win32.WindowStyles)(stylePtr.ToInt64() & 0xFFFFFFFF);

            IntPtr exStylePtr = Win32.GetWindowLongPtr(hwnd, Win32.GWL.EXSTYLE);
            Win32.WindowExStyles exStyle = (Win32.WindowExStyles)(exStylePtr.ToInt64() & 0xFFFFFFFF);

            if ((exStyle & Win32.WindowExStyles.WS_EX_TOOLWINDOW) != 0)
                return true;
            if ((exStyle & Win32.WindowExStyles.WS_EX_APPWINDOW) == 0)
            {
                int test = ((int)style & 0xFFFF);
                if (test > 0)
                {
                    // Have no idea what these first 16 bits of the style flag are. They've not
                    // documented, but Reaper and Spy++ use them and they stay visible in the
                    // taskbar, so if these are set I'll assume we show the app.
                }
                else if ((style & Win32.WindowStyles.WS_MINIMIZE) != 0)
                {
                    //return true;
                }
            }

            _windows.Add(new Window(hwnd));
            return true;
        }


        public class Window : IDisposable
        {
            private IntPtr _hwnd;
            private string _title = null;
            private string _executable = null;
            private string _cmdLine = null;
            private Icon _iconSmall = null;
            private Icon _iconLarge = null;

            public Window(IntPtr hwnd)
            {
                _hwnd = hwnd;
            }

            #region IDisposable Members
            public void Dispose()
            {
                if (_iconSmall != null)
                {
                    _iconSmall.Dispose();
                    _iconSmall = null;
                }

            }
            #endregion

            public IntPtr Hwnd
            {
                get
                {
                    return _hwnd;
                }
            }

            public string Title
            {
                get
                {
                    if (_title == null)
                    {
                        _title = Win32.GetWindowText(_hwnd);
                    }
                    return _title;
                }
            }

            public string Executable
            {
                get
                {
                    if (_executable == null)
                    {
                        uint processId = 0;
                        Win32.GetWindowThreadProcessId(_hwnd, out processId);

                        _executable = Win32.GetProcessExecutableName((int)processId);
                    }
                    return _executable;
                }
            }

            public string CmdLine
            {
                get
                {
                    if (_cmdLine == null)
                    {
                        uint processId = 0;
                        Win32.GetWindowThreadProcessId(_hwnd, out processId);

                        _cmdLine = Win32.GetProcessCmdLine((int)processId);
                    }
                    return _cmdLine;
                }
            }

            public Icon IconSmall
            {
                get
                {
                    if (_iconSmall == null)
                    {
                        IntPtr hIcon = IntPtr.Zero;

                        if (hIcon == IntPtr.Zero)
                            hIcon = Win32.SendMessage(_hwnd, (uint)Win32.WM.GETICON, IntPtr.Zero, IntPtr.Zero);
                        if (hIcon == IntPtr.Zero)
                            hIcon = Win32.GetClassLongPtr(_hwnd, Win32.GCL.HICONSM);

                        if (hIcon != IntPtr.Zero)
                            _iconSmall = Icon.FromHandle(hIcon);
                    }
                    return _iconSmall;
                }
            }
            public Icon IconLarge
            {
                get
                {
                    if (_iconLarge == null)
                    {
                        IntPtr hIcon = IntPtr.Zero;

                        if (hIcon == IntPtr.Zero)
                            hIcon = Win32.SendMessage(_hwnd, (uint)Win32.WM.GETICON, new IntPtr(1), IntPtr.Zero);
                        if (hIcon == IntPtr.Zero)
                            hIcon = Win32.GetClassLongPtr(_hwnd, Win32.GCL.HICON);

                        if (hIcon != IntPtr.Zero)
                            _iconLarge = Icon.FromHandle(hIcon);
                    }
                    return _iconLarge;
                }
            }

        }

    }
}
