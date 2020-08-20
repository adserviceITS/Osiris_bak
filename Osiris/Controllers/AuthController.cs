using Osiris.Models;
using Osiris.Modules;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Osiris.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        // その内、認証クッキーにログイン情報があればログイン画面をすっ飛ばすようにする。
        [HttpPost]
        public ActionResult Login(LoginModels model)
        {
            // 認証
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT * ");
            stbSql.Append("FROM dbo.M_USER ");
            stbSql.Append("WHERE ID = '" + model.ID + "' AND ");
            stbSql.Append("      PASS = '" + model.Password + "'");

            DSNLibrary dsnLib = new DSNLibrary();
            dsnLib.ExecSQLRead(stbSql.ToString());

            if (!dsnLib.Reader.HasRows)
            {
                ModelState.AddModelError(string.Empty, "ID、または Password が違います");
                dsnLib.DB_Close();
                return View(model);
            }
            dsnLib.DB_Close();

            // 認証成功
            // 認証クッキーにユーザーIDをセット
            FormsAuthentication.SetAuthCookie(model.ID, false);
            // ユーザーオブジェクト作成してセッションにセット
            UserInfo userInfo = new UserInfo(model.ID);
            Session["userInfo"] = userInfo;

            return RedirectToAction("Index", "Acceptance");
        }

        // GET: Auth/Logout
        public ActionResult Logout()
        {
            // 認証クッキーの削除
            FormsAuthentication.SignOut();
            // セッションの破棄
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}
