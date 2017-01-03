using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL.Admin {
    public class User {

        /// <summary>
        /// DataAccessLayer
        /// </summary>
        private DataAccessLayer dal = new DataAccessLayer();

        /// <summary>
        /// 分页查询数据
        /// </summary>
        public IEnumerable<Users> List(int pi, int ps) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from user where status=1 order by id asc limit @begin, @end");
            var param = new { begin = (pi - 1) * ps, end = ps };
            return dal.QuerySet<Users>(sb.ToString(), param);
        }

        /// <summary>
        /// 查询某一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Users> One(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from user where id=@id");
            var param = new { id = id };
            return dal.QuerySet<Users>(sb.ToString(), param);
        }

        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <returns></returns>
        public int Count() {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from user where status=1");
            return dal.QueryScalar<int>(sb.ToString());
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Users model) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update user set name=@name,phone=@phone,email=@email,userid=@userid,utime=@utime where id=@id and super=0");
            return dal.ExecBool(sb.ToString(), model);
        }

        /// <summary>
        /// 修改 密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditPass(int id,string pass) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update user set pass=@pass where id=@id");
            var param = new { id = id, pass = pass };
            return dal.ExecBool(sb.ToString(), param);
        }

        /// <summary>
        /// 查询账号的密码
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string GetPass(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select pass from user where id=@id");
            var param = new { id = id };
            return dal.QueryScalar<string>(sb.ToString(), param);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ResetPass(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update user set pass='e10adc3949ba59abbe56e057f20f883e' where id=@id");
            var param = new { id = id };
            return dal.ExecBool(sb.ToString(), param);
        }

        /// <summary>
        /// 查询是否存在当前账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RepeatNum(string num) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(id) from user where num=@num");
            var param = new { num = num };
            return dal.QueryScalar<int>(sb.ToString(), param) > 0;
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Users model) {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into user (num,name,pass,phone,email,super,error,etime,login,userid,utime,status) values(@num,@name,@pass,@phone,@email,@super,@error,@etime,@login,@userid,@utime,@status)");
            return dal.ExecBool(sb.ToString(), model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        public bool Del(int id) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update user set status=0 where  id=@id and super=0");
            var param = new { id = id };
            return dal.ExecBool(sb.ToString(), param);
        }
    }
}
