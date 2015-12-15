using Crawler.Model;
using CrawlerPro.Common;
using Ivony.Html;
using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CrawlerPro
{
    /// <summary>
    /// 北京企业信息网爬虫核心基类
    /// </summary>
    public abstract class CrawlerCom
    {
        #region URL种子
        /// <summary>
        /// 根URL
        /// </summary>
        const string url = "http://211.94.187.236";
        /// <summary>
        /// CookieURL
        /// </summary>
        const string firsturl = "http://211.94.187.236/wap";
        /// <summary>
        /// 查询列表
        /// </summary>
        const string targetUrl = "http://211.94.187.236/wap/creditWapAction!QueryEnt20130412.dhtml";
        /// <summary>
        /// 组织机构代码 URL种子
        /// </summary>
        const string orgCodeUrl = "http://211.94.187.236/wap/creditWapAction!view_zj_wap.dhtml";
        /// <summary>
        /// 工行注册登记号 URL种子
        /// </summary>
        const string gSzcUrl = "http://211.94.187.236/wap/creditWapAction!qr.dhtml";
        /// <summary>
        /// 税务 URL种子
        /// </summary>
        const string sWUrl = "http://211.94.187.236/wap/creditWapAction!qy.dhtml";
        /// <summary>
        /// 投资人 URL种子
        /// </summary>
        const string tzrUrl = "http://211.94.187.236/wap/creditWapAction!view_tzr_wap.dhtml";
        /// <summary>
        /// 出资历史 URL种子
        /// </summary>
        const string czlsUrl = "http://211.94.187.236/wap/creditWapAction!view_czlsxx_wap.dhtml";
        /// <summary>
        /// 主要人员 URL种子
        /// </summary>
        const string zyryUrl = "http://211.94.187.236/wap/creditWapAction!view_zyry_wap.dhtml";
        const string zhuUrl = "http://211.94.187.236/xycx/queryCreditAction!qyxq_view.dhtml";
        #endregion

        /// <summary>
        /// 爬虫逻辑
        /// </summary>
        /// <param name="companyName">企业名称</param>
        /// <param name="isChangeIp">是否切换IP</param>
        /// <returns></returns>
        public KeyValuePair<TargetModel, string> CrawlerWork(string companyName, string isChangeIp)
        {
            KeyValuePair<TargetModel, string> Transition = new KeyValuePair<TargetModel, string>();
            TargetModel model = new TargetModel();
            try
            {
                string ips = isChangeIp; //代理IP
                bool ipbool = true; //ip重置开关
                CookieContainer cookieContainer = new CookieContainer();
                var resultCookie = new HttpUtility(null, Encoding.UTF8).Request("post", firsturl, "", cookieContainer, OnComplete);
                bool isw = false;
                while (!isw)
                {
                    if (!string.IsNullOrEmpty(isChangeIp))
                    {
                        isw = WebCrawlerBegin(companyName, ips, cookieContainer, out ipbool, out Transition);
                    }
                    else
                    {
                        if (ipbool) ips = GetIp();
                        isw = WebCrawlerBegin(companyName, ips, cookieContainer, out ipbool, out Transition);
                    }

                    if (!isw && !string.IsNullOrEmpty(isChangeIp))
                    {
                        ips = GetIp();
                    }

                }
                model = Transition.Key ?? new TargetModel();
                model.basicsInfo = model.basicsInfo ?? new BasicsInfo();
                model.capitalInfo = model.capitalInfo ?? new CapitalInfo();
                model.orgCodeInfo = model.orgCodeInfo ?? new OrgCodeInfo();
                model.taxInfo = model.taxInfo ?? new TaxInfo();
                model.basicsInfo.市工商局_查询名称 = companyName;
                Transition = new KeyValuePair<TargetModel, string>(model, ips);
                //OutLog("单元数据抓取结束");
            }
            catch (Exception e)
            {
                OutLog(e.Message);
            }
            return Transition;
        }

        /// <summary>
        /// 单个企业查询（爬行逻辑主体）
        /// </summary>
        /// <param name="companyName">企业名称</param>
        /// <param name="ips">代理IP地址</param>
        /// <param name="cookieContainer">cookie</param>
        /// <param name="ipbool">是否切换IP</param>
        /// <param name="keyvalue">企业名称，组织机构代码</param>
        /// <returns></returns>
        public bool WebCrawlerBegin(string companyName, string ips, CookieContainer cookieContainer, out bool ipbool, out KeyValuePair<TargetModel, string> modelKey)
        {
            ipbool = false; //初始化状态
            TargetModel model = new TargetModel();
            modelKey = new KeyValuePair<TargetModel, string>();
            try
            {
                string parameter = $"queryStr={Uri.EscapeUriString(companyName)}&module=&idFlag=qyxy";
                var resultBody = new HttpUtility(ips, Encoding.UTF8).Request("get", targetUrl, parameter, cookieContainer, OnComplete);
                if (!ChangeIp(resultBody)) { ipbool = true; return false; }//检查状态
                string nextUrl = "", outCompName = "";
                ParsingHtml(resultBody, url, out outCompName, out nextUrl);
                if (nextUrl.Equals(""))
                {
                    OutLog("正常查询，但此处并无数据");
                }
                else
                {
                    var resultsecondBody = new HttpUtility(ips, Encoding.UTF8).Request("get", zhuUrl + new Uri(firsturl + nextUrl).Query, "", cookieContainer,
                        OnComplete);
                    if (!ChangeIp(resultsecondBody)) { ipbool = true; return false; }
                    model = HtmlAnalytical(resultsecondBody);
                }
                modelKey = new KeyValuePair<TargetModel, string>(model ?? new TargetModel(), null);
                return true;
            }
            catch (Exception e)
            {
                OutLog("结构循环出现一次错误：" + e.Message);
                ipbool = true; return false;
            }
        }

        /// <summary>
        /// 解析HTML文本信息
        /// </summary>
        /// <param name="SourceHtml"></param>
        /// <returns></returns>
        private TargetModel HtmlAnalytical(string SourceHtml)
        {
            TargetModel model = new TargetModel(); //模型容器
            //model.HtmlScore = SourceHtml; //存储源码
            var sorceIhtml = new JumonyParser().Parse(SourceHtml);
            var tdHtmlBases = sorceIhtml.Find(".f-lan tr");
            var list = new List<string>();
            var elements = tdHtmlBases as IHtmlElement[] ?? tdHtmlBases.ToArray();
            for (int i = 0; i < elements.Count(); i++)
            {
                Console.WriteLine(elements[i].Find("td").FirstOrDefault().InnerText());
                var text = elements[i].Find("td").FirstOrDefault().InnerText();
                switch (text.Trim())
                {
                    case "工商登记注册基本信息":
                        Modular01(sorceIhtml, model, i);
                        break;
                    case "资本相关信息":
                        Modular02(sorceIhtml, model, i);
                        break;
                    case "组织机构代码信息":
                        Modular03(sorceIhtml, model, i);
                        break;
                    case "税务登记信息":
                        Modular04(sorceIhtml, model, i);
                        break;
                    default:
                        break;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取IP代理地址
        /// </summary>
        /// <returns></returns>
        private string GetIp()
        {
            string myIp = "";
            int iptimes = 1;
            while (true)
            {
                OutLog("尝试获取代理中，第" + iptimes++ + "次");
                myIp = new HttpUtility(null, Encoding.Default).Request("get", "http://vxer.daili666.com/ip/?tid=557541152620047&num=1&delay=5&category=2&sortby=time&foreign=none&filter=on", "", new CookieContainer(), OnComplete);
                var info = IpAddress(myIp);
                if (!string.IsNullOrEmpty(info))
                {
                    OutLog(info);
                    ShowIpAddress(myIp);
                    break;
                }
                Thread.Sleep(250);
            }
            return myIp;
        }

        /// <summary>
        /// 查询当前请求IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private string IpAddress(string ip)
        {
            const string searchIp = "http://1111.ip138.com/ic.asp";
            ResponseItem myIp = new HttpRequestHelper().RequestDataByPost(
            new RequestItem
            {
                RequestUrl = searchIp,
                Method = HttpMethodEnum.Get,
                AutoRedirect = false,
                Timeout = 3 * 1000,
                ProxyInfo = new ProxySetInfo { ProxyIp = ip }
            });
            if (myIp == null || !myIp.ResultHtml.Contains("IP是"))
            {
                return null;
            }
            var documentmyIp = new JumonyParser().Parse(myIp.ResultHtml).Find("center");
            var isCenter = documentmyIp.FirstOrDefault();
            return isCenter.InnerText();
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <param name="responsebody"></param>
        /// <returns>返回false 无效，需要重置， 返回true 通过</returns>
        private bool ChangeIp(string responsebody)
        {
            if (string.IsNullOrEmpty(responsebody)
                || responsebody.Contains("error")
                || responsebody.Contains("ERROR")
                || responsebody.Contains(">403")
                || responsebody.Contains(">302")
                || responsebody.Contains("gb2312")
                || responsebody.Contains("操作超时")
                || responsebody.Contains("无法连接到远程服务器")
                || responsebody.Contains("访问异常"))
            {
                return sendEnd();
            }
            return true;
        }

        /// <summary>
        /// 输出切换代理信息
        /// </summary>
        private bool sendEnd()
        {
            OutLog("该IP被封杀，切换代理中...");
            return false;
        }

        /// <summary>
        /// 主线程回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        private void OnComplete(HttpStatusCode code, string result)
        {
            //执行完毕后，执行内容
        }

        /// <summary>
        /// 获取二级菜单列表
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="url"></param>
        /// <param name="companyName"></param>
        /// <param name="nextUrl"></param>
        private void ParsingHtml(string strHtml, string url, out string companyName, out string nextUrl)
        {
            var parser = new JumonyParser();
            var document = parser.Parse(strHtml).Find("li a");
            foreach (var htmlElement in document)
            {
                OutLog(htmlElement.InnerText());
                //OutLog(htmlElement.Attribute("href").AttributeValue);
                companyName = htmlElement.InnerText();
                nextUrl = url + htmlElement.Attribute("href").AttributeValue;
                return;
            }
            companyName = "";
            nextUrl = "";
        }

        /// <summary>
        /// 输出日志信息
        /// </summary>
        /// <param name="message"></param>
        public abstract void OutLog(string message);


        /// <summary>
        /// 输出IP地址
        /// </summary>
        /// <param name="message"></param>
        public abstract void ShowIpAddress(string Ip);

        #region HTML解析帮助
        public static TargetModel Modular01(IHtmlDocument sorceIhtml, TargetModel model, int i)
        {
            //基础信息
            var tdHtml_base = sorceIhtml.Find(".f-lbiao").ElementAt(i).Find("tr td").ToList();
            //foreach (var td in tdHtml_base)
            //{
            //    Console.WriteLine(td.InnerText());
            //}
            model.basicsInfo = FillModel<BasicsInfo>(tdHtml_base);
            return model;
        }

        public static TargetModel Modular02(IHtmlDocument sorceIhtml, TargetModel model, int i)
        {
            //资本相关信息
            var tdHtml_zb = sorceIhtml.Find(".f-lbiao").ElementAt(i).Find("tr td").ToList();
            //foreach (var td in tdHtml_zb)
            //{
            //    Console.WriteLine(td.InnerText());
            //}
            model.capitalInfo = FillModel<CapitalInfo>(tdHtml_zb);
            return model;
        }

        public static TargetModel Modular03(IHtmlDocument sorceIhtml, TargetModel model, int i)
        {
            //组织机构代码信息
            var tdHtml_orgCode = sorceIhtml.Find(".f-lbiao").ElementAt(i).Find("tr td").ToList();
            //foreach (var td in tdHtml_orgCode)
            //{
            //    Console.WriteLine(td.InnerText());
            //}
            model.orgCodeInfo = FillModel<OrgCodeInfo>(tdHtml_orgCode);
            return model;
        }

        public static TargetModel Modular04(IHtmlDocument sorceIhtml, TargetModel model, int x)
        {
            //税务登记信息
            var trHtml_tax = sorceIhtml.Find(".f-lbiao").ElementAt(x).Find("tr").ToList();
            foreach (var tr in trHtml_tax)
            {
                var sorceth = tr.Find("th");
                var sorcetd = tr.Find("td");
                //for (var i = 0; i < sorceth.Count(); i++)
                //{
                //    Console.WriteLine(sorceth.ElementAt(i).InnerText());
                //    Console.WriteLine(sorcetd.ElementAt(i).InnerText());
                //}
            }
            model.taxInfo = FillModel<TaxInfo>(trHtml_tax.Find("th").ToList(), trHtml_tax.Find("td").ToList());
            return model;
        }

        /// <summary>
        /// List 映射到 模型，针对td
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scoreList"></param>
        /// <returns></returns>
        private static T FillModel<T>(List<IHtmlElement> scoreList) where T : new()
        {
            T t = new T();
            PropertyInfo[] info = t.GetType().GetProperties();
            foreach (var item in info)
            {
                string name = item.Name.Split('_')[1];
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        var trName = scoreList[i].InnerText().Replace("：", "");
                        var isContent = trName.Equals(name);
                        if (isContent) item.SetValue(t, scoreList[i + 1].InnerText(), null);
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// List 映射到 模型，针对th和td
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scoreTh"></param>
        /// <param name="scoreTd"></param>
        /// <returns></returns>
        private static T FillModel<T>(List<IHtmlElement> scoreTh, List<IHtmlElement> scoreTd) where T : new()
        {
            T t = new T();
            PropertyInfo[] info = t.GetType().GetProperties();
            foreach (var item in info)
            {
                string name = item.Name.Split('_')[1];
                for (int i = 0; i < scoreTh.Count; i++)
                {
                    var trName = scoreTh[i].InnerText().Replace("：", "");
                    var isContent = trName.Equals(name);
                    if (isContent)
                    {
                        item.SetValue(t, scoreTd[i].InnerText(), null); break;
                    }
                }
            }
            return t;
        }

        #endregion
    }
}
