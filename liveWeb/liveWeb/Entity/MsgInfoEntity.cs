using Carpa.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class MsgInfoEntity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string msg { get; set; }
        public string OtherData { get; set; }

        public string roomid { get; set; }
        public DateTime Msgtime { get; set; }

        public string msgid { get; set; }
    }

    [EntityClass(LowerCaseKey = true, TableName = "msg")]
    [Serializable]
    public class MsgInfoTable
    {
        public string username { get; set; }
        public string msg { get; set; }
        public string OtherData { get; set; }

        public string roomid { get; set; }
        public DateTime Msgtime { get; set; }

        public string msgid { get; set; }

        public int recordid { get; set; }

        public int hisid { get; set; }
    }

    public class MsgListPagerQueryParams
    {
        public int PageSize { get; set; }
        public string roomid { get; set; }
        public DateTime TheTimeBefore { get; set; }
    }




}