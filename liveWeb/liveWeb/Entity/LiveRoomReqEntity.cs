﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class LiveRoomReqEntity
    {
        public string roomname { get; set; }
        public DateTime? starttime { get; set; }
        public bool isOpen { get; set; }
    }

    public class MyRoomReqEntity
    {
        public int userid { get; set; }

        public int usertype { get; set; }
    }

    public class RoomreqEntity
    {
        public string roomid { get; set;}
        public string roomname { get; set; }
    }

}