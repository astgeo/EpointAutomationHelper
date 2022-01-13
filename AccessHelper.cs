using System;
using System.Data;
using System.Data.OleDb;

namespace EpointAutomationHelper
{
	/// <summary>
	/// Access数据库工具类
	/// </summary>
	public class AccessHelper
	{
        /// <summary>
        ///  连接器
        /// </summary>
        protected static OleDbConnection conn = new OleDbConnection();

        /// <summary>
        /// 命令
        /// </summary>
        protected static OleDbCommand comm = new OleDbCommand();

        /// <summary>
        /// 无参构造函数
        /// </summary>
		public AccessHelper()
		{
		}

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="filePath">数据库文件绝对路径</param>
        private static void OpenConnection(string filePath)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath;
                Ranorex.Report.Info("ConnectionString=" + conn.ConnectionString);
                comm.Connection = conn;
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                { 
                    throw new Exception(e.Message); 
                }
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        private static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                comm.Dispose();
            }
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="filePath">数据库文件绝对路径</param>
        public static void ExcuteSql(string sqlstr, string filePath)
        {
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            { CloseConnection(); }
        }
        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象，使用时请注意关闭这个对象。
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        /// <returns></returns>
        private static OleDbDataReader DataReader(string sqlstr,string filePath)
        {
            OleDbDataReader dr = null;
            try
            {
                OpenConnection(filePath);
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;

                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    CloseConnection();
                }
                catch { }
            }
            return dr;
        }
        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象,使用时请注意关闭
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dr"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        private static void DataReader(string sqlstr, ref OleDbDataReader dr, string filePath)
        {
            try
            {
                OpenConnection(filePath);
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    if (dr != null && !dr.IsClosed)
                        dr.Close();
                }
                catch
                {
                }
                finally
                {
                    CloseConnection();
                }
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        /// <returns></returns>
        private static DataSet DataSet(string sqlstr, string filePath)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="ds"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        private static void DataSet(string sqlstr, ref DataSet ds, string filePath)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        /// <returns></returns>
        private static DataTable DataTable(string sqlstr, string filePath)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dt"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        private static void DataTable(string sqlstr, ref DataTable dt, string filePath)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataview
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="filePath">数据库文件绝对路径</param>
        /// <returns></returns>
        private static DataView DataView(string sqlstr, string filePath)
        {
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            try
            {
                OpenConnection(filePath);
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dv;
        }
	}
}
