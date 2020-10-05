using Dapper;
using ModelClass.auth;
using ModelClass.model.filter;
using ModelClass.model.oracle;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory.imlp;
using Oracle.ManagedDataAccess.Client;
using REPORT.reponsitory.impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.data;

namespace REPORT.service.user.impl
{
    public class UserImpl : PermissionReponsitory<Users>, IUser
    {
        public UserImpl(PermissionContext context) : base(context)
        {
        }

        public bool checkUserExist(string username)
        {
            List<Users> user = getAll().Where(m => m.username == username).ToList();
            if (user.Count > 1)
            {
                return false;
            }
            return true;
        }

        public Users getUserByUserName(string username)
        {
            return getAll().FirstOrDefault(m=>m.username== username);
        }

        public dynamic searchUser(DataFilter dataFilter)
        {
            var filterby = (dataFilter.filter ?? "").Trim().ToLowerInvariant();
            var log = getAll()
                    .AsQueryable()
                    .Where(n =>
                            n.username.ToString().ToLowerInvariant().Contains(filterby)
                        //|| n.email.ToString().ToLowerInvariant().Contains(filterby)
                        //|| n.phone.ToString().ToLowerInvariant().Contains(filterby)
                        || n.usid.ToString().ToLowerInvariant().Contains(filterby)
            );
            return log;
        }

        public dynamic getUserCNTT(RequestUser rq)
        {
            List<OracleUsers> users = new List<OracleUsers>();
            var dyParam = new OracleDynamicParameters();
            /*
             * Truyền biến nếu có
             * gồm tên biến, biến input và output
             */
            dyParam.Add("v_ID_DON_VI", OracleDbType.Int16, ParameterDirection.Input, rq.v_ID_DON_VI);
            dyParam.Add("out_Param", OracleDbType.RefCursor, ParameterDirection.Output);
            string connString = new ConnectionString().returnConnection();
            OracleConnection conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "PKG_CNTT.sp_Get_All_User_CNTT";
                users = SqlMapper.Query<OracleUsers>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<OracleUsers>();
            }
            List<OracleUsers> filterByName = new List<OracleUsers>();
            if (rq.name != null && rq.name.Length > 0)
            {
                var filterby = rq.name.Trim().ToLowerInvariant();
                filterByName = users.AsQueryable().Where(u => u.HO_TEN != null &&
                                    u.HO_TEN.ToString().ToLowerInvariant().Contains(filterby)).ToList();
            }
            else
            {
                filterByName = users;
            }
            return filterByName;
        }
    }
}