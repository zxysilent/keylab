using Keylab.BLL.Admin;
using Keylab.Models;
using Keylab.Web.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Areas.Admin.Controllers {
    [FilterAdmin]
    public class ArticleController : Controller {
        /// <summary>
        /// json 返回格式
        /// </summary>
        private AjaxResult ajaxResult = new AjaxResult();
        /// <summary>
        /// bll article
        /// </summary>
        private Article article = new Article();
        public ActionResult Index() {
            return View();
        }
        public ActionResult Edit(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            var one = article.One(id.Value).ToList<Articles>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.article = one[0];
            return View();
        }
        public ActionResult Add() {
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
            var one = article.One(id.Value).ToList<Articles>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.article = one[0];
            return View();
        }
        /*----------------------------------------------------------------*/
        /*--------------------------- 功能区域 ---------------------------*/

        #region 基本类别
        public ActionResult Supclass() {
            var list = article.Supclass().ToList<Supclass>();
            if (list.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = list;
            return Content(this.ajaxResult.ToJson());
        }
        public ActionResult Subclass() {
            var list = article.Subclass().ToList<Subclass>();
            if (list.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = list;
            return Content(this.ajaxResult.ToJson());
        }
        public ActionResult SubBySup(string sup) {
            var list = article.SubBySup(sup).ToList<Subclass>();
            if (list.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = list;
            return Content(this.ajaxResult.ToJson());
        }
        #endregion

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
            if (article.Del(id.Value)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "删除成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "删除失败！";
            return Content(this.ajaxResult.ToJson());
        }

        /// <summary>
        /// api 分页信息
        /// </summary>
        /// <param name="pi">PageIndex</param>
        /// <param name="ps">PageSize</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public ActionResult ListInfo(int? pi, int? ps, string sup, string sub, string key) {
            if (!pi.HasValue || !ps.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            List<Articles> resList = article.List(pi.Value, ps.Value, sup, sub, key).ToList<Articles>();
            if (resList.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            int count = article.Count(sup, sub, key);
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = new { count = count, data = resList };
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddInfo(Articles model) {
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
            if (String.IsNullOrEmpty(model.url)) {
                model.url = "";
            }
            if (String.IsNullOrEmpty(model.origin)) {
                model.origin = "";
            }
            model.userid = logins.id;
            model.status = true;
            if (article.Add(model)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "添加成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "添加失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditInfo(Articles model) {
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
            if (String.IsNullOrEmpty(model.url)) {
                model.url = "";
            }
            if (String.IsNullOrEmpty(model.origin)) {
                model.origin = "";
            }
            model.userid = logins.id;
            model.status = true;
            if (article.Edit(model)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "修改成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "修改失败！";
            return Content(this.ajaxResult.ToJson());
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file) {
            if (file == null) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据！";
                return Content(this.ajaxResult.ToJson());
            }
            var vPath = "/Upload/article/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            var localPath = Request.MapPath(vPath);
            if (!Directory.Exists(localPath)) {
                Directory.CreateDirectory(localPath);
            }
            var fileName = Utils.Strings.Rand() + Path.GetExtension(file.FileName);
            try {
                file.SaveAs(localPath + fileName);
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = vPath + fileName;
                return Content(this.ajaxResult.ToJson());
            } catch {
                this.ajaxResult.status = Status.except;
                this.ajaxResult.message = "上传异常";
                return Content(this.ajaxResult.ToJson());
            }
        }
    }
}
