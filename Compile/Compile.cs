using System;
using System.Diagnostics;
using System.IO;

namespace Tool
{
    public class Compile
    {
        public void CompileProjects(string compileTxt)
        {
            string currentPath = Directory.GetCurrentDirectory();
            //Get the AppConfig settings
            const string loggerClass = "XmlLogger";
            const string loggerPath = "XmlLogger.dll";
            string nugetPath = Directory.GetParent("../../../Resources/nuget.exe") + @"/nuget.exe";
            string msbuildPath = Directory.GetParent("../../../Resources/MSBuild.exe") + @"/MSBuild.exe";
            //Switch work path
            string diskDriveCMD = currentPath.Substring(0, 2) + " & cd " + currentPath;
            string compileTxtPath = currentPath + "\\" + compileTxt;
            //Setting up the log generation directory
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string projectPath = Directory.GetParent("../../../Result/Project") + @"/Project";
            string logDirctoryPath = Directory.GetParent("../../../Result/Log") + @"/Log/" + dateTime;
            if (!File.Exists(nugetPath))
            {
                ShowMessage("");
                return;
            }
            if (!File.Exists(msbuildPath))
            {
                ShowMessage(msbuildPath);
                return;
            }
            if (!File.Exists(compileTxtPath))
            {
                ShowMessage(compileTxt);
                return;
            }
            if (!File.Exists(loggerPath))
            {
                ShowMessage(loggerPath);
                return;
            }
            if (!Directory.Exists(logDirctoryPath))
            {
                Directory.CreateDirectory(logDirctoryPath);
            }
            //Call CMD
            Process compile = new Process();
            compile.StartInfo.FileName = "cmd.exe";
            compile.StartInfo.UseShellExecute = false;
            compile.StartInfo.RedirectStandardInput = true;
            compile.StartInfo.RedirectStandardOutput = true;
            compile.StartInfo.RedirectStandardError = true;
            compile.StartInfo.CreateNoWindow = true;
            compile.Start();
            compile.StandardInput.WriteLine(diskDriveCMD);
            string[] projectNames = File.ReadAllLines(compileTxtPath);
            foreach (var item in projectNames)
            {
                var files = Directory.GetFiles(projectPath + @"/" + item, "*.sln");
                if (files.Length == 0)
                    continue;
                //Update command for nuget
                string updataCmd = nugetPath + " update " + files[0];
                //Nuget's recovery command
                string restoreCmd = nugetPath + " restore " + files[0];
                //Compile command of nuget
                string msbuildCmd = msbuildPath + " " + files[0] + " /t:Rebuild /p:Configuration=Debug /nologo /noconsolelogger /logger:" +
                    loggerClass + "," + loggerPath + ";" + logDirctoryPath + "\\" + item + ".xml";
                //Pass the command to the CMD
                compile.StandardInput.WriteLine(updataCmd);
                compile.StandardInput.WriteLine(restoreCmd);
                compile.StandardInput.WriteLine(msbuildCmd);
            }
            compile.StandardInput.AutoFlush = true;
            //The last command passes the exit command.Otherwise the last call to the ReadToEnd method will be suspended
            compile.StandardInput.WriteLine("exit");
            string str = compile.StandardOutput.ReadToEnd();//Why does not call this method at last will die
            compile.WaitForExit();
            compile.Close();
        }

        private static void ShowMessage(string messageStr)
        {
            Console.WriteLine(messageStr + " does not exist！" + "Please check the configuration file!");
        }
    }
}