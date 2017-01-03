using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {
    [Serializable]
    public class Articles {
        public int id { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public bool type { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string origin { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int hit { get; set; }

        /// <summary>
        /// 父类编码
        /// </summary>
        public string supnum { get; set; }

        /// <summary>
        /// 父类名称
        /// </summary>
        public string supname { get; set; }

        /// <summary>
        /// 子类编码
        /// </summary>
        public string subnum { get; set; }

        /// <summary>
        /// 子类名称
        /// </summary>
        public string subname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }

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
