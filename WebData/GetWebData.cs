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

        //寺庙信息
        public void GetTempleInfobyWebService()
        {
            string ssTempleInfo = getInfoFromInterFace("TempleInfo_Interface", "TempleInfo_Param", "TempleInfo_id");
            m_pTempInfoData = JsonConvert.DeserializeObject<TempleInfo>(ssTempleInfo);
        }
        //大师信息
        public void GetMonkInfobyWebService()
        {
            string ssMonkInfo = getInfoFromInterFace("MonkInfo_Interface", "MonkInfo_Param", "MonkInfo_id");
            m_pMonkInfoData = JsonConvert.DeserializeObject<MonkInfo>(ssMonkInfo);
        }
        //寺庙活动信息
        public void GetActivityInfobyWebService()
        {
            string ssString = getInfoFromInterFace("ActivityInfo_Interface", "ActivityInfo_Param", "ActivityInfo_id");
            m_pActivityInfoData = JsonConvert.DeserializeObject<ActivityInfo>(ssString);
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
            m_pqRCodeInfoData = JsonConvert.DeserializeObject<qRCodeInfo>(ssString);
        }

        //寺庙布施记录
        public void GetTemplePayHistorybyWebService()
        {
            string ssString = getInfoFromInterFace("TemplePayHistory_Interface", "TemplePayHistory_Param", "TemplePayHistory_id");
            m_pTemplePayHistoryData = JsonConvert.DeserializeObject<TemplePayHistory>(ssString);
        }

        //大殿布施记录
        public void GetHousePayHistorybyWebService()
        {
            string ssString = getInfoFromInterFace("housePayHistory_Interface", "housePayHistory_Param", "housePayHistory_id");
            m_pHousePayHistoryData = JsonConvert.DeserializeObject<HousePayHistory>(ssString);
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
        public string getInfoFromInterFace(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strFullInterface;
            string strBaseWebLink = setBaseWebLinkPath();
            string strInterfaceLink = getWebInterFaceLinkPath(Inferface_Field, Param_Field, Id_Field);
            strFullInterface = strBaseWebLink + strInterfaceLink;
            return HttpGet(strFullInterface);
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

        public static string HttpGet(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
