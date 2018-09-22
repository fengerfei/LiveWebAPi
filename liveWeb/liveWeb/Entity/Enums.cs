using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    public class Enums
    {
    }

    public enum ResponeCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("success")]
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("error")]
        Error = -1,
        /// <summary>
        /// Token无效
        /// </summary>

        [Description("token is invalid")]
        TokenInvalid = -2,
        /// <summary>
        /// 参数无效
        /// </summary>
        [Description("invalid argument")]
        ParamsInvalid = -3,
        /// <summary>
        /// 参数无效
        /// </summary>
        [Description("sign is invalid")]
        SignInvalid = -4
    }
}