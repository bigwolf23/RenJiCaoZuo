using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using log4net;
namespace RenJiCaoZuo.WebData
{
    public class GetWebData
    {
        public TempleInfo m_pTempInfoData = new TempleInfo();
        public MonkInfo m_pMonkInfoData = new MonkInfo();
        public ActivityInfo m_pActivityInfoData = new ActivityInfo();
        
        public qRCodeInfo m_pqRCodeInfoData  = new qRCodeInfo();
        public TemplePayHistory m_pTemplePayHistoryData = new TemplePayHistory();
        public HousePayHistory m_pHousePayHistoryData = new HousePayHistory();


        public GetWebData()
        {
            GetTempleInfobyWebService();
            GetMonkInfobyWebService();
            GetActivityInfobyWebService();
//             GetGetActivityInfobyWebService();
            GetTemplePayHistorybyWebService();
            GetqRCodeInfobyWebService();
            GetHousePayHistorybyWebService();
        }

        public string NoHTML(string Htmlstring)  //替换HTML标记
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "\"", RegexOptions.IgnoreCase);//保留【 “ 】的标点符合
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "\"", RegexOptions.IgnoreCase);//保留【 ” 】的标点符合
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        //获取前半部分link的字符串长度
        public string getLinkByPic()
        {
            string strDomino = ConfigurationManager.AppSettings["domino"];
            string strPort = ConfigurationManager.AppSettings["port"];
            string strInterfacelink = @"http://" + strDomino + @":" + strPort + @"/";
            return strInterfacelink;
        }

        //获取全部link的字符串长度
        public string getFullpathPicLink(string URLfromDatastruct)
        {
            string str3 = URLfromDatastruct.Substring(2, (URLfromDatastruct.Length - 2));
            return getLinkByPic() + str3;
        }

        //寺庙信息
        public void GetTempleInfobyWebService()
        {
            string ssTempleInfo = getInfoFromInterFace("TempleInfo_Interface", "TempleInfo_Param", "TempleInfo_id");
            if (ssTempleInfo.Length > 0)
            {
                m_pTempInfoData = JsonConvert.DeserializeObject<TempleInfo>(ssTempleInfo);

                m_pTempInfoData.body.data.info = NoHTML(m_pTempInfoData.body.data.info);
                m_pTempInfoData.body.data.detail = NoHTML(m_pTempInfoData.body.data.detail);

                m_pTempInfoData.body.data.url = getFullpathPicLink(m_pTempInfoData.body.data.url);
            }
            
        }
        //大师信息
        public void GetMonkInfobyWebService()
        {
            string ssMonkInfo = getInfoFromInterFace("MonkInfo_Interface", "MonkInfo_Param", "MonkInfo_id");
            if (ssMonkInfo.Length > 0)
            {
                m_pMonkInfoData = JsonConvert.DeserializeObject<MonkInfo>(ssMonkInfo);

                foreach (MonkInfoDatabody temp in m_pMonkInfoData.body.data)
                {
                    temp.url = getFullpathPicLink(temp.url);
                }
            }
        }
        //寺庙活动信息
        public void GetActivityInfobyWebService()
        {
            string ssString = getInfoFromInterFace("ActivityInfo_Interface", "ActivityInfo_Param", "ActivityInfo_id");
            if (ssString.Length > 0)
            {
                m_pActivityInfoData = JsonConvert.DeserializeObject<ActivityInfo>(ssString);
                foreach (ActivityInfoDatabody temp in m_pActivityInfoData.body.data)
                {
                    temp.detail = NoHTML(temp.detail);
                }
            }

        }

        //寺庙活动详细
        public void GetGetActivityInfobyWebService()
        {
            //string ssString = getInfoFromInterFace("GetActivityInfo_Interface", "GetActivityInfo_Param", "GetActivityInfo_id");
            //m_pActivityInfoData = JsonConvert.DeserializeObject<GetActivityInfoData>(ssString);
        }

        
        //二维码
        public void GetqRCodeInfobyWebService()
        {
            string ssString = getInfoFromInterFace("qRCodeInfo_Interface", "qRCodeInfo_Param", "qRCodeInfo_id");
            if (ssString.Length > 0)
            {
                m_pqRCodeInfoData = JsonConvert.DeserializeObject<qRCodeInfo>(ssString);

                m_pqRCodeInfoData.body.data.url = getFullpathPicLink(m_pqRCodeInfoData.body.data.url);
            }
        }

        //寺庙布施记录
        public void GetTemplePayHistorybyWebService()
        {
            string ssString = getInfoFromInterFace("TemplePayHistory_Interface", "TemplePayHistory_Param", "TemplePayHistory_id");
            if (ssString.Length > 0)
            {
                m_pTemplePayHistoryData = JsonConvert.DeserializeObject<TemplePayHistory>(ssString);
            }
        }

        //大殿布施记录
        public void GetHousePayHistorybyWebService()
        {
            string ssString = getInfoFromInterFace("housePayHistory_Interface", "housePayHistory_Param", "housePayHistory_id");
            if (ssString.Length > 0)
            { 
                m_pHousePayHistoryData = JsonConvert.DeserializeObject<HousePayHistory>(ssString); 
            }
        }

        //获取服务器Link
        public string setBaseWebLinkPath()
        {
            string strDomino = ConfigurationManager.AppSettings["domino"];
            string strPort = ConfigurationManager.AppSettings["port"];
            string strurl = ConfigurationManager.AppSettings["url"];
            string strInterfacelink = @"http://" + strDomino + @":" + strPort + strurl + @"/";
            return strInterfacelink;
        }
        //获取接口和参数
        public string getWebInterFaceLinkPath(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strInterfaceName = ConfigurationManager.AppSettings[Inferface_Field];
            string strParamName = ConfigurationManager.AppSettings[Param_Field];
            string strIdName = ConfigurationManager.AppSettings[Id_Field];
            string strInterfacelink = strInterfaceName + @"?" + strParamName + @"=" + strIdName;
            return strInterfacelink;
        }

        //获取所有Link
        public string getFullLink(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strBaseWebLink = setBaseWebLinkPath();
            string strInterfaceLink = getWebInterFaceLinkPath(Inferface_Field, Param_Field, Id_Field);
            string strFullInterface = strBaseWebLink + strInterfaceLink;
            return strFullInterface;
        }

        //获取所有Link的信息
        public string getInfoFromInterFace(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strFullInterface = getFullLink(Inferface_Field, Param_Field, Id_Field);
//             string strBaseWebLink = setBaseWebLinkPath();
//             string strInterfaceLink = getWebInterFaceLinkPath(Inferface_Field, Param_Field, Id_Field);
//             strFullInterface = strBaseWebLink + strInterfaceLink;
            return HttpGet(strFullInterface, Inferface_Field);
        }
//         private void GetInfobyWebService()
//         {
// //             //寺庙信息
// //             string ssTempleInfo = getInfoFromInterFace("TempleInfo_Interface", "TempleInfo_Param", "TempleInfo_id");
// //             //string ssTempleInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templeInfo?templeId=61ccf194f2b24e1a8a17d5a70251d589");
// //             //             TempleInfoData jp = (TempleInfoData)JsonConvert.DeserializeObject(ssTempleInfo);
// // 
// // 
// //             //ssTempleInfo
// //             //大师信息
// //             string ssMonkInfo = getInfoFromInterFace("MonkInfo_Interface", "MonkInfo_Param", "MonkInfo_id");
// //             //          string ssMonkInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/monkInfo?id=6217fb65b54848679590b9478182f527");
// // 
// //             //寺庙活动信息
// //             string ssActivityInfo = getInfoFromInterFace("ActivityInfo_Interface", "ActivityInfo_Param", "ActivityInfo_id");
// // 
// //             //          string ssActivityInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/activityInfo?id=f618a2a094ca4b419e828fd7d2aeade5");
// //             //寺庙活动详细
// //             //string ssActivityInfo = getInfoFromInterFace("GetActivityInfo_Interface", "GetActivityInfo_Param", "GetActivityInfo_id");
// //             //          string ssGetActivityById = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/getActivityById?id=12a9800b25e14ba190d6ba6e5a649c5d");
// //             //二维码
// //             string ssqRCodeInfo = getInfoFromInterFace("qRCodeInfo_Interface", "qRCodeInfo_Param", "qRCodeInfo_id");
//             //          string ssqRCodeInfo = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/qRCodeInfo?id=20f757cbac914ec3abd9e5686038430d");
//             //寺庙布施记录
//             string ssTemplePayHistory = getInfoFromInterFace("TemplePayHistory_Interface", "TemplePayHistory_Param", "TemplePayHistory_id");
//             //          string ssTemplePayHistory = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templePayHistory?id=73ba7b2b78e74ba0ac1d31d10270994c");
//             //大殿布施记录
//             string sshousePayHistory = getInfoFromInterFace("housePayHistory_Interface", "housePayHistory_Param", "housePayHistory_id");
//             //          string sshousePayHistory = HttpGet("http://39.108.244.227:8080/sim/a/pc/access/templePayHistory?id=73ba7b2b78e74ba0ac1d31d10270994c");
// 
//         }

        public string HttpGet(string url, string Inferface_Field)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            try
            {
                Encoding encoding = Encoding.UTF8;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (request.Connection != null)
                {
                    request.Method = "GET";
                    request.Accept = "text/html, application/xhtml+xml, */*";
                    request.ContentType = "application/json";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    LogHelper.Warn(typeof(GetWebData), url + "无法连接服务器");
                }
                
            }
            catch (WebException ex)
            {
                //HttpWebResponse res = (HttpWebResponse)ex.Response;
                MessageBox.Show(ex.Message);
                LogHelper.Error(typeof(GetWebData), url+ "无法连接服务器");
                //Inferface_Field
            }
            return "";
        }
    }
}
