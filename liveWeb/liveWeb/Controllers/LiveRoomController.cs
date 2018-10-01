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
    public class LiveRoomController : BaseApiController
    {
        public ResponseEntity<IList<LiveRoomEntiy>> Get([FromUri]LiveRoomReqEntity req)
        {

            IList<LiveRoomEntiy> result = new List<LiveRoomEntiy>();
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                result = dal.getRoomList(dbhelper, req);

                return ResponseHelper<IList<LiveRoomEntiy>>.Success(result);

            }
        }

        [Route("api/LiveRoom/MyRoom")]
        public ResponseEntity<LiveRoomEntiy> GetMyRoom([FromUri]MyRoomReqEntity req)
        {



            LiveRoomEntiy result = new LiveRoomEntiy();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                result = dal.GetMyRoom(dbhelper, req);



                return ResponseHelper<LiveRoomEntiy>.Success(result);

            }
        }

        [Route("api/LiveRoom/UpdateRoom")]
        public ResponseEntity<LiveRoomEntiy> PostUpdateRoom([FromBody]liveRoomUpdateEntity req)
        {
            LiveRoomEntiy result = new LiveRoomEntiy();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                var userDal = new UserDAL();
                dal.UpdateRoom(dbhelper, req);


                MyRoomReqEntity myreq = new MyRoomReqEntity();
                if (req.liveid != 0)
                {
                    myreq.userid = req.liveid;                    
                }
                else
                {
                    myreq.userid = req.mainid;
                }

                result = dal.GetMyRoom(dbhelper, myreq);
           }


           return ResponseHelper<LiveRoomEntiy>.Success(result);

        }

        public ResponseEntity<LiveRoomEntiy> PostCreateRoom([FromBody]liveRoomCreateEntity req)
        {
            LiveRoomEntiy result = new LiveRoomEntiy();

            if (req.mainid == 0 && req.liveid == 0)
            {
                return ResponseHelper<LiveRoomEntiy>.Error(result, "传入人员不能为空!");
            }

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                var userDal = new UserDAL();
                dal.CreateRoom(dbhelper, req);


                MyRoomReqEntity myreq = new MyRoomReqEntity();
                if (req.mainid != 0)
                {
                    myreq.userid = req.mainid;
                    myreq.usertype = 1;
                }
                else
                {
                    myreq.userid = req.liveid;
                    myreq.usertype = 2;
                }

                result = dal.GetMyRoom(dbhelper, myreq);

                if (result != null)
                {
                    if (req.mainid != 0)
                    {
                        ChangeUserRoom userroom = new ChangeUserRoom();
                        userroom.userid = req.mainid;
                        userroom.roomid = result.roomid;
                        userDal.updateRoom(dbhelper, userroom);
                    }
                    else if (req.liveid != 0)
                    {
                        ChangeUserRoom userroom = new ChangeUserRoom();
                        userroom.userid = req.liveid;
                        userroom.roomid = result.roomid;
                        userDal.updateRoom(dbhelper, userroom);
                    }

                }


                return ResponseHelper<LiveRoomEntiy>.Success(result);

            }
        }

        [Route("api/LiveRoom/RoomNumber")]
        public ResponseEntity<IList<userEntiy>> GetLiveRoomNumber([FromUri]RoomreqEntity req)
        {
            IList<userEntiy> result = new List<userEntiy>();
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                result = dal.getRoomNumber(dbhelper, req);

                return ResponseHelper<IList<userEntiy>>.Success(result);

            }
        }

        [Route("api/LiveRoom/ChangeRoomMainId")]
        public ResponseEntity<LiveRoomEntiy> ChangeRoomMainId([FromBody]ChangeUserRoom req)
        {
            LiveRoomEntiy result = new LiveRoomEntiy();

            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                var userDal = new UserDAL();
                dal.changeRoommainid(dbhelper, req.roomid,req.userid);

                MyRoomReqEntity myreq = new MyRoomReqEntity();

                myreq.userid = req.userid;
                myreq.usertype = 2;

                result = dal.GetMyRoom(dbhelper, myreq);
            }


            return ResponseHelper<LiveRoomEntiy>.Success(result);

        }

        [Route("api/LiveRoom/CloseRoom")]
        public ResponseEntity<LiveRoomEntiy> GetCloseRoom([FromUri]ChangeUserRoom req)
        {
            //关闭房间同时清空视频相关信息。
            LiveRoomEntiy result = new LiveRoomEntiy();

            using (var dbhelper = CreateMobileDbHelper())
            {

                var dal = new LiveRoomDAL();
                var userDal = new UserDAL();
                dal.CloseRoom(dbhelper, req);
                result = dal.GetRoomById(dbhelper, req.roomid);
            }

            return ResponseHelper<LiveRoomEntiy>.Success(result);

        }


    }
}

