using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mymeswpf.COMMON
{
    public class Lot//: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        //private static readonly Lot instance = new Lot();

        
        //public static Lot GetInstance()
        //{
        //    return instance;
        //}

        public Lot(string slot)
        {
            //数据库中获取信息赋给属性
            string route = "";
            //this.routing = new Routing(route);
            //this.currentStep = new Step("");
            //this.nextStep = new Step("");
        }
        public Lot(string slot, String rou)
        {
            //数据库中获取信息赋给属性
            this.id = slot;
            //this.routing = rou;
            this.state = LotStatus.Active;
            this.quantity = 233;
            this.eqpid = "";
            //this.currentStep = new Step("");
            //this.nextStep = new Step("");
        }
        public string id { get; set; }
        public string state { get; set; }

        public string hold_code { get; set; }

        public Int64 quantity { get; set; }

        public string eqpid { get; set; }
        public string currentStep { get { return routing.getCurrentStep().step; }
            set { }
        }
        public string currentStepRule {

            get { return routing.getCurrentStep().getCurrentRule(); }

            set

            {


                //if (PropertyChanged != null)

                //{

                //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("currentStepRule"));

                //}

            }
        }

        public string stepStatus { get; set; }
        //public string currentStep;
        //public string currentStepRule;
        //public Step nextStep;
        //public string nextStepRule;
        private Routing rout;
        public Routing routing {
            get { return rout; }
            set {
                rout = value;
                this.stepStatus = "waitingFor" + value.getCurrentStep().getCurrentRule();
            }
        }
        public void 进入下个站点() {
            routing.进入下个状态();
            //routing.getCurrentStep();
            //this.step = new Step(step);
            //this.currentStep = nextStep;
            //Step st = new Step(step);
            //this.currentStepRule = st.statelist[st.currentStateIndex];

            //this.nextStepRule;
            //nextStep = routing.站点排程.Find(this.currentStep).Next.Value;
        }

        public void 扣次品(string defectCode,double num) {

          string res =   Base.是否满足扣次品条件(this,defectCode,num);
            if (res != "")
            {
                MessageBox.Show(res);
            }
            else {
                if ((this.quantity - num) / this.quantity < 0.90) {

                    this.Hold(defectCode);
                }
            }
          //扣次品后判断良率 不足 hold

        }

        public void Hold(string holdcode) {
            state = LotStatus.Hold;
            this.hold_code = holdcode;

        }

    }

    public class LotStatus {
        public const string Active = "Active";
        public const string Terminated = "Terminated";
        public const string Hold = "Hold";
    }
}
