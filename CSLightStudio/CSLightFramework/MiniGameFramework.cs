using CSLight.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSLight.Framework
{
    interface IMiniGameEnv
    {

    }
    class MiniGameFramework
    {
        public void InitMiniGameEnv(IMiniGameEnv env,ICLS_Logger logger)
        {
            ScriptMgr<IMiniGameEnv> gameenv = new ScriptMgr<IMiniGameEnv>(logger);
            gameenv.Init();

        }
    }
}
