using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace script
{
    [CSLE.NotScipt]
    class Program
    {
        static void Main(string[] args)
        {
            string initpath = "./";
            string outpath = "../bin/script02.CSLEDll.bytes";

            var logger = new Logger();

            ScriptEnv.Instance.Reset(logger);
            logger.Log_Warn("Begin Script Test.");

            string scriptpath = System.IO.Path.GetFullPath(initpath);//需要修改

            logger.Log("ScriptPath=" + scriptpath);
            logger.Log_Warn("Please Check if the path is correct.Press any key to continue.");

            logger.Pause();

            string[] filelist = System.IO.Directory.GetFiles(scriptpath, "*.cs", System.IO.SearchOption.AllDirectories);
            logger.Log_Warn("got code file:" + filelist.Length);
            logger.Log_Warn("BeginTokenParse");
            Dictionary<string, IList<CSLE.Token>> project = new Dictionary<string, IList<CSLE.Token>>();
            foreach (var f in filelist)
            {
                string code = System.IO.File.ReadAllText(f);
                var tokens = ScriptEnv.Instance.scriptEnv.tokenParser.Parse(code);
                var filename = System.IO.Path.GetFileName(f);
                project[filename] = tokens;
                logger.Log("TokenParse:" + filename + " len:" + code.Length + " token:" + tokens.Count);
            }
            logger.Log_Warn("TokenParser Finish.");
            logger.Log_Warn("BeginCompile");
            ScriptEnv.Instance.scriptEnv.Project_Compiler(project, true);
            logger.Log_Warn("EndCompile");


            using (System.IO.Stream s = System.IO.File.Open(outpath, System.IO.FileMode.Create))
            {
                ScriptEnv.Instance.scriptEnv.Project_PacketToStream(project, s);
            }
            logger.Log("Write script.CSLEDll.bytes in:" + outpath);


            logger.Log_Warn("Test end.Press any key to exit.");
            logger.Pause();

        }

        class Logger : CSLE.ICLS_Logger
        {
            public void Log(string str)
            {

                Console.WriteLine(str);
            }

            public void Log_Warn(string str)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(str);
                Console.ForegroundColor = c;
            }

            public void Log_Error(string str)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(str);
                Console.ForegroundColor = c;

            }
            public void Pause()
            {
                Console.ReadKey();
            }
        }
    }
}
