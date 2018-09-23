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

        [Route("api/User/ChangeUserRoom")]
        public ResponseEntity<userEntiy> PostChangeUserRoom([FromBody] ChangeUserRoom req)
        {
            //userEntiy> result = new List<userEntiy>();
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new UserDAL();

                dal.updateRoom(dbhelper, req);

                userEntiy entiy = new userEntiy();

                entiy = dal.getUser(dbhelper, req.userid);

                return ResponseHelper<userEntiy>.Success(entiy);

            }
        }

        public ResponseEntity<userEntiy> Post([FromBody] ChangeUserEntity req)
        {
            //userEntiy> result = new List<userEntiy>();
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new UserDAL();

                dal.updateUser(dbhelper, req);

                userEntiy entiy = new userEntiy();

                entiy = dal.getUser(dbhelper, req.id);

                return ResponseHelper<userEntiy>.Success(entiy);

            }
        }


    }
}