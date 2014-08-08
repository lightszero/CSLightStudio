using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace CSLightDebugger_Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CSLE.Remote.IRemoteServer server = new CSLE.Remote.RemoteServer();
        public MainWindow()
        {
            InitializeComponent();
            server.StartServer(5588);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fbd.SelectedPath;

                FillProject(this.FileList.Items, path);

            }


        }
        void FillProject(ItemCollection pitem, string path)
        {
            var dirs = System.IO.Directory.GetDirectories(path);
            foreach (var d in dirs)
            {
                TreeViewItem i = new TreeViewItem();
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                Image pic = new Image();
                pic.Source = new BitmapImage(new Uri("1.jpg", UriKind.Relative));
                pic.Width = 18;
                pic.Height = 18;
                panel.Children.Add(pic);
                TextBlock tb = new TextBlock();
                tb.Text = System.IO.Path.GetFileName(d);
                panel.Children.Add(tb);
                i.Header = panel;// "<Path>" + System.IO.Path.GetFileName(d);

                FillProject(i.Items, d);
                if (i.Items.Count > 0)
                    pitem.Add(i);

            }
            var files = System.IO.Directory.GetFiles(path, "*.cs");
            foreach (var f in files)
            {
                TreeViewItem i = new TreeViewItem();
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                Image pic = new Image();
                pic.Source = new BitmapImage(new Uri("2.jpg", UriKind.Relative));
                pic.Width = 18;
                pic.Height = 18;
                panel.Children.Add(pic);
                TextBlock tb = new TextBlock();
                tb.Text = System.IO.Path.GetFileName(f);
                panel.Children.Add(tb);
                i.Header = panel;
                i.Tag = f;
                pitem.Add(i);

            }
        }

        private void FileList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem i = e.NewValue as TreeViewItem;
            if (i == null) return;
            string strtag = i.Tag as string;
            if (string.IsNullOrEmpty(strtag) == false)
            {
                string filecode = System.IO.File.ReadAllText(strtag);
                int code = filecode.GetHashCode();
                fileEditTitle.Header = System.IO.Path.GetFileName(strtag) + "_" + code;
                fileEdit.Document.Text = filecode;
            }
        }
    }
}
