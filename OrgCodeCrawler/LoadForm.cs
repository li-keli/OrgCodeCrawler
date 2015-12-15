using System;
using System.Threading;
using System.Windows.Forms;
using FSLib.App.SimpleUpdater;
using System.ServiceProcess;

namespace OrgCodeCrawler
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            l_logout.Text = @"正在验证更新信息...";
            var updater = Updater.CreateUpdaterInstance("http://192.168.7.250:8111/update/{0}", "update.xml");
            //updater.EnsureNoUpdate();   必须先正确引导更新？
            updater.Error += (s, e1) =>
            {
                l_logout.Text = @"获取更新失败...";
                MessageBox.Show(@"更新发生了错误：" + updater.Context.Exception.Message);
                BeginWork();
            };
            updater.UpdatesFound += (s, e1) => MessageBox.Show(@"发现了新版本：" + updater.Context.UpdateInfo.AppVersion);
            updater.NoUpdatesFound += (s, e1) =>
            {
                BeginWork();
                //MessageBox.Show("没有新版本！");
            };
            updater.MinmumVersionRequired += (s, e1) =>
            {
                MessageBox.Show(@"当前版本过低无法使用自动更新！");
            };
            Updater.CheckUpdateSimple();
        }

        private void BeginWork()
        {
            Thread recvThread;
            if (MessageBox.Show("是否转向独立查询引擎？", "系统转向消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //独立引擎
                recvThread = new Thread(SingelEngine);
            }
            else {
                //批量多线程引擎
                //检测是否安装了Sqlserver
                //if (!ExistSqlServerService())
                //{
                //    if (MessageBox.Show("检测到本机似乎未安装SqlServer，是否启用Sqlite数据库？", "系统消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //        //启用sqlite数据库
                //        recvThread = new Thread(SqliteConnect);
                //    else
                //        //启用sqlserver数据库
                //        recvThread = new Thread(CheckSqlConnect);
                //}
                //else
                //{
                //    //启用sqlserver数据库
                //    recvThread = new Thread(SqliteConnect);
                //}
                recvThread = new Thread(SqliteConnect);
            }
            /*
                李科笠 2015年11月16日 17:59:14
                由于该操作在新创建的线程中执行，应该是不能访问UI的。
                按照“多线程中安全的访问控件”中提到的方法的话，必须创建一个委托，然后异步调用该委托才可以
            */
            recvThread.SetApartmentState(ApartmentState.STA);
            recvThread.Start();
        }

        private void CheckSqlConnect()
        {
            var point = ".";
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Invoke((MethodInvoker)delegate { l_logout.Text = @"初始化数据库配置" + point; });
                point += point;
            }
            Application.Run(new SqlLoginForm(this));
        }

        private void SqliteConnect()
        {
            var point = ".";
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Invoke((MethodInvoker)delegate { l_logout.Text = @"初始化本地引擎配置" + point; });
                point += point;
            }
            Invoke(new MethodInvoker(() =>
            {
                Visible = false;
            }));
            Application.Run(new MainForm());
        }

        private void SingelEngine()
        {
            var point = ".";
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Invoke((MethodInvoker)delegate { l_logout.Text = @"初始化本地引擎配置" + point; });
                point += point;
            }
            Invoke(new MethodInvoker(() =>
            {
                Visible = false;
            }));
            Application.Run(new SingerSearch());
        }

        public static bool ExistSqlServerService()
        {
            bool ExistFlag = false;
            ServiceController[] service = ServiceController.GetServices();
            for (int i = 0; i < service.Length; i++)
            {
                if (service[i].DisplayName.ToString() == "MSSQLSERVER")
                {
                    ExistFlag = true;
                }
            }
            return ExistFlag;
        }

    }
}
