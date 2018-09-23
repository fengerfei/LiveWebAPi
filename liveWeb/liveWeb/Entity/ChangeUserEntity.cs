using Carpa.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{


    public class ChangeUserEntity
    {
        public int id { get; set; }
        public int status { get; set; }
        public string Flag { get; set; }
        public int insystem { get; set; }

    }

    [EntityClass(LowerCaseKey = true, TableName = "user")]
    [Serializable]
    public class ChangeUserTable : ChangeUserEntity
    {
        public DateTime freeStartTime { get; set; }
    }

    public class ChangeUserRoom
    {
        public int userid { get; set; }
        public int roomid { get; set; }
    }

}