using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {
    [Serializable]
    public class Sliders {
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 是否可跳转
        /// </summary>
        public bool skip { get; set; }
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
