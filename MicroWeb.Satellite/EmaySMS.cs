using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using MicroWeb.Satellite.EmayWebService;

namespace MicroWeb.Satellite
{
    /// <summary>
    /// 亿美短信方法相关类
    /// </summary>
    public class EmaySMS
    {


        //http://sdk4report.eucp.b2m.cn:8080/sdk/SDKService?wsdl   //新
        //http://sdk4http.eucp.b2m.cn/sdk/SDKService?wsdl     //旧  无限制，只限开发阶段，开发完后联系 010-58750491
        //http://sdkhttp.eucp.b2m.cn/sdk/SDKService?wsdl //旧  有白名单限制

        static readonly SDKService emaySMS = new SDKService();
        private static readonly  string _EmaySerialNo = ConfigurationManager.AppSettings["EmaySN"];
        private static readonly string _DecryptEmaykey = "hzataw2012";
        //亿美短信平台扩展号码 可为空
        private static readonly string _EmayaddSerial = "";
        private static readonly string _Emayserialpass = ConfigurationManager.AppSettings["EmaySNPass"];
        private static readonly string _strSignature = " 退订回复TD";


        #region 亿美短信方法


        //注册亿美
        public static string RegEmay()
        {
            //object[] obj = new object[3];
            //obj[0] = _EmaySerialNo ;
            //obj[1] = _DecryptEmaykey;
            //obj[2] = _Emayserialpass;

            //string name = "registEx";   //javaWebService开放的接口  
            //    string result = CallWebService(name, obj);
             
        int result = emaySMS.registEx(_EmaySerialNo, _DecryptEmaykey, _Emayserialpass);
            return GetRegEmayStatus(result.ToString());
        }


        //查询余额
        public static string GetEmayMoney()
        {
            //object[] obj = new object[2];
            //obj[0] = _EmaySerialNo;
            //obj[1] = _DecryptEmaykey;

            //string name = "getBalance";   //javaWebService开放的接口  
            //string result = CallWebService(name, obj);
            //return result;

            double mo = emaySMS.getBalance(_EmaySerialNo,
                               _DecryptEmaykey);
            return mo.ToString();
        }

        //注销亿美
        public static string unRegEmay()
        {
            //object[] obj = new object[2];
            //obj[0] = _EmaySerialNo;
            //obj[1] = _DecryptEmaykey;

            //string name = "logout";   //javaWebService开放的接口  
            //string result = CallWebService(name, obj);
            //return GetRegEmayStatus(result);

            int result = emaySMS.logout(_EmaySerialNo, _DecryptEmaykey);
            return GetRegEmayStatus(result.ToString());
        }

        #endregion



        #region 额外方法

        /// <summary>
        /// webservice固定调用方法
        /// </summary>
        /// <returns></returns>
        private static string CallWebService(string name, object[] obj)
        {
            try
            {
                string url = "http://sdk4rptws.eucp.b2m.cn:8080/sdk/SDKService";//wsdl地址  
                WebServiceProxy wsd = new WebServiceProxy(url, name);
                string suc = (string)wsd.ExecuteQuery(name, obj);
                if (obj.Length == 1)
                {
                    if (obj[0] != null)
                    {
                        //记录？
                    }

                }
                if (obj.Length == 2)
                {
                    if (obj[1] != null)
                    {   //记录？
                    }
                }

                //记录结果

                return suc;
            }
            catch (Exception e)
            {
                return "wsdlFail";
            }

        }



        /// <summary>
        /// 亿美注册状态
        /// </summary>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        private static  string GetRegEmayStatus(string  StatusID)
        {
            string StatusName = StatusID;
            string _StatusID = StatusID.ToString();
            switch (_StatusID)
            {
                //亿美
                case "0":
                    StatusName = "成功";
                    break;
                case "10":
                    StatusName = "客户端注册失败";
                    break;
                case "22":
                    StatusName = "注销失败";
                    break;
                case "303":
                    StatusName = "客户端网络故障";
                    break;
                case "305":
                    StatusName = "服务器端返回错误，错误的返回值（返回值不是数字字符串）";
                    break;
                case "999":
                    StatusName = "操作频繁";
                    break;
          

            }
            return StatusName;
        }



        /// <summary>
        /// 发送状态返回
        /// </summary>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        private static string GetStatus(string StatusID)
        {
            string StatusName = StatusID;
            switch (StatusID)
            {
                //亿美
                case "0":
                    StatusName = "短信发送成功";
                    break;
                case "17":
                    StatusName = "发送信息失败";
                    break;
                case "303":
                    StatusName = "客户端网络故障";
                    break;
                case "305":
                    StatusName = "服务器端返回错误，错误的返回值（返回值不是数字字符串）";
                    break;
                case "997":
                    StatusName = "平台返回找不到超时的短信，该信息是否成功无法确定";
                    break;
                case "998":
                    StatusName = "由于客户端网络问题导致信息发送超时，该信息是否成功下发无法确定";
                    break;
                //旧
                case "1000":
                    StatusName = "发送成功";
                    break;
                case "1001":
                    StatusName = "发送失败";
                    break;
                case "1002":
                    StatusName = "用户未设置密码";
                    break;
                case "1003":
                    StatusName = "密码不对";
                    break;
                case "1004":
                    StatusName = "用户不存在";
                    break;
                case "1005":
                    StatusName = "手机号码长度不正确";
                    break;
                case "1006":
                    StatusName = "手机号码中有非数字字符";
                    break;
                case "1007":
                    StatusName = "手机号码非法";
                    break;
                case "1008":
                    StatusName = "群发短信内容超过700字";
                    break;
                case "1009":
                    StatusName = "发送的手机号码(DestAddr)个数与参数(SMSCount)的值不一致";
                    break;
                case "1010":
                    StatusName = "发送的短信内容为空";
                    break;
                case "1011":
                    StatusName = "单条短信发送内容超过70字";
                    break;
            }
            return StatusName;
        }
        #endregion


    }
}
