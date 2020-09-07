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
            stbSql.Append("FROM dbo.ユーザー ");
            stbSql.Append("WHERE ログイン名 = '" + model.ID + "' AND ");
            stbSql.Append("      パスワード = '" + model.Password + "'");

            DSNLibrary dsnLib = new DSNLibrary();
            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            if (!sqlRdr.HasRows)
            {
                ModelState.AddModelError(string.Empty, "ID、または Password が違います");
                sqlRdr.Close();
                dsnLib.DB_Close();
                return View(model);
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            // 認証成功
            // 認証クッキーにユーザーIDをセット
            FormsAuthentication.SetAuthCookie(model.ID, false);
            // ユーザーオブジェクト作成してセッションにセット
            UserInfo userInfo = new UserInfo(model.ID);
            Session["userInfo"] = userInfo;

            return RedirectToAction("Index", "Main");
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
