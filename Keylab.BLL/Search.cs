using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL {
    public class Search {
        private DataAccessLayer dal = new DataAccessLayer();

        public IEnumerable<Articles> List(int pi, int ps, string key) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from article where status=1 and type=1 ");
            if (!String.IsNullOrEmpty(key)) {
                sb.Append(" and title like concat('%',@key,'%') ");
            }
            sb.Append(" ORDER BY `order` desc, utime  DESC limit @begin, @end");
            var param = new { begin = (pi - 1) * ps, end = ps, key = key };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <returns></returns>
        public int Count(string key) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from article where status=1 and type=1 ");
            if (!String.IsNullOrEmpty(key)) {
                sb.Append(" and title like concat('%',@key,'%') ");
            }
            var param = new { key = key };
            return dal.QueryScalar<int>(sb.ToString(), param);
        }
    }
}
