using Osiris.Modules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Osiris.Models
{
    public class Acceptance2Models
    {
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

        // コンストラクタ
        public Acceptance2Models()
        {
            Step = "2";
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

            sqlRdr.Close();
            stbSql.Clear();
            dsnLib.DB_Close();
        }

    }
}
