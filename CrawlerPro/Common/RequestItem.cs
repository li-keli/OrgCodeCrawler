using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CrawlerPro.Common
{
    public class RequestItem
    {
        #region 字段
        private readonly IDictionary<string, string> _dicParamsData;
        private readonly WebHeaderCollection _headerCollection;
        private string _reqUrl = string.Empty;
        private CookieContainer _reqCookie = new CookieContainer();
        private int _timeout = 10000;
        private string _referer = string.Empty;
        private bool _autoRedirect = true;
        private HttpMethodEnum _method = HttpMethodEnum.Get;
        private int _readWriteTimeout = 10000;
        #endregion

        #region 构造函数

        public RequestItem()
        {
            _dicParamsData = new Dictionary<string, string>();
            _headerCollection = new WebHeaderCollection();
        }
        public RequestItem(string url)
            : this()
        {
            _reqUrl = url;
        }
        public RequestItem(string url, CookieContainer cookie)
            : this(url)
        {
            _reqCookie = cookie;
        }

        #endregion

        #region  属性
        /// <summary>
        /// 请求链接
        /// </summary>
        public string RequestUrl
        {
            get { return _reqUrl; }
            set { _reqUrl = value; }
        }
        /// <summary>
        /// Cookie集合
        /// </summary>
        public CookieContainer RequestCookies
        {
            get { return _reqCookie; }
            set { _reqCookie = value; }
        }
        /// <summary>
        /// 请求超时时间 单位：毫秒 默认10秒超时
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// 请求标头值
        /// </summary>
        public string RequestAccept { get; set; }

        /// <summary>
        /// 客户端访问信息
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 是否允许自动跳转
        /// </summary>
        public bool AutoRedirect
        {
            get { return _autoRedirect; }
            set { _autoRedirect = value; }
        }

        /// <summary>
        /// 来源地址上次访问地址/自动跳转地址
        /// </summary>
        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        /// <summary>
        /// 是否与服务器建立持久链接
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 代理服务器信息
        /// </summary>
        public ProxySetInfo ProxyInfo { get; set; }

        /// <summary>
        /// 是否设置请求证书
        /// </summary>
        public bool IsNeedCertificate
        {
            get
            {
                if (!string.IsNullOrEmpty(RequestUrl))
                {
                    return RequestUrl.StartsWith("https:");
                }
                return false;
            }
        }

        /// <summary>
        /// 请求返回类型默认 text/html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///  HTTP请求类型 默认为GET
        /// </summary>
        public HttpMethodEnum Method
        {
            get { return _method; }
            set { _method = value; }
        }
        /// <summary>
        /// POST写入数据超时时间  单位：毫秒  默认 10秒
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _readWriteTimeout; }
            set { _readWriteTimeout = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加HttpWebRequest标头信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetHeaderData(string name, string value)
        {
            _headerCollection.Add(name, value);
        }
        /// <summary>
        /// 添加HttpWebRequest标头信息
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        public void SetHeaderData(HttpRequestHeader header, string value)
        {
            _headerCollection.Add(header, value);
        }

        /// <summary>
        /// 获取HttpWebRequest标头信息
        /// </summary>
        /// <returns></returns>
        public WebHeaderCollection GetHeaderData()
        {
            return _headerCollection;
        }

        /// <summary>
        /// 添加POST请求
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetParamsData(string name, string value)
        {
            if (!_dicParamsData.ContainsKey(name))
            {
                _dicParamsData.Add(name, value);
            }
        }
        /// <summary>
        /// 获取POST请求数据
        /// </summary>
        /// <returns></returns>
        public string GetParamsData()
        {
            var sbResult = new StringBuilder();
            if (null != _dicParamsData)
            {

                bool isTag = true;
                foreach (var current in _dicParamsData)
                {
                    if (!isTag)
                    {
                        sbResult.AppendFormat("&{0}={1}", current.Key, current.Value);
                    }
                    else
                    {
                        sbResult.AppendFormat("{0}={1}", current.Key, current.Value);
                        isTag = false;
                    }
                }
            }
            return sbResult.ToString();
        }

        #endregion
    }
    public class ProxySetInfo
    {
        private string _proxyUserName = string.Empty;
        /// <summary>
        /// 代理服务器 用户名
        /// </summary>
        public string ProxyUserName
        {
            get { return _proxyUserName; }
            set { _proxyUserName = value; }
        }
        private string _proxyPassword = string.Empty;
        /// <summary>
        /// 代理服务器 密码
        /// </summary>
        public string ProxyPassword
        {
            get { return _proxyPassword; }
            set { _proxyPassword = value; }
        }
        private string _proxyIp = string.Empty;
        /// <summary>
        /// 代理服务器 IP
        /// </summary>
        public string ProxyIp
        {
            get { return _proxyIp; }
            set { _proxyIp = value; }
        }
    }

    /// <summary>
    /// HTTP请求类型
    /// </summary>
    public enum HttpMethodEnum
    {
        /// <summary>
        /// GET请求
        /// </summary>
        Get = 0,
        /// <summary>
        /// POST请求
        /// </summary>
        Post = 1
    }
}
