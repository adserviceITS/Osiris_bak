using System.Data.SqlClient;
using System.Text;

namespace Osiris.Modules
{
    public class UserInfo
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string EmployeeNumber { get; set; }
        public string BlockID { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string AuthorityKbn { get; set; }

        public UserInfo (string strID)
        {
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT * ");
            stbSql.Append("FROM dbo.M_USER ");
            stbSql.Append("WHERE ID = '" + strID + "' ");

            DSNLibrary dsnLib = new DSNLibrary();
            dsnLib.ExecSQLRead(stbSql.ToString());

            while (dsnLib.Reader.Read())
            {
                ID = dsnLib.Reader["ID"].ToString();
                UserName = dsnLib.Reader["USER_NAME"].ToString();
                EmployeeNumber = dsnLib.Reader["EMPLOYEE_NUMBER"].ToString();
                BlockID = dsnLib.Reader["BLOCK_ID"].ToString();
                Password = dsnLib.Reader["PASS"].ToString();
                Mail = dsnLib.Reader["MAIL"].ToString();
                AuthorityKbn = dsnLib.Reader["AUTHORITY_KBN"].ToString();
            }

            dsnLib.DB_Close();
        }
    }
}
