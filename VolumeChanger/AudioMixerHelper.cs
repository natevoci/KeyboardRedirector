using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Runtime.InteropServices;

namespace VolumeChanger
{
    // translated to c# by arkam@caramail.com
    // first published by Arsenio Locsin, Microsoft Corporation in VB
    // PSS ID Number: 178456

    public class AudioMixerHelper
    {

        #region "Interop"
        public const int MMSYSERR_NOERROR = 0;
        public const int MAXPNAMELEN = 32;
        public const int MIXER_LONG_NAME_CHARS = 64;
        public const int MIXER_SHORT_NAME_CHARS = 16;
        public const int MIXER_GETLINEINFOF_COMPONENTTYPE = 0x3;
        public const int MIXER_GETCONTROLDETAILSF_VALUE = 0x0;
        public const int MIXER_GETLINECONTROLSF_ONEBYTYPE = 0x2;
        public const int MIXER_SETCONTROLDETAILSF_VALUE = 0x0;
        public const int MIXERLINE_COMPONENTTYPE_DST_FIRST = 0x0;
        public const int MIXERLINE_COMPONENTTYPE_SRC_FIRST = 0x1000;
        public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LINE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 2);
        public const int MIXERCONTROL_CT_CLASS_SWITCH = 0x20000000;
        public const int MIXERCONTROL_CT_CLASS_FADER = 0x50000000;
        public const int MIXERCONTROL_CT_UNITS_UNSIGNED = 0x30000;
        public const int MIXERCONTROL_CT_SC_SWITCH_BOOLEAN = 0;
        public const int MIXERCONTROL_CT_UNITS_BOOLEAN = 0x10000;
        public const int MIXERCONTROL_CONTROLTYPE_BOOLEAN = (MIXERCONTROL_CT_CLASS_SWITCH | MIXERCONTROL_CT_SC_SWITCH_BOOLEAN | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_FADER = (MIXERCONTROL_CT_CLASS_FADER | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_VOLUME = (MIXERCONTROL_CONTROLTYPE_FADER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_MUTE = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 2);


        public const int CALLBACK_WINDOW = 0x10000;

        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerClose(int hmx);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetControlDetailsA(int hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetDevCapsA(int uMxId, MIXERCAPS pmxcaps, int cbmxcaps);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetID(int hmxobj, int pumxID, int fdwId);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetLineControlsA(int hmxobj, ref MIXERLINECONTROLS pmxlc, int fdwControls);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetLineInfoA(int hmxobj, ref MIXERLINE pmxl, int fdwInfo);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerGetNumDevs();
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerMessage(int hmx, int uMsg, int dwParam1, int dwParam2);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerOpen(out int phmx, int uMxId, int dwCallback, int dwInstance, int fdwOpen);
        [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
        protected static extern int mixerSetControlDetails(int hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);

        public struct MIXERCAPS
        {
            public int wMid;
            public int wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAXPNAMELEN)]
            public string szPname;
            public int fdwSupport;
            public int cDestinations;
        }

        public struct MIXERCONTROL
        {
            public int cbStruct;
            public int dwControlID;
            public int dwControlType;
            public int fdwControl;
            public int cMultipleItems;
            [MarshalAs(UnmanagedType.ByValTStr,
            SizeConst = MIXER_SHORT_NAME_CHARS)]
            public string szShortName;
            [MarshalAs(UnmanagedType.ByValTStr,
            SizeConst = MIXER_LONG_NAME_CHARS)]
            public string szName;
            public int lMinimum;
            public int lMaximum;
            [MarshalAs(UnmanagedType.U4, SizeConst = 10)]
            public int reserved;
        }

        public struct MIXERCONTROLDETAILS
        {
            public int cbStruct;
            public int dwControlID;
            public int cChannels;
            public int item;
            public int cbDetails;
            public IntPtr paDetails;
        }

        public struct MIXERCONTROLDETAILS_UNSIGNED
        {
            public int dwValue;
        }
        public struct MIXERCONTROLDETAILS_BOOLEAN
        {
            public int fValue;
        }

        public struct MIXERLINE
        {
            public int cbStruct;
            public int dwDestination;
            public int dwSource;
            public int dwLineID;
            public int fdwLine;
            public int dwUser;
            public int dwComponentType;
            public int cChannels;
            public int cConnections;
            public int cControls;
            [MarshalAs(UnmanagedType.ByValTStr,
            SizeConst = MIXER_SHORT_NAME_CHARS)]
            public string szShortName;
            [MarshalAs(UnmanagedType.ByValTStr,
            SizeConst = MIXER_LONG_NAME_CHARS)]
            public string szName;
            public int dwType;
            public int dwDeviceID;
            public int wMid;
            public int wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAXPNAMELEN)]
            public string szPname;
        }

        public struct MIXERLINECONTROLS
        {
            public int cbStruct;
            public int dwLineID;

            public int dwControl;
            public int cControls;
            public int cbmxctrl;
            public IntPtr pamxctrl;
        }
        #endregion

        protected static bool GetVolumeControl(int hmixer, int componentType, int ctrlType, out MIXERCONTROL mxc, out int vCurrentVol)
        {
            // This function attempts to obtain a mixer control.
            // Returns True if successful.
            MIXERLINECONTROLS mxlc = new MIXERLINECONTROLS();
            MIXERLINE mxl = new MIXERLINE();
            MIXERCONTROLDETAILS pmxcd = new MIXERCONTROLDETAILS();
            MIXERCONTROLDETAILS_UNSIGNED du = new MIXERCONTROLDETAILS_UNSIGNED();
            mxc = new MIXERCONTROL();
            int rc;
            bool retValue;
            vCurrentVol = -1;

            mxl.cbStruct = Marshal.SizeOf(mxl);
            mxl.dwComponentType = componentType;

            rc = mixerGetLineInfoA(hmixer, ref mxl, MIXER_GETLINEINFOF_COMPONENTTYPE);

            if (MMSYSERR_NOERROR == rc)
            {
                int sizeofMIXERCONTROL = 152;
                int ctrl = Marshal.SizeOf(typeof(MIXERCONTROL));
                mxlc.pamxctrl = Marshal.AllocCoTaskMem(sizeofMIXERCONTROL);
                mxlc.cbStruct = Marshal.SizeOf(mxlc);
                mxlc.dwLineID = mxl.dwLineID;
                mxlc.dwControl = ctrlType;
                mxlc.cControls = 1;
                mxlc.cbmxctrl = sizeofMIXERCONTROL;

                // Allocate a buffer for the control
                mxc.cbStruct = sizeofMIXERCONTROL;

                // Get the control
                rc = mixerGetLineControlsA(hmixer, ref mxlc, MIXER_GETLINECONTROLSF_ONEBYTYPE);
                if (MMSYSERR_NOERROR == rc)
                {
                    retValue = true;

                    // Copy the control into the destination structure
                    mxc = (MIXERCONTROL)Marshal.PtrToStructure(
                    mxlc.pamxctrl, typeof(MIXERCONTROL));
                }
                else
                {
                    retValue = false;
                }

                int sizeofMIXERCONTROLDETAILS = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS));
                int sizeofMIXERCONTROLDETAILS_UNSIGNED = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS_UNSIGNED));
                pmxcd.cbStruct = sizeofMIXERCONTROLDETAILS;
                pmxcd.dwControlID = mxc.dwControlID;
                pmxcd.paDetails = Marshal.AllocCoTaskMem(sizeofMIXERCONTROLDETAILS_UNSIGNED);
                pmxcd.cChannels = 1;
                pmxcd.item = 0;
                pmxcd.cbDetails = sizeofMIXERCONTROLDETAILS_UNSIGNED;

                rc = mixerGetControlDetailsA(hmixer, ref pmxcd, MIXER_GETCONTROLDETAILSF_VALUE);

                du = (MIXERCONTROLDETAILS_UNSIGNED)Marshal.PtrToStructure(pmxcd.paDetails, typeof(MIXERCONTROLDETAILS_UNSIGNED));

                vCurrentVol = du.dwValue;

                return retValue;
            }

            retValue = false;
            return retValue;
        }

        protected static bool GetMuteControl(int hmixer, int componentType, int ctrlType, out MIXERCONTROL mxc, out bool vCurrentMute)
        {
            // This function attempts to obtain a mixer control.
            // Returns True if successful.
            MIXERLINECONTROLS mxlc = new MIXERLINECONTROLS();
            MIXERLINE mxl = new MIXERLINE();
            MIXERCONTROLDETAILS pmxcd = new MIXERCONTROLDETAILS();
            MIXERCONTROLDETAILS_BOOLEAN du = new MIXERCONTROLDETAILS_BOOLEAN();
            mxc = new MIXERCONTROL();
            int rc;
            bool retValue;
            vCurrentMute = false;

            mxl.cbStruct = Marshal.SizeOf(mxl);
            mxl.dwComponentType = componentType;

            rc = mixerGetLineInfoA(hmixer, ref mxl, MIXER_GETLINEINFOF_COMPONENTTYPE);

            if (MMSYSERR_NOERROR == rc)
            {
                int sizeofMIXERCONTROL = 152;
                int ctrl = Marshal.SizeOf(typeof(MIXERCONTROL));
                mxlc.pamxctrl = Marshal.AllocCoTaskMem(sizeofMIXERCONTROL);
                mxlc.cbStruct = Marshal.SizeOf(mxlc);
                mxlc.dwLineID = mxl.dwLineID;
                mxlc.dwControl = ctrlType;
                mxlc.cControls = 1;
                mxlc.cbmxctrl = sizeofMIXERCONTROL;

                // Allocate a buffer for the control
                mxc.cbStruct = sizeofMIXERCONTROL;

                // Get the control
                rc = mixerGetLineControlsA(hmixer, ref mxlc, MIXER_GETLINECONTROLSF_ONEBYTYPE);
                if (MMSYSERR_NOERROR == rc)
                {
                    retValue = true;

                    // Copy the control into the destination structure
                    mxc = (MIXERCONTROL)Marshal.PtrToStructure(
                    mxlc.pamxctrl, typeof(MIXERCONTROL));
                }
                else
                {
                    retValue = false;
                }

                int sizeofMIXERCONTROLDETAILS = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS));
                int sizeofMIXERCONTROLDETAILS_BOOLEAN = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS_BOOLEAN));
                pmxcd.cbStruct = sizeofMIXERCONTROLDETAILS;
                pmxcd.dwControlID = mxc.dwControlID;
                pmxcd.paDetails = Marshal.AllocCoTaskMem(sizeofMIXERCONTROLDETAILS_BOOLEAN);
                pmxcd.cChannels = 1;
                pmxcd.item = 0;
                pmxcd.cbDetails = sizeofMIXERCONTROLDETAILS_BOOLEAN;

                rc = mixerGetControlDetailsA(hmixer, ref pmxcd, MIXER_GETCONTROLDETAILSF_VALUE);

                du = (MIXERCONTROLDETAILS_BOOLEAN)Marshal.PtrToStructure(pmxcd.paDetails, typeof(MIXERCONTROLDETAILS_BOOLEAN));

                vCurrentMute = (du.fValue != 0);

                return retValue;
            }

            retValue = false;
            return retValue;
        }

        protected static bool SetVolumeControl(int hmixer, MIXERCONTROL mxc, int volume)
        {
            // This function sets the value for a volume control.
            // Returns True if successful

            bool retValue;
            int rc;
            MIXERCONTROLDETAILS mxcd = new MIXERCONTROLDETAILS();
            MIXERCONTROLDETAILS_UNSIGNED vol = new
            MIXERCONTROLDETAILS_UNSIGNED();

            mxcd.item = 0;
            mxcd.dwControlID = mxc.dwControlID;
            mxcd.cbStruct = Marshal.SizeOf(mxcd);
            mxcd.cbDetails = Marshal.SizeOf(vol);

            // Allocate a buffer for the control value buffer
            mxcd.cChannels = 1;
            vol.dwValue = volume;

            // Copy the data into the control value buffer
            mxcd.paDetails = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(MIXERCONTROLDETAILS_UNSIGNED)));
            Marshal.StructureToPtr(vol, mxcd.paDetails, false);

            // Set the control value
            rc = mixerSetControlDetails(hmixer, ref mxcd, MIXER_SETCONTROLDETAILSF_VALUE);

            if (MMSYSERR_NOERROR == rc)
            {
                retValue = true;
            }
            else
            {
                retValue = false;
            }

            return retValue;
        }

        public static int GetVolume()
        {
            int mixer;
            MIXERCONTROL volCtrl = new MIXERCONTROL();
            int currentVol;
            mixerOpen(out mixer, 0, 0, 0, 0);
            int type = MIXERCONTROL_CONTROLTYPE_VOLUME;
            GetVolumeControl(mixer, MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);
            mixerClose(mixer);

            return currentVol;
        }

        public static void SetVolume(int vVolume)
        {
            int mixer;
            MIXERCONTROL volCtrl = new MIXERCONTROL();
            int currentVol;
            mixerOpen(out mixer, 0, 0, 0, 0);
            int type = MIXERCONTROL_CONTROLTYPE_VOLUME;
            GetVolumeControl(mixer,
            MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);
            if (vVolume > volCtrl.lMaximum) vVolume = volCtrl.lMaximum;
            if (vVolume < volCtrl.lMinimum) vVolume = volCtrl.lMinimum;
            SetVolumeControl(mixer, volCtrl, vVolume);
            GetVolumeControl(mixer,
            MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);
            if (vVolume != currentVol)
            {
                throw new Exception("Cannot Set Volume");
            }
            mixerClose(mixer);
        }

        public static bool GetMute()
        {
            int mixer;
            MIXERCONTROL volCtrl = new MIXERCONTROL();
            bool currentMute;
            mixerOpen(out mixer, 0, 0, 0, 0);
            int type = MIXERCONTROL_CONTROLTYPE_MUTE;
            GetMuteControl(mixer, MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentMute);
            mixerClose(mixer);

            return currentMute;
        }

        public static MixerMonitor MonitorControl(int windowHandle)     // iw is the window handle                          // MonitorControl
        {
            int rc = -1;

            int hmixer;
            rc = mixerOpen(out hmixer, 0, windowHandle, 0, CALLBACK_WINDOW);

            if (0 == rc)
            {
                return new MixerMonitor(hmixer);
            }
            else
            {
                return new MixerMonitor(-1);
            }
        }

        public class MixerMonitor : AudioMixerHelper, IDisposable
        {
            private int _mixer = -1;

            public MixerMonitor(int mixer)
            {
                _mixer = mixer;
            }

            public void Dispose()
            {
                if (_mixer != -1)
                {
                    mixerClose(_mixer);
                    _mixer = -1;
                }
            }
        }

    }
}
