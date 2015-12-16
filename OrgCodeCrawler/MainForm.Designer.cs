namespace OrgCodeCrawler
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.b_showDbList = new System.Windows.Forms.Button();
            this.b_go = new System.Windows.Forms.Button();
            this.gmTrackBar1 = new Gdu.WinFormUI.GMTrackBar();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gmRollingBar1 = new Gdu.WinFormUI.GMRollingBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.l_runTime = new System.Windows.Forms.Label();
            this.l_Speed = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.l_SurplusNum = new System.Windows.Forms.Label();
            this.l_taskNum = new System.Windows.Forms.Label();
            this.l_threadNum = new System.Windows.Forms.Label();
            this.l_LivethreadNum = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gmProgressBar_main = new Gdu.WinFormUI.GMProgressBar();
            this.timer_run = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.saveExcel = new System.Windows.Forms.SaveFileDialog();
            this.label_ip = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.b_showDbList);
            this.groupBox1.Controls.Add(this.b_go);
            this.groupBox1.Controls.Add(this.gmTrackBar1);
            this.groupBox1.Controls.Add(this.cb_type);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 247);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置";
            // 
            // b_showDbList
            // 
            this.b_showDbList.Location = new System.Drawing.Point(13, 116);
            this.b_showDbList.Name = "b_showDbList";
            this.b_showDbList.Size = new System.Drawing.Size(258, 34);
            this.b_showDbList.TabIndex = 5;
            this.b_showDbList.Text = "打开采集数据查看器";
            this.b_showDbList.UseVisualStyleBackColor = true;
            this.b_showDbList.Click += new System.EventHandler(this.b_showDbList_Click);
            // 
            // b_go
            // 
            this.b_go.Location = new System.Drawing.Point(13, 156);
            this.b_go.Name = "b_go";
            this.b_go.Size = new System.Drawing.Size(258, 34);
            this.b_go.TabIndex = 4;
            this.b_go.Text = "Go!";
            this.b_go.UseVisualStyleBackColor = true;
            this.b_go.Click += new System.EventHandler(this.b_go_Click);
            // 
            // gmTrackBar1
            // 
            this.gmTrackBar1.Location = new System.Drawing.Point(78, 21);
            this.gmTrackBar1.Maximum = 7;
            this.gmTrackBar1.Minimum = 1;
            this.gmTrackBar1.Name = "gmTrackBar1";
            this.gmTrackBar1.Size = new System.Drawing.Size(174, 35);
            this.gmTrackBar1.TabIndex = 1;
            this.gmTrackBar1.TickSide = System.Windows.Forms.TickStyle.BottomRight;
            this.gmTrackBar1.Value = 5;
            this.gmTrackBar1.XTheme = null;
            this.gmTrackBar1.ValueChanged += new System.EventHandler(this.gmTrackBar1_ValueChanged);
            // 
            // cb_type
            // 
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "全国组织机构代码库",
            "北京企业信息网"});
            this.cb_type.Location = new System.Drawing.Point(78, 59);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(174, 20);
            this.cb_type.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(11, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "采集源";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "采集速度";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.gmRollingBar1);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.l_runTime);
            this.groupBox2.Controls.Add(this.l_Speed);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.l_SurplusNum);
            this.groupBox2.Controls.Add(this.l_taskNum);
            this.groupBox2.Controls.Add(this.l_threadNum);
            this.groupBox2.Controls.Add(this.l_LivethreadNum);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.gmProgressBar_main);
            this.groupBox2.Location = new System.Drawing.Point(292, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 249);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据与图表";
            // 
            // gmRollingBar1
            // 
            this.gmRollingBar1.Location = new System.Drawing.Point(312, 205);
            this.gmRollingBar1.Name = "gmRollingBar1";
            this.gmRollingBar1.Size = new System.Drawing.Size(43, 41);
            this.gmRollingBar1.Style = Gdu.WinFormUI.RollingBarStyle.BigGuyLeadsLittleGuys;
            this.gmRollingBar1.TabIndex = 5;
            this.gmRollingBar1.TabStop = false;
            this.gmRollingBar1.XTheme = null;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 227);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "已经运行：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "抓取速度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "剩余数目：";
            // 
            // l_runTime
            // 
            this.l_runTime.AutoSize = true;
            this.l_runTime.Location = new System.Drawing.Point(82, 227);
            this.l_runTime.Name = "l_runTime";
            this.l_runTime.Size = new System.Drawing.Size(77, 12);
            this.l_runTime.TabIndex = 3;
            this.l_runTime.Text = "00:00:00.000";
            // 
            // l_Speed
            // 
            this.l_Speed.AutoSize = true;
            this.l_Speed.Location = new System.Drawing.Point(82, 108);
            this.l_Speed.Name = "l_Speed";
            this.l_Speed.Size = new System.Drawing.Size(11, 12);
            this.l_Speed.TabIndex = 2;
            this.l_Speed.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "任务池数目：";
            // 
            // l_SurplusNum
            // 
            this.l_SurplusNum.AutoSize = true;
            this.l_SurplusNum.Location = new System.Drawing.Point(82, 89);
            this.l_SurplusNum.Name = "l_SurplusNum";
            this.l_SurplusNum.Size = new System.Drawing.Size(11, 12);
            this.l_SurplusNum.TabIndex = 2;
            this.l_SurplusNum.Text = "0";
            // 
            // l_taskNum
            // 
            this.l_taskNum.AutoSize = true;
            this.l_taskNum.Location = new System.Drawing.Point(82, 68);
            this.l_taskNum.Name = "l_taskNum";
            this.l_taskNum.Size = new System.Drawing.Size(11, 12);
            this.l_taskNum.TabIndex = 2;
            this.l_taskNum.Text = "0";
            // 
            // l_threadNum
            // 
            this.l_threadNum.AutoSize = true;
            this.l_threadNum.Location = new System.Drawing.Point(82, 23);
            this.l_threadNum.Name = "l_threadNum";
            this.l_threadNum.Size = new System.Drawing.Size(11, 12);
            this.l_threadNum.TabIndex = 2;
            this.l_threadNum.Text = "5";
            // 
            // l_LivethreadNum
            // 
            this.l_LivethreadNum.AutoSize = true;
            this.l_LivethreadNum.Location = new System.Drawing.Point(82, 46);
            this.l_LivethreadNum.Name = "l_LivethreadNum";
            this.l_LivethreadNum.Size = new System.Drawing.Size(11, 12);
            this.l_LivethreadNum.TabIndex = 2;
            this.l_LivethreadNum.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "启用线程数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "活动线程数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(226, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "总进度";
            // 
            // gmProgressBar_main
            // 
            this.gmProgressBar_main.Location = new System.Drawing.Point(138, 17);
            this.gmProgressBar_main.Name = "gmProgressBar_main";
            this.gmProgressBar_main.Shape = Gdu.WinFormUI.ProgressBarShapeStyle.Circle;
            this.gmProgressBar_main.Size = new System.Drawing.Size(213, 197);
            this.gmProgressBar_main.TabIndex = 0;
            this.gmProgressBar_main.TabStop = false;
            this.gmProgressBar_main.XTheme = null;
            // 
            // timer_run
            // 
            this.timer_run.Tick += new System.EventHandler(this.timer_run_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::OrgCodeCrawler.Properties.Resources.djgm20lw;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 77);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label_ip.Location = new System.Drawing.Point(3, 333);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(110, 10);
            this.label_ip.TabIndex = 7;
            this.label_ip.Text = "127.0.0.1（本地网络）";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(651, 345);
            this.Controls.Add(this.label_ip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "企业工商信息数据采集器 V3.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private Gdu.WinFormUI.GMProgressBar gmProgressBar_main;
        private Gdu.WinFormUI.GMTrackBar gmTrackBar1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button b_go;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label l_LivethreadNum;
        private System.Windows.Forms.Label l_taskNum;
        private System.Windows.Forms.Label l_SurplusNum;
        private System.Windows.Forms.Label l_threadNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label l_runTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timer_run;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label l_Speed;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private Gdu.WinFormUI.GMRollingBar gmRollingBar1;
        private System.Windows.Forms.SaveFileDialog saveExcel;
        private System.Windows.Forms.Button b_showDbList;
        private System.Windows.Forms.Label label_ip;
    }
}

