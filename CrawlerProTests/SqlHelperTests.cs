using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrawlerPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crawler.Model;

namespace CrawlerPro.Tests
{
    [TestClass()]
    public class SqlHelperTests
    {
        [TestMethod()]
        public void InserInfoTest()
        {
            TargetModel model = new TargetModel
            {
                capitalInfo = new CapitalInfo
                {
                    市工商局_注册资本 = "100 万元 奥地利先令",
                    市工商局_实收资本 = " 780 万元 人民币 "
                }
            };
            model = Cutting(model);
        }

        private static readonly Dictionary<string, string> Dic = new Dictionary<string, string>()
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
        public static TargetModel Cutting(TargetModel model)
        {
            model.basicsInfo = model.basicsInfo ?? new BasicsInfo();
            model.capitalInfo = model.capitalInfo ?? new CapitalInfo();
            model.orgCodeInfo = model.orgCodeInfo ?? new OrgCodeInfo();
            model.taxInfo = model.taxInfo ?? new TaxInfo();
            return model;
        }
    }
}
