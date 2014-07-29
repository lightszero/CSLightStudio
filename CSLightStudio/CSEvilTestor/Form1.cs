using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSEvilTestor
{
    public partial class Form1 : Form, CSEvil.ICLS_Logger
    {
        public Form1()
        {
            InitializeComponent();
        }
        CSEvil.CLS_Environment env = null;// 
        private void Form1_Load(object sender, EventArgs e)
        {

            env = new CSEvil.CLS_Environment(this);
            //查找所有脚本文件
            string[] files = System.IO.Directory.GetFiles("script", "*.cs");
            //处理文件，组织成项目文件
            Dictionary<string, IList<CSEvil.Token>> scriptProject = new Dictionary<string, IList<CSEvil.Token>>();
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
            var tokens= env.tokenParser.Parse(code);
            var expr = env.Expr_CompilerToken(tokens);
            env.Expr_Execute(expr);
        }
    }
}
