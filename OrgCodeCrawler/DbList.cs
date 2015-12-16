using Crawler.Model;
using CrawlerPro;
using CrawlerPro.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OrgCodeCrawler
{
    public partial class DbList : Form
    {
        public DbList()
        {
            InitializeComponent();
            radiob_qg.Select();
            listView_data.ContextMenuStrip = contextMenuStrip_tableHead;
            //和comboxlist的事件是一个功能，重复了
            //radiob_qg.CheckedChanged += new EventHandler(comboDbList_SelectedIndexChanged);
            //radiob_bj.CheckedChanged += new EventHandler(comboDbList_SelectedIndexChanged);
        }

        private void DbList_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "\\db");
            if (dir == null)
                return;
            foreach (var item in dir.GetFiles("*.db"))
            {
                comboDbList.Items.Add(item.Name + "[" + item.CreationTime + "]");
            }
            if (comboDbList.Items.Count > 0)
            {
                comboDbList.SelectedIndex = 0;
            }
        }

        private void b_search_Click(object sender, EventArgs e)
        {
            BindListView();
        }

        private void comboDbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
        }

        public void BindListView()
        {
            label_message.Text = "";
            var dbName = (comboDbList.Text ?? "").ToString().Split('[')[0].Trim(']');
            if (string.IsNullOrWhiteSpace(dbName))
            {
                label_message.Text = "没有历史存留数据";
                ClearListView();
                return;
            }
            if (radiob_qg.Checked)
                ShowListView(DoSql.SelectInfoByDb<CrawlerQgOut>($"select * from {(radiob_qg.Checked ? "Crawler_Qg" : "Crawler_Bj")}", dbName));
            else
                ShowListView(DoSql.SelectInfoByDb<CrawlerBjOut>($"select * from {(radiob_qg.Checked ? "Crawler_Qg" : "Crawler_Bj")}", dbName));
        }

        private void ShowListView<T>(IList<T> lists)
        {
            if (lists == null || lists.Count <= 0)
            {
                label_message.Text = "抱歉，没有查找到数据";
                ClearListView();
                return;
            }
            listView_data.GridLines = true; //显示表格线
            listView_data.View = View.Details;//显示表格细节
            listView_data.Scrollable = true;//有滚动条
            listView_data.FullRowSelect = true;//是否可以选择行
            ClearListView();
            listView_data.Columns.Add("", 1, HorizontalAlignment.Left);
            BuildHead(lists[0]);
            foreach (var item in lists)
                AppednText(item);
        }

        public void ClearListView()
        {
            listView_data.Items.Clear();
            listView_data.Columns.Clear();
        }

        public void BuildHead<T>(T t)
        {
            if (t == null)
                return;
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
                return;
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                int width = name.Length * 20;
                listView_data.Columns.Add(name, width, HorizontalAlignment.Left);
            }
        }

        public void AppednText<T>(T t)
        {
            if (t == null)
                return;
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
                return;
            ListViewItem listviewitem = new ListViewItem();
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    listviewitem.SubItems.Add((value ?? "").ToString().Trim());
                }
            }
            listView_data.Items.Add(listviewitem);
        }

        private void 导出至ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var data = listView_data.SelectedItems.Cast<dynamic>().ToArray();
            //var a = listView_data.SelectedItems[1].SubItems[1].Text;
            var dbName = (comboDbList.Text ?? "").ToString().Split('[')[0].Trim(']');
            if (string.IsNullOrWhiteSpace(dbName))
            {
                label_message.Text = "没有历史存留数据";
                return;
            }
            IList<string> data = new List<string>();
            for (int i = 0; i < listView_data.SelectedItems.Count; i++)
            {
                data.Add(listView_data.SelectedItems[i].SubItems[2].Text);
            }
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
            var selectIndex = radiob_qg.Checked ? 0 : 1;
            new Thread(delegate ()
            {
                byte[] bytes = null;
                if (radiob_qg.Checked)
                {
                    bytes = DoSql.SelectInfoByDb<CrawlerQgOut>($"select * from {(radiob_qg.Checked ? "Crawler_Qg" : "Crawler_Bj")}", dbName, data).ListToExcel("导出数据");
                }
                else
                {
                    bytes = DoSql.SelectInfoByDb<CrawlerBjOut>($"select * from {(radiob_qg.Checked ? "Crawler_Qg" : "Crawler_Bj")}", dbName, data).ListToExcel("导出数据");
                }
                File.WriteAllBytes(localFilePath, bytes);
                Invoke(new Action(() =>
                {
                    MessageBox.Show(@"导出成功！");
                }));
            })
            { IsBackground = true }.Start();
        }

    }
}
