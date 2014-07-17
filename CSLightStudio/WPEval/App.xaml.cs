using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPEval.Resources;

namespace WPEval
{
    public partial class App : Application
    {
        /// <summary>
        ///提供对电话应用程序的根框架的轻松访问。
        /// </summary>
        /// <returns>电话应用程序的根框架。</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Application 对象的构造函数。
        /// </summary>
        public App()
        {
            // 未捕获的异常的全局处理程序。
            UnhandledException += Application_UnhandledException;

            // 标准 XAML 初始化
            InitializeComponent();

            // 特定于电话的初始化
            InitializePhoneApplication();

            // 语言显示初始化
            InitializeLanguage();

            // 调试时显示图形分析信息。
            if (Debugger.IsAttached)
            {
                // 显示当前帧速率计数器。
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // 显示在每个帧中重绘的应用程序区域。
                //Application.Current.Host.Settings.EnableRedrawRegions = true；

                // 启用非生产分析可视化模式，
                // 该模式显示递交给 GPU 的包含彩色重叠区的页面区域。
                //Application.Current.Host.Settings.EnableCacheVisualization = true；

                // 通过禁用以下对象阻止在调试过程中关闭屏幕
                // 应用程序的空闲检测。
                //  注意: 仅在调试模式下使用此设置。禁用用户空闲检测的应用程序在用户不使用电话时将继续运行
                // 并且消耗电池电量。
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // 应用程序启动(例如，从“开始”菜单启动)时执行的代码
        // 此代码在重新激活应用程序时不执行
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // 激活应用程序(置于前台)时执行的代码
        // 此代码在首次启动应用程序时不执行
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // 停用应用程序(发送到后台)时执行的代码
        // 此代码在应用程序关闭时不执行
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // 应用程序关闭(例如，用户点击“后退”)时执行的代码
        // 此代码在停用应用程序时不执行
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // 导航失败时执行的代码
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // 导航已失败；强行进入调试器
                Debugger.Break();
            }
        }

        // 出现未处理的异常时执行的代码
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // 出现未处理的异常；强行进入调试器
                Debugger.Break();
            }
        }

        #region 电话应用程序初始化

        // 避免双重初始化
        private bool phoneApplicationInitialized = false;

        // 请勿向此方法中添加任何其他代码
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // 创建框架但先不将它设置为 RootVisual；这允许初始
            // 屏幕保持活动状态，直到准备呈现应用程序时。
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // 处理导航故障
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // 在下一次导航中处理清除 BackStack 的重置请求，
            RootFrame.Navigated += CheckForResetNavigation;

            // 确保我们未再次初始化
            phoneApplicationInitialized = true;
        }

        // 请勿向此方法中添加任何其他代码
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // 设置根视觉效果以允许应用程序呈现
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // 删除此处理程序，因为不再需要它
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // 如果应用程序收到“重置”导航，则需要进行检查
            // 以确定是否应重置页面堆栈
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // 取消注册事件，以便不再调用该事件
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // 只为“新建”(向前)和“刷新”导航清除堆栈
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // 为了获得 UI 一致性，请清除整个页面堆栈
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // 不执行任何操作
            }
        }

        #endregion

        // 初始化应用程序在其本地化资源字符串中定义的字体和排列方向。
        //
        // 若要确保应用程序的字体与受支持的语言相符，并确保
        // 这些语言的 FlowDirection 都采用其传统方向，ResourceLanguage
        // 应该初始化每个 resx 文件中的 ResourceFlowDirection，以便将这些值与以下对象匹配
        // 文件的区域性。例如: 
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage 的值应为“es-ES”
        //    ResourceFlowDirection 的值应为“LeftToRight”
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage 的值应为“ar-SA”
        //     ResourceFlowDirection 的值应为“RightToLeft”
        //
        // 有关本地化 Windows Phone 应用程序的详细信息，请参见 http://go.microsoft.com/fwlink/?LinkId=262072。
        //
        private void InitializeLanguage()
        {
            try
            {
                // 将字体设置为与由以下对象定义的显示语言匹配
                // 每种受支持的语言的 ResourceLanguage 资源字符串。
                //
                // 如果显示出现以下情况，则回退到非特定语言的字体
                // 手机的语言不受支持。
                //
                // 如果命中编译器错误，则表示以下对象中缺少 ResourceLanguage
                // 资源文件。
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // 根据以下条件设置根框架下的所有元素的 FlowDirection
                // 每个以下对象的 ResourceFlowDirection 资源字符串上的
                // 受支持的语言。
                //
                // 如果命中编译器错误，则表示以下对象中缺少 ResourceFlowDirection
                // 资源文件。
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // 如果此处导致了异常，则最可能的原因是
                // ResourceLangauge 未正确设置为受支持的语言
                // 代码或 ResourceFlowDirection 设置为 LeftToRight 以外的值
                // 或 RightToLeft。

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}