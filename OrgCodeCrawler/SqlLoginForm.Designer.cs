namespace OrgCodeCrawler
{
    partial class SqlLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_DataSource = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.t_user = new System.Windows.Forms.TextBox();
            this.t_pwd = new System.Windows.Forms.TextBox();
            this.cb_dblist = new System.Windows.Forms.ComboBox();
            this.b_test = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器名称（S）:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "登录名（L）:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码（P）:";
            // 
            // cb_DataSource
            // 
            this.cb_DataSource.FormattingEnabled = true;
            this.cb_DataSource.Items.AddRange(new object[] {
            "localhost",
            "127.0.0.1"});
            this.cb_DataSource.Location = new System.Drawing.Point(145, 17);
            this.cb_DataSource.Name = "cb_DataSource";
            this.cb_DataSource.Size = new System.Drawing.Size(144, 20);
            this.cb_DataSource.TabIndex = 3;
            this.cb_DataSource.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据库（D）:";
            // 
            // t_user
            // 
            this.t_user.Location = new System.Drawing.Point(145, 43);
            this.t_user.Name = "t_user";
            this.t_user.Size = new System.Drawing.Size(143, 21);
            this.t_user.TabIndex = 4;
            this.t_user.Text = "sa";
            // 
            // t_pwd
            // 
            this.t_pwd.Location = new System.Drawing.Point(144, 70);
            this.t_pwd.Name = "t_pwd";
            this.t_pwd.Size = new System.Drawing.Size(144, 21);
            this.t_pwd.TabIndex = 1;
            this.t_pwd.UseSystemPasswordChar = true;
            // 
            // cb_dblist
            // 
            this.cb_dblist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dblist.FormattingEnabled = true;
            this.cb_dblist.Location = new System.Drawing.Point(145, 97);
            this.cb_dblist.Name = "cb_dblist";
            this.cb_dblist.Size = new System.Drawing.Size(144, 20);
            this.cb_dblist.TabIndex = 3;
            this.cb_dblist.SelectedIndexChanged += new System.EventHandler(this.cb_dblist_SelectedIndexChanged);
            // 
            // b_test
            // 
            this.b_test.Location = new System.Drawing.Point(85, 138);
            this.b_test.Name = "b_test";
            this.b_test.Size = new System.Drawing.Size(75, 23);
            this.b_test.TabIndex = 5;
            this.b_test.Text = "连接/测试";
            this.b_test.UseVisualStyleBackColor = true;
            this.b_test.Click += new System.EventHandler(this.b_test_Click);
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(176, 138);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 5;
            this.b_ok.Text = "登陆";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // SqlLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 184);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.b_test);
            this.Controls.Add(this.t_pwd);
            this.Controls.Add(this.t_user);
            this.Controls.Add(this.cb_dblist);
            this.Controls.Add(this.cb_DataSource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SqlLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库登陆";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlLoginForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_DataSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox t_user;
        private System.Windows.Forms.TextBox t_pwd;
        private System.Windows.Forms.ComboBox cb_dblist;
        private System.Windows.Forms.Button b_test;
        private System.Windows.Forms.Button b_ok;
    }
}