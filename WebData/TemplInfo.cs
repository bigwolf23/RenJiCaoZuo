﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenJiCaoZuo.WebData
{
    public class DispData
    {
        List<TemplePayHistory> lstTemplePay;
        List<HousePayHistory> lstHostPay;
    }

    public class TempleInfoData
    {
        public int success;
        public int errorCode;
        public string msg;
        public TempleInfobody body;
    }

    public class TempleInfobody
    {
        public TempleInfoDatabody data;
    }

    public class TempleInfoDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody owner;
        public string info;
        public string url;
        public string detail;
    }

    

    public class TemplePayHistory
    {

    }

    public class HousePayHistory
    {

    }


}
