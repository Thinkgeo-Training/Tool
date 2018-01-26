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
            string cmd = "for /f %i in (" + filePath + ") do git clone " + baseUrl + "/" + "%i";
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
            downProjectCMD.StandardInput.WriteLine(diskDrive);
            //call git cmd
            downProjectCMD.StandardInput.WriteLine(cmd);
            //exit
            downProjectCMD.StandardInput.WriteLine("&exit");
            downProjectCMD.StandardInput.AutoFlush = true;
            downProjectCMD.Close();
        }
    }
}