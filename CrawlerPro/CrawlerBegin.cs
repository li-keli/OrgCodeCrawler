using CrawlerPro.Common;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CrawlerPro
{
    /// <summary>
    /// 北京企业信息网爬虫中继
    /// </summary>
    public class CrawlerBegin : CrawlerCom
    {
        private Label IpAddress { set; get; }
        public CrawlerBegin(Label Ip)
        {
            IpAddress = Ip;
        }
        /// <summary>
        /// 北京企业信息网爬虫中继
        /// </summary>
        public void CrawlerBeginFunc(TaskList tasklist, int threadCount)
        {
            try
            {
                //new Thread(new ParameterizedThreadStart(CrawlerThread)).Start(tasklist);
                tasklist.Threads = threadCount;
                for (int i = 0; i < threadCount; i++)
                {
                    new Thread(CrawlerThread).Start(tasklist);
                }
            }
            catch (Exception e)
            {
                OutLog(e.Message);
            }
        }

        /// <summary>
        /// 爬虫单元线程
        /// </summary>
        private void CrawlerThread(object obj)
        {
            TaskList tasklist = obj as TaskList;
            string changeIp = null;
            while (tasklist.GetSerplusNum() > 0)
            {
                try
                {
                    string companyName = tasklist.GetNext();
                    var transitionModel = CrawlerWork(companyName, changeIp);
                    changeIp = transitionModel.Value;
                    //if (!SqlHelper.InserInfo(transitionModel.Key)) OutLog(companyName + "写入数据库错误。");
                    if (!DoSql.InsertBj(transitionModel.Key)) OutLog(companyName + "写入数据库错误。");
                }
                catch (Exception e)
                {
                    OutLog(e.Message);
                }
            }
            OutLog("线程【" + Thread.CurrentThread.ManagedThreadId + "】爬取任务结束");
            tasklist.StopThead();
        }

        private TextBox ListLogs { set; get; }
        public string SingelCrawlerThread(string searchText, ListBox list, TextBox listbox, 
            string changeIp = null)
        {
            ListLogs = listbox;
            try
            {
                string companyName = searchText;
                var transitionModel = CrawlerWork(companyName, changeIp);
                changeIp = transitionModel.Value;
                if (!DoSql.InsertBj(transitionModel.Key)) OutLog(companyName + ",写入数据库错误。");
            }
            catch (Exception e)
            {
                OutLog(e.Message);
            }
            OutLog("线程【" + Thread.CurrentThread.ManagedThreadId + "】爬取任务结束");
            return changeIp;
        }

        /// <summary>
        /// 显示IP地址
        /// </summary>
        /// <param name="IP"></param>
        private string SetIpAddress(string IP)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(IP))
                    return "";
                var returnmModel = IpToAddress.IpAddress(IP.Split(':')[0].Trim());
                if (returnmModel == null)
                    return "IP地址解析错误";
                return returnmModel.s0 + " " + returnmModel.s1;
            }
            catch (Exception e)
            {
                OutLog("IP地址解析：" + e.Message);
                return "IP地址解析错误";
            }

        }


        public override void OutLog(string message)
        {
            if (ListLogs != null)
            {
                ListLogs.Invoke(new MethodInvoker(delegate ()
                {
                    ListLogs.AppendText("\r\n" + DateTime.Now + " " + message);
                }));
            }
            Console.WriteLine(message);
        }

        public override void ShowIpAddress(string Ip)
        {
            if (Ip.Contains(":"))
            {
                var aa = SetIpAddress(Ip);
                IpAddress.Invoke(new MethodInvoker(delegate ()
                {
                    IpAddress.Text = aa;
                }));
            }
        }
    }
}
