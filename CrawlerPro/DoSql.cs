using Dapper;
using System;
using Crawler.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace CrawlerPro
{
    public class DoSql
    {
        public static string Ds = "Data Source =" + Environment.CurrentDirectory + "\\db\\" + Guid.NewGuid() + ".db";
        public delegate int Execute<in T>(T t);

        public static void ChangeDb()
        {
            Ds = "Data Source =" + Environment.CurrentDirectory + "\\db\\" + Guid.NewGuid() + ".db";
        }

        #region 币种编号
        /// <summary>
        /// 币种编号
        /// </summary>
        private static readonly Dictionary<string, string> Dic = new Dictionary<string, string>
        {
            {"澳洲元", "AUD"},
            {"奥地利先令", ""},
            {"比利时法郎", ""},
            {"加拿大元", "CAD"},
            {"人民币", "CNY"},
            {"新台湾币", ""},
            {"丹麦克朗", "DKK"},
            {"芬兰马克", "FIM"},
            {"法国法郎", "FRF"},
            {"德国马克", "DEM"},
            {"港币", "HKD"},
            {"意大利里拉", "ITL"},
            {"日元", "JPY"},
            {"澳门元", "MOP"},
            {"马来西亚币", "MYR"},
            {"荷兰盾", ""},
            {"新西兰元", "NZD"},
            {"挪威克朗", ""},
            {"菲律宾比索", "PHP"},
            {"新加坡元", "SGD"},
            {"西班牙比塞塔", ""},
            {"瑞典克朗", "RKS"},
            {"瑞士法郎", "SEK"},
            {"泰国铢", "THB"},
            {"英镑", "GBP"},
            {"美元", "USD"},
            {"欧元", "EURO"},
            {"瑞朗", "CHF"}
        };
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static void ValidateDb() {
            ChangeDb();
            ValidateQgTable();
            ValidateBjTable();
        }

        /// <summary>
        /// 初始化全国数据库
        /// </summary>
        public static void ValidateQgTable()
        {
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(@" CREATE TABLE IF NOT EXISTS Crawler_Qg( 
	                [Id] INTEGER  PRIMARY KEY AUTOINCREMENT, 
	                [查询名称] varchar(500) NULL, 
	                [机构名称] varchar(500) NULL, 
	                [组织机构代码] varchar(50) NULL, 
	                [机构登记证号] varchar(50) NULL, 
	                [机构类型] varchar(500) NULL, 
	                [地址] varchar(500) NULL, 
	                [注册日期] varchar(50) NULL, 
	                [备注日期] varchar(50) NULL, 
	                [截止有效期] varchar(50) NULL, 
	                [Ly] varchar(500) NULL, 
	                [EntryJgdm] varchar(500) NULL, 
	                [Eeservea] varchar(500) NULL, 
	                [RowNum] varchar(500) NULL 
                ) ");
            }
        }

        /// <summary>
        /// 初始化北京数据库
        /// </summary>
        public static void ValidateBjTable()
        {
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(@" CREATE TABLE IF NOT EXISTS Crawler_Bj(
	                            [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
	                            [查询名称] varchar(2000) NULL,
	                            [市工商局_名称] varchar(2000) NULL,
	                            [市工商局_注册号] varchar(2000) NULL,
	                            [市工商局_类型] varchar(2000) NULL,
	                            [市工商局_法定代表人] varchar(2000) NULL,
	                            [市工商局_成立日期] varchar(2000) NULL,
	                            [市工商局_住所] varchar(2000) NULL,
	                            [市工商局_营业期限自] varchar(2000) NULL,
	                            [市工商局_营业期限至] varchar(2000) NULL,
	                            [市工商局_经营范围] varchar(4000) NULL,
	                            [市工商局_登记机关] varchar(2000) NULL,
	                            [市工商局_核准日期] varchar(2000) NULL,
	                            [市工商局_登记状态] varchar(2000) NULL,
	                            [市工商局_注册资本] varchar(2000) NULL,
	                            [市工商局_实收资本] varchar(2000) NULL,
	                            [市工商局_货币单位] varchar(2000) NULL,
	                            [市工商局_币种] varchar(2000) NULL,
	                            [市工商局_实缴出资金额] varchar(2000) NULL,
	                            [市工商局_最终实缴出资时间] varchar(2000) NULL,
	                            [市质监局_组织机构代码] varchar(2000) NULL,
	                            [市质监局_代码证颁发机关] varchar(2000) NULL,
	                            [市地税局_纳税人名称] varchar(2000) NULL,
	                            [市地税局_税务登记类型] varchar(2000) NULL,
	                            [市地税局_税务登记证号] varchar(2000) NULL,
	                            [市地税局_注册号] varchar(2000) NULL,
	                            [市地税局_法人姓名] varchar(2000) NULL,
	                            [市地税局_组织机构代码] varchar(2000) NULL,
	                            [市地税局_登记受理类型] varchar(2000) NULL,
	                            [市地税局_经营地址] varchar(2000) NULL,
	                            [市地税局_经营地址联系电话] varchar(2000) NULL,
	                            [市地税局_经营地址邮编] varchar(2000) NULL,
	                            [市地税局_企业主页网址] varchar(2000) NULL,
	                            [市地税局_所处街乡] varchar(2000) NULL,
	                            [市地税局_国地税共管户标识] varchar(2000) NULL,
	                            [市地税局_登记注册类型] varchar(2000) NULL,
	                            [市地税局_隶属关系] varchar(2000) NULL,
	                            [市地税局_国家标准行业] varchar(2000) NULL,
	                            [市地税局_税务登记日期] varchar(2000) NULL,
	                            [市地税局_主管税务机关] varchar(2000) NULL,
	                            [市地税局_纳税人状态] varchar(2000) NULL,
	                            [是否为历史名称] varchar(2000) NULL,
                                [HTMLScore] text null)
                            ");
            }
        }
        #endregion

        #region 全国
        /// <summary>
        /// 全国资料写入
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public static bool InsertQg(object listModel)
        {
            var sql = @" INSERT INTO Crawler_Qg(查询名称,机构名称,组织机构代码,机构登记证号,机构类型,地址,注册日期,备注日期,截止有效期,Ly,EntryJgdm,RowNum) VALUES(@companyName,@jgmc,@jgdm,@zch,@jglx,@jgdz,@zcrq,@bzrq,@zfrq,@Ly,@EntryJgdm,@RowNum) ";
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(sql, listModel);
            }
            return false;
        }

        /// <summary>
        /// 查询全国信息
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<CrawlerQgOut> SelectQgInfo(string whereStr = null, object param = null)
        {
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                return conn.Query<CrawlerQgOut>(" select * from Crawler_Qg where 1=1 " + whereStr, param).ToList();
            }
        }
        #endregion

        #region 北京
        /// <summary>
        /// 北京企业信息插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertBj(TargetModel model)
        {
            var sql = @" insert into Crawler_Bj(查询名称, 市工商局_名称, 市工商局_注册号, 市工商局_类型, 市工商局_法定代表人, 市工商局_成立日期, 市工商局_住所, 市工商局_营业期限自, 市工商局_营业期限至, 市工商局_经营范围, 市工商局_登记机关, 市工商局_核准日期, 市工商局_登记状态, 市工商局_注册资本, 市工商局_实收资本, 市工商局_货币单位, 市工商局_币种, 市工商局_实缴出资金额, 市工商局_最终实缴出资时间, 市质监局_组织机构代码, 市质监局_代码证颁发机关, 市地税局_纳税人名称, 市地税局_税务登记类型, 市地税局_税务登记证号, 市地税局_注册号, 市地税局_法人姓名, 市地税局_组织机构代码, 市地税局_登记受理类型, 市地税局_经营地址, 市地税局_经营地址联系电话, 市地税局_经营地址邮编, 市地税局_企业主页网址, 市地税局_所处街乡, 市地税局_国地税共管户标识, 市地税局_登记注册类型, 市地税局_隶属关系, 市地税局_国家标准行业, 市地税局_税务登记日期, 市地税局_主管税务机关, 市地税局_纳税人状态, 是否为历史名称, HTMLScore) values(@查询名称, @市工商局_名称, @市工商局_注册号, @市工商局_类型, @市工商局_法定代表人, @市工商局_成立日期, @市工商局_住所, @市工商局_营业期限自, @市工商局_营业期限至, @市工商局_经营范围, @市工商局_登记机关, @市工商局_核准日期, @市工商局_登记状态, @市工商局_注册资本, @市工商局_实收资本, @市工商局_货币单位, @市工商局_币种, @市工商局_实缴出资金额, @市工商局_最终实缴出资时间, @市质监局_组织机构代码, @市质监局_代码证颁发机关, @市地税局_纳税人名称, @市地税局_税务登记类型, @市地税局_税务登记证号, @市地税局_注册号, @市地税局_法人姓名, @市地税局_组织机构代码, @市地税局_登记受理类型, @市地税局_经营地址, @市地税局_经营地址联系电话, @市地税局_经营地址邮编, @市地税局_企业主页网址, @市地税局_所处街乡, @市地税局_国地税共管户标识, @市地税局_登记注册类型, @市地税局_隶属关系, @市地税局_国家标准行业, @市地税局_税务登记日期, @市地税局_主管税务机关, @市地税局_纳税人状态, @是否为历史名称, @HTMLScore) ";

            string 市工商局_货币单位 = "";
            string 市工商局_币种 = "";
            #region 市工商局_注册资本分割
            var zczb = (model.capitalInfo.市工商局_注册资本 ?? "").TrimStart().TrimEnd().Split(' ');
            if (zczb.Length >= 1)
                model.capitalInfo.市工商局_注册资本 = zczb[0];
            if (zczb.Length >= 2)
                市工商局_货币单位 = zczb[1];
            if (zczb.Length >= 3)
                市工商局_币种 = string.IsNullOrEmpty(Dic[zczb[2]]) ? zczb[2] : Dic[zczb[2]];
            #endregion
            #region 市工商局_实收资本分割
            var sszb = (model.capitalInfo.市工商局_实收资本 ?? "").TrimStart().TrimEnd().Split(' ');
            if (zczb.Length >= 1)
                model.capitalInfo.市工商局_实收资本 = sszb[0];
            #endregion

            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(sql, new
                {
                    查询名称 = model.basicsInfo.市工商局_查询名称,
                    市工商局_名称 = (model.basicsInfo.市工商局_名称 ?? "").Trim(),
                    市工商局_注册号 = (model.basicsInfo.市工商局_注册号 ?? "").Trim(),
                    市工商局_类型 = (model.basicsInfo.市工商局_类型 ?? "").Trim(),
                    市工商局_法定代表人 = (model.basicsInfo.市工商局_法定代表人 ?? "").Trim(),
                    市工商局_成立日期 = (model.basicsInfo.市工商局_成立日期 ?? "").Trim(),
                    市工商局_住所 = (model.basicsInfo.市工商局_住所 ?? "").Trim(),
                    市工商局_营业期限自 = (model.basicsInfo.市工商局_营业期限自 ?? "").Trim(),
                    市工商局_营业期限至 = (model.basicsInfo.市工商局_营业期限至 ?? "").Trim(),
                    市工商局_经营范围 = (model.basicsInfo.市工商局_经营范围 ?? "").Trim(),
                    市工商局_登记机关 = (model.basicsInfo.市工商局_登记机关 ?? "").Trim(),
                    市工商局_核准日期 = (model.basicsInfo.市工商局_核准日期 ?? "").Trim(),
                    市工商局_登记状态 = (model.basicsInfo.市工商局_登记状态 ?? "").Trim(),
                    市工商局_注册资本 = (model.capitalInfo.市工商局_注册资本 ?? "").Trim(),
                    市工商局_实收资本 = (model.capitalInfo.市工商局_实收资本 ?? "").Trim(),
                    市工商局_货币单位 = 市工商局_货币单位.Trim(),
                    市工商局_币种 = 市工商局_币种.Trim(),
                    市工商局_实缴出资金额 = (model.capitalInfo.市工商局_实缴出资金额 ?? "").Trim(),
                    市工商局_最终实缴出资时间 = (model.capitalInfo.市工商局_最终实缴出资时间 ?? "").Trim(),
                    市质监局_组织机构代码 = (model.orgCodeInfo.市质监局_组织机构代码 ?? "").Trim(),
                    市质监局_代码证颁发机关 = (model.orgCodeInfo.市质监局_代码证颁发机关 ?? "").Trim(),
                    市地税局_纳税人名称 = (model.taxInfo.市地税局_纳税人名称 ?? "").Trim(),
                    市地税局_税务登记类型 = (model.taxInfo.市地税局_税务登记类型 ?? "").Trim(),
                    市地税局_税务登记证号 = (model.taxInfo.市地税局_税务登记证号 ?? "").Trim(),
                    市地税局_注册号 = (model.taxInfo.市地税局_注册号 ?? "").Trim(),
                    市地税局_法人姓名 = (model.taxInfo.市地税局_法人姓名 ?? "").Trim(),
                    市地税局_组织机构代码 = (model.taxInfo.市地税局_组织机构代码 ?? "").Trim(),
                    市地税局_登记受理类型 = (model.taxInfo.市地税局_登记受理类型 ?? "").Trim(),
                    市地税局_经营地址 = (model.taxInfo.市地税局_经营地址 ?? "").Trim(),
                    市地税局_经营地址联系电话 = (model.taxInfo.市地税局_经营地址联系电话 ?? "").Trim(),
                    市地税局_经营地址邮编 = (model.taxInfo.市地税局_经营地址邮编 ?? "").Trim(),
                    市地税局_企业主页网址 = (model.taxInfo.市地税局_企业主页网址 ?? "").Trim(),
                    市地税局_所处街乡 = (model.taxInfo.市地税局_所处街乡 ?? "").Trim(),
                    市地税局_国地税共管户标识 = (model.taxInfo.市地税局_国地税共管户标识 ?? "").Trim(),
                    市地税局_登记注册类型 = (model.taxInfo.市地税局_登记注册类型 ?? "").Trim(),
                    市地税局_隶属关系 = (model.taxInfo.市地税局_隶属关系 ?? "").Trim(),
                    市地税局_国家标准行业 = (model.taxInfo.市地税局_国家标准行业 ?? "").Trim(),
                    市地税局_税务登记日期 = (model.taxInfo.市地税局_税务登记日期 ?? "").Trim(),
                    市地税局_主管税务机关 = (model.taxInfo.市地税局_主管税务机关 ?? "").Trim(),
                    市地税局_纳税人状态 = (model.taxInfo.市地税局_纳税人状态 ?? "").Trim(),
                    HTMLScore = model.HtmlScore ?? "",
                    是否为历史名称 = (model.basicsInfo.市工商局_查询名称 ?? "").Trim().Equals((model.basicsInfo.市工商局_名称 ?? "").Trim()) ? "否" : "是"
                });
            }
            return true;
        }

        /// <summary>
        /// 查询北京信息
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<CrawlerBjOut> SelectBjInfo(string whereStr = null, object param = null)
        {
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                return conn.Query<CrawlerBjOut>(" select * from Crawler_Bj where 1=1 " + whereStr, param).ToList();
            }
        }
        #endregion

        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> SelectInfoByDb<T>(string sql, string db, object param = null)
            where T : class
        {
            db = "Data Source =" + Environment.CurrentDirectory + "\\db\\" + db;
            using (IDbConnection conn = new SQLiteConnection(db))
            {
                conn.Open();
                return conn.Query<T>(sql + " where 1=1 ", param).ToList();
            }
        }
    }
}