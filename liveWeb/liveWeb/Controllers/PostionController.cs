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
    public class PostionController : BaseApiController
    {
        // GET api/<controller>
        public ResponseEntity<IList<PostionInfo>> Get([FromUri]PostionReqEntity req)
        {
            IList<PostionInfo> result = new List<PostionInfo>();
 
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new PostionDAL();

                result = dal.GetPostion(dbhelper, req);

                if (result.Count == 0)
                {
                    return ResponseHelper<IList<PostionInfo>>.Error(result, "未查到数据");
                }

                return ResponseHelper<IList<PostionInfo>>.Success(result);

            }
        }

        // POST api/<controller>
        public ResponseEntity<string> Post([FromBody]PostionEntity req)
        {
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new PostionDAL();
                var userdal = new UserDAL();

                dal.updatePostion(dbhelper, req);

                return ResponseHelper<string>.Success("更新位置成功");

            }

        }

    }
}