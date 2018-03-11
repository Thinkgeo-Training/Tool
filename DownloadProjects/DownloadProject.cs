using System;
using System.Diagnostics;
using System.IO;

namespace Tool
{
    public class DownloadProject
    {
        public void DownloadProjects(string downSouceTxt)
        {
            //Get the AppConfig settings
            string currentPath = Directory.GetCurrentDirectory();
            string projectPath = Directory.GetParent("../../../Result/Project") + @"/Project";
            //Switch work path command
            string diskDrive = currentPath.Substring(0, 2) + " & cd " + projectPath;
            string filePath = string.Format(currentPath + "\\" + downSouceTxt);
            string baseUrl = "https://github.com/ThinkGeo";
            //Because only a command is needed.So run a CMD command directly.  git clone url
            // string cmd = "for /f %i in (" + filePath + ") do git clone " + baseUrl + "/" + "%i";
            string cmd;
            if (!File.Exists(filePath))
            {
                Console.WriteLine(downSouceTxt + " does not exist！" + "Please check the configuration file!");
                return;
            }
            if (!Directory.Exists(projectPath))
            {
                Directory.CreateDirectory(projectPath);
            }
            //call cmd
            Process downProjectCMD = new Process();
            downProjectCMD.StartInfo.FileName = "cmd.exe";
            downProjectCMD.StartInfo.UseShellExecute = false;
            downProjectCMD.StartInfo.RedirectStandardInput = true;
            downProjectCMD.StartInfo.RedirectStandardOutput = true;
            downProjectCMD.StartInfo.RedirectStandardError = true;
            downProjectCMD.StartInfo.CreateNoWindow = true;
            downProjectCMD.Start();
            //cd work path
            // downProjectCMD.StandardInput.WriteLine(diskDrive);
            downProjectCMD.StandardInput.WriteLine("echo off");
            downProjectCMD.StandardInput.AutoFlush = true;
            string[] projectNames = File.ReadAllLines(downSouceTxt);
            foreach (var item in projectNames)
            {
                downProjectCMD.StandardInput.WriteLine(diskDrive);
                string sampleDirectory = projectPath + @"/" + item;
                //Console.WriteLine(sampleDirectory);
                //if (Directory.Exists(sampleDirectory))
                //{
                //    Directory.Delete(sampleDirectory, true);  //在删除git目录时，报没有权限的错误，应该递归删除了git服务器中的东西，所以如果要删除，就值
                //}
                //如果没权限那就直接跳过？
                cmd = "git clone " + baseUrl + "/" + item;
                //Console.WriteLine(cmd);
                //call git cmd
                downProjectCMD.StandardInput.WriteLine(cmd);
                downProjectCMD.StandardInput.AutoFlush = true;
                //string outputs = downProjectCMD.StandardOutput.ReadToEnd();
                //Console.WriteLine(outputs);

                //downProjectCMD.StandardInput.WriteLine(cmd + "&exit");
                // downProjectCMD.StandardInput.WriteLine("&exit");
                //downProjectCMD.StandardInput.AutoFlush = true;
                //Console.WriteLine(item);
            }  //这里刚好可以进行异常处理，有问题就一直重复当前的即可，但是问题在于下载出现的问题仅可能是没网和空间不足
               //空间不足直接终止，断网则反复尝试，也可以隔几分钟再尝试一下，这个时间的长短可以在配置文件中进行设置，这个后面再说

            //现在的问题就在于这个控制台的下

            //exit
            downProjectCMD.StandardInput.WriteLine("exit");
            downProjectCMD.StandardInput.AutoFlush = true;
            //string output = downProjectCMD.StandardOutput.ReadToEnd();
            string output = downProjectCMD.StandardError.ReadToEnd();
            downProjectCMD.WaitForExit();
            downProjectCMD.Close();
           Console.WriteLine(output);
        }

        //public void DownloadProjects(string downSouceTxt)
        //{
        //    //Get the AppConfig settings
        //    string currentPath = Directory.GetCurrentDirectory();
        //    string projectPath = Directory.GetParent("../../../Result/Project") + @"/Project";
        //    //Switch work path command
        //    string diskDrive = currentPath.Substring(0, 2) + " & cd " + projectPath;
        //    string filePath = string.Format(currentPath + "\\" + downSouceTxt);
        //    string baseUrl = "https://github.com/ThinkGeo";
        //    //Because only a command is needed.So run a CMD command directly.  git clone url
        //    string cmd = "for /f %i in (" + filePath + ") do git clone " + baseUrl + "/" + "%i";
        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine(downSouceTxt + " does not exist！" + "Please check the configuration file!");
        //        return;
        //    }
        //    if (!Directory.Exists(projectPath))
        //    {
        //        Directory.CreateDirectory(projectPath);
        //    }
        //    string[] projectNames = File.ReadAllLines(downSouceTxt);
        //    foreach (var item in projectNames)
        //    {
        //        string sampleDirectory = projectPath + @"/" + item;
        //        if (Directory.Exists(sampleDirectory))
        //        {
        //            Directory.Delete(sampleDirectory, true);
        //        }

        //    }
        //    //call cmd
        //    Process downProjectCMD = new Process();
        //    downProjectCMD.StartInfo.FileName = "cmd.exe";
        //    downProjectCMD.StartInfo.UseShellExecute = false;
        //    downProjectCMD.StartInfo.RedirectStandardInput = true;
        //    downProjectCMD.StartInfo.RedirectStandardOutput = true;
        //    downProjectCMD.StartInfo.RedirectStandardError = true;
        //    downProjectCMD.StartInfo.CreateNoWindow = true;
        //    downProjectCMD.Start();
        //    //cd work path
        //    downProjectCMD.StandardInput.WriteLine(diskDrive);
        //    //call git cmd
        //    downProjectCMD.StandardInput.WriteLine(cmd);
        //    //exit
        //    downProjectCMD.StandardInput.WriteLine("&exit");
        //    downProjectCMD.StandardInput.AutoFlush = true;
        //    downProjectCMD.Close();
        //}
    }
}