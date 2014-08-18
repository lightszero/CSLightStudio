using System;
using System.Collections.Generic;

using System.Text;
using UnityEngine;


class Logger : CSLE.ICLS_Logger
{
    public void Log(string str)
    {
        Debug.Log(str);
    }

    public void Log_Error(string str)
    {
        Debug.LogError(str);
    }

    public void Log_Warn(string str)
    {
        Debug.LogWarning(str);
    }
}
public class MYDebug:CSLE.RegHelper_Type
{
    public MYDebug():base(typeof(Debug),"Debug")
{
    function = new MyFunc();
}
    public class MyFunc:CSLE.RegHelper_TypeFunction
    {
        public MyFunc():base(typeof(Debug))
        {

        }
        public override CSLE.CLS_Content.Value StaticCall(CSLE.CLS_Content environment, string function, IList<CSLE.CLS_Content.Value> _params)
        {
            if(function=="Log")
            {
                Debug.Log(_params[0].value as string);
                return CSLE.CLS_Content.Value.Void;
            }
            return base.StaticCall(environment, function, _params);
        }
    }
}
public class MYGameObject : CSLE.RegHelper_Type
{
    public MYGameObject()
        : base(typeof(GameObject), "GameObject")
    {
        function = new MyFunc();
    }
    public class MyFunc : CSLE.RegHelper_TypeFunction
    {
        public MyFunc()
            : base(typeof(GameObject))
        {

        }
        public override CSLE.CLS_Content.Value StaticCall(CSLE.CLS_Content environment, string function, IList<CSLE.CLS_Content.Value> _params)
        {
            switch(function)
            {
                case "Destory":
                    GameObject.Destroy(_params[0].value as UnityEngine.Object);
                    return CSLE.CLS_Content.Value.Void;
                case "DestroyImmediate":
                    GameObject.DestroyImmediate(_params[0].value as UnityEngine.Object);
                    return CSLE.CLS_Content.Value.Void;
                case "Find":
                    {
                        CSLE.CLS_Content.Value v =new CSLE.CLS_Content.Value();
                        v.value=GameObject.Find(_params[0].value as string);
                        v.type = typeof(GameObject);
                        return v;
                    }
            }

            
            return base.StaticCall(environment, function, _params);
        }
        public override void MemberValueSet(CSLE.CLS_Content content, object object_this, string valuename, object value)
        {
            if (valuename == "name")
            {
                ((GameObject)object_this).name = value as string;
                return;
            }
            base.MemberValueSet(content, object_this, valuename, value);
        }
        public override CSLE.CLS_Content.Value MemberValueGet(CSLE.CLS_Content environment, object object_this, string valuename)
        {
            if (valuename == "transform")
            {
                CSLE.CLS_Content.Value v = new CSLE.CLS_Content.Value();
                v.value=                ((GameObject)object_this).transform;
                v.type =typeof(Transform);

                return v;
            }
            return base.MemberValueGet(environment, object_this, valuename);
        }
    }
}
public class MYTransform : CSLE.RegHelper_Type
{
    public MYTransform()
        : base(typeof(Transform), "Transform")
    {
        function = new MyFunc();
    }
    public class MyFunc : CSLE.RegHelper_TypeFunction
    {
        public MyFunc()
            : base(typeof(Transform))
        {

        }

        public override void MemberValueSet(CSLE.CLS_Content content, object object_this, string valuename, object value)
        {
            switch(valuename)
            {
                case "localScale":
                    ((Transform)object_this).localScale = (Vector3)value;
                    return;
                case "parent":
                    ((Transform)object_this).parent = value as Transform;
                return;
            }

            base.MemberValueSet(content, object_this, valuename, value);
        }
    }
}

public class MYVector3 : CSLE.RegHelper_Type
{
    public MYVector3()
        : base(typeof(Vector3), "Vector3")
    {
        function = new MyFunc();
    }
    public class MyFunc : CSLE.RegHelper_TypeFunction
    {
        public MyFunc()
            : base(typeof(Vector3))
        {

        }
        public override CSLE.CLS_Content.Value StaticValueGet(CSLE.CLS_Content environment, string valuename)
        {
            if(valuename=="one")
            {
                CSLE.CLS_Content.Value v = new CSLE.CLS_Content.Value();
                v.value = Vector3.one;
                v.type = typeof(Vector3);
                return v;
            }
            return base.StaticValueGet(environment, valuename);
        }
    }
}
    class CSLightMgr
    {
        public static CSLE.CLS_Environment env = null;
        //public static LuaInterface.LuaState luser = null;
        static Dictionary<int, CSLE.ICLS_Expression> expcache = new Dictionary<int, CSLE.ICLS_Expression>();
        public static void InitCSLight()
        {
            env = new CSLE.CLS_Environment(new Logger());
            //env.RegType(new CSLE.RegHelper_Type(typeof(GameObject)));
            env.RegType(new MYGameObject());
            //env.RegType(new CSLE.RegHelper_Type(typeof(Debug)));
            env.RegType(new MYDebug());      
            //env.RegType(new CSLE.RegHelper_Type(typeof(Transform)));
            env.RegType(new MYTransform());
            env.RegType(new CSLE.RegHelper_Type(typeof(Camera)));
            env.RegType(new CSLE.RegHelper_Type(typeof(Vector3)));
        }
        public static void InitLua()
        {
//            luser = new LuaInterface.LuaState();
//            string initlua =
//            @"  luanet.load_assembly('UnityEngine')
//			    luanet.load_assembly('Assembly-CSharp')
//                GameObject = luanet.import_type('UnityEngine.GameObject')
//                Debug = luanet.import_type('UnityEngine.Debug')
//			    Camera = luanet.import_type('UnityEngine.Camera')
//			    Vector3 = luanet.import_type('UnityEngine.Vector3');
//			    Resources = luanet.import_type('UnityEngine.Resources')
//";
//            luser.DoString(initlua);
        }
        public static void LuaDoString(string code)
        {
            //luser.DoString(code);
        }
        public static void CSLightDoString(string code)
        {
            var c = new CSLE.CLS_Content(env,false);
            int hash=code.GetHashCode();
            if(expcache.ContainsKey(hash))
            {
                env.Expr_Execute(expcache[hash],c);
                return;
            }
            var t = env.ParserToken(code);
            var e = env.Expr_CompilerToken(t);
            expcache[hash] = e;
            env.Expr_Execute(e,c);
        }
    }

