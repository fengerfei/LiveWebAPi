using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class HistroyRoomDL
    {
        public IList<RoomHistroyEntity> GetHistroyRoom(DbHelper db, RoomHistroyReqEntity req)
        {
            string sql = @" SELECT h.id,h.roomid,ua.name as livename,h.starttime,h.memo,h.flag,h.mainid,h.liveid,h.hxroomid,h.liveroomid,h.endtime,h.isopen
                ,lr.roomname
                FROM roomhistory h INNER JOIN userjoinroomhis u ON h.id=u.hisid
                INNER JOIN user ua on h.liveid = ua.id
                INNER JOIN liveroom lr on h.roomid= lr.id
                WHERE h.isOpen = FALSE AND u.userid= @userid AND h.starttime>=@date AND h.endtime <=@dateend";

            //日期取整数处理


            DateTime sDate = Convert.ToDateTime(req.findDate.ToString("yyyy-MM-dd"));

            db.AddParameter("@userid",req.userId);
            db.AddParameter("@date", sDate);
            DateTime enddate = sDate.AddDays(1);
            db.AddParameter("@dateend", enddate);

            DbEntity entity = new DbEntity(db);
            return entity.Select<RoomHistroyEntity>(sql);

        }

        public static int GetHisId(DbHelper db, string roomid)
        {
            string sql = @"SELECT id FROM roomhistory WHERE roomid=@roomid AND isopen=1";

            db.AddParameter("@roomid", roomid);

            return db.SelectFirstRow(sql).GetValue<int>("id", 0);


        }

        internal IList<userEntiy> GetHistroyRoomMember(DbHelper dbhelper, HisRoomMemberReqEntity req)
        {
            string sql = @"select u.id,u.name,u.password,u.usertype,u.lastlogintime,u.status,u.Flag,u.insystem,u.roomid,
                        u.longitude,u.latitude,u.freeStartTime from user u inner join userjoinroomhis j on u.id=j.userid 
                        where j.hisid=@hisid ";
            sql += " order by u.usertype";

            dbhelper.AddParameter("@hisid", req.hisroomid);

            DbEntity dbEntity = new DbEntity(dbhelper);
            var result = dbEntity.Select<userEntiy>(sql);
            return result;            

        }
    }
}