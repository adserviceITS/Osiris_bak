using System.Data.SqlClient;
using System.Text;

namespace Osiris.Modules
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string DisplayOrder { get; set; }
        public string EffectiveFLG { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }

        public UserInfo (string strID)
        {
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT * ");
            stbSql.Append("FROM dbo.ユーザー ");
            stbSql.Append("WHERE ログイン名 = '" + strID + "' ");

            DSNLibrary dsnLib = new DSNLibrary();
            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());
            sqlRdr.Read();

            UserName = sqlRdr.GetValue<string>("ユーザー");
            DisplayOrder = sqlRdr.GetValue<int>("表示順").ToString();
            EffectiveFLG = sqlRdr.GetValue<string>("有効フラグ");
            ID = sqlRdr.GetValue<string>("ログイン名");
            Password = sqlRdr.GetValue<string>("パスワード");

            sqlRdr.Close();
            dsnLib.DB_Close();
        }
    }
}
