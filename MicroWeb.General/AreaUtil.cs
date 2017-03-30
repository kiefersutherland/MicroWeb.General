using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWeb.General
{
    /// <summary>
    /// 地区相关
    /// </summary>
  public  class AreaUtil
    {


        /// <summary>
        /// 地区转换成省市首拼
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static string HZAreaToPYSimple(string area)
        {
            var array = area.Split('-');

            if (array.Length == 1)
            {
                return "";
            }


            var PYArea = "";
            var ll = array.Length == 3 ? array.Length - 1 : array.Length;
            for (var i = 0; i < ll; i++)
            {
                if (i == 0)
                {
                    if (array[i] == "内蒙古自治区" || array[i] == "黑龙江省")
                    {
                        array[i] = array[i].Substring(0, 3);
                    }
                    else
                    {
                        array[i] = array[i].Substring(0, 2);
                    }
                }
                else if (i == 1)
                {
                    if (array[i].Contains("市"))
                    {
                        array[i] = array[i].Substring(0, array[i].Length - 1);
                    }
                }
                else
                {
                    array[i] = "";
                }

                PYArea += HZToPYSimple(array[i]);
            }

            //20161130加
            if (PYArea.Length > 4)
            {
                PYArea = PYArea.Substring(0, 4);
            }

            return PYArea;
        }



        /// <summary>
        /// 得到汉字拼音首字母
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <returns></returns>
        public static string HZToPYSimple(string chineseStr)
        {
            try
            {
                byte[] b = UnicodeEncoding.Default.GetBytes(chineseStr);
                string res = "";
                for (int i = 0; i < b.Length;)
                {
                    if (i == b.Length - 1)
                    {
                        if (char.IsNumber((char)b[i]) || char.IsLetter((char)b[i]))
                            res += (char)b[i++];
                        else
                        {
                            i++;
                            continue;
                        }
                    }
                    else if (Convert.ToByte(b[i]) > 127)//汉字
                    {
                        string tmp = UnicodeEncoding.Default.GetString(b, i, 2);
                        //    tmp = HZToCode(tmp, "拼");
                        tmp = HZToCode(tmp);
                        if (tmp.Length > 0)
                            res += tmp[0];
                        i += 2;
                    }
                    else
                    {
                        if (char.IsNumber((char)b[i]) || char.IsLetter((char)b[i]))
                            res += (char)b[i++];
                        else
                        {
                            i++;
                            continue;
                        }
                    }
                }
                return res.ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception("错误:", ex);
            }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        private static string HZToCode(string chineseStr)//typeStr是指拼音还是五笔码
        {
            try
            {
                string resultStr = "";
                byte[] arrCN = Encoding.Default.GetBytes(chineseStr);
                if (arrCN.Length > 1)
                {
                    int area = (short)arrCN[0];
                    int pos = (short)arrCN[1];
                    int code = (area << 8) + pos;
                    int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614,
                        48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387,
                        51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                    for (int i = 0; i < 26; i++)
                    {
                        int max = 55290;
                        if (i != 25) max = areacode[i + 1];
                        if (areacode[i] <= code && code < max)
                        {
                            resultStr = Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                            break;
                        }
                    }
                }

                return resultStr;
            }
            catch (Exception ex)
            {
                throw new Exception("错误:", ex);
            }
        }





        private static Dictionary<string, string> fAreasCache;

        private static Dictionary<string, string> GetAreas()
        {
            if (fAreasCache == null)
            {
                var result = new Dictionary<string, string>();
                result.Add("北京市", "10");
                result.Add("上海市", "21");
                result.Add("天津市", "22");
                result.Add("重庆市", "23");

                //********************河南省*******************************
                result.Add("漯河市", "395");
                result.Add("郑州市", "371");
                result.Add("开封市", "378");
                result.Add("三门峡市", "398");
                result.Add("洛阳市", "379");
                result.Add("焦作市", "391");
                result.Add("新乡市", "373");
                result.Add("鹤壁市", "392");
                result.Add("安阳市", "372");
                result.Add("濮阳市", "393");
                result.Add("商丘市", "370");
                result.Add("许昌市", "374");
                result.Add("平顶山市", "375");
                result.Add("南阳市", "377");
                result.Add("信阳市", "376");
                result.Add("周口市", "394");
                result.Add("驻马店市", "396");
                result.Add("济源市", "391");

                //********************河北省*******************************
                result.Add("石家庄市", "311");
                result.Add("唐山市", "315");
                result.Add("秦皇岛市", "355");
                result.Add("邯郸市", "310");
                result.Add("邢台市", "319");
                result.Add("保定市", "312");
                result.Add("张家口市", "313");
                result.Add("承德市", "314");
                result.Add("沧州市", "317");
                result.Add("廊坊市", "316");
                result.Add("衡水市", "318");

                //********************山西省*******************************
                result.Add("太原市", "351");
                result.Add("大同市", "352");
                result.Add("阳泉市", "353");
                result.Add("长治市", "355");
                result.Add("晋城市", "356");
                result.Add("朔州市", "349");
                result.Add("晋中市", "354");
                result.Add("运城市", "359");
                result.Add("忻州市", "350");
                result.Add("临汾市", "357");
                result.Add("吕梁市", "358");

                //********************内蒙古自治区*******************************
                result.Add("呼和浩特市", "471");
                result.Add("包头市", "472");
                result.Add("乌海市", "473");
                result.Add("赤峰市", "476");
                result.Add("通辽市", "475");
                result.Add("鄂尔多斯市", "477");
                result.Add("呼伦贝尔市", "470");
                result.Add("巴彦淖尔市", "478");
                result.Add("乌兰察布市", "474");
                result.Add("兴安盟", "482");
                result.Add("锡林郭勒盟", "479");
                result.Add("阿拉善盟", "483");

                //********************辽宁省*******************************
                result.Add("沈阳市", "24");
                result.Add("大连市", "411");
                result.Add("鞍山市", "412");
                result.Add("抚顺市", "413");
                result.Add("本溪市", "414");
                result.Add("丹东市", "415");
                result.Add("锦州市", "416");
                result.Add("营口市", "417");
                result.Add("阜新市", "418");
                result.Add("辽阳市", "419");
                result.Add("盘锦市", "427");
                result.Add("葫芦岛市", "429");
                result.Add("铁岭市", "410");
                result.Add("朝阳市", "421");

                //********************吉林省*******************************
                result.Add("长春市", "431");
                result.Add("吉林市", "432");
                result.Add("四平市", "434");
                result.Add("辽源市", "437");
                result.Add("通化市", "435");
                result.Add("白山市", "439");
                result.Add("松原市", "438");
                result.Add("白城市", "436");
                result.Add("延边朝鲜族自治州", "433");

                //********************黑龙江省*******************************
                result.Add("哈尔滨市", "451");
                result.Add("齐齐哈尔市", "452");
                result.Add("鸡西市", "467");
                result.Add("鹤岗市", "468");
                result.Add("双鸭山市", "469");
                result.Add("大庆市", "459");
                result.Add("伊春市", "458");
                result.Add("佳木斯市", "454");
                result.Add("七台河市", "464");
                result.Add("牡丹江市", "453");
                result.Add("黑河市", "456");
                result.Add("绥化市", "455");
                result.Add("大兴安岭地区", "457");

                //********************江苏省*******************************
                result.Add("南京市", "25");
                result.Add("无锡市", "510");
                result.Add("徐州市", "516");
                result.Add("常州市", "519");
                result.Add("苏州市", "512");
                result.Add("南通市", "513");
                result.Add("连云港市", "518");
                result.Add("淮安市", "517");
                result.Add("盐城市", "515");
                result.Add("扬州市", "514");
                result.Add("镇江市", "511");
                result.Add("泰州市", "523");
                result.Add("宿迁市", "527");

                //********************浙江省*******************************
                result.Add("杭州市", "571");
                result.Add("宁波市", "574");
                result.Add("温州市", "577");
                result.Add("嘉兴市", "573");
                result.Add("湖州市", "572");
                result.Add("绍兴市", "575");
                result.Add("金华市", "579");
                result.Add("衢州市", "570");
                result.Add("舟山市", "580");
                result.Add("台州市", "576");
                result.Add("丽水市", "578");

                //********************安徽省*******************************
                result.Add("合肥市", "551");
                result.Add("芜湖市", "553");
                result.Add("蚌埠市", "552");
                result.Add("淮南市", "554");
                result.Add("马鞍山市", "555");
                result.Add("淮北市", "561");
                result.Add("铜陵市", "562");
                result.Add("安庆市", "556");
                result.Add("黄山市", "559");
                result.Add("滁州市", "550");
                result.Add("阜阳市", "558");
                result.Add("宿州市", "557");
                result.Add("巢湖市", "565");
                result.Add("六安市", "564");
                result.Add("亳州市", "558");
                result.Add("池州市", "566");
                result.Add("宣城市", "563");

                //********************福建省*******************************
                result.Add("福州市", "591");
                result.Add("厦门市", "592");
                result.Add("莆田市", "594");
                result.Add("三明市", "598");
                result.Add("泉州市", "595");
                result.Add("漳州市", "596");
                result.Add("南平市", "599");
                result.Add("龙岩市", "597");
                result.Add("宁德市", "593");

                //********************江西省*******************************
                result.Add("南昌市", "791");
                result.Add("景德镇市", "798");
                result.Add("萍乡市", "799");
                result.Add("九江市", "792");
                result.Add("新余市", "790");
                result.Add("鹰潭市", "701");
                result.Add("赣州市", "797");
                result.Add("吉安市", "796");
                result.Add("宜春市", "795");
                result.Add("抚州市", "794");
                result.Add("上饶市", "793");

                //********************山东省*******************************
                result.Add("济南市", "531");
                result.Add("青岛市", "532");
                result.Add("淄博市", "533");
                result.Add("枣庄市", "632");
                result.Add("东营市", "546");
                result.Add("烟台市", "535");
                result.Add("潍坊市", "536");
                result.Add("济宁市", "537");
                result.Add("泰安市", "538");
                result.Add("威海市", "631");
                result.Add("日照市", "633");
                result.Add("莱芜市", "634");
                result.Add("临沂市", "539");
                result.Add("德州市", "534");
                result.Add("聊城市", "635");
                result.Add("滨州市", "543");
                result.Add("菏泽市", "530");

                //********************湖北省*******************************
                result.Add("武汉市", "27");
                result.Add("黄石市", "714");
                result.Add("十堰市", "719");
                result.Add("宜昌市", "717");
                result.Add("襄樊市", "710");
                result.Add("鄂州市", "711");
                result.Add("荆门市", "724");
                result.Add("孝感市", "712");
                result.Add("荆州市", "716");
                result.Add("黄冈市", "713");
                result.Add("咸宁市", "715");
                result.Add("随州市", "722");
                result.Add("恩施土家族苗族自治州", "718");

                //********************湖南省*******************************
                result.Add("长沙市", "731");
                result.Add("株洲市", "733");
                result.Add("湘潭市", "732");
                result.Add("衡阳市", "734");
                result.Add("邵阳市", "739");
                result.Add("岳阳市", "730");
                result.Add("常德市", "736");
                result.Add("张家界市", "744");
                result.Add("益阳市", "737");
                result.Add("郴州市", "735");
                result.Add("永州市", "746");
                result.Add("怀化市", "745");
                result.Add("娄底市", "738");
                result.Add("湘西土家族苗族自治州", "743");

                //********************广东省*******************************
                result.Add("广州市", "20");
                result.Add("韶关市", "751");
                result.Add("深圳市", "755");
                result.Add("珠海市", "756");
                result.Add("汕头市", "754");
                result.Add("佛山市", "757");
                result.Add("江门市", "750");
                result.Add("湛江市", "759");
                result.Add("茂名市", "668");
                result.Add("肇庆市", "758");
                result.Add("惠州市", "752");
                result.Add("梅州市", "753");
                result.Add("汕尾市", "660");
                result.Add("河源市", "762");
                result.Add("阳江市", "662");
                result.Add("清远市", "763");
                result.Add("东莞市", "769");
                result.Add("中山市", "760");
                result.Add("潮州市", "768");
                result.Add("揭阳市", "663");
                result.Add("云浮市", "766");

                //********************广西壮族自治区*******************************
                result.Add("南宁市", "771");
                result.Add("柳州市", "772");
                result.Add("桂林市", "773");
                result.Add("梧州市", "774");
                result.Add("北海市", "779");
                result.Add("防城港市", "770");
                result.Add("钦州市", "777");
                result.Add("贵港市", "775");
                result.Add("玉林市", "775");
                result.Add("百色市", "776");
                result.Add("贺州市", "774");
                result.Add("河池市", "778");
                result.Add("来宾市", "772");
                result.Add("崇左市", "771");

                //********************海南省*******************************
                result.Add("海口市", "898");
                result.Add("三亚市", "898");

                //********************四川省*******************************
                result.Add("成都市", "28");
                result.Add("自贡市", "813");
                result.Add("攀枝花市", "812");
                result.Add("泸州市", "830");
                result.Add("德阳市", "838");
                result.Add("绵阳市", "816");
                result.Add("广元市", "839");
                result.Add("遂宁市", "825");
                result.Add("内江市", "832");
                result.Add("乐山市", "833");
                result.Add("南充市", "817");
                result.Add("眉山市", "28");
                result.Add("宜宾市", "831");
                result.Add("广安市", "826");
                result.Add("达州市", "818");
                result.Add("雅安市", "835");
                result.Add("巴中市", "827");
                result.Add("资阳市", "28");
                result.Add("阿坝藏族羌族自治州", "837");
                result.Add("甘孜藏族自治州", "836");
                result.Add("凉山彝族自治州", "834");

                //********************贵州省*******************************
                result.Add("贵阳市", "851");
                result.Add("六盘水市", "858");
                result.Add("遵义市", "852");
                result.Add("安顺市", "853");
                result.Add("铜仁地区", "856");
                result.Add("黔西南布依族苗族自治州", "859");
                result.Add("毕节地区", "857");
                result.Add("黔东南苗族侗族自治州", "855");
                result.Add("黔南布依族苗族自治州", "854");

                //********************云南省*******************************
                result.Add("昆明市", "871");
                result.Add("曲靖市", "874");
                result.Add("玉溪市", "877");
                result.Add("保山市", "875");
                result.Add("昭通市", "870");
                result.Add("丽江市", "888");
                result.Add("普洱市", "879");
                result.Add("临沧市", "883");
                result.Add("楚雄彝族自治州", "878");
                result.Add("红河哈尼族彝族自治州", "873");
                result.Add("文山壮族苗族自治州", "876");
                result.Add("西双版纳傣族自治州", "691");
                result.Add("大理白族自治州", "878");
                result.Add("德宏傣族景颇族自治州", "692");
                result.Add("怒江傈僳族自治州", "886");
                result.Add("迪庆藏族自治州", "887");

                //********************西藏自治区*******************************
                result.Add("拉萨市", "891");
                result.Add("昌都地区", "895");
                result.Add("山南地区", "893");
                result.Add("日喀则地区", "892");
                result.Add("那曲地区", "896");
                result.Add("阿里地区", "897");
                result.Add("林芝地区", "894");

                //********************陕西省*******************************
                result.Add("西安市", "29");
                result.Add("铜川市", "919");
                result.Add("宝鸡市", "917");
                result.Add("咸阳市", "910");
                result.Add("渭南市", "913");
                result.Add("延安市", "911");
                result.Add("汉中市", "916");
                result.Add("榆林市", "912");
                result.Add("安康市", "915");
                result.Add("商洛市", "914");

                //********************甘肃省*******************************
                result.Add("兰州市", "931");
                result.Add("嘉峪关市", "937");
                result.Add("金昌市", "935");
                result.Add("白银市", "943");
                result.Add("天水市", "938");
                result.Add("武威市", "935");
                result.Add("张掖市", "936");
                result.Add("平凉市", "933");
                result.Add("酒泉市", "937");
                result.Add("庆阳市", "934");
                result.Add("定西市", "932");
                result.Add("陇南市", "939");
                result.Add("临夏回族自治州", "930");
                result.Add("甘南藏族自治州", "941");

                //********************青海省*******************************
                result.Add("西宁市", "971");
                result.Add("海东地区", "975");
                result.Add("海北藏族自治州", "979");
                result.Add("黄南藏族自治州", "973");
                result.Add("海南藏族自治州", "974");
                result.Add("果洛藏族自治州", "976");
                result.Add("玉树藏族自治州", "973");
                result.Add("海西蒙古族藏族自治州", "977");

                //********************宁夏回族自治区*******************************
                result.Add("银川市", "951");
                result.Add("石嘴山市", "952");
                result.Add("吴忠市", "953");
                result.Add("固原市", "954");
                result.Add("中卫市", "955");

                //********************新疆维吾尔自治区*******************************
                result.Add("乌鲁木齐市", "991");
                result.Add("克拉玛依市", "990");
                result.Add("石河子市", "993");
                result.Add("吐鲁番地区", "995");
                result.Add("哈密地区", "997");
                result.Add("昌吉回族自治州", "994");
                result.Add("博尔塔拉蒙古自治州", "909");
                result.Add("巴音郭楞蒙古自治州", "996");
                result.Add("阿克苏地区", "997");
                result.Add("克孜勒苏柯尔克孜自治州", "908");
                result.Add("喀什地区", "998");
                result.Add("和田地区", "903");
                result.Add("伊犁哈萨克自治州", "999");
                result.Add("塔城地区", "992");
                result.Add("阿勒泰地区", "906");

                fAreasCache = result;
            }
            return fAreasCache;
        }

        public static string GetAreaCode(string area)
        {
            var areaArr = area.Split('-');
            var province = areaArr[0];
            var city = areaArr[1];
            Dictionary<string, string> result = GetAreas();
            var searchStr = city;
            if (province == "北京市" || province == "天津市" || province == "上海市" || province == "重庆市")
            {
                searchStr = province;
            }
            if (result.ContainsKey(searchStr))
            {
                return result[searchStr];
            }
            return "";
        }




    }
}
