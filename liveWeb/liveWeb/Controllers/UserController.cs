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
    public class UserController : BaseApiController
    {
        // GET api/<controller>
        public ResponseEntity<IList<userEntiy>> Get([FromUri]UserReqEntity req)
        {

            IList<userEntiy> result = new List<userEntiy>();
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new UserDAL();
                result = dal.getUserList(dbhelper, req);

                return ResponseHelper<IList<userEntiy>>.Success(result);

            }
        }
    }
}