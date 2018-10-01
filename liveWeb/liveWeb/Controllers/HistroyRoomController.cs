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
    public class HistroyRoomController : BaseApiController
    {
        public ResponseEntity<IList<RoomHistroyEntity>> GetHistroyRoom([FromUri]RoomHistroyReqEntity req)
        {

            IList<RoomHistroyEntity> result = new List<RoomHistroyEntity>();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new HistroyRoomDL();
                result = dal.GetHistroyRoom(dbhelper, req);


                if (result == null || result.Count == 0)
                {
                    return ResponseHelper<IList<RoomHistroyEntity>>.Error(result);
                }
                else
                {
                    return ResponseHelper<IList<RoomHistroyEntity>>.Success(result);
                }
                

            }
        }
        [Route("api/HistroyRoom/Msg")]
        public ResponseEntity<IList<MsgInfoEntity>> GetHistroyRoomMsg([FromUri]RoomHistroyMsgListQueryParams req)
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

                return ResponseHelper<IList<MsgInfoEntity>>.Success(result);
            }
        }

        public ResponseEntity<IList<PostionInfo>> Get([FromUri]HistroyPostionReqEntity req)
        {
            IList<PostionInfo> result = new List<PostionInfo>();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new PostionDAL();

                result = dal.GetHisPostion(dbhelper, req);

                if (result.Count == 0)
                {
                    return ResponseHelper<IList<PostionInfo>>.Error(result, "未查到数据");
                }

                return ResponseHelper<IList<PostionInfo>>.Success(result);

            }
        }





    }
}