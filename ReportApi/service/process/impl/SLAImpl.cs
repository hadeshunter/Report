using Dapper;
using Microsoft.Extensions.Configuration;
using ModelClass.model.oracle;
using ModelClass.model.proccess;
using ModelClassLibrary.connection;
using Oracle.ManagedDataAccess.Client;
using REPORT.reponsitory.impl;
using REPORT.service.process;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.data;

namespace REPORT.service.impl
{
    public class SLAImpl : Reponsitory<SLAData>, ISLA
    {

        private IConfiguration m_configuration;
        string table = "cau_hinh_sla_ttcntt";
        public SLAImpl(DataContext context) : base(context)
        {
        }
        public void commitFunction()
        {
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (conn.State == ConnectionState.Open)
                {
                    var query = "commit";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
                }

            }
            catch (Exception e)
            {
            }
        }
        public IDbConnection GetConnection()
        {
            var connectionString = m_configuration.GetSection("configuration").GetSection("connectionStrings").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }
        private dynamic execureStoreDB(string source)
        {
            object result = null;
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("vstr", OracleDbType.Varchar2, ParameterDirection.Input, source);

            var conn = GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            if (conn.State == ConnectionState.Open)
            {
                var storename = "HDDT_BK.cau_hinh_sla_ttcntt.execureQuery";
                result = SqlMapper.Query(conn, storename, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }
        public dynamic createSLA(SLAData data)
        {
            var query = "insert into " + table + "" +
                 "ID_NHOM_GIAI_DOAN" +
                  ",ID_GIAI_DOAN" +
                  ",ID_DO_UU_TIEN" +
                  ",THOI_GIAN" +
                 "',NGAY_TAO = " + (data.NGAY_TAO != null ? "" + "to_date('" + data.NGAY_TAO + "','dd/MM/yyyy')" : "null") +
                  ",NGUOI_TAO" +
                  ",NGAY_CAP_NHAT = " + (data.NGAY_CAP_NHAT != null ? "" + "to_date('" + data.NGAY_CAP_NHAT + "','dd/MM/yyyy')" : "null") +
                  ",NGUOI_CAP_NHAT" +
                  ",TTHD" +
                  ",NGAY_KT" + (data.NGAY_KT != null ? "" + "to_date('" + data.NGAY_KT + "','dd/MM/yyyy')" : "null") +
                  ",THEO_VB" +
                  "values (" +
                  data.ID_NHOM_GIAI_DOAN + "," +
                  data.ID_GIAI_DOAN + "," +
                  data.ID_DO_UU_TIEN + "," +
                  data.THOI_GIAN + "," +
                  (data.NGAY_TAO != null ? "" + "to_date('" + data.NGAY_TAO + "','dd-MM-yyyy H:mm')" : "null") + "," +
                  data.NGUOI_TAO + "," +
                  (data.NGAY_CAP_NHAT != null ? "" + data.NGAY_CAP_NHAT + "(','dd-MM-yyyy H:mm')" : "null") + "," +
                  data.NGUOI_CAP_NHAT + "," +
                  data.TTHD + "," +
                  (data.NGAY_KT != null ? "" + data.NGAY_KT + "(','dd-MM-yyyy H:mm')" : "null") + "," +
                  "'" + (data.THEO_VB != null? "" + data.THEO_VB : "null") + "'"
                  + ")";
            return execureStoreDB(query);
        }

        public dynamic getAllSLAFromOracle()
        {
            IEnumerable<dynamic> result = null;
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            var dyParam = new OracleDynamicParameters();
            if (conn.State == ConnectionState.Open)
            {
                var query = "select * from cau_hinh_sla_ttcntt";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
            }
            return result;
        }


        public dynamic updateSla(SLAData data)
        {
            var query = "update " + table + " set " +
                 "ID_NHOM_GIAI_DOAN = " + data.ID_NHOM_GIAI_DOAN +
                 ",ID_GIAI_DOAN = " + data.ID_GIAI_DOAN +
                 ",ID_DO_UU_TIEN=" + data.ID_DO_UU_TIEN +
                 ",NGUOI_TAO = " + data.NGUOI_TAO +
                 ",THOI_GIAN = " + data.THOI_GIAN +
                 ",NGAY_TAO = to_date('" + data.NGAY_TAO + "','yyyy/mm/dd')" +
                 ",NGAY_CAP_NHAT = to_date('" + data.NGAY_CAP_NHAT + "','yyyy/mm/dd')" +
                 ",NGAY_KT = to_date('" + data.NGAY_KT + "','yyyy/mm/dd')" +
                 ",NGUOI_CAP_NHAT=" + data.NGUOI_CAP_NHAT +
                 ",TTHD=" + data.TTHD +
                 ",THEO_VB='" + data.THEO_VB +
                 "' where ID = " + data.ID + "";
            object result = null;
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("vstr", OracleDbType.Varchar2, ParameterDirection.Input, query);

            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            if (conn.State == ConnectionState.Open)
            {
                var storename = "HDDT_BK.PKG_CNTT.execureQuery";
                result = SqlMapper.Query(conn, storename, param: dyParam, commandType: CommandType.StoredProcedure);
            }
            return result;
        }
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
                    var query = "select * from cau_hinh_sla_ttcntt";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
                }
            }
            catch (Exception e)
            {
                result = e;
            }

            return result;
        }

        public dynamic execureQuery(string query)
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
                    // var query = "select * from cau_hinh_sla_ttcntt";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.Text);
                }
            }
            catch (Exception e)
            {
                result = e;
            }

            return result;
        }

        public dynamic GetByIdSla(int id)
        {
            var query = "select * from " + table + " where id=" + id + "";
            return execureQuery(query);
        }

        public dynamic deleteSla(int id)
        {
            var query = "delete from " + table + " where id= " + id + "";
            return execureQuery(query);
        }
    }
}