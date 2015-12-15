using Crawler.Model;
using System;
using System.Windows.Forms;

namespace OrgCodeCrawler
{
    public partial class Detailed : Form
    {
        private object searchName;
        private DatabaseType _type;
        public Detailed()
        {
            InitializeComponent();
        }

        public Detailed(object obj, DatabaseType type)
        {
            InitializeComponent();
            list_show.ContextMenuStrip = contextMenu_copy;
            searchName = obj;
            _type = type;
        }

        private void Detailed_Load(object sender, EventArgs e)
        {
            list_show.Columns.Add("项目", 90);
            list_show.Columns.Add("值", 200);
            list_show.GridLines = true; //显示表格线
            list_show.View = View.Details;//显示表格细节
            list_show.LabelEdit = true; //是否可编辑,ListView只可编辑第一列。
            list_show.Scrollable = true;//有滚动条
            list_show.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
            list_show.FullRowSelect = true;//是否可以选择行
            switch (_type)
            {
                case DatabaseType.QgDb:
                    foreach (var item in CrawlerPro.DoSql.SelectQgInfo(" and 机构名称=@机构名称 ", new { 机构名称 = searchName }))
                    {
                        AppednText(item, list_show);
                    }
                    break;
                case DatabaseType.BjDb:
                    foreach (var item in CrawlerPro.DoSql.SelectBjInfo(" and 市工商局_名称=@市工商局_名称 ", new { 市工商局_名称 = searchName }))
                    {
                        AppednText(item, list_show);
                    }
                    break;
                default:
                    break;
            }
        }

        public void AppednText<T>(T t, ListView listview)
        {
            if (t == null)
            {
                return;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    listview.Items.Add(new ListViewItem(
                         new string[] { name, (value ?? "").ToString().Trim() }
                         ));
                }
                else
                {
                    AppednText(value, listview);
                }
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = list_show.SelectedItems[0].SubItems[1].Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetDataObject(text);
                MessageBox.Show("已经复制到剪贴板！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
