using Carpa.Web.Entity;
using Carpa.Web.Script;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.DAL
{
    public class VersionDL
    {
        public VersionEntity GetVersion(DbHelper dbhelper)
        {
            string sql = @"select  versionnumber,updateUrl from version";


            DbEntity entity = new DbEntity(dbhelper);

            return entity.SelectFirst<VersionEntity>(sql);

        }

        public bool UpdateData(DbHelper dbhelper)
        {
            string sql = @"DELETE FROM userjoinroomhis";
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"DELETE FROM roomhistory";
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"DELETE FROM msg";
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"UPDATE liveroom SET memo='',flag='',mainid=0,isOpen=0,liveroomid=0,isclose=1,livestatus=0,chatstatus=0";
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"UPDATE `user` SET STATUS=0,flag=0,longitude=0.00,latitude=0.00 ";
            dbhelper.ExecuteNonQuerySQL(sql);

            sql = @"UPDATE `user` SET Roomid='' WHERE id>=6 ";
            dbhelper.ExecuteNonQuerySQL(sql);
            

            return true;
        }
    }
}