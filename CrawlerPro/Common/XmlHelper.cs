using Crawler.Model;
using System;
using System.Xml;

namespace CrawlerPro.Common
{
    public class XmlHelper
    {
        public static void CreateConfig(DbDto dto)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            XmlElement xmlelem = xmldoc.CreateElement("", "configuration", "");
            xmldoc.AppendChild(xmlelem);

            XmlNode root = xmldoc.SelectSingleNode("configuration");
            XmlElement xe1 = xmldoc.CreateElement("Setting");

            XmlElement xesub1 = xmldoc.CreateElement("DBConn");
            xesub1.SetAttribute("value", string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", dto.DataSource, dto.InitialCatalog, dto.UserID, dto.Password));
            xe1.AppendChild(xesub1);

            root.AppendChild(xe1);
            xmldoc.Save(AppDomain.CurrentDomain.BaseDirectory + "\\App.config");
        }

        /// <summary>
        /// 读取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDBConnect()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("App.config");
                XmlElement rootElem = doc.DocumentElement;
                XmlNode config = rootElem.SelectSingleNode("/configuration/Setting");
                return config.SelectSingleNode("DBConn").Attributes["value"].Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
