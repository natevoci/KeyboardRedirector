// <copyright file="NativeMethods.cs" company="Nicholas Piasecki"> 
// Copyright (c) 2009 by Nicholas Piasecki All rights reserved. 
// </copyright>

namespace RawInput
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Contains native methods invoked via P/Invoke to the underlying Windows
    /// operating system.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// An invalid handle value.
        /// </summary>
        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        /// <summary>
        /// The GUID for the HID device interface class.
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_HID = new Guid("{4D1E55B2-F16F-11CF-88CB-001111000030}");

        /// <summary>
        /// Window message for raw input.
        /// </summary>
        internal const int WM_INPUT = 0xFF;

        /// <summary>
        /// Window message for key down event.
        /// </summary>
        internal const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// Delegate for a low level keyboard proc used in conjunction with the
        /// SetWindowsHookEx() method.
        /// </summary>
        /// <param name="nCode">[in] Specifies a code the hook procedure uses 
        /// to determine how to process the message. If nCode is less than
        /// zero, the hook procedure must pass the message to the CallNextHookEx 
        /// function without further processing and should return the value 
        /// returned by CallNextHookEx.</param>
        /// <param name="wParam">in] Specifies the identifier of the keyboard
        /// message. This parameter can be one of the following messages: 
        /// WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP.</param>
        /// <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure.</param>
        /// <returns>If nCode is less than zero, the hook procedure must return 
        /// the value returned by CallNextHookEx.
        /// If nCode is greater than or equal to zero, and the hook
        /// procedure did not process the message, it is highly recommended 
        /// that you call CallNextHookEx and return the value it returns; 
        /// otherwise, other applications that have installed WH_KEYBOARD_LL 
        /// hooks will not receive hook notifications and may behave incorrectly 
        /// as a result. If the hook procedure processed the message, it may 
        /// return a nonzero value to prevent the system from passing the 
        /// message to the rest of the hook chain or the target window 
        /// procedure.</returns>
        internal delegate IntPtr LowLevelKeyboardProc(
            int nCode,
            IntPtr wParam, 
            IntPtr lParam);

        /// <summary>
        /// Enumeration of removal options.
        /// </summary>
        internal enum PeekMessageRemoveFlag : uint
        {
            /// <summary>
            /// Messages are not removed from the queue after processing by PeekMessage.
            /// </summary>
            PM_NOREMOVE = 0x0,

            /// <summary>
            /// Messages are removed from the queue after processing by PeekMessage.
            /// </summary>
            PM_REMOVE = 0x1
        }

        /// <summary>
        /// Enumeration of command flags for the GetRawInputData function.
        /// </summary>
        internal enum RawInputCommandFlag : uint
        {
            /// <summary>
            /// Get the raw data from the RAWINPUT structure.
            /// </summary>
            RID_INPUT = 0x10000003,

            /// <summary>
            /// Get the header information from the RAWINPUT structure.
            /// </summary>
            RID_HEADER = 0x10000005
        }

        /// <summary>
        /// Enumeration of command flags for GetRawInputDeviceInfo.
        /// </summary>
        internal enum RawInputDeviceInfoCommand : uint
        {
            /// <summary>
            /// pData points to the previously parsed data.
            /// </summary>
            RIDI_PREPARSEDDATA = 0x20000005,

            /// <summary>
            /// pData points to a string that contains the device name.
            /// For this uiCommand only, the value in pcbSize is the character 
            /// count (not the byte count).
            /// </summary>
            RIDI_DEVICENAME = 0x20000007,

            /// <summary>
            /// pData points to an RID_DEVICE_INFO structure.
            /// </summary>
            RIDI_DEVICEINFO = 0x2000000B   
        }

        /// <summary>
        /// Enumeration containing flags for a raw input device.
        /// </summary>
        [Flags]
        internal enum RawInputDeviceFlags : uint
        {
            /// <summary>
            /// No flags, the default.
            /// </summary>
            NONE = 0x0,

            /// <summary>
            /// Microsoft Windows XP Service Pack 1 (SP1): If set, the 
            /// application command keys are handled. RIDEV_APPKEYS can be 
            /// specified only if RIDEV_NOLEGACY is specified for a keyboard 
            /// device.
            /// </summary>
            RIDEV_APPKEYS = 0x400,

            /// <summary>
            /// If set, the mouse button click does not activate the other
            /// window.
            /// </summary>
            RIDEV_CAPTUREMOUSE = 0x200,

            /// <summary>
            /// If set, this specifies the top level collections to exclude when
            /// reading a complete usage page. This flag only affects a TLC 
            /// whose usage page is already specified with RIDEV_PAGEONLY.
            /// </summary>
            RIDEV_EXCLUDE = 0x10,

            /// <summary>
            /// Windows Vista or later: If set, this enables the caller to 
            /// receive input in the background only if the foreground 
            /// application does not process it. In other words, if the 
            /// foreground application is not registered for raw input, then 
            /// the background application that is registered will receive the 
            /// input.
            /// </summary>
            RIDEV_EXINPUTSINK = 0x1000,

            /// <summary>
            /// If set, this enables the caller to receive the input even when 
            /// the caller is not in the foreground. Note that hwndTarget 
            /// must be specified.
            /// </summary>
            RIDEV_INPUTSINK = 0x100,

            /// <summary>
            /// If set, the application-defined keyboard device hotkeys are not 
            /// handled. However, the system hotkeys; for example, ALT+TAB and 
            /// CTRL+ALT+DEL, are still handled. By default, all keyboard 
            /// hotkeys are handled. RIDEV_NOHOTKEYS can be specified even if 
            /// RIDEV_NOLEGACY is not specified and hwndTarget is NULL.
            /// </summary>
            RIDEV_NOHOTKEYS = 0x200,

            /// <summary>
            /// If set, this prevents any devices specified by usUsagePage or 
            /// usUsage from generating legacy messages. This is only for the 
            /// mouse and keyboard. If RIDEV_NOLEGACY is set for a mouse or a
            /// keyboard, the system does not generate any legacy message for 
            /// that device for the application. For example, if the mouse TLC 
            /// is set with RIDEV_NOLEGACY, WM_LBUTTONDOWN and related legacy
            /// mouse messages are not generated. Likewise, if the keyboard 
            /// TLC is set with RIDEV_NOLEGACY, WM_KEYDOWN and related legacy 
            /// keyboard messages are not generated.
            /// </summary>
            RIDEV_NOLEGACY = 0x30,

            /// <summary>
            /// If set, this specifies all devices whose top level collection 
            /// is from the specified usUsagePage. Note that usUsage must be zero. 
            /// To exclude a particular top level collection, use RIDEV_EXCLUDE.
            /// </summary>
            RIDEV_PAGEONLY = 0x20,

            /// <summary>
            /// If set, this removes the top level collection from the inclusion 
            /// list. This tells the operating system to stop reading from a 
            /// device which matches the top level collection.
            /// </summary>
            RIDEV_REMOVE = 0x1
        }

        /// <summary>
        /// Enumeration containing the type device the raw input is coming from.
        /// </summary>
        internal enum RawInputType : uint
        {
            /// <summary>
            /// Raw input comes from the mouse.
            /// </summary>
            RIM_TYPEMOUSE = 0,

            /// <summary>
            /// Raw input comes from the keyboard.
            /// </summary>
            RIM_TYPEKEYBOARD = 1,

            /// <summary>
            /// Raw input comes from some device that is not a keyboard or a mouse.
            /// </summary>
            RIM_TYPEHID = 2
        }

        /// <summary>
        /// Enumeration of properties that can be retrieved via a call to
        /// SetupDiGetDeviceRegistryProperty().
        /// </summary>
        internal enum SetupDiGetDeviceRegistryPropertyType : uint
        {
            /// <summary>
            /// The function retrieves a description of the device.
            /// </summary>
            SPDRP_DEVICEDESC = 0x0,

            /// <summary>
            /// The function retrieves a REG_SZ string that contains the friendly
            /// name of the device.
            /// </summary>
            SPDRP_FRIENDLYNAME = 0xC
        }

        /// <summary>
        /// Enumeration of flags for getting class devs.
        /// </summary>
        [Flags]
        internal enum SetupDiGetClassDevsFlags : uint
        {
            /// <summary>
            /// Return only the device that is associated with the system default
            /// device interface, if one is set, for the specified device interface classes.
            /// </summary>
            DIGCF_DEFAULT = 0x1,

            /// <summary>
            /// Return only devices that are currently present in a system.
            /// </summary>
            DIGCF_PRESENT = 0x2,

            /// <summary>
            /// Return a list of installed devices for all device setup classes 
            /// or all device interface classes.
            /// </summary>
            DIGCF_ALLCLASSES = 0x4,

            /// <summary>
            /// Return only devices that are a part of the current hardware profile.
            /// </summary>
            DIGCF_PROFILE = 0x8,

            /// <summary>
            /// Return devices that support device interfaces for the specified 
            /// device interface classes. This flag must be set in the Flags 
            /// parameter if the Enumerator parameter specifies a device instance ID.
            /// </summary>
            DIGCF_DEVICEINTERFACE = 0x10
        }

        /// <summary>
        /// The various hook types that can be used with the SetWindowsHookEx()
        /// method.
        /// </summary>
        internal enum SetWindowsHookExHookType : int
        {
            /// <summary>
            /// Installs a hook procedure that records input messages posted 
            /// to the system message queue. This hook is useful for recording 
            /// macros. For more information, see the JournalRecordProc 
            /// hook procedure.
            /// </summary>
            WH_JOURNALRECORD = 0,

            /// <summary>
            /// Installs a hook procedure that posts messages previously 
            /// recorded by a WH_JOURNALRECORD hook procedure. For more 
            /// information, see the JournalPlaybackProc hook procedure.
            /// </summary>
            WH_JOURNALPLAYBACK = 1,

            /// <summary>
            /// Installs a hook procedure that monitors keystroke messages.
            /// For more information, see the KeyboardProc hook procedure.
            /// </summary>
            WH_KEYBOARD = 2,

            /// <summary>
            /// Installs a hook procedure that monitors messages posted to a
            /// message queue. For more information, see the GetMsgProc hook 
            /// procedure.
            /// </summary>
            WH_GETMESSAGE = 3,

            /// <summary>
            /// Installs a hook procedure that monitors messages before the 
            /// system sends them to the destination window procedure. For 
            /// more information, see the CallWndProc hook procedure.
            /// </summary>
            WH_CALLWNDPROC = 4,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful to
            /// a computer-based training (CBT) application. For more 
            /// information, see the CBTProc hook procedure.
            /// </summary>
            WH_CBT = 5,

            /// <summary>
            /// Installs a hook procedure that monitors messages generated 
            /// as a result of an input event in a dialog box, message box, 
            /// menu, or scroll bar. The hook procedure monitors these 
            /// messages for all applications in the same desktop as the 
            /// calling thread. For more information, see the SysMsgProc 
            /// hook procedure.
            /// </summary>
            WH_SYSMSGFILTER = 6,

            /// <summary>
            /// Installs a hook procedure that monitors mouse messages. For more 
            /// information, see the MouseProc hook procedure.
            /// </summary>
            WH_MOUSE = 7,

            /// <summary>
            /// This hook is not documented.
            /// </summary>
            WH_HARDWARE = 8,

            /// <summary>
            /// Installs a hook procedure useful for debugging other hook 
            /// procedures. For more information, see the DebugProc hook 
            /// procedure.
            /// </summary>
            WH_DEBUG = 9,

            /// <summary>
            /// Installs a hook procedure that receives notifications useful to 
            /// shell applications. For more information, see the ShellProc 
            /// hook procedure.
            /// </summary>
            WH_SHELL = 10,

            /// <summary>
            /// Installs a hook procedure that will be called when the 
            /// application's foreground thread is about to become idle. 
            /// This hook is useful for performing low priority tasks during 
            /// idle time. For more information, see the ForegroundIdleProc 
            /// hook procedure.
            /// </summary>
            WH_FOREGROUNDIDLE = 11,

            /// <summary>
            /// Installs a hook procedure that monitors messages after they 
            /// have been processed by the destination window procedure. For 
            /// more information, see the CallWndRetProc hook procedure.
            /// </summary>
            WH_CALLWNDPROCRET = 12, 
   
            /// <summary>
            /// Windows NT/2000/XP: Installs a hook procedure that monitors 
            /// low-level keyboard input events. For more information, see the
            /// LowLevelKeyboardProc hook procedure.
            /// </summary>
            WH_KEYBOARD_LL = 13,
            
            /// <summary>
            /// Windows NT/2000/XP: Installs a hook procedure that monitors 
            /// low-level mouse input events. For more information, 
            /// see the LowLevelMouseProc hook procedure.
            /// </summary>
            WH_MOUSE_LL = 14
        }

        /// <summary>
        /// Passes the hook information to the next hook procedure in the 
        /// current hook chain. A hook procedure can call this function either 
        /// before or after processing the hook information.
        /// </summary>
        /// <param name="hhk">[in] Windows 95/98/ME: Handle to the current hook. 
        /// An application receives this handle as a result of a previous call 
        /// to the SetWindowsHookEx function. Windows NT/XP/2003: Ignored.</param>
        /// <param name="nCode">[in] Specifies the hook code passed to the 
        /// current hook procedure. The next hook procedure uses this code to 
        /// determine how to process the hook information.</param>
        /// <param name="wParam">[in] Specifies the wParam value passed to 
        /// the current hook procedure. The meaning of this parameter depends 
        /// on the type of hook associated with the current hook chain.</param>
        /// <param name="lParam">[in] Specifies the lParam value passed to the 
        /// current hook procedure. The meaning of this parameter depends on 
        /// the type of hook associated with the current hook chain.</param>
        /// <returns>undocumented return value</returns>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811",
            Justification = "We might find use for this again someday.")]
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(
            IntPtr hhk, 
            int nCode,
            IntPtr wParam, 
            IntPtr lParam);

        /// <summary>
        /// Copies the status of the 256 virtual keys to the specified buffer.
        /// </summary>
        /// <param name="lpKeyState">[in] Pointer to the 256-byte array that 
        /// receives the status data for each virtual key.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If 
        /// the function fails, the return value is zero. To get extended
        /// error information, call GetLastError.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// Retrieves a module handle for the specified module. 
        /// The module must have been loaded by the calling process.
        /// </summary>
        /// <param name="lpModuleName">The name of the loaded module (either a 
        /// .dll or .exe file). If the file name extension is omitted, the 
        /// default library extension .dll is appended. The file name string 
        /// can include a trailing point character (.) to indicate that the 
        /// module name has no extension. The string does not have to specify 
        /// a path. When specifying a path, be sure to use backslashes (\), 
        /// not forward slashes (/). The name is compared (case independently)
        /// to the names of modules currently mapped into the address space of 
        /// the calling process. If this parameter is NULL, GetModuleHandle
        /// returns a handle to the file used to create the calling process 
        /// (.exe file). The GetModuleHandle function does not retrieve handles 
        /// for modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag.
        /// For more information, see LoadLibraryEx.</param>
        /// <returns>If the function succeeds, the return value is a handle to 
        /// the specified module. If the function fails, the return value is 
        /// NULL. To get extended error information, call GetLastError.</returns>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811",
            Justification = "We might find use for this again someday.")]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Gets the raw input from the specified device.
        /// </summary>
        /// <param name="hRawInput">[in] Handle to the RAWINPUT structure. 
        /// This comes from the lParam in WM_INPUT.</param>
        /// <param name="uiCommand">[in] Command flag.</param>
        /// <param name="pData">[out] Pointer to the data that comes from the 
        /// RAWINPUT structure. This depends on the value of uiCommand. If 
        /// pData is NULL, the required size of the buffer is returned in 
        /// *pcbSize.</param>
        /// <param name="pcbSize">[in, out] Pointer to a variable that specifies
        /// the size, in bytes, of the data in pData.</param>
        /// <param name="cbSizeHeader">[in] Size, in bytes, of RAWINPUTHEADER</param>
        /// <returns>If pData is NULL and the function is successful, the return 
        /// value is 0. If pData is not NULL and the function is successful, 
        /// the return value is the number of bytes copied into pData.
        /// If there is an error, the return value is (UINT)-1.</returns>
        [DllImport("User32.dll")]
        internal static extern uint GetRawInputData(
            IntPtr hRawInput, 
            RawInputCommandFlag uiCommand, 
            IntPtr pData, 
            ref uint pcbSize, 
            uint cbSizeHeader);

        /// <summary>
        /// Gets information about the raw input device.
        /// </summary>
        /// <param name="hDevice">[in] Handle to the raw input device. This 
        /// comes from the lParam of the WM_INPUT message, from 
        /// RAWINPUTHEADER.hDevice, or from GetRawInputDeviceList. It can also 
        /// be NULL if an application inserts input data, for example, by using 
        /// SendInput.</param>
        /// <param name="uiCommand">[in] Specifies what data will be returned in pData.</param>
        /// <param name="pData">[in, out] Pointer to a buffer that contains the 
        /// information specified by uiCommand. If uiCommand is RIDI_DEVICEINFO, 
        /// set RID_DEVICE_INFO.cbSize to sizeof(RID_DEVICE_INFO) before 
        /// calling GetRawInputDeviceInfo.</param>
        /// <param name="pcbSize">[in, out] Pointer to a variable that contains 
        /// the size, in bytes, of the data in pData.</param>
        /// <returns>If successful, this function returns a non-negative number 
        /// indicating the number of bytes copied to pData. If pData is not 
        /// large enough for the data, the function returns -1. If pData is NULL, 
        /// the function returns a value of zero. In both of these cases, pcbSize 
        /// is set to the minimum size required for the pData buffer. Call 
        /// GetLastError to identify any other errors.</returns>
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern uint GetRawInputDeviceInfo(
            IntPtr hDevice,
            RawInputDeviceInfoCommand uiCommand, 
            IntPtr pData,
            ref uint pcbSize);


        /// <summary>
        /// Enumerates the raw input devices attached to the system.
        /// </summary>
        /// <param name="pRawInputDeviceList">[out] Pointer to buffer that holds 
        /// an array of RAWINPUTDEVICELIST structures for the devices attached 
        /// to the system. If NULL, the number of devices are returned in 
        /// *puiNumDevices.</param>
        /// <param name="uiNumDevices">[in, out] Pointer to a variable. If 
        /// pRawInputDeviceList is NULL, the function populates this variable 
        /// with the number of devices attached to the system; otherwise, this 
        /// variable specifies the number of RAWINPUTDEVICELIST structures that 
        /// can be contained in the buffer to which pRawInputDeviceList points. 
        /// If this value is less than the number of devices attached to the 
        /// system, the function returns the actual number of devices in this 
        /// variable and fails with ERROR_INSUFFICIENT_BUFFER.</param>
        /// <param name="cbSize">[in] Size of a RAWINPUTDEVICELIST structure.</param>
        /// <returns>If the function is successful, the return value is the 
        /// number of devices stored in the buffer pointed to by pRawInputDeviceList.
        /// On any other error, the function returns (UINT) -1 and GetLastError 
        /// returns the error indication.</returns>
        /// <remarks>The devices returned from this function are the mouse, the 
        /// keyboard, and other Human Interface Device (HID) devices. To get more 
        /// detailed information about the attached devices, call GetRawInputDeviceInfo 
        /// using the hDevice from RAWINPUTDEVICELIST.</remarks>
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern uint GetRawInputDeviceList(
            IntPtr pRawInputDeviceList, 
            ref uint uiNumDevices, 
            uint cbSize);

        /// <summary>
        /// Dispatches incoming sent messages, checks the thread message queue 
        /// for a posted message, and retrieves the message (if any exist).
        /// </summary>
        /// <param name="lpmsg">[out] Pointer to an MSG structure that receives
        /// message information.</param>
        /// <param name="hwnd">[in] Handle to the window whose messages are to be 
        /// retrieved. The window must belong to the current thread.
        /// If hWnd is NULL, PeekMessage retrieves messages for any window 
        /// that belongs to the current thread, and any messages on the current 
        /// thread's message queue whose hwnd value is NULL (see the MSG structure). 
        /// Therefore if hWnd is NULL, both window messages and thread messages
        /// are processed. If hWnd is -1, PeekMessage retrieves only messages
        /// on the current thread's message queue whose hwnd value is NULL,
        /// that is, thread messages as posted by PostMessage (when the hWnd 
        /// parameter is NULL) or PostThreadMessage.</param>
        /// <param name="wMsgFilterMin">in] Specifies the value of the first 
        /// message in the range of messages to be examined. Use WM_KEYFIRST to 
        /// specify the first keyboard message or WM_MOUSEFIRST to specify the 
        /// first mouse message. If wMsgFilterMin and wMsgFilterMax are both 
        /// zero, PeekMessage returns all available messages (that is, no 
        /// range filtering is performed).</param>
        /// <param name="wMsgFilterMax">[in] Specifies the value of the last 
        /// message in the range of messages to be examined. Use WM_KEYLAST
        /// to specify the last keyboard message or WM_MOUSELAST to specify 
        /// the last mouse message. If wMsgFilterMin and wMsgFilterMax are 
        /// both zero, PeekMessage returns all available messages (that is, 
        /// no range filtering is performed).</param>
        /// <param name="wRemoveMsg">[in] Specifies how messages are handled. 
        /// This parameter can be one of the following values.</param>
        /// <returns>whether or not a message was found</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PeekMessage(
            out System.Windows.Forms.Message lpmsg,
            IntPtr hwnd,
            uint wMsgFilterMin,
            uint wMsgFilterMax,
            PeekMessageRemoveFlag wRemoveMsg);

        /// <summary>
        /// Registers the devices that supply the raw input data.
        /// </summary>
        /// <param name="pRawInputDevice">[in] Pointer to an array of 
        /// RAWINPUTDEVICE structures that represent the devices that 
        /// supply the raw input.</param>
        /// <param name="uiNumDevices">[in] Number of RAWINPUTDEVICE structures 
        /// pointed to by pRawInputDevices.</param>
        /// <param name="cbSize">[in] Size, in bytes, of a RAWINPUTDEVICE 
        /// structure.</param>
        /// <returns>TRUE if the function succeeds; otherwise, FALSE. If the 
        /// function fails, call GetLastError for more information</returns>
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterRawInputDevices(
            RAWINPUTDEVICE[] pRawInputDevice, 
            uint uiNumDevices, 
            uint cbSize);

        /// <summary>
        /// Deletes a device information set and frees all associated memory.
        /// </summary>
        /// <param name="hDevInfo">A handle to the device information set to delete.</param>
        /// <returns>The function returns TRUE if it is successful. Otherwise, 
        /// it returns FALSE and the logged error can be retrieved with a 
        /// call to GetLastError.</returns>
        [DllImport(@"setupapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr hDevInfo);

        /// <summary>
        /// Returns a handle to a device information set that contains 
        /// requested device information elements for a local machine.
        /// </summary>
        /// <param name="ClassGuid">A pointer to the GUID for a device setup 
        /// class or a device interface class. This pointer is optional and can
        /// be NULL..</param>
        /// <param name="Enumerator">A pointer to a NULL-terminated string that 
        /// specifies:
        /// <list>
        /// <item>
        /// An identifier (ID) of a Plug and Play (PnP) enumerator. 
        /// This ID can either be the value’s globally unique identifier (GUID) 
        /// or symbolic name. For example, “PCI” can be used to specify the 
        /// PCI PnP value. Other examples of symbolic names for PnP values 
        /// include “USB,” “PCMCIA,” and “SCSI”.
        /// </item>
        /// <item>
        /// A PnP device instance ID. When 
        /// specifying a PnP device instance ID, DIGCF_DEVICEINTERFACE must be 
        /// set in the Flags parameter.
        /// </item>
        /// </list></param>
        /// <param name="hwndParent">A handle of the top-level window to be used
        /// for a user interface that is associated with installing a device 
        /// instance in the device information set. This handle is optional 
        /// and can be NULL.</param>
        /// <param name="Flags">A variable of type DWORD that specifies control 
        /// options that filter the device information elements that are added to the device information set.</param>
        /// <returns>If the operation succeeds, SetupDiGetClassDevs returns a 
        /// handle to a device information set that contains all installed 
        /// devices that matched the supplied parameters. If the operation 
        /// fails, the function returns INVALID_HANDLE_VALUE. To get extended 
        /// error information, call GetLastError.</returns>
        [DllImport("setupapi.dll", 
            SetLastError = true, 
            CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,
            string Enumerator,
            IntPtr hwndParent,
            SetupDiGetClassDevsFlags Flags);

        /// <summary>
        /// Retrieves the device instance ID that is associated with a device 
        /// information element.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to the device information set 
        /// that contains the device information element that represents the 
        /// device for which to retrieve a device instance ID.</param>
        /// <param name="DeviceInfoData">A pointer to an SP_DEVINFO_DATA 
        /// structure that specifies the device information element in 
        /// DeviceInfoSet.</param>
        /// <param name="DeviceInstanceId">A pointer to the character buffer 
        /// that will receive the NULL-terminated device instance ID for the 
        /// specified device information element. For information about device 
        /// instance IDs, see Device Identification Strings.</param>
        /// <param name="DeviceInstanceIdSize">The size, in characters, of the
        /// DeviceInstanceId buffer.</param>
        /// <param name="RequiredSize">A pointer to the variable that receives
        /// the number of characters required to store the device instance ID.</param>
        /// <returns>The function returns TRUE if it is successful. Otherwise, 
        /// it returns FALSE and the logged error can be retrieved with a call 
        /// to GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceInstanceId(
           IntPtr DeviceInfoSet,
           ref SP_DEVINFO_DATA DeviceInfoData,
           IntPtr DeviceInstanceId,
           int DeviceInstanceIdSize,
           out int RequiredSize);

        /// <summary>
        /// Returns a SP_DEVINFO_DATA structure that specifies a device 
        /// information element in a device information set.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to the device information set 
        /// for which to return an SP_DEVINFO_DATA structure that represents 
        /// a device information element.</param>
        /// <param name="MemberIndex">A zero-based index of the device 
        /// information element to retrieve.</param>
        /// <param name="DeviceInfoData">A pointer to an SP_DEVINFO_DATA 
        /// structure to receive information about an enumerated device 
        /// information element. The caller must set DeviceInfoData.cbSize 
        /// to sizeof(SP_DEVINFO_DATA).</param>
        /// <returns>The function returns TRUE if it is successful. Otherwise, 
        /// it returns FALSE and the logged error can be retrieved with a call 
        /// to GetLastError.</returns>
        /// <remarks>
        /// Repeated calls to this function return a device information element
        /// for a different device. This function can be called repeatedly to 
        /// get information about all devices in the device information set.
        /// To enumerate device information elements, an installer should 
        /// initially call SetupDiEnumDeviceInfo with the MemberIndex 
        /// parameter set to 0. The installer should then increment 
        /// MemberIndex and call SetupDiEnumDeviceInfo until there are no 
        /// more values (the function fails and a call to GetLastError
        /// returns ERROR_NO_MORE_ITEMS).</remarks>
        [DllImport("setupapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet, 
            uint MemberIndex, 
            ref SP_DEVINFO_DATA DeviceInfoData);

        /// <summary>
        /// Retrieves a specified Plug and Play device property.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to a device information set 
        /// that contains a device information element that represents the 
        /// device for which to retrieve a Plug and Play property.</param>
        /// <param name="DeviceInfoData">A pointer to an SP_DEVINFO_DATA 
        /// structure that specifies the device information element in 
        /// DeviceInfoSet.</param>
        /// <param name="Property">Indicates the property to be retrieved.</param>
        /// <param name="PropertyRegDataType">A pointer to a variable that 
        /// receives the data type of the property that is being retrieved. 
        /// This is one of the standard registry data types. This parameter 
        /// is optional and can be NULL.</param>
        /// <param name="PropertyBuffer">A pointer to a buffer that receives 
        /// the property that is being retrieved. If this parameter is set to 
        /// NULL, and PropertyBufferSize is also set to zero, the function 
        /// returns the required size for the buffer in RequiredSize.</param>
        /// <param name="PropertyBufferSize">The size, in bytes, of the 
        /// PropertyBuffer buffer.</param>
        /// <param name="RequiredSize">A pointer to a variable of type DWORD 
        /// that receives the required size, in bytes, of the PropertyBuffer 
        /// buffer that is required to hold the data for the requested property.
        /// This parameter is optional and can be NULL.</param>
        /// <returns>SetupDiGetDeviceRegistryProperty returns TRUE if the call 
        /// was successful. Otherwise, it returns FALSE and the logged error 
        /// can be retrieved with a call to GetLastError. 
        /// SetupDiGetDeviceRegistryProperty returns the ERROR_INVALID_DATA 
        /// error code if the requested property does not exist for a device 
        /// or if the property data is not valid.</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            SetupDiGetDeviceRegistryPropertyType Property,
            out uint PropertyRegDataType,
            IntPtr PropertyBuffer,
            uint PropertyBufferSize,
            out uint RequiredSize);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. 
        /// You would install a hook procedure to monitor the system for certain 
        /// types of events. These events are associated either with a specific 
        /// thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook">[in] Specifies the type of hook procedure to be installed.</param>
        /// <param name="lpfn">[in] Pointer to the hook procedure. If the 
        /// dwThreadId parameter is zero or specifies the identifier of a thread 
        /// created by a different process, the lpfn parameter must point to a 
        /// hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure 
        /// in the code associated with the current process.</param>
        /// <param name="hMod">[in] Handle to the DLL containing the hook
        /// procedure pointed to by the lpfn parameter. The hMod parameter 
        /// must be set to NULL if the dwThreadId parameter specifies a thread 
        /// created by the current process and if the hook procedure is within 
        /// the code associated with the current process.</param>
        /// <param name="dwThreadId">[in] Specifies the identifier of the 
        /// thread with which the hook procedure is to be associated. If 
        /// this parameter is zero, the hook procedure is associated with 
        /// all existing threads running in the same desktop as the
        /// calling thread.</param>
        /// <returns>If the function succeeds, the return value is the handle to the hook procedure.
        /// If the function fails, the return value is NULL. To get extended
        /// error information, call GetLastError.</returns>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811",
            Justification = "We might find use for this again someday.")]
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(
            SetWindowsHookExHookType idHook,
            LowLevelKeyboardProc lpfn, 
            IntPtr hMod, 
            uint dwThreadId);

        /// <summary>
        /// Translates the specified virtual-key code and keyboard state to the
        /// corresponding Unicode character or characters.
        /// </summary>
        /// <param name="wVirtKey">[in] Specifies the virtual-key 
        /// code to be translated.</param>
        /// <param name="wScanCode">[in] Specifies the hardware scan code of the 
        /// key to be translated. The high-order bit of this value is set 
        /// if the key is up.</param>
        /// <param name="lpKeyState">[in] Pointer to a 256-byte array that 
        /// contains the current keyboard state. Each element (byte) in the 
        /// array contains the state of one key. If the high-order bit of a
        /// byte is set, the key is down.</param>
        /// <param name="pwszBuff">[out] Pointer to the buffer that receives 
        /// the translated Unicode character or characters. However, this 
        /// buffer may be returned without being null-terminated even though 
        /// the variable name suggests that it is null-terminated.</param>
        /// <param name="cchBuff">[in] Specifies the size, in wide characters, 
        /// of the buffer pointed to by the pwszBuff parameter.</param>
        /// <param name="wFlags">[in] Specifies the behavior of the function. 
        /// If bit 0 is set, a menu is active. Bits 1 through 31 are reserved.</param>
        /// <returns>-1 if the key is a dead-key character; 0 if the virtual key
        /// has no translation for the current state; 1 if a character was written
        /// to the buffer specified by pwszBuff, 2 or more if two or more
        /// characters were written to the buffer specified by pwszBuff</returns>
        [DllImport("user32.dll")]
        internal static extern int ToUnicode(      
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the 
        /// SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">[in] Handle to the hook to be removed. This 
        /// parameter is a hook handle obtained by a previous 
        /// call to SetWindowsHookEx.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended 
        /// error information, call GetLastError.</returns>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811",
            Justification = "We might find use for this again someday.")]
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Contains information about a RAWMOUSE structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTONSSTR
        {
            /// <summary>
            /// Transition state of the mouse buttons.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonFlags;

            /// <summary>
            /// If usButtonFlags is RI_MOUSE_WHEEL, this member is a signed 
            /// value that specifies the wheel delta.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonData;
        }

        /// <summary>
        /// A native windows message.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            /// <summary>
            /// Handle to the window whose window procedure receives the message. 
            /// hwnd is NULL when the message is a thread message.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// Specifies the message identifier. Applications can only use
            /// the low word; the high word is reserved by the system.
            /// </summary>
            public uint message;

            /// <summary>
            /// Specifies additional information about the message. 
            /// The exact meaning depends on the value of the message member.
            /// </summary>
            public IntPtr wParam;

            /// <summary>
            /// Specifies additional information about the message. 
            /// The exact meaning depends on the value of the message member.
            /// </summary>
            public IntPtr lParam;

            /// <summary>
            /// Specifies the time at which the message was posted.
            /// </summary>
            public uint time;

            /// <summary>
            /// Specifies the cursor position, in screen coordinates, when the
            /// message was posted.
            /// </summary>
            public POINT pt;
        }

        /// <summary>
        /// Defines the X and y coordinates of a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            /// <summary>
            /// Specifies the x-coordinate of the point.
            /// </summary>
            public long x;

            /// <summary>
            /// Specifies the y-coordinate of the point.
            /// </summary>
            public long y;
        }

        /// <summary>
        /// Contains the header information that is part of the raw input data.
        /// To get more information on the device, use hDevice in a call to 
        /// GetRawInputDeviceInfo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        { 
            /// <summary>
            /// Type of raw input.
            /// </summary>
            public RawInputType dwType;

            /// <summary>
            /// Size, in bytes, of the entire input packet of data. This 
            /// includes RAWINPUT plus possible extra input reports in the 
            /// RAWHID variable length array.
            /// </summary>
            public uint dwSize;
            
            /// <summary>
            /// Handle to the device generating the raw input data.
            /// </summary>
            public IntPtr hDevice;

            /// <summary>
            /// Value passed in the wParam parameter of the WM_INPUT message.
            /// </summary>
            public IntPtr wParam;
        }

        /// <summary>
        /// Contains the raw input from a device.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            /// <summary>
            /// A RAWINPUTHEADER structure for the raw input data.
            /// </summary>
            [FieldOffset(0)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public RAWINPUTHEADER header;
            
            /// <summary>
            /// If the data comes from a mouse, this is the RAWMOUSE structure
            /// for the raw input data.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            [FieldOffset(16)]
            public RAWMOUSE mouse;
            
            /// <summary>
            /// If the data comes from a keyboard, this is the RAWKEYBOARD
            /// structure for the raw input data.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            [FieldOffset(16)]
            public RAWKEYBOARD keyboard;
            
            /// <summary>
            /// If the data comes from a Human Interface Device (HID), this is the
            /// RAWHID structure for the raw input data.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            [FieldOffset(16)]
            public RAWHID hid;
        }

        /// <summary>
        /// Contains information about the state of the keyboard.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWKEYBOARD
        {
            /// <summary>
            /// Scan code from the key depression. The scan code for keyboard 
            /// overrun is KEYBOARD_OVERRUN_MAKE_CODE.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort MakeCode;

            /// <summary>
            /// Flags for scan code information.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort Flags;

            /// <summary>
            /// Reserved; must be zero.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort Reserved;

            /// <summary>
            /// Microsoft Windows message compatible virtual-key code.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort VKey;

            /// <summary>
            /// Corresponding window message, for example WM_KEYDOWN, 
            /// WM_SYSKEYDOWN, and so forth.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public uint Message;

            /// <summary>
            /// Device-specific additional information for the event.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public uint ExtraInformation;
        }

        /// <summary>
        /// Describes the format of the raw input from a Human Interface
        /// Device (HID). Each WM_INPUT can indicate several inputs, but all of
        /// the inputs come from the same HID. The size of the bRawData array is
        /// dwSizeHid * dwCount.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWHID
        {
            /// <summary>
            /// Size, in bytes, of each HID input in bRawData.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int dwSizeHid;

            /// <summary>
            /// Number of HID inputs in bRawData.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public int dwCount;

            /// <summary>
            /// Raw input data as an array of bytes.
            /// </summary>
            public IntPtr bRawData;
        }
        
        /// <summary>
        /// Contains information about the state of the mouse.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWMOUSE
        {
            /// <summary>
            /// Mouse state.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public ushort usFlags;

            /// <summary>
            /// Reserved; this field must be zero.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public uint ulButtons;

            /// <summary>
            /// Transition state of the mouse buttons.
            /// </summary>
            [FieldOffset(4)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public BUTTONSSTR buttonsStr;

            /// <summary>
            /// Raw state of the mouse buttons.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(8)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public uint ulRawButtons;

            /// <summary>
            /// Motion in the X direction. This is signed relative motion or 
            /// absolute motion, depending on the value of usFlags.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            [FieldOffset(12)]
            public int lLastX;

            /// <summary>
            /// Motion in the Y direction. This is signed relative motion or 
            /// absolute motion, depending on the value of usFlags.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            [FieldOffset(16)]
            public int lLastY;

            /// <summary>
            /// Device-specific additional information for the event.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(20)]
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1823",
                Justification = "It's a struct.")]
            public uint ulExtraInformation;
        }

        /// <summary>
        /// Defines information for the raw input devices.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICE
        {
            /// <summary>
            /// Top level collection Usage page for the raw input device.
            /// For usage pages, see the USB Serial Bus HID Usage Tables document
            /// at http://www.usb.org/developers/devclass_docs/Hut1_12.pdf.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsagePage;

            /// <summary>
            /// Top level collection Usage for the raw input device.
            /// For usage types, see the USB Serial Bus HID Usage Tables document
            /// at http://www.usb.org/developers/devclass_docs/Hut1_12.pdf.
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsage;

            /// <summary>
            /// Mode flag that specifies how to interpret the information provided
            /// by the usUsagePage and usUsage. It can be zero (the default) or
            /// one of the following values. By default, the operating system
            /// sends raw input from devices with the specified top level
            /// collection (TLC) to the registered application as long as it has
            /// the window focus.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public RawInputDeviceFlags dwFlags;

            /// <summary>
            /// Handle to the target window. If NULL, it follows the keyboard
            /// focus.
            /// </summary>
            public IntPtr hwndTarget;
        }

        /// <summary>
        /// Contains information about a raw input device.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICELIST
        {
            /// <summary>
            /// Handle to the raw input device.
            /// </summary>
            public IntPtr hDevice;

            /// <summary>
            /// Type of device.
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public RawInputType dwType;
        }

        /// <summary>
        /// The SP_DEVINFO_DATA structure defines a device instance that 
        /// is a member of a device information set.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVINFO_DATA
        {
            /// <summary>
            /// The size, in bytes, of the SP_DEVINFO_DATA structure.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// The GUID of the device's setup class.
            /// </summary>
            public Guid ClassGuid;

            /// <summary>
            /// An opaque handle to the device instance (also known as a handle 
            /// to the devnode). Some functions, such as SetupDiXxx functions, 
            /// take the whole SP_DEVINFO_DATA structure as input to identify a 
            /// device in a device information set. Other functions, such as 
            /// CM_Xxx functions like CM_Get_DevNode_Status, take this DevInst 
            /// handle as input.
            /// </summary>
            public uint DevInst;

            /// <summary>
            /// Reserved. For internal use only.
            /// </summary>
            public uint Reserved;
        }
    }
}
