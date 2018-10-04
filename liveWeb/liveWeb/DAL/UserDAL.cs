using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class UserDAL
    {
        public userEntiy getUser(DbHelper dbHelper, string name)
        {
            string sql = @"select id,name,password,usertype,lastlogintime,status,Flag,insystem,roomid,
                        longitude,latitude from user where name=@name";

            dbHelper.AddParameter("@name", name);

            DbEntity dbEntity = new DbEntity(dbHelper);
            return dbEntity.SelectFirst<userEntiy>(sql);

        }
        public userEntiy getUser(DbHelper dbHelper, int userid)
        {
            string sql = @"select id,name,password,usertype,lastlogintime,status,Flag,insystem,roomid,
                        longitude,latitude,freeStartTime from user where id=@userid";

            dbHelper.AddParameter("@userid", userid);

            DbEntity dbEntity = new DbEntity(dbHelper);
            return dbEntity.SelectFirst<userEntiy>(sql);
        }



        public IList<userEntiy> getUserList(DbHelper dbHelper, UserReqEntity req)
        {
            string sql = @"select id,name,password,usertype,lastlogintime,status,Flag,insystem,roomid,
                        longitude,latitude,freeStartTime from user where 1=1";
            if (!string.IsNullOrEmpty(req.name))
            {
                sql += " and name like @name";
                dbHelper.AddLikeParameter("@name", req.name);
            }
            if (req.userType > 0)
            {
                sql += " and usertype = @usertype";
                dbHelper.AddParameter("@usertype", req.userType);
            }
            if (req.userStatus > -1)
            {
                sql += " and status =@status";
                dbHelper.AddParameter("@status", req.userStatus);
            }

            if (req.sorttype == 1)
            {
                sql += " order by freeStartTime desc";
            }
            else if (req.sorttype == 2)
            {
                sql += " order by name";
            }
            else
            {
                sql += " order by status desc,freeStartTime";
            }
            

            DbEntity dbEntity = new DbEntity(dbHelper);
            var result =dbEntity.Select<userEntiy>(sql);
            return result;


        }

        public void updateRoom(DbHelper dbHelper,ChangeUserRoom room)
        {
            string sql = @" update user set Roomid=@roomid where id=@userid";

            dbHelper.AddParameter("@roomid", room.roomid);
            dbHelper.AddParameter("@userid", room.userid);

            dbHelper.ExecuteNonQuerySQL(sql);

            int hisid = HistroyRoomDL.GetHisId(dbHelper, room.roomid);
            try
            {

                UserJoinRoomHisTable his = new UserJoinRoomHisTable();
                his.hisid = hisid;
                his.roomid = room.roomid;
                his.userid = room.userid;
                his.jointime = DateTime.Now;
                DbEntity dbEntity = new DbEntity(dbHelper);
                dbEntity.Insert(his);

            }
            catch (Exception ex)
            {
                //重复插入会报错，忽略掉这里
            }


        }

        public void updateUser(DbHelper dbHelper, ChangeUserEntity entity)
        {
            DbEntity dbEntity = new DbEntity(dbHelper);

            var OldUser = getUser(dbHelper, entity.id);


            ChangeUserTable table = new ChangeUserTable();
            table.id = entity.id;
            table.insystem = entity.insystem;
            table.status = entity.status;
            table.Flag = entity.Flag;
            table.roomid = entity.roomid;
            //如果等于9那么更新日期重新排序
            if (entity.status == 9)
            {
                table.freeStartTime = DateTime.Now;
            }
            else
            {
                table.freeStartTime = OldUser.freeStartTime;
            }

            dbEntity.Update(table, "id");

            //如果roomid有变化那么将其加入历史中。
            if (String.IsNullOrEmpty(entity.roomid)){
                return;
            }
            if (entity.roomid.Equals(OldUser.roomid)){
                return;
            }


            try
            {
                int hisid = HistroyRoomDL.GetHisId(dbHelper, entity.roomid);
                UserJoinRoomHisTable his = new UserJoinRoomHisTable();
                his.hisid = hisid;
                his.roomid = entity.roomid;
                his.userid = entity.id;
                his.jointime = DateTime.Now;
                dbEntity.Insert(his);

            }
            catch (Exception ex)
            {
                //重复插入会报错，忽略掉这里
            }


        }
    }
}