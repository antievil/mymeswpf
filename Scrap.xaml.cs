using mymeswpf.COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mymeswpf
{
    /// <summary>
    /// Scrap.xaml 的交互逻辑
    /// </summary>
    public partial class Scrap : Page
    {
        Lot lot;
        public Scrap(Lot lot)
        {
            this.lot = lot;
            InitializeComponent();
            List<defectSelect> dicItem = new List<defectSelect>();
            dicItem.Add(new defectSelect { name = "yield<90%", code = "Yiled_90" });
            dicItem.Add(new defectSelect { name = "num <", code = "2" });
            dicItem.Add(new defectSelect { name = "timespan >", code = "3" });
            dicItem.Add(new defectSelect { name = "xx", code = "4" });

            defectCode.DisplayMemberPath = "name";
            defectCode.SelectedValuePath = "code";
            defectCode.ItemsSource = dicItem;
            defectCode.SelectedIndex = 1;
            currentNum.Content = lot.quantity;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lot.quantity = lot.quantity - Convert.ToInt64(modifyNum);
            
        }
    }

    public class defectSelect {

        public string name;
        public string code;

    }
}
