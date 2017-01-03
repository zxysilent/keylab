using Keylab.DAL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.BLL.Admin {
    public class Login {

        /// <summary>
        /// DataAccessLayer
        /// </summary>
        private DataAccessLayer dal = new DataAccessLayer();

        /// <summary>
        /// 用户登录-是否存在此账号信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Logins> CheckNum(string num) {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from user where num=@num");
            var param = new { num = num };
            return dal.QuerySet<Logins>(sb.ToString(), param);
        }

        /// <summary>
        /// 更新用户登陆信息
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool ModifyLogin(Logins login) {
            StringBuilder sb = new StringBuilder();
            sb.Append("update user set login=@login,token=@token,error=@error,etime=@etime where id=@id ");
            return dal.ExecBool(sb.ToString(), login);
        }
    }
}
