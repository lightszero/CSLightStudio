using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSLightFrameworkDemo
{

    public partial class Form1 : Form, MyType,CSLight.ICLS_Logger
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scriptmgr = new CSLight.Framework.ScriptMgr<MyType>(this);
            scriptmgr.Init();
            scriptmgr.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(MyType)));

            scriptmgr.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(A.B), "A.B"));
        }

        public static CSLight.Framework.ScriptMgr<MyType> scriptmgr = null;




        private void timer1_Tick(object sender, EventArgs e)
        {
            if (script != null)
            {
                script.CallScriptFuncWithParamString("_update", DateTime.Now.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CodeFile_Debug 和类型 testcode01 绑定，将其作为代码使用
            scriptmgr.SetCodeFile("testcode01", new CSLight.Framework.CodeFile_Debug<MyType>(scriptmgr, "testcode01", typeof(testcode01)));

            var sscript = scriptmgr.GetCodeFile("testcode01");
            this.script = sscript;
            sscript.New(this);
        }
        public CSLight.Framework.IScript script
        {
            get;
            private set;
        }
        public void AddTextToList(string str)
        {
            this.listBox1.Items.Add(str);
        }

        public void SetColorRed()
        {
            this.pictureBox1.BackColor = Color.Red;
        }

        public void SetColorBlue()
        {
            this.pictureBox1.BackColor = Color.Blue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string code = System.IO.File.ReadAllText("script/testcode01.cs");
            scriptmgr.SetCodeFile("testcode01", new CSLight.Framework.CodeFile_CLScript<MyType>(scriptmgr, "testcode01", code));

            var sscript = scriptmgr.GetCodeFile("testcode01");
            this.script = sscript;
            sscript.New(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (script != null)
            {
                script.CallScriptFuncWithoutParam("_click01");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (script != null)
            {
                script.CallScriptFuncWithParamString("_click02", "000");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (script != null)
            {
                List<string> ss = new List<string>();
                ss.Add("123");
                ss.Add("567");
                script.CallScriptWithParamStrings("_click03", ss);
            }
        }

        public void Log(string str)
        {
            Console.WriteLine(str);
        }

        public void Log_Warn(string str)
        {
            Console.WriteLine("<W>"+str);
        }

        public void Log_Error(string str)
        {
            Console.WriteLine("<E>"+str);
        }
    }

    public interface MyType
    {
        void AddTextToList(string str);
        void SetColorRed();
        void SetColorBlue();

        CSLight.Framework.IScript script
        {
            get;
        }
    }
    public class A
    {
        public class B
        {

        }
    }
}
