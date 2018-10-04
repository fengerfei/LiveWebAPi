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
                r.chatstatus,r.livestatus,r.liveid,u1.name AS mainname,u2.name AS livename,
                r.hxroomid,r.liveroomid from liveroom r left join
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
                r.chatstatus,r.livestatus,r.liveid,u1.name AS mainname,u2.name AS livename,
                r.hxroomid,r.liveroomid,h.id as hisid from liveroom r left join
                 user u1 on r.mainid = u1.id left join user u2 on r.liveid = u2.id 
                 left join roomhistory h on r.id = h.roomid and h.isopen = 1";

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

            DateTime st =  DateTime.Now;

            table.id = req.id;
            table.liveid = req.liveid;
            table.flag = req.flag;
            table.livestatus = req.livestatus;
            table.mainid = req.mainid;
            table.memo = req.memo;
            table.roomname = req.roomname;
            table.chatstatus = req.chatstatus;
            table.isclose = req.isclose;
            table.hxroomid = req.hxroomid;
            table.liveroomid = req.liveroomid;
            table.isOpen = req.isOpen;

            if (req.isOpen)
            {
                if (String.IsNullOrEmpty(req.startTime)){
                    table.startTime = st.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    table.startTime =req.startTime;
                }
                
            }


            DbEntity dbEntity = new DbEntity(dbhelper);

            dbEntity.Update(table,"id");

            if (req.isOpen){
                //新增历史表
                RoomHistroyTable rht = new RoomHistroyTable();
                rht.roomid = req.id;
                rht.startTime = st;
                rht.liveid = req.liveid;
                rht.flag = req.flag;
                rht.mainid = req.mainid;
                rht.memo = req.memo;
                rht.hxroomid = req.hxroomid;
                rht.liveroomid = req.liveroomid;
                rht.isopen = req.isOpen;
                rht.roomname = req.roomname;
                dbEntity.Insert(rht);


                //插入历史表
                int hisid = HistroyRoomDL.GetHisId(dbhelper, rht.roomid);
                try
                {
                    UserJoinRoomHisTable his = new UserJoinRoomHisTable();
                    his.hisid = hisid;
                    his.roomid = rht.roomid;
                    his.userid = req.liveid;
                    his.jointime = DateTime.Now;
                    dbEntity.Insert(his);
                }
                catch (Exception ex)
                {
                    //重复插入会报错，忽略掉这里

                }


            }

            
        }
        public void changeRoommainid(DbHelper dbhelper,string roomid,int userid)
        {
            string sql = @" update liveroom set mainid = @userid where id=@roomid";
            dbhelper.AddParameter("@roomid", roomid);
            dbhelper.AddParameter("@userid", userid);
            dbhelper.ExecuteNonQuerySQL(sql);

        }

        public IList<userEntiy> getRoomNumber(DbHelper dbhelper, RoomreqEntity req)
        {
            string sql = @"select n.id,n.name,n.password,n.usertype,n.lastlogintime,n.status,n.Flag,n.insystem,n.roomid,
                        n.longitude,n.latitude,n.freeStartTime from user n inner join liveroom l on n.roomid=l.id
                        where 1=1";
            if (!string.IsNullOrEmpty(req.roomname))
            {
                sql += " and l.roomname = @name";
                dbhelper.AddParameter("@name", req.roomname);
            }
            if (!String.IsNullOrEmpty(req.roomid) && !req.roomid.Equals("0"))
            {
                sql += " and n.roomid = @roomid";
                dbhelper.AddParameter("@roomid", req.roomid);
            }
            sql += " order by status desc,freeStartTime";

            DbEntity dbEntity = new DbEntity(dbhelper);
            var result = dbEntity.Select<userEntiy>(sql);
            return result;
        }

        public void CloseRoom(DbHelper dbhelper, ChangeUserRoom req)
        {

            //首先是所有群成员状态变成BCDEA离线
            string sql = @"update user set status=0, roomid='0' where roomid=@roomid and usertype>1";
            dbhelper.AddParameter("@roomid", req.roomid);
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"update user set status=0 where roomid=@roomid and usertype=1";
            dbhelper.AddParameter("@roomid", req.roomid);
            dbhelper.ExecuteNonQuerySQL(sql);
            //其次聊天室群主写为空


            //最后更新历史记录状态改变
            sql = @" UPDATE roomhistory h INNER JOIN liveroom r ON h.roomid=r.id 
            SET h.roomname = r.roomname,h.memo = r.memo,h.flag=r.flag,h.mainid = r.mainid,
            h.endtime = CURRENT_TIMESTAMP,h.isOpen=FALSE
            WHERE h.roomid = @roomid AND h.isOpen=1 ";
            dbhelper.AddParameter("@roomid", req.roomid);
            dbhelper.ExecuteNonQuerySQL(sql);

            //最后群清空

            sql = @"update liveroom set mainid=0,flag='',liveroomid='0',isclose=1,isopen=0 where id=@roomid ";
            dbhelper.AddParameter("@roomid", req.roomid);
            dbhelper.ExecuteNonQuerySQL(sql);

        }

        public LiveRoomEntiy GetRoomById(DbHelper dbhelper, string roomid)
        {
            string sql = @"select r.id as roomid,r.roomname,r.starttime,r.memo,r.flag,r.mainid,
                r.chatstatus,r.livestatus,r.liveid,u1.name AS mainname,u2.name AS livename,
                r.hxroomid,r.liveroomid from liveroom r left join
                 user u1 on r.mainid = u1.id left join user u2 on r.liveid = u2.id 
                where r.id = @roomid";

            dbhelper.AddParameter("@roomid", roomid);
            DbEntity entity = new DbEntity(dbhelper);
            return entity.SelectFirst<LiveRoomEntiy>(sql);

        }
    }
}