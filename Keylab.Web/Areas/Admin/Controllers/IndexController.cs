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
    public class IndexController : Controller {
        /// <summary>
        /// json 返回格式
        /// </summary>
        private AjaxResult ajaxResult = new AjaxResult();
        /// <summary>
        /// bll slider
        /// </summary>
        private Index index = new Index();
        public ActionResult Index() {
            var sliderCount = index.SliderCount();
            ViewBag.sliderCount = sliderCount;
            var articleCount = index.ArticleCount();
            ViewBag.articleCount = articleCount;
            var userCount = index.UserCount();
            ViewBag.userCount = userCount;
            return View();
        }

    }
}
