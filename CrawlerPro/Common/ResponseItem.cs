using System.Net;

namespace CrawlerPro.Common
{
    public class ResponseItem
    {
        private CookieCollection _cookieColl = new CookieCollection();
        /// <summary>
        ///WebResponse.Cookies 集合
        /// </summary>
        public CookieCollection CookieColl
        {
            get { return _cookieColl; }
            set { _cookieColl = value; }
        }

        private string _setCookies = string.Empty;
        /// <summary>
        /// WebResponse.Headers Set-Cookie 
        /// </summary>
        public string SetCookies
        {
            get { return _setCookies; }
            set { _setCookies = value; }
        }

        private WebHeaderCollection _headerColl = new WebHeaderCollection();
        /// <summary>
        /// Header
        /// </summary>
        public WebHeaderCollection HeaderColl
        {
            get { return _headerColl; }
            set { _headerColl = value; }
        }

        /// <summary>
        /// 返回HTML字符串
        /// </summary>
        public string ResultHtml { get; set; }

        /// <summary>
        /// 返回字节流
        /// </summary>
        public byte[] ResultBytes { get; set; }

        private string _locationUrl = string.Empty;

        public ResponseItem()
        {
            ResultBytes = null;
        }

        /// <summary>
        ///WebResponse.Headers 自动跳转链接
        /// </summary>
        public string LocationUrl
        {
            get { return _locationUrl; }
            set { _locationUrl = value; }
        }

        /// <summary>
        /// Http响应状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Http相应信息
        /// </summary>
        public string StatusMsg { get; set; }
    }
}
