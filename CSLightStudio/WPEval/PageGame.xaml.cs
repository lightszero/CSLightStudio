using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Resources;
using System.Windows.Threading;
using CSLight.Framework;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPEval
{
    public partial class PageGame : PhoneApplicationPage, IMiniGameEnv, CSLight.ICLS_Logger
    {
        DispatcherTimer timer = null;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            timer = new DispatcherTimer();
            timer.Tick += (ss, ee) =>
                {
                    Update();
                };
            timer.Start();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            timer.Stop();
            timer = null;
        }
        public PageGame()
        {
            InitializeComponent();
        }

        void Update()
        {
            if(bRun)
            {
                framework.Update();
            }
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("game/game1.cs", UriKind.Relative);//这个就是所以的pack uri。
            StreamResourceInfo info = Application.GetResourceStream(uri);
            byte[] b = new byte[info.Stream.Length];
            info.Stream.Read(b, 0, b.Length);
            string code = System.Text.Encoding.UTF8.GetString(b, 0, b.Length);
            this.textCode.Text = code;

            framework = new MiniGameFramework();
            framework.InitMiniGameEnv(this, this);

        }
        bool bRun = false;
        MiniGameFramework framework = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!bRun)
            {//Run
                var state = new MiniGameState();
                framework.AddState("game1", this.textCode.Text, state);
                framework.ChangeState(state);
                bRun = true;

            }
            else
            {
                this.ClearPic();
                bRun = false;

            }

        }
        #region GameEnv
        Dictionary<string, Image> imgs = new Dictionary<string, Image>();
        public void AddPic(string name, string picfile, double x, double y, double width, double height)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("game/" + picfile, UriKind.Relative));
            img.Width = width * canvas.RenderSize.Width;
            img.Height = height * canvas.RenderSize.Height;
            img.Stretch = Stretch.Fill;
            canvas.Children.Add(img);
            Canvas.SetLeft(img, canvas.RenderSize.Width * x - img.Width * 0.5f);
            Canvas.SetTop(img, canvas.RenderSize.Height * y - img.Height * 0.5f);
            imgs[name] = img;
        }

        public void MovePic(string name, double x, double y)
        {
            if (imgs.ContainsKey(name))
            {
                var img = imgs[name];
                Canvas.SetLeft(img, canvas.RenderSize.Width * x - img.Width * 0.5f);
                Canvas.SetTop(img, canvas.RenderSize.Height * y - img.Height * 0.5f);

            }
        }

        public void RemvePic(string name)
        {
            if (imgs.ContainsKey(name))
            {
                canvas.Children.Remove(imgs[name]);
                imgs.Remove(name);
            }
        }

        public void ClearPic()
        {
            canvas.Children.Clear();
            imgs.Clear();

        }
        #endregion

        public void Log(string str)
        {
            //throw new NotImplementedException();
        }

        public void Log_Warn(string str)
        {
            //throw new NotImplementedException();
        }

        public void Log_Error(string str)
        {
            //throw new NotImplementedException();
        }
    }
}