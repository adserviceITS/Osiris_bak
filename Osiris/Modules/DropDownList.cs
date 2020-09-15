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
                    VendorID = sqlRdr.GetValue<string>("ID"),
                    VendorName = sqlRdr.GetValue<string>("販売店名")
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
                    DistributorID = sqlRdr.GetValue<string>("ID"),
                    DistributorName = sqlRdr.GetValue<string>("代理店名")
                });
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return DropDownListDistributor;
        }

        // 商品
        public List<CombProduct> GetDropDownListProduct()
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    商品名マスタ.ID, ");
            stbSql.Append("    商品名マスタ.商品名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    商品名マスタ ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    商品名マスタ.表示順 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            List<CombProduct> DropDownListProduct = new List<CombProduct>();

            while (sqlRdr.Read())
            {
                DropDownListProduct.Add(new CombProduct
                {
                    ProductID = sqlRdr.GetValue<string>("ID"),
                    ProductName = sqlRdr.GetValue<string>("商品名")
                });
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return DropDownListProduct;
        }

        // 製品型番
        public List<CombModel> GetDropDownListModel(string strProductID)
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();

            stbSql.Append("SELECT ");
            stbSql.Append("    型名マスタ.型名番号, ");
            stbSql.Append("    型名マスタ.型名 ");
            stbSql.Append("FROM ");
            stbSql.Append("    型名マスタ INNER JOIN 商品名マスタ ON ");
            stbSql.Append("    型名マスタ.商品名 = 商品名マスタ.商品名 ");
            stbSql.Append("WHERE ");
            stbSql.Append("    型名マスタ.削除フラグ <> '1' AND ");
            stbSql.Append("    商品名マスタ.ID = '" + strProductID + "' ");
            stbSql.Append("ORDER BY ");
            stbSql.Append("    型名マスタ.序列 ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            List<CombModel> DropDownListModel = new List<CombModel>();

            while (sqlRdr.Read())
            {
                DropDownListModel.Add(new CombModel
                {
                    ModelNumber = sqlRdr.GetValue<string>("型名番号"),
                    ModelName = sqlRdr.GetValue<string>("型名")
                });
            }
            sqlRdr.Close();
            dsnLib.DB_Close();

            return DropDownListModel;
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

    public class CombProduct
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
    }

    public class CombModel
    {
        public string ModelNumber { get; set; }
        public string ModelName { get; set; }
    }
}
