using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeChanger
{
    internal abstract class AudioMixer
    {
        public const long VOLUME_MIN = 0;
        public const long VOLUME_MAX = 65535;

        public abstract int GetVolume();
        public abstract void SetVolume(int volume);

        public abstract bool GetMute();

        private int _attachedHandlers = 0;
        public delegate void VolumeChangedHandler();
        private VolumeChangedHandler _volumeChangedDelegate;
        public virtual event VolumeChangedHandler VolumeChanged
        {
            add
            {
                _volumeChangedDelegate += value;

                if (_attachedHandlers == 0)
                    AttachVolumeHandler();
                _attachedHandlers++;
            }
            remove
            {
                _volumeChangedDelegate -= value;

                if (_attachedHandlers == 1)
                    DetachVolumeHandler();
                _attachedHandlers--;
            }
        }

        protected void RaiseVolumeChanged()
        {
            if (_volumeChangedDelegate != null)
                _volumeChangedDelegate();
        }

        protected abstract void AttachVolumeHandler();
        protected abstract void DetachVolumeHandler();
    }

    internal class AudioMixerXP : AudioMixer
    {
        private int _windowHandle;

        public AudioMixerXP(int windowHandle)
        {
            _windowHandle = windowHandle;
        }

        public override int GetVolume()
        {
            return AudioMixerHelper.GetVolume();
        }
        public override void SetVolume(int volume)
        {
            AudioMixerHelper.SetVolume(volume);
        }
        public override bool GetMute()
        {
            return AudioMixerHelper.GetMute();
        }
        protected override void AttachVolumeHandler()
        {
            StartMixerMonitor();
        }
        protected override void DetachVolumeHandler()
        {
            StopMixerMonitor();
        }

        private AudioMixerHelper.MixerMonitor _mixerMonitor;

        private void StopMixerMonitor()
        {
            if (_mixerMonitor != null)
            {
                _mixerMonitor.Dispose();
                _mixerMonitor = null;
            }
        }
        private void StartMixerMonitor()
        {
            StopMixerMonitor();

            _mixerMonitor = AudioMixerHelper.MonitorControl(_windowHandle);
        }

        public void WndProc(System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x3D1)
            {
                RaiseVolumeChanged();
            }
        }

    }

    internal class AudioMixerVista : AudioMixer
    {
        private CoreAudioApi.MMDevice _device;

        public AudioMixerVista()
        {
            CoreAudioApi.MMDeviceEnumerator devEnum = new CoreAudioApi.MMDeviceEnumerator();
            _device = devEnum.GetDefaultAudioEndpoint(CoreAudioApi.EDataFlow.eRender, CoreAudioApi.ERole.eMultimedia);
        }

        public override int GetVolume()
        {
            return (int)(_device.AudioEndpointVolume.MasterVolumeLevelScalar * VOLUME_MAX);
        }
        public override void SetVolume(int volume)
        {
            _device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)(volume / (float)VOLUME_MAX);
        }
        public override bool GetMute()
        {
            return _device.AudioEndpointVolume.Mute;
        }
        protected override void AttachVolumeHandler()
        {
            _device.AudioEndpointVolume.OnVolumeNotification += new CoreAudioApi.AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
        }
        protected override void DetachVolumeHandler()
        {
            _device.AudioEndpointVolume.OnVolumeNotification -= new CoreAudioApi.AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
        }

        void AudioEndpointVolume_OnVolumeNotification(CoreAudioApi.AudioVolumeNotificationData data)
        {
            RaiseVolumeChanged();
        }
    }


}
