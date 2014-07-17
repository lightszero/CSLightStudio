using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPEval.Resources;

namespace WPEval
{
    public partial class MainPage : PhoneApplicationPage,CSLight.ICLS_Logger
    {
        CSLight.CLS_Environment envScript = null;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            envScript = new CSLight.CLS_Environment(this);
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }
        public List<string> loginfo = new List<string>();
        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            txt_ExprInput.Text = "\"HelloWorld\"+(2*5+2*2+20*100);";
        }


        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        public void Log(string str)
        {
            loginfo.Add(str);
            //throw new NotImplementedException();
        }

        public void Log_Warn(string str)
        {
            loginfo.Add("<W>"+str);

            //throw new NotImplementedException();
        }

        public void Log_Error(string str)
        {
            loginfo.Add("<E>" + str);

            //throw new NotImplementedException();
        }
        #region exprevent
        private void Button_Click(object sender, RoutedEventArgs e)
        {//EXPR_RUN
            CSLight.CLS_Content.Value value = null;
            try
            {
                var tlist = envScript.ParserToken(txt_ExprInput.Text);
                var expr = envScript.CompilerToken(tlist, true);
                value = envScript.Execute(expr);
            }
            catch (Exception err)
            {
                this.Log_Error(err.ToString());
            }
            string sout = "";
            foreach (var l in loginfo)
            {
                sout += l + "\n";
            }
            sout += value;

            txt_ExprOut.Text = sout;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region aboutevent
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(btn_aboutlink.Content.ToString()));
        }
        #endregion
    }
}