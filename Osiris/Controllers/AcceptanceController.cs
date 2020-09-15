using Osiris.Models;
using Osiris.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Osiris.Controllers
{
    public class AcceptanceController : Controller
    {
        // GET: 新規登録_1
        //public ActionResult NewEntry_1()
        //{
        //    Acceptance1Models models = new Acceptance1Models();
        //    models.SetReceptionNumber();

        //    ViewBag.NewEntryFlg = "true";

        //    return View("Acceptance1", models);
        //}

        // GET: 新規登録_2
        //public ActionResult NewEntry_2()
        //{
        //    Acceptance1Models models = new Acceptance1Models();
        //    models.SetReceptionNumber();

        //    JsonResult ReceptionNumberInfo = Json(new
        //    {
        //        models.ReceptionNumber
        //    },
        //    JsonRequestBehavior.AllowGet);

        //    return ReceptionNumberInfo;
        //}

        // GET: 新規登録
        public ActionResult NewEntry()
        {
            AcceptanceModels models = new AcceptanceModels();
            models.SetReceptionNumber();

            return SearchRcpInfo(models.ReceptionNumber);
        }

        // POST: 受番検索
        public ActionResult SearchRcpInfo(string SearchReceptionNumber)
        {
            AcceptanceModels models = new AcceptanceModels();
            models.ReceptionNumber = SearchReceptionNumber;
            models.SetReceptionInfo();
            models.SetDropDownListVendor();
            models.SetDropDownListDistributor();
            models.Step = "1";

            return View("Acceptance1", models);
        }

        // POST: 共通メモ登録
        [HttpPost]
        public ActionResult EntryCommonMemo(AcceptanceModels models)
        {
            models.UpdateCommonMemo();
            models.SetDropDownListVendor();
            models.SetDropDownListDistributor();

            return View("Acceptance1", models);
        }

        // GET: 代理店情報取得
        [HttpGet]
        public ActionResult GetDistributorInfo(string prmDistributorID)
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    代理店マスタ.顧客名, ");
            stbSql.Append("    代理店マスタ.郵便番号, ");
            stbSql.Append("    代理店マスタ.住所１, ");
            stbSql.Append("    代理店マスタ.住所２, ");
            stbSql.Append("    代理店マスタ.法人名, ");
            stbSql.Append("    代理店マスタ.電話番号１, ");
            stbSql.Append("    代理店マスタ.FAX番号, ");
            stbSql.Append("    代理店マスタ.返送先顧客名, ");
            stbSql.Append("    代理店マスタ.返送先郵便番号, ");
            stbSql.Append("    代理店マスタ.返送先住所１, ");
            stbSql.Append("    代理店マスタ.返送先住所２, ");
            stbSql.Append("    代理店マスタ.返送先法人名, ");
            stbSql.Append("    代理店マスタ.返送先電話番号１, ");
            stbSql.Append("    代理店マスタ.返送先FAX番号 ");
            stbSql.Append("FROM ");
            stbSql.Append("    代理店マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    代理店マスタ.ID = '" + prmDistributorID + "' ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    代理店マスタ.序列 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            string strCustomerName = sqlRdr.GetValue<string>("顧客名");
            string strPostalCode = sqlRdr.GetValue<string>("郵便番号");
            string strAddress1 = sqlRdr.GetValue<string>("住所１");
            string strAddress2 = sqlRdr.GetValue<string>("住所２");
            string strCorporateName = sqlRdr.GetValue<string>("法人名");
            string strTelNo1 = sqlRdr.GetValue<string>("電話番号１");
            string strFaxNo = sqlRdr.GetValue<string>("FAX番号");
            string strRtnCustomerName = sqlRdr.GetValue<string>("返送先顧客名");
            string strRtnPostalCode = sqlRdr.GetValue<string>("返送先郵便番号");
            string strRtnAddress1 = sqlRdr.GetValue<string>("返送先住所１");
            string strRtnAddress2 = sqlRdr.GetValue<string>("返送先住所２");
            string strRtnCorporateName = sqlRdr.GetValue<string>("返送先法人名");
            string strRtnTelNo1 = sqlRdr.GetValue<string>("返送先電話番号１");
            string strRtnFaxNo = sqlRdr.GetValue<string>("返送先FAX番号");

            sqlRdr.Close();
            dsnLib.DB_Close();

            JsonResult DistributorInfo = Json( new
            {
                CustomerName = strCustomerName,
                PostalCode = strPostalCode,
                Address1 = strAddress1,
                Address2 = strAddress2,
                CorporateName = strCorporateName,
                TelNo1 = strTelNo1,
                FaxNo = strFaxNo,
                RtnCustomerName = strRtnCustomerName,
                RtnPostalCode = strRtnPostalCode,
                RtnAddress1 = strRtnAddress1,
                RtnAddress2 = strRtnAddress2,
                RtnCorporateName = strRtnCorporateName,
                RtnTelNo1 = strRtnTelNo1,
                RtnFaxNo = strRtnFaxNo
            },
            JsonRequestBehavior.AllowGet);

            return DistributorInfo;
        }

        // POST: STEP1の次へボタン押下時
        public ActionResult NextToStep2(AcceptanceModels models)
        {
            models.UpdateStep1();

            models.SetReceptionInfo();
            models.SetDropDownListProduct();
            models.SetDropDownListModel(models.ProductID);
            models.SetShowReportAccNum();
            models.SetHideReportAccNum();
            models.Step = "2";

            return View("Acceptance2", models);
        }

        // GET: 商品名変更時の製品型番リスト取得
        public ActionResult GetModelDropDownList(string prmProductID)
        {
            // 製品型番ドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            IEnumerable<CombModel> DropDownListModel = ddList.GetDropDownListModel(prmProductID);

            return Json(DropDownListModel, JsonRequestBehavior.AllowGet);
        }

        // GET: 商品名変更時の帳票表示付属品の初期値取得
        public ActionResult GetShowReportAcc(string prmProductID)
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("   ACM.* ");
            stbSql.Append("FROM ");
            stbSql.Append("   同梱物初期値マスタ ACM INNER JOIN 商品名マスタ PRM ON ");
            stbSql.Append("   ACM.商品名 = PRM.商品名 ");
            stbSql.Append("WHERE ");
            stbSql.Append("   ACM.削除フラグ <> '1' AND ");
            stbSql.Append("   PRM.ID = '" + prmProductID + "' ");
            stbSql.Append("ORDER BY ACM.序列 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            ArrayList ShowReportAccInitList = new ArrayList();

            while (sqlRdr.Read())
            {
                ShowReportAccInitList.Add(sqlRdr.GetValue<string>("同梱物名"));
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return Json(ShowReportAccInitList, JsonRequestBehavior.AllowGet);
        }

        // GET: 商品名変更時の帳票非表示付属品の初期値取得
        public ActionResult GetHideReportAcc(string prmProductID)
        {
            return Json(ConstDef.HIDE_REPORT_ACC_INIT, JsonRequestBehavior.AllowGet);
        }

        // POST: STEP2の次へボタン押下時
        public ActionResult NextToStep3(AcceptanceModels models)
        {
            models.UpdateStep2Call();
            models.UpdateStep2Acc();

            models.SetReceptionInfo();
            models.Step = "3";

            return View("Acceptance3", models);
        }

        // POST: STEP1へ戻る
        public ActionResult BackToStep1(AcceptanceModels models)
        {
            models.UpdateStep2Call();
            models.UpdateStep2Acc();

            return SearchRcpInfo(models.ReceptionNumber);
        }
    }
}
