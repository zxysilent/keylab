using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL {
    public class Detail {
        private DataAccessLayer dal = new DataAccessLayer();
        /// <summary>
        /// 查询某一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Articles> One(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from article where id=@id");
            var param = new { id = id };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        /// <summary>
        /// 查询上一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Articles> PrevOne(string sub, DateTime tim, int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM article where  subnum=@sub and id<>@id and utime<=@tim  ORDER BY utime DESC limit 1");
            var param = new { sub = sub, tim = tim, id = id };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        /// <summary>
        /// 查询下一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Articles> NextOne(string sub,DateTime tim,int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM article where  subnum=@sub and id<>@id and utime>=@tim  ORDER BY utime DESC limit 1");
            var param = new { sub = sub,tim=tim,id=id };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        /// <summary>
        /// 查询下一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Hit(Articles model) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update article set hit=@hit where id=@id");
            return dal.ExecBool(sb.ToString(), model);
        }
    }
}
