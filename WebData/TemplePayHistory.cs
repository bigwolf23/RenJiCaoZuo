using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class TemplePayHistoryData
    {
        public int success;
        public int errorCode;
        public string msg;
        public TemplePayHistorybody body;

    }

    public class TemplePayHistorybody
    {
        public List<TemplePayHistoryDatabody> data;
    }

    public class TemplePayHistoryDatabody
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
