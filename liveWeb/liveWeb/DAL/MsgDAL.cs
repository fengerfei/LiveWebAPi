using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class MsgDAL
    {
        public IList<MsgInfoEntity> GetMsgInfo(DbHelper dbhelper, MsgListPagerQueryParams req)
        {
            string sql = @"select  id,username,msg,otherdata,roomid,msgtime,msgid 
                    from msg where roomid=@roomid and msgtime<@msgtime order by msgtime limit 0,"+req.PageSize;

            dbhelper.AddParameter("@roomid", req.roomid);
            dbhelper.AddParameter("@msgtime", req.TheTimeBefore);

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<MsgInfoEntity>(sql);            
        }

        public void InsertMsg(DbHelper db, MsgInfoEntity req)
        {

            //得到当前hisid
            int hisid = HistroyRoomDL.GetHisId(db, req.roomid);

            MsgInfoTable table = new MsgInfoTable();

            table.msg = req.msg;
            table.msgid = req.msgid;
            table.Msgtime = req.Msgtime;
            table.OtherData = req.OtherData;
            table.roomid = req.roomid;
            table.username = req.username;
            table.hisid = hisid;
            DbEntity entity = new DbEntity(db);

            entity.Insert(table);            



        }



        public IList<MsgInfoEntity> GetMsgInfo(DbHelper dbhelper, RoomHistroyMsgListQueryParams req)
        {
            string sql = @"select  id,username,msg,otherdata,roomid,msgtime,msgid 
                    from msg where hisid=@hisid and msgtime<@msgtime order by msgtime limit 0," + req.PageSize;

            dbhelper.AddParameter("@hisid", req.hisroomid);
            dbhelper.AddParameter("@msgtime", req.TheTimeBefore);

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<MsgInfoEntity>(sql);
        }

    }
}