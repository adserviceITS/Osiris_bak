using Osiris.Modules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Osiris.Models
{
    public class AcceptanceModels
    {
        public string Step { get; set; }                // Step数

        public IEnumerable<CombVendor> DropDownListVendor { get; set; }
        public IEnumerable<CombDistributor> DropDownListDistributor { get; set; }
        public IEnumerable<CombProduct> DropDownListProduct { get; set; }
        public IEnumerable<CombModel> DropDownListModel { get; set; }
        public IEnumerable<CombRepairStatus> DropDownListRepairStatus { get; set; }

        public string ReceptionNumber { get; set; }     // 受付番号
        public string Status { get; set; }              // ステータス
        public string ReceptionDate { get; set; }       // 受付日
        public string ArrivalDate { get; set; }         // 入荷日
        public string ShipDesignDate { get; set; }      // 出荷指定日
        public string ShipDate { get; set; }            // 出荷日
        public string ProductID { get; set; }           // 商品ID
        public string ProductName { get; set; }         // 商品名
        public string ModelNumber { get; set; }         // 製品型番
        public string ModelName { get; set; }           // 製品型名
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
        public String BuyDate { get; set; }             // 購入日
        public int WarrantyPeriod { get; set; }        // 保証期間
        public string WarrantyCode { get; set; }        // 保証コード
        public string WarrantyName { get; set; }        // 保証名
        public string PointOutProblem { get; set; }     // 指摘症状
        public List<ShowReportAccList> ShowReportAccLists { get; set; }
        public List<HideReportAccList> HideReportAccLists { get; set; }
        public string CartonLoc { get; set; }           // カートンロケ
        public string RepairStatusNo { get; set; }      // 修理ステータス番号
        public string AlertMsg { get; set; }            // アラートメッセージ

        // コンストラクタ
        public AcceptanceModels()
        {
            DropDownListVendor = Enumerable.Empty<CombVendor>();
            DropDownListDistributor = Enumerable.Empty<CombDistributor>();
            DropDownListProduct = Enumerable.Empty<CombProduct>();
            DropDownListModel = Enumerable.Empty<CombModel>();
            DropDownListRepairStatus = Enumerable.Empty<CombRepairStatus>();
            ShowReportAccLists = new List<ShowReportAccList>();
            HideReportAccLists = new List<HideReportAccList>();
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

        public void SetDropDownListProduct()
        {
            // 商品ドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            DropDownListProduct = ddList.GetDropDownListProduct();
        }

        public void SetDropDownListModel(string strProductID)
        {
            // 製品型番ドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            DropDownListModel = ddList.GetDropDownListModel(strProductID);
        }

        public void SetDropDownListRepairStatus()
        {
            // 修理ステータスドロップダウンリストを取得
            DropDownList ddList = new DropDownList();
            DropDownListRepairStatus = ddList.GetDropDownListRepairStatus();
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

            string strRcpNoCnt = sqlRdr.GetValue<string>("RCP_NUM");

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
            stbSql.Append("    商品名マスタ.ID AS 商品ID, ");
            stbSql.Append("    型名マスタ.型名番号 AS 製品型番, ");
            stbSql.Append("    型名マスタ.保証期間 AS 保証期間, ");
            stbSql.Append("    販売店マスタ.ID AS 販路ID, ");
            stbSql.Append("    代理店マスタ.ID AS 代理店ID, ");
            stbSql.Append("    保証コードマスタ.保証コード名 AS 保証コード名 ");
            //stbSql.Append("    ARRCPT_アラートマスタ.メッセージ ");
            stbSql.Append("FROM ");
            stbSql.Append("    コール受付 LEFT JOIN ステータスマスタ ON ");
            stbSql.Append("    コール受付.ステータス = ステータスマスタ.ステータス番号 ");
            stbSql.Append("    LEFT JOIN 商品名マスタ ON ");
            stbSql.Append("    コール受付.商品名 = 商品名マスタ.商品名 ");
            stbSql.Append("    LEFT JOIN 型名マスタ ON ");
            stbSql.Append("    コール受付.商品名 = 型名マスタ.商品名 AND ");
            stbSql.Append("    コール受付.型名 = 型名マスタ.型名 ");
            stbSql.Append("    LEFT JOIN 販売店マスタ ON ");
            stbSql.Append("    コール受付.販売店名 = 販売店マスタ.販売店名 ");
            stbSql.Append("    LEFT JOIN 代理店マスタ ON ");
            stbSql.Append("    コール受付.代理店名 = 代理店マスタ.代理店名 ");
            stbSql.Append("    LEFT JOIN 保証コードマスタ ON ");
            stbSql.Append("    コール受付.保証コード = 保証コードマスタ.保証コード ");
            //stbSql.Append("    LEFT JOIN ");
            //stbSql.Append("       (SELECT * FROM アラートマスタ ");
            //stbSql.Append("        WHERE 修理ステータス = '" + ConstDef.CPL_ARR_REP_STTS_CD + "' ");
            //stbSql.Append("       ) ARRCPT_アラートマスタ ON ");
            //stbSql.Append("    コール受付.受付番号 = ARRCPT_アラートマスタ.受付番号 ");
            stbSql.Append("WHERE ");
            stbSql.Append("    コール受付.受付番号 = '" + ReceptionNumber + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            Status = sqlRdr.GetValue<string>("ステータス名");                                    // ステータス

            // 受付日
            if (sqlRdr.GetValue<string>("受付日") != null)
                ReceptionDate = sqlRdr.GetValue<DateTime>("受付日").ToString("yyyy/MM/dd");
            else
                ReceptionDate = null;

            // 入荷日
            if (sqlRdr.GetValue<string>("入荷日") != null)
                ArrivalDate = sqlRdr.GetValue<DateTime>("入荷日").ToString("yyyy/MM/dd");
            else
                ArrivalDate = null;

            // 出荷指定日
            if (sqlRdr.GetValue<string>("出荷予定日") != null)
                ShipDesignDate = sqlRdr.GetValue<DateTime>("出荷予定日").ToString("yyyy/MM/dd");
            else
                ShipDesignDate = null;

            // 出荷日
            if (sqlRdr.GetValue<string>("出荷日") != null)
                ShipDate = sqlRdr.GetValue<DateTime>("出荷日").ToString("yyyy/MM/dd");
            else
                ShipDate = null;

            // 購入日
            if (sqlRdr.GetValue<string>("購入日") != null)
                BuyDate = sqlRdr.GetValue<DateTime>("購入日").ToString("yyyy/MM/dd");
            else
                BuyDate = null;

            ProductID = sqlRdr.GetValue<string>("商品ID");                                     // 商品ID
            ProductName = sqlRdr.GetValue<string>("商品名");                                     // 商品名
            ModelNumber = sqlRdr.GetValue<string>("製品型番");                                 // 製品型番
            ModelName = sqlRdr.GetValue<string>("型名");                                         // 型名
            SerialNo = sqlRdr.GetValue<string>("シリアル番号");                                  // シリアル番号
            VendorID = sqlRdr.GetValue<string>("販路ID");                                        // 販路ID
            VendorName = sqlRdr.GetValue<string>("販売店名");                                    // 販路名
            DistributorID = sqlRdr.GetValue<string>("代理店ID");                                 // 代理店ID
            DistributorName = sqlRdr.GetValue<string>("代理店名");                               // 代理店名
            CharterFlg = sqlRdr.GetValue<bool>("転送チャーター");                                   // 転送チャーター
            DeliveryFlg = sqlRdr.GetValue<bool>("転送宅配");                                        // 転送宅配
            AccidentFlg = sqlRdr.GetValue<bool>("品質事故");                                        // 品質事故
            ReRepairFlg = sqlRdr.GetValue<bool>("再修理");                                          // 再修理
            CommonMemo = sqlRdr.GetValue<string>("共通メモ");                                    // 共通メモ
            CodCost = sqlRdr.GetValue<int>("着払い金額");                  // 着払い金額（COD：Cash-On-Delivery）
            Shop = sqlRdr.GetValue<string>("取扱店");                                            // 取扱店
            InvoiceNo = sqlRdr.GetValue<string>("請求書番号");                                   // 請求書番号
            ArrCustomerName = sqlRdr.GetValue<string>("顧客名");                                 // 入荷元顧客名
            ArrPostalCode = sqlRdr.GetValue<string>("郵便番号");                                 // 入荷元郵便番号
            ArrAddress1 = sqlRdr.GetValue<string>("住所1");                                      // 入荷元住所１
            ArrAddress2 = sqlRdr.GetValue<string>("住所2");                                      // 入荷元住所２
            ArrCorporateName = sqlRdr.GetValue<string>("会社名");                                // 入荷元法人名
            ArrTelNo1 = sqlRdr.GetValue<string>("TEL1");                                         // 入荷元電話番号１
            ArrFaxNo = sqlRdr.GetValue<string>("TEL2");                                          // 入荷元FAX番号
            ReturnFlg = sqlRdr.GetValue<bool>("返却先変更Yes") ? "1" : "0";                         // 返送先ありなしフラグ
            RtnCustomerName = sqlRdr.GetValue<string>("顧客名R");                                // 返送先顧客名
            RtnPostalCode = sqlRdr.GetValue<string>("郵便番号R");                                // 返送先郵便番号
            RtnAddress1 = sqlRdr.GetValue<string>("住所");                                       // 返送先住所１
            RtnAddress2 = sqlRdr.GetValue<string>("建物R");                                      // 返送先住所２
            RtnCorporateName = sqlRdr.GetValue<string>("会社名R");                               // 返送先法人名
            RtnTelNo1 = sqlRdr.GetValue<string>("自宅電話番号");                                 // 返送先電話番号１
            RtnFaxNo = sqlRdr.GetValue<string>("会社電話番号");                                  // 返送先FAX番号
            WarrantyPeriod = sqlRdr.GetValue<int>("保証期間");                               // 保証コード
            WarrantyCode = sqlRdr.GetValue<string>("保証コード");                               // 保証コード
            WarrantyName = sqlRdr.GetValue<string>("保証コード名");                               // 保証名
            PointOutProblem = sqlRdr.GetValue<string>("指摘症状");                               // 指摘症状
            CartonLoc = sqlRdr.GetValue<string>("カートンロケ");                               // カートンロケ
            //AlertMsg = sqlRdr.GetValue<string>("メッセージ");                                    // アラートメッセージ
            AlertMsg = "";

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

            if (sqlRdr.HasRows)
            {
                sqlRdr.Read();
                VendorName = sqlRdr.GetValue<string>("販売店名");
            }

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

            if (sqlRdr.HasRows)
            {
                sqlRdr.Read();
                DistributorName = sqlRdr.GetValue<string>("代理店名");
            }

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
            if(!string.IsNullOrEmpty(ArrivalDate))
                stbSql.Append("    コール受付.入荷日 = '" + DateTime.Parse(ArrivalDate) + "', ");
            stbSql.Append("    コール受付.販売店名 = '" + VendorName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.代理店名 = '" + DistributorName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.着払い金額 = '" + CodCost + "', ");
            stbSql.Append("    コール受付.取扱店 = '" + Shop.SingleEscape() + "', ");
            stbSql.Append("    コール受付.請求書番号 = '" + InvoiceNo + "', ");
            stbSql.Append("    コール受付.顧客名 = '" + ArrCustomerName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.郵便番号 = '" + ArrPostalCode + "', ");
            stbSql.Append("    コール受付.住所1 = '" + ArrAddress1.SingleEscape() + "', ");
            stbSql.Append("    コール受付.住所2 = '" + ArrAddress2.SingleEscape() + "', ");
            stbSql.Append("    コール受付.会社名 = '" + ArrCorporateName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.TEL1 = '" + ArrTelNo1 + "', ");
            stbSql.Append("    コール受付.TEL2 = '" + ArrFaxNo + "', ");
            stbSql.Append("    コール受付.返却先変更Yes = '" + ReturnFlg + "', ");
            stbSql.Append("    コール受付.返却先変更No = '" + strReturnNoFlg + "', ");
            stbSql.Append("    コール受付.顧客名R = '" + RtnCustomerName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.郵便番号R = '" + RtnPostalCode + "', ");
            stbSql.Append("    コール受付.住所 = '" + RtnAddress1.SingleEscape() + "', ");
            stbSql.Append("    コール受付.建物R = '" + RtnAddress2.SingleEscape() + "', ");
            stbSql.Append("    コール受付.会社名R = '" + RtnCorporateName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.自宅電話番号 = '" + RtnTelNo1 + "', ");
            stbSql.Append("    コール受付.会社電話番号 = '" + RtnFaxNo + "' ");
            stbSql.Append("WHERE ");
            stbSql.Append("    コール受付.受付番号 = '" + ReceptionNumber + "' ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());
            dsnLib.DB_Close();
        }

        // 帳票表示付属品数のセット
        public void SetShowReportAccNum()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT * FROM 同梱物 ");
            stbSql.Append("WHERE ");
            stbSql.Append("    受付番号 = '" + ReceptionNumber + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            while (sqlRdr.Read())
            {
                ShowReportAccLists.Add(new ShowReportAccList
                {
                    ShowReportAccID = sqlRdr.GetValue<string>("ID"),
                    ShowReportAccName = sqlRdr.GetValue<string>("同梱物名"),
                    ShowReportAccQTY = sqlRdr.GetValue<int>("数量"),
                    ShowReportAccIMG = ""
                });

            }
            sqlRdr.Close();
            dsnLib.DB_Close();
        }

        // 帳票非表示付属品数のセット
        public void SetHideReportAccNum()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT * FROM T_HIDE_REPORT_ACC ");
            stbSql.Append("WHERE ");
            stbSql.Append("    RECEPTION_NUMBER = '" + ReceptionNumber + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            if (sqlRdr.HasRows)
            {
                while (sqlRdr.Read())
                {
                    HideReportAccLists.Add(new HideReportAccList
                    {
                        HideReportAccID = sqlRdr.GetValue<string>("HIDE_REPORT_ACC_ID"),
                        HideReportAccName = sqlRdr.GetValue<string>("HIDE_REPORT_ACC_NAME"),
                        HideReportAccQTY = sqlRdr.GetValue<int>("HIDE_REPORT_ACC_QTY"),
                        HideReportAccIMG = ""
                    });
                }
            } else      // 何も表示するものがなかったら初期値をセット
            {
                List<ConstValue> constList = ExtensionMethods.GetConstValue(ConstDef.HIDE_REPORT_ACC_INIT_CD);

                foreach (ConstValue item in constList)
                {
                    HideReportAccLists.Add(new HideReportAccList
                    {
                        HideReportAccID = item.ConstCd,
                        HideReportAccName = item.Value,
                        HideReportAccQTY = 0,
                        HideReportAccIMG = ""
                    });
                }
            }
            sqlRdr.Close();
            dsnLib.DB_Close();
        }

        public void UpdateStep2Call()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            // 商品名取得
            stbSql.Append("SELECT ");
            stbSql.Append("    商品名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    商品名マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    ID = '" + ProductID + "' ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            if (sqlRdr.HasRows)
            {
                sqlRdr.Read();
                ProductName = sqlRdr.GetValue<string>("商品名");
            }

            stbSql.Clear();
            sqlRdr.Close();

            // 製品型番取得
            stbSql.Append("SELECT ");
            stbSql.Append("    型名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    型名マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    型名番号 = '" + ModelNumber + "' ");

            sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            if (sqlRdr.HasRows)
            {
                sqlRdr.Read();
                ModelName = sqlRdr.GetValue<string>("型名");
            }

            stbSql.Clear();
            sqlRdr.Close();

            // 各フラグをbitに変換
            string strCharterFlg = CharterFlg ? "1" : "0";
            string strDeliveryFlg = DeliveryFlg ? "1" : "0";
            string strAccidentFlg = AccidentFlg ? "1" : "0";
            string strReRepairFlg = ReRepairFlg ? "1" : "0";

            // 共通メモに帳票非表示付属品を追記
            string strCommonMemo = CommonMemo.SingleEscape();
            for (int i = 0; i < HideReportAccLists.Count; i++)
            {
                if (HideReportAccLists[i].HideReportAccQTY != 0) {
                    strCommonMemo = strCommonMemo + "\r\n" +
                                    HideReportAccLists[i].HideReportAccName + "　" +
                                    "数量：" + HideReportAccLists[i].HideReportAccQTY;
                }
            }

            stbSql.Append("UPDATE コール受付 ");
            stbSql.Append("SET ");
            stbSql.Append("    コール受付.転送チャーター = '" + strCharterFlg + "', ");
            stbSql.Append("    コール受付.転送宅配 = '" + strDeliveryFlg + "', ");
            stbSql.Append("    コール受付.品質事故 = '" + strAccidentFlg + "', ");
            stbSql.Append("    コール受付.再修理 = '" + strReRepairFlg + "', ");
            stbSql.Append("    コール受付.シリアル番号 = '" + SerialNo + "', ");
            stbSql.Append("    コール受付.商品名 = '" + ProductName.SingleEscape() + "', ");
            stbSql.Append("    コール受付.型名 = '" + ModelName.SingleEscape() + "', ");
            if (!string.IsNullOrEmpty(BuyDate))
                stbSql.Append("    コール受付.購入日 = '" + DateTime.Parse(BuyDate) + "', ");
            stbSql.Append("    コール受付.保証コード = '" + WarrantyCode + "', ");
            stbSql.Append("    コール受付.指摘症状 = '" + PointOutProblem.SingleEscape() + "', ");
            stbSql.Append("    コール受付.共通メモ = '" + strCommonMemo + "', ");
            stbSql.Append("    コール受付.カートンロケ = '" + CartonLoc + "' ");
            stbSql.Append("WHERE ");
            stbSql.Append("    コール受付.受付番号 = '" + ReceptionNumber + "' ");

            dsnLib.ExecSQLUpdate(stbSql.ToString());

            dsnLib.DB_Close();
        }

        public void UpdateStep2Acc()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            // 帳票表示付属品
            // ID属性があるのでMERGEする。
            for (int i = 0; i < ShowReportAccLists.Count; i++)
            {
                stbSql.Append("MERGE INTO 同梱物 AS DKN ");
                stbSql.Append("USING ( ");
                stbSql.Append("    SELECT ");
                stbSql.Append("     '" + ReceptionNumber + "' AS 受付番号, ");
                stbSql.Append("     '" + ShowReportAccLists[i].ShowReportAccName + "' AS 同梱物名, ");
                stbSql.Append("      " + ShowReportAccLists[i].ShowReportAccQTY + " AS 数量 ");
                stbSql.Append("    ) AS DATA ON ");
                stbSql.Append("    ( ");
                stbSql.Append("       DKN.受付番号 = DATA.受付番号 AND ");
                stbSql.Append("       DKN.同梱物名 = DATA.同梱物名 ");
                stbSql.Append("    ) ");
                stbSql.Append("    WHEN MATCHED THEN ");
                stbSql.Append("       UPDATE SET ");
                stbSql.Append("          数量 = DATA.数量 ");
                stbSql.Append("    WHEN NOT MATCHED THEN ");
                stbSql.Append("       INSERT (受付番号, 同梱物名, 数量) ");
                stbSql.Append("       VALUES ");
                stbSql.Append("       ( ");
                stbSql.Append("          DATA.受付番号, ");
                stbSql.Append("          DATA.同梱物名, ");
                stbSql.Append("          DATA.数量 ");
                stbSql.Append("       ); ");

                dsnLib.ExecSQLUpdate(stbSql.ToString());
                stbSql.Clear();
            }

            // 帳票非表示付属品
            // ID属性ないけどメンドイのでMERGEする。
            for (int i = 0; i < HideReportAccLists.Count; i++)
            {
                stbSql.Append("MERGE INTO T_HIDE_REPORT_ACC AS DKN ");
                stbSql.Append("USING ( ");
                stbSql.Append("    SELECT ");
                stbSql.Append("     '" + ReceptionNumber + "' AS RECEPTION_NUMBER, ");
                stbSql.Append("     '" + HideReportAccLists[i].HideReportAccName + "' AS HIDE_REPORT_ACC_NAME, ");
                stbSql.Append("      " + HideReportAccLists[i].HideReportAccQTY + " AS HIDE_REPORT_ACC_QTY ");
                stbSql.Append("    ) AS DATA ON ");
                stbSql.Append("    ( ");
                stbSql.Append("       DKN.RECEPTION_NUMBER = DATA.RECEPTION_NUMBER AND ");
                stbSql.Append("       DKN.HIDE_REPORT_ACC_NAME = DATA.HIDE_REPORT_ACC_NAME ");
                stbSql.Append("    ) ");
                stbSql.Append("    WHEN MATCHED THEN ");
                stbSql.Append("       UPDATE SET ");
                stbSql.Append("          HIDE_REPORT_ACC_QTY = DATA.HIDE_REPORT_ACC_QTY ");
                stbSql.Append("    WHEN NOT MATCHED THEN ");
                stbSql.Append("       INSERT (RECEPTION_NUMBER, HIDE_REPORT_ACC_ID, HIDE_REPORT_ACC_NAME, HIDE_REPORT_ACC_QTY) ");
                stbSql.Append("       VALUES ");
                stbSql.Append("       ( ");
                stbSql.Append("          DATA.RECEPTION_NUMBER, ");
                stbSql.Append("          NULL, ");
                stbSql.Append("          DATA.HIDE_REPORT_ACC_NAME, ");
                stbSql.Append("          DATA.HIDE_REPORT_ACC_QTY ");
                stbSql.Append("       ); ");

                dsnLib.ExecSQLUpdate(stbSql.ToString());
                stbSql.Clear();
            }
            dsnLib.DB_Close();
        }
    }

    public class ShowReportAccList
    {
        public string ShowReportAccID { get; set; }
        public string ShowReportAccName { get; set; }
        public int ShowReportAccQTY { get; set; }
        public string ShowReportAccIMG { get; set; }
    }

    public class HideReportAccList
    {
        public string HideReportAccID { get; set; }
        public string HideReportAccName { get; set; }
        public int HideReportAccQTY { get; set; }
        public string HideReportAccIMG { get; set; }
    }
}
