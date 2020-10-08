using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Osiris.Modules
{
    public static class ExtensionMethods
    {
        // DBNULL ⇒ NULL 変換 と 指定型へのキャスト
        public static T GetValue<T>(this SqlDataReader sqlRdr, string columnName)
        {
            var value = sqlRdr[columnName];

            if (value != DBNull.Value)
            {
                if (typeof(T) == typeof(string))
                {
                    T strT = (T)(object)value.ToString();
                    return strT;
                }

                if (typeof(T) == typeof(int))
                {
                    T intT = (T)(object)Convert.ToInt32(value);
                    return intT;
                }
                return (T)value;
            }

            return default(T);
        }

        // シングルコーテーションエスケープ Utility
        public static string SingleEscape(this string param)
        {
            if (!string.IsNullOrEmpty(param)) param = param.Replace("'", "''");

            return param;
        }

        // NULL変換 string
        public static Object IsNUll(this Object param, Object objIsNull, Object objIsNotNull)
        {
            if (param == null)
            {
                return objIsNull;
            } else
            {
                return objIsNotNull;
            }
        }

        // 定数マスタからの単純な値取得
        public static List<ConstValue> GetConstValue(string GroupCd)
        {
            DSNLibrary dsnLib = new DSNLibrary();
            StringBuilder stbSql = new StringBuilder();
            List<ConstValue> constList = new List<ConstValue>();

            stbSql.Append("SELECT * FROM M_CONST ");
            stbSql.Append("WHERE ");
            stbSql.Append("   GROUP_CD = '" + GroupCd + "' AND ");
            stbSql.Append("   DEL_FLG = '0' ");
            stbSql.Append("ORDER BY SORT_ORDER ");

            SqlDataReader sqlRdr = dsnLib.ExecSQLRead(stbSql.ToString());

            while (sqlRdr.Read())
            {
                constList.Add(new ConstValue
                {
                    ConstCd = sqlRdr.GetValue<string>("CONST_CD"),
                    Value = sqlRdr.GetValue<string>("VALUE"),
                });
            }

            sqlRdr.Close();
            dsnLib.DB_Close();

            return constList;
        }
    }

    public class ConstValue
    {
        public string ConstCd { get; set; }
        public string Value { get; set; }
    }
}
