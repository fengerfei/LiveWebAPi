using liveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liveWeb.Comm
{
    /// <summary>
    /// 返回辅助
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseHelper<T>
    {
        /// <summary>
        /// 构建成功返回数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static ResponseEntity<T> Success(T data, string message = "success")
        {
            return new ResponseEntity<T>()
            {
                Code = ResponeCode.Success,
                Message = message,
                Data = data
            };
        }
        /// <summary>
        /// 构建成功返回数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static ResponseEntity<T> Error(T data, string message = "error")
        {
            return new ResponseEntity<T>()
            {
                Code = ResponeCode.Error,
                Message = message,
                Data = data
            };
        }
        /// <summary>
        /// 分页处理返回数据
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="recordCount">数据总数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public static ResponseEntity<PagerEntity<T>> SuccessPager(IList<T> data, int recordCount, int pageSize, int pageIndex)
        {
            return new ResponseEntity<PagerEntity<T>>()
            {
                Code = ResponeCode.Success,
                Message = "success",
                Data = new PagerEntity<T>()
                {
                    RecordData = data,
                    RecordCount = recordCount,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    HasMore = (pageSize * pageIndex) < recordCount
                }
            };
        }

        public static ResponseEntity<PagerExtraEntity<T>> SuccessExtraPager(IList<T> data, object extraData, int recordCount, int pageSize, int pageIndex)
        {
            return new ResponseEntity<PagerExtraEntity<T>>()
            {
                Code = ResponeCode.Success,
                Message = "success",
                Data = new PagerExtraEntity<T>()
                {
                    RecordData = data,
                    RecordCount = recordCount,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    HasMore = (pageSize * pageIndex) < recordCount,
                    ExtraData = extraData
                }
            };
        }

        public static ResponseEntity<PagerDateEntity<T>> SuccessPager(IList<T> data, DateTime dateTime, int pageSize)
        {
            return new ResponseEntity<PagerDateEntity<T>>()
            {
                Code = ResponeCode.Success,
                Message = "success",
                Data = new PagerDateEntity<T>()
                {
                    RecordData = data,
                    recordTime = dateTime,
                    PageSize = pageSize,
                    HasMore = pageSize == data.Count
                }
            };
        }
    }

    /// <summary>
    /// 简单类型返回
    /// </summary>
    public class ResponseHelper
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static BaseResponseEntity Success(string message = "success")
        {
            return new BaseResponseEntity()
            {
                Code = ResponeCode.Success,
                Message = message
            };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">失败消息</param>
        /// <param name="code">异常code</param>
        /// <returns></returns>
        public static BaseResponseEntity Error(string message, ResponeCode code = ResponeCode.Error)
        {
            return new BaseResponseEntity()
            {
                Code = code,
                Message = message
            };
        }
    }
}