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
using System.Windows.Media;

namespace WPEval
{
    public partial class MainPage : PhoneApplicationPage, CSLight.ICLS_Logger
    {
        CSLight.CLS_Environment envScript = null;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            envScript = new CSLight.CLS_Environment(this);
            envScript.RegType(new CSLight.RegHelper_Type(typeof(Math)));
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }
        public List<string> loginfo = new List<string>();
        bool bTrial = false;
        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Click_1(null, null);
            Button_Click_5(null, null);
            adv.Start();
            UpdateTrial();
            Windows.ApplicationModel.Store.CurrentApp.LicenseInformation.LicenseChanged += () =>
                {

                    UpdateTrial();
                };

        }
        void UpdateTrial()
        {
            bTrial = Windows.ApplicationModel.Store.CurrentApp.LicenseInformation.IsTrial;
            if (!bTrial)
            {
                advpos.Height = new GridLength(0);
                btn_aboutbuy.Visibility = System.Windows.Visibility.Collapsed;
            }
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
            loginfo.Add("<W>" + str);

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
            loginfo.Clear();
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
        int exprIndex = 0;
        string[] exprCode = { "\"HelloWorld\"+(2*5+2*2+20*100);" ,
                            "1>2",
                            "1>2?3:5",
                            "1>2&&3>=2"
                            };
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//EXPR_CHANGE
            exprIndex++;
            if (exprIndex >= exprCode.Length) exprIndex = 0;
            txt_ExprInput.Text = exprCode[exprIndex];
        }
        #endregion
        #region blockevent
        bool bBlockInit = false;
        void Draw(int x,int y)
        {
            TextBlock img = new TextBlock();
            img.Text = "R";
            img.Width = 16;
            img.Height = 16;
            img.Foreground=new SolidColorBrush(Color.FromArgb(255,0,0,0));

            canvas_Block.Children.Add(img);
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, x);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {//BLOCK_RUN
            if(!bBlockInit)
            {
                Action<int, int> draw = Draw;
                envScript.RegFunction(new CSLight.RegHelper_Function(draw,"Draw"));
                bBlockInit = true;
            }
            CSLight.CLS_Content.Value value = null;
            loginfo.Clear();
            try
            {
                var tlist = envScript.ParserToken(txt_BlockInput.Text);
                var expr = envScript.CompilerToken(tlist, true);
                value = envScript.Execute(expr);
            }
            catch (Exception err)
            {
                string sout = "";
                foreach (var l in loginfo)
                {
                    sout += l + "\n";
                }
                sout += value;
                sout += err.ToString();
                MessageBox.Show(sout);
            }
        }
        int blockIndex = 0;
        string[] blockCode = { "for(int i=0;i<10;i++)\nDraw(i*10,i*10);" ,
                            "1>2",
                            "1>2?3:5",
                            "1>2&&3>=2"
                            };
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {//BLOCK_CHANGE
            blockIndex++;
            if (blockIndex >= blockCode.Length) blockIndex = 0;
            txt_BlockInput.Text = blockCode[blockIndex];
        }
        #endregion
        #region aboutevent
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(btn_aboutlink.Content.ToString()));
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {//About Buy
            Windows.ApplicationModel.Store.CurrentApp.RequestAppPurchaseAsync(false);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("zune:reviewapp?appid=" + Windows.ApplicationModel.Store.CurrentApp.AppId));

        }
        #endregion








    }
}