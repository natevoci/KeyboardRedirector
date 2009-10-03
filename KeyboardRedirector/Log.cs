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
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path + @"\KeyboardRedirector\exception.log", true))
                    {
                        writer.WriteLine("Unhandled Exception: " + ex.Message + Environment.NewLine);
                        writer.WriteLine(ex.ToString());
                        writer.WriteLine("--------" + Environment.NewLine + Environment.NewLine);
                    }
                }
            }
        }
        #endregion


        #region "static memebers"
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
                output.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":");
                output.Append(prefix.PadRight(PREFIX_LENGTH) + ":");
                //output.Append("".PadLeft(_indent, ' '));
                output.Append(line);
                output.Append(Environment.NewLine);
            }

            lock (this)
            {
                if (_logWriterThread == null)
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

    }
}
