using Microsoft.Data.SqlClient;
using MyInfrastructure;
using System;
using System.Data;

namespace Demo.WorkerService.Helper
{
    public class SqlserverHelper
    {
        public static readonly string Constr = ConfigurationHelper.GetConnectionString("SQLServerCon");
        /// <summary>
        /// 增删改 都可以 就是不能查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ps">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 查询首航首列
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="ps">参数</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    con.Open();//
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 实时查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] ps)
        {
            SqlConnection con = new SqlConnection(Constr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询的是整张表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(string sql, params SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, Constr))
            {
                if (ps != null)
                {
                    sda.SelectCommand.Parameters.AddRange(ps);
                }
                sda.Fill(dt);
            }
            return dt;
        }
    }


}
