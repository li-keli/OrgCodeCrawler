using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CrawlerPro.Common
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class HttpRequestHelper
    {
        #region  FIELD
        protected HttpWebRequest WebRequest = null;
        protected HttpWebResponse WebResponse = null;
        public const string UserAgentIe = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; .NET CLR 1.1.4322; InfoPath.2; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        public const string UserAgentChrome = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.116 Safari/537.36";
        public const string UserAgentFireFox = "Mozilla/5.0 (Windows NT 5.2; rv:18.0) Gecko/20100101 Firefox/18.0";
        #endregion

        #region   PUBLIC FUNCTION

        /// <summary>
        /// 创建HttpWebRequest请求
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="requestItem">请求参数</param>
        /// <param name="responseCallBack"></param>
        /// <returns></returns>
        public T CreateWebRequest<T>(RequestItem requestItem, Func<HttpWebResponse, T> responseCallBack)
        {
            T result = default(T);
            if (null == requestItem)
            {
                throw new ArgumentNullException("请求参数RequestItem为Null");
            }
            if (responseCallBack == null)
            {
                throw new ArgumentNullException("委托responseCallBack为Null");
            }
            try
            {
                SetCertificateParams(requestItem);
                if (null != WebRequest)
                {
                    SetWebProxyParams(requestItem);   //设置代理
                    SetServicePointParams();     //设置请求的服务端信息
                    SetRequestParams(requestItem);    // 设置HTTP请求头,HTTP版本信息等信息
                    SetPostRequestParams(requestItem);    //设置POST请求参数
                    result = GetWebResponse(responseCallBack);    //获取WebResponse响应
                }
                else
                {
                    throw new ArgumentNullException("创建HttpWebRequest请求失败 HttpWebRequest为Null");
                }
            }
            catch (Exception ex)
            {
                //OutLog("代理服务器拒绝握手，超小气。。。");
                //OutLog("HttpRequest高速缓存池出现致命错误:");
                //OutLog(ex.Message);
                return default(T);
            }
            finally
            {
                CloseWebResponse();     //关闭响应
                AbortWebRequest();      //取消请求
            }
            return result;
        }

        /// <summary>
        ///  使用POST方式请求数据
        /// </summary>
        /// <param name="requestItem"></param>
        /// <returns></returns>
        public ResponseItem RequestDataByPost(RequestItem requestItem)
        {
            return RequestDataByPost<ResponseItem>(requestItem, ResponseCallBack);
        }

        /// <summary>
        /// 使用POST方式请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestItem"></param>
        /// <param name="responseCallBack"></param>
        /// <returns></returns>
        public T RequestDataByPost<T>(RequestItem requestItem, Func<HttpWebResponse, T> responseCallBack)
        {
            T result = default(T);
            if (null != requestItem)
            {
                requestItem.Method = HttpMethodEnum.Post;
                result = CreateWebRequest(requestItem, responseCallBack);
            }
            else
            {
                throw new ArgumentNullException("POST请求-请求参数RequestItem为Null");
            }
            return result;
        }

        /// <summary>
        /// 使用GET方式请求数据
        /// </summary>
        /// <param name="requestItem"></param>
        /// <returns></returns>
        public ResponseItem RequestDataByGet(RequestItem requestItem)
        {
            return RequestDataByGet<ResponseItem>(requestItem, ResponseCallBack);
        }

        /// <summary>
        /// 使用GET方式请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestItem"></param>
        /// <param name="responseCallBack"></param>
        /// <returns></returns>
        public T RequestDataByGet<T>(RequestItem requestItem, Func<HttpWebResponse, T> responseCallBack)
        {
            T result = default(T);
            if (null != requestItem)
            {
                requestItem.Method = HttpMethodEnum.Get;
                result = CreateWebRequest(requestItem, responseCallBack);
            }
            else
            {
                throw new ArgumentNullException("GET请求-请求参数RequestItem为Null");
            }
            return result;
        }

        #endregion

        #region PROTECTED FUNCTION

        /// <summary>
        /// 设置WebRequest请求证书信息
        /// </summary>
        /// <param name="requestItem"></param>
        protected void SetCertificateParams(RequestItem requestItem)
        {
            if (requestItem.IsNeedCertificate)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            WebRequest = System.Net.WebRequest.Create(requestItem.RequestUrl) as HttpWebRequest;
        }

        /// <summary>
        /// 设置HTTP请求代理
        /// </summary>
        /// <param name="requestItem"></param>
        protected void SetWebProxyParams(RequestItem requestItem)
        {
            if (null != requestItem.ProxyInfo)
            {
                ProxySetInfo proxyInfo = requestItem.ProxyInfo;
                if (!string.IsNullOrEmpty(proxyInfo.ProxyIp))
                {
                    WebProxy webProxy = new WebProxy(proxyInfo.ProxyIp);
                    webProxy.Credentials = new NetworkCredential(proxyInfo.ProxyUserName, proxyInfo.ProxyPassword);
                    WebRequest.Proxy = webProxy;
                    WebRequest.Credentials = CredentialCache.DefaultCredentials;  //设置安全凭证
                }
            }
        }

        /// <summary>
        ///  设置HTTP请求头,HTTP版本信息等信息
        /// </summary>
        /// <param name="requestItem"></param>
        protected void SetRequestParams(RequestItem requestItem)
        {
            WebRequest.Method = requestItem.Method.ToString();   //请求类型
            WebRequest.ProtocolVersion = HttpVersion.Version11;  //请求HTTP版本
            WebRequest.Timeout = requestItem.Timeout;            //超时时间

            if (requestItem.RequestCookies == null)
            {
                requestItem.RequestCookies = new CookieContainer();
            }
            WebRequest.CookieContainer = requestItem.RequestCookies;   //设置与请求关联的Cookies

            WebRequest.KeepAlive = requestItem.KeepAlive;     //是否保持持久链接
            WebRequest.AllowAutoRedirect = requestItem.AutoRedirect;       //是否允许自动跳转
            if (!string.IsNullOrEmpty(requestItem.Referer))
            {
                WebRequest.Referer = requestItem.Referer;   //请求来源Url
            }
            if (!string.IsNullOrEmpty(requestItem.ContentType))
            {
                WebRequest.ContentType = requestItem.ContentType;
            }
            if (!string.IsNullOrEmpty(requestItem.RequestAccept))
            {
                WebRequest.Accept = requestItem.RequestAccept;    //设置Accept标头
            }
            if (string.IsNullOrEmpty(requestItem.UserAgent))
            {
                requestItem.UserAgent = UserAgentIe;
            }
            WebRequest.UserAgent = requestItem.UserAgent;  //设置User-Agent标头

            WebHeaderCollection headerColl = requestItem.GetHeaderData();
            if (null != headerColl && headerColl.Count > 0)
            {
                WebRequest.Headers.Add(headerColl);
            }
        }

        /// <summary>
        /// 通过配置不使用缓冲、最大连接数提高效率
        /// </summary>
        protected void SetServicePointParams()
        {
            WebRequest.ServicePoint.ConnectionLimit = 1024;
            WebRequest.ServicePoint.Expect100Continue = false;
            WebRequest.ServicePoint.UseNagleAlgorithm = false;
            WebRequest.AllowWriteStreamBuffering = false;
        }

        /// <summary>
        /// 设置POST请求参数
        /// </summary>
        /// <param name="requestItem"></param>
        protected void SetPostRequestParams(RequestItem requestItem)
        {
            if (WebRequest.Method.ToLower() == "post")
            {
                if (string.IsNullOrEmpty(WebRequest.ContentType))
                {
                    WebRequest.ContentType = "application/x-www-form-urlencoded";
                }
                WebRequest.ReadWriteTimeout = requestItem.ReadWriteTimeout;
                byte[] buffer = Encoding.UTF8.GetBytes(requestItem.GetParamsData());
                WebRequest.ContentLength = buffer.Length;
                Stream writer = WebRequest.GetRequestStream();
                writer.Write(buffer, 0, buffer.Length);
                writer.Close();
            }
        }

        #endregion

        #region  PRIVATE FUNCTION

        /// <summary>
        /// 获取WebResponse响应
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseCallBack"></param>
        /// <returns></returns>
        private T GetWebResponse<T>(Func<HttpWebResponse, T> responseCallBack)
        {
            int webExStatus = 0;    //响应状态
            try
            {
                WebResponse = WebRequest.GetResponse() as HttpWebResponse;
            }
            catch (WebException webEx)
            {
                webExStatus = (int)webEx.Status;
                WebResponse = (HttpWebResponse)webEx.Response;
            }
            if (null != WebResponse)
            {
                return responseCallBack(WebResponse);
            }
            else
            {
                throw new ArgumentNullException("获取HttpWebResponse响应失败 HttpWebResponse为Null,响应状态：" + webExStatus.ToString());
            }
        }

        /// <summary>
        ///  取消HttpWebRequest请求
        /// </summary>
        private void AbortWebRequest()
        {
            if (null != WebRequest)
            {
                try
                {
                    WebRequest.Abort();
                    WebRequest = null;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 关闭HttpWebResponse相应
        /// </summary>
        private void CloseWebResponse()
        {
            if (null != WebResponse)
            {
                try
                {
                    WebResponse.Close();
                    WebResponse = null;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 读取 HttpWebResponse 返回数据
        /// </summary>
        /// <param name="webResponse"></param>
        /// <returns></returns>
        private ResponseItem ResponseCallBack(HttpWebResponse webResponse)
        {
            ResponseItem resultItem = new ResponseItem();
            //默认编码
            Encoding encoding = Encoding.Default;
            resultItem.CookieColl = webResponse.Cookies;
            resultItem.HeaderColl = webResponse.Headers;
            resultItem.LocationUrl = webResponse.Headers[HttpResponseHeader.Location];
            resultItem.SetCookies = webResponse.Headers[HttpResponseHeader.SetCookie];
            resultItem.StatusCode = webResponse.StatusCode;
            resultItem.StatusMsg = webResponse.StatusDescription;
            Stream stream = webResponse.GetResponseStream();
            int readCount = 0;
            const int length = 1024;
            byte[] bufferBytes = new byte[length];
            MemoryStream memoryStream = new MemoryStream();
            while ((readCount = stream.Read(bufferBytes, 0, length)) > 0)
            {
                memoryStream.Write(bufferBytes, 0, readCount);
            }
            resultItem.ResultBytes = memoryStream.ToArray();
            string contentType = webResponse.Headers["Content-Type"];
            Regex regex = new Regex("^charset\\s*=\\s*([\\v-]+)", RegexOptions.IgnoreCase);
            if (regex.IsMatch(contentType))
            {
                encoding = Encoding.GetEncoding(regex.Match(contentType).Groups[1].Value.Trim());
            }
            //Encoding.UTF8.GetString(resultItem.ResultBytes, 0, resultItem.ResultBytes.Length);
            resultItem.ResultHtml = encoding.GetString(resultItem.ResultBytes, 0, resultItem.ResultBytes.Length);
            memoryStream.Close();
            stream.Close();
            return resultItem;
        }
        #endregion
    }
}
