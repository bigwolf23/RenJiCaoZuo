using System;
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

    public class TemplePayHistory
    {

    }

    public class HousePayHistory
    {

    }

    public class TempleInfoData
    {
        public int success;
        public int errorCode;
        public string msg;
        public Infobody body;
        public int mailTo;
        public string tel;
        public string expTextName;
    }

    public class Infobody
    {
        public InfoDatabody data;
    }

    public class InfoDatabody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        InfoOwnerbody owner;
        public string url;
    }

    public class InfoOwnerbody
    {
        public string id;
        public bool isNewRecord;
        public string remarks;
        public string createDate;
        public string updateDate;
        public string parentIds;
        public string name;
        public int sort;
        public string area;
        public string code;
        public string type;
        public string grade;
        public string address;
        public string zipCode;
        public string master;
        public string phone;
        public string fax;
        public string email;
        public string useable;
        public string primaryPerson;
        public string deputyPerson;
        public string childDeptList;
        public string parentId;
    }

    class TemplInfo
    {

    }
}
