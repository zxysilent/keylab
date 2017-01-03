using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {
    [Serializable]
    public class Subclass {
        public int id { get; set; }
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
    }
}
