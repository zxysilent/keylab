using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL {
   public class Lists {
       private DAL.DataAccessLayer dal = new DataAccessLayer();
       public IEnumerable<Subclass> ClsInfo(string sup, string sub) {
           StringBuilder sb = new StringBuilder();
           sb.Append("select * from subclass where id>0  ");
           if (!String.IsNullOrEmpty(sup)) {
               sb.Append(" and supnum=@sup ");
           }
           if (!String.IsNullOrEmpty(sub)) {
               sb.Append(" and subnum=@sub ");
           }
           var param = new { sup = sup, sub = sub };
           return dal.QuerySet<Subclass>(sb.ToString(), param);
       }
       public IEnumerable<Articles> List(int pi, int ps, string sup, string sub) {
           StringBuilder sb = new StringBuilder();
           sb.Append("select * from article where status=1 ");
           if (!String.IsNullOrEmpty(sup)) {
               sb.Append(" and supnum=@sup ");
           }
           if (!String.IsNullOrEmpty(sub)) {
               sb.Append(" and subnum=@sub ");
           }
           sb.Append(" ORDER BY utime  DESC limit @begin, @end");
           var param = new { begin = (pi - 1) * ps, end = ps, sup = sup, sub = sub};
           return dal.QuerySet<Articles>(sb.ToString(), param);
       }
       /// <summary>
       /// 查询分页总数
       /// </summary>
       /// <returns></returns>
       public int Count(string sup, string sub) {
           StringBuilder sb = new StringBuilder();
           sb.Append("select count(id) from article where status=1 ");
           if (!String.IsNullOrEmpty(sup)) {
               sb.Append(" and supnum=@sup ");
           }
           if (!String.IsNullOrEmpty(sub)) {
               sb.Append(" and subnum=@sub ");
           }
           var param = new { sup = sup, sub = sub };
           return dal.QueryScalar<int>(sb.ToString(), param);
       }
    }
}
