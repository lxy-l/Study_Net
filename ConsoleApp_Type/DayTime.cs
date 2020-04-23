using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Type
{
    //创建一个Daytime类代码如下：
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public class Daytime
    {
        // Internet Time Server class by Alastair Dallas 01/27/04  

        // Number of seconds  
        private const int THRESHOLD_SECONDS = 15;
        // that Windows clock can deviate from NIST and still be okay  

        //Server IP addresses from   
        //http://www.boulder.nist.gov/timefreq/service/time-servers.html  
        private static string[] Servers = {
        "129.6.15.28",
        "129.6.15.29",
        "132.163.4.101",
        "132.163.4.102",
        "132.163.4.103",
        "128.138.140.44",
        "192.43.244.18",
        "131.107.1.10",
        "66.243.43.21",
        "216.200.93.8",
        "208.184.49.9",
        "207.126.98.204",
        "205.188.185.33"  
        //"65.55.21.15"
    };
        public static string LastHost = "";

        public static DateTime LastSysTime;
        public static DateTime GetTime()
        {
            //Returns UTC/GMT using an NIST server if possible,   
            // degrading to simply returning the system clock  

            //If we are successful in getting NIST time, then  
            // LastHost indicates which server was used and  
            // LastSysTime contains the system time of the call  
            // If LastSysTime is not within 15 seconds of NIST time,  
            //  the system clock may need to be reset  
            // If LastHost is "", time is equal to system clock  

            string host = null;
            DateTime result = default(DateTime);

            LastHost = "";
            foreach (string host_loopVariable in Servers)
            {
                host = host_loopVariable;
                result = GetNISTTime(host);
                if (result > DateTime.MinValue)
                {
                    LastHost = host;
                    break; // TODO: might not be correct. Was : Exit For  
                }
            }

            if (string.IsNullOrEmpty(LastHost))
            {
                //No server in list was successful so use system time  
                result = DateTime.UtcNow;
            }

            return result;
        }

        public static int SecondsDifference(DateTime dt1, DateTime dt2)
        {
            TimeSpan span = dt1.Subtract(dt2);
            return span.Seconds + (span.Minutes * 60) + (span.Hours * 360);
        }

        public static bool WindowsClockIncorrect()
        {
            DateTime nist = GetTime();
            if ((Math.Abs(SecondsDifference(nist, LastSysTime)) > THRESHOLD_SECONDS))
            {
                return true;
            }
            return false;
        }

        private static DateTime GetNISTTime(string host)
        {
            //Returns DateTime.MinValue if host unreachable or does not produce time  
            DateTime result = default(DateTime);
            string timeStr = null;

            try
            {
                StreamReader reader = new StreamReader(new TcpClient(host, 13).GetStream());
                LastSysTime = DateTime.UtcNow;
                timeStr = reader.ReadToEnd();
                reader.Close();
            }
            catch (SocketException ex)
            {
                //Couldn't connect to server, transmission error  
                Debug.WriteLine("Socket Exception [" + host + "]");
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                //Some other error, such as Stream under/overflow  
                return DateTime.MinValue;
            }

            //Parse timeStr  
            if ((timeStr.Substring(38, 9) != "UTC(NIST)"))
            {
                //This signature should be there  
                return DateTime.MinValue;
            }
            if ((timeStr.Substring(30, 1) != "0"))
            {
                //Server reports non-optimum status, time off by as much as 5 seconds  
                return DateTime.MinValue;
                //Try a different server  
            }

            int jd = int.Parse(timeStr.Substring(1, 5));
            int yr = int.Parse(timeStr.Substring(7, 2));
            int mo = int.Parse(timeStr.Substring(10, 2));
            int dy = int.Parse(timeStr.Substring(13, 2));
            int hr = int.Parse(timeStr.Substring(16, 2));
            int mm = int.Parse(timeStr.Substring(19, 2));
            int sc = int.Parse(timeStr.Substring(22, 2));

            if ((jd < 15020))
            {
                //Date is before 1900  
                return DateTime.MinValue;
            }
            if ((jd > 51544))
                yr += 2000;
            else
                yr += 1900;

            return new DateTime(yr, mo, dy, hr, mm, sc);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public Int16 wYear;
            public Int16 wMonth;
            public Int16 wDayOfWeek;
            public Int16 wDay;
            public Int16 wHour;
            public Int16 wMinute;
            public Int16 wSecond;
            public Int16 wMilliseconds;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern Int32 GetSystemTime(ref SYSTEMTIME stru);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern Int32 SetSystemTime(ref SYSTEMTIME stru);

        public static void SetWindowsClock(DateTime dt)
        {
            //Sets system time. Note: Use UTC time; Windows will apply time zone  

            SYSTEMTIME timeStru = default(SYSTEMTIME);
            Int32 result = default(Int32);

            timeStru.wYear = (Int16)dt.Year;
            timeStru.wMonth = (Int16)dt.Month;
            timeStru.wDay = (Int16)dt.Day;
            timeStru.wDayOfWeek = (Int16)dt.DayOfWeek;
            timeStru.wHour = (Int16)dt.Hour;
            timeStru.wMinute = (Int16)dt.Minute;
            timeStru.wSecond = (Int16)dt.Second;
            timeStru.wMilliseconds = (Int16)dt.Millisecond;
            result = SetSystemTime(ref timeStru);
        }
    }
}
