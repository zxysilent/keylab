using Keylab.BLL.Admin;
using Keylab.Models;
using Keylab.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Keylab.Web.Filter;
namespace Keylab.Web.Areas.Admin.Controllers {
    [FilterAdmin]
    public class SliderController : Controller {
        /// <summary>
        /// json 返回格式
        /// </summary>
        private AjaxResult ajaxResult = new AjaxResult();
        /// <summary>
        /// bll slider
        /// </summary>
        private Slider slider = new Slider();

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
            var one = slider.One(id.Value).ToList<Sliders>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.slider = one[0];
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
            var one = slider.One(id.Value).ToList<Sliders>();
            if (one.Count != 1) {
                return RedirectToAction("Index");
            }
            ViewBag.slider = one[0];
            return View();
        }
        /// <summary>
        /// 渲染 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add() {
            return View();
        }
        /*----------------------------------------------------------------*/
        /*--------------------------- 功能区域 ---------------------------*/

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
            List<Sliders> resList = slider.List(pi.Value, ps.Value).ToList<Sliders>();
            if (resList.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            int count = slider.Count();
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
        public ActionResult EditInfo(Sliders sld) {
            if (sld == null) {
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
            if (!sld.skip) {
                sld.link = "javascript:;";
            }
            sld.status = true;
            sld.userid = logins.id;
            if (slider.Edit(sld)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "修改成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "修改失败！";
            return Content(this.ajaxResult.ToJson());
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sld"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddInfo(Sliders sld) {
            if (sld == null) {
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
            if (!sld.skip) {
                sld.link = "javascript:;";
            }
            sld.status = true;
            sld.userid = logins.id;
            if (slider.Add(sld)) {
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
            if (slider.Del(id.Value)) {
                this.ajaxResult.status = Status.success;
                this.ajaxResult.message = "删除成功！";
                return Content(this.ajaxResult.ToJson());
            }
            this.ajaxResult.status = Status.failed;
            this.ajaxResult.message = "删除失败！";
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
            var vPath = "/Upload/slider/" + DateTime.Now.ToString("yyyyMMdd") + "/";
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
