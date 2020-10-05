using Dapper;
using ModelClass.model.holiday;
using ModelClass.model.oracle;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using WebApi.data;

namespace REPORT.service.holiday.impl
{
    public class HolidayImpl : IHoliday
    {
        public dynamic getListHoliday(int v_YEAR)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, v_YEAR);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_CNTT.sp_GET_NGAY_NGHI";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertByDate(int v_DATE)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_DATE", OracleDbType.Int16, ParameterDirection.Input, v_DATE);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_CNTT.INSERT_NGAY_NGHI";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertHoliday(int v_YEAR)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, v_YEAR);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_CNTT.INSERT_NGAY_NGHI_NAM";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic deleteDate(int v_DATE)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_DATE", OracleDbType.Int16, ParameterDirection.Input, v_DATE);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_CNTT.DELETE_NGAY_NGHI";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

    }
}