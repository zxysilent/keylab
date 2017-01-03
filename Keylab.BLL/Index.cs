using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL {
    public class Index {
        private DataAccessLayer dal = new DataAccessLayer();
        //获取slider
        public IEnumerable<Sliders> Silder() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from slider where status=1 order by id desc");
            return dal.QuerySet<Sliders>(sb.ToString());
        }
        public IEnumerable<Articles> Article(string sup, string sub, int top) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from article where status=1 ");
            if (!String.IsNullOrEmpty(sup)) {
                sb.Append(" and supnum=@sup ");
            }
            if (!String.IsNullOrEmpty(sub)) {
                sb.Append(" and subnum=@sub ");
            }
            sb.Append(" order by `order` desc,utime desc limit @top");
            var param = new { sup = sup, sub = sub, top = top };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        public IEnumerable<Articles> ArticleImg(string sup, string sub, int top) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from article where status=1 and LENGTH(url)>0 ");
            if (!String.IsNullOrEmpty(sup)) {
                sb.Append(" and supnum=@sup ");
            }
            if (!String.IsNullOrEmpty(sub)) {
                sb.Append(" and subnum=@sub ");
            }
            sb.Append(" order by `order` desc,utime desc limit @top");
            var param = new { sup = sup, sub = sub, top = top };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
    }
}
