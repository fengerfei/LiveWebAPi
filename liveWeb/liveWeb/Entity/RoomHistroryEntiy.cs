using Carpa.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class RoomHistroyBase
    {
        public string roomid { get; set; }
        public DateTime startTime { get; set; }
        public string memo { get; set; }
        public string flag { get; set; }
        public int mainid { get; set; }
        public int liveid { get; set; }

        public string hxroomid { get; set; }
        public string liveroomid { get; set; }

        public DateTime endtime { get; set; }

        public bool isopen { get; set; }

        public string roomname { get; set; }
    }

    public class RoomHistroyEntity :RoomHistroyBase
    {
        public int id { get; set; }
        public string livename { get; set; }

    }

    [EntityClass(LowerCaseKey = true, TableName = "roomhistory")]
    [Serializable]
    public class RoomHistroyTable :RoomHistroyBase
    {

    }

    [EntityClass(LowerCaseKey = true, TableName = "userjoinroomhis")]
    [Serializable]
    public class UserJoinRoomHisTable
    {
        public int hisid { get; set; }
        public string roomid { get; set; }
        public DateTime jointime { get; set; }
        public int userid { get; set; }
    }


    public class RoomHistroyReqEntity
    {
        public int userId { get; set; }
        public DateTime findDate { get; set; }
    }


    public class RoomHistroyMsgListQueryParams
    {
        public int PageSize { get; set; }
        //历史房间id
        public int hisroomid { get; set; }
        public DateTime TheTimeAfter { get; set; }
    }

    public class HistroyPostionReqEntity
    {
        public int userid { get; set; }

        public int hisroomid { get; set; }

        public DateTime? startTime { get; set; }

        public int Size { get; set; }
    }

    public class HisRoomMemberReqEntity
    {
        public int hisroomid { get; set; }
    }
}