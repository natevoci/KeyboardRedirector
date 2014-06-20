using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DW.Utils
{
    class Time
    {
        private static long ticksPerSecond;
        private static long baseTicks;

        private Time() { }

        static Time()
        {
            QueryPerformanceFrequency(ref ticksPerSecond);
            QueryPerformanceCounter(ref baseTicks);
        }

        public static double GetTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            double preciseTime = (time - baseTicks) * 1000.0 / (double)ticksPerSecond;
            return preciseTime;
        }

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("kernel32")]
        public static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("kernel32")]
        public static extern bool QueryPerformanceCounter(ref long PerformanceCount);
    }
}
