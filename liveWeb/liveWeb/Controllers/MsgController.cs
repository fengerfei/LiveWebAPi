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
    public class MsgController : BaseApiController
    {
        // GET api/<controller>
        public ResponseEntity<PagerDateEntity<MsgInfoEntity>> Get([FromUri]MsgListPagerQueryParams req)
        {
            int pageSize = 30;

            IList<MsgInfoEntity> result = new List<MsgInfoEntity>();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new MsgDAL();
                if (req.PageSize == 0)
                {
                    req.PageSize = pageSize;
                }

                result = dal.GetMsgInfo(dbhelper, req);


                return ResponseHelper<MsgInfoEntity>.SuccessPager(result, req.TheTimeBefore, req.PageSize);
            }
        }


        // POST api/<controller>
        public ResponseEntity<string> Post([FromBody]MsgInfoEntity req)
        {
            using (var dbhelper = CreateMobileDbHelper())
            {

                var dal = new MsgDAL();

                dal.InsertMsg(dbhelper, req);

                return ResponseHelper<string>.Success("更新消息成功");

            }
        }

    }
}