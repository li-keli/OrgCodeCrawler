namespace OrgCodeCrawler
{
    partial class CreateDB
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
            this.t_dbtext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.b_createDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // t_dbtext
            // 
            this.t_dbtext.Location = new System.Drawing.Point(83, 21);
            this.t_dbtext.Name = "t_dbtext";
            this.t_dbtext.Size = new System.Drawing.Size(146, 21);
            this.t_dbtext.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库名：";
            // 
            // b_createDB
            // 
            this.b_createDB.Location = new System.Drawing.Point(235, 19);
            this.b_createDB.Name = "b_createDB";
            this.b_createDB.Size = new System.Drawing.Size(75, 23);
            this.b_createDB.TabIndex = 2;
            this.b_createDB.Text = "创建数据库";
            this.b_createDB.UseVisualStyleBackColor = true;
            this.b_createDB.Click += new System.EventHandler(this.b_createDB_Click);
            // 
            // CreateDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 66);
            this.Controls.Add(this.b_createDB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.t_dbtext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建数据库";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t_dbtext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button b_createDB;
    }
}