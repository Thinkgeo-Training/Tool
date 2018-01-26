using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ShowCompileMessageXML
{
    internal class TreeViewFunction
    {
        public static Collection<XmlNode> allxmlNodeinTree = new Collection<XmlNode>();//定义一个数组存储XML文档所有的叶节点

        public static void CreatTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                TreeNode newchild = new TreeNode();//创建新的树节点
                newchild.Text = node.Name;//给树节点赋值
                treeNodes.Add(newchild);//加入树节点
                                        //  allxmlNodeinTree.Add(node);//将xml节点存入数组
                                        //test
                XmlAttributeCollection x = node.Attributes;//获取当前节点下的所有属性
                int flag = 0;//定义标志
                if (x != null)//若属性集非空
                {
                    foreach (XmlAttribute xx in x)//遍历属性集
                    {
                        if (xx.Name == "warningFile")//若遍历到warningFile属性，则退出，停止创建树节点
                        {
                            flag = 1;//标志变为1
                            break;
                        }
                    }
                }

                if (flag == 0)//若标识变量为0
                {
                    CreatTreeNode(node, newchild.Nodes);//继续创建树节点
                }
                else
                {
                    allxmlNodeinTree.Add(node);//将xml节点存入数组
                }
            }
        }//递归创建树节点

        public static void SetManyTree(string xmlFileName, string sourthPath, TreeView treeView)//为一个文件夹建树
        {
            DirectoryInfo dir = new DirectoryInfo(sourthPath);
            FileInfo[] filesub = dir.GetFiles();
            int i = 0;
            foreach (FileInfo d in filesub)//遍历文件夹
            {
                TreeNode newchild = new TreeNode();//创建根节点
                newchild.Text = d.Name;//给根节点赋值
                treeView.Nodes.Add(newchild);//加入根节点
                SetTree(xmlFileName, d.FullName, treeView, newchild.Nodes);
                i++;
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="xmlFilename"></param>
        public static void SetTree(string xmlFileName, string fileName, TreeView treeView, TreeNodeCollection treeNodes)//读取XML文件，建树
        {
            XmlDocument filesXml = new XmlDocument();
            filesXml.Load(fileName);//下载xml文件
            XmlNodeList rootList = filesXml.ChildNodes;
            int i = 0;
            int flag = 0;
            foreach (XmlNode xmlNode in rootList)
            {
                if (flag == 1)
                {
                    TreeNode newchild = new TreeNode();//创建根节点
                    newchild.Text = xmlNode.Name;//给根节点赋值
                    treeNodes.Add(newchild);//加入根节点
                    CreatTreeNode(xmlNode, newchild.Nodes);//继续创建根节点以下结点
                    i++;
                }
                flag = 1;
            }
        }

        public static void ClearTreeView(TreeView treeView)
        {
            if (treeView.Nodes.Count != 0)//若树不为空
            {
                treeView.Nodes.Clear();//清空树
            }
        }
    }
}