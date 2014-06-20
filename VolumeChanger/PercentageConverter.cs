using System;
using System.Collections.Generic;
using System.Text;

namespace VolumeChanger
{
    class PercentageConverter
    {
        private long _valueMax = 65535;
        private double _expValue = -1;
        private double _powValue = -1;

        public PercentageConverter(long valueMax)
        {
            _valueMax = valueMax;
        }

        public void SetLinear()
        {
            _expValue = -1;
            _powValue = -1;
        }
        public void SetExponential(double value)
        {
            _expValue = value;
            _powValue = -1;
        }
        public void SetPower(double value)
        {
            _expValue = -1;
            _powValue = value;
        }

        public double VolumeToPercent(long vol)
        {
            if (_powValue >= 0.0)
                return VolumeToPercentPow(vol);
            if (_expValue >= 0.0)
                return VolumeToPercentExp(vol);
            return VolumeToPercentLinear(vol);
        }

        public long PercentToVolume(double percent)
        {
            if (_powValue >= 0.0)
                return PercentToVolumePow(percent);
            if (_expValue >= 0.0)
                return PercentToVolumeExp(percent);
            return PercentToVolumeLinear(percent);
        }


        // Linear
        private double VolumeToPercentLinear(long vol)
        {
            return vol / (double)_valueMax * 100.0;
        }
        private long PercentToVolumeLinear(double percent)
        {
            return (long)(percent / 100.0 * (double)_valueMax);
        }


        // Power
        private double VolumeToPercentPow(long vol)
        {
            double percent = vol / (double)_valueMax;
            percent = Math.Pow(percent, (1.0 / _powValue));
            return percent * 100.0;
        }
        private long PercentToVolumePow(double percent)
        {
            double val = Math.Pow(percent / 100.0, _powValue);
            return (long)Math.Round(val * (double)_valueMax);
        }


        // Exponential
        private double VolumeToPercentExp(long vol)
        {
            double percent = vol / (double)_valueMax;
            percent = percent * (_expValue - 1) + 1;
            percent = Math.Log(percent, _expValue);
            return percent * 100.0;
        }
        private long PercentToVolumeExp(double percent)
        {
            double val = (Math.Pow(_expValue, percent / 100.0) - 1) / (_expValue - 1);
            return (long)Math.Round(val * (double)_valueMax);
        }

    }
}
