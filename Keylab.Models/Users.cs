using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {

    [Serializable]
    public class Users {
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
        /// 电话号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
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
        /// <summary>
        /// 修改用户id
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime utime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
    }
}
