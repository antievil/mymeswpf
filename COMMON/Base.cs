using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Dapper;
using System.Threading;
using System.Windows;

namespace mymeswpf.COMMON
{
    class Base
    {
        static string userid;
        private static OracleHelper ora;// = OracleHelper.InstanceSingleTon();
        public static OracleConnection oraConnection;// = OracleHelper.oracleconnection;
        private static readonly Base instance = new Base();

        private Base()
        {
        }

        public static Base GetInstance()
        {
            return instance;
        }

        public static DataTable getLotList() {


            string sSQL = @"select APPID, PROCESSINGSTATUS, 
                           HOLDSTATE, LASTLOCATION, PRODUCTNAME, 
                           PRODUCTREVISION, CONSTRUCTTYPE, LASTTRANSACTIONTIME, 
                           FUTUREACTIONS, PLANNAME, PLANREVISION, 
                           LASTSTEPID, TRACKOUTTIME, CAPACITYQTY, 
                           COMPONENTQTY, STARTCOMPONENTQTY, 
                           SUBCOMPTOTALQTY, COMPONENTUNITS, UNITTRACE, 
                           LOTTYPE, NUMCHILDREN, STARTDATE, 
                           STARTTIME, DUEDATE, SCHEDULEDCOMPLETEDATE, 
                           ORDERNUMBER, PRIORITY, PROCESSSPEED, 
                            VENDORID
                            from FWLOT where appid like 'CHID12N0N7%'";
            //var dt = oraConnection.Query<String>(sSQL);
            DataTable dt = OracleHelper2.ExecuteDataset("Data Source=KS_QAS_AY;User ID=FWASSY;Password=FWASSY", sSQL, CommandType.Text).Tables[0];

            return dt;

        }
        static string getNextRule(string step,string currentRule) {
            string sql = "select nextrule from steprule where step='' and currentrule=''";
            return "";
        }

        

        static string getCurrentStep(string lot)
        {

            return "";
        }

        public static void TrackIn(Lot lot, Eqp eqp) {
            string sql;
            if (check_if_lot_can_trackin(lot,eqp) == false) {
                return;
            }
            lot.stepStatus = "Excuting" + lot.currentStepRule;
            //改变机台，lot状态,其他客户端不可再操作此机台和lot
            //判断此站点是否消耗物料，是则扣减物料
            //改变lot站点
            eqp.ConsumeMaterial();
            lot.routing.getCurrentStep().进入下个状态();// currentStep.进入下个状态();
            
            lot.currentStepRule = lot.routing.getCurrentStep().getCurrentRule();
            Log.WriteLog(lot.id + "进入下个rule" + lot.currentStepRule);
            lot.stepStatus = "waitingFor" + lot.currentStepRule;


        }

        //public static void trackIn(Lot lot)
        //{
        //    string sql;
        //    if (check_if_lot_can_trackin(lot, new eqp(lot.eqpid) { }) == false)
        //    {
        //        return;
        //    }
        //    lot.stepStatus = "Excuting" + lot.rule;
        //    //改变机台，lot状态,其他客户端不可再操作此机台和lot
        //    //判断此站点是否消耗物料，是则扣减物料
        //    //改变lot站点
        //    eqp.ConsumeMaterial(lot);
        //    lot.routing.getCurrentStep().进入下个状态();// currentStep.进入下个状态();


        //}

        public static void TrackOut(Lot lot, Eqp eqp) {
            string sql;
            if (check_if_lot_can_trackout(lot, eqp) == false)
            {
                MessageBox.Show("状态不对，不能出账");
                return;
            }
            lot.stepStatus = "Excuting" + lot.currentStepRule;

            
            lot.进入下个站点();
            记录员工业绩(lot, eqp);

            lot.stepStatus = "waitingFor" + lot.currentStepRule;
        }

        public static void DefaultProcess(Lot lot, Eqp eqp)
        {
            string sql;
           
            lot.stepStatus = "Excuting" + lot.currentStepRule;


          
            eqp.ConsumeMaterial();
            lot.routing.getCurrentStep().进入下个状态();// currentStep.进入下个状态();
            lot.currentStepRule = lot.routing.getCurrentStep().getCurrentRule();

            记录员工业绩(lot, eqp);

            lot.stepStatus = "waitingFor" + lot.currentStepRule;
        }



        static bool check_if_lot_can_trackin(Lot lot,Eqp eqp) {
            if (lot.stepStatus.Contains("Excuting") || !lot.state.Equals("Active"))
            {
                return false;
                
            }
            else {
                return true;

            }
            if (lot.routing.getCurrentStep().NeedMatType.Count > 0 ) {
                foreach (Material mattype in lot.routing.getCurrentStep().NeedMatType) {
                    if (!eqp.need_mats.Keys.Contains(mattype)) {
                        return false;
                    }
                }

            }
            
       }


        static bool check_if_lot_can_trackout(Lot lot, Eqp eqp)
        {
            if (lot.stepStatus.Contains("Excuting") || !lot.state.Equals("Active"))
            {
                return false;

            }
            else
            {
                return true;

            }
            

        }


        static void 记录员工业绩(Lot lot,Eqp eqp) {
            if (lot.id == eqp.currentLot.id) {
                //eqp.logonAccount;
                //insert into performance (acc,lot,num,eqpid)
            }
            


        }


       public static string 是否满足扣次品条件(Lot lot, string defectCode, double num) {
            //某站点不允许
            //批次状态
            //不允许扣除超过某个数量  
            return "";
        }

       public static string 是否满足扣次品条件(string lot)
        {
            //某站点不允许
            //批次状态
            //不允许扣除超过某个数量  
            return "";
        }


    }


    
   

   
   

   
   
}
