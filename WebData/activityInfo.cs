using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class ActivityInfoData
    {
        public int success;
        public int errorCode;
        public string msg;
        public ActivityInfobody body;

    }

    public class ActivityInfobody
    {
        public List<ActivityInfoDatabody> data;
    }

    public class ActivityInfoDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody owner;
        public string dt;
        public string activity;
        public string display;
    }
}
