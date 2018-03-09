using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class MonkInfoData
    {
        public int success;
        public int errorCode;
        public string msg;
        public MonkInfobody body;

    }

    public class MonkInfobody
    {
        public List<MonkInfoDatabody> data;
    }

    public class MonkInfoDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody owner;
        public string name;
        public string info;
        public string url;
        public int paixu;
    }
}
