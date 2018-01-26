using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace ShowCompileMessageXML
{
    internal class DataGridViewFunction
    {
        public static int wCount = 0;
        public static int eCount = 0;

        /// <summary>
        /// 设置DataGridView控件的属性
        /// </summary>
        /// <param name="dataGridView">待设置的DataGridView控件</param>
        public static void SetDataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = 5;//设置列数
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//设置表头文本居中
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//可支持多行显示
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//可自动调整行高
            dataGridView.BackgroundColor = Color.White;//设置背景色为白色
        }

        /// <summary>
        /// 初始化单元格
        /// </summary>
        /// <param name="dataGridView"></param>
        /// 选中的单元格
        /// <param name="typestr"></param>
        /// 创建的标志
        public static void InitDataGridView(DataGridView dataGridView, string typestr)
        {
            dataGridView.Columns[0].Name = typestr;//设置第一列标题为“Warning”或“Error”
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//允许多行
            dataGridView.Columns[0].Width = 40;//第一列宽度
            dataGridView.Columns[1].Name = "Location";
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].Width = 43;//第二列宽度
            dataGridView.Columns[2].Name = "Infocode";
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].Width = 43;//第三列宽度
            dataGridView.Columns[3].Name = "Description";
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[4].Name = "Path";
            dataGridView.Columns[4].Width = 180;//第五列宽度
        }

        /// <summary>
        /// 设置单元格行颜色
        /// </summary>
        /// <param name="dataGridView"></param>
        /// 选中的单元格
        public static void SetRowColor(DataGridView dataGridView)
        {
            int i;
            if (dataGridView.Rows.Count != 0)
            {
                for (i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (i % 2 != 0)
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
                    else
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
                }
            }
        }

        /// <summary>
        /// 将单元格内容清空
        /// </summary>
        /// <param name="dataGridView"></param>
        /// 选中的单元格
        public static void ClearDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.RowCount > 1)
            {
                while (dataGridView.RowCount > 1)
                {
                    dataGridView.Rows.Remove(dataGridView.Rows[0]);
                }
            }
        }

        /// <summary>
        /// 在单元格中创建一个新的行
        /// </summary>
        /// <param name="dataGridView"></param>
        /// 选中的单元格
        /// <param name="xmlNode"></param>
        /// 选中的XML结点
        /// <param name="i"></param>
        /// 该XML节点的序号
        public static void CreatNewRow(DataGridView dataGridView, XmlNode xmlNode, int i, string path)
        {
            string num = Convert.ToString(++i);//将i转为字符串
            int index = dataGridView.Rows.Add();//添加新的行
            dataGridView.Rows[index].Cells[0].Value = num;//设置行的序号
            dataGridView.Rows[index].Cells[1].Value = xmlNode.SelectSingleNode("location").InnerText;//设置列值1
            dataGridView.Rows[index].Cells[2].Value = xmlNode.SelectSingleNode("infoCode").InnerText;//设置列值2
            dataGridView.Rows[index].Cells[3].Value = "\t" + xmlNode.SelectSingleNode("description").InnerText;//设置列值3
            dataGridView.Rows[index].Cells[4].Value = path;//设置列值4
        }

        /// <summary>
        /// 在两个单元格中分别显示警告和错误的编译信息
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// 显示单元格1
        /// <param name="dataGridView2"></param>
        /// 显示单元格2
        /// <param name="xmlNode"></param>
        /// 选中的结点
        public static void ShowCompileMessage(DataGridView dataGridView1, DataGridView dataGridView2, XmlNode xmlNode)
        {
            ClearDataGridView(dataGridView1);//每次点击事件后将表格1中的数据清空，重新显示
            ClearDataGridView(dataGridView2);//每次点击事件后将表格2中的数据清空，重新显示
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;//获得该节点下的所有子节点
            wCount = 0;
            eCount = 0;
            foreach (XmlNode xmlNode1 in xmlNodeList)//遍历子节点
            {
                if ((xmlNode1.Name).Substring(0, 7) == "warning")//匹配节点，选出“警告”提示
                {
                    CreatNewRow(dataGridView1, xmlNode1, wCount++, "");
                }
                if ((xmlNode1.Name).Substring(0, 5) == "error")//匹配节点，选出“错误”提示
                {
                    CreatNewRow(dataGridView1, xmlNode1, eCount++, "");
                }
            }
            SetRowColor(dataGridView1);//设置表格1的颜色
            SetRowColor(dataGridView2);//设置表格2的颜色
        }

        /// <summary>
        /// 在DataGridView中显示搜索结果
        /// </summary>
        /// <param name="searchList">搜索结果字符串数组</param>
        /// <param name="dataGridView1">DataGridView控件1</param>
        /// <param name="dataGridView2">DataGridView控件2</param>
        public static void ShowSerarchResult(Collection<string> searchList, DataGridView dataGridView1, DataGridView dataGridView2)
        {
            DataGridViewFunction.ClearDataGridView(dataGridView1);//每次点击事件后将表格1中的数据清空，重新显示
            DataGridViewFunction.ClearDataGridView(dataGridView2);//每次点击事件后将表格2中的数据清空，重新显示
            int i = 0;
            wCount = 0;
            eCount = 0;
            foreach (string s in searchList)//遍历输出
            {
                string xmlNodeName = s.Substring(s.LastIndexOf("/*") + 2);//获取xml结点的名称
                foreach (XmlNode node in TreeViewFunction.allxmlNodeinTree)//遍历子节点
                {
                    if (xmlNodeName == node.Name)//若找到子节点
                    {
                        XmlNodeList xmlNodeList = node.ChildNodes;//获得该节点下的所有子节点
                        foreach (XmlNode x in xmlNodeList)//遍历
                        {
                            if (x.Name.Count() > 7 && x.Name.Substring(0, 7) == "warning")//匹配节点，选出“警告”提示
                            {
                                CreatNewRow(dataGridView1, x, i++, s);
                                wCount++;
                            }
                            if (x.Name.Count() > 5 && x.Name.Substring(0, 5) == "error")//匹配节点，选出“错误”提示
                            {
                                CreatNewRow(dataGridView1, x, i++, s);
                                eCount++;
                            }
                        }
                    }
                }
                DataGridViewFunction.SetRowColor(dataGridView1);//设置表格1的颜色
                DataGridViewFunction.SetRowColor(dataGridView2);//设置表格2的颜色
            }
        }
    }
}