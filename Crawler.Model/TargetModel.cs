namespace Crawler.Model
{
    /// <summary>
    /// 抓取页面实体模型
    /// </summary>
    public class TargetModel
    {
        public BasicsInfo basicsInfo { set; get; }
        public CapitalInfo capitalInfo { set; get; }
        public OrgCodeInfo orgCodeInfo { set; get; }
        public TaxInfo taxInfo { set; get; }
        public string HtmlScore { set; get; }
    }

    /// <summary>
    /// 基础信息
    /// </summary>
    public class BasicsInfo
    {
        public string 市工商局_查询名称 { set; get; }
        public string 市工商局_名称 { get; set; }
        public string 市工商局_注册号 { get; set; }
        public string 市工商局_类型 { get; set; }
        public string 市工商局_法定代表人 { get; set; }
        public string 市工商局_成立日期 { get; set; }
        public string 市工商局_住所 { get; set; }
        public string 市工商局_营业期限自 { get; set; }
        public string 市工商局_营业期限至 { get; set; }
        public string 市工商局_经营范围 { get; set; }
        public string 市工商局_登记机关 { get; set; }
        public string 市工商局_核准日期 { get; set; }
        public string 市工商局_登记状态 { get; set; }
    }

    /// <summary>
    /// 资本相关信息
    /// </summary>
    public class CapitalInfo
    {
        public string 市工商局_注册资本 { get; set; }
        public string 市工商局_实收资本 { get; set; }
        public string 市工商局_实缴出资金额 { get; set; }
        public string 市工商局_最终实缴出资时间 { get; set; }
    }

    /// <summary>
    /// 组织机构代码信息
    /// </summary>
    public class OrgCodeInfo
    {
        public string 市质监局_组织机构代码 { get; set; }
        public string 市质监局_代码证颁发机关 { get; set; }
    }

    /// <summary>
    /// 税务登记信息
    /// </summary>
    public class TaxInfo
    {
        public string 市地税局_纳税人名称 { get; set; }
        public string 市地税局_税务登记类型 { get; set; }
        public string 市地税局_税务登记证号 { get; set; }
        public string 市地税局_注册号 { get; set; }
        public string 市地税局_法人姓名 { get; set; }
        public string 市地税局_组织机构代码 { get; set; }
        public string 市地税局_登记受理类型 { get; set; }
        public string 市地税局_经营地址 { get; set; }
        public string 市地税局_经营地址联系电话 { get; set; }
        public string 市地税局_经营地址邮编 { get; set; }
        public string 市地税局_企业主页网址 { get; set; }
        public string 市地税局_所处街乡 { get; set; }
        public string 市地税局_国地税共管户标识 { get; set; }
        public string 市地税局_登记注册类型 { get; set; }
        public string 市地税局_隶属关系 { get; set; }
        public string 市地税局_国家标准行业 { get; set; }
        public string 市地税局_税务登记日期 { get; set; }
        public string 市地税局_主管税务机关 { get; set; }
        public string 市地税局_纳税人状态 { get; set; }
    }
}
