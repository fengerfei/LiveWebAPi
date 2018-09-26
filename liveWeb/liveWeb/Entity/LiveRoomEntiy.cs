using Carpa.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class LiveRoomEntiy
    {
        public string roomid { get; set; }
        public string roomname { get; set; }
        public DateTime startTime { get; set; }
        public string memo { get; set; }
        public string flag { get; set; }
        public string mainid { get; set; }
        public string mainname { get; set; }

        public int chatstatus { get; set; }
        public int livestatus { get; set; }

        public int liveid { get; set; }
        public string livename { get; set; }

        public bool isclose { get; set; }

        public string hxroomid { get; set; }

        public string liveroomid { get; set; }

    }

    public class liveRoomCreateEntity
    {
        public int mainid { get; set; }
        public int liveid { get; set; }
        public int chatstatus { get; set; }
        public int livestatus { get; set; }
        public string roomname { get; set; }
        public string memo { get; set; }
        public string flag { get; set; }

        public string hxroomid { get; set; }

        public string liveroomid { get; set; }

    }

    public class liveRoomUpdateEntity:liveRoomCreateEntity
    {
        public bool isclose { get; set; }

        public string id { get; set; }
    }

    [EntityClass(LowerCaseKey = true, TableName = "liveroom")]
    [Serializable]
    public class liveRoomCreateTable : liveRoomUpdateEntity
    {

    }
}