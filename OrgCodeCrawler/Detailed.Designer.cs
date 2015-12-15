namespace OrgCodeCrawler
{
    partial class Detailed
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
            this.list_show = new System.Windows.Forms.ListView();
            this.contextMenu_copy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_copy.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_show
            // 
            this.list_show.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_show.Location = new System.Drawing.Point(0, 0);
            this.list_show.Name = "list_show";
            this.list_show.Size = new System.Drawing.Size(632, 378);
            this.list_show.TabIndex = 0;
            this.list_show.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenu_copy
            // 
            this.contextMenu_copy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem});
            this.contextMenu_copy.Name = "contextMenu_copy";
            this.contextMenu_copy.Size = new System.Drawing.Size(101, 26);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // Detailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 378);
            this.Controls.Add(this.list_show);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Detailed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "详细";
            this.Load += new System.EventHandler(this.Detailed_Load);
            this.contextMenu_copy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView list_show;
        private System.Windows.Forms.ContextMenuStrip contextMenu_copy;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
    }
}