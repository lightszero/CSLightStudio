using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CLScriptTestor
{
    public partial class Form1 : Form, CSLE.ICLS_Logger
    {

        public Form1()
        {
            InitializeComponent();
        }
        CSLE.CLS_Environment scriptService;
        class UserData
        {
            public static UserData g_this = new UserData();
            public static UserData Instance()
            {
                return g_this;
            }
            public Dictionary<string, string> HeroDataMap = new Dictionary<string, string>();
        }
        private void Form1_Load(object sender, EventArgs e)
        {


            scriptService = new CSLE.CLS_Environment(this); //初始化CLScript
            scriptService.RegFunction(new CSLE.RegHelper_Function((_call)testCallAdd));
            scriptService.RegFunction(new CSLE.RegHelper_Function((_call)testCallDec));
            scriptService.RegFunction(new CSLE.RegHelper_Function((_call2)testCallAdd4));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(UserData)));

            scriptService.RegType(new CSLE.RegHelper_Type(typeof(CLScriptExt.Country)));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(CLScriptExt.Vector3)));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(CLScriptExt.Student)));

            Type t =Type.GetType("System.Collections.Generic.List`1");
            scriptService.RegType(new CSLE.RegHelper_Type(t, "List"));

           
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(List<CLScriptExt.Student>), "List<Student>"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(List<CLScriptExt.Vector3>), "List<Vector3>"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(List<int>), "List<int>"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(List<List<int>>), "List<List<int>>"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(int[]), "int[]"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(string[]), "string[]"));

            scriptService.RegType(new CSLE.RegHelper_Type(typeof(Dictionary<string, string>), "Dictionary<string,string>"));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(CLScriptExt.Student.S1), "Student.S1"));

            scriptService.RegType(new CSLE.RegHelper_Type(typeof(DateTime)));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(TimeSpan)));
            scriptService.RegType(new CSLE.RegHelper_Type(typeof(Form1)));


            scriptService.RegType(new CSLE.RegHelper_DeleAction<int>(typeof(Action<int>), "Action<int>"));
            //CLScriptExt.Type_Vector3());
            InitCodeFile();
            ScriptNET.Runtime.RuntimeHost.Initialize();

        }
        public static event Action<int> onact;
        public static int Cell(double v)
        {
            return (int)v;
        }
        delegate int _call(int a, int b);
        public static void Try()
        {
            if (onact != null) onact(33);
        }
        static int testCallAdd(int a, int b)
        {
            Console.WriteLine("a=" + a + " b=" + b);
            return a + b;
        }
        static int testCallDec(int a, int b)
        {
            return a - b;
        }
        delegate int _call2(int a, int b, int c = 0, int d = 0);
        static int testCallAdd4(int a, int b, int c = 0, int d = 0)
        {
            return a + b + c + d;
        }

        void InitCodeFile()
        {
            string[] files = System.IO.Directory.GetFiles("code", "*.cls.txt", System.IO.SearchOption.AllDirectories);
            this.listCodeFile.Items.AddRange(files);
        }

        string curCodeFile;
        private void listCodeFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            compilerResult = null;
            curCodeFile = this.listCodeFile.SelectedItem as string;
            if (curCodeFile != null)
            {
                this.richTextBox_Code.Text = System.IO.File.ReadAllText(curCodeFile);
                ReFormat();
            }
        }

        private void button_saveCode_Click(object sender, EventArgs e)
        {
            if (curCodeFile != null)
            {
                System.IO.File.WriteAllText(curCodeFile, this.richTextBox_Code.Text);
                ReFormat();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<CSLE.Token> tokens = this.scriptService.ParserToken(richTextBox_Code.Text);
            using (System.IO.Stream fiels = System.IO.File.OpenWrite(curCodeFile + ".bytes"))
            {
                this.scriptService.tokenParser.SaveTokenList(tokens, fiels);
            }

            ParseTokenFromat(tokens, this.richTextBox_Code);

            using (System.IO.Stream fiels = System.IO.File.OpenRead(curCodeFile + ".bytes"))
            {
                tokens = this.scriptService.tokenParser.ReadTokenList(fiels);
            }

        }
        int ParseTokenFromat(IList<CSLE.Token> tlist, RichTextBox rbox)
        {
            int nErrorCount = 0;
            foreach (CSLE.Token m in tlist)
            {
                rbox.Select(m.pos, m.text.Length);
                switch (m.type)
                {
                    case CSLE.TokenType.UNKNOWN:
                        rbox.SelectionColor = Color.Red;
                        nErrorCount++;
                        break;
                    case CSLE.TokenType.KEYWORD:
                        rbox.SelectionColor = Color.Blue;
                        break;
                    case CSLE.TokenType.COMMENT:
                        rbox.SelectionColor = Color.Green;
                        break;
                    case CSLE.TokenType.IDENTIFIER:
                        rbox.SelectionColor = Color.Gray;
                        break;
                    case CSLE.TokenType.TYPE:
                        rbox.SelectionColor = Color.DarkGoldenrod;
                        break;
                    case CSLE.TokenType.STRING:
                        rbox.SelectionColor = Color.DarkGreen;
                        break;
                    case CSLE.TokenType.PUNCTUATION:
                        rbox.SelectionColor = Color.Black;
                        break;
                    case CSLE.TokenType.VALUE:
                        rbox.SelectionColor = Color.DarkBlue;
                        break;
                }
                //this.listBox1.Items.Add(m);
            }
            return nErrorCount;
        }


        void ReFormat()
        {
            IList<CSLE.Token> tokens = null;
            try
            {
                tokens = this.scriptService.ParserToken(richTextBox_Code.Text);
            }
            catch (Exception err)
            {
                Log_Error(err.ToString());
                return;
            }
            int now = this.richTextBox_Code.SelectionStart;
            ParseTokenFromat(tokens, this.richTextBox_Code);
            this.richTextBox_Code.SelectionStart = now;
            this.richTextBox_Code.SelectionLength = 0;
        }
        private void richTextBox_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReFormat();
            }
        }

        public void Log(string str)
        {
            SafeLog(str, "I");
        }

        public void Log_Warn(string str)
        {
            SafeLog(str, "W");
        }

        public void Log_Error(string str)
        {
            SafeLog(str, "E");
        }
        delegate void Action();
        void SafeLog(string str, string type)
        {
            string outl = "<" + type + ">" + str;
            Action a = () =>
                {
                    this.listLog.Items.Add(outl);
                };
            this.Invoke(a);
        }

        IList<CSLE.Token> tokensResult = null;
        CSLE.ICLS_Expression compilerResult = null;
        private void button2_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();

            tokensResult = null;
            try
            {
                tokensResult = this.scriptService.ParserToken(richTextBox_Code.Text);
                using (System.IO.Stream fiels = System.IO.File.OpenWrite(curCodeFile + ".bytes"))
                {
                    this.scriptService.tokenParser.SaveTokenList(tokensResult, fiels);
                }

            }
            catch (Exception err)
            {
                Log_Error("词法识别失败");
            }
            using (System.IO.Stream fiels = System.IO.File.OpenRead(curCodeFile + ".bytes"))
            {
                tokensResult = this.scriptService.tokenParser.ReadTokenList(fiels);
            }
            if (tokensResult != null && tokensResult.Count > 0)
            {
                compilerResult = scriptService.Expr_CompilerToken(tokensResult);


                //if (compilerResult == null)
                //{
                //    Log("尝试作为表达式编译");
                //    compilerResult = scriptService.CompilerToken(tokens, true);
                //}
                //compilerResult = compiler.Optimize(compilerResult);
                ShowExp(compilerResult);
            }
        }
        void ShowExp(CSLE.ICLS_Expression value)
        {
            treeViewExp.Nodes.Clear();
            if (value == null) return;
            TreeNode node = new TreeNode();
            ShowExpNode(node, value);
            treeViewExp.Nodes.Add(node);

            treeViewExp.ExpandAll();
        }
        void ShowExpNode(TreeNode node, CSLE.ICLS_Expression value)
        {
            if (value == null)
            {
                node.Text = "null";

            }
            else
            {
                node.Text = value.ToString();
                CSLE.ICLS_Expression exp = value as CSLE.ICLS_Expression;
                if (exp != null && exp.listParam != null)
                    foreach (var v in exp.listParam)
                    {
                        TreeNode subnode = new TreeNode();
                        ShowExpNode(subnode, v);
                        node.Nodes.Add(subnode);
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();
            if (compilerResult == null)
            {
                button2_Click(sender, e);
            }

            if (compilerResult != null)
            {
                CSLE.ICLS_Expression exp = compilerResult;
                CSLE.CLS_Content.Value returnvalue = new CSLE.CLS_Content.Value();
                CSLE.CLS_Content content = this.scriptService.CreateContent();
                try
                {

                    returnvalue = exp.ComputeValue(content);


                }
                catch (Exception err)
                {
                    string contentValue = content.DumpValue();
                    string contentStack = content.DumpStack(null);
                    string systemError = "SystemError:\n"+err.ToString();

                    MessageBox.Show(contentValue + "\n" + contentStack +"\n"+ systemError);
                    Log_Error("执行错误" + err.ToString() + ":" + contentStack+"\n"+contentValue);
                }
                if (returnvalue == null)
                {
                    Log("result=<none>");
                }
                else if (null!=returnvalue.type)
                {
                    Log("result=<" + returnvalue.type.Name + ">" + returnvalue.value);
                }
                else
                {
                    Log("result=<unknown>" + returnvalue.value);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();
        }

        private void listLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int count = 1000;
            Profile(count);
            //double num=lua.ToNumber(-1);
        }

        private void Profile(int count)
        {

            string code = richTextBox_Code.Text;

            try
            {
                string codelite = "return " + code;
                var tokens = this.scriptService.ParserToken(code);
                compilerResult = this.scriptService.Expr_CompilerToken(tokens, true);
                compilerResult = this.scriptService.Expr_Optimize(compilerResult);
                if (compilerResult != null)
                {
                    CSLE.CLS_Content.Value result = null;
                    DateTime t = DateTime.Now;
                    for (int i = 0; i < count; i++)
                    {
                        CSLE.CLS_Content content = this.scriptService.CreateContent();
                        //compilerResult = compiler.Compiler(code);
                        CSLE.ICLS_Expression exp = compilerResult;

                        result = exp.ComputeValue(content);

                    }
                    DateTime t2 = DateTime.Now;
                    Log("C#Lite count=" + count + "time:" + (t2 - t).TotalSeconds);
                    if (result.type != null)
                    {
                        Log("result=<" + result.type.Name + ">" + result.value.ToString());
                    }
                    else
                    {
                        Log("result=null");
                    }
                }

            }
            catch (Exception err)
            {
                Log("C#Lite error");
            }
            string codelua = "function test() \n return " + code + "\nend";
            try
            {
                int result = 0;
                var lua = UniLua.LuaAPI.NewState();
                var state = lua.L_DoString(codelua);
                int itop = lua.GetTop();
                DateTime t = DateTime.Now;
                for (int i = 0; i < count; i++)
                {
                    //lua.SetTop(itop);
                    lua.GetGlobal("test"); // 加载 lua 中定义的一个名叫 foo 的全局函数到堆栈
                    lua.Call(0, 1); // 调用函数 foo, 指明有2个参数，没有返回值

                    //lua.SetTop(0);
                    //lua.PCall(0, -1, 0);
                    result = lua.ToInteger(-1);
                    lua.Pop(1);
                    //lua.p(1);
                }
                DateTime t2 = DateTime.Now;
                Log("unilua count=" + count + " time=" + (t2 - t).TotalSeconds);
                Log("result=<int>" + result);
            }
            catch (Exception err)
            {
                Log("unilua error.");
            }
            try
            {
                object ssresult = null;
                ScriptNET.Script ssc = ScriptNET.Script.CompileExpression(code);
                DateTime t = DateTime.Now;
                for (int i = 0; i < count; i++)
                {
                    ssc.Context = new ScriptNET.Runtime.ScriptContext();
                    ssresult = ssc.Execute();
                }
                DateTime t2 = DateTime.Now;
                Log("ssharp count=" + count + " time=" + (t2 - t).TotalSeconds);
                Log("result=<int>" + ssresult);
            }
            catch (Exception err)
            {
                Log("ssharp error.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (compilerResult == null) return;
            compilerResult = this.scriptService.Expr_Optimize(compilerResult);
            ShowExp(compilerResult);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Profile(100 * 1000);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Profile(1000 * 1000);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();
            if (compilerResult == null)
            {
                button2_Click(sender, e);
            }

            if (compilerResult != null)
            {
                CSLE.ICLS_Expression exp = compilerResult;
                CSLE.CLS_Content.Value returnvalue = new CSLE.CLS_Content.Value();
                //try
                {
                    CSLE.CLS_Content content = this.scriptService.CreateContent();

                    returnvalue = exp.ComputeValue(content);

                }
                //catch (Exception err)
                //{
                //    Log_Error("执行错误" + err.ToString());
                //}
                if (returnvalue.type != null)
                {
                    Log("result=<" + returnvalue.type.Name + ">" + returnvalue.value);
                }
                else
                {
                    Log("result=<unknown>" + returnvalue.value);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();
            if (compilerResult == null)
            {
                button2_Click(sender, e);
            }

            if (compilerResult != null)
            {
                DateTime t1 = DateTime.Now;
                CSLE.CLS_Content.Value returnvalue = new CSLE.CLS_Content.Value();
                int count = 1000;
                for (int i = 0; i < count; i++)
                {
                    CSLE.ICLS_Expression exp = compilerResult;

                    try
                    {
                        CSLE.CLS_Content content = this.scriptService.CreateContent();

                        returnvalue = exp.ComputeValue(content);

                    }
                    catch (Exception err)
                    {
                        Log_Error("执行错误" + err.ToString());
                        return;
                    }
                }
                DateTime t2 = DateTime.Now;
                Log("C#Lite count=" + count + "time:" + (t2 - t1).TotalSeconds);
                if (returnvalue.type != null)
                {
                    Log("result=<" + returnvalue.type.Name + ">" + returnvalue.value);
                }
                else
                {
                    Log("result=<unknown>" + returnvalue.value);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //scriptService.ClearCodeCache();
            int rightcount = 0;
            foreach (string file in listCodeFile.Items)
            {
                string code = System.IO.File.ReadAllText(file);
                //code=code.Replace("\r", "");
                CSLE.CLS_Content.Value v = null;
                var token = scriptService.tokenParser.Parse(code);
                try
                {

                    var vexpr = this.scriptService.Expr_CompilerToken(token, false);
                    v = this.scriptService.Expr_Execute(vexpr);
                }
                catch
                {
                    try
                    {
                        var vexpr = this.scriptService.Expr_CompilerToken(token, true);
                        v = this.scriptService.Expr_Execute(vexpr);

                    }
                    catch
                    {
                        v = null;
                    }
                }
                if (v != null)
                {
                    Log("result=<" + v.type.Name + ">" + v.value);
                    rightcount++;
                }
                else
                {
                    Log_Error(file);
                }

            }
            Log("succ=(" + rightcount + "/" + listCodeFile.Items.Count + ")");
        }

        private void richTextBox_Code_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
