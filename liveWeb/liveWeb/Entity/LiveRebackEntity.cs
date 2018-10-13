using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class LiveRebackEntity
    {
        public string publishdomain { get; set; }
        public string streamid { get; set; }
        public string filename { get; set; }
        public int filesize { get; set; }

        public string spacename { get; set; }

        public int duration { get; set; }

        public string url { get; set; }


    }
}