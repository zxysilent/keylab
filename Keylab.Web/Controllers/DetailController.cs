using Keylab.BLL;
using Keylab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keylab.Web.Controllers {
    public class DetailController : Controller {
        //
        // GET: /Detail/
        /// <summary>
        /// bll Detail
        /// </summary>
        private Detail detail = new Detail();
         [OutputCache(Duration = 30)]
        public ActionResult Index(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index", "Index");
            }
            var one = detail.One(id.Value).ToList<Articles>();
            if (one.Count != 1) {
                return RedirectToAction("Index", "Index");
            }
            var info = one[0];
            info.hit++;
            detail.Hit(info);
            ViewBag.info = info;
            if (info.type) {
                var prev = detail.PrevOne(info.subnum, info.utime, info.id).ToList<Articles>();
                if (prev.Count == 1) {
                    ViewBag.prev = prev[0];
                } else {
                    ViewBag.prev = null;
                }
                var next = detail.NextOne(info.subnum, info.utime, info.id).ToList<Articles>();
                if (next.Count == 1) {
                    ViewBag.next = next[0];
                } else {
                    ViewBag.next = null;
                }
                return View();
            } else {
                return View("Page");
            }
        }
    }
}
