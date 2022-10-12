using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace mymeswpf.COMMON
{
    public class Log
    {
        private static ConcurrentQueue<QContent> logQueue = new ConcurrentQueue<QContent>();
        private static Dictionary<string, long> fileDic = new Dictionary<string, long>();
        //private static ConcurrentQueue<QContent> logQueue = new ConcurrentQueue<QContent>();
        private static string paramPath = "param.txt";

        static DispatcherTimer _timer = new DispatcherTimer();
        public static void DoWork(TextBox textBlock)
        {
            _timer.Interval = new TimeSpan(0, 0, 1);
            EventHandler event1 = new EventHandler(timer_Tick);
            _timer.Tick += event1;
            _timer.Tag = textBlock;
            _timer.Start();
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "./";
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            watcher.Filter = "log.txt";
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.EnableRaisingEvents = true;
           
        }
        public static void Stop()
        {
            _timer.Stop();
        }
        static void timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            TextBox box = timer.Tag as TextBox;
            WatchLog(box);
            //box.Text = "张三" + DateTime.Now;
        }

        public static void WriteLog(string text)
        {
            File.AppendAllText("./log.txt", text + Environment.NewLine);
        }

        public static void WatchLog(Object tb)
        {
            QContent c = new QContent();
            
            logQueue.TryDequeue(out c);
            if (c != null && !string.IsNullOrEmpty(c.Content)) {
                //TextBox s = (TextBox)tb;
                ((TextBox)tb).Text = c.Content;
            }
                

            
            
        }


        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("Changed 文件路径：" + e.FullPath);
            long temp = 0;
            fileDic.TryGetValue(e.FullPath, out temp);
            if (temp == 0)
            {
                fileDic[e.FullPath] = 0;
            }
            AppendContentToCosole(fileDic[e.FullPath], e.FullPath, e.Name);

        }


        private static void AppendContentToCosole(long offset, string filePath, string name)
        {
            string line = string.Empty;
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fs.CanSeek)
                    {
                        fs.Seek(offset, SeekOrigin.Begin);
                        fileDic[filePath] = fs.Length;
                        Debug.WriteLine("fs.Length:" + fs.Length);
                        if (offset < fs.Length)//防止期间文件删除后创建导致偏移变化
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                string tmp = "";
                                while (string.IsNullOrEmpty(tmp = sr.ReadLine()) != true)
                                {
                                    Debug.WriteLine("追加:" + tmp);//输出追加的内容
                                    QContent _q = new QContent() { FileName = name, Content = tmp };
                                    logQueue.Enqueue(_q);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("当前流不支持查找");
                    }
                }
            }
        }



    }

    public class QContent
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }

}
