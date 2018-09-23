using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class userEntiy
    {
        public int id { get; set; }

        public string name { get; set; }
        public string password { get; set; }
        public int usertype {get;set;}
        public DateTime lastlogintime {get;set;}
        public int status {get;set;}

        public string Flag {get;set;}

        public bool insystem {get;set;}
        public int roomid {get;set;}

        public double longitude {get;set;}
        public double latitude {get;set;}

        public DateTime freeStartTime { get; set; }

    }
}