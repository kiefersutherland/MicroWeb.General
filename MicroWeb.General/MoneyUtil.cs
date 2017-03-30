using System;
using System.Globalization;


namespace MicroWeb.General
{
    /// <summary>
    /// 主要为金额
    /// </summary>
    public class MoneyUtil
    {


        /// <summary>
        /// 取金额文字串
        /// </summary>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static string GetMoneyString(decimal? Money)
        {
            var cc = "";
            if (Money != null)
            {
                return Money.ToString();
            }

            return cc;
        }

        /// <summary>  
        /// 输入Float格式数字，将其转换为货币表达方式  
        /// </summary>  
        /// <param name="fmoney">传入的int数字</param>  
        /// <param name="ftype">货币表达类型：0=带￥的货币表达方式；1=不带￥的货币表达方式；其它=带￥的货币表达方式</param>  
        /// <returns>返回转换的货币表达形式</returns>  
        public static string Rmoney(decimal fmoney, int ftype = 1)
        {
            string _rmoney;
            try
            {
                switch (ftype)
                {
                    case 0:
                        _rmoney = string.Format("{0:C2}", fmoney);
                        break;

                    case 1:
                        _rmoney = string.Format("{0:N2}", fmoney);
                        break;

                    default:
                        _rmoney = string.Format("{0:C2}", fmoney);
                        break;
                }
            }
            catch
            {
                _rmoney = "";
            }

            return _rmoney;
        }



        /// <summary>
        /// 格式化数值，无小数
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal FormatPriceNoPoint(decimal price)
        {
            try
            {
                return decimal.Round((decimal)price, 0);
            }
            catch (Exception)
            {
                return price;
            }
        }



        /// <summary>
        /// 格式化数值，二位小数
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal FormatPrice(decimal price)
        {
            try
            {
                return decimal.Round((decimal)price, 2);
            }
            catch (Exception)
            {
                return price;
            }
        }

        /// <summary>
        /// 格式化数值，二位小数
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal? FormatPrice(decimal? price)
        {
            if (price == null)
            {
                return 0;
            }

            try
            {
                return decimal.Round((decimal)price, 2);
            }
            catch (Exception)
            {
                return price;
            }
        }

        /// <summary>
        /// 格式化百分比
        /// </summary>
        /// <param name="Percentage"></param>
        /// <returns></returns>
        public static string FormatPercentage(decimal? Percentage)
        {
            var cc = FormatPrice(Percentage) * 100 ?? 0;
            cc = decimal.Round((decimal)cc, 0);
            return cc + " %";
        }


        /// <summary>
        /// 格式化数值，无小数
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal? FormatPricenumber(decimal? price)
        {
            try
            {
                return price != null ? decimal.Round((decimal)price, 0) : price;
            }
            catch (Exception)
            {
                return price;
            }

        }



    }
}
