using liveWeb.Comm;
using liveWeb.DAL;
using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace liveWeb.Controllers
{
    public class VersionController : BaseApiController
    {
        // GET api/<controller>
        public ResponseEntity<VersionEntity> Get()
        {

            VersionEntity entiy = null;
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new VersionDL();
                entiy = dal.GetVersion(dbhelper);

            }
            return ResponseHelper<VersionEntity>.Success(entiy);

        } 
    }
}