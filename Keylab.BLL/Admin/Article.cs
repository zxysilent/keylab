using Keylab.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keylab.Models;
namespace Keylab.BLL.Admin {
    public class Article {

        /// <summary>
        /// DataAccessLayer
        /// </summary>
        private DataAccessLayer dal = new DataAccessLayer();

        #region 基本类别
        /// <summary>
        /// 获取父类
        /// </summary>
        public IEnumerable<Supclass> Supclass() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from supclass");
            return dal.QuerySet<Supclass>(sb.ToString());
        }

        /// <summary>
        /// 获取子类
        /// </summary>
        public IEnumerable<Subclass> Subclass() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from subclass");
            return dal.QuerySet<Subclass>(sb.ToString());
        }

        /// <summary>
        /// 通过父类获取子类
        /// </summary>
        public IEnumerable<Subclass> SubBySup(string sup) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from subclass where supnum=@sup");
            var param = new { sup = sup };
            return dal.QuerySet<Subclass>(sb.ToString(), param);
        }
        #endregion

        public IEnumerable<Articles> List(int pi, int ps, string sup, string sub, string key) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from article where status=1 ");
            if (!String.IsNullOrEmpty(sup)) {
                sb.Append(" and supnum=@sup ");
            }
            if (!String.IsNullOrEmpty(sub)) {
                sb.Append(" and subnum=@sub ");
            }
            if (!String.IsNullOrEmpty(key)) {
                sb.Append(" and title like concat('%',@key,'%') ");
            }
            sb.Append(" ORDER BY utime  DESC limit @begin, @end");
            var param = new { begin = (pi - 1) * ps, end = ps, sup = sup, sub = sub, key = key };
            return dal.QuerySet<Articles>(sb.ToString(), param);
        }
        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <returns></returns>
        public int Count(string sup, string sub, string key) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from article where status=1 ");
            if (!String.IsNullOrEmpty(sup)) {
                sb.Append(" and supnum=@sup ");
            }
            if (!String.IsNullOrEmpty(sub)) {
                sb.Append(" and subnum=@sub ");
            }
            if (!String.IsNullOrEmpty(key)) {
                sb.Append(" and title like concat('%',@key,'%') ");
            }
            var param = new { sup = sup, sub = sub, key = key };
            return dal.QueryScalar<int>(sb.ToString(), param);
        }

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
        /// 添加信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Articles model) {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into article (author, type, origin, title, content, url, hit, supnum, supname, subnum, subname, `order`, userid, utime, status) values(@author, @type, @origin, @title, @content, @url, @hit, @supnum, @supname, @subnum, @subname, @order, @userid, @utime, @status)");
            return dal.ExecBool(sb.ToString(), model);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Edit(Articles model) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update article set author=@author, type=@type, origin=@origin, title=@title, content=@content, url=@url, hit=@hit, supnum=@supnum, supname=@supname, subnum=@subnum, subname=@subname, `order`=@order, userid=@userid, utime=@utime, status=1 where id=@id");
            return dal.ExecBool(sb.ToString(), model);
        }



        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Del(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update article set status=0 where id=@id");
            var param = new { id = id };
            return dal.ExecBool(sb.ToString(), param);
        }
    }
}
