using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWeb.General
{

    /// <summary>
    /// 时间相关
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 时间戳转为C#格式时间(小于0的返回null)
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime? FromTimeStamp(long timeStamp)
        {
            if (timeStamp < 0)
                return null;
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var toNow = TimeSpan.FromSeconds(Convert.ToDouble(timeStamp));
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式(null值返回-1)
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static long ToTimeStamp(DateTime? time)
        {
            if (time == null)
                return -1;
            var temp = time.Value;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var seconds = (temp - startTime).TotalSeconds;
            return Convert.ToInt64(seconds);
        }

        /// <summary>
        /// 时间转换  20160501转成时间
        /// </summary>
        /// <param name="DateNumber"></param>
        /// <returns></returns>
        public static DateTime? NumberToDateTime(string DateNumber)
        {
            if (DateNumber.Length == 8)
            {
                var cc = DateNumber.Substring(0, 4) + "-" + DateNumber.Substring(4, 2) + "-" + DateNumber.Substring(6, 2);
                return Convert.ToDateTime(cc);
            }
            return DateTime.Now;
        }

        /// <summary>
        /// 一天的开始时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? DatesOfBegin(DateTime? time)
        {
            var a = Convert.ToDateTime(time);
            var cc = Convert.ToDateTime(a.ToString("yyyy-MM-dd 00:00:00"));
            return cc;
        }
        /// <summary>
        /// 一天的结束时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? DatesOfEnd(DateTime? time)
        {
            var a = Convert.ToDateTime(time);
            var cc = Convert.ToDateTime(a.ToString("yyyy-MM-dd 23:59:59"));
            return cc;
        }

        /// <summary>
        /// 日期部分
        /// </summary>
        public static DateTime? DatePart(this DateTime? input)
        {
            if (input == null)
                return null;
            return input.Value.Date;
        }

        /// <summary>
        /// 日期部分
        /// </summary>
        public static DateTime? TimePart(this DateTime? input)
        {
            if (input == null)
                return null;
            var temp = input.Value.ToShortTimeString();
            return Convert.ToDateTime(temp);
        }

        /// <summary>
        /// 长日期格式
        /// </summary>
        public static string Format(this DateTime? input, string format = "yyyy-MM-dd")
        {
            if (input == null)
                return null;
            return input.Value.ToString(format);
        }

        /// <summary>
        /// 长日期格式
        /// </summary>
        public static string LongDateString(this DateTime? input)
        {
            if (input == null)
                return null;
            return input.Value.ToLongDateString();
        }

        /// <summary>
        /// 短时间格式
        /// </summary>
        public static string ShortTimeString(this DateTime? input)
        {
            if (input == null)
                return null;
            return input.Value.ToShortTimeString();
        }

         


        /// <summary>
        /// 时间格式化yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static readonly string TimeFMT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间格式化yyyy-MM-dd   HH:mm
        /// </summary>
        public static readonly string TimeFMTMiddle = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 时间格式化yyyy-MM-dd 
        /// </summary>
        public static readonly string TimeFMTday = "yyyy-MM-dd";

        /// <summary>
        /// 时间格式化HH:mm
        /// </summary>
        public static readonly string TimeFMTMS = "HH:mm";


        /// <summary>
        /// 类似 201609 转换成  2016-09-01   
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstFromAPP(string Month)
        {
            var M1 = Month.Substring(0, 4);
            var M2 = Month.Substring(4);
            var cc = M1 + "-" + M2 + "-01";
            return Convert.ToDateTime(cc);
        }


        /// <summary>
        ///类似 201609 转换成  2016-09
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static string GetMonthDateString(string Month)
        {
            var M1 = Month.Substring(0, 4);
            var M2 = Month.Substring(4);
            var cc = M1 + "-" + M2;
            return cc;
        }




        /// <summary>
        /// 转换时间格式(string2Date)  无效？
        /// </summary>
        /// <param name="timestring"></param>
        /// <returns></returns>
        public static DateTime? TimeConvertByString(string timestring)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

            dtFormat.ShortDatePattern = TimeFMTday;

            DateTime? cc = Convert.ToDateTime(timestring, dtFormat);
            return cc;
        }

        public static DateTime TimeConvertByStringFix(string timestring)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

            dtFormat.ShortDatePattern = TimeFMTday;

            DateTime cc = Convert.ToDateTime(timestring, dtFormat);
            return cc;
        }

        /// <summary>
        /// 换成短时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetShortTime(DateTime? dateTime)
        {
            var tt = Convert.ToDateTime(dateTime);
            return tt.ToString(TimeFMTday);
        }


        /// <summary>
        ///  DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (time - startTime).TotalSeconds.ToString();
        }





        /// <summary>
        /// 转成类似2008年9月4日
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToLongDate(DateTime? datetime)
        {
            var cc = Convert.ToDateTime(datetime).ToLongDateString().ToString();
            return cc;
        }



        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>

        public static DateTime FirstDayOfMonth(DateTime datetime)
        {

            return datetime.AddDays(1 - datetime.Day);

        }




        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>

        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);

        }



        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }



        /// <summary>  
        /// 获取当前时间
        /// </summary>  
        /// <returns></returns>  
        public static DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 取时间简称
        /// </summary>
        /// <param name="inputTime"></param>
        /// <param name="referenceTime"></param>
        /// <returns></returns>
        public static string GetPeriodString(this DateTime inputTime, DateTime? referenceTime = null)
        {
            var referenceValue = referenceTime ?? DateTime.Now;
            var period = referenceValue - inputTime;
            if (period <= TimeSpan.FromMinutes(1))
                return "刚刚";
            var minutes = (int)Math.Round(period.TotalMinutes);
            if (minutes < 60)
                return minutes + "分钟前";
            var days = (int)(referenceValue.Date - inputTime.Date).TotalDays;
            if (days <= 0)
            {
                var hours = (int)Math.Round(period.TotalHours);
                return hours + "小时前";
            }
            if (days == 1)
                return "昨天";
            if (days == 2)
                return "前天";
            return inputTime.ToString("M月d日");
        }

    }
}
