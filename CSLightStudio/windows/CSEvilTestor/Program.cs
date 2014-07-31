using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSEvilTestor
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void Trace(string txt)
        {
            Console.WriteLine(txt);
        }
    }

}
