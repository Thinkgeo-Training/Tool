using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace Tool
{
    public class Tool
    {
        public void AutoCompile()
        {
            Collection<string> keys = GetSelectKeys();
            string path = GetSelectResultPath();
            if (path == null)
            {
                Console.WriteLine("No search keyword set, the program ends");
                return;
            }
            WebCrawl crawler = new WebCrawl();
            Console.WriteLine("start...");
            Console.WriteLine("Searching...");
            Console.WriteLine("====================================");
            Console.WriteLine("selectResult:");
            crawler.Crawl(keys, path);
            Console.WriteLine("====================================");
            Console.WriteLine("Search end, start downloading sample");
            DownloadProject downloadProject = new DownloadProject();
            downloadProject.DownloadProjects(path);
            Console.WriteLine("Download end, start compiling ");
            Compile compile = new Compile();
            compile.CompileProjects(path);
            Console.WriteLine("Compile end");
        }

        public string GetCompileTxtPath()
        {
            const string nodeName = "compileTxt";
            return GetTheSpecifiedPath(nodeName);
        }

        public string GetDownSouceTxtPath()
        {
            const string nodeName = "compileTxt";
            return GetTheSpecifiedPath(nodeName);
        }

        public Collection<string> GetSelectKeys()
        {
            XmlDocument xmldoc = new XmlDocument();
            string configFilePath = "../../../config.xml";
            xmldoc.Load(configFilePath);
            XmlNodeList keyNodes = xmldoc.GetElementsByTagName("selectKey");
            Collection<string> keys = new Collection<string>();
            foreach (XmlElement element in keyNodes)
            {
                string key = element.GetElementsByTagName("value")[0].InnerText;
                if (key.Equals("all"))
                {
                    keys.Clear();
                    keys.Add(key);
                    break;
                }
                keys.Add(key);
            }
            return keys;
        }

        public string GetSelectResultPath()
        {
            const string nodeName = "compileTxt";
            return GetTheSpecifiedPath(nodeName);
        }

        private static void Main(string[] args)
        {
            Tool tool = new Tool();
            tool.AutoCompile();
        }

        private string GetTheSpecifiedPath(string nodeName)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("../../../config.xml");
            XmlNode pathNode = xmldoc.SelectSingleNode("params/" + nodeName);
            return pathNode?.InnerText;
        }
    }
}