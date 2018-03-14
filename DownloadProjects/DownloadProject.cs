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
            downProjectCMD.StandardInput.WriteLine("echo off");
            downProjectCMD.StandardInput.AutoFlush = true;
            string[] projectNames = File.ReadAllLines(downSouceTxt);
            foreach (var item in projectNames)
            {
                downProjectCMD.StandardInput.WriteLine(diskDrive);
                string sampleDirectory = projectPath + @"/" + item;
                //if the directory already exists,delete the directory to download new sample
                if (Directory.Exists(sampleDirectory))
                {
                    try
                    {
                        Directory.Delete(sampleDirectory, true);
                    }
                    catch (UnauthorizedAccessException)   //If there is an exception that does not have permissions, call the  method to grant permissions to files and folders and delete them
                    {
                        AssignPermissionsAndDelete(sampleDirectory);
                        goto Download;  //delete the directory,continue executing
                    }
                }
                Download:
                cmd = "git clone " + baseUrl + "/" + item;
                //call git cmd
                downProjectCMD.StandardInput.WriteLine(cmd);
                downProjectCMD.StandardInput.AutoFlush = true;
            }

            downProjectCMD.StandardInput.WriteLine("exit");
            downProjectCMD.StandardInput.AutoFlush = true;
            string output = downProjectCMD.StandardError.ReadToEnd();
            downProjectCMD.WaitForExit();
            downProjectCMD.Close();
           Console.WriteLine(output);
        }

        static void AssignPermissionsAndDelete(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //deal the files in the directory
            foreach (FileInfo fChild in directoryInfo.GetFiles("*"))
            {
                if (fChild.Attributes != FileAttributes.Normal)
                    fChild.Attributes = FileAttributes.Normal; 
                fChild.Delete();
            }

            //deal the child directories in the directory
            foreach (DirectoryInfo dChild in directoryInfo.GetDirectories("*"))
            {
                if (dChild.Attributes != FileAttributes.Normal)
                    dChild.Attributes = FileAttributes.Normal;
                AssignPermissionsAndDelete(dChild.FullName);
                dChild.Delete(true);
            }
        }

    }
}