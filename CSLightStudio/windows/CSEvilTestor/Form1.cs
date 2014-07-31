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
        private void Form1_Load(object sender, EventArgs e)
        {

            env = new CSLE.CLS_Environment(this);
            env.RegType(new CSLE.RegHelper_Type(typeof(TestDele)));
            env.RegType(new CSLE.RegHelper_Type(typeof(Program)));
            env.RegType(new CSLE.RegHelper_DeleAction("Action"));
            //查找所有脚本文件
            string[] files = System.IO.Directory.GetFiles("script", "*.cs");
            //处理文件，组织成项目文件
            Dictionary<string, IList<CSLE.Token>> scriptProject = new Dictionary<string, IList<CSLE.Token>>();
            foreach (var f in files)
            {
                string code = System.IO.File.ReadAllText(f);
                var tokens = env.ParserToken(code);
                scriptProject[f] = tokens;
            }
            //编译脚本项目
            env.Project_Compiler(scriptProject);

        }



        public void Log(string str)
        {
            this.listBox1.Items.Add(str);
        }

        public void Log_Warn(string str)
        {
            this.listBox1.Items.Add("<W>" + str);
        }

        public void Log_Error(string str)
        {
            this.listBox1.Items.Add("<E>" + str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text;
            var tokens = env.tokenParser.Parse(code);
            var expr = env.Expr_CompilerToken(tokens);
            env.Expr_Execute(expr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var content = env.CreateContent();//执行任何代码都需要一个上下文

            var type = env.GetTypeByKeyword("ScriptClass"); //获得脚本类型
            var typeinst = type.function.New(content, null);//调用构造函数产生一个脚本实例
            var value = type.function.MemberCall(content, typeinst.value, "GetI", null);//调用成员函数
            this.Log("Run GetI= " + value.type.ToString() + "|" + value.value.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                TestDele.instance.Run();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
