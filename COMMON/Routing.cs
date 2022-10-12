using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mymeswpf.COMMON
{
    public class Routing : State<Step>
    {

        string route;
        public Lot lot;
        public Routing(string rou, List<Step> list, Lot lot) : base(list)
        {
            this.route = rou;
            this.lot = lot;
        }

        public LinkedList<Step> 站点排程;

        //对排程进行增删改查

        public void addStep(Step step)
        {

            站点排程.AddLast(step);

        }

        public Step getCurrentStep()
        {

            return base.statelist[base.currentStateIndex];

        }

        public Step getNextStep()
        {

            return base.statelist[base.nextStateIndex];
        }

        new public void 进入下个状态()
        {

            base.进入下个状态();
            lot.currentStep = getCurrentStep().step;
            Log.WriteLog(lot.id + "进入下一个站点" + getCurrentStep().step);

        }

    }
}
