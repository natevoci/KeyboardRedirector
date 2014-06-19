#region Copyright (C) 2009,2010 Nate

/* 
 *	Copyright (C) 2009,2010 Nate
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
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using MS;
using System.Collections.Generic;

namespace KeyboardRedirector
{
    /// <summary>
    /// Handles raw input from keyboard devices.
    /// </summary>
    public sealed class InputDevice
    {
        #region const definitions

        // The following constants are defined in Windows.h

        private const int RIDEV_APPKEYS     = 0x00000400;
        private const int RIDEV_INPUTSINK   = 0x00000100;
        private const int RIDEV_NOLEGACY    = 0x00000030;
        private const int RIDEV_PAGEONLY    = 0x00000020;
        private const int RID_INPUT         = 0x10000003;

        private const int FAPPCOMMAND_MASK  = 0xF000;
        private const int FAPPCOMMAND_MOUSE = 0x8000;
        private const int FAPPCOMMAND_OEM   = 0x1000;

        private const int RIDI_DEVICENAME   = 0x20000007;
        private const int RIDI_DEVICEINFO   = 0x2000000B;
        
        private const int VK_OEM_CLEAR      = 0xFE;
        private const int VK_LAST_KEY       = VK_OEM_CLEAR; // this is a made up value used as a sentinel
       
        #endregion const definitions

        #region structs & enums

        /// <summary>
        /// An enum representing the different types of input devices.
        /// </summary>
        public enum DeviceType
        {
            Mouse = 0,
            Keyboard = 1,
            HID = 2,
            Unknown = 3
        }

        public enum RawKeyboardFlags : ushort
        {
            MAKE = 0x0,
            BREAK = 0x1,
            E0 = 0x2,
            E1 = 0x4,
            TERMSRV_SET_LED = 0x8,
            TERMSRV_SHADOW = 0x10
        }

        /// <summary>
        /// Class encapsulating the information about a
        /// keyboard event, including the device it
        /// originated with and what key was pressed
        /// </summary>
        public class DeviceInfo
        {
            public IntPtr DeviceHandle;
            public DeviceType DeviceType;
            public string DeviceName;
            public RID_DEVICE_INFO RawInputDeviceInfo;
            public string DeviceDesc;
            public string Name;

            public string source;
            public Keys keys;
            public int hidKey;
        }

        #region Windows.h structure declarations
        
        // The following structures are defined in Windows.h

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICELIST
        {
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsagePage;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsage;
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
            public IntPtr hwndTarget;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUT
        {
            public RAWINPUTHEADER header;
            public RAWINPUTDATA data;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct RAWINPUTDATA
        {
            [FieldOffset(0)]
            public RAWMOUSE mouse;
            [FieldOffset(0)]
            public RAWKEYBOARD keyboard;
            [FieldOffset(0)]
            public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public DeviceType dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWHID
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwSizHid;
            [MarshalAs(UnmanagedType.U4)]
            public int dwCount;
            public IntPtr pData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BUTTONSSTR
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonFlags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonData;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RAWMOUSE
        {
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)] 
            public ushort usFlags;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)] 
            public uint ulButtons; 
            [FieldOffset(4)] 
            public BUTTONSSTR buttonsStr;
            [MarshalAs(UnmanagedType.U4)][FieldOffset(8)] 
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [MarshalAs(UnmanagedType.U4)][FieldOffset(20)]
            public uint ulExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWKEYBOARD
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort MakeCode;
            [MarshalAs(UnmanagedType.U2)]
            public RawKeyboardFlags Flags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Reserved;
            [MarshalAs(UnmanagedType.U2)]
            public ushort VKey;
            [MarshalAs(UnmanagedType.U4)]
            public Win32.WM Message;
            [MarshalAs(UnmanagedType.U4)]
            public uint ExtraInformation;
        }


        [StructLayout(LayoutKind.Explicit)]
        public struct RID_DEVICE_INFO
        {
            [FieldOffset(0), MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [FieldOffset(4), MarshalAs(UnmanagedType.U4)]
            public DeviceType dwType;

            [FieldOffset(8)]
            public RID_DEVICE_INFO_MOUSE mouse;
            [FieldOffset(8)]
            public RID_DEVICE_INFO_KEYBOARD keyboard;
            [FieldOffset(8)]
            public RID_DEVICE_INFO_HID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RID_DEVICE_INFO_MOUSE
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint dwId;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwNumberOfButtons;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwSampleRate;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasHorizontalWheel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RID_DEVICE_INFO_KEYBOARD
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint dwType;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwSubType;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwKeyboardMode;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwNumberOfFunctionKeys;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwNumberOfIndicators;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwNumberOfKeysTotal;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RID_DEVICE_INFO_HID
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint dwVendorId;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwProductId;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwVersionNumber;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsagePage;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsage;
        }


        #endregion Windows.h structure declarations


        #endregion structs & enums
        
        #region DllImports
        
        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);
        
        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);
        
        [DllImport("User32.dll")]
        extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll")]
        extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("User32.dll")]
        extern static uint GetRawInputBuffer(IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        #endregion DllImports

        #region Variables and event handling
        
        /// <summary>
        /// List of keyboard devices. Key: the device handle
        /// Value: the device info class
        /// </summary>
        private Dictionary<IntPtr, DeviceInfo> _deviceList = new Dictionary<IntPtr, DeviceInfo>();

        public Dictionary<IntPtr, DeviceInfo> DeviceList
        {
            get
            {
                if (_deviceList.Count == 0)
                    EnumerateDevices();
                return _deviceList;
            }
        }

        /// <summary>
        /// The delegate to handle KeyPressed events.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">A set of KeyControlEventArgs information about the key that was pressed and the device it was on.</param>
        public delegate void KeyPressedEventHandler(object sender, KeyControlEventArgs e);

        /// <summary>
        /// The event raised when InputDevice detects that a key was pressed.
        /// </summary>
        public event KeyPressedEventHandler KeyPressed;


        public delegate void DeviceEventHandler(object sender, DeviceInfo dInfo, RAWINPUT rawInput);
        public event DeviceEventHandler DeviceEvent;


        /// <summary>
        /// Arguments provided by the handler for the KeyPressed
        /// event.
        /// </summary>
        public class KeyControlEventArgs : EventArgs
        {
            private DeviceInfo  m_deviceInfo;
            private DeviceType  m_device;
           
            public KeyControlEventArgs( DeviceInfo dInfo, DeviceType device )
            {
                m_deviceInfo = dInfo;
                m_device = device;
            }
            
            public KeyControlEventArgs()
            {
            }

            public DeviceInfo Keyboard
            {
                get { return m_deviceInfo; }
                set { m_deviceInfo = value; }
            }

            public DeviceType Device
            {
                get { return m_device; }
                set { m_device = value; }
            }
        }

        #endregion Variables and event handling

        #region InputDevice( IntPtr hwnd )

        /// <summary>
        /// InputDevice constructor; registers the raw input devices
        /// for the calling window.
        /// </summary>
        /// <param name="hwnd">Handle of the window listening for key presses</param>
        public InputDevice( IntPtr hwnd )
        {
            //Create an array of all the raw input devices we want to 
            //listen to. In this case, only keyboard devices.
            //RIDEV_INPUTSINK determines that the window will continue
            //to receive messages even when it doesn't have the focus.
            var rids = new List<RAWINPUTDEVICE>();

            var usagePairList = new List<int>();

            foreach (var device in DeviceList.Values)
            {
                if (device.RawInputDeviceInfo.dwType == DeviceType.Keyboard)
                {
                    int usagePair = 0x00010006;
                    if (!usagePairList.Contains(usagePair))
                        usagePairList.Add(usagePair);
                }
                else if (device.RawInputDeviceInfo.dwType == DeviceType.HID)
                {
                    int usagePair = (device.RawInputDeviceInfo.hid.usUsagePage << 16) + device.RawInputDeviceInfo.hid.usUsage;
                    if (!usagePairList.Contains(usagePair))
                        usagePairList.Add(usagePair);
                }
            }

            foreach (int usagePair in usagePairList)
            {
                ushort usagePage = (ushort)(usagePair >> 16);
                ushort usage = (ushort)(usagePair & 0xFFFF);

                var rid = new RAWINPUTDEVICE();
                rid.usUsagePage = usagePage;
                rid.usUsage = usage;
                rid.dwFlags = RIDEV_INPUTSINK;
                rid.hwndTarget = hwnd;
                if (usage == 0)
                    rid.dwFlags |= RIDEV_PAGEONLY;
                rids.Add(rid);
            }

            var ridArray = rids.ToArray();
            if (!RegisterRawInputDevices(ridArray, (uint)rids.Count, (uint)Marshal.SizeOf(ridArray[0])))
            {
                string errorMessage = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;
                throw new ApplicationException( "Failed to register raw input device(s)." );
            }
        }

        #endregion InputDevice( IntPtr hwnd )

        #region ReadReg( string item, ref bool isKeyboard )
        
        /// <summary>
        /// Reads the Registry to retrieve a friendly description
        /// of the device, and determine whether it is a keyboard.
        /// </summary>
        /// <param name="item">The device name to search for, as provided by GetRawInputDeviceInfo.</param>
        /// <param name="isKeyboard">Determines whether the device's class is "Keyboard".</param>
        /// <returns>The device description stored in the Registry entry's DeviceDesc value.</returns>
        private string ReadReg( string item, ref bool isKeyboard )
        {
            RegistryKey OurKey = null;
            int retryCount = 0;
            string deviceDesc = null;
            string deviceClass = null;
            try
            {
                // Example Device Identification string
                // @"\??\ACPI#PNP0303#3&13c0b0c5&0#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}";

                // remove the \??\
                item = item.Substring(4);

                string[] split = item.Split('#');

                string id_01 = split[0];    // ACPI (Class code)
                string id_02 = split[1];    // PNP0303 (SubClass code)
                string id_03 = split[2];    // 3&13c0b0c5&0 (Protocol code)
                //The final part is the class GUID and is not needed here

                string findme = string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", id_01, id_02, id_03);

                OurKey = Registry.LocalMachine.OpenSubKey(findme, false);

                while (OurKey == null)
                {
                    if (retryCount > 20)
                    {
                        // Failed to find the information about this item in the registry
                        isKeyboard = false;
                        return item;
                    }

                    System.Threading.Thread.Sleep(100);
                    OurKey = Registry.LocalMachine.OpenSubKey(findme, false);
                    retryCount++;
                }

                //Retrieve the desired information and set isKeyboard
                deviceDesc = (string)OurKey.GetValue("DeviceDesc");
                deviceClass = (string)OurKey.GetValue("Class");

                if (deviceClass.ToUpper().Equals("KEYBOARD"))
                {
                    isKeyboard = true;
                }
                else
                {
                    isKeyboard = false;
                }

                return deviceDesc;
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("Object reference not set to an instance of an object. item=" + item + ", OurKey " + ((OurKey == null) ? "not found" : "found") + ", retryCount=" + retryCount.ToString() + ", deviceDesc=" + deviceDesc + ", deviceClass=" + deviceClass, ex);
            }
        }

        #endregion ReadReg( string item, ref bool isKeyboard )

        #region int EnumerateDevices()

        /// <summary>
        /// Iterates through the list provided by GetRawInputDeviceList,
        /// counting keyboard devices and adding them to deviceList.
        /// </summary>
        /// <returns>The number of keyboard devices found.</returns>
        public int EnumerateDevices()
        {
            
            int NumberOfDevices = 0;
            uint deviceCount = 0;
            int dwSize = ( Marshal.SizeOf( typeof( RAWINPUTDEVICELIST )));
            _deviceList.Clear();

            // Get the number of raw input devices in the list,
            // then allocate sufficient memory and get the entire list
            if( GetRawInputDeviceList( IntPtr.Zero, ref deviceCount, (uint)dwSize ) == 0 )
            {
                IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                // Iterate through the list, discarding undesired items
                // and retrieving further information on keyboard devices
                for (int i = -1; i < deviceCount; i++)
                {
                    DeviceInfo dInfo;
                    string deviceName;
                    uint pcbSize = 0;

                    IntPtr hDevice = IntPtr.Zero;
                    int deviceType = 0;
                    RID_DEVICE_INFO deviceInfo = new RID_DEVICE_INFO();

                    if (i == -1)
                    {
                        dInfo = new DeviceInfo();

                        dInfo.DeviceName = "SendInput";
                        dInfo.DeviceHandle = IntPtr.Zero;
                        dInfo.DeviceType = DeviceType.Keyboard;
                        dInfo.Name = "SendInput (Keystrokes simulated by applications)";

                        NumberOfDevices++;
                        _deviceList.Add(IntPtr.Zero, dInfo);
                        continue;
                    }

                    RAWINPUTDEVICELIST rid = (RAWINPUTDEVICELIST)Marshal.PtrToStructure(
                                               new IntPtr((pRawInputDeviceList.ToInt32() + (dwSize * i))),
                                               typeof(RAWINPUTDEVICELIST));
                    hDevice = rid.hDevice;
                    deviceType = rid.dwType;

                    GetRawInputDeviceInfo(hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                    if (pcbSize > 0)
                    {
                        IntPtr pData = Marshal.AllocHGlobal((int)pcbSize);
                        GetRawInputDeviceInfo(hDevice, RIDI_DEVICENAME, pData, ref pcbSize);
                        deviceName = (string)Marshal.PtrToStringAnsi(pData);
                        Marshal.FreeHGlobal(pData);

                        // Drop the "root" keyboard and mouse devices used for Terminal 
                        // Services and the Remote Desktop
                        if (deviceName.ToUpper().StartsWith(@"\\?\ROOT"))
                            continue;
                        if (deviceName.ToUpper().StartsWith(@"\??\ROOT"))
                            continue;

                        GetRawInputDeviceInfo(hDevice, RIDI_DEVICEINFO, IntPtr.Zero, ref pcbSize);
                        
                        string bits = "";

                        if (pcbSize > 0)
                        {
                            pData = Marshal.AllocHGlobal((int)pcbSize);
                            GetRawInputDeviceInfo(hDevice, RIDI_DEVICEINFO, pData, ref pcbSize);
                            
                            var data = new byte[pcbSize];
                            Marshal.Copy(pData, data, 0, (int)pcbSize);
                            bits = BitConverter.ToString(data);

                            deviceInfo = (RID_DEVICE_INFO)Marshal.PtrToStructure(pData, typeof(RID_DEVICE_INFO));
                            Marshal.FreeHGlobal(pData);
                        }

                        // If the device is identified in the list as a keyboard or 
                        // HID device, create a DeviceInfo object to store information 
                        // about it
                        //if (rid.dwType == RIM_TYPEKEYBOARD || rid.dwType == RIM_TYPEHID)
                        {
                            dInfo = new DeviceInfo();

                            dInfo.DeviceHandle = hDevice;
                            dInfo.DeviceType = GetDeviceType(deviceType);
                            dInfo.DeviceName = deviceName;
                            dInfo.RawInputDeviceInfo = deviceInfo;

                            // Check the Registry to see whether this is actually a 
                            // keyboard, and to retrieve a more friendly description.
                            bool IsKeyboardDevice = false;
                            dInfo.DeviceDesc = ReadReg(deviceName, ref IsKeyboardDevice);
                            dInfo.Name = dInfo.DeviceDesc.Substring(dInfo.DeviceDesc.LastIndexOf(";") + 1);

                            // If it is a keyboard and it isn't already in the list,
                            // add it to the deviceList hashtable and increase the
                            // NumberOfDevices count
                            //if (!deviceList.Contains(hDevice) && IsKeyboardDevice)
                            if (!_deviceList.ContainsKey(hDevice))
                            {
                                NumberOfDevices++;
                                _deviceList.Add(hDevice, dInfo);
                            }
                        }
                    }
                }


                Marshal.FreeHGlobal(pRawInputDeviceList);

                return NumberOfDevices;

            }
            else
            {
                throw new ApplicationException( "An error occurred while retrieving the list of devices." );
            }

        }

        #endregion EnumerateDevices()

        #region ProcessInputCommand( Message message )
        
        /// <summary>
        /// Processes WM_INPUT messages to retrieve information about any
        /// keyboard events that occur.
        /// </summary>
        /// <param name="message">The WM_INPUT message to process.</param>
        public void ProcessInputCommand(IntPtr message_LParam)
        {
            uint dwSize = 0;

            //System.Threading.Thread.Sleep(1000);

            // First call to GetRawInputData sets the value of dwSize,
            // which can then be used to allocate the appropriate amount of memory,
            // storing the pointer in "buffer".
            GetRawInputData( message_LParam, 
                             RID_INPUT, IntPtr.Zero, 
                             ref dwSize, 
                             (uint)Marshal.SizeOf( typeof( RAWINPUTHEADER )));

            if (dwSize == 0)
                throw new Exception("GetRawInputData returned 0");

            IntPtr buffer = Marshal.AllocHGlobal( (int)dwSize );
            try
            {
                // Check that buffer points to something
                if (buffer == IntPtr.Zero)
                    throw new OutOfMemoryException("Could not allocate buffer.");

                // call GetRawInputData again to fill the allocated memory
                // with information about the input
                uint bytesCopied = GetRawInputData(message_LParam,
                                                   RID_INPUT,
                                                   buffer,
                                                   ref dwSize,
                                                   (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

                if (bytesCopied != dwSize)
                    throw new Exception("GetRawInputData returned a mismatching number of bytes.");

                // Store the message information in "raw", then check
                // that the input comes from a keyboard device before
                // processing it to raise an appropriate KeyPressed event.

                RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                // Retrieve information about the device and the
                // key that was pressed.
                DeviceInfo dInfo = null;

                if (_deviceList.ContainsKey(raw.header.hDevice))
                {
                    dInfo = (DeviceInfo)_deviceList[raw.header.hDevice];
                }
                if (dInfo == null)
                {
                    string errMessage = String.Format("Handle :{0} was not in hashtable. The device may support more than one handle or usage page, and is probably not a standard keyboard.", raw.header.hDevice);
                    throw new ApplicationException(errMessage);
                    //Debug.WriteLine(errMessage);
                    //return;
                }


                if (DeviceEvent != null)
                {
                    DeviceEvent(this, dInfo, raw);
                }

                if (raw.header.dwType == DeviceType.HID)
                {
                    int length = raw.data.hid.dwCount * raw.data.hid.dwSizHid;
                    byte[] data = new byte[length];

                    int offset = Marshal.OffsetOf(typeof(RAWHID), "pData").ToInt32();
                    offset += Marshal.OffsetOf(typeof(RAWINPUTDATA), "hid").ToInt32();
                    offset += Marshal.OffsetOf(typeof(RAWINPUT), "data").ToInt32();

                    Marshal.Copy(new IntPtr(buffer.ToInt32() + offset), data, 0, length);

                    if (raw.data.hid.dwSizHid >= 3)
                    {
                        for (offset = 0; offset < length; offset += raw.data.hid.dwSizHid)
                        {
                            dInfo.hidKey = data[1] | data[2] << 8;

                            Debug.WriteLine(string.Format("  HID:{0,8} wParam:{1} hidKey:0x{2:X} usagePage:{4} usage:{5} | {3}",
                                raw.header.hDevice,
                                raw.header.wParam,
                                dInfo.hidKey,
                                BitConverter.ToString(data),
                                dInfo.RawInputDeviceInfo.hid.usUsagePage,
                                dInfo.RawInputDeviceInfo.hid.usUsage));

                            if (KeyPressed != null)
                            {
                                KeyPressed(this, new KeyControlEventArgs(dInfo, dInfo.DeviceType));
                            }
                        }
                    }
                    else
                    {
                        var bits = BitConverter.ToString(data);

                        Debug.WriteLine("input: " + dInfo.Name);
                        Debug.WriteLine(string.Format("  HID:{0,8} wParam:{1} | {2}",
                            raw.header.hDevice,
                            raw.header.wParam,
                            bits));
                    }
                }

                if (raw.header.dwType == DeviceType.Keyboard)
                {
                    //Debug.WriteLine("input: " + dInfo.Name);
                    //Debug.WriteLine(string.Format("  {0,8} {1,8} {2,16} | {3,4} {4,4} {5,4} ({6,4}) {7} {8,8}",
                    //    raw.header.hDevice,
                    //    raw.header.dwType,
                    //    raw.header.wParam,
                    //    raw.data.keyboard.MakeCode,
                    //    raw.data.keyboard.Flags,
                    //    raw.data.keyboard.Reserved,
                    //    raw.data.keyboard.VKey,
                    //    raw.data.keyboard.Message.ToString(),
                    //    raw.data.keyboard.ExtraInformation));

                    // Filter for Key Down events and then retrieve information 
                    // about the keystroke
                    //if (raw.data.keyboard.Message == WM_KEYDOWN || raw.data.keyboard.Message == WM_SYSKEYDOWN)
                    if (KeyPressed != null)
                    {

                        ushort key = raw.data.keyboard.VKey;


                        // On most keyboards, "extended" keys such as the arrow or 
                        // page keys return two codes - the key's own code, and an
                        // "extended key" flag, which translates to 255. This flag
                        // isn't useful to us, so it can be disregarded.
                        if (key > VK_LAST_KEY)
                        {
                            return;
                        }

                        Keys myKey;
                        string name = Enum.GetName(typeof(Keys), key);
                        if (name != null)
                            myKey = (Keys)Enum.Parse(typeof(Keys), name);
                        else
                            myKey = (Keys)key;
                        //dInfo.vKey = myKey.ToString();
                        dInfo.keys = myKey;

                        //Debug.WriteLine("key: " + myKey.ToString());

                        // If the key that was pressed is valid and there
                        // was no problem retrieving information on the device,
                        // raise the KeyPressed event.
                        if (dInfo != null)
                        {
                            KeyPressed(this, new KeyControlEventArgs(dInfo, GetDevice(message_LParam.ToInt32())));
                        }
                        else
                        {
                            string errMessage = String.Format("Received Unknown Key: {0}. Possibly an unknown device", key);
                            throw new ApplicationException(errMessage);
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal( buffer );
            }
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2 + 2);
            string hexAlphabet = "0123456789ABCDEF";

            result.Append("0x");
            int i = 0;
            foreach (byte b in bytes)
            {
                if ((i == 4) || (i == 8)|| (i == 12) || (i == 16))
                    result.Append(" ");
                if ((i == 16))
                    result.Append(" ");
                if ((i == 18) || (i == 20) || (i == 22) || (i == 24) || (i == 28))
                    result.Append(" ");
                i++;

                result.Append(hexAlphabet[(int)(b >> 4)]);
                result.Append(hexAlphabet[(int)(b & 0xF)]);
            }

            return result.ToString();
        }


        #endregion ProcessInputCommand( Message message )

        #region DeviceType GetDevice( int param )

        /// <summary>
        /// Determines what type of device triggered a WM_INPUT message.
        /// (Used in the ProcessInputCommand method).
        /// </summary>
        /// <param name="param">The LParam from a WM_INPUT message.</param>
        /// <returns>A DeviceType enum value.</returns>
        private DeviceType GetDevice(int param)
        {
            DeviceType deviceType;

            switch( (int)(((ushort)(param >> 16)) & FAPPCOMMAND_MASK ))
            {
                case FAPPCOMMAND_OEM:
                    deviceType = DeviceType.HID;
                    break;
                case FAPPCOMMAND_MOUSE:
                    deviceType = DeviceType.Mouse;
                    break;
                default:
                    deviceType = DeviceType.Keyboard;
                    break;
            }

            return deviceType;
        }

        #endregion DeviceType GetDevice( int param )

        #region ProcessMessage( Message message )

        public bool IsInputMessage(Message message)
        {
            return (message.Msg == (int)Win32.WM.INPUT);
        }
        public bool IsInputMessage(Win32.MSG message)
        {
            return (message.msg == (int)Win32.WM.INPUT);
        }

        /// <summary>
        /// Filters Windows messages for WM_INPUT messages and calls
        /// ProcessInputCommand if necessary.
        /// </summary>
        /// <param name="message">The Windows message.</param>
        public void ProcessMessage( Message message )
		{
            if (IsInputMessage(message))
	        {
	            ProcessInputCommand( message.LParam );
	        }
        }
        public void ProcessMessage(Win32.MSG message)
        {
            if (IsInputMessage(message))
            {
                ProcessInputCommand(message.lParam);
            }
        }

        #endregion ProcessMessage( Message message )

        #region GetDeviceType( int device )

        /// <summary>
        /// Converts a RAWINPUTDEVICELIST dwType value to a string
        /// describing the device type.
        /// </summary>
        /// <param name="device">A dwType value (RIM_TYPEMOUSE, 
        /// RIM_TYPEKEYBOARD or RIM_TYPEHID).</param>
        /// <returns>A string representation of the input value.</returns>
        private DeviceType GetDeviceType(int device)
        {
            DeviceType deviceType;
            switch (device)
            {
                case (int)DeviceType.Mouse: deviceType = DeviceType.Mouse; break;
                case (int)DeviceType.Keyboard: deviceType = DeviceType.Keyboard; break;
                case (int)DeviceType.HID: deviceType = DeviceType.HID; break;
                default: deviceType = DeviceType.Unknown; break;
            }
            return deviceType;
        }

        #endregion GetDeviceType( int device )

    }
}
