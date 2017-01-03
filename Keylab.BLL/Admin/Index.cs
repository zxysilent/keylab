using Keylab.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL.Admin {
    public class Index {
        private DataAccessLayer dal = new DataAccessLayer();
        /// <summary>
        /// 查询轮播图总数
        /// </summary>
        /// <returns></returns>
        public int SliderCount() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from slider where status=1");
            return dal.QueryScalar<int>(sb.ToString());
        }
        /// <summary>
        /// 查询文章总数
        /// </summary>
        /// <returns></returns>
        public int ArticleCount() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from article where status=1");
            return dal.QueryScalar<int>(sb.ToString());
        }
        /// <summary>
        /// 查询管理员总数
        /// </summary>
        /// <returns></returns>
        public int UserCount() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from user where status=1");
            return dal.QueryScalar<int>(sb.ToString());
        }
    }
}
