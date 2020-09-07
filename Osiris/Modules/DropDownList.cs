using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Osiris.Modules
{
    public class DropDownList
    {
        // 販路
        public List<CombVendor> GetDropDownListVendor()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    販売店マスタ.ID, ");
            stbSql.Append("    販売店マスタ.販売店名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    販売店マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    販売店マスタ.削除フラグ <> '1' ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    販売店マスタ.序列 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            List<CombVendor> DropDownListVendor = new List<CombVendor>();

            while (sqlRdr.Read())
            {
                DropDownListVendor.Add(new CombVendor
                {
                    VendorID = sqlRdr["ID"].ToString(),
                    VendorName = sqlRdr["販売店名"].ToString()
                });
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return DropDownListVendor;
        }

        // 代理店
        public List<CombDistributor> GetDropDownListDistributor()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    代理店マスタ.ID, ");
            stbSql.Append("    代理店マスタ.代理店名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    代理店マスタ ");
            stbSql.Append("WHERE ");
            stbSql.Append("    代理店マスタ.削除フラグ <> '1' ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    代理店マスタ.序列 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            List<CombDistributor> DropDownListDistributor = new List<CombDistributor>();

            while (sqlRdr.Read())
            {
                DropDownListDistributor.Add(new CombDistributor
                {
                    DistributorID = sqlRdr["ID"].ToString(),
                    DistributorName = sqlRdr["代理店名"].ToString()
                });
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return DropDownListDistributor;
        }
    }

    public class CombVendor
    {
        public string VendorID { get; set; }
        public string VendorName { get; set; }
    }

    public class CombDistributor
    {
        public string DistributorID { get; set; }
        public string DistributorName { get; set; }
    }
}
