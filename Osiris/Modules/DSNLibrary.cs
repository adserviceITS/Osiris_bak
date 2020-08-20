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
        public SqlDataReader Reader { get; set; }

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
        public void ExecSQLRead(string strSql)
        {
            try
            {
                command.CommandText = strSql;
                Reader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Reader.Close();
                transaction.Rollback();
                transaction.Dispose();
                connection.Close();
                connection.Dispose();
                connection = null;

                Debug.WriteLine(strSql.ToString());
                Console.WriteLine(ex.Message);
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
            catch (SqlException ex)
            {
                transaction.Rollback();
                transaction.Dispose();
                connection.Close();
                connection.Dispose();
                connection = null;

                Console.WriteLine(ex.Message);
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
                    Reader.Close();
                    transaction.Commit();
                    transaction.Dispose();
                    connection.Close();
                }
                connection.Dispose();
                connection = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
