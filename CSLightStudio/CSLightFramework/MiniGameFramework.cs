using CSLight.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight.Framework
{
    public interface IGameState
    {
        IScript script
        {
            get;
        }
        IMiniGameEnv gameEnv
        {
            get;
        }
        void AddTimer(float delay, string callbackscript, bool once = false);
    }
    public interface IMiniGameEnv //用户提供,
    {

        void AddPic(string name, double x, double y);
        void MovePic(string name, double x, double y);
        void RemvePic(string name);
        void ClearPic();
    }
    public class MiniGameState : IGameState
    {
        public virtual void _New(IScript script, IMiniGameEnv gameenv)
        {
            this.script = script;
            this.gameEnv = gameenv;
            //script.CallScriptFuncWithoutParam("_New");

        }
        public virtual void _OnInit()
        {
            script.CallScriptFuncWithoutParam("_init");
        }
        public virtual void _OnExit()
        {
            script.CallScriptFuncWithoutParam("_exit");
        }
        class Timer
        {
            public float delay;
            public string callbackScript;
            public bool once;
            public float timer = 0;
            public Timer(float delay, string callbackScript, bool once = false)
            {
                this.delay = delay;
                this.callbackScript = callbackScript;
                this.once = once;
            }
        }
        List<Timer> timers = new List<Timer>();
        public void AddTimer(float delay, string callbackscript, bool once = false)
        {
            timers.Add(new Timer(delay, callbackscript, once));
        }
        public void RemoveTimer(string callbackscript)
        {
            foreach(var t in timers)
            {
                if(t.callbackScript==callbackscript)
                {
                    timers.Remove(t);
                    return;
                }
            }
        }
        public virtual void _OnUpdate(float delta)
        {
            List<Timer> remove = new List<Timer>();
            foreach(var t in timers)
            {
                t.timer += delta;
                if(t.timer>=t.delay)
                {
                    
                    this.script.CallScriptFuncWithoutParam(t.callbackScript);
                    if(t.once)
                    {
                        remove.Add(t);
                    }
                    else
                    {
                        t.timer -= t.delay;
                    }
                }
            }
            foreach(var t in remove)
            {
                timers.Remove(t);
            }
        }

        public IScript script
        {
            get;
            private set;
        }

        public IMiniGameEnv gameEnv
        {
            get;
            private set;
        }
    }
    public class MiniGameFramework
    {
        public ScriptMgr<IGameState> scriptenv
        {
            get;
            private set;
        }

        IMiniGameEnv gameenv;
        MiniGameState gameState;
        public void InitMiniGameEnv(IMiniGameEnv env, ICLS_Logger logger)
        {
            scriptenv = new ScriptMgr<IGameState>(logger);
            gameenv = env;
            scriptenv.Init();
            scriptenv.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(IMiniGameEnv)));
            tlast = DateTime.Now;
        }
        public void ChangeState(MiniGameState state)
        {
            if (this.gameState != null)
            {
                this.gameState._OnExit();
            }

            this.gameState = state;
            if (this.gameState != null)
                this.gameState._OnInit();
        }
        public void AddState(string name, string src, MiniGameState state)
        {
            var code = new CodeFile_CLScript<IGameState>(scriptenv, name, src);
            state._New(code, gameenv);
            code.New(state);

        }
        DateTime tlast ;
        public void Update()
        {
            DateTime tnow = DateTime.Now;
            float delta =(float)((tnow-tlast).TotalSeconds);
            tlast = tnow;

            if (gameState != null) gameState._OnUpdate(delta);
        }
    }
}
