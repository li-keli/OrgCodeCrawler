namespace OrgCodeCrawler
{
    partial class DbList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbList));
            this.listView_data = new System.Windows.Forms.ListView();
            this.menuStrip_top = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择显示项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboDbList = new System.Windows.Forms.ComboBox();
            this.radiob_qg = new System.Windows.Forms.RadioButton();
            this.radiob_bj = new System.Windows.Forms.RadioButton();
            this.b_search = new System.Windows.Forms.Button();
            this.label_message = new System.Windows.Forms.Label();
            this.contextMenuStrip_tableHead = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.隐藏本列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出至ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_top.SuspendLayout();
            this.contextMenuStrip_tableHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_data
            // 
            this.listView_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_data.Location = new System.Drawing.Point(0, 25);
            this.listView_data.Name = "listView_data";
            this.listView_data.Size = new System.Drawing.Size(1001, 376);
            this.listView_data.TabIndex = 0;
            this.listView_data.UseCompatibleStateImageBehavior = false;
            // 
            // menuStrip_top
            // 
            this.menuStrip_top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem});
            this.menuStrip_top.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_top.Name = "menuStrip_top";
            this.menuStrip_top.Size = new System.Drawing.Size(1001, 25);
            this.menuStrip_top.TabIndex = 1;
            this.menuStrip_top.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择显示项ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 选择显示项ToolStripMenuItem
            // 
            this.选择显示项ToolStripMenuItem.Name = "选择显示项ToolStripMenuItem";
            this.选择显示项ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.选择显示项ToolStripMenuItem.Text = "选择显示项";
            // 
            // comboDbList
            // 
            this.comboDbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDbList.FormattingEnabled = true;
            this.comboDbList.ItemHeight = 12;
            this.comboDbList.Location = new System.Drawing.Point(61, 2);
            this.comboDbList.Name = "comboDbList";
            this.comboDbList.Size = new System.Drawing.Size(414, 20);
            this.comboDbList.TabIndex = 2;
            this.comboDbList.SelectedIndexChanged += new System.EventHandler(this.comboDbList_SelectedIndexChanged);
            // 
            // radiob_qg
            // 
            this.radiob_qg.AutoSize = true;
            this.radiob_qg.Location = new System.Drawing.Point(484, 3);
            this.radiob_qg.Name = "radiob_qg";
            this.radiob_qg.Size = new System.Drawing.Size(47, 16);
            this.radiob_qg.TabIndex = 3;
            this.radiob_qg.TabStop = true;
            this.radiob_qg.Text = "全国";
            this.radiob_qg.UseVisualStyleBackColor = true;
            // 
            // radiob_bj
            // 
            this.radiob_bj.AutoSize = true;
            this.radiob_bj.Location = new System.Drawing.Point(537, 3);
            this.radiob_bj.Name = "radiob_bj";
            this.radiob_bj.Size = new System.Drawing.Size(47, 16);
            this.radiob_bj.TabIndex = 3;
            this.radiob_bj.TabStop = true;
            this.radiob_bj.Text = "北京";
            this.radiob_bj.UseVisualStyleBackColor = true;
            // 
            // b_search
            // 
            this.b_search.Location = new System.Drawing.Point(590, 0);
            this.b_search.Name = "b_search";
            this.b_search.Size = new System.Drawing.Size(75, 23);
            this.b_search.TabIndex = 4;
            this.b_search.Text = "搜索";
            this.b_search.UseVisualStyleBackColor = true;
            this.b_search.Click += new System.EventHandler(this.b_search_Click);
            // 
            // label_message
            // 
            this.label_message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_message.AutoSize = true;
            this.label_message.ForeColor = System.Drawing.Color.Red;
            this.label_message.Location = new System.Drawing.Point(870, 5);
            this.label_message.Name = "label_message";
            this.label_message.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_message.Size = new System.Drawing.Size(0, 12);
            this.label_message.TabIndex = 5;
            // 
            // contextMenuStrip_tableHead
            // 
            this.contextMenuStrip_tableHead.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.隐藏本列ToolStripMenuItem,
            this.导出至ExcelToolStripMenuItem});
            this.contextMenuStrip_tableHead.Name = "contextMenuStrip_tableHead";
            this.contextMenuStrip_tableHead.Size = new System.Drawing.Size(142, 48);
            // 
            // 隐藏本列ToolStripMenuItem
            // 
            this.隐藏本列ToolStripMenuItem.Name = "隐藏本列ToolStripMenuItem";
            this.隐藏本列ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.隐藏本列ToolStripMenuItem.Text = "隐藏本列";
            this.隐藏本列ToolStripMenuItem.Click += new System.EventHandler(this.隐藏本列ToolStripMenuItem_Click);
            // 
            // 导出至ExcelToolStripMenuItem
            // 
            this.导出至ExcelToolStripMenuItem.Name = "导出至ExcelToolStripMenuItem";
            this.导出至ExcelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.导出至ExcelToolStripMenuItem.Text = "导出至Excel";
            this.导出至ExcelToolStripMenuItem.Click += new System.EventHandler(this.导出至ExcelToolStripMenuItem_Click);
            // 
            // DbList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 401);
            this.Controls.Add(this.label_message);
            this.Controls.Add(this.b_search);
            this.Controls.Add(this.radiob_bj);
            this.Controls.Add(this.radiob_qg);
            this.Controls.Add(this.comboDbList);
            this.Controls.Add(this.listView_data);
            this.Controls.Add(this.menuStrip_top);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_top;
            this.Name = "DbList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据列表";
            this.Load += new System.EventHandler(this.DbList_Load);
            this.menuStrip_top.ResumeLayout(false);
            this.menuStrip_top.PerformLayout();
            this.contextMenuStrip_tableHead.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_data;
        private System.Windows.Forms.MenuStrip menuStrip_top;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择显示项ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboDbList;
        private System.Windows.Forms.RadioButton radiob_qg;
        private System.Windows.Forms.RadioButton radiob_bj;
        private System.Windows.Forms.Button b_search;
        private System.Windows.Forms.Label label_message;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tableHead;
        private System.Windows.Forms.ToolStripMenuItem 隐藏本列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出至ExcelToolStripMenuItem;
    }
}