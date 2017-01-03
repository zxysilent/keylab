using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keylab.Models;
namespace Keylab.Web.Filter {
    public class FilterAdmin : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var content = filterContext.HttpContext;
            var method = content.Request.HttpMethod;
            if (method.ToUpper() == "GET") {
                var token = content.Session[Utils.Strings.TokenKeyAdmin] as string;
                if (token == null || !CheckToken(token)) {
                    content.Session.Remove(Utils.Strings.TokenKeyAdmin);
                    // content.Response.Write("你没有权限哦<a href='/admin/login/index'>走去登陆</a>");
                    content.Response.Redirect("/admin/login/index");
                    content.Response.End();
                } else {
                    base.OnActionExecuting(filterContext);
                }
            } else if (method.ToUpper() == "POST") {
                var token = content.Session[Utils.Strings.TokenKeyAdmin] as string;
                if (token == null || !CheckToken(token)) {
                    AjaxResult ajaxReult = new AjaxResult();
                    ajaxReult.status = Status.timeout;
                    ajaxReult.message = "你没有登陆哦";
                    content.Session.Remove(Utils.Strings.TokenKeyAdmin);
                    content.Response.Write(ajaxReult.ToJson());
                    content.Response.End();
                } else {
                    base.OnActionExecuting(filterContext);
                }
            } else {
                base.OnActionExecuting(filterContext);
            }
        }
        private static bool CheckToken(string token) {
            var tks = token.Split('`');
            if (tks.Length == 2) {
                if (Utils.Cache.Has(tks[0])) {
                    var logins = Utils.Cache.Get(tks[0]) as Logins;
                    if (logins != null && logins.token == tks[1]) {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
    }
}