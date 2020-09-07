using System;
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

        // 1画面に表示する行数
        public const int PAGE_ROW_SIZE = 300;
    }
}
