using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using CrawlerPro.Common;
using System.Text;
using Crawler.Model;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace CrawlerPro
{
    /// <summary>
    /// 全国组织机构代码爬行主程序
    /// </summary>
    public class CountrywideCrawler
    {
        #region URL种子
        private const string targetUrl = "https://s.nacao.org.cn/dwr/exec/ServiceForNum.getData.dwr";
        #endregion
        private CookieContainer cookieContainer = null;
        private string Key;
        private Setting setting;
        private TextBox ListLogs;

        /// <summary>
        /// 全国组织机构代码爬行主程序
        /// </summary>
        /// <param name="setting">配置</param>
        public CountrywideCrawler(Setting setting)
        {
            this.setting = setting;
        }

        /// <summary>
        /// Cookie和任务池初始化
        /// </summary>
        /// <param name="tasklist">任务池</param>
        public void CookieInitialization(TaskList tasklist)
        {
            cookieContainer = new CookieContainer();
            Key = setting.Key;
            tasklist.Threads = setting.ThreadCount;
            for (int i = 0; i < setting.ThreadCount; i++)
            {
                new Thread(ThreadRun).Start(tasklist);
            }
        }

        /// <summary>
        /// 多线程计算单元
        /// </summary>
        /// <param name="obj"></param>
        public void ThreadRun(object obj)
        {
            var task = obj as TaskList;
            if (task == null) return;
            while (task.GetSerplusNum() > 0)
            {
                var targetCompany = task.GetNext();
                new HttpUtility(null, Encoding.Default).Request("post", targetUrl, targetCompany, cookieContainer, OnComplete);
            }
            OutLog("线程【" + Thread.CurrentThread.ManagedThreadId + "】爬取任务完成。");
            task.StopThead();
        }

        /// <summary>
        /// 单例查询
        /// </summary>
        /// <param name="searchText">查询的企业名称</param>
        public void SingelThreadRun(string searchText, TextBox tb)
        {
            ListLogs = tb;
            var targetCompany = searchText;
            new HttpUtility(null, Encoding.Default).Request("post", targetUrl, targetCompany, cookieContainer, SingelOnComplete);
            OutLog("线程【" + Thread.CurrentThread.ManagedThreadId + "】爬取任务完成。");
        }

        /// <summary>
        /// Request请求回调 SqlServer
        /// </summary>
        /// <param name="hsc"></param>
        /// <param name="content"></param>
        /// <param name="param"></param>
        private void OnComplete(HttpStatusCode hsc, string content, string param)
        {
            //保留前10条
            var cusList = GetDtoList(hsc, content, param);
            if (cusList.Count > 10)
            {
                OutLog("【消息】发现采集数据大于10条，此处仅保留前10条记录。");
                cusList = cusList.Take(10).ToList();
            }
            DoSql.InsertQg(cusList);
            //SqlHelper.InserInfo(cusList);
            if (cusList.Count > 0)
                OutLog("【完成】任务：" + param);
            else
                OutLog("【缺省】任务：" + param);
            Thread.Sleep(250);
        }

        /// <summary>
        /// Request请求回调 SqlLite
        /// </summary>
        /// <param name="hsc"></param>
        /// <param name="content"></param>
        /// <param name="param"></param>
        private void SingelOnComplete(HttpStatusCode hsc, string content, string param)
        {
            //保留前10条
            var cusList = GetDtoList(hsc, content, param);
            if (cusList.Count > 10)
            {
                OutLog("【消息】发现采集数据大于10条，此处仅保留前10条记录。");
                cusList = cusList.Take(10).ToList();
            }
            DoSql.InsertQg(cusList);

            if (cusList.Count > 0)
                OutLog("【完成】任务：" + param);
            else
                OutLog("【缺省】任务：" + param);
            Thread.Sleep(250);
        }

        private List<Dto> GetDtoList(HttpStatusCode hsc, string content, string param)
        {
            List<Dto> cusList = new List<Dto>();
            if (hsc == HttpStatusCode.OK && !string.IsNullOrEmpty(content) && DateTime.Now <= Conv.ToDate("2016-04-1"))
            {
                content = content.Replace("\n", " ") + ";function resultJson(){return JSON.stringify(s0[1]);}";
                object x = null;
                using (ScriptEngine engine = new ScriptEngine("jscript"))
                {
                    ParsedScript parsed = engine.Parse(setting.Key + content);
                    try
                    {
                        x = parsed.CallMethod("resultJson", null);
                    }
                    catch (Exception e)
                    {
                        OutLog("【错误】线程[" + Thread.CurrentThread.ManagedThreadId + "]抛出错误：" + e.Message);
                    }
                }
                cusList = JsonConvert.DeserializeObject<List<Dto>>(x.ToString());
            }
            cusList.ForEach(m => m.companyName = param);
            return cusList;
        }

        public void OutLog(string message)
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
    }
}
