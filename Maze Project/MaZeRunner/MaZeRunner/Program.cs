using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace MaZeRunner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string configDir = Path.Combine(Path.GetPathRoot(Environment.CurrentDirectory), "DONT_DETELTE");
            string config = decryptConfig(File.ReadAllText(configDir));
            string[] toRun = config.Split(';');
            foreach (string x in toRun)
            {
                System.Diagnostics.Process.Start(x);
            }
        }
        public static string decryptConfig(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (char)((int)charArray[i] - 12);
            }
            return new string(charArray);
        }

    }
}
