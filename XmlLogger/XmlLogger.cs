using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;

namespace XmlLogger
{
    public class XmlLogger : Logger
    {
        private XmlElement contentElement;

        private XmlDeclaration dec;

        private XmlDocument doc;

        private int errorNumber;

        private Dictionary<XmlElement, int[]> infoNumber;

        private string logFile;

        private int order;

        private XmlElement projectElement;

        private Collection<XmlElement> projectElements;

        private XmlElement slnElement;

        private int warningNumber;

        public override void Initialize(IEventSource eventSource)
        {
            if (null == Parameters)
            {
                throw new LoggerException("Log file was not set.");
            }
            string[] parameters = Parameters.Split(';');

            logFile = parameters[0];
            if (String.IsNullOrEmpty(logFile))
            {
                throw new LoggerException("Log file was not set.");
            }

            if (parameters.Length > 1)
            {
                throw new LoggerException("Too many parameters passed.");
            }

            try
            {
                doc = new XmlDocument();
                dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "");
                doc.AppendChild(dec);
                order = 0;
                warningNumber = 0;
                errorNumber = 0;
                infoNumber = new Dictionary<XmlElement, int[]>();
                projectElements = new Collection<XmlElement>();
            }
            catch (Exception ex)
            {
                if
                (
                    ex is UnauthorizedAccessException
                    || ex is ArgumentNullException
                    || ex is PathTooLongException
                    || ex is DirectoryNotFoundException
                    || ex is NotSupportedException
                    || ex is ArgumentException
                    || ex is SecurityException
                    || ex is IOException
                )
                {
                    throw new LoggerException("Failed to create log file: " + ex.Message);
                }
                else
                {
                    throw;
                }
            }

            eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
            eventSource.WarningRaised += new BuildWarningEventHandler(eventSource_WarningRaised);
            eventSource.ErrorRaised += new BuildErrorEventHandler(eventSource_ErrorRaised);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
        }

        public override void Shutdown()
        {
            XmlElement projectCompileResult = doc.CreateElement("projectCompileResult");
            slnElement.PrependChild(projectCompileResult);
            if (projectElement == null)
            {
                projectCompileResult.InnerText = "the solution have no project";
            }
            else
            {
                foreach (var project in projectElements)
                {
                    string status;
                    if (infoNumber[project][0] == 0)
                    {
                        status = "success";
                    }
                    else
                    {
                        status = "fail";
                    }
                    XmlNode node = createXmlNode(project.Name + "ProjectCompileStatus", "compile " + status);
                    projectCompileResult.AppendChild(node);
                }
            }
            if (warningNumber == 0 && errorNumber == 0)
            {
                slnElement.RemoveChild(contentElement);
            }
            doc.Save(logFile);
            Console.WriteLine("End");
        }

        private void CloseNode(BuildEventArgs e)
        {
            if (order != 0)
            {
                if (infoNumber[projectElement][0] == 0 && infoNumber[projectElement][1] == 0)
                {
                    contentElement.RemoveChild(projectElement);
                    projectElements.Remove(projectElement);
                }
            }
        }

        private XmlNode createXmlNode(string name, string value)
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            return node;
        }

        private void eventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            errorNumber++;
            Console.WriteLine("Error: Project" + e.ProjectFile + "\tFile:" + e.File);
            XmlElement result = getTheElement(e.File);
            infoNumber[result][0]++;
            infoNumber[projectElement][0]++;
            result.SetAttribute("errorFile", "true");
            XmlElement errorElement = doc.CreateElement("error" + infoNumber[result][0]);
            setTheInfoElement(errorElement, e.LineNumber, e.ColumnNumber, e.Code, e.Message);
            result.PrependChild(errorElement);
        }

        private void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            order--;
            CloseNode(e);
        }

        private void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            Console.WriteLine("ProjectStarted:\t" + e.SenderName + "\tFi:" + e.ProjectFile);
            WriteRoot(e);
            order++;
        }

        private void eventSource_WarningRaised(object sender, BuildWarningEventArgs e)
        {
            warningNumber++;
            Console.WriteLine("Warning: Project:" + e.ProjectFile + "\tFile:" + e.File);
            XmlElement result = getTheElement(e.File);
            result.SetAttribute("warningFile", "true");
            infoNumber[result][1]++;
            infoNumber[projectElement][1]++;
            XmlElement warningElement = doc.CreateElement("warning" + infoNumber[result][1]);
            setTheInfoElement(warningElement, e.LineNumber, e.ColumnNumber, e.Code, e.Message);
            result.AppendChild(warningElement);
        }

        private XmlElement getTheElement(string filePath)
        {
            string[] folderName = filePath.Split('\\');
            for (int t = 0; t < folderName.Length; t++)
            {
                folderName[t] = Regex.Replace(folderName[t].Trim(), @"\W", "");
            }
            XmlElement element = null;
            int i = 0;
            try
            {
                XmlNode x = null;
                for (i = 0; i < folderName.Length; i++)
                {
                    x = doc.SelectSingleNode(@"//" + folderName[i].Trim());
                    if (x == null)
                    {
                        break;
                    }
                }
                if (i == folderName.Length)
                {
                    return (XmlElement)x;
                }
                XmlElement parentElement;
                if (i == 0)
                {
                    parentElement = projectElement;
                }
                else
                {
                    parentElement = (XmlElement)contentElement.SelectSingleNode(@"//" + folderName[i - 1]);
                }
                if (i < folderName.Length)
                {
                    for (; i < folderName.Length; i++)
                    {
                        XmlElement childrenElement = doc.CreateElement(folderName[i]);
                        parentElement.AppendChild(childrenElement);
                        parentElement = childrenElement;
                    }
                    element = parentElement;
                    infoNumber.Add(element, new int[2]);
                }
                return element;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error!");
                Console.WriteLine(ex.Message);
                return element;
            }
        }

        private void setTheInfoElement(XmlElement element, int lineNumber, int columnNumber, string infoCode, string description)
        {
            XmlNode locationElement = createXmlNode("location", "(" + lineNumber + "," + columnNumber + ")");
            XmlNode infoCodeElement = createXmlNode("infoCode", infoCode);
            XmlNode descriptionElement = createXmlNode("description", description);
            element.AppendChild(locationElement);
            element.AppendChild(infoCodeElement);
            element.AppendChild(descriptionElement);
        }
        private void WriteRoot(BuildEventArgs e)
        {
            ProjectStartedEventArgs es = (ProjectStartedEventArgs)e;
            string name = es.ProjectFile;
            int startIndex = name.LastIndexOf('\\');
            int endIndex = name.LastIndexOf('.');
            int length = endIndex - startIndex - 1;
            string nodeName = name.Substring(startIndex + 1, length);
            if (order == 0)
            {
                XmlElement slnEle = doc.CreateElement(nodeName);
                slnElement = slnEle;
                doc.AppendChild(slnEle);
                contentElement = doc.CreateElement("projectCompileMessage");
                slnElement.AppendChild(contentElement);
            }
            else
            {
                XmlElement projectEle = doc.CreateElement(nodeName);
                projectElement = projectEle;
                infoNumber.Add(projectEle, new int[2]);
                contentElement.AppendChild(projectEle);
                projectElements.Add(projectEle);
            }
        }
    }
}