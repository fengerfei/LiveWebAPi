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
                FROM roomhistory h INNER JOIN userjoinroomhis u ON h.id=u.hisid
                INNER JOIN user ua on h.liveid = ua.id
                WHERE h.isOpen = FALSE AND u.userid= @userid AND h.starttime>=@date AND h.endtime <=@dateend ";

            db.AddParameter("@userid",req.userId);
            db.AddParameter("@date",req.findDate);
            DateTime enddate = req.findDate.AddDays(1);
            db.AddParameter("@date",enddate);

            DbEntity entity = new DbEntity(db);
            return entity.Select<RoomHistroyEntity>(sql);

        }

        public static int GetHisId(DbHelper db, string roomid)
        {
            string sql = @"SELECT id FROM roomhistory WHERE roomid=@roomid AND isopen=1";

            db.AddParameter("@roomid", roomid);

            return db.SelectFirstRow(sql).GetValue<int>("id", 0);


        }
    }
}