using Dapper;
using ModelClass.auth;
using ModelClass.model.oracle;
using ModelClass.model.report.request;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApi.data;

namespace REPORT.service.report.impl
{
    public class ReportImpl : IReport
    {
        public ReportImpl()
        {
        }

        public dynamic TestSql()
        {
            //DataContext db = new DataContext();
            //Users us = db.Users.FirstOrDefault();
            //return us;
            return null;
        }
        /*
         * execure querery
         */
        public dynamic execureQuery()
        {
            object result = null;
            try
            {
                string connString = new ConnectionString().returnConnection();
                OracleConnection conn = new OracleConnection(connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var dyParam = new OracleDynamicParameters();
                if (conn.State == ConnectionState.Open)
                {
                    var query = "select * from queue_privileges";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
                }
            }
            catch (Exception e)
            {
                result = e;
            }

            return result;
        }

        /*
        * execure store procedures
        */
        public dynamic execureStoreProcedure(int test)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */

            //dyParam.Add("v_test", OracleDbType.Int32, ParameterDirection.Input, test);
            //dyParam.Add("name variable", OracleDbType.Varchar2, ParameterDirection.Input, "data of variable ");

            dyParam.Add("out_param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_BANG.TestByKhanhIT";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic getReportDasboard(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();

            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_FROM_DATE", OracleDbType.Date, ParameterDirection.Input, rq.v_FROM_DATE);
            dyParam.Add("v_TO_DATE", OracleDbType.Date, ParameterDirection.Input, rq.v_TO_DATE);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {

                var query = "PKG_CNTT.sp_GetReportDashboard";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic getReportTongYeuCau(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_FROM_DATE", OracleDbType.Date, ParameterDirection.Input, rq.v_FROM_DATE);
            dyParam.Add("v_TO_DATE", OracleDbType.Date, ParameterDirection.Input, rq.v_TO_DATE);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_GetReportTongYeuCau";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic getListReportSLA(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, rq.v_YEAR);
            dyParam.Add("v_MONTH", OracleDbType.Int16, ParameterDirection.Input, rq.v_MONTH);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Report_SLA";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertSLADetail1st(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, rq.v_YEAR);
            dyParam.Add("v_MONTH", OracleDbType.Int16, ParameterDirection.Input, rq.v_MONTH);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_KSYC";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertSLADetail2nd(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, rq.v_YEAR);
            dyParam.Add("v_MONTH", OracleDbType.Int16, ParameterDirection.Input, rq.v_MONTH);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_TKHD";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertSLADetail3rd(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, rq.v_YEAR);
            dyParam.Add("v_MONTH", OracleDbType.Int16, ParameterDirection.Input, rq.v_MONTH);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_DWST";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic filterByGD_DG(ReportRequest rq)
        {
            List<ReportResult> result = new List<ReportResult>();
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_YEAR", OracleDbType.Int16, ParameterDirection.Input, rq.v_YEAR);
            dyParam.Add("v_MONTH", OracleDbType.Int16, ParameterDirection.Input, rq.v_MONTH);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("v_DANH_GIA", OracleDbType.Int16, ParameterDirection.Input, rq.v_DANH_GIA);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<ReportResult> temp = new List<ReportResult>();
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Report_SLA_Condition";
                result = SqlMapper.Query<ReportResult>(conn, query, param: dyParam,
                    commandType: CommandType.StoredProcedure).AsList<ReportResult>();
                if (rq.v_TEN_NV != null && rq.v_TEN_NV.Length > 0)
                {
                    var filterby = rq.v_TEN_NV.Trim().ToLowerInvariant();
                    if (rq.v_ID_GIAI_DOAN == 6)
                    {
                        temp = result
                            .AsQueryable()
                            .Where(n => n.NGUOI_TIEP_NHAN_KSYC != null &&
                                        n.NGUOI_TIEP_NHAN_KSYC.ToString().ToLowerInvariant().Contains(filterby)).ToList();
                    }
                    else if (rq.v_ID_GIAI_DOAN == 7)
                    {
                        temp = result
                            .AsQueryable()
                            .Where(n => n.NGUOI_TIEP_NHAN_TKHD != null &&
                                        n.NGUOI_TIEP_NHAN_TKHD.ToString().ToLowerInvariant().Contains(filterby)).ToList();
                    }
                    else if (rq.v_ID_GIAI_DOAN == 8)
                    {
                        temp = result
                            .AsQueryable()
                            .Where(n => n.NGUOI_TIEP_NHAN_DWST != null &&
                                        n.NGUOI_TIEP_NHAN_DWST.ToString().ToLowerInvariant().Contains(filterby)).ToList();
                    }
                }
                else
                {
                    temp = result;
                }
            }
            return temp;
        }
        public dynamic getThongTinYeuCau(int v_ID_YEU_CAU)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_YEU_CAU", OracleDbType.Int16, ParameterDirection.Input, v_ID_YEU_CAU);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_GetChiTietYeuCau";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByTKH1(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_TEN_KHACH_HANG", OracleDbType.NVarchar2, ParameterDirection.Input, rq.v_TEN_KHACH_HANG);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_TKH_1";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByTKH2(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_TEN_KHACH_HANG", OracleDbType.NVarchar2, ParameterDirection.Input, rq.v_TEN_KHACH_HANG);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_TKH_2";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByTKH3(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_TEN_KHACH_HANG", OracleDbType.NVarchar2, ParameterDirection.Input, rq.v_TEN_KHACH_HANG);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_TKH_3";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic getFilterByTKH(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_TEN_KHACH_HANG", OracleDbType.NVarchar2, ParameterDirection.Input, rq.v_TEN_KHACH_HANG);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Report_SLA_By_TKH";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByID1(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_ID_YEU_CAU", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_YEU_CAU);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_ID_1";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByID2(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_ID_YEU_CAU", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_YEU_CAU);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_ID_2";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic insertFilterByID3(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_ID_YEU_CAU", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_YEU_CAU);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.INSERT_SLA_BY_ID_3";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public dynamic getFilterByID(ReportRequest rq)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_ID_YEU_CAU", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_YEU_CAU);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Report_SLA_By_ID";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }
        public dynamic getWarningList(ReportRequest rq)
        {
            List<WarningList> result = new List<WarningList>();
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int64, ParameterDirection.Input, rq.v_ID_NHOM_GIAI_DOAN);
            dyParam.Add("v_ID_GIAI_DOAN", OracleDbType.Int64, ParameterDirection.Input, rq.v_ID_GIAI_DOAN);
            dyParam.Add("v_TIME_WARNING", OracleDbType.Int64, ParameterDirection.Input, rq.v_TIME_WARNING);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Report_SLA_Warning";
                result = SqlMapper.Query<WarningList>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<WarningList>();
            }
            List<WarningList> result1 = new List<WarningList>();
            if (rq.v_TEN_NV != null && rq.v_TEN_NV.Length > 0)
            {
                var filterby = rq.v_TEN_NV.Trim().ToLowerInvariant();
                result1 = result
                        .AsQueryable()
                        .Where(n => n.NGUOI_TIEP_NHAN != null &&
                                    n.NGUOI_TIEP_NHAN.ToString().ToLowerInvariant().Contains(filterby)).ToList();
            }
            else
            {
                result1 = result;
            }
            return result1;
        }
    }
}