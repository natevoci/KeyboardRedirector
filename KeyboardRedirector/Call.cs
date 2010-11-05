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
using System.Linq;
using System.Text;
using System.Threading;

namespace KeyboardRedirector
{
    class Call
    {
        public static void WithTimeout(int timeoutMilliseconds, bool abortThreadOnTimeout, Action action)
        {
            if (action == null) throw new ArgumentNullException("action");

            Thread workerThread = null;
            Action wrappedAction = () =>
            {
                workerThread = Thread.CurrentThread;
                action();
            };

            IAsyncResult result = wrappedAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                string stackTrace = "<unknown>";
                if ((workerThread != null) && (workerThread.IsAlive))
                {
                    stackTrace = StackTrace.GetStackTrace(workerThread);
                }
                if (abortThreadOnTimeout)
                    workerThread.Abort();
                throw new TimeoutException("Action failed to complete within the given timeout period. " + Environment.NewLine + Environment.NewLine + "Timeout caused at the following location:" + Environment.NewLine + stackTrace);
            } 
        }
        
    }
}
