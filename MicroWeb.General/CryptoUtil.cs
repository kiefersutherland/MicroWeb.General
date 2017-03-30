using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroWeb.General
{
    /// <summary>
    /// 密码相关
    /// </summary>
    public static class CryptoUtil
    {
        private const string IV_KEY = "CCTV5WCF";


        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < ba.Length; i += 2)
            {
                if (ba[i + 1] == 0)
                {
                    //数字,大小写字母,以及"+-*/._"不变
                    if (
                          (ba[i] >= 48 && ba[i] <= 57)
                        || (ba[i] >= 64 && ba[i] <= 90)
                        || (ba[i] >= 97 && ba[i] <= 122)
                        || (ba[i] == 42 || ba[i] == 43 || ba[i] == 45 || ba[i] == 46 || ba[i] == 47 || ba[i] == 95)
                        )//保持不变
                    {
                        sb.Append(Encoding.Unicode.GetString(ba, i, 2));

                    }
                    else//%xx形式
                    {
                        sb.Append("%");
                        sb.Append(ba[i].ToString("X2"));
                    }
                }
                else
                {
                    sb.Append("%u");
                    sb.Append(ba[i + 1].ToString("X2"));
                    sb.Append(ba[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }



        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encryptValue"></param>
        /// <returns></returns>
        public static string Md5(string encryptValue)
        {
            if (string.IsNullOrEmpty(encryptValue))
                return encryptValue;
            var md5 = MD5.Create();
            //将输入的字符串转换成字节数组
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptValue));
            //加密后的字符串为strMD5
            return BinaryToHex(bt);
            //string strMD5 = Convert.ToBase64String(bt);
            //return strMD5;
            // string str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(encryptValue, "MD5").ToLower();
            // return str;
        }

        public static string BinaryToHex(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            char[] chArray = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                byte num2 = data[i];
                chArray[2 * i] = NibbleToHex((byte)(num2 >> 4));
                chArray[(2 * i) + 1] = NibbleToHex((byte)(num2 & 15));
            }
            return new string(chArray);
        }

        private static char NibbleToHex(byte nibble)
        {
            return ((nibble < 10) ? ((char)(nibble + 0x30)) : ((char)((nibble - 10) + 0x41)));
        }

        /// <summary>
        /// 签名
        /// </summary>
        public static string MakeSign(string appSecrect, params object[] parts)
        {
            var arr = new List<string>();
            foreach (var part in parts)
            {
                if (part != null)
                {
                    var type = part.GetType();
                    type = Nullable.GetUnderlyingType(type) ?? type;
                    if (type.IsEnum)
                    {
                        var newValue = Convert.ChangeType(part, System.Enum.GetUnderlyingType(type));
                        arr.Add(newValue.ToString());
                    }
                    else
                        arr.Add(part.ToString());
                }
            }
            var temp = string.Join("-", arr);
            return Encrypt(appSecrect, temp);
        }

        /// <summary>
        /// 3Des加密
        /// </summary>
        public static string Encrypt(string key, string orignal)
        {
            if (string.IsNullOrEmpty(orignal))
                return string.Empty;
            var provider = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = Convert.FromBase64String(key),
                IV = Encoding.ASCII.GetBytes(IV_KEY)
            };
            var buffer = Encoding.UTF8.GetBytes(orignal);
            using (var stream1 = new MemoryStream())
            using (var stream = new CryptoStream(stream1, provider.CreateEncryptor(), CryptoStreamMode.Write))
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.FlushFinalBlock();
                stream.Close();
                var result = Convert.ToBase64String(stream1.ToArray());
                return result;
            }
        }

        /// <summary>
        /// 3Des解密
        /// </summary>
        public static string Descrypt(string key, string orignal)
        {
            try
            {
                if (string.IsNullOrEmpty(orignal))
                    return string.Empty;
                var provider = new TripleDESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    Key = Convert.FromBase64String(key),
                    IV = Encoding.ASCII.GetBytes(IV_KEY)
                };
                var buffer = Convert.FromBase64String(orignal);
                using (var stream1 = new MemoryStream())
                using (var stream = new CryptoStream(stream1, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream.Write(buffer, 0, buffer.Length);
                    stream.FlushFinalBlock();
                    stream.Close();
                    var result = Encoding.UTF8.GetString(stream1.ToArray());
                    return result;
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
