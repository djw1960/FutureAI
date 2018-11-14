namespace Future.WinForm
{
    partial class Form1
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
            this.gbx1 = new System.Windows.Forms.GroupBox();
            this.txt_content = new System.Windows.Forms.TextBox();
            this.btn_50 = new System.Windows.Forms.Button();
            this.gbx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.btn_50);
            this.gbx1.Location = new System.Drawing.Point(13, 13);
            this.gbx1.Name = "gbx1";
            this.gbx1.Size = new System.Drawing.Size(881, 58);
            this.gbx1.TabIndex = 0;
            this.gbx1.TabStop = false;
            this.gbx1.Text = "操作";
            // 
            // txt_content
            // 
            this.txt_content.Location = new System.Drawing.Point(13, 77);
            this.txt_content.Multiline = true;
            this.txt_content.Name = "txt_content";
            this.txt_content.Size = new System.Drawing.Size(881, 439);
            this.txt_content.TabIndex = 1;
            // 
            // btn_50
            // 
            this.btn_50.Location = new System.Drawing.Point(6, 21);
            this.btn_50.Name = "btn_50";
            this.btn_50.Size = new System.Drawing.Size(111, 31);
            this.btn_50.TabIndex = 0;
            this.btn_50.Text = "50种生产资料导入";
            this.btn_50.UseVisualStyleBackColor = true;
            this.btn_50.Click += new System.EventHandler(this.btn_50_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 528);
            this.Controls.Add(this.txt_content);
            this.Controls.Add(this.gbx1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbx1;
        private System.Windows.Forms.Button btn_50;
        private System.Windows.Forms.TextBox txt_content;
    }
}

