using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mymeswpf.COMMON
{
    class Units
    {

        public static void ReadXml(string path, Dictionary<string, string[]> dict)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlElement xmlRoot = xml.DocumentElement;      //根节点
            XmlNodeList xmllist = xmlRoot.ChildNodes;      //根节点下所有子节点（一般是二级节点）
            foreach (XmlNode item in xmllist)
            {
                XmlNodeList inxmllist = item.ChildNodes;   //每个子节点下的所有子节点（一般是三级节点，也基本是最内层节点）
                string[] param = new string[inxmllist.Count];
                for (int i = 0; i <= inxmllist.Count - 1; i++)
                {
                    param[i] = inxmllist[i].InnerText;     //将每个子节点的值放入数组
                }
                dict.Add(item.Name, param);
            }
        }
    }
}
