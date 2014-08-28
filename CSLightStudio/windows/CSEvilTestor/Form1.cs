using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSEvilTestor
{

    public partial class Form1 : Form, CSLE.ICLS_Logger
    {
        public Form1()
        {
            InitializeComponent();
        }
        CSLE.CLS_Environment env = null;// 
        class Item
        {
            public string path;
            public string test;
            public override string ToString()
            {
                return path;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.Logger = this;
            //bool useNamespace = false;
            env = new CSLE.CLS_Environment(this);//如果要启用命名空间，第二个参数要打开
            TestReg.Reg(env);

            //查找所有脚本文件
            string[] dirs = System.IO.Directory.GetDirectories("script");

            foreach (var d in dirs)
            {
                try
                {
                    Item i = new Item();
                    i.path = d;
                    i.test = System.IO.File.ReadAllText(d + "/test.txt");

                    listItem.Items.Add(i);
                }
                catch (Exception err)
                {

                }
            }


        }



        public void Log(string str)
        {
            this.listDebug.Items.Add(str);
        }

        public void Log_Warn(string str)
        {
            this.listDebug.Items.Add("<W>" + str);
        }

        public void Log_Error(string str)
        {
            this.listDebug.Items.Add("<E>" + str);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var content = env.CreateContent();//执行任何代码都需要一个上下文

            var type = env.GetTypeByKeyword("ScriptClass"); //获得脚本类型
            var typeinst = type.function.New(content, null);//调用构造函数产生一个脚本实例
            var value = type.function.MemberCall(content, typeinst.value, "GetI", null);//调用成员函数
            this.Log("Run GetI= " + value.type.ToString() + "|" + value.value.ToString());
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listDebug.Items.Clear();
        }

        private void listItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            builded = false;
            this.Text = "builded=" + builded;

            Item i = listItem.SelectedItem as Item;
            if (i == null) return;
            textBox1.Text = i.test;
        }
        bool builded = false;
        public void Build(string path, bool useTry)
        {
            //if (builded) return;


            string[] allfile = System.IO.Directory.GetFiles(path, "*.cs", System.IO.SearchOption.AllDirectories);
            int succ = 0;
            Dictionary<string, IList<CSLE.Token>> project = new Dictionary<string, IList<CSLE.Token>>();
            foreach (var file in allfile)
            {
                IList<CSLE.Token> tokens = null;
                if (useTry)
                {

                    try
                    {
                        tokens = env.ParserToken(System.IO.File.ReadAllText(file));

                    }
                    catch (Exception err)
                    {
                        this.Log_Error("ErrInFile:" + file);
                        MessageBox.Show(err.ToString());
                        continue;
                    }
                }
                else
                {
                    tokens = env.ParserToken(System.IO.File.ReadAllText(file));
                }

                project[file] = tokens;
                succ++;
            }
            Log("file parse:" + succ + "/" + allfile.Length);
            if (succ == allfile.Length)
            {
                if (useTry)
                {
                    try
                    {
                        env.Project_Compiler(project, true);
                    }
                    catch (Exception err)
                    {

                        MessageBox.Show(err.ToString());
                    }
                }
                else
                {
                    env.Project_Compiler(project, true);
                }
            }
            builded = true;
            Log("build OK.");
            this.Text = "builded=" + builded;
        }

        void Run(string code, bool useTry)
        {
            if (!builded)
            {
                Log_Error("Build 失败无法运行");
            }



            CSLE.CLS_Content content = env.CreateContent();//创建一个上下文，出错以后可以用此上下文捕获信息
            if (useTry)
            {
                try
                {
                    __Run(code, content);
                }
                catch (Exception err)
                {
                    string errValue = content.DumpValue();
                    string errStack = content.DumpStack(null);
                    string errSystem = "SystemError:\n" + err.ToString();

                    MessageBox.Show(errValue + errStack + errSystem);
                }
            }
            else
            {
                __Run(code, content);
            }
        }

        private void __Run(string code, CSLE.CLS_Content content)
        {
            var tokens = env.tokenParser.Parse(code);
            var expr = env.Expr_CompilerToken(tokens);
            env.Expr_Execute(expr, content);
        }
        private void button3_Click(object sender, EventArgs e)
        {//Build Only
            env = new CSLE.CLS_Environment(this);//如果要启用命名空间，第二个参数要打开
            TestReg.Reg(env);
            Item i = listItem.SelectedItem as Item;
            if (i == null) return;
            Build(i.path, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {//Build &Run
            env = new CSLE.CLS_Environment(this);//如果要启用命名空间，第二个参数要打开
            TestReg.Reg(env);
            Item i = listItem.SelectedItem as Item;
            if (i == null) return;
            Build(i.path, true);
            Run(i.test, true);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            env = new CSLE.CLS_Environment(this);//如果要启用命名空间，第二个参数要打开
            TestReg.Reg(env);
            Item i = listItem.SelectedItem as Item;
            if (i == null) return;
            Build(i.path, false);
            Run(i.test, false);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            env = new CSLE.CLS_Environment(this);//如果要启用命名空间，第二个参数要打开
            TestReg.Reg(env);

            int succ = 0;
            for(int i=0;i<listItem.Items.Count;i++)
            {
                Item item = listItem.Items[i] as Item;
                try
                {
                    builded = false;
                    Build(item.path, false);
                    Run(item.test, false);
                    Log("Build Succ("+i+"/"+ listItem.Items.Count+")"+ item.path);
                    succ++;
                }
                catch(Exception err)
                {
                    Log("Build Fail(" + i + "/" + listItem.Items.Count + ")" + item.path);

                }
            }
            Log("Test Result:(" + succ + "/" + listItem.Items.Count + ")");
        }
    }

    public class Debug
    {
        public static CSLE.ICLS_Logger Logger;
        public static void Log(string str)
        {
            Logger.Log(str);
        }
    }
}
