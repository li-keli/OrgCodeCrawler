using CrawlerPro;
using CrawlerPro.Common;
using Gdu.WinFormUI;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OrgCodeCrawler
{
    public partial class MainForm : Form
    {
        readonly Setting _setting; TaskList _task;

        public MainForm()
        {
            InitializeComponent();
            cb_type.SelectedIndex = 0;
            _setting = new Setting() //初始化配置
            {
                IsWork = true,
                ThreadCount = gmTrackBar1.Value
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //base.XTheme = new ThemeFormDevExpress();
            //var theme = new ThemeScrollbarDevStyle();
            gmProgressBar_main.XTheme = new ThemeProgressBarSoftGreen();
            gmTrackBar1.XTheme = new ThemeTrackBarMetropolis();
            notifyIcon.ShowBalloonTip(1000 * 10, "爬虫消息", "爬虫初始化完成，启动成功！", ToolTipIcon.Info);
            gmRollingBar1.Visible = false;
        }

        private void gmTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            l_threadNum.Text = gmTrackBar1.Value.ToString();
            _setting.ThreadCount = gmTrackBar1.Value;
        }

        private void b_go_Click(object sender, EventArgs e)
        {
            //IsExist(); //检查是否存在历史数据
            //初始化
            DoSql.ValidateDb();
            try
            {
                MessageBox.Show(@"任务列表文本文件中，企业名一行一个。", @"温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFileDialog openfile = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = @"请选择文本文件",
                    Filter = @"文本文件|*.txt"
                };
                openfile.Multiselect = false;
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    groupBox1.Enabled = false;
                    gmRollingBar1.Visible = true;
                    gmRollingBar1.StartRolling();
                    var fileUrl = openfile.FileName;
                    _task = new TaskList(fileUrl);
                    GetTaskList(_task);
                    var crawlerType = cb_type.SelectedIndex;
                    switch (crawlerType)
                    {
                        case 0:
                            new CountrywideCrawler(_setting).CookieInitialization(_task); //全国组织机构代码爬虫线程
                            break;
                        case 1:
                            new CrawlerBegin().CrawlerBeginFunc(_task, _setting.ThreadCount); //北京企业信息爬虫线程
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"致命错误");
            }
        }

        /// <summary>
        /// 验证是否存在历史记录
        /// </summary>
        private void IsExist()
        {
            var tableName = cb_type.SelectedIndex == 0 ? "Crawler_Qg" : "Crawler_Result";
            if (SqlHelper.IsExist(tableName))
            {
                if (MessageBox.Show(@"发现存有历史数据，是否清空历史数据？", @"消息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (tableName.Equals("Crawler_Qg"))
                        SqlHelper.DelQgInfo();
                    else
                        SqlHelper.DelBjInfo();
                }
            }
        }

        /// <summary>
        /// UI辅助线程
        /// </summary>
        /// <param name="tasklist"></param>
        private void GetTaskList(TaskList tasklist)
        {
            l_taskNum.Text = tasklist.GetSerplusNum().ToString();
            l_SurplusNum.Text = tasklist.GetSerplusNum().ToString();
            _dtNow = DateTime.Now;
            timer_run.Start();
            new Thread(ListeningProgressBar).Start(tasklist);   // 启动总任务进度监听器
            new Thread(CalculationSpeed).Start(tasklist);       // 速度与预估完成时间计算
        }

        /// <summary>
        /// 总任务进度监听器
        /// </summary>
        /// <param name="obj"></param>
        private void ListeningProgressBar(object obj)
        {
            var tasklist = obj as TaskList;
            Thread.Sleep(1000); //采集线程优先执行
            if (tasklist == null) return;
            while (tasklist.GetSerplusNum() > 0 || tasklist.Threads > 0)
            {
                var getListNum = Conv.ToDecimal(tasklist.GetListNum());
                var getSerplusNum = Conv.ToDecimal(tasklist.GetSerplusNum());
                var b = Conv.ToDecimal((getListNum - getSerplusNum) / getListNum) * 100;
                var outint = Conv.ToInt(Math.Round(b));
                if (outint != 100)
                    SetgmProgressBar(outint, tasklist);
                Thread.Sleep(250);
            }
            SetgmProgressBar(100, tasklist);
        }

        delegate void ChangegmProgressBarValue(int num, TaskList tasklist);

        /// <summary>
        /// 总进度设置
        /// </summary>
        /// <param name="num">已运行比例</param>
        /// <param name="tasklist"></param>
        private void SetgmProgressBar(int num, TaskList tasklist)
        {
            if (InvokeRequired)
            {
                Invoke(new ChangegmProgressBarValue(SetgmProgressBar), num, tasklist);
            }
            else
            {
                if (num != 100)
                {
                    l_SurplusNum.Text = tasklist.GetSerplusNum().ToString();
                    gmProgressBar_main.Percentage = num;
                    l_LivethreadNum.Text = tasklist.Threads.ToString();
                }
                else
                {
                    //任务完成
                    groupBox1.Enabled = true;
                    gmRollingBar1.Visible = false;
                    gmProgressBar_main.Percentage = num;
                    l_LivethreadNum.Text = tasklist.Threads.ToString();
                    timer_run.Stop();
                    l_SurplusNum.Text = @"0";
                    notifyIcon.ShowBalloonTip(1000 * 3, "采集器消息", "采集任务以全部分发完成，请等待剩余任务线程完成任务。", ToolTipIcon.Info);
                    ExportExcel(); //触发导出
                }
            }
        }

        /// <summary>
        /// 计算抓取速度
        /// </summary>
        private void CalculationSpeed(object obj)
        {
            TaskList tasklist = obj as TaskList;
            int begin = 0, end = 0;
            while (tasklist.GetSerplusNum() > 0)
            {
                begin = tasklist.GetSerplusNum();
                Thread.Sleep(1000 * 10);
                end = tasklist.GetSerplusNum();
                var speed = (begin - end) * 6;
                if (speed <= 0)
                {
                    SetSpeed(0, "计算中");
                }
                else
                {
                    var EstimateTimeStr = Conv.ToDouble(tasklist.GetSerplusNum() / Conv.ToDecimal(speed));
                    TimeSpan ts = TimeSpan.FromSeconds(EstimateTimeStr);
                    SetSpeed(speed, ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":"
                    + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString());
                }
            }
        }
        delegate void DelegateCalculationSpeed(int speed, string estimateTime);
        /// <summary>
        /// 设置抓取速度
        /// </summary>
        private void SetSpeed(int speed, string estimateTime)
        {
            if (InvokeRequired)
            {
                Invoke(new DelegateCalculationSpeed(SetSpeed), speed, estimateTime);
            }
            else
            {
                l_Speed.Text = speed + "家/分钟";
            }
        }

        private DateTime _dtNow; //初始时间
        private void timer_run_Tick(object sender, EventArgs e)
        {
            TimeSpan ts1 = new TimeSpan(_dtNow.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            l_runTime.Text = ts.Hours + ":" + ts.Minutes + ":"
                + ts.Seconds + "." + ts.Milliseconds;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (_task != null)
            //{
            //    MessageBox.Show(@"主程序关闭，为执行的任务列表以存档至根目录。", @"退出消息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    _task.GetSurplusTask(AppDomain.CurrentDomain.BaseDirectory +
            //                         $"\\剩余列表[{DateTime.Now.ToLongDateString()}].txt");
            //}
            Environment.Exit(0);
        }

        private void ExportExcel()
        {
            var index = cb_type.SelectedIndex;
            var localFilePath = "";
            var saveFile = new SaveFileDialog
            {
                Filter = @"xls files(*.xls)|*.txt|All files(*.*)|*.*",
                FileName = "导出.xls",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
                localFilePath = saveFile.FileName;
            if (string.IsNullOrEmpty(localFilePath))
                return;
            new Thread(delegate ()
            {
                byte[] bytes = null;
                switch (index)
                {
                    case 0:
                        //bytes = SqlHelper.ReadTable("Crawler_Result").ListToExcel("北京库");
                        bytes = DoSql.SelectQgInfo().ListToExcel("全国库导出数据");
                        break;
                    case 1:
                        //bytes = SqlHelper.ReadTable("Crawler_Result").ListToExcel("北京库");
                        bytes = DoSql.SelectBjInfo().ListToExcel("北京库导出数据");
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

        private void b_showDbList_Click(object sender, EventArgs e)
        {
            new DbList().Show();
        }
    }
}
