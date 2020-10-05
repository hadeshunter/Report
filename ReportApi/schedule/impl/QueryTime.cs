using Dapper;
using ModelClass.model.oracle;
using ModelClass.model.proccess;
using ModelClass.model.schedule;
using ModelClassLibrary.connection;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Quartz;
using REPORT.service.process;
using REPORT.service.process.impl;
using REPORT.service.sms;
using REPORT.service.sms.impl;
using System;
using System.Data;
using System.Linq;
using System.Web;
using WebApi.data;
using ModelClass.model.sms;
using System.Collections.Generic;

namespace REPORT.schedule.impl
{
    public class QueryTime : IJob
    {
        const int time_not_end = 1;
        const int timeend = 2;
        const int  time1st = 20;
        const int time2nd = 30;
        int timeksyc = 30;
        int timetkhd = 960;
        int timedwst = 120;

        public void Execute(IJobExecutionContext context)
        {
            var x = getData();
            //getTimeQuery();
            throw new NotImplementedException();
        }
        //get time query
        //private void getTimeQuery()
        //{
        //    ITimer m_timer = new TimerImpl(new DataContext());
        //    System.Collections.Generic.List<Timer> t = m_timer.getAll();

        //}


        public object getData()
        {
            IEnumerable<MainProcess> main = null;
            List<MainProcess> list = new List<MainProcess>();
            try
            {

                var dyParam = new OracleDynamicParameters();

                dyParam.Add("v_ID_NHOM_GIAI_DOAN", OracleDbType.Int32, ParameterDirection.Input, 18);
                dyParam.Add("v_YEAR", OracleDbType.Int32, ParameterDirection.Input, 2020);
                dyParam.Add("v_MONTH", OracleDbType.Int32, ParameterDirection.Input, 8);

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
                    main = SqlMapper.Query<MainProcess>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<MainProcess>();
                }
                /*
                 * send sms
                 */
                getLIstCustomer(main);
                getListCustomerUnSuccess(main);
            }
            catch (Exception e)
            {
                return e;
            }
            return main;
        }

        private int stadardTime(int timeint, int timeout)
        {
            if (timeint <= timeout)
            {
                return 1;// dung han
            }
            else return 2;//tre han
        }

        public void getLIstCustomer(IEnumerable<MainProcess> listpr)
        {
            List<Process> listcustomer = new List<Process>();

            if (listpr == null)
            {
                return;
            }
            foreach (var process in listpr)
            {
                timeksyc = process.SLA_KSYC;
                timetkhd = process.SLA_TKHD;
                timedwst = process.SLA_DWST;
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_KSYC_CHITIET), timeksyc) == time_not_end)
                {
                    if (Int32.Parse(process.THOI_GIAN_SLA_KSYC_CHITIET) - process.SLA_KSYC > time1st
                        && Int32.Parse(process.THOI_GIAN_SLA_KSYC_CHITIET) - process.SLA_KSYC < time2nd
                        && checksendSms(process.ID_YEU_CAU, 1))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 1);
                    }
                }
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_TKHD_CHITIET), timetkhd) == time_not_end)
                {
                    if (Int32.Parse(process.THOI_GIAN_SLA_TKHD_CHITIET) - process.SLA_TKHD > time1st
                        && Int32.Parse(process.THOI_GIAN_SLA_TKHD_CHITIET) - process.SLA_TKHD < time2nd
                        && checksendSms(process.ID_YEU_CAU, 1))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 1);
                    }
                }
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_DWST_CHITIET), timedwst) == time_not_end)
                {
                    if (Int32.Parse(process.THOI_GIAN_SLA_DWST_CHITIET) - process.SLA_DWST > time1st
                        && Int32.Parse(process.THOI_GIAN_SLA_DWST_CHITIET) - process.SLA_DWST < time2nd
                        && checksendSms(process.ID_YEU_CAU, 1))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 1);
                    }
                }
            }
            //send sms
            if (listcustomer.Count() > 0)
            {
                var str = sendSms(listcustomer,0);
            }
        }
        /*
         * Checkd sended sms 
         */
        public Boolean checksendSms(int idyc, int stt)
        {
            IProcessReport m_process = new ProcessImpl(new DataContext());
            var pro = m_process.getAll().FirstOrDefault(m => m.id_yeu_cau == idyc);
            if (stt == 1)
            {
                if (pro != null && pro.sendth == 1)
                {
                    return false;
                }
            }
            else
            {
                if (pro != null && pro.sendth == 2)
                {
                    return false;
                }
            }

            return true;
        }
        /*
         * save id_yc send sms
         * stt la lan guiw thu stt
         */
        public void saveSms(int idyc, int stt)
        {

            IProcessReport m_process = new ProcessImpl(new DataContext());
            //check exist
            var pr = m_process.getAll().FirstOrDefault(m => m.id_yeu_cau == idyc);
            if (pr == null)
            {
                Process prinsert = new Process();
                prinsert.id_yeu_cau = idyc;
                prinsert.sendth = stt;
                prinsert.active = true;
                prinsert.createday = DateTime.Now;
                prinsert.updateday = DateTime.Now;
                prinsert.status = 0;// success
                m_process.insert(prinsert);
            }
            else
            {
                if (stt == 2)
                {
                    pr.sendth = 2;
                    m_process.update(pr);
                }
            }
        }

        /*
         * save phone sended 
         */
        public void savePhoneSended(List<PhoneSended> lists)
        {
            IPhoneSended m_phone= new PhoneSendedImpl(new DataContext());
            foreach (var phone in lists)
            {
                m_phone.insert(phone);
            }
        }

        /*
         * qua han chua hoan thanh
         */
        public void getListCustomerUnSuccess(IEnumerable<MainProcess> listpr)
        {
            List<Process> listcustomer = new List<Process>();

            if (listpr == null)
            {
                return;
            }
            foreach (var process in listpr)
            {
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_KSYC_CHITIET), timeksyc) == timeend)
                {
                    if (Int32.Parse(process.THOI_GIAN_SLA_KSYC_CHITIET) - process.SLA_KSYC > time2nd
                        && checksendSms(process.ID_YEU_CAU, 2))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 2);
                    }
                }
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_TKHD_CHITIET), timetkhd) == timeend)
                {
                    if (Int32.Parse(process.THOI_GIAN_SLA_TKHD_CHITIET) - process.SLA_TKHD > time2nd
                        && checksendSms(process.ID_YEU_CAU, 2))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 2);
                    }
                }
                if (stadardTime(Int32.Parse(process.THOI_GIAN_SLA_DWST_CHITIET), timedwst) == timeend)
                {
                    if ( Int32.Parse(process.THOI_GIAN_SLA_DWST_CHITIET) - process.SLA_DWST > time2nd
                        && checksendSms(process.ID_YEU_CAU, 2))
                    {
                        Process pro = new Process();
                        pro.id_yeu_cau = process.ID_YEU_CAU;
                        listcustomer.Add(pro);
                        saveSms(process.ID_YEU_CAU, 2);
                    }
                }
            }
            //send sms
            if (listcustomer.Count() > 0)
            {
                var str = sendSms(listcustomer,1);
            }
        }


        /*
         * call server send sms
         */
        public string sendSms(List<Process> listcustomer,int type)
        {

            ISms m_sms = new SmsImpl(new DataContext());
            IPhoneSended m_phone = new PhoneSendedImpl(new DataContext());

            foreach( var sms in m_sms.getAll())
            {
                foreach (var i in listcustomer)
                {
                    ModelClass.WS_800126.AuthHeader auth = new ModelClass.WS_800126.AuthHeader();
                    auth.Username = "hampn";
                    auth.Password = "vnpt@20190723";

                    string content = "";
                    switch (type)
                    {
                        case 0:
                            content = "Yeu cau khac hang " + i.id_yeu_cau + " sap toi gio hen vui long thuc hien.";
                            break;
                        case 1:
                            content = "Yeu cau khac hang " + i.id_yeu_cau + " da tre hen.";
                            break;
                    }
                    string phonenumber = sms.phone;
                    ModelClass.WS_800126.Service1 smsend = new ModelClass.WS_800126.Service1();
                    smsend.AuthHeaderValue = auth;
                    var str = smsend.sendsms(phonenumber, content);

                    //save 
                    PhoneSended sended = new PhoneSended();
                    sended.processid = i.processid;
                    sended.timer = type == 0 ? time1st : time2nd;
                    sended.createday = DateTime.Now;
                    sended.updateday = DateTime.Now;
                    sended.phone = sms.phone;
                    sended.content = content;
                    sended.result = str;
                    sended.status = 0;//success
                    m_phone.insert(sended); 
                }
            }
            return null;
        }
    }

}