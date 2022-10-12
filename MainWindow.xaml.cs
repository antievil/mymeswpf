using mymeswpf.COMMON;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using System.Xml;
using Dapper;
using System.Threading;
namespace mymeswpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();

            List<string> ftrulelist = new List<string>() { "TrackIn","TrackOut"};
            //List<string> ftrulelist = new List<string>() { "TrackIn", "EDC", "TrackOut" };
            

            
            
            Lot l = new Lot("GSS12343434", "F_R_V");
            Step ss = new Step("FT1T0", ftrulelist);
            List<Step> exp_rou = new List<Step>() { ss };
            Routing rr = new Routing("Routing", exp_rou, l);
            l.routing = rr;
            List<Lot> lotlist = new List<Lot>() {l };
            //gridAolots.ItemsSource = Base.getLotList().DefaultView;

            gridAolots.ItemsSource = lotlist;
            //gridCitys.ItemsSource = dt.DefaultView;
            //txtMsg.DataContext = dt.ToString();

            //new Thread(Log.WatchLog).Start();
            //Task ss = new TaskFactory();
            //Task.Run(new Action());
            //Thread thread = new Thread(new ParameterizedThreadStart(Log.WatchLog));

            //thread.Start((object)txtMsg);

            Log.DoWork(txtMsg);
            //Kits.PreventForCurrentThread();
            //new Thread(Kits.NeverSleep).Start();
        }
        private void eqp_Click(object sender,RoutedEventArgs e) {

            var testTask = new Task(() =>
            {
                //
                while (true) {
                    Kits.ResetIdle();
                    Thread.Sleep(30000);
                }
                //MessageBox.Show("ok");
                //txtMsg.Text = Math.Cos( Convert.ToDouble( DateTime.Now.Second)).ToString();
                
            });
            testTask.Start();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            

            //var function = Activator.CreateInstance(typeof(Function)) as Function;
            //var method = function.GetType().GetMethod(name);
            //object result = method.Invoke(function, parameter);
            int res;
            //Console.WriteLine(result.ToString());

            //var obj = Type.GetType("Window1").Assembly.CreateInstance("Window1");
            //MethodInfo[] info = Type.GetType("Window1").GetMethods();

            //for (int i = 0; i < info.Length; i++)
            //{
            //    if (info[i].Name == "ShowDialog") {
            //        info[i].Invoke(obj, null);
            //    }
            //    //info[i].Invoke(obj, null);
            //}
           

            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string path = @"InputParam.xml";

            Units.ReadXml(path, dict);
            
            foreach (KeyValuePair<string, string[]> kvp in dict)
            {

                #region 根据字符串去调用与字符串相同名称的方法
                object[] _params = new object[kvp.Value.Length];  //根据键值对中值的多少声明一个同样大小的字符串数组
                for (int i = 0; i <= _params.Length - 1; i++)
                {
                    _params[i] = kvp.Value[i];       //将键值对中的值写入数组中
                }

                Type t = typeof(FwRule);           //获得方法所在的类的类型
                MethodInfo mi = t.GetMethod(kvp.Key.ToString());       //根据字符串名称获得对应的方法
                object objinstance = Activator.CreateInstance(t);      //创建一个方法所在的类的一个对象
                                                                       //res = (int)
                mi.Invoke(objinstance, _params);     //根据对象和方法参数去执行方法
                #endregion

                //Console.WriteLine(res);
            }


        }

       
        protected override void OnClosed(EventArgs e) {

            //oraConnection.Close();
            //oraConnection.Dispose();
           
        }
       

        private void gridCitys_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(gridAolots.SelectedValue.ToString());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string lotid;
            //DataRowView dr =  gridAolots.CurrentItem as DataRowView;
            Lot lot = gridAolots.CurrentItem as Lot;
            String currentrule = lot.routing.getCurrentStep().getCurrentRule();
            Eqp eqp = new Eqp(lot.eqpid);


            Type t = typeof(Base);           //获得方法所在的类的类型
            MethodInfo mi = t.GetMethod(currentrule);       //根据字符串名称获得对应的方法
            if (mi == null) {

                Base.DefaultProcess(lot, eqp);
            }
            else
            {
                //if (currentrule == "TrackIn") Base.TrackIn(lot,eqp);
                //object objinstance = Activator.CreateInstance(t);      //创建一个方法所在的类的一个对象
                                                                       //res = (int)
                mi.Invoke(Base.GetInstance(), new object[] { lot, eqp });

            }
            
            //if (currentrule == "TrackIn") {
                
            //    Base.TrackIn(lot,eqp);
            //}
            //if (currentrule == "TrackOut")
            //{

            //    Base.TrackOut(lot, eqp);
            //}

            gridAolots.Items.Refresh();
            //MessageBox.Show(lot.id);
        }

        private void Scrap_Click(object sender, RoutedEventArgs e)
        {
           
            //isw.Title = "扣次品";

            NavigationWindow win = new NavigationWindow();
            //未设置大小
            //win.Content = new Page1();
            //宿主大小大于Page尺寸
            //win.Content = new Page1(300,300,500,500);
            //宿主大小小于Page尺寸
            Lot lot = gridAolots.CurrentItem as Lot;
            win.Content = new Scrap(lot);
            win.Show();
        }
    }
    

    public struct CheckLFProductID
    {
        public bool IsValid;
        public string ProductID;
        public string ErrMsg;
        public string Customer;
        public string CustDevice; 
        public string PKG; 
        public string Line; 
        public string WaferProcess; 
    }
}
