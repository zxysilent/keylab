using Keylab.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Controllers {
    public class IndexController : Controller {

        private Index index = new Index();

        [OutputCache(Duration=30)]
        public ActionResult Index() {
            //
            ViewBag.Silder = index.Silder();
            ViewBag.ArticleXwgg = index.Article("tzgg", "xwgg",7);
            ViewBag.ArticleZxdt = index.Article("tzgg", "zxdt", 7);
            ViewBag.ArticleKycg = index.Article("xsky", "kycg", 7);
            ViewBag.ArticleXsbg = index.Article("xsky", "xsbg", 7);
            ViewBag.ArticleZxdtImg = index.ArticleImg("tzgg", "zxdt", 4);
            return View();
        }
        public ActionResult Error() {
            return View();
        }
    }
}
