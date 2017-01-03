using Keylab.BLL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Controllers {
    public class ListController : Controller {
        private Lists list = new Lists();
        private AjaxResult ajaxResult = new AjaxResult();
         [OutputCache(Duration = 30)]
        public ActionResult Index(string super, string suber) {
            if (String.IsNullOrEmpty(super) || String.IsNullOrEmpty(suber)) {
                return RedirectToAction("Index", "Index");
            }
            if (String.IsNullOrEmpty(super) || String.IsNullOrEmpty(suber)) {
                return RedirectToAction("Index", "Index");
            }
            var oneinfo = list.ClsInfo(super, suber).ToList<Subclass>();
            if (oneinfo.Count != 1) {
                return RedirectToAction("Index", "Index");
            }
            ViewBag.cls = oneinfo[0];
            return View();
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult ListInfo(int? pi, int? ps, string sup, string sub) {
            if (!pi.HasValue || !ps.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            if (String.IsNullOrEmpty(sup) || String.IsNullOrEmpty(sub)) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            List<Articles> resList = list.List(pi.Value, ps.Value, sup, sub).ToList<Articles>();
            if (resList.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            int count = list.Count(sup, sub);
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = new { count = count, data = resList };
            return Content(this.ajaxResult.ToJson());
        }
    }
}
