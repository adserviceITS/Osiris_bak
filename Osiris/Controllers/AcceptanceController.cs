using Osiris.Models;
using Osiris.Modules;
using System;
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
            Acceptance1Models models = new Acceptance1Models();
            models.SetReceptionNumber();

            return SearchRcpInfo(models.ReceptionNumber);
        }

        // POST: 受番検索
        public ActionResult SearchRcpInfo(string SearchReceptionNumber)
        {
            Acceptance1Models models = new Acceptance1Models();
            models.ReceptionNumber = SearchReceptionNumber;
            models.SetReceptionInfo();
            models.SetDropDownListVendor();
            models.SetDropDownListDistributor();

            return View("Acceptance1", models);
        }

        // POST: 共通メモ登録
        [HttpPost]
        public ActionResult EntryCommonMemo(Acceptance1Models models)
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

            string strCustomerName = sqlRdr["顧客名"].ToString();
            string strPostalCode = sqlRdr["郵便番号"].ToString();
            string strAddress1 = sqlRdr["住所１"].ToString();
            string strAddress2 = sqlRdr["住所２"].ToString();
            string strCorporateName = sqlRdr["法人名"].ToString();
            string strTelNo1 = sqlRdr["電話番号１"].ToString();
            string strFaxNo = sqlRdr["FAX番号"].ToString();
            string strRtnCustomerName = sqlRdr["返送先顧客名"].ToString();
            string strRtnPostalCode = sqlRdr["返送先郵便番号"].ToString();
            string strRtnAddress1 = sqlRdr["返送先住所１"].ToString();
            string strRtnAddress2 = sqlRdr["返送先住所２"].ToString();
            string strRtnCorporateName = sqlRdr["返送先法人名"].ToString();
            string strRtnTelNo1 = sqlRdr["返送先電話番号１"].ToString();
            string strRtnFaxNo = sqlRdr["返送先FAX番号"].ToString();

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
        public ActionResult NextToStep2(Acceptance1Models models)
        {
            models.UpdateStep1();

            Acceptance2Models models2 = new Acceptance2Models();
            models2.ReceptionNumber = models.ReceptionNumber;
            models2.SetReceptionInfo();

            return View("Acceptance2", models2);
        }
    }
}
