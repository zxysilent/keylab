using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL.Admin {
    public class Slider {

        /// <summary>
        /// DataAccessLayer
        /// </summary>
        private DataAccessLayer dal = new DataAccessLayer();

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public IEnumerable<Sliders> List(int pi, int ps) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from slider where status=1 order by id desc limit @begin, @end");
            var param = new { begin = (pi - 1) * ps, end = ps };
            return dal.QuerySet<Sliders>(sb.ToString(), param);
        }

        /// <summary>
        /// 查询某一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Sliders> One(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from slider where id=@id");
            var param = new { id = id };
            return dal.QuerySet<Sliders>(sb.ToString(), param);
        }

        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <returns></returns>
        public int Count() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from slider where status=1");
            return dal.QueryScalar<int>(sb.ToString());
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Add(Sliders sld) {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into slider (title,url,link,skip,userid,utime,status) values(@title,@url,@link,@skip,@userid,@utime,@status)");
            return dal.ExecBool(sb.ToString(), sld);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Edit(Sliders sld) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update slider set title=@title,url=@url,link=@link,skip=@skip,userid=@userid,utime=@utime where id=@id");
            return dal.ExecBool(sb.ToString(), sld);
        }

      

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Del(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update slider set status=0 where id=@id");
            var param = new { id = id };
            return dal.ExecBool(sb.ToString(), param);
        }
    }
}
