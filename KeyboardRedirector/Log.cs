#region Copyright (C) 2009,2010 Nate

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
using System.Text;
using System.Threading;
using System.IO;

namespace KeyboardRedirector
{
    public class Log
    {
        #region "exception logging"
        private static object _exceptionLock = new object();
        public static void LogException(Exception ex)
        {
            if (ex != null)
            {
                lock (_exceptionLock)
                {
                    string path = Settings.SettingsPath;
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + @"exception.log", true))
                    {
                        writer.WriteLine(DateTime.Now.ToString(@"yyyy/MM/dd HH\:mm\:ss.fff") + ": Unhandled Exception: " + ex.Message + Environment.NewLine);
                        writer.WriteLine(ex.ToString());
                        writer.WriteLine("--------" + Environment.NewLine + Environment.NewLine);
                        MainLog.WriteError(ex.Message);
                    }
                }
            }
        }
        #endregion


        #region "static members"
        private static Log _mainLog = new Log();

        internal static Log MainLog
        {
            get { return Log._mainLog; }
        }
        #endregion

        #region "Variables"
        private const int PREFIX_LENGTH = 5;
        private const int LEVEL_LENGTH = 5;
        private string _filename = "";

        private Thread _logWriterThread = null;
        private StringBuilder _logBuffer = new StringBuilder();
        private bool _keepFileOpen = false;
        private FileStream _fileStream = null;
        private object _fileStreamLock = new object();

        private double _logWriteTime = 0.0;
        private double _totalLogWriteTime = 0.0;
        private int _logWrites = 0;
        private bool _logOn = true;
        #endregion

        public bool KeepFileOpen
        {
            get { return _keepFileOpen; }
            set
            {
                _keepFileOpen = value;
                lock (_fileStreamLock)
                {
                    if ((_keepFileOpen == false) && (_fileStream != null))
                    {
                        _fileStream.Close();
                        _fileStream = null;
                    }
                }
            }
        }

        public double LastLogWriteTime
        {
            get { return _logWriteTime; }
        }
        public double AvgLogWriteTime
        {
            get
            {
                if (_logWrites < 1)
                    return 0.0;
                return _totalLogWriteTime / (double)_logWrites;
            }
        }
        public double TotalLogWriteTime
        {
            get { return _totalLogWriteTime; }
        }


        public void SetFilename(string filename)
        {
            _filename = filename;
        }

        private void Write(string level, string message)
        {
            if (!_logOn) return;
            string prefix = System.Threading.Thread.CurrentThread.Name;
            if (prefix == null)
                prefix = "";
            if (prefix.Length > PREFIX_LENGTH)
                prefix = prefix.Substring(0, PREFIX_LENGTH);

            if (level == null)
                level = "";
            if (level.Length > LEVEL_LENGTH)
                level = level.Substring(0, LEVEL_LENGTH);

            string[] eols = new string[] { "\r\n", "\r", "\n" };
            string[] lines = message.Split(eols, StringSplitOptions.None);
            StringBuilder output = new StringBuilder();

            foreach (string line in lines)
            {
                output.Append(level.PadRight(LEVEL_LENGTH) + ":");
                output.Append(DateTime.Now.ToString(@"yyyy/MM/dd HH\:mm\:ss.fff") + ":");
                output.Append(prefix.PadRight(PREFIX_LENGTH) + ":");
                //output.Append("".PadLeft(_indent, ' '));
                output.Append(line);
                output.Append(Environment.NewLine);
            }

            lock (this)
            {
                if (_logWriterThread == null || _logWriterThread.ThreadState == ThreadState.Stopped)
                {
                    _logWriterThread = new Thread(new ThreadStart(LogWriterThread));
                    _logWriterThread.IsBackground = true;
                    _logWriterThread.Name = "LogWriter";
                    _logWriterThread.Start();
                }

                _logBuffer.Append(output.ToString());
            }


        }

        public void WriteInfo(string message)
        {
            Write("info", message);
        }

        public void WriteError(string message)
        {
            Write("error", message);
        }

        public void WriteDebug(string message)
        {
            Write("debug", message);
        }

        private void LogWriterThread()
        {
            while (true)
            {
                Thread.Sleep(1000);

                StringBuilder buffer = null;

                try
                {
                    lock (this)
                    {
                        if (_logBuffer.Length > 0)
                        {
                            buffer = _logBuffer;
                            _logBuffer = new StringBuilder();
                        }

                        if (buffer != null)
                        {
                            double startTimeMS = Utils.Time.GetTime();

                            WriteToFile(buffer.ToString());

                            _logWriteTime = Utils.Time.GetTime() - startTimeMS;
                            _totalLogWriteTime += _logWriteTime;
                            _logWrites++;
                        }
                    }
                }
                catch (ThreadAbortException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    Log.LogException(e);
                }
            }
        }

        private void WriteToFile(string output)
        {
            if ((_filename != null) && (_filename.Length > 0))
            {
                lock (_fileStreamLock)
                {
                    if (_fileStream == null)
                    {
                        FileInfo fi = new FileInfo(_filename);
                        string folder = fi.DirectoryName;
                        if (Directory.Exists(folder) == false)
                            Directory.CreateDirectory(folder);

                        _fileStream = File.Open(fi.FullName, FileMode.Append, FileAccess.Write, FileShare.Read);
                    }


                    if (_fileStream.Length == 0)
                    {
                        _fileStream.WriteByte(0xFF);
                        _fileStream.WriteByte(0xFE);
                    }

                    byte[] bytes = System.Text.Encoding.Unicode.GetBytes(output);

                    _fileStream.Write(bytes, 0, bytes.Length);
                    _fileStream.Flush();


                    if (_keepFileOpen == false)
                    {
                        _fileStream.Close();
                        _fileStream = null;
                    }
                }

            }
        }

        public void Flush()
        {
            while (true)
            {
                lock (this)
                {
                    if (_logBuffer.Length == 0)
                        break;
                }
                Thread.Sleep(100);
            }
        }

        public void LogOff()
        {
            if (_logWriterThread != null || _logWriterThread.ThreadState == ThreadState.Running)
                _logWriterThread.Abort();
            _logOn = false;
        }

        public void LogOn()
        {
            _logOn = true;
        }
    }
}
