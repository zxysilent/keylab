using Keylab.BLL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Controllers {
    public class SearchController : Controller {
        private Search search = new Search();
        private AjaxResult ajaxResult = new AjaxResult();
         [OutputCache(Duration = 30)]
        public ActionResult Index(string key) {
            ViewBag.key = key;
            return View();
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult ListInfo(int? pi, int? ps, string key) {
            ViewBag.key = key;
            if (!pi.HasValue || !ps.HasValue) {
                this.ajaxResult.status = Status.isnull;
                this.ajaxResult.message = "请输入正确的数据";
                return Content(this.ajaxResult.ToJson());
            }
            List<Articles> resList = search.List(pi.Value, ps.Value, key).ToList<Articles>();
            if (resList.Count < 1) {
                this.ajaxResult.status = Status.nodata;
                this.ajaxResult.message = "未查询到数据";
                return Content(this.ajaxResult.ToJson());
            }
            int count = search.Count(key);
            this.ajaxResult.status = Status.success;
            this.ajaxResult.message = new { count = count, data = resList };
            return Content(this.ajaxResult.ToJson());
        }
    }
}
