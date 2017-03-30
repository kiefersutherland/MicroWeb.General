using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MicroWeb.General
{
    /// <summary>
    /// 常用的方法
    /// </summary>
    public   class  Common
    {

        //随机小数 如 0.12563256
        public static double GetRandomNumber()
        {
            double minimum = 0;
            double maxmum = 1;
            int Len = 8;
            Random random = new Random();
            return Math.Round(random.NextDouble() * (maxmum - minimum) + minimum, Len);
        }

        /// <summary>
        /// 去掉电话前的0
        /// </summary>
        /// <param name="Tel"></param>
        /// <returns></returns>
        public static string GetNoZeroTel(string Tel)
        {
            var cc = Tel;
            if (string.IsNullOrEmpty(Tel))
            {
                return cc;
            }
            if (cc.StartsWith("01") & cc.Length == 12)
            {
                cc = cc.Substring(1);
            }

            return cc;
        }



        /// <summary>
        ///截字,15字
        /// </summary> 
        /// <returns></returns>
        public static string GetShortString(string NormalString)
        {
            var cc = NormalString;
            if (string.IsNullOrEmpty(cc))
            {
                return "";
            }
            if (NormalString.Length > 20)
            {
                cc = NormalString.Substring(0, 19) + "...";
            }
            return cc;
        }

    

        /// <summary>
        /// 防sql注入格式过滤
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string FormatHtmlStr(string html, int length, string end)
        {
            if (!string.IsNullOrEmpty(html))
            {
                Regex regex1 = new Regex(@"<script[/s/S]+</script *>", RegexOptions.IgnoreCase);
                Regex regex2 = new Regex(@" href *= *[/s/S]*script *:", RegexOptions.IgnoreCase);
                Regex regex3 = new Regex(@" no[/s/S]*=", RegexOptions.IgnoreCase);
                Regex regex4 = new Regex(@"<iframe[/s/S]+</iframe *>", RegexOptions.IgnoreCase);
                Regex regex5 = new Regex(@"<frameset[/s/S]+</frameset *>", RegexOptions.IgnoreCase);
                Regex regex6 = new Regex(@"/<img[^/>]+/>", RegexOptions.IgnoreCase);
                Regex regex7 = new Regex(@"</p>\n", RegexOptions.IgnoreCase);
                //  System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"</span> \n", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Regex regex9 = new Regex(@"\n\t", RegexOptions.IgnoreCase);
                Regex regex10 = new Regex(@"</p>", RegexOptions.IgnoreCase);
                Regex regex11 = new Regex(@"<p>", RegexOptions.IgnoreCase);
                Regex regex12 = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
                Regex regex13 = new Regex(@"&nbsp;", RegexOptions.IgnoreCase);

                html = regex1.Replace(html, ""); //过滤<script></script>标记
                html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
                html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
                html = regex4.Replace(html, ""); //过滤iframe
                html = regex5.Replace(html, ""); //过滤frameset
                html = regex6.Replace(html, ""); //过滤frameset
                html = regex7.Replace(html, ""); //过滤frameset
                // html = regex8.Replace(html, ""); //过滤frameset
                html = regex9.Replace(html, "");
                html = regex10.Replace(html, "");
                html = regex11.Replace(html, "");
                html = regex12.Replace(html, "");
                html = regex13.Replace(html, " ");

                html = html.Replace("</strong>", "");
                html = html.Replace("<strong>", "");
                //html=html.Replace("\t", "");
                var tempStr = html.Replace(" ", "");
                if (tempStr.Length > length)
                {
                    return html.Substring(0, length) + end;
                }
                return html;
            }
            return "";
        }

        /// <summary>
        /// 过滤参数,防止SQL语句注入 搜索需要空格 括号等等
        /// </summary>
        /// <param name="str">string</param>
        /// <returns> string</returns>
        public static string FilterSql(string str)
        {
            string strs = str;
            if (!string.IsNullOrEmpty(strs))
            {
                strs = strs.Trim();
                strs.ToLower();
                strs = strs.Replace(@"exec", "");
                strs = strs.Replace("master", "");
                strs = strs.Replace("truncate", "");
                strs = strs.Replace("declare", "");
                strs = strs.Replace("create", "");
                strs = strs.Replace("xp_", "no");
                strs = strs.Replace(" ", "");      
                strs = strs.Replace("'", "");
                strs = strs.Replace("=", "");
                strs = strs.Replace("%", "");
                strs = strs.Replace("<", "");
                strs = strs.Replace(">", "");
                strs = strs.Replace("(", "");
                strs = strs.Replace(")", "");
                strs = strs.Replace("/", "");
                strs = strs.Replace(@"\", "");
                strs = strs.Replace("<br>", "");
                strs = strs.Replace("insert", "");
                strs = strs.Replace("update", "");
                strs = strs.Replace("select", "");
                strs = strs.Replace("delete", "");
                strs = strs.Replace("<>", "");
                strs = strs.Replace("in", "");
                strs = strs.Replace("or", "");
                strs = strs.Replace("and", "");
                strs = strs.Replace("not", "");
                strs = strs.Replace("+", "");
                strs = strs.Replace("&", "");
                strs = strs.Replace("&lt", "");
                strs = strs.Replace("&gt", "");
                strs = strs.Replace("&#39", "");
                strs = strs.Replace("--", "");
                strs = strs.Replace("*", "");
                strs = strs.Replace("&", "");
            }
            else
            {
                strs = "";
            }
            return strs.Trim();
        }


         

        /// <summary>
        /// 写日志，方便测试（看网站需求，也可以改成把记录存入数据库）
        /// </summary>
        /// <param name="sWord">要写入日志里的文本内容</param>
        public static void LogResult(string sWord)
        {
            string folder =  "~/log/";
            string strPath = HttpContext.Current.Server.MapPath(folder);
            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            strPath = strPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!File.Exists(strPath)) File.Create(strPath).Close();
            string log = DateTime.Now + "----------------------------------------添加-------------------------------  " + sWord;

            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("{0}", log);
                sw.Flush();
                sw.Close();

            }

        }


        /// <summary>
        /// 取得N位验证(随机)数字
        /// </summary>
        /// <returns></returns>
        public static string getNCharVerifyCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(9);
                if (temp == t)
                {
                    return getNCharVerifyCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

 

        /// <summary>
        ///获得六位随机数
        /// </summary>
        /// <returns></returns>
        public string GetValidCode()
        {
            Random rd = new Random();
            string code = "";
            for (int i = 0; i < 6; i++)
            {
                code = code + rd.Next(0, 9).ToString();
            }
            return code;
        }
        ///// <summary>
        ///// 发送验证码信息
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="receivePhone"></param>
        ///// <param name="content"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public int SendSMS(string userId, string receivePhone, string content, int type)
        //{
        //    return new MessageRecordDal().SendSMS(userId, receivePhone, content, type);
        //}
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string SetValidCodeInSession( string val)
        {
            var name = ConfigurationManager.AppSettings["MicroWebValidCodeName"];
            if (HttpContext.Current.Session[name] != null)
            {
                HttpContext.Current.Session.Remove(name);
            }
           HttpContext.Current.Session.Add(name, val);

            return "true";
        }
        /// <summary>
        /// 获取验证码session
        /// </summary>
        /// <returns></returns>
        public string GetSession()
        {
            var name = ConfigurationManager.AppSettings["MicroWebValidCodeName"];
            var cc = HttpContext.Current.Session[name];
            if (cc != null)
            {
                return cc.ToString() ;
            }
            return "";
        }


    }
}
