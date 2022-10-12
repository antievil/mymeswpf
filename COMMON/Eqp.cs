using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mymeswpf.COMMON
{
    public class Eqp
    {


        public Eqp(string eqpid)
        {
        }

        public string id;
        public string state;
        public string step;
        public Dictionary<Material,Double> need_mats;
        //public List<Material> mats;
        public Lot currentLot;
        public string stepGroup;
        public string logonAccount;
        public void ConsumeMaterial()
        {
            if (need_mats == null) return;
            foreach (var mat in need_mats) {
                mat.Key.QUANTITY = mat.Key.QUANTITY - mat.Value;//在给对象中数量属性改变时 改变数据库中数量
            }

        }
        public void 分配批次(Lot lot)
        {
            if (lot.state != "Active")
            {

                return;
            }
            if (lot.routing.getCurrentStep().stepGroup != this.stepGroup)
            {

                return;
            }
            currentLot = lot;
        }

        public void 配置物料消耗量() {


        }

    }
}
