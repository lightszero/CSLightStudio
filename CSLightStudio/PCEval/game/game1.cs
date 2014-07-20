using CSLight.Framework;
using System;
using System.Collections.Generic;
using System.Text;


public static class game1
{
    public static void _new(IGameState state)
    {
        state.gameEnv.AddPic("box", 0.5f, 0.5f);
    }
    public static void _init(IGameState state)
    {

    }
    public static void _exit(IGameState state)
    {

    }

}

