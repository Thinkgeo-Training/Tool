using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ShowCompileMessageXML
{
    public partial class XMLReader : Form
    {
        public XMLReader()
        {
            InitializeComponent();
        }

        private string xmlName = "ReadAndWriteXml.xml";
        private string fileName = "";
        private string fileFolderName = "";
        private string searchFileName = null;//搜索记录

        private Collection<string> searchResult = new Collection<string>();//创建一个数组存储查找到的路径
        private Collection<string> allPath = new Collection<string>();//创建一个数组存储所有节点路径
        private TreeNode searchTreeNode = null;//搜索中选中的树节点

        public void AutoScale(Form frm)
        {
            frm.Tag = frm.Width.ToString() + "," + frm.Height.ToString();
            frm.SizeChanged += new EventHandler(Form1_Resize);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            searchFileText.DroppedDown = false;//设置下拉框隐藏
            DataGridViewFunction.SetDataGridView(dataGridView1);//默认设置表格1
            DataGridViewFunction.SetDataGridView(dataGridView2);//默认设置表格2
            DataGridViewFunction.InitDataGridView(dataGridView1, "Warning");//初始化表格1
            DataGridViewFunction.InitDataGridView(dataGridView2, "Error");//初始化表格2

            ReadAndWriteXML.CreateXmlFile(xmlName, 4, 4, 4);//创建xml文件
            ShowHistory(xmlName, openFileText, 1);//显示历史文件记录
            ShowHistory(xmlName, openFileFolderText, 2);//显示历史文件夹记录
            AutoScale(this);
        }

        private void openFile_Button_Click(object sender, EventArgs e)//打开文件
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileText.Text = openFileDialog.FileName;//将文件夹名称显示在openFileText文本框
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)//treeview响应事件
        {
            if (searchTreeNode != null)
            {
                searchTreeNode.ForeColor = Color.Black;
            }
            if (e.Node.Nodes.Count == 0)//若选中叶节点
            {
                CleanWindows();//将窗体程序相应地方清空

                foreach (XmlNode x in TreeViewFunction.allxmlNodeinTree)//遍历存储的xml结点
                {
                    if (x.Name == e.Node.Text)//若两个结点名称相同
                    {
                        DataGridViewFunction.ShowCompileMessage(dataGridView1, dataGridView2, x);//显示信息
                        showw.Text = Convert.ToString(DataGridViewFunction.wCount);
                        showe.Text = Convert.ToString(DataGridViewFunction.eCount);
                        break;//退出循环
                    }
                }
            }
        }

        private void openFileFolder_Button_Click(object sender, EventArgs e)//打开文件夹
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)//打开文件夹
            {
                openFileFolderText.Text = dialog.SelectedPath;
            }
            dialog.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)//查找文件
        {
            CleanWindows();//将窗体程序相应地方清空

            ReadAndWriteXML.WriteFolderOrFileToXml(xmlName, searchFileName, 3);//将历史搜索结果写入XML程序
            if (searchFileName.Length == 0)//若无查找内容
            {
                MessageBox.Show("请输入查找的内容");
            }
            else
            {
                if (searchTreeNode != null)
                {
                    searchTreeNode.ForeColor = Color.Black;//将上次选中树节点的颜色变为黑色
                }
                treeView1.CollapseAll();//关闭所有节点
                ExpandNodes(treeView1.Nodes, searchFileName);//选择展开某节点
                DataGridViewFunction.ShowSerarchResult(searchResult, dataGridView1, dataGridView2);//在DataGridView中显示结果

                showw.Text = Convert.ToString(DataGridViewFunction.wCount);//lable刷新显示warning数量
                showe.Text = Convert.ToString(DataGridViewFunction.eCount);//lable刷新显示error数量
            }
        }

        /// <summary>
        /// 在ComboBox中显示历史记录
        /// </summary>
        /// <param name="xmlName">存储历史记录的XML文件夹</param>
        /// <param name="comboBox">被显示的ComboBox控件</param>
        /// <param name="type">显示记录的类型（1：文件 2：文件夹 3：搜索记录）</param>
        private void ShowHistory(string xmlName, ComboBox comboBox, int type)//在ComboBox中显示历史记录
        {
            comboBox.Items.Clear();
            if (File.Exists(xmlName))
            {
                Collection<string> names = ReadAndWriteXML.LoadXmlFile(xmlName, type);
                foreach (string str in names)
                {
                    comboBox.Items.Add(str);
                }
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 遍历所有树（TreeView）节点，根据关键字找出指定节点
        /// </summary>
        /// <param name="tnodes">被查找的树节点集合</param>
        /// <param name="searchFileName">查找关键字（字符串）</param>
        private void ExpandNodes(TreeNodeCollection tnodes, string searchFileName)//遍历所有节点，找出指定节点
        {
            foreach (TreeNode node in tnodes)//遍历树节点集合
            {
                if (node.Text == searchFileName && node.Nodes.Count == 0)//若找到符合条件的节点且该节点为最后一级节点
                {
                    searchTreeNode = node;//赋值
                    searchTreeNode.ForeColor = Color.Red;//改变颜色
                    ExpandParentNodes(node.Parent.Nodes);//调用下一个递归方法，使其从根到当前节点全部展开
                }
                ExpandNodes(node.Nodes, searchFileName);//如果没有找到符合条件的结点，则递归遍历它的下一级结点集合
            }
        }

        /// <summary>
        /// 从根节点展开到当前节点
        /// </summary>
        /// <param name="tnodes">子节点</param>
        private void ExpandParentNodes(TreeNodeCollection tnodes)
        {
            TreeNode parentNode;
            foreach (TreeNode node in tnodes)
            {
                parentNode = node;
                if (parentNode.Parent != null)
                {
                    parentNode.Parent.Expand();
                    if (parentNode.Parent.Parent != null)//判断父节点的父节点是否为空，如果已经达到根节点就是null
                    {
                        ExpandParentNodes(parentNode.Parent.Parent.Nodes);
                    }
                    else
                        break;//已经达到根节点，退出
                }
            }
        }

        /// <summary>
        /// 递归调用获取XML节点在文档中的的绝对路径
        /// </summary>
        /// <param name="x">待获取路径的结点</param>
        /// <returns></returns>
        private static string getXMLAbsolutePath(XmlNode x)//递归调用获取节点的绝对路径
        {
            if (x.ParentNode != null)
            {
                return getXMLAbsolutePath(x.ParentNode) + "/*" + x.Name;
            }
            else
                return x.Name;
        }

        /// <summary>
        /// 获取所有XML子节点的在文档中的路径
        /// </summary>
        /// <param name="xlist">XML节点集合</param>
        /// <returns></returns>
        private static Collection<string> GetallXMLNodePath(Collection<XmlNode> xlist)//获取所有节点的路径
        {
            Collection<string> allpath = new Collection<string>();
            foreach (XmlNode node in xlist)
            {
                allpath.Add(getXMLAbsolutePath(node));
            }
            return allpath;
        }

        /// <summary>
        /// 遍历XML所有节点的路径，找出符合条件的结点
        /// </summary>
        /// <param name="allpath">存储所有XML结点路径的字符串数组</param>
        /// <param name="value">查找字符串</param>
        /// <returns></returns>
        private Collection<string> FindNodes(Collection<string> allpath, string value)//遍历XML所有节点的路径，找出符合条件的结点
        {
            Collection<string> searchList = new Collection<string>();
            foreach (string s in allpath)//遍历所有路径
            {
                string ss = s.Substring(s.LastIndexOf("/*") + 2);
                if (ss.Length >= value.Length && ss.Substring(0, value.Length).ToLower() == value.ToLower())//全部转换为大小写比较
                {
                    searchList.Add(s);//存储路径
                }
            }
            return searchList;//返回搜索数组
        }

        /// <summary>
        /// 使窗体程序部分内容清空
        /// </summary>
        private void CleanWindows()
        {
            DataGridViewFunction.ClearDataGridView(dataGridView1);//将表格1清空
            DataGridViewFunction.ClearDataGridView(dataGridView1);//将表格2清空
            showw.Text = "";///将标签清空
            showe.Text = "";//将标签清空
                            // searchFileText.Items.Clear();//清空下拉框
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            CleanWindows();//将窗体程序相应地方清空

            searchFileText.Text = "";//搜索框文本清空
            searchFileName = "";//将搜索名称清空
            searchResult.Clear();//清空搜索结果
            allPath.Clear();//清空所有路径

            fileName = openFileText.Text;//保存文件路径
            TreeViewFunction.ClearTreeView(treeView1);//每次重新兼树前将树清空

            try//错误检测
            {
                if (TreeViewFunction.allxmlNodeinTree != null)//如果数组非空
                {
                    TreeViewFunction.allxmlNodeinTree.Clear();//将数组清空
                }//若存储叶节点数组非空，则将数组清空
                TreeViewFunction.SetTree(xmlName, fileName, treeView1, treeView1.Nodes);//建树
                ReadAndWriteXML.WriteFolderOrFileToXml(xmlName, fileName, 1);
                ShowHistory(xmlName, openFileText, 1);//显示历史记录
                allPath = GetallXMLNodePath(TreeViewFunction.allxmlNodeinTree);//获得当前xml文档的所有绝对路径'
            }
            catch (Exception ex)
            {
                MessageBox.Show("error,error code:" + ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void LoadFileFolder_Click(object sender, EventArgs e)//文件下载
        {
            CleanWindows();//将窗体程序相应地方清空

            searchFileText.Text = "";//搜索框文本清空
            searchFileName = "";//将搜索名称清空
            searchResult.Clear();//清空搜索结果
            allPath.Clear();//清空所有路径

            fileFolderName = openFileFolderText.Text;//保存文件夹路径
            TreeViewFunction.ClearTreeView(treeView1);//每次重新兼树前将树清空

            try
            {
                if (TreeViewFunction.allxmlNodeinTree != null)//如果数组非空
                {
                    TreeViewFunction.allxmlNodeinTree.Clear();//将数组清空
                }//若存储叶节点数组非空，则将数组清空
                TreeViewFunction.SetManyTree(xmlName, fileFolderName, treeView1);
                ReadAndWriteXML.WriteFolderOrFileToXml(xmlName, fileFolderName, 2);
                ShowHistory(xmlName, openFileFolderText, 2);//显示历史记录
                if (allPath != null)
                {
                    allPath.Clear();//请空所有路径数组
                }
                allPath = GetallXMLNodePath(TreeViewFunction.allxmlNodeinTree);//获得当前xml文档的所有绝对路径
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error,error code:" + ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show("文件格式错误！");
                TreeViewFunction.ClearTreeView(treeView1);//将树清空
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 显示已查找到的文件名
        /// </summary>
        /// <param name="searchResult">查找结果字符串数组</param>
        /// <param name="comboBox">被显示的ComboBox控件</param>
        private void ShowSearchFile(Collection<string> searchResult, ComboBox comboBox)//显示已查找到的文件名
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(searchFileName);
            foreach (string name in searchResult)//遍历查找结果
            {
                comboBox.Items.Add(name.Substring(name.LastIndexOf("/*") + 2));
            }
        }

        private void searchFileText_TextUpdate(object sender, EventArgs e)//搜索文本框内容改变触发事件
        {
            searchFileName = searchFileText.Text;
            if (searchFileName.Length != 0)//如果输入不为空
            {
                searchResult = FindNodes(allPath, searchFileName);//返回查找到的存在关键字的路径
                ShowSearchFile(searchResult, searchFileText);//在下拉狂显示搜索结果
                Cursor = Cursors.Default;//设置鼠标状态为一开始的默认状态，防止鼠标被覆盖
                searchFileText.SelectionStart = searchFileText.Text.Length;//使光标位置在文本末
                searchFileText.DroppedDown = true;//设置下拉框显示
            }
        }

        private void searchFileText_SelectedIndexChanged(object sender, EventArgs e)//下拉框点击响应事件
        {
            searchFileName = searchFileText.Text;
            searchResult = FindNodes(allPath, searchFileName);//返回查找到的存在关键字的路径
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            string[] tmp = ((Form)sender).Tag.ToString().Split(',');
            float width = (float)((Form)sender).Width / (float)Convert.ToInt16(tmp[0]);
            float heigth = (float)((Form)sender).Height / (float)Convert.ToInt16(tmp[1]);
            ((Form)sender).Tag = ((Form)sender).Width.ToString() + "," + ((Form)sender).Height;
            foreach (Control control in ((Form)sender).Controls)
            {
                control.Scale(new SizeF(width, heigth));
            }
        }
    }
}