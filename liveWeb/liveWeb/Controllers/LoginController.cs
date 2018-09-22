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
    public class LoginController : BaseApiController
    {
        // POST api/<controller>
        public ResponseEntity<userEntiy> Post([FromBody]LoginReqEntiy req)
        {

            userEntiy entiy = new userEntiy();
            if (string.IsNullOrEmpty(req.name))
            {
                return ResponseHelper<userEntiy>.Error(entiy,"没有输入名称");
            }

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new UserDAL();
                entiy = dal.getUser(dbhelper, req.name);

                if (entiy.password != req.password)
                {
                    return ResponseHelper<userEntiy>.Error(entiy, "密码错误");
                }
                return ResponseHelper<userEntiy>.Success(entiy);

            }
        }


    }
}