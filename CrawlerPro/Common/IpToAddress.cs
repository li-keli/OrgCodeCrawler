using Newtonsoft.Json;

namespace CrawlerPro.Common
{
    public class IpToAddress
    {
        /// <summary>
        /// 查询当前请求IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static dynamic IpAddress(string ip)
        {
            string searchIp = "http://www.133ip.com/gn/jk.php?an=1&q=" + ip;
            ResponseItem myIp = new HttpRequestHelper().RequestDataByGet(
            new RequestItem
            {
                RequestUrl = searchIp,
                Method = HttpMethodEnum.Get,
                AutoRedirect = false,
                Timeout = 3 * 1000
            });
            if (myIp == null || string.IsNullOrWhiteSpace(myIp.ResultHtml))
            {
                return null;
            }
            var resultJson = myIp.ResultHtml;
            var model = JsonConvert.DeserializeObject<dynamic>(resultJson);
            return model;
        }
    }
}
