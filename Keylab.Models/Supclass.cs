using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Models {
    [Serializable]
    public class Supclass {
        public int id { get; set; }
        /// <summary>
        /// 父类编码
        /// </summary>
        public string supnum { get; set; }
        /// <summary>
        /// 父类名称
        /// </summary>
        public string supname{ get; set; }
    }
}
