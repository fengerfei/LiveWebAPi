using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class PostionReqEntity
    {
        public int userid { get; set; }

        public string roomid { get; set; }

        public DateTime? startTime { get; set; }

        public int Size { get; set; } 
    }
}