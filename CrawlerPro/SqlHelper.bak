using System.Data.SqlClient;
using Crawler.Model;
using System.Data;
using System;
using System.Collections.Generic;
using CrawlerPro.Common;
using System.Collections;

namespace CrawlerPro
{
    public static class SqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string ConnctionString = XmlHelper.GetDBConnect();

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

        #region SqlHelper操作

        /// <summary>
        /// 初始化任务数据库 - 全国库
        /// </summary>
        /// <returns></returns>
        public static bool DelQgInfo()
        {
            try
            {
                const string strSql = "delete Crawler_Qg;";
                ExecuteSql(strSql);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 初始化任务数据库 - 北京库
        /// </summary>
        /// <returns></returns>
        public static bool DelBjInfo()
        {
            try
            {
                const string strSql = "delete Crawler_Result;";
                ExecuteSql(strSql);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 全国组织机构代码单元数据插入 （SqlHelper）
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static bool InserInfo(List<Dto> lists)
        {
            try
            {
                const string strsql = " insert into dbo.Crawler_Qg(查询名称, 机构名称, 组织机构代码, 机构登记证号, 机构类型, 地址, 注册日期, 备注日期, 截止有效期, Ly, EntryJgdm, Eeservea, RowNum) values(@查询名称,@机构名称,@组织机构代码,@机构登记证号,@机构类型,@地址,@注册日期,@备注日期,@截止有效期,@Ly,@EntryJgdm,@Eeservea,@RowNum) ";
                foreach (var model in lists)
                {
                    SqlParameter[] sqlparameters = {
                        new SqlParameter("@查询名称",model.companyName),
                        new SqlParameter("@机构名称",model.jgmc),
                        new SqlParameter("@组织机构代码",model.jgdm),
                        new SqlParameter("@机构登记证号",model.zch),
                        new SqlParameter("@机构类型",model.jglx),
                        new SqlParameter("@地址",model.jgdz),
                        new SqlParameter("@注册日期",model.zcrq),
                        new SqlParameter("@备注日期",model.bzrq),
                        new SqlParameter("@截止有效期",model.zfrq),
                        new SqlParameter("@Ly",model.ly),
                        new SqlParameter("@EntryJgdm",model.entryJgdm),
                        new SqlParameter("@Eeservea",model.reservea),
                        new SqlParameter("@RowNum",model.rowNum)
                    };
                    if (ExecuteSql(strsql, sqlparameters) > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        /// <summary>
        /// 北京企业信息网单元数据插入（SqlHelper）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InserInfo(TargetModel model)
        {
            model = model.Cutting();
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

            string strsql = " insert into dbo.Crawler_Result(查询名称, 市工商局_名称, 市工商局_注册号, 市工商局_类型, 市工商局_法定代表人, 市工商局_成立日期, 市工商局_住所, 市工商局_营业期限自, 市工商局_营业期限至, 市工商局_经营范围, 市工商局_登记机关, 市工商局_核准日期, 市工商局_登记状态, 市工商局_注册资本, 市工商局_实收资本, 市工商局_货币单位, 市工商局_币种, 市工商局_实缴出资金额, 市工商局_最终实缴出资时间, 市质监局_组织机构代码, 市质监局_代码证颁发机关, 市地税局_纳税人名称, 市地税局_税务登记类型, 市地税局_税务登记证号, 市地税局_注册号, 市地税局_法人姓名, 市地税局_组织机构代码, 市地税局_登记受理类型, 市地税局_经营地址, 市地税局_经营地址联系电话, 市地税局_经营地址邮编, 市地税局_企业主页网址, 市地税局_所处街乡, 市地税局_国地税共管户标识, 市地税局_登记注册类型, 市地税局_隶属关系, 市地税局_国家标准行业, 市地税局_税务登记日期, 市地税局_主管税务机关, 市地税局_纳税人状态, 是否为历史名称) values(@查询名称, @市工商局_名称, @市工商局_注册号, @市工商局_类型, @市工商局_法定代表人, @市工商局_成立日期, @市工商局_住所, @市工商局_营业期限自, @市工商局_营业期限至, @市工商局_经营范围, @市工商局_登记机关, @市工商局_核准日期, @市工商局_登记状态, @市工商局_注册资本, @市工商局_实收资本, @市工商局_货币单位, @市工商局_币种, @市工商局_实缴出资金额, @市工商局_最终实缴出资时间, @市质监局_组织机构代码, @市质监局_代码证颁发机关, @市地税局_纳税人名称, @市地税局_税务登记类型, @市地税局_税务登记证号, @市地税局_注册号, @市地税局_法人姓名, @市地税局_组织机构代码, @市地税局_登记受理类型, @市地税局_经营地址, @市地税局_经营地址联系电话, @市地税局_经营地址邮编, @市地税局_企业主页网址, @市地税局_所处街乡, @市地税局_国地税共管户标识, @市地税局_登记注册类型, @市地税局_隶属关系, @市地税局_国家标准行业, @市地税局_税务登记日期, @市地税局_主管税务机关, @市地税局_纳税人状态, @是否为历史名称) ";
            SqlParameter[] sqlparameters = {
                new SqlParameter("@查询名称",                model.basicsInfo.市工商局_查询名称),
                new SqlParameter("@市工商局_名称",            (model.basicsInfo.市工商局_名称 ?? "").Trim()),
                new SqlParameter("@市工商局_注册号",           (model.basicsInfo.市工商局_注册号 ?? "").Trim()),
                new SqlParameter("@市工商局_类型",            (model.basicsInfo.市工商局_类型 ?? "").Trim()),
                new SqlParameter("@市工商局_法定代表人",         (model.basicsInfo.市工商局_法定代表人 ?? "").Trim()),
                new SqlParameter("@市工商局_成立日期",          (model.basicsInfo.市工商局_成立日期 ?? "").Trim()),
                new SqlParameter("@市工商局_住所",            (model.basicsInfo.市工商局_住所 ?? "").Trim()),
                new SqlParameter("@市工商局_营业期限自",         (model.basicsInfo.市工商局_营业期限自 ?? "").Trim()),
                new SqlParameter("@市工商局_营业期限至",         (model.basicsInfo.市工商局_营业期限至 ?? "").Trim()),
                new SqlParameter("@市工商局_经营范围",          (model.basicsInfo.市工商局_经营范围 ?? "").Trim()),
                new SqlParameter("@市工商局_登记机关",          (model.basicsInfo.市工商局_登记机关 ?? "").Trim()),
                new SqlParameter("@市工商局_核准日期",          (model.basicsInfo.市工商局_核准日期 ?? "").Trim()),
                new SqlParameter("@市工商局_登记状态",          (model.basicsInfo.市工商局_登记状态 ?? "").Trim()),
                new SqlParameter("@市工商局_注册资本",          (model.capitalInfo.市工商局_注册资本 ?? "").Trim()),
                new SqlParameter("@市工商局_实收资本",          (model.capitalInfo.市工商局_实收资本 ?? "").Trim()),
                new SqlParameter("@市工商局_货币单位",          市工商局_货币单位.Trim()),
                new SqlParameter("@市工商局_币种",              市工商局_币种.Trim()),
                new SqlParameter("@市工商局_实缴出资金额",        (model.capitalInfo.市工商局_实缴出资金额 ?? "").Trim()),
                new SqlParameter("@市工商局_最终实缴出资时间",      (model.capitalInfo.市工商局_最终实缴出资时间 ?? "").Trim()),
                new SqlParameter("@市质监局_组织机构代码",        (model.orgCodeInfo.市质监局_组织机构代码 ?? "").Trim()),
                new SqlParameter("@市质监局_代码证颁发机关",       (model.orgCodeInfo.市质监局_代码证颁发机关 ?? "").Trim()),
                new SqlParameter("@市地税局_纳税人名称",         (model.taxInfo.市地税局_纳税人名称 ?? "").Trim()),
                new SqlParameter("@市地税局_税务登记类型",        (model.taxInfo.市地税局_税务登记类型 ?? "").Trim()),
                new SqlParameter("@市地税局_税务登记证号",        (model.taxInfo.市地税局_税务登记证号 ?? "").Trim()),
                new SqlParameter("@市地税局_注册号",           (model.taxInfo.市地税局_注册号 ?? "").Trim()),
                new SqlParameter("@市地税局_法人姓名",          (model.taxInfo.市地税局_法人姓名 ?? "").Trim()),
                new SqlParameter("@市地税局_组织机构代码",        (model.taxInfo.市地税局_组织机构代码 ?? "").Trim()),
                new SqlParameter("@市地税局_登记受理类型",        (model.taxInfo.市地税局_登记受理类型 ?? "").Trim()),
                new SqlParameter("@市地税局_经营地址",          (model.taxInfo.市地税局_经营地址 ?? "").Trim()),
                new SqlParameter("@市地税局_经营地址联系电话",      (model.taxInfo.市地税局_经营地址联系电话 ?? "").Trim()),
                new SqlParameter("@市地税局_经营地址邮编",        (model.taxInfo.市地税局_经营地址邮编 ?? "").Trim()),
                new SqlParameter("@市地税局_企业主页网址",        (model.taxInfo.市地税局_企业主页网址 ?? "").Trim()),
                new SqlParameter("@市地税局_所处街乡",          (model.taxInfo.市地税局_所处街乡 ?? "").Trim()),
                new SqlParameter("@市地税局_国地税共管户标识",      (model.taxInfo.市地税局_国地税共管户标识 ?? "").Trim()),
                new SqlParameter("@市地税局_登记注册类型",        (model.taxInfo.市地税局_登记注册类型 ?? "").Trim()),
                new SqlParameter("@市地税局_隶属关系",          (model.taxInfo.市地税局_隶属关系 ?? "").Trim()),
                new SqlParameter("@市地税局_国家标准行业",        (model.taxInfo.市地税局_国家标准行业 ?? "").Trim()),
                new SqlParameter("@市地税局_税务登记日期",        (model.taxInfo.市地税局_税务登记日期 ?? "").Trim()),
                new SqlParameter("@市地税局_主管税务机关",        (model.taxInfo.市地税局_主管税务机关 ?? "").Trim()),
                new SqlParameter("@市地税局_纳税人状态",         (model.taxInfo.市地税局_纳税人状态 ?? "").Trim()),
                new SqlParameter("@是否为历史名称",                (model.basicsInfo.市工商局_查询名称 ?? "").Trim().Equals((model.basicsInfo.市工商局_名称 ?? "").Trim()) ? "否" : "是")
            };
            return ExecuteSql(strsql, sqlparameters) > 0;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataTable ReadTable(string tableName)
        {
            string strSql = $" select * from {tableName} ";
            return ExecuteQuery(strSql, null);
        }

        /// <summary>
        /// 判断该表内容是否为空
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static bool IsExist(string tableName)
        {
            string strSql = $" select top 10 * from {tableName} ";
            return ExecuteQuery(strSql, null).Rows.Count > 0;
        }

        /// <summary>
        /// 分割资本
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TargetModel Cutting(this TargetModel model)
        {
            model.basicsInfo = model.basicsInfo ?? new BasicsInfo();
            model.capitalInfo = model.capitalInfo ?? new CapitalInfo();
            model.orgCodeInfo = model.orgCodeInfo ?? new OrgCodeInfo();
            model.taxInfo = model.taxInfo ?? new TaxInfo();
            return model;
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnctionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException)
                    {
                        connection.Close();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnctionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        throw;
                    }
                }
            }
        }

        public static DataTable ExecuteQuery(string sql, SqlParameter[] paras)
        {
            using (SqlConnection connection = new SqlConnection(ConnctionString))
            {
                DataTable dt = new DataTable();
                var sqlcom = new SqlCommand(sql, connection);
                //sqlcom.Parameters.AddRange(paras);
                connection.Open();
                using (var sdr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    dt.Load(sdr);
                }
                return dt;
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion
    }

    public class DBHelper
    {
        #region 测试数据库连接

        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static bool ConnectionTest(string connectionString)
        {
            bool IsCanConnectioned = false;
            //创建连接对象
            SqlConnection mySqlConnection = new SqlConnection(connectionString);
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                mySqlConnection.Close();
            }
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                return IsCanConnectioned;
            }
            else
            {
                return IsCanConnectioned;
            }
        }

        /// <summary>
        /// 取所有数据库名
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllDataBase(string sqlConnect)
        {
            ArrayList DBNameList = new ArrayList();
            SqlConnection Connection = new SqlConnection(sqlConnect);
            DataTable DBNameTable = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter("select name from master..sysdatabases", Connection);

            lock (Adapter)
            {
                Adapter.Fill(DBNameTable);
            }

            foreach (DataRow row in DBNameTable.Rows)
            {
                DBNameList.Add(row["name"]);
            }

            return DBNameList;
        }

        /// <summary>
        /// 验证目标数据库是否存在
        /// </summary>
        /// <param name="sqlConnnect"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static bool IsAlive(string sqlConnnect, string dbName)
        {
            string strSql = string.Format(@"select count(*) from master..sysdatabases where name='{0}'", dbName);
            if (ExecuteSql(strSql, sqlConnnect) > 0)
            {
                return true; // 存在
            }
            else
            {
                return false; //不存在
            }
        }

        /// <summary>
        /// 创建自定义数据库
        /// </summary>
        /// <returns></returns>
        public static bool CreateNewDb(string sqlConnect, string dbName)
        {
            string strSql = string.Format(@"
CREATE DATABASE {0} ON PRIMARY 
(NAME = {0}_Data, 
FILENAME = 'E:\{0}.mdf', 
SIZE = 5MB, MAXSIZE = 50MB, FILEGROWTH = 10%) 
LOG ON (NAME = {1}, 
FILENAME = 'E:\{1}.ldf', 
SIZE = 1MB, 
MAXSIZE = 50MB, 
FILEGROWTH = 10%)", dbName, dbName + "log");
            try
            {
                ExecuteSql(strSql, sqlConnect);
                CreateNewTable(sqlConnect, dbName);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 创建自定义数据库附属表
        /// </summary>
        /// <returns></returns>
        public static bool CreateNewTable(string sqlConnect, string dbName)
        {
            string strSql = string.Format(@"
USE {0} 
; 
SET ANSI_NULLS ON 
; 
SET QUOTED_IDENTIFIER ON 
; 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crawler_Result]') AND type in (N'U')) 
BEGIN 
CREATE TABLE [dbo].[Crawler_Result]( 
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[查询名称] [varchar](2000) NULL, 
	[市工商局_名称] [varchar](2000) NULL, 
	[市工商局_注册号] [varchar](2000) NULL, 
	[市工商局_类型] [varchar](2000) NULL, 
	[市工商局_法定代表人] [varchar](2000) NULL, 
	[市工商局_成立日期] [varchar](2000) NULL, 
	[市工商局_住所] [varchar](2000) NULL, 
	[市工商局_营业期限自] [varchar](2000) NULL, 
	[市工商局_营业期限至] [varchar](2000) NULL, 
	[市工商局_经营范围] [varchar](4000) NULL, 
	[市工商局_登记机关] [varchar](2000) NULL, 
	[市工商局_核准日期] [varchar](2000) NULL, 
	[市工商局_登记状态] [varchar](2000) NULL, 
	[市工商局_注册资本] [varchar](2000) NULL, 
	[市工商局_实收资本] [varchar](2000) NULL, 
	[市工商局_货币单位] [varchar](2000) NULL, 
	[市工商局_币种] [varchar](2000) NULL, 
	[市工商局_实缴出资金额] [varchar](2000) NULL, 
	[市工商局_最终实缴出资时间] [varchar](2000) NULL, 
	[市质监局_组织机构代码] [varchar](2000) NULL, 
	[市质监局_代码证颁发机关] [varchar](2000) NULL, 
	[市地税局_纳税人名称] [varchar](2000) NULL, 
	[市地税局_税务登记类型] [varchar](2000) NULL, 
	[市地税局_税务登记证号] [varchar](2000) NULL, 
	[市地税局_注册号] [varchar](2000) NULL, 
	[市地税局_法人姓名] [varchar](2000) NULL, 
	[市地税局_组织机构代码] [varchar](2000) NULL, 
	[市地税局_登记受理类型] [varchar](2000) NULL, 
	[市地税局_经营地址] [varchar](2000) NULL, 
	[市地税局_经营地址联系电话] [varchar](2000) NULL, 
	[市地税局_经营地址邮编] [varchar](2000) NULL, 
	[市地税局_企业主页网址] [varchar](2000) NULL, 
	[市地税局_所处街乡] [varchar](2000) NULL, 
	[市地税局_国地税共管户标识] [varchar](2000) NULL, 
	[市地税局_登记注册类型] [varchar](2000) NULL, 
	[市地税局_隶属关系] [varchar](2000) NULL, 
	[市地税局_国家标准行业] [varchar](2000) NULL, 
	[市地税局_税务登记日期] [varchar](2000) NULL, 
	[市地税局_主管税务机关] [varchar](2000) NULL, 
	[市地税局_纳税人状态] [varchar](2000) NULL, 
	[是否为历史名称] [varchar](2000) NULL, 
 CONSTRAINT [PK_Crawler_Result] PRIMARY KEY CLUSTERED  
( 
	[Id] ASC 
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY] 
) ON [PRIMARY] 
END 
; 
SET ANSI_NULLS ON 
; 
SET QUOTED_IDENTIFIER ON 
; 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crawler_Qg]') AND type in (N'U')) 
BEGIN 
CREATE TABLE [dbo].[Crawler_Qg]( 
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[查询名称] [varchar](500) NULL, 
	[机构名称] [varchar](500) NULL, 
	[组织机构代码] [varchar](50) NULL, 
	[机构登记证号] [varchar](50) NULL, 
	[机构类型] [varchar](500) NULL, 
	[地址] [varchar](500) NULL, 
	[注册日期] [varchar](50) NULL, 
	[备注日期] [varchar](50) NULL, 
	[截止有效期] [varchar](50) NULL, 
	[Ly] [varchar](500) NULL, 
	[EntryJgdm] [varchar](500) NULL, 
	[Eeservea] [varchar](500) NULL, 
	[RowNum] [varchar](500) NULL 
) ON [PRIMARY] 
END ", dbName);
            try
            {
                ExecuteSql(strSql, sqlConnect);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static int ExecuteSql(string sqlString, string connctionString)
        {
            using (SqlConnection connection = new SqlConnection(connctionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        var rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException)
                    {
                        connection.Close();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
