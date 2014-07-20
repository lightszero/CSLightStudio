using CSLight.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace PCEval
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IMiniGameEnv, CSLight.ICLS_Logger
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MiniGameFramework framework = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            framework = new MiniGameFramework();
            framework.InitMiniGameEnv(this, this);
            var state = new MiniGameState();

            Uri uri = new Uri("/game/game1.cs", UriKind.Relative);//这个就是所以的pack uri。
            StreamResourceInfo info = Application.GetResourceStream(uri);
            byte[] b = new byte[info.Stream.Length];
            info.Stream.Read(b, 0, b.Length);
            string code = System.Text.Encoding.UTF8.GetString(b);
            framework.AddState("game1", code, state);
            framework.ChangeState(state);

            
            
        }

        #region MINIGAMEENV
        public void AddPic(string name, double x, double y)
        {
        }

        public void MovePic(string name, double x, double y)
        {
        }

        public void RemvePic(string name)
        {
        }

        public void ClearPic()
        {
        }
        #endregion
        #region Logger
        public void Log(string str)
        {
            this.listbox.Items.Add("" + str);
        }

        public void Log_Warn(string str)
        {
            this.listbox.Items.Add("<W>" + str);

        }

        public void Log_Error(string str)
        {
            this.listbox.Items.Add("<E>" + str);

        }
        #endregion
    }
}
