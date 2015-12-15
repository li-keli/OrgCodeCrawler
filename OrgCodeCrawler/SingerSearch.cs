using CrawlerPro;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using CrawlerPro.Common;
using System.IO;
using Crawler.Model;

namespace OrgCodeCrawler
{
    public partial class SingerSearch : Form
    {
        private static Hashtable objCache = new Hashtable();
        private Thread searchThread;
        public SingerSearch()
        {
            InitializeComponent();
            AcceptButton = b_search;
            lb_return.ContextMenuStrip = contextMenuStrip1;
            cb_type.SelectedIndex = 0;
        }

        private void b_search_Click(object sender, EventArgs e)
        {
            var searchText = t_search.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("查询不能为空，我想你应该知道~", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            b_search.Enabled = false;
            LoadFirst();
            var crawlerType = cb_type.SelectedIndex;
            searchThread = new Thread(() =>
            {
                switch (crawlerType)
                {
                    case 0:
                        //全国组织机构代码爬虫线程
                        new CountrywideCrawler(new Setting()).SingelThreadRun(searchText, tb_logs);
                        foreach (var item in DoSql.SelectQgInfo().Select(m => m.机构名称))
                        {
                            if (item != null && !lb_return.Items.Contains(item))
                            {
                                Invoke(new MethodInvoker(() =>
                                {
                                    lb_return.Items.Add(item);
                                }));
                            }
                        };
                        break;
                    case 1:
                        //北京企业信息爬虫线程
                        var Ip = new CrawlerBegin().SingelCrawlerThread(searchText, lb_return, tb_logs, l_ipaddress, (string)objCache["Ip"]);
                        objCache["Ip"] = Ip;
                        foreach (var item in DoSql.SelectBjInfo().Select(m => m.市工商局_名称))
                        {
                            if (item != null && !lb_return.Items.Contains(item))
                            {
                                Invoke(new MethodInvoker(() =>
                                {
                                    lb_return.Items.Add(item);
                                }));
                            }
                        };
                        break;
                    default:
                        //爬你妹，没有这个选项
                        break;
                }
                Invoke(new MethodInvoker(() => { b_search.Enabled = true; }));
            })
            { IsBackground = true };
            searchThread.Start();
        }

        private void LoadFirst()
        {
            tb_logs.AppendText((string.IsNullOrWhiteSpace(tb_logs.Text) ? "" : "\r\n") + DateTime.Now + " 采集引擎初始化...");
            DoSql.ValidateDb();
        }

        private void lb_return_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (cb_type.SelectedIndex)
            {
                case 0:
                    new Detailed((sender as ListBox).SelectedItem, DatabaseType.QgDb).Show();
                    break;
                case 1:
                    new Detailed((sender as ListBox).SelectedItem, DatabaseType.BjDb).Show();
                    break;
                default:
                    break;
            }
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = lb_return.SelectedItems.Count - 1; i >= 0; i--)
            {
                var index = lb_return.Items.IndexOf(lb_return.SelectedItems[i]);
                lb_return.Items.Remove(lb_return.Items[index]);
            }
        }

        private void 导出ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = lb_return.SelectedItems.Cast<dynamic>().ToArray();
            string localFilePath = "";
            var saveFile = new SaveFileDialog
            {
                Filter = @"xls files(*.xls)|*.txt|All files(*.*)|*.*",
                FileName = "导出数据.xls",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
                localFilePath = saveFile.FileName;
            if (string.IsNullOrEmpty(localFilePath))
                return;
            var selectIndex = cb_type.SelectedIndex;
            new Thread(delegate ()
            {
                byte[] bytes = null;
                switch (selectIndex)
                {
                    case 0: //全国
                        var exportQg = DoSql.SelectQgInfo(" and 机构名称 in @机构名称", new { 机构名称 = data });
                        bytes = exportQg.ListToExcel("导出数据");
                        break;
                    case 1: //北京
                        var exportBj = DoSql.SelectBjInfo(" and 市工商局_名称 in @市工商局_名称", new { 市工商局_名称 = data });
                        bytes = exportBj.ListToExcel("导出数据");
                        break;
                    default:
                        break;
                }

                File.WriteAllBytes(localFilePath, bytes);
                Invoke(new Action(() =>
                {
                    MessageBox.Show(@"导出成功！");
                }));
            })
            { IsBackground = true }.Start();
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_return.Items.Clear();
        }

        private void 在IE中打开选中的项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("容我三思，没想好这功能有啥用。");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void SingerSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
