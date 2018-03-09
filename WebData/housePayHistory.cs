using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class HousePayHistory
    {
        public bool success { get; set; }
        public int errorCode { get; set; }
        public string msg { get; set; }
        public HousePayHistorybody body { get; set; }

    }

    public class HousePayHistorybody
    {
        public List<HousePayHistoryDatabody> data { get; set; }
    }

    public class HousePayHistoryDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody temple;
        InfoOwnerbody house;
        public string name;
        public string tel;
        public string payType;
        public string payTypeName;
        public int amount;
        public string content;
    }
}
