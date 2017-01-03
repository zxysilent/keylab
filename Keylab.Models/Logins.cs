using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Keylab.Models {
    [Serializable]
    public class Logins {
        public int id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pass { get; set; }
        /// <summary>
        /// 超级管理员
        /// </summary>
        public bool super { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int error { get; set; }
        /// <summary>
        /// 取消限定时间
        /// </summary>
        public DateTime etime { get; set; }
        /// <summary>
        /// 登陆次数
        /// </summary>
        public int login { get; set; }
    }
}