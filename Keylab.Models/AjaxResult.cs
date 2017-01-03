using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {
    /**
    * author 曾祥银
    * time 2016-07-13
    */
    /// <summary>
    /// AjaxResult 返回数据格式
    /// </summary>
    public class AjaxResult {
        /// <summary>
        /// 通信状态码
        /// </summary>
        public Status status {
            get;
            set;
        }
        /// <summary>
        /// 通信数据
        /// </summary>
        public object message {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public AjaxResult() {
            this.status = Status.unknown;
            this.message = "unknown";
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="status">枚举状态</param>
        /// <param name="message">返回信息</param>
        public AjaxResult(Status status, object message) {
            this.status = status;
            this.message = message;
        }
        /// <summary>
        /// 转换成Json字符串 不包含时间格式
        /// </summary>
        /// <returns>string</returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// 转换成Json字符串 包含时间格式:yyyy-MM-dd
        /// </summary>
        /// <returns>string</returns>
        public string ToJson2() {
            IsoDateTimeConverter format = new IsoDateTimeConverter();
            format.DateTimeFormat = "yyyy-MM-dd";
            return JsonConvert.SerializeObject(this, format);
        }
    }

    /// <summary>
    /// AjaxResult 状态枚举
    /// </summary>
    public enum Status {
        /// <summary>
        ///失败
        /// </summary>
        failed = 0,

        /// <summary>
        /// 正常:预期结果,成功
        /// </summary>
        success = 1,

        /// <summary>
        /// 请求数据非法
        /// </summary>
        isnull = 220,

        /// <summary>
        /// 重复:数据验证重复了
        /// </summary>
        repeat = 220,
       
        /// <summary>
        /// 没有数据:未查询到数据
        /// </summary>
        nodata = 230,

        /// <summary>
        /// 超时:登陆超时
        /// </summary>
        timeout = -1,

        /// <summary>
        /// 权限不足:拒绝
        /// </summary>
        denied = 260,


        /// <summary>
        /// 未知,初始化
        /// </summary>
        unknown = -1,

        /// <summary>
        /// 异常:失败
        /// </summary>
        except = -1,

    }
}
