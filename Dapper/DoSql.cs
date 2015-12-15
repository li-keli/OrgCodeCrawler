using System;
using System.Data;
using System.Data.SQLite;

namespace Dapper
{
    public class DoSql
    {
        public readonly static string Ds = "Data Source =" + Environment.CurrentDirectory + "/Config.db";
        public delegate int Execute<in T>(T t);
        #region 全国
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

        public static bool InsertQg(object listModel)
        {
            var sql = @" INSERT INTO Crawler_Qg(查询名称,机构名称,组织机构代码,机构登记证号,机构类型,地址,注册日期,备注日期,截止有效期,Ly,EntryJgdm,Eeservea,RowNum) VALUES('@查询名称','@机构名称','@组织机构代码','@机构登记证号','@机构类型','@地址','@注册日期','@备注日期','@截止有效期','@Ly','@EntryJgdm','@Eeservea','@RowNum') ";
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(sql, listModel);
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 北京企业信息插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertBj(object model)
        {
            var sql = @" INSERT INTO Crawler_Qg(查询名称,机构名称,组织机构代码,机构登记证号,机构类型,地址,注册日期,备注日期,截止有效期,Ly,EntryJgdm,Eeservea,RowNum) VALUES('@查询名称','@机构名称','@组织机构代码','@机构登记证号','@机构类型','@地址','@注册日期','@备注日期','@截止有效期','@Ly','@EntryJgdm','@Eeservea','@RowNum') ";
            using (IDbConnection conn = new SQLiteConnection(Ds))
            {
                conn.Open();
                conn.Execute(sql, model);
            }
            return false;
        }
    }
}
