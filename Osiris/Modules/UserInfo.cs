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

            UserName = sqlRdr["ユーザー"].ToString();
            DisplayOrder = sqlRdr["表示順"].ToString();
            EffectiveFLG = sqlRdr["有効フラグ"].ToString();
            ID = sqlRdr["ログイン名"].ToString();
            Password = sqlRdr["パスワード"].ToString();

            sqlRdr.Close();
            dsnLib.DB_Close();
        }
    }
}
