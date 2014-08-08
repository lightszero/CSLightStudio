using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace CSLE.Remote
{
    public class RemoteServer : IRemoteServer
    {
        System.Net.HttpListener host = new System.Net.HttpListener();
        public void StartServer(int port)//启动服务器
        {
            host.Prefixes.Add("http://*:" + port + "/");
            host.Start();
            host.BeginGetContext(onHttpIn, null);
        }
        void onHttpIn(IAsyncResult hr)
        {
            host.BeginGetContext(onHttpIn, null);
            var content = host.EndGetContext(hr);
            try
            {
                string from = content.Request.RemoteEndPoint.ToString();
                NameValueCollection query = content.Request.QueryString;
                string c = query["c"];
                string s = query["s"];
                string m = query["m"];
                string p = query["p"];
                int sid = 0;
                if (string.IsNullOrEmpty(s) == false) sid = int.Parse(s);
                string returnstr = ParserRequest(from, c, sid, m, p);

                byte[] b = System.Text.Encoding.UTF8.GetBytes(returnstr);
                content.Response.OutputStream.BeginWrite(b, 0, b.Length, (hhr) =>
                {
                    content.Response.OutputStream.EndWrite(hhr);
                    content.Response.Close();
                }, null);
            }
            catch
            {

            }
        }
        string ParserRequest(string from, string client, int sid, string cmd, string param)
        {
            string clientID = from + client;
            if (clientList.Contains(clientID) == false)
            {
                clientList.Add(clientID);
                if (OnClientLink != null)
                    OnClientLink(clientID);
            }
            if (clientID == activeClient)
            {
                if (cmd == "LOG")
                {
                    if (OnClientLog != null)
                    {
                        OnClientLog(param);
                    }
                    string returnstr = GetCommandStr();
                    return returnstr;
                }
                else
                {
                    DebugCommand rescmd;
                    if (Enum.TryParse<DebugCommand>(cmd, true, out rescmd))
                    {
                        if (OnClientDebugCommandResponse != null)
                        {
                            OnClientDebugCommandResponse(sid, rescmd, param);
                        }
                        //移除所有小于串号的指令
                        while(cmdlist.Count>0 && cmdlist.Peek().sid<=sid)
                        {
                            cmdlist.Dequeue();
                        }
                        string returnstr = GetCommandStr();
                        return returnstr;
                    }
                    else
                    {
                        return "Unknown\n";
                    }
                }

            }
            else
            {
                return "Sleep\n";
            }

        }

        List<string> clientList = new List<string>();
        public event Action<string> OnClientLink;
        public IList<string> GetClientList()
        {
            return clientList.AsReadOnly();
        }


        public void SetActiveClient(string client)
        {
            if (activeClient != client)
            {
                activeClient = client;
                cursid = 0;
                cmdlist.Clear();
            }
        }

        public string activeClient
        {
            get;
            private set;
        }

        public event Action<string> OnClientLog;

        public int SendDebugCommand(DebugCommand cmd, string param)
        {
            cursid++;
            Cmd c = new Cmd();
            c.sid = cursid;
            c.cmd = cmd;
            c.param = param;
            return cursid;
        }
        int cursid = 0;
        class Cmd
        {
            public int sid;
            public DebugCommand cmd;
            public string param;
        }
        Queue<Cmd> cmdlist = new Queue<Cmd>();
        string GetCommandStr()
        {
            if (cmdlist.Count == 0)
                return "Empty\n";
            string cmdstr = "Cmd " + cmdlist.Count+"\n";
            foreach(var c in cmdlist)
            {
                cmdstr += c.sid + "|" + c.cmd.ToString() + "|" + c.param + "\n";
            }
            return cmdstr;
        }
        public event Action<int, DebugCommand, string> OnClientDebugCommandResponse;




        public void SendResetCommand()
        {
            cursid = 0;
            cmdlist.Clear();
            SendDebugCommand(DebugCommand.Reset, null);
        }
    }
}
