using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
    }
}
