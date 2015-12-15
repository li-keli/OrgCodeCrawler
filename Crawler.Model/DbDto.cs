namespace Crawler.Model
{
    /// <summary>
    /// 数据库连接方式传输对象
    /// </summary>
    public class DbDto
    {
        public string DataSource { set; get; }
        public string InitialCatalog { set; get; }
        public string UserID { set; get; }
        public string Password { set; get; }
    }
}
