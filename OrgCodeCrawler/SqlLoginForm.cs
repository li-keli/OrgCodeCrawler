using Crawler.Model;
using System;
using System.Windows.Forms;

namespace OrgCodeCrawler
{
    public partial class SqlLoginForm : Form
    {
        public ComboBox DbList { get; set; }
        string sqlConnect;
        Form load;
        public SqlLoginForm(LoadForm load)
        {
            InitializeComponent();
            this.load = load;
            DbList = cb_dblist;
            cb_dblist.Enabled = false;
            b_ok.Enabled = false;
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            DbDto dbdto = new DbDto
            {
                DataSource = cb_DataSource.Text,
                InitialCatalog = (string)cb_dblist.SelectedValue,
                UserID = t_user.Text,
                Password = t_pwd.Text
            };
            CrawlerPro.Common.XmlHelper.CreateConfig(dbdto);
            Visible = false;
            Invoke(new MethodInvoker(delegate () { load.Visible = false; }));
            new MainForm().Show();
        }

        private void b_test_Click(object sender, EventArgs e)
        {
            var DataSource = cb_DataSource.SelectedItem;
            var UserID = t_user.Text;
            var Password = t_pwd.Text;
            sqlConnect = string.Format("Data Source={0};Initial Catalog=master;User ID={1};Password={2}", DataSource, UserID, Password);
            if (CrawlerPro.DBHelper.ConnectionTest(sqlConnect))
            {
                MessageBox.Show("数据库连接成功。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var dblist = CrawlerPro.DBHelper.GetAllDataBase(sqlConnect);
                dblist.Add("新建自定义数据库");
                cb_dblist.DataSource = dblist;
                cb_dblist.Enabled = true;
                b_ok.Enabled = true;
            }
            else
            {
                MessageBox.Show("数据库连接失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cb_dblist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_dblist.SelectedItem.Equals("新建自定义数据库"))
            {
                new CreateDB(sqlConnect, this).ShowDialog();
            }
        }

        /// <summary>
        /// 刷新数据库下拉菜单
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <param name="cb_dblist"></param>
        public static void RefreshDbList(string sqlConn, ComboBox cb_dblist)
        {
            var dblist = CrawlerPro.DBHelper.GetAllDataBase(sqlConn);
            cb_dblist.DataSource = dblist;
        }

        private void SqlLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
