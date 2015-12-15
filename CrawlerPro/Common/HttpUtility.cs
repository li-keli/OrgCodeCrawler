using System;
using System.IO;
using System.Net;
using System.Text;

namespace CrawlerPro.Common
{
    public class HttpUtility
    {
        public HttpUtility(string agentIp, Encoding encoding)
        {
            AgentIp = agentIp;
            Encoding = encoding;
        }

        /// <summary>
        /// 代理IP
        /// </summary>
        public string AgentIp { set; get; }
        /// <summary>
        /// 页面编码
        /// </summary>
        public Encoding Encoding { set; get; }

        ///  <summary>
        /// 通用HttpWebRequest
        ///  </summary>
        ///  <param name="method">请求方式（POST/GET）</param>
        ///  <param name="url">请求地址</param>
        ///  <param name="param">请求参数</param>
        /// <param name="cookieContainer"></param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        ///  <returns>请求返回的结果</returns>
        public string Request(string method, string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string> onComplete = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("URL");

            switch (method.ToUpper())
            {
                case "POST":
                    return Post(url, param, cookieContainer, onComplete);
                case "GET":
                    return Get(url, param, cookieContainer, onComplete);
                default:
                    throw new EntryPointNotFoundException("method:" + method);
            }
        }

        ///  <summary>
        /// 通用HttpWebRequest
        ///  </summary>
        ///  <param name="method">请求方式（POST/GET）</param>
        ///  <param name="url">请求地址</param>
        ///  <param name="param">请求参数</param>
        /// <param name="cookieContainer"></param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        ///  <returns>请求返回的结果</returns>
        public string Request(string method, string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string, string> onComplete = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("URL");

            switch (method.ToUpper())
            {
                case "POST":
                    return Post(url, param, cookieContainer, onComplete);
                case "GET":
                    return Get(url, param, cookieContainer, onComplete);
                default:
                    throw new EntryPointNotFoundException("method:" + method);
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="cookieContainer"></param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回的结果</returns>
        public string Post(string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string> onComplete = null)
        {
            UrlCheck(ref url);
            var Company = System.Web.HttpUtility.UrlEncode(param).ToUpper();
            string data = "callCount=1\r\nc0-scriptName=ServiceForNum\r\nc0-methodName=getData\r\nc0-id=2025_1440664403388\r\nc0-e1=string:jgmc%20%3D" + Company + "%20%20not%20ZYBZ%3D('2')%20\r\nc0-e2=string:jgmc%20%3D" + Company + "%20%20not%20ZYBZ%3D('2')%20\r\nc0-e3=string:" + Company + "\r\nc0-e4=string:2\r\nc0-e5=string:" + Company + "\r\nc0-e6=string:%E5%85%A8%E5%9B%BD\r\nc0-e7=string:alll\r\nc0-e8=string:\r\nc0-e9=boolean:false\r\nc0-e10=boolean:true\r\nc0-e11=boolean:false\r\nc0-e12=boolean:false\r\nc0-e13=string:\r\nc0-e14=string:\r\nc0-e15=string:\r\nc0-param0=Object:{firststrfind:reference:c0-e1, strfind:reference:c0-e2, key:reference:c0-e3, kind:reference:c0-e4, tit1:reference:c0-e5, selecttags:reference:c0-e6, xzqhName:reference:c0-e7, button:reference:c0-e8, jgdm:reference:c0-e9, jgmc:reference:c0-e10, jgdz:reference:c0-e11, zch:reference:c0-e12, strJgmc:reference:c0-e13, :reference:c0-e14, secondSelectFlag:reference:c0-e15}\r\nxml=true";
            byte[] bufferBytes = Encoding.UTF8.GetBytes(data);
            CookieContainer cookies = new CookieContainer();
            var request = WebRequest.Create(url) as HttpWebRequest;//HttpWebRequest方法继承自WebRequest, Create方法在WebRequest中定义，因此此处要显示的转换
            if (!string.IsNullOrEmpty(AgentIp))
            {
                request.Proxy = new WebProxy(AgentIp);
            }
            request.Method = "POST";
            request.ContentLength = bufferBytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 3 * 1000;
            request.ReadWriteTimeout = 3 * 1000;

            try
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.Default))
                {
                    writer.Write(data);
                    writer.Flush();
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }

            return HttpRequest(request, param, onComplete);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="cookieContainer"></param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回的结果</returns>
        public string Post(string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string, string> onComplete = null)
        {
            UrlCheck(ref url);
            var Company = System.Web.HttpUtility.UrlEncode(param).ToUpper();
            string data = "callCount=1\r\nc0-scriptName=ServiceForNum\r\nc0-methodName=getData\r\nc0-id=2025_1440664403388\r\nc0-e1=string:jgmc%20%3D" + Company + "%20%20not%20ZYBZ%3D('2')%20\r\nc0-e2=string:jgmc%20%3D" + Company + "%20%20not%20ZYBZ%3D('2')%20\r\nc0-e3=string:" + Company + "\r\nc0-e4=string:2\r\nc0-e5=string:" + Company + "\r\nc0-e6=string:%E5%85%A8%E5%9B%BD\r\nc0-e7=string:alll\r\nc0-e8=string:\r\nc0-e9=boolean:false\r\nc0-e10=boolean:true\r\nc0-e11=boolean:false\r\nc0-e12=boolean:false\r\nc0-e13=string:\r\nc0-e14=string:\r\nc0-e15=string:\r\nc0-param0=Object:{firststrfind:reference:c0-e1, strfind:reference:c0-e2, key:reference:c0-e3, kind:reference:c0-e4, tit1:reference:c0-e5, selecttags:reference:c0-e6, xzqhName:reference:c0-e7, button:reference:c0-e8, jgdm:reference:c0-e9, jgmc:reference:c0-e10, jgdz:reference:c0-e11, zch:reference:c0-e12, strJgmc:reference:c0-e13, :reference:c0-e14, secondSelectFlag:reference:c0-e15}\r\nxml=true";
            byte[] bufferBytes = Encoding.UTF8.GetBytes(data);
            CookieContainer cookies = new CookieContainer();
            var request = WebRequest.Create(url) as HttpWebRequest;//HttpWebRequest方法继承自WebRequest, Create方法在WebRequest中定义，因此此处要显示的转换
            if (!string.IsNullOrEmpty(AgentIp))
            {
                request.Proxy = new WebProxy(AgentIp);
            }
            request.Method = "POST";
            request.ContentLength = bufferBytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 3 * 1000;
            request.ReadWriteTimeout = 3 * 1000;

            try
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.Default))
                {
                    writer.Write(data);
                    writer.Flush();
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }

            return HttpRequest(request, param, onComplete);
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回结果</returns>
        public string Get(string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string> onComplete = null)
        {
            UrlCheck(ref url);

            if (!string.IsNullOrEmpty(param))
                if (!param.StartsWith("?"))
                    param = ("?" + param);
                else
                    param += param;

            var request = WebRequest.Create(url + param) as HttpWebRequest;
            request.Method = "GET";
            if (!string.IsNullOrEmpty(AgentIp))
            {
                request.Proxy = new WebProxy(AgentIp);
            }
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.Timeout = 3 * 1000;
            request.ReadWriteTimeout = 3 * 1000;

            return HttpRequest(request, param, onComplete);
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回结果</returns>
        public string Get(string url, string param, CookieContainer cookieContainer, Action<HttpStatusCode, string, string> onComplete = null)
        {
            UrlCheck(ref url);

            if (!string.IsNullOrEmpty(param))
                if (!param.StartsWith("?"))
                    param = ("?" + param);
                else
                    param += param;

            var request = WebRequest.Create(url + param) as HttpWebRequest;
            request.Method = "GET";
            if (!string.IsNullOrEmpty(AgentIp))
            {
                request.Proxy = new WebProxy(AgentIp);
            }
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            request.Timeout = 3 * 1000;
            request.ReadWriteTimeout = 3 * 1000;

            return HttpRequest(request, param, onComplete);
        }

        /// <summary>
        /// 请求的主体部分（由此完成请求并返回请求结果）
        /// </summary>
        /// <param name="request">请求的对象</param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回结果</returns>
        private string HttpRequest(HttpWebRequest request, string param, Action<HttpStatusCode, string> onComplete = null)
        {
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            if (response == null)
            {
                if (onComplete != null)
                    //请求远程返回为空
                    onComplete(HttpStatusCode.NotFound, null);
                return null;
            }

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding))
            {
                if (reader != null)
                {
                    result = reader.ReadToEnd();
                }
            }

            if (onComplete != null)
                onComplete(response.StatusCode, result);
            return result;
        }

        /// <summary>
        /// 请求的主体部分（由此完成请求并返回请求结果）
        /// </summary>
        /// <param name="request">请求的对象</param>
        /// <param name="onComplete">完成后执行的操作(可选参数，通过此方法可以获取到HTTP状态码)</param>
        /// <returns>请求返回结果</returns>
        private string HttpRequest(HttpWebRequest request, string param, Action<HttpStatusCode, string, string> onComplete = null)
        {
            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            if (response == null)
            {
                if (onComplete != null)
                    //请求远程返回为空
                    onComplete(HttpStatusCode.NotFound, null, param);
                return null;
            }

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding))
            {
                if (reader != null)
                {
                    result = reader.ReadToEnd();
                }
            }

            if (onComplete != null)
                onComplete(response.StatusCode, result, param);

            return result;

        }

        /// <summary>
        /// URL拼写完整性检查
        /// </summary>
        /// <param name="url">待检查的URL</param>
        private void UrlCheck(ref string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;
        }
    }
}
