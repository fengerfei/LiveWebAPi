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
    }
}