using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mymeswpf.COMMON
{
    //状态机  此数据结构应该保持干净  不掺杂业务逻辑数据结构 比如LOT
    //routing没有当前状态  站点和rule有
    public class State<T> : Log
    {

        public int currentStateIndex;
        public int nextStateIndex;
        public int lastStateIndex;
        public List<T> statelist;
        //public Lot lot;
        //初始化状态链 及 第一个状态
        public State(List<T> list)
        {

            //currentState = state;
            statelist = list;// new List<T>();//todo
            currentStateIndex = 0;
            if (statelist.Count > 1)
            {
                nextStateIndex = 1;
            }

        }

        public void 进入下个状态()
        {
            if (statelist.Count != currentStateIndex + 1)
            {
                lastStateIndex = currentStateIndex;
                currentStateIndex = nextStateIndex;
                nextStateIndex++;

            }

           
            if ((typeof(T).ToString() == "System.String"))
            {
                Log.WriteLog("进入下个状态" + statelist[currentStateIndex].ToString());
                //lot.currentStepRule = statelist[currentStateIndex].ToString();
            }
        }



    }
}
