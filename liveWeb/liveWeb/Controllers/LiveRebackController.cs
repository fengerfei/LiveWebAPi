using Carpa.Web.Ajax;
using Carpa.Web.Ajax.Serialization;
using liveWeb.DAL;
using liveWeb.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace liveWeb.Controllers
{
    public class LiveRebackController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> GetLiveReback([FromUri]LiveRebackEntity req)
        {
            using (var dbhelper = CreateMobileDbHelper())
            {

                var dal = new LiveRoomDAL();
                dal.LiveReback(dbhelper, req,"GET");
            }


            return new string[] { "OK", "" };
        }

        // POST api/<controller>
        [HttpPost]
        public void PostLiveReback([FromBody]LiveRebackEntity req)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
             HttpRequestBase request = context.Request;//定义传统request对象

             if (req == null)
             {
                 req = new LiveRebackEntity();
                 req.publishdomain =request.QueryString["publishdomain"];
                 req.streamid =request.QueryString["streamid"];
                 req.filesize = Int32.Parse(request.QueryString["filesize"]);
                 req.spacename =request.QueryString["spacename"];
                 req.duration =Int32.Parse(request.QueryString["duration"]);
                 req.url =request.QueryString["url"];
             }


            //json = publishdomain+"|"+streamid + "|" + filename + "|" + filesize + "|" + spacename + "|" + duration + "|" + url;
            using (var dbhelper = CreateMobileDbHelper())
            {
                var dal = new LiveRoomDAL();
                try
                {
                    dal.LiveReback(dbhelper, req, "POST");
                    dal.saveLog(dbhelper, "OK"+req.streamid);
                }
                catch (Exception ex)
                {
                    //重复插入会报错，忽略掉这里
                    //Carpa.Logging.Log.Debug("错误:" + ex.Message);

                    dal.saveLog(dbhelper, "ERR:" + ex.Message);
                }



            }
            
        }

    }
}