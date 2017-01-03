using Keylab.BLL.Admin;
using Keylab.Models;
using Keylab.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Areas.Admin.Controllers {
    [FilterAdmin]
    public class UserController : Controller {
        /// <summary>
        /// json 返回格式
        /// </summary>
        private AjaxResult ajaxResult = new AjaxResult();
        /// <summary>
        /// bll user
        /// </summary>
        private User user = new User();
        /// <summary>
        /// 渲染 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            return View();
        }
        /// <summary>
        /// 渲染 详细页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            var one = user.One(id.Value).ToList<Users>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.user = one[0];
            return View();
        }
        /// <summary>
        /// 渲染 编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            var one = user.One(id.Value).ToList<Users>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.user = one[0];
            return View();
        }
        /// <summary>
        /// 渲染 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add() {
            return View();
        }
        /// <summary>
        /// 渲染 修改自身信息页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditSelf() {
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                return RedirectToAction("Index");
            }
            var one = user.One(logins.id).ToList<Users>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.user = one[0];
            return View();
        }
        /// <summary>
        /// 渲染 修改密码页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditPass() {
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                return RedirectToAction("Index");
            }
            var one = user.One(logins.id).ToList<Users>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.user = one[0];
            return View();
        }
        /// <summary>
        /// 渲染 个人信息页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DetailSelf() {
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                return RedirectToAction("Index");
            }
            var one = user.One(logins.id).ToList<Users>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.user = one[0];
            return View();
        }
        /*----------------------------------------------------------------*/
        /*--------------------------- 功能区域 ---------------------------*/

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Logins LoginInfo(HttpContextBase content) {
            var token = content.Session[Utils.Strings.TokenKeyAdmin] as string;
            if (token != null) {
                var tks = token.Split('`');
                if (tks.Length == 2) {
                    if (Utils.Cache.Has(tks[0])) {
                        var logins = Utils.Cache.Get(tks[0]) as Logins;
                        if (logins != null) {
                            return logins;
                        }
                        return null;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }
        /// <summary>
        /// api slider 分页信息
        /// </summary>
        /// <param name="pi">PageIndex</param>
        /// <param name="ps">PageSize</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public ActionResult ListInfo(int? pi, int? ps) {
            if (!pi.HasValue || !ps.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            List<Users> resList = user.List(pi.Value, ps.Value).ToList<Users>();
            if (resList.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            int count = user.Count();
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = new { count = count, data = resList };
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditInfo(Users model) {
            if (model == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (!logins.super) {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "没有此操作权限!";
                return Content(this.ajaxResult.ToJson());
            }
            if (model.phone == null) {
                model.phone = "";
            }
            if (model.email == null) {
                model.email = "";
            }
            model.userid = logins.id;
            model.utime = DateTime.Now;
            if (user.Edit(model)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "修改成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "修改失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 编辑自身数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSelfInfo(Users model) {
            if (model == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (model.phone == null) {
                model.phone = "";
            }
            if (model.email == null) {
                model.email = "";
            }
            model.userid = logins.id;
            model.utime = DateTime.Now;
            if (user.Edit(model)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "修改成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "修改失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 编辑自身数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPassInfo(int? id, string pass, string newpass) {
            if (!id.HasValue || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(newpass)) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据！";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (logins.id != id.Value) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            var res = user.GetPass(id.Value);
            if (String.IsNullOrEmpty(res) || res != pass) {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "原始密码输入错误!";
                return Content(this.ajaxResult.ToJson());
            }
            if (user.EditPass(id.Value, newpass)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "修改成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "修改失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// num  重复性检测
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RepeatNumInfo(string num) {
            if (String.IsNullOrEmpty(num)) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            if (user.RepeatNum(num)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "有数据成功！";
                return Content(this.ajaxResult.ToJson());
            } else {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "没有数据！";
                return Content(this.ajaxResult.ToJson());
            }

        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassInfo(int? id) {
            if (!id.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据！";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (!logins.super) {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "没有此操作权限!";
                return Content(this.ajaxResult.ToJson());
            }
            if (user.ResetPass(id.Value)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "操作成功！";
                return Content(this.ajaxResult.ToJson());
            } else {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "操作失败！";
                return Content(this.ajaxResult.ToJson());
            }

        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddInfo(Users model) {
            if (model == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据!";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (!logins.super) {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "没有此操作权限!";
                return Content(this.ajaxResult.ToJson());
            }
            if (model.phone == null) {
                model.phone = "";
            }
            if (model.email == null) {
                model.email = "";
            }
            model.super = false;
            model.error = 0;
            model.etime = DateTime.Now;
            model.userid = logins.id;
            model.utime = DateTime.Now;
            model.status = true;
            if (user.Add(model)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "添加成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "添加失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelInfo(int? id) {
            if (!id.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据！";
                return Content(this.ajaxResult.ToJson());
            }
            var logins = UserController.LoginInfo(this.HttpContext);
            if (logins == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "非法的操作!";
                return Content(this.ajaxResult.ToJson());
            }
            if (!logins.super) {
                this.ajaxResult.status = Status.failed;
                this.ajaxResult.message = "没有此操作权限!";
                return Content(this.ajaxResult.ToJson());
            }
            if (user.Del(id.Value)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "删除成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "删除失败！";
            return Content(this.ajaxResult.ToJson());
        }
    }
}
