using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace ShowCompileMessageXML
{
    internal class ReadAndWriteXML
    {
        private static int folderCount;
        private static int filesCount;
        private static int historyCount;

        public static void CreateXmlFile(string xmlName, int nfolderCount, int nfilesCount, int nhistoryCount)
        {
            folderCount = nfolderCount;
            filesCount = nfilesCount;
            historyCount = nhistoryCount;
            XmlDocument filesXml = new XmlDocument();
            if (!File.Exists(xmlName))
            {
                //添加XML文件的申明
                XmlDeclaration xmlDec = filesXml.CreateXmlDeclaration("1.0", "gb2312", null);
                filesXml.AppendChild(xmlDec);
                //添加根节点
                XmlElement xmlElement = filesXml.CreateElement("Infomation");
                filesXml.AppendChild(xmlElement);
                //选中当前节点
                XmlNode node = filesXml.SelectSingleNode("Infomation");

                //创建Folder节点
                xmlElement = filesXml.CreateElement("Folder");
                xmlElement.SetAttribute("FolderCount", Convert.ToString(nfolderCount));
                node.AppendChild(xmlElement);

                //创建Files节点
                xmlElement = filesXml.CreateElement("Files");
                xmlElement.SetAttribute("FilesCount", Convert.ToString(nfilesCount));
                node.AppendChild(xmlElement);

                //创建History节点
                xmlElement = filesXml.CreateElement("History");
                xmlElement.SetAttribute("HistoriesCount", Convert.ToString(nhistoryCount));
                node.AppendChild(xmlElement);

                filesXml.Save(xmlName);//保存文件
            }
        }

        public static void WriteFolderOrFileToXml(string xmlName, string name, int type)
        {
            XmlDocument filesXml = new XmlDocument();
            filesXml.Load(xmlName);

            XmlNode root = filesXml.SelectSingleNode("Infomation");

            XmlNode xRoot1 = root.SelectSingleNode("Folder");
            int count1 = Convert.ToInt32(((XmlElement)xRoot1).Attributes["FolderCount"].Value);

            XmlNode xRoot2 = root.SelectSingleNode("Files");
            int count2 = Convert.ToInt32(((XmlElement)xRoot2).Attributes["FilesCount"].Value);

            XmlNode xRoot3 = root.SelectSingleNode("History");
            int count3 = Convert.ToInt32(((XmlElement)xRoot3).Attributes["HistoriesCount"].Value);

            int count = 0;
            XmlNode r = null;//赋不用值

            root = filesXml.SelectSingleNode("Infomation");
            //保存文件夹路径

            if (type == 1)
            {
                r = root.SelectSingleNode("Folder");
                count = count1;
            }
            //保存文件路径
            if (type == 2)
            {
                r = root.SelectSingleNode("Files");
                count = count2;
            }
            if (type == 3)
            {
                r = root.SelectSingleNode("History");
                count = count3;
            }

            XmlNodeList nodeList = r.SelectNodes("List");//获取List节点集合
            foreach (XmlNode node in nodeList)//遍历List
            {//保证最近使用过的文件在后后面
                if (node.InnerText == name)//若xml中存在该文件
                {
                    r.RemoveChild(node);//将该文件删除，节点自动向前移
                    r.AppendChild(node);//在末尾加入该节点
                    filesXml.Save(xmlName);//保存文件
                    return;
                }
            }

            if (nodeList.Count < count)
            {
                //若当前节点数量小于默认设置的数量，在文件末尾加入该节点
                XmlElement xmlElement2 = filesXml.CreateElement("List");
                xmlElement2.InnerText = name;
                r.AppendChild(xmlElement2);
            }
            else
            {//保证节点先后次序,将nodeList中第一个节点删除
                r.RemoveAll();//移除所有节点,再重置其属性
                for (int i = 1; i <= nodeList.Count - 1; i++)
                {
                    r.AppendChild(nodeList[i]);
                }//此时删除了第一个节点；
                XmlElement xmlElement2 = filesXml.CreateElement("List");
                xmlElement2.InnerText = name;
                r.AppendChild(xmlElement2);
                //  MessageBox.Show("已达存储上限，删除一个节点");
                //增加删掉的属性
                if (type == 1)
                    ((XmlElement)r).SetAttribute("FolderCount", Convert.ToString(folderCount));
                if (type == 2)
                    ((XmlElement)r).SetAttribute("FilesCount", Convert.ToString(filesCount));
                if (type == 3)
                    ((XmlElement)r).SetAttribute("HistoriesCount", Convert.ToString(historyCount));
            }
            filesXml.Save(xmlName);
        }

        public static Collection<string> LoadXmlFile(string xmlName, int type)
        {
            Collection<string> names = new Collection<string>();
            XmlDocument filesXml = new XmlDocument();
            filesXml.Load(xmlName);
            XmlNode root = filesXml.SelectSingleNode("Infomation");
            XmlNode r = root;
            if (type == 1)
            {
                r = root.SelectSingleNode("Folder");
            }
            if (type == 2)
            {
                r = root.SelectSingleNode("Files");
            }
            if (type == 3)
            {
                r = root.SelectSingleNode("History");
            }
            XmlNodeList nodeList = r.SelectNodes("List");
            foreach (XmlNode node in nodeList)
            {
                names.Add(node.InnerText);
            }
            return names;
        }
    }
}