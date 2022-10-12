using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mymeswpf.COMMON
{
    public class Step : State<String>
    {
        public string step;
        public string stepGroup;
        public Step(string stepname, List<String> list) : base(list)
        {
            this.step = stepname;

        }
        public string getCurrentRule()
        {

            return base.statelist[base.currentStateIndex];

        }

        public string getNextRule()
        {

            return base.statelist[base.nextStateIndex];
        }
        //public Step(string step) {
        //    this.step = step;
        //}
        public List<Material> NeedMatType;
        //public LinkedList<string> rules;
    }
}
