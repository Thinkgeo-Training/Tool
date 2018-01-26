namespace ShowCompileMessageXML
{
    partial class XMLReader
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
            this.openFileFolder_Button = new System.Windows.Forms.Button();
            this.LoadFileFolder = new System.Windows.Forms.Button();
            this.LoadFile = new System.Windows.Forms.Button();
            this.openFile_Button = new System.Windows.Forms.Button();
            this.openFileFolderText = new System.Windows.Forms.ComboBox();
            this.openFileText = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.showw = new System.Windows.Forms.Label();
            this.showe = new System.Windows.Forms.Label();
            this.searchFileText = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileFolder_Button
            // 
            this.openFileFolder_Button.Location = new System.Drawing.Point(945, 40);
            this.openFileFolder_Button.Margin = new System.Windows.Forms.Padding(2);
            this.openFileFolder_Button.Name = "openFileFolder_Button";
            this.openFileFolder_Button.Size = new System.Drawing.Size(50, 23);
            this.openFileFolder_Button.TabIndex = 0;
            this.openFileFolder_Button.Text = "Browse..";
            this.openFileFolder_Button.UseVisualStyleBackColor = true;
            this.openFileFolder_Button.Click += new System.EventHandler(this.openFileFolder_Button_Click);
            // 
            // LoadFileFolder
            // 
            this.LoadFileFolder.Location = new System.Drawing.Point(994, 40);
            this.LoadFileFolder.Margin = new System.Windows.Forms.Padding(2);
            this.LoadFileFolder.Name = "LoadFileFolder";
            this.LoadFileFolder.Size = new System.Drawing.Size(50, 23);
            this.LoadFileFolder.TabIndex = 1;
            this.LoadFileFolder.Text = "Load";
            this.LoadFileFolder.UseVisualStyleBackColor = true;
            this.LoadFileFolder.Click += new System.EventHandler(this.LoadFileFolder_Click);
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(441, 40);
            this.LoadFile.Margin = new System.Windows.Forms.Padding(2);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(50, 23);
            this.LoadFile.TabIndex = 2;
            this.LoadFile.Text = "Load";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // openFile_Button
            // 
            this.openFile_Button.Location = new System.Drawing.Point(393, 40);
            this.openFile_Button.Margin = new System.Windows.Forms.Padding(2);
            this.openFile_Button.Name = "openFile_Button";
            this.openFile_Button.Size = new System.Drawing.Size(50, 23);
            this.openFile_Button.TabIndex = 3;
            this.openFile_Button.Text = "Browse..";
            this.openFile_Button.UseVisualStyleBackColor = true;
            this.openFile_Button.Click += new System.EventHandler(this.openFile_Button_Click);
            // 
            // openFileFolderText
            // 
            this.openFileFolderText.FormattingEnabled = true;
            this.openFileFolderText.Location = new System.Drawing.Point(663, 43);
            this.openFileFolderText.Margin = new System.Windows.Forms.Padding(2);
            this.openFileFolderText.Name = "openFileFolderText";
            this.openFileFolderText.Size = new System.Drawing.Size(279, 20);
            this.openFileFolderText.TabIndex = 4;
            // 
            // openFileText
            // 
            this.openFileText.FormattingEnabled = true;
            this.openFileText.Location = new System.Drawing.Point(99, 40);
            this.openFileText.Margin = new System.Windows.Forms.Padding(2);
            this.openFileText.Name = "openFileText";
            this.openFileText.Size = new System.Drawing.Size(291, 20);
            this.openFileText.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(970, 87);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "OK";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(23, 133);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(261, 479);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(287, 163);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(766, 218);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(286, 417);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(767, 194);
            this.dataGridView2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "OpenFile:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(588, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "FileFolder:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(607, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Search:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "Warning:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 393);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Error:";
            // 
            // showw
            // 
            this.showw.AutoSize = true;
            this.showw.Location = new System.Drawing.Point(403, 139);
            this.showw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.showw.Name = "showw";
            this.showw.Size = new System.Drawing.Size(11, 12);
            this.showw.TabIndex = 15;
            this.showw.Text = " ";
            // 
            // showe
            // 
            this.showe.AutoSize = true;
            this.showe.Location = new System.Drawing.Point(395, 395);
            this.showe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.showe.Name = "showe";
            this.showe.Size = new System.Drawing.Size(11, 12);
            this.showe.TabIndex = 16;
            this.showe.Text = " ";
            // 
            // searchFileText
            // 
            this.searchFileText.FormattingEnabled = true;
            this.searchFileText.Location = new System.Drawing.Point(663, 88);
            this.searchFileText.Margin = new System.Windows.Forms.Padding(2);
            this.searchFileText.Name = "searchFileText";
            this.searchFileText.Size = new System.Drawing.Size(304, 20);
            this.searchFileText.TabIndex = 17;
            this.searchFileText.SelectedIndexChanged += new System.EventHandler(this.searchFileText_SelectedIndexChanged);
            this.searchFileText.TextUpdate += new System.EventHandler(this.searchFileText_TextUpdate);
            // 
            // XMLReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 640);
            this.Controls.Add(this.showe);
            this.Controls.Add(this.showw);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.openFileText);
            this.Controls.Add(this.openFileFolderText);
            this.Controls.Add(this.openFile_Button);
            this.Controls.Add(this.LoadFile);
            this.Controls.Add(this.LoadFileFolder);
            this.Controls.Add(this.openFileFolder_Button);
            this.Controls.Add(this.searchFileText);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "XMLReader";
            this.Text = "XML Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileFolder_Button;
        private System.Windows.Forms.Button LoadFileFolder;
        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.Button openFile_Button;
        private System.Windows.Forms.ComboBox openFileFolderText;
        private System.Windows.Forms.ComboBox openFileText;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label showw;
        private System.Windows.Forms.Label showe;
        private System.Windows.Forms.ComboBox searchFileText;
    }
}

