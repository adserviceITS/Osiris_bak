using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Osiris.Modules
{
    public class DSNLibrary
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlTransaction transaction;

        // コンストラクタ
        public DSNLibrary()
        {
            connection = new SqlConnection(ConstDef.DB_CONNECTION_STRING);
            connection.Open();

            command = connection.CreateCommand();

            // トランザクションスタート
            transaction = connection.BeginTransaction();

            command.Connection = connection;
            command.Transaction = transaction;
        }

        // SQL実行 Select
        public SqlDataReader ExecSQLRead(string strSql)
        {
            try
            {
                command.CommandText = strSql;

                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception exception)
            {
                DB_Close(exception);
                throw;
            }
        }

        // SQL実行 INSERT UPDATE DELETE
        public void ExecSQLUpdate(string strSql)
        {
            try
            {
                command.CommandText = strSql;
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                DB_Close(exception);
                throw;
            }
        }

        // DB切断
        public void DB_Close()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    transaction.Commit();
                    transaction.Dispose();
                    connection.Close();
                }
                connection.Dispose();
                connection = null;

            }
            catch (Exception exception)
            {
                DB_Close(exception);
                throw;
            }
        }

        // DB切断エラー時
        public void DB_Close(Exception exception)
        {
            transaction.Rollback();
            transaction.Dispose();
            command.Dispose();
            connection.Close();
            connection.Dispose();
            connection = null;

            Console.WriteLine(exception.Message);
        }

        // シングルコーテーションエスケープ Utility
        public string SingleEscape(string param)
        {
            if (!string.IsNullOrEmpty(param)) param = param.Replace("'", "''");

            return param;
        }
    }
}
