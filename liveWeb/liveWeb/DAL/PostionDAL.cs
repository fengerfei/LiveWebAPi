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

            DbEntity entity = new DbEntity(dbhelper);

            
            PostionTable table = new PostionTable();

            table.UserId = req.UserId;
            table.latitude = req.latitude;
            table.longitude = req.longitude;
            table.RoomId = req.RoomId;
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

            DbEntity entity = new DbEntity(dbhelper);

            return entity.Select<PostionInfo>(sql);

        }
    }
}