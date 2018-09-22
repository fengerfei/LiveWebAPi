using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class PagerEntity<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int RecordCount { get; set; }
        public bool HasMore { get; set; }
        public IList<T> RecordData { get; set; }
    }

    public class PagerExtraEntity<T> : PagerEntity<T>
    {
        public object ExtraData { get; set; }
    }
}