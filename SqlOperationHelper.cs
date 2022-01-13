using MySql.Data.MySqlClient;

using Ranorex;

namespace EpointAutomationHelper
{
    /// <summary>
    /// Sql工具类
    /// </summary>
    public class SqlOperationHelper
    {
        private string _IP;
        private string _Port;
        private string _UserName;
        private string _Password;
        private string _DataBase;

        /// <summary>
        /// Sql操作
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="dataBase">数据库名称</param>
        public SqlOperationHelper(string ip, string port, string userName, string passWord, string dataBase)
        {
            this._IP = ip; //数据库IP地址
            this._Port = port; //数据库端口号
            this._UserName = userName; //数据库登录用户名
            this._Password = passWord; //数据库登录密码
            this._DataBase = dataBase; //数据库名称
        }

        /// <summary>
        /// 连接数据库并进行相关操作
        /// </summary>
        /// <param name="sqlOp">Sql语句</param>
        public void SqlOperation(string sqlOp)
        {
            string connectStr = string.Format("Server={0};port={1};User={2};Password={3}; Database={4};", this._IP, this._Port, this._UserName, this._Password, this._DataBase);
            MySqlConnection con = new MySqlConnection(connectStr);
            Report.Info("建立连接.....");
            con.Open();
            Report.Info("已经建立连接");
            MySqlCommand cmd = new MySqlCommand(sqlOp, con);
            cmd.ExecuteNonQuery();
            Report.Info("关闭连接.....");
            con.Close();
        }

    }
}
