using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class LiveRoomDAL
    {
        public IList<LiveRoomEntiy> getRoomList(DbHelper dbhelper, LiveRoomReqEntity req)
        {
            string sql = @"select r.id as roomid,r.roomname,r.starttime,r.memo,r.flag,r.mainid,
                r.chatstatus,r.livestatus,r.liveid,u1.name AS mainname,u2.name AS livename from liveroom r left join
                 user u1 on r.mainid = u1.id left join user u2 on r.liveid = u2.id where 1=1 ";
            if (!String.IsNullOrEmpty(req.roomname))
            {
                sql += " and r.roomname like @roomname";
                dbhelper.AddLikeParameter("@roomname", req.roomname);
            }

            if (req.starttime != null)
            {
                sql += " and r.starttime >= @starttime";
                dbhelper.AddParameter("@starttime", req.starttime);

            }
            if (req.isOpen)
            {
                sql += " and r.isclose = 0";
            }

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<LiveRoomEntiy>(sql);

            
        }

        public LiveRoomEntiy GetMyRoom(DbHelper dbhelper, MyRoomReqEntity req)
        {
            string sql = @"select r.id as roomid,r.roomname,r.starttime,r.memo,r.flag,r.mainid,
                r.chatstatus,r.livestatus,r.liveid,u1.name AS mainname,u2.name AS livename from liveroom r left join
                 user u1 on r.mainid = u1.id left join user u2 on r.liveid = u2.id ";

            if (req.usertype == 0){
                sql +=" inner join user o on o.roomid = r.id where o.id =@userid";
            }
            else if (req.usertype == 1)
            {
                sql += " where u2.id = @userid";
            }
            else{
                sql += " where u1.id = @userid ";
            }
            dbhelper.AddParameter("@userid",req.userid);

            DbEntity entity = new DbEntity(dbhelper);
            return entity.SelectFirst<LiveRoomEntiy>(sql);

        }




        public void CreateRoom(DbHelper dbhelper, liveRoomCreateEntity req)
        {
            liveRoomCreateTable table = new liveRoomCreateTable();

            table.liveid = req.liveid;
            table.flag = req.flag;
            table.livestatus = req.livestatus;
            table.mainid = req.mainid;
            table.memo = req.memo;
            table.roomname = req.roomname;
            table.chatstatus = req.chatstatus;
            table.isclose = false;

            DbEntity dbEntity = new DbEntity(dbhelper);

            dbEntity.Insert(table);

        }

        public void UpdateRoom(DbHelper dbhelper, liveRoomUpdateEntity req)
        {
            liveRoomCreateTable table = new liveRoomCreateTable();

            table.id = req.id;
            table.liveid = req.liveid;
            table.flag = req.flag;
            table.livestatus = req.livestatus;
            table.mainid = req.mainid;
            table.memo = req.memo;
            table.roomname = req.roomname;
            table.chatstatus = req.chatstatus;
            table.isclose = req.isclose;

            DbEntity dbEntity = new DbEntity(dbhelper);

            dbEntity.Update(table,"id");

            
        }
    }
}