using Osiris.Modules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Osiris.Models
{
    public class Acceptance1Models
    {
        public IEnumerable<CombVendor> DropDownListVendor { get; set; }
        public IEnumerable<CombDistributor> DropDownListDistributor { get; set; }

        public string ReceptionNumber { get; set; }     // 受付番号
        public string Status { get; set; }              // ステータス
        public string ReceptionDate { get; set; }       // 受付日
        public string ArrivalDate { get; set; }         // 入荷日
        public string ShipDesignDate { get; set; }      // 出荷指定日
        public string ShipDate { get; set; }            // 出荷日
        public string ProductName { get; set; }         // 商品名
        public string ModelNumber { get; set; }         // 製品型番
        public string SerialNo { get; set; }            // シリアル番号
        public string VendorID { get; set; }            // 販路ID
        public string VendorName { get; set; }          // 販路名
        public string DistributorID { get; set; }       // 代理店ID
        public string DistributorName { get; set; }     // 代理店名
        public bool CharterFlg { get; set; }            // 転送チャーター
        public bool DeliveryFlg { get; set; }           // 転送宅配
        public bool AccidentFlg { get; set; }           // 品質事故
        public bool ReRepairFlg { get; set; }           // 再修理
        public string CommonMemo { get; set; }          // 共通メモ
        public string Step { get; set; }                // Step数
        public string RouteCd { get; set; }             // 経由
        public int CodCost { get; set; }                // 着払い金額（COD：Cash-On-Delivery）
        public string Shop { get; set; }                // 取扱店
        public string InvoiceNo { get; set; }           // 請求書番号
        public string ArrCustomerName { get; set; }     // 入荷元顧客名
        public string ArrPostalCode { get; set; }       // 入荷元郵便番号
        public string ArrAddress1 { get; set; }         // 入荷元住所１
        public string ArrAddress2 { get; set; }         // 入荷元住所２
        public string ArrCorporateName { get; set; }    // 入荷元法人名
        public string ArrTelNo1 { get; set; }           // 入荷元電話番号１
        public string ArrFaxNo { get; set; }         　 // 入荷元FAX番号
        public string ReturnFlg { get; set; }           // 返送先ありなしフラグ 1:あり
        public string RtnCustomerName { get; set; }     // 返送先顧客名
        public string RtnPostalCode { get; set; }       // 返送先郵便番号
        public string RtnAddress1 { get; set; }         // 返送先住所１
        public string RtnAddress2 { get; set; }         // 返送先住所２
        public string RtnCorporateName { get; set; }    // 返送先法人名
        public string RtnTelNo1 { get; set; }           // 返送先電話番号１
        public string RtnFaxNo { get; set; }         　 // 返送先FAX番号

        // コンストラクタ
        public Acceptance1Models()
        {
            DropDownListVendor = Enumerable.Empty<CombVendor>();
            DropDownListDistributor = Enumerable.Empty<CombDistributor>();
            Step = "1";
        }

        public void SetDropDownListVendor()
        {
            // 販路ドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            DropDownListVendor = ddList.GetDropDownListVendor();
        }

        public void SetDropDownListDistributor()
        {
            // 代理店ドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            DropDownListDistributor = ddList.GetDropDownListDistributor();
        }

        public void SetReceptionNumber()
        {
            // 受番の作成
            DateTime dateTime = DateTime.Now;
            string RcpDate = dateTime.ToString("yyMMdd");
            string HeadRcpNo = "B" + RcpDate;

            // 受番の最大値を取得。トランザクション開始
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    FORMAT(ISNULL(RIGHT(MAX(受付番号), 3) + 1, 1), '000') AS RCP_NUM ");
            stbSql.Append("FROM ");
            stbSql.Append("    コール受付 ");
            stbSql.Append("WHERE ");
            stbSql.Append("    受付番号 LIKE '" + HeadRcpNo + "%' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            string strRcpNoCnt = sqlRdr["RCP_NUM"].ToString();

            ReceptionNumber = HeadRcpNo + strRcpNoCnt;

            sqlRdr.Close();
            stbSql.Clear();

            // 受番の登録
            UserInfo userInfo = (UserInfo)HttpContext.Current.Session["userInfo"];
            string userName = userInfo.UserName;

            string initMsg = "ご申告いただいた症状を確認いたしました。\r\n" +
                             "ホコリセンサーの不良による症状のため、センサーボックスを交換いたしました。\r\n" +
                             "その他、各種メンテナンス・ロードテスト・クリーニングを行っております。\r\n" +
                             "以後、ご愛用いただければ幸いです。";

            stbSql.Append("INSERT ");
            stbSql.Append("INTO コール受付 ");
            stbSql.Append("( ");
            stbSql.Append("    受付番号, ");
            stbSql.Append("    修理受付番号, ");
            stbSql.Append("    ステータス, ");
            stbSql.Append("    修理ステータス, ");
            stbSql.Append("    ユーザー, ");
            stbSql.Append("    修理明細, ");
            stbSql.Append("    受付日 ");
            stbSql.Append(") ");
            stbSql.Append("VALUES ");
            stbSql.Append("( ");
            stbSql.Append("    '" + ReceptionNumber + "', ");
            stbSql.Append("    '" + ReceptionNumber + "', ");
            stbSql.Append("    '9996', ");
            stbSql.Append("    '1000', ");
            stbSql.Append("    '" + userName + "', ");
            stbSql.Append("    '" + initMsg + "', ");
            stbSql.Append("    GETDATE() ");
            stbSql.Append(") ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());
            stbSql.Clear();

            stbSql.Append("INSERT ");
            stbSql.Append("INTO コール履歴 ");
            stbSql.Append("( ");
            stbSql.Append("    受付番号, ");
            stbSql.Append("    発信日, ");
            stbSql.Append("    発信時間, ");
            stbSql.Append("    ユーザー ");
            stbSql.Append(") ");
            stbSql.Append("VALUES ");
            stbSql.Append("( ");
            stbSql.Append("    '" + ReceptionNumber + "', ");
            stbSql.Append("    GETDATE(), ");
            stbSql.Append("    GETDATE(), ");
            stbSql.Append("    '" + userName + "' ");
            stbSql.Append(") ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());

            stbSql.Clear();
            dsnLib.DB_Close();

        }

        public void SetReceptionInfo()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    コール受付.*, ");
            stbSql.Append("    ステータスマスタ.ステータス名, ");
            stbSql.Append("    販売店マスタ.ID AS 販路ID, ");
            stbSql.Append("    代理店マスタ.ID AS 代理店ID ");
            stbSql.Append("FROM ");
            stbSql.Append("    コール受付 LEFT JOIN ステータスマスタ ON ");
            stbSql.Append("    コール受付.ステータス = ステータスマスタ.ステータス番号 ");
            stbSql.Append("    LEFT JOIN 販売店マスタ ON ");
            stbSql.Append("    コール受付.販売店名 = 販売店マスタ.販売店名 ");
            stbSql.Append("    LEFT JOIN 代理店マスタ ON ");
            stbSql.Append("    コール受付.代理店名 = 代理店マスタ.代理店名 ");
            stbSql.Append("WHERE ");
            stbSql.Append("    受付番号 = '" + ReceptionNumber + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            Status = sqlRdr["ステータス名"].ToString();                                    // ステータス
            ReceptionDate = sqlRdr["受付日"].ToString();                                   // 受付日
            ArrivalDate = Convert.ToDateTime(sqlRdr["入荷日"]).ToString("yyyy/MM/dd");     // 入荷日
            ShipDesignDate = sqlRdr["出荷予定日"].ToString();                              // 出荷指定日
            ShipDate = sqlRdr["出荷日"].ToString();                                        // 出荷日
            ProductName = sqlRdr["商品名"].ToString();                                     // 商品名
            ModelNumber = sqlRdr["モデル番号"].ToString();                                 // 製品型番
            SerialNo = sqlRdr["シリアル番号"].ToString();                                  // シリアル番号
            VendorID = sqlRdr["販路ID"].ToString();                                        // 販路ID
            VendorName = sqlRdr["販売店名"].ToString();                                    // 販路名
            DistributorID = sqlRdr["代理店ID"].ToString();                                 // 代理店ID
            DistributorName = sqlRdr["代理店名"].ToString();                               // 代理店名
            CharterFlg = (bool)sqlRdr["転送チャーター"];                                   // 転送チャーター
            DeliveryFlg = (bool)sqlRdr["転送宅配"];                                        // 転送宅配
            AccidentFlg = (bool)sqlRdr["品質事故"];                                        // 品質事故
            ReRepairFlg = (bool)sqlRdr["再修理"];                                          // 再修理
            CommonMemo = sqlRdr["共通メモ"].ToString();                                    // 共通メモ
            CodCost = (int)double.Parse(sqlRdr["着払い金額"].ToString());                  // 着払い金額（COD：Cash-On-Delivery）
            Shop = sqlRdr["取扱店"].ToString();                                            // 取扱店
            InvoiceNo = sqlRdr["請求書番号"].ToString();                                   // 請求書番号
            ArrCustomerName = sqlRdr["顧客名"].ToString();                                 // 入荷元顧客名
            ArrPostalCode = sqlRdr["郵便番号"].ToString();                                 // 入荷元郵便番号
            ArrAddress1 = sqlRdr["住所1"].ToString();                                      // 入荷元住所１
            ArrAddress2 = sqlRdr["住所2"].ToString();                                      // 入荷元住所２
            ArrCorporateName = sqlRdr["会社名"].ToString();                                // 入荷元法人名
            ArrTelNo1 = sqlRdr["TEL1"].ToString();                                         // 入荷元電話番号１
            ArrFaxNo = sqlRdr["TEL2"].ToString();                                          // 入荷元FAX番号
            ReturnFlg = (bool)sqlRdr["返却先変更Yes"] ? "1" : "0";                         // 返送先ありなしフラグ
            RtnCustomerName = sqlRdr["顧客名R"].ToString();                                // 返送先顧客名
            RtnPostalCode = sqlRdr["郵便番号R"].ToString();                                // 返送先郵便番号
            RtnAddress1 = sqlRdr["住所"].ToString();                                       // 返送先住所１
            RtnAddress2 = sqlRdr["建物R"].ToString();                                      // 返送先住所２
            RtnCorporateName = sqlRdr["会社名R"].ToString();                               // 返送先法人名
            RtnTelNo1 = sqlRdr["自宅電話番号"].ToString();                                 // 返送先電話番号１
            RtnFaxNo = sqlRdr["会社電話番号"].ToString();                                  // 返送先FAX番号

            sqlRdr.Close();
            stbSql.Clear();
            dsnLib.DB_Close();
        }

        public void UpdateCommonMemo()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("UPDATE コール受付 ");
            stbSql.Append("SET ");
            stbSql.Append("    コール受付.共通メモ = '" + CommonMemo + "' ");
            stbSql.Append("WHERE ");
            stbSql.Append("    コール受付.受付番号 = '" + ReceptionNumber + "' ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());

            dsnLib.DB_Close();
        }

        public void UpdateStep1()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            // 販売店名取得
            stbSql.Append("SELECT ");
            stbSql.Append("    販売店名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    販売店マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    ID = '" + VendorID + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            VendorName = sqlRdr["販売店名"].ToString();

            stbSql.Clear();
            sqlRdr.Close();

            // 代理店名取得
            stbSql.Append("SELECT ");
            stbSql.Append("    代理店名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    代理店マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    ID = '" + DistributorID + "' ");

            sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            DistributorName = sqlRdr["代理店名"].ToString();

            sqlRdr.Close();
            stbSql.Clear();

            // 各フラグをbitに変換
            string strCharterFlg = CharterFlg ? "1" : "0";
            string strDeliveryFlg = DeliveryFlg ? "1" : "0";
            string strAccidentFlg = AccidentFlg ? "1" : "0";
            string strReRepairFlg = ReRepairFlg ? "1" : "0";
            string strReturnNoFlg = ReturnFlg == "1" ? "0" : "1";

            stbSql.Append("UPDATE コール受付 ");
            stbSql.Append("SET ");
            stbSql.Append("    コール受付.転送チャーター = '" + strCharterFlg + "', ");
            stbSql.Append("    コール受付.転送宅配 = '" + strDeliveryFlg + "', ");
            stbSql.Append("    コール受付.品質事故 = '" + strAccidentFlg + "', ");
            stbSql.Append("    コール受付.再修理 = '" + strReRepairFlg + "', ");
            stbSql.Append("    コール受付.入荷日 = '" + DateTime.Parse(ArrivalDate) + "', ");
            stbSql.Append("    コール受付.販売店名 = '" + dsnLib.SingleEscape(VendorName) + "', ");
            stbSql.Append("    コール受付.代理店名 = '" + dsnLib.SingleEscape(DistributorName) + "', ");
            stbSql.Append("    コール受付.着払い金額 = '" + CodCost + "', ");
            stbSql.Append("    コール受付.取扱店 = '" + dsnLib.SingleEscape(Shop) + "', ");
            stbSql.Append("    コール受付.請求書番号 = '" + InvoiceNo + "', ");
            stbSql.Append("    コール受付.顧客名 = '" + dsnLib.SingleEscape(ArrCustomerName) + "', ");
            stbSql.Append("    コール受付.郵便番号 = '" + ArrPostalCode + "', ");
            stbSql.Append("    コール受付.住所1 = '" + dsnLib.SingleEscape(ArrAddress1) + "', ");
            stbSql.Append("    コール受付.住所2 = '" + dsnLib.SingleEscape(ArrAddress2) + "', ");
            stbSql.Append("    コール受付.会社名 = '" + dsnLib.SingleEscape(ArrCorporateName) + "', ");
            stbSql.Append("    コール受付.TEL1 = '" + ArrTelNo1 + "', ");
            stbSql.Append("    コール受付.TEL2 = '" + ArrFaxNo + "', ");
            stbSql.Append("    コール受付.返却先変更Yes = '" + ReturnFlg + "', ");
            stbSql.Append("    コール受付.返却先変更No = '" + strReturnNoFlg + "', ");
            stbSql.Append("    コール受付.顧客名R = '" + dsnLib.SingleEscape(RtnCustomerName) + "', ");
            stbSql.Append("    コール受付.郵便番号R = '" + RtnPostalCode + "', ");
            stbSql.Append("    コール受付.住所 = '" + dsnLib.SingleEscape(RtnAddress1) + "', ");
            stbSql.Append("    コール受付.建物R = '" + dsnLib.SingleEscape(RtnAddress2) + "', ");
            stbSql.Append("    コール受付.会社名R = '" + dsnLib.SingleEscape(RtnCorporateName) + "', ");
            stbSql.Append("    コール受付.自宅電話番号 = '" + RtnTelNo1 + "', ");
            stbSql.Append("    コール受付.会社電話番号 = '" + RtnFaxNo + "' ");
            stbSql.Append("WHERE ");
            stbSql.Append("    コール受付.受付番号 = '" + ReceptionNumber + "' ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());
            dsnLib.DB_Close();
        }

    }
}
