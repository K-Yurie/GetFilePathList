namespace GetFilePathList
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbSelectedPath = new System.Windows.Forms.TextBox();
            this.btnSortFile = new System.Windows.Forms.Button();
            this.lblSelectedFolderName = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOutputCSVFile = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.既定のフォルダを指定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectNewFile = new System.Windows.Forms.Button();
            this.btnSelectAddFile = new System.Windows.Forms.Button();
            this.chkFirstEncountOnly = new System.Windows.Forms.CheckBox();
            this.chkIsSortByExtension = new System.Windows.Forms.CheckBox();
            this.btnOutputExcelFile = new System.Windows.Forms.Button();
            this.pnlNamedByUser = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOutputFileName = new System.Windows.Forms.TextBox();
            this.chkIsNamedByUser = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pnlNamedByUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSelectedPath
            // 
            this.tbSelectedPath.Enabled = false;
            this.tbSelectedPath.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbSelectedPath.Location = new System.Drawing.Point(78, 49);
            this.tbSelectedPath.Name = "tbSelectedPath";
            this.tbSelectedPath.Size = new System.Drawing.Size(677, 23);
            this.tbSelectedPath.TabIndex = 0;
            // 
            // btnSortFile
            // 
            this.btnSortFile.Location = new System.Drawing.Point(27, 51);
            this.btnSortFile.Name = "btnSortFile";
            this.btnSortFile.Size = new System.Drawing.Size(45, 23);
            this.btnSortFile.TabIndex = 1;
            this.btnSortFile.Text = "…";
            this.btnSortFile.UseVisualStyleBackColor = true;
            // 
            // lblSelectedFolderName
            // 
            this.lblSelectedFolderName.AutoSize = true;
            this.lblSelectedFolderName.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSelectedFolderName.Location = new System.Drawing.Point(23, 77);
            this.lblSelectedFolderName.Name = "lblSelectedFolderName";
            this.lblSelectedFolderName.Size = new System.Drawing.Size(106, 21);
            this.lblSelectedFolderName.TabIndex = 2;
            this.lblSelectedFolderName.Text = "ファイル名：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(31, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(742, 360);
            this.dataGridView1.TabIndex = 3;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Number";
            this.Column3.HeaderText = "No.";
            this.Column3.Name = "Column3";
            this.Column3.Width = 50;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "FileName";
            this.Column1.HeaderText = "ファイル名";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FullPath";
            this.Column2.HeaderText = "ファイルパス";
            this.Column2.Name = "Column2";
            this.Column2.Width = 460;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ファイルを選択";
            // 
            // btnOutputCSVFile
            // 
            this.btnOutputCSVFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOutputCSVFile.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOutputCSVFile.Location = new System.Drawing.Point(413, 528);
            this.btnOutputCSVFile.Name = "btnOutputCSVFile";
            this.btnOutputCSVFile.Size = new System.Drawing.Size(360, 24);
            this.btnOutputCSVFile.TabIndex = 5;
            this.btnOutputCSVFile.Text = "ＣＳＶ出力(カンマ区切り)";
            this.btnOutputCSVFile.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.既定のフォルダを指定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 既定のフォルダを指定ToolStripMenuItem
            // 
            this.既定のフォルダを指定ToolStripMenuItem.Name = "既定のフォルダを指定ToolStripMenuItem";
            this.既定のフォルダを指定ToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.既定のフォルダを指定ToolStripMenuItem.Text = "既定のフォルダを指定";
            this.既定のフォルダを指定ToolStripMenuItem.Click += new System.EventHandler(this.既定のフォルダを指定ToolStripMenuItem_Click);
            // 
            // btnSelectNewFile
            // 
            this.btnSelectNewFile.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSelectNewFile.Location = new System.Drawing.Point(527, 101);
            this.btnSelectNewFile.Name = "btnSelectNewFile";
            this.btnSelectNewFile.Size = new System.Drawing.Size(120, 30);
            this.btnSelectNewFile.TabIndex = 8;
            this.btnSelectNewFile.Text = "新規抽出";
            this.btnSelectNewFile.UseVisualStyleBackColor = true;
            // 
            // btnSelectAddFile
            // 
            this.btnSelectAddFile.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSelectAddFile.Location = new System.Drawing.Point(653, 101);
            this.btnSelectAddFile.Name = "btnSelectAddFile";
            this.btnSelectAddFile.Size = new System.Drawing.Size(120, 30);
            this.btnSelectAddFile.TabIndex = 9;
            this.btnSelectAddFile.Text = "追加で抽出";
            this.btnSelectAddFile.UseVisualStyleBackColor = true;
            // 
            // chkFirstEncountOnly
            // 
            this.chkFirstEncountOnly.AutoSize = true;
            this.chkFirstEncountOnly.Checked = true;
            this.chkFirstEncountOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstEncountOnly.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkFirstEncountOnly.Location = new System.Drawing.Point(27, 111);
            this.chkFirstEncountOnly.Name = "chkFirstEncountOnly";
            this.chkFirstEncountOnly.Size = new System.Drawing.Size(253, 20);
            this.chkFirstEncountOnly.TabIndex = 10;
            this.chkFirstEncountOnly.Text = "重複した素材データは表示しない";
            this.chkFirstEncountOnly.UseVisualStyleBackColor = true;
            // 
            // chkIsSortByExtension
            // 
            this.chkIsSortByExtension.AutoSize = true;
            this.chkIsSortByExtension.Checked = true;
            this.chkIsSortByExtension.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsSortByExtension.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkIsSortByExtension.Location = new System.Drawing.Point(286, 111);
            this.chkIsSortByExtension.Name = "chkIsSortByExtension";
            this.chkIsSortByExtension.Size = new System.Drawing.Size(206, 20);
            this.chkIsSortByExtension.TabIndex = 11;
            this.chkIsSortByExtension.Text = "ファイルの種類ごとに並べる";
            this.chkIsSortByExtension.UseVisualStyleBackColor = true;
            // 
            // btnOutputExcelFile
            // 
            this.btnOutputExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOutputExcelFile.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOutputExcelFile.Location = new System.Drawing.Point(43, 528);
            this.btnOutputExcelFile.Name = "btnOutputExcelFile";
            this.btnOutputExcelFile.Size = new System.Drawing.Size(362, 24);
            this.btnOutputExcelFile.TabIndex = 12;
            this.btnOutputExcelFile.Text = "Excelファイル出力";
            this.btnOutputExcelFile.UseVisualStyleBackColor = true;
            // 
            // pnlNamedByUser
            // 
            this.pnlNamedByUser.Controls.Add(this.label3);
            this.pnlNamedByUser.Controls.Add(this.tbOutputFileName);
            this.pnlNamedByUser.Enabled = false;
            this.pnlNamedByUser.Location = new System.Drawing.Point(43, 577);
            this.pnlNamedByUser.Name = "pnlNamedByUser";
            this.pnlNamedByUser.Size = new System.Drawing.Size(730, 40);
            this.pnlNamedByUser.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(33, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "ファイル名：";
            // 
            // tbOutputFileName
            // 
            this.tbOutputFileName.Location = new System.Drawing.Point(100, 12);
            this.tbOutputFileName.Name = "tbOutputFileName";
            this.tbOutputFileName.Size = new System.Drawing.Size(612, 19);
            this.tbOutputFileName.TabIndex = 0;
            // 
            // chkIsNamedByUser
            // 
            this.chkIsNamedByUser.AutoSize = true;
            this.chkIsNamedByUser.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkIsNamedByUser.Location = new System.Drawing.Point(46, 567);
            this.chkIsNamedByUser.Name = "chkIsNamedByUser";
            this.chkIsNamedByUser.Size = new System.Drawing.Size(293, 16);
            this.chkIsNamedByUser.TabIndex = 14;
            this.chkIsNamedByUser.Text = "出力ファイルの名前を指定する（拡張子なしで記述）";
            this.chkIsNamedByUser.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 629);
            this.Controls.Add(this.chkIsNamedByUser);
            this.Controls.Add(this.pnlNamedByUser);
            this.Controls.Add(this.btnOutputExcelFile);
            this.Controls.Add(this.chkIsSortByExtension);
            this.Controls.Add(this.chkFirstEncountOnly);
            this.Controls.Add(this.btnSelectAddFile);
            this.Controls.Add(this.btnSelectNewFile);
            this.Controls.Add(this.btnOutputCSVFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblSelectedFolderName);
            this.Controls.Add(this.btnSortFile);
            this.Controls.Add(this.tbSelectedPath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "利用素材情報を取得する";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlNamedByUser.ResumeLayout(false);
            this.pnlNamedByUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbSelectedPath;
        private System.Windows.Forms.Button btnSortFile;
        private System.Windows.Forms.Label lblSelectedFolderName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOutputCSVFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 既定のフォルダを指定ToolStripMenuItem;
        private System.Windows.Forms.Button btnSelectNewFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnSelectAddFile;
        private System.Windows.Forms.CheckBox chkFirstEncountOnly;
        private System.Windows.Forms.CheckBox chkIsSortByExtension;
        private System.Windows.Forms.Button btnOutputExcelFile;
        private System.Windows.Forms.Panel pnlNamedByUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOutputFileName;
        private System.Windows.Forms.CheckBox chkIsNamedByUser;
    }
}

