using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
namespace Keylab.DAL {
    public class DataAccessLayer {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["LintulKeylab"].ToString();

        /// <summary>
        /// QueryScalar 返回第一行的第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>返回T类型的数据</returns>
        public T QueryScalar<T>(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                return conn.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// QueryScalar 返回第一行的第一列
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>object</returns>
        public object QueryScalar(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                return conn.ExecuteScalar(sql, param);
            }
        }

        /// <summary>
        /// QuerySet 返回结果集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>返回T类型的数据集合</returns>
        public IEnumerable<T> QuerySet<T>(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                return conn.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// QuerySet 返回结果集合
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>返回动态类型的数据集合</returns>
        public IEnumerable<dynamic> QuerySet(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                return conn.Query(sql, param);
            }
        }

        /// <summary>
        /// ExecAffect 返回受影响行数
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>int</returns>
        public int ExecAffect(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                conn.Open();
                IDbTransaction trans = conn.BeginTransaction();
                try {
                    int affect = conn.Execute(sql, param, trans);
                    trans.Commit();
                    return affect;
                } catch (Exception ex) {
                    Logs("执行Sql==>"+sql, ex);
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// ExecAffect 返回bool
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="param">执行参数(可选)</param>
        /// <returns>bool</returns>
        public bool ExecBool(string sql, object param = null) {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                conn.Open();
                IDbTransaction trans = conn.BeginTransaction();
                try {
                    int affect = conn.Execute(sql, param, trans);
                    trans.Commit();
                    return affect > 0;
                } catch (Exception ex) {
                    trans.Rollback();
                    Logs("执行Sql==>" + sql, ex);
                    return false;
                }
            }
        }
        public static void Logs(string msg, Exception ex) {
            msg += ex.Message + ".\nException\t:" + ex.StackTrace;
            if (ex.InnerException != null) {
                msg += ex.InnerException.Message + ";\nInnerException\t:" + ex.InnerException.StackTrace;
            }
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SqlErr\");
            if (!Directory.Exists(logDir)) {
                Directory.CreateDirectory(logDir);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(logDir + fileName, true)) {
                sw.WriteLine("====================================================== BEGIN ======================================================");
                sw.WriteLine("\n\nTime\t:" + DateTime.Now.ToString());
                sw.WriteLine("Message\t:" + msg);
                sw.WriteLine("====================================================== END ======================================================");
            }
        }
    }
}
