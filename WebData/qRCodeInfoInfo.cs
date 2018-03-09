using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class qRCodeInfoInfoData
    {
        public int success;
        public int errorCode;
        public string msg;
        public qRCodeInfoInfobody body;

    }

    public class qRCodeInfoInfobody
    {
        public qRCodeInfoInfoDatabody data;
    }

    public class qRCodeInfoInfoDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody owner;
        public string url;
    }
}
