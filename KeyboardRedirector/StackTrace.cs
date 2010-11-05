#region Copyright (C) 2010 Nate

/* 
 *	Copyright (C) 2010 Nate
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
using System.Reflection;

namespace KeyboardRedirector
{
    class StackTrace
    {
        public static string GetStackTrace()
        {
            return GetStackTrace(System.Threading.Thread.CurrentThread);
        }

        public static string GetStackTrace(Thread thread)
        {
            StringBuilder text = new StringBuilder();

            try
            {
                System.Diagnostics.StackTrace stackTrace;

                if (thread != System.Threading.Thread.CurrentThread)
                {
                    thread.Suspend();

                    int retryCount = 0;
                    System.Threading.ThreadState state;
                    while (true)
                    {
                        state = System.Threading.ThreadState.Suspended;
                        if ((thread.ThreadState & state) == state)
                            break;

                        state = ThreadState.WaitSleepJoin | ThreadState.SuspendRequested;
                        if ((thread.ThreadState & state) == state)
                            break;

                        Thread.Sleep(10);
                        retryCount++;
                        if (retryCount > 50)
                            return "Failed to suspend thread: " + thread.ThreadState.ToString();
                    }
                    stackTrace = new System.Diagnostics.StackTrace(thread, true);
                }
                else
                {
                    stackTrace = new System.Diagnostics.StackTrace(1, true);
                }
                //string fullTrace = stackTrace.ToString();

                int currFrameID = 0;
                if (thread == System.Threading.Thread.CurrentThread)
                    currFrameID = 1;

                while (currFrameID < stackTrace.FrameCount)
                {
                    System.Diagnostics.StackFrame sf = stackTrace.GetFrame(currFrameID);

                    MethodBase method = sf.GetMethod();
                    string declaringType = "<unknown>";
                    if (method.DeclaringType != null)
                        declaringType = method.DeclaringType.FullName;

                    StringBuilder methodName = new StringBuilder(declaringType + "." + method.Name + "(");

                    ParameterInfo[] parameters = method.GetParameters();
                    foreach (ParameterInfo param in parameters)
                    {
                        if (!methodName.ToString().EndsWith("("))
                            methodName.Append(", ");
                        if (param.IsOptional)
                            methodName.Append("Optional ");

                        methodName.Append(param.ParameterType.Name);
                    }
                    methodName.Append(")");

                    text.Append("  " + methodName);

                    int lineNumber = sf.GetFileLineNumber();
                    if (lineNumber > 0)
                    {
                        text.Append(" : " + lineNumber.ToString());
                    }

                    text.Append(Environment.NewLine);

                    currFrameID += 1;
                }
            }
            catch (Exception e)
            {
                return "Exception getting stack trace. Thread: " + thread.Name + ", Error: " + e.Message;
            }
            finally
            {
                try
                {
                    thread.Resume();
                }
                catch
                {
                }
            }

            return text.ToString();
        }

    }
}
