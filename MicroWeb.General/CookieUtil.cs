using System;
using System.Configuration;
using System.Web;

namespace MicroWeb.General
{
    /// <summary>
    /// cookie相关,对应 ConfigurationManager.AppSettings["MicroWebAppName"];
    /// </summary>
    public sealed class MicroWebCookieUtil
    {
        private static readonly string AppName = ConfigurationManager.AppSettings["MicroWebAppName"];
        
        public static void Add(string key, string value)
        {
            value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(AppName + key, value));
            HttpContext.Current.Request.Cookies.Remove(AppName + key);
            HttpContext.Current.Request.Cookies.Add(new HttpCookie(AppName + key, value));
 
        }
        public static void Add(string key, string value, string Domain)
        {
            value = HttpUtility.UrlEncode(value);
            var cc = new HttpCookie(AppName + key, value);
            cc.Domain = Domain;
        
            HttpContext.Current.Response.Cookies.Add(cc);
            HttpContext.Current.Request.Cookies.Remove(AppName + key);
            HttpContext.Current.Request.Cookies.Add(cc);
          
        }
        public static void Add(string key, string value, int Days)
        {
            value = HttpUtility.UrlEncode(value);
            var cc = new HttpCookie(AppName + key, value);
              cc.Expires = DateTime.Now.AddDays(Days);
            HttpContext.Current.Response.Cookies.Add(cc);
            HttpContext.Current.Request.Cookies.Remove(AppName + key);
            HttpContext.Current.Request.Cookies.Add(cc);

        }


        public static string Get(string key)
        {
            var keyC = HttpContext.Current.Request.Cookies[AppName + key];
            string str = "";
            if (keyC != null)
            {
                str = HttpUtility.UrlDecode(keyC.Value);
            }
            return str;
        }
        public static string ResponseGet(string key)
        {
            var keyC = HttpContext.Current.Response.Cookies[AppName + key];
            return keyC == null ? null : HttpUtility.HtmlDecode(keyC.Value);
        }

    }
}
