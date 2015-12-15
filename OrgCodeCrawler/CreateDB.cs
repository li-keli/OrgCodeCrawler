using System;
using System.Windows.Forms;

namespace OrgCodeCrawler
{
    public partial class CreateDB : Form
    {
        private SqlLoginForm SqlLoginForm { get; set; }
        private string SqlConnect { get; set; }
        public CreateDB(string sqlConnect, SqlLoginForm form)
        {
            InitializeComponent();
            SqlConnect = sqlConnect;
            SqlLoginForm = form;
        }

        private void b_createDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(t_dbtext.Text))
            {
                MessageBox.Show("数据库名不能为空", "消息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (CrawlerPro.DBHelper.IsAlive(SqlConnect, t_dbtext.Text))
            {
                MessageBox.Show(string.Format("名叫{0}的数据库已经存在。", t_dbtext.Text), "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!CrawlerPro.DBHelper.CreateNewDb(SqlConnect, t_dbtext.Text))
            {
                MessageBox.Show("数据库创建错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                SqlLoginForm.RefreshDbList(SqlConnect, SqlLoginForm.DbList);
                Dispose();
            }
        }
    }
}
