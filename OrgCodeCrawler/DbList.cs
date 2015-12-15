using Crawler.Model;
using CrawlerPro;
using System;
using System.IO;
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
            comboDbList.SelectedIndex = 0;
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
                return;
            }
            var data = DoSql.SelectInfoByDb<CrawlerQgOut>($"select * from {(radiob_qg.Checked ? "Crawler_Qg" : "Crawler_Bj")}", dbName);
            if (data == null || data.Count <= 0)
            {
                label_message.Text = "抱歉，没有查找到数据";
                return;
            }

            listView_data.GridLines = true; //显示表格线
            listView_data.View = View.Details;//显示表格细节
            listView_data.Scrollable = true;//有滚动条
            listView_data.FullRowSelect = true;//是否可以选择行
            listView_data.Items.Clear();
            listView_data.Columns.Clear();
            listView_data.Columns.Add("", 1, HorizontalAlignment.Left);
            BuildHead(data[0]);
            foreach (var item in data)
                AppednText(item);
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

        private void 隐藏本列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var text = listView_data.SelectedItems[0].SubItems[1].Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetDataObject(text);
                MessageBox.Show("已经复制到剪贴板！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 导出至ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = listView_data.SelectedItems[0].SubItems[1].Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetDataObject(text);
                MessageBox.Show("已经复制到剪贴板！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
