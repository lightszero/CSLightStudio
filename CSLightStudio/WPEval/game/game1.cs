using CSLight.Framework;
using System;
using System.Collections.Generic;
using System.Text;


public static class game1
{
    public static void _init(IGameState state)
    {
        state.gameEnv.AddPic("boxme", "box.png", 0.5f, 0.9f, 0.2f, 0.05f);
        state.gameEnv.AddPic("boxcom", "box.png", 0.5f, 0.1f, 0.2f, 0.05f);
        state.gameEnv.AddPic("ball", "ball.png", 0.5f, 0.5f, 0.05f, 0.05f);
        float timer = 0;
        state.SetTempValue("timer", timer);
        state.AddTimer(0.05f, "_update", false);
    }
    public static void _update(IGameState state, float delta)
    {
        float timer =(float)state.GetTempValue("timer");
        timer+=delta;
        if(timer>2.0f)
            timer=0;
        state.SetTempValue("timer", timer);

        float x =timer;
        if(timer>1.0f)x=2.0f-timer;
        state.gameEnv.MovePic("boxme", x, 0.9f);
        state.gameEnv.MovePic("boxcom", x, 0.1f);
        state.gameEnv.MovePic("ball", x, 0.1f+0.8f*x);

    }

    public static void _exit(IGameState state)
    {
        state.gameEnv.ClearPic();
    }

}

