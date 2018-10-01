using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class PostionDAL
    {
        public void updatePostion(DbHelper dbhelper, PostionEntity req)
        {

            //得到当前hisid
            int hisid = HistroyRoomDL.GetHisId(dbhelper, req.RoomId);

            DbEntity entity = new DbEntity(dbhelper);

            
            PostionTable table = new PostionTable();

            table.UserId = req.UserId;
            table.latitude = req.latitude;
            table.longitude = req.longitude;
            table.RoomId = req.RoomId;
            table.hisid = hisid;
            entity.Insert(table);

            //更新user里面的位置

            string sql = @" update user set latitude=@latitude,longitude=@longitude
            where id=@userid";

            dbhelper.AddParameter("@longitude", req.longitude);
            dbhelper.AddParameter("@latitude", req.latitude);

            dbhelper.AddParameter("@userid", req.UserId);
            dbhelper.ExecuteNonQuerySQL(sql);
           
        }

        public IList<PostionInfo> GetPostion(DbHelper dbhelper, PostionReqEntity req)
        {
            string sql =@"select  id as postionid,UserId,longitude,latitude,roomid,recordtime 
                    from postion where userid=@userid ";
            
            dbhelper.AddParameter("@userid",req.userid);
            if (!string.IsNullOrEmpty(req.roomid) && req.roomid != "0")
            {

                sql +=" and roomid=@roomid";
                dbhelper.AddParameter("@roomid",req.roomid);
            }

            if (req.startTime != null)
            {
                sql += " and recordtime>@recordtime";
                dbhelper.AddParameter("@recordtime", req.startTime);
            }
            if (req.Size > 0)
            {
                sql += " limit 0," + req.Size;
            }

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<PostionInfo>(sql);

        }

        public IList<PostionInfo> GetHisPostion(DbHelper dbhelper, HistroyPostionReqEntity req)
        {
            string sql = @"select  id as postionid,UserId,longitude,latitude,roomid,recordtime 
                    from postion where userid=@userid ";

            dbhelper.AddParameter("@userid", req.userid);
            if (req.hisroomid != 0)
            {

                sql += " and hisid=@hisid";
                dbhelper.AddParameter("@hisid", req.hisroomid);
            }

            if (req.startTime != null)
            {
                sql += " and recordtime>@recordtime";
                dbhelper.AddParameter("@recordtime", req.startTime);
            }
            if (req.Size > 0)
            {
                sql += " limit 0," + req.Size;
            }

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<PostionInfo>(sql);

        }
    }
}