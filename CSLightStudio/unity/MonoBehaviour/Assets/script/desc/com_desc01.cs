using UnityEngine;
using System.Collections;

public class com_desc01 : MonoBehaviour
{


    string desc =   "这里使用了Com_CallDynScript组件\n"+
                    "这个组件的功能是直接在参数里提供Start和Update两个字串，作为脚本执行。\n"+
                    "另外可以设置脚本Update的更新时间，这个演示设为3，就是每秒钟执行3次的意思。\n";

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 300, 200), desc);
    }
}
