using Keylab.BLL.Admin;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Areas.Admin.Controllers {
    public class LoginController : Controller {
        /// <summary>
        /// json 返回格式
        /// </summary>
        private AjaxResult ajaxResult = new AjaxResult();
        /// <summary>
        /// bll slider
        /// </summary>
        private Login login = new Login();


        public ActionResult Index() {
            return View();
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public ActionResult Login(string num, string pass) {
            //必须输入账号和密码
            if (String.IsNullOrEmpty(num) || String.IsNullOrEmpty(pass)) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入完整的数据";
                return Content(this.ajaxResult.ToJson());
            }
            //通过账号查询用户信息
            var resList = login.CheckNum(num).ToList<Logins>();
            if (resList.Count != 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "用户信息不存在";
                return Content(this.ajaxResult.ToJson());
            }
            //获取用户信息 logins
            var info = resList[0];
            TimeSpan ts = info.etime - DateTime.Now;
            if (ts.TotalMinutes > 0 && info.error == 0) {//处于锁定时间内
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "账号锁定中请(" + Math.Ceiling(ts.TotalMinutes) + ")分钟后重试";
                return Content(this.ajaxResult.ToJson());
            } else if (ts.TotalMinutes <-15) {//若不是锁定状态并且上次登陆时间大于约定的时间则重置错误次数
                                              //并设置时间为当前时间 防止多次更新数据库
                info.error = 0;
                info.etime = DateTime.Now;
                login.ModifyLogin(info);
            }
            if (info.pass != pass) {//密码错误
                info.error++;
                info.etime = DateTime.Now;
                var msg = "密码错误("+info.error+")次";
                if (info.error == 3) {//密码错误三次则进入限制逻辑
                    msg = "账号已经锁定,请15分钟后尝试登陆";
                    info.etime = DateTime.Now.AddMinutes(15);
                    info.error = 0;
                }
                login.ModifyLogin(info);
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = msg;
                return Content(this.ajaxResult.ToJson());
            }

            //登陆成功

            //生成token信息
            info.token = Utils.Strings.Rand();
            //更新登陆次数
            info.login++;
            //修改登录信息到数据库
            login.ModifyLogin(info);
            var token = info.id + ";" + info.token;
            //保存用户登陆信息到 Cache   key=>id,value=>logins
            Utils.Cache.Set(info.id.ToString(), info);
            //保存用户登陆信息到session  key=>sessionKey,value=>id+'`'+token
            HttpContext.Session.Add(Utils.Strings.TokenKeyAdmin, info.id + "`" + info.token);
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = "登陆成功";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 用户注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff() {
            var token = HttpContext.Session[Utils.Strings.TokenKeyAdmin] as string;
            if (token!=null) {
                var tk = token.Split('`');
                if (tk.Length == 2) {
                    Utils.Cache.Remove(tk[0]);
                } 
            }
            HttpContext.Session.Remove(Utils.Strings.TokenKeyAdmin);
            return View("Index");
        }
    }
}
