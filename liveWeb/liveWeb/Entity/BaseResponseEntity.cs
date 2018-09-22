using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Entity
{
    /// <summary>
    /// 返回基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseEntity<T> : BaseResponseEntity
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }
    }
    public class BaseResponseEntity
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public ResponeCode Code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Message { get; set; }
    }
}