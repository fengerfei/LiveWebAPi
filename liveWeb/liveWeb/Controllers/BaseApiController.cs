using Carpa.Web.Script;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace liveWeb.Controllers
{
    /// <summary>
    /// Api 基类
    /// </summary>
    public class BaseApiController : ApiController
    {
        protected DbHelper CreateMobileDbHelper()
        {
            string constr = ConfigurationManager.ConnectionStrings["mobile_connstr"].ToString();
            return new DbHelper(constr);
        }
    }
}