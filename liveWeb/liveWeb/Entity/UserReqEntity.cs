using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class UserReqEntity
    {
        public int userType { get; set; } //0全部 1A 2B 3C 4E
        public int userStatus { get; set; } //-1全部0离线，1在线 2，忙碌 3 观察

        public string name { get; set; }

        public int sorttype { get; set; }   //排序类型 0,按状态来，先进入在前面，1.按登录时间来，后登录在前面。2按名字排序
    }
}