using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mymeswpf.COMMON
{
    public class OracleHelper
    {
        private static OracleHelper instance;
        public static OracleConnection oracleconnection;
        private static object lockHelper = new object();
        private static string EqpStatusConnectionString = "User ID=FWASSY;Password=FWASSY;Data Source=KS_QAS_AY;";
        public OracleHelper()
        {
            GetOpenConnection();

        }
        /// <summary>
        /// 单件模式，防止多线程-同步实例化
        /// </summary>
        /// <returns></returns>
        public static OracleHelper InstanceSingleTon()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new OracleHelper();
                    }
                }
            }

            if (oracleconnection.State == ConnectionState.Closed) { oracleconnection.Open(); }

            return instance;
        }
        private static void GetOpenConnection()
        {
            try
            {
                oracleconnection = new OracleConnection(EqpStatusConnectionString);
                oracleconnection.Open();
            }
            catch (Exception e)
            {
                //Log.Logger.Error(e.Message);
            }
        }

    }

}
