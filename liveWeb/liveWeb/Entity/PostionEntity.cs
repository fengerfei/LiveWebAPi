using Carpa.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class PostionEntity
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }

    }

    public class PostionInfo :PostionEntity
    {
        public int postionid { get; set; }
        public DateTime recordtime { get; set; }
        public int roomid { get; set; }
    }

    [EntityClass(LowerCaseKey = true, TableName = "postion")]
    [Serializable]
    public class PostionTable:PostionEntity
    {

    }
}