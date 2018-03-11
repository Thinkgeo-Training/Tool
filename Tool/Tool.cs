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
            Console.WriteLine(path);
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
            int times = 0;
            Visit:
            try
            {
                crawler.Crawl(keys, path);  //这个也就是说无论咋样，只要这个爬虫模块除了问题都重新来过，其余的模块与他相似
            }
            catch (Exception ex)
            {
                if (++times <= 5)
                {
                    Console.WriteLine("An error occurred during the search process and the {0} times attempt is under way", times);
                    goto Visit;
                }
                else
                {
                    Console.WriteLine("An error occurred during the search process,the program abort");
                    Console.WriteLine(ex);
                    Environment.Exit(0);  //其实不应该是返回0的
                }
            }
            Console.WriteLine("====================================");
            Console.WriteLine("Search end, start downloading sample");
            DownloadProject downloadProject = new DownloadProject();
            downloadProject.DownloadProjects(path);
            //等下载的时候，就把
            Console.WriteLine("Download end, start compiling ");
            //Compile compile = new Compile();
            //compile.CompileProjects(path);
            //
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