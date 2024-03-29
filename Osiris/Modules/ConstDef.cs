﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Osiris.Modules
{
    // 定数は大文字とアンダースコアで定義する。
    public class ConstDef
    {
        /* DB定義変数 */
        /* ローカル開発用 */
        public const string DB_CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TOMITAGON";

        /* 開発機用 */
        //public const string DB_DATA_SOURCE = "192.168.201.242";
        //public const string DB_DATA_BASE = "TOMITAGON";
        //public const string DB_USER_ID = "Bal_Staff";
        //public const string DB_PASSWORD = "15mh49WB58";

        /* 本番機用 */
        //public const string DB_DATA_SOURCE = "192.168.0.1";
        //public const string DB_USER_ID = "sa";
        //public const string DB_PASSWORD = "takota";

        //public const string DB_CONNECTION_STRING = @"Data Source=" + DB_DATA_SOURCE + ";"
        //                                         + @"Database=" + DB_DATA_BASE + ";"
        //                                         + @"Integrated Security=False;"
        //                                         + @"User ID=" + DB_USER_ID + ";"
        //                                         + @"Password=" + DB_PASSWORD;

        // 定数マスタのグループ設定
        public const string PAGE_ROW_SIZE_CD = "001";   // 1画面に表示する行数
        public const string HIDE_REPORT_ACC_INIT_CD = "002";    // 帳票に表示しない付属品の初期設定
        public const string REQ_COD_COST_DISTRIBUTOR = "003";   // 着払い金額必須代理店
        public const string PHOTO_STUDIO_DIR_PATH = "004";   // 写真館ディレクトリ
        public const string ALERT_UNLOCK_KEY = "005";   // アラート解除キー
        public const string ADMIN_USER_ID = "006";   // 管理者ユーザー

        // 入荷済みのステータスコード
        public const string CPL_ARR_REP_STTS_CD = "3100";

        // レポートエクセルの格納場所
        public const string EXCEL_DIR_PATH = ".\\Content\\Report\\";

        // レポートエクセルのファイル
        public const string REPORT_CARTON_TAG = "CartonTag.xlsx";
    }
}
