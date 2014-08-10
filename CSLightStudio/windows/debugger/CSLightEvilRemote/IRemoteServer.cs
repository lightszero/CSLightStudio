using System;
using System.Collections.Generic;
namespace CSLE.Remote
{
    public interface IRemoteServer
    {
        void StartServer(int port);//启动服务器

        IList<string> GetClientList();//得到客户端列表

        event Action<string> OnClientLink;//当有客户端接入时的回调

        void SetActiveClient(string client);//激活客户端
        string activeClient//取得激活客户端
        {
            get;
        }

        event Action<string> OnClientLog;//当客户端输入日志的回调

        /// <summary>
        /// 执行调试命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        /// <returns>每一条调试命令有个流水号,根据流水号判断客户端响应情况</returns>
        int SendDebugCommand(DebugCommand cmd,string param);
        
        /// <summary>
        /// 重置命令，会删除服务器的命令队列，流水号归零。发送信息给客户端，请求客户端同样操作
        /// 当客户端服务器流水号同步异常时使用，比如中间网络不畅断开了
        /// </summary>
        void SendResetCommand();
        /// <summary>
        /// //当客户端响应调试命令的回调
        /// 三个参数分别是流水号,客户端收到的命令,客户端返回字符串
        /// </summary>
        event Action<int,DebugCommand,string> OnClientDebugCommandResponse;//当客户端响应调试命令的回调


    }
    public enum DebugCommand
    {
        Reset,

        Play,           //继续运行，相当于VS F5
        PauseNow,       //中断当前程序，相当于VS ctrl+alt+break
        
        PauseNext,      //逐语句调试,相当于VS F10
        PauseInStack,   //跳入，相当于VS F11
        PauseOutStack,  //跳出，相当于VS shift+F11

        BreakPoint_Clear,      //清除所有断点,无参数
        BreakPoint_Add,        //增加断点，有参数
        BreakPoint_Remove,     //删除断点，有参数

        Dump_Stack,//让客户端汇报堆栈
        Dump_Value,//让客户端汇报变量
        Dump_Content,//让客户端汇报上下文信息
    };
}
