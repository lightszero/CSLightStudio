using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    public class RegHelper_TypeFunction : ICLS_TypeFunction
    {
        Type type;
        public RegHelper_TypeFunction(Type type)
        {
            this.type = type;
        }
        public virtual CLS_Content.Value New(CLS_Content environment, IList<CLS_Content.Value> _params)
        {

            List<Type> types = new List<Type>();
            List<object> objparams = new List<object>();
            foreach (var p in _params)
            {
                types.Add(p.type);
                objparams.Add(p.value);
            }
            CLS_Content.Value value = new CLS_Content.Value();
            value.type = type;
            var con = this.type.GetConstructor(types.ToArray());
            if (con == null)
            {
                value.value = Activator.CreateInstance(this.type);
            }
            else
            {
                value.value = con.Invoke(objparams.ToArray());
            }
            return value;
        }
        public virtual CLS_Content.Value StaticCall(CLS_Content environment, string function, IList<CLS_Content.Value> _params, MethodCache cache = null)
        {
            bool needConvert = false;
            List<object> _oparams = new List<object>();
            List<Type> types = new List<Type>();
            bool bEm = false;
            foreach (var p in _params)
            {
                _oparams.Add(p.value);
                if ((SType)p.type != null)
                {
                    types.Add(typeof(object));
                }
                else
                {
                    if (p.type == null)
                    {
                        bEm = true;

                    }
                    types.Add(p.type);
                }
            }
            System.Reflection.MethodInfo targetop = null;
            if (!bEm)
                targetop = type.GetMethod(function, types.ToArray());
            //if (targetop == null && type.BaseType != null)//加上父类型静态函数查找,典型的现象是 GameObject.Destory
            //{
            //    targetop = type.BaseType.GetMethod(function, types.ToArray());
            //}
            if (targetop == null)
            {
                if (function[function.Length - 1] == '>')//这是一个临时的模板函数调用
                {
                    int sppos = function.IndexOf('<', 0);
                    string tfunc = function.Substring(0, sppos);
                    string strparam = function.Substring(sppos + 1, function.Length - sppos - 2);
                    string[] sf = strparam.Split(',');
                    //string tfunc = sf[0];
                    Type[] gtypes = new Type[sf.Length];
                    for (int i = 0; i < sf.Length; i++)
                    {
                        gtypes[i] = environment.environment.GetTypeByKeyword(sf[i]).type;
                    }
                    targetop = FindTMethod(type, tfunc, _params, gtypes);

                }
                if (targetop == null)
                {
                    Type ptype = type.BaseType;
                    while (ptype != null)
                    {
                        targetop = ptype.GetMethod(function, types.ToArray());
                        if (targetop != null) break;
                        var t = environment.environment.GetType(ptype);
                        try
                        {
                            return t.function.StaticCall(environment, function, _params, cache);
                        }
                        catch
                        {

                        }
                        ptype = ptype.BaseType;
                    }

                }
            }
            if (targetop == null)
            {//因为有cache的存在，可以更慢更多的查找啦，哈哈哈哈
                targetop = GetMethodSlow(environment, true, function, types, _oparams);
                needConvert = true;
            }

            if (targetop == null)
            {
                throw new Exception("函数不存在function:" + type.ToString() + "." + function);
            }
            if (cache != null)
            {
                cache.info = targetop;
                cache.slow = needConvert;
            }


            CLS_Content.Value v = new CLS_Content.Value();
            v.value = targetop.Invoke(null, _oparams.ToArray());
            v.type = targetop.ReturnType;
            return v;


        }

        public virtual CLS_Content.Value StaticCallCache(CLS_Content content, IList<CLS_Content.Value> _params, MethodCache cache)
        {

            List<object> _oparams = new List<object>();
            List<Type> types = new List<Type>();
            foreach (var p in _params)
            {
                _oparams.Add(p.value);
                if ((SType)p.type != null)
                {
                    types.Add(typeof(object));
                }
                else
                {
                    types.Add(p.type);
                }
            }
            var targetop = cache.info;
            if (cache.slow)
            {
                var pp = targetop.GetParameters();
                for (int i = 0; i < pp.Length; i++)
                {
                    if (i >= _params.Count)
                    {
                        _oparams.Add(pp[i].DefaultValue);
                    }
                    else
                    {
                        if (pp[i].ParameterType != (Type)_params[i].type)
                        {
                            _oparams[i] = content.environment.GetType(_params[i].type).ConvertTo(content, _oparams[i], pp[i].ParameterType);
                        }
                    }
                }
            }
            CLS_Content.Value v = new CLS_Content.Value();
            v.value = targetop.Invoke(null, _oparams.ToArray());
            v.type = targetop.ReturnType;
            return v;


        }

        public virtual CLS_Content.Value StaticValueGet(CLS_Content environment, string valuename)
        {
            var v = MemberValueGet(environment, null, valuename);
            if (v == null)
            {
                if (type.BaseType != null)
                {
                    return environment.environment.GetType(type.BaseType).function.StaticValueGet(environment, valuename);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                return v;
            }

            //var targetf = type.GetField(valuename);
            //if (targetf != null)
            //{
            //    CLS_Content.Value v = new CLS_Content.Value();
            //    v.value = targetf.GetValue(null);
            //    v.type = targetf.FieldType;
            //    return v;
            //}
            //else
            //{
            //    var methodf = type.GetMethod("get_" + valuename);
            //    if (methodf != null)
            //    {
            //        CLS_Content.Value v = new CLS_Content.Value();
            //        v.value = methodf.Invoke(null, null);
            //        v.type = methodf.ReturnType;
            //        return v;
            //    }
            //    //var targetf = type.GetField(valuename);
            //    //if (targetf != null)
            //    //{
            //    //    CLS_Content.Value v = new CLS_Content.Value();
            //    //    v.value = targetf.GetValue(null);
            //    //    v.type = targetf.FieldType;
            //    //    return v;
            //    //}
            //    else
            //    {
            //        var targete = type.GetEvent(valuename);
            //        if (targete != null)
            //        {
            //            CLS_Content.Value v = new CLS_Content.Value();

            //            v.value = new DeleEvent(null, targete);
            //            v.type = targete.EventHandlerType;
            //            return v;
            //        }
            //    }
            //}
            //if (type.BaseType != null)
            //{
            //    return environment.environment.GetType(type.BaseType).function.StaticValueGet(environment, valuename);
            //}


            //throw new NotImplementedException();
        }

        public virtual bool StaticValueSet(CLS_Content content, string valuename, object value)
        {

            bool b = MemberValueSet(content, null, valuename, value);
            if (!b)
            {
                if (type.BaseType != null)
                {
                    content.environment.GetType(type.BaseType).function.StaticValueSet(content, valuename, value);
                    return true;
                }
                else
                {

                    throw new NotImplementedException();
                }
            }
            else
            {
                return b;
            }


        }
        Dictionary<int, System.Reflection.MethodInfo> cacheT;//= new Dictionary<string, System.Reflection.MethodInfo>();
        System.Reflection.MethodInfo FindTMethod(Type type, string func, IList<CLS_Content.Value> _params, Type[] gtypes)
        {
            string hashkey = func + "_" + _params.Count + ":";
            foreach (var p in _params)
            {
                hashkey += p.type.ToString();
            }
            foreach (var t in gtypes)
            {
                hashkey += t.ToString();
            }
            int hashcode = hashkey.GetHashCode();
            if (cacheT != null)
            {
                if (cacheT.ContainsKey(hashcode))
                {
                    return cacheT[hashcode];
                }
            }
            //+"~" + (sf.Length - 1).ToString();
            var ms = type.GetMethods();
            foreach (var t in ms)
            {
                if (t.Name == func && t.IsGenericMethodDefinition)
                {
                    var pp = t.GetParameters();
                    if (pp.Length != _params.Count) continue;
                    bool match = true;
                    for (int i = 0; i < pp.Length; i++)
                    {
                        if (pp[i].ParameterType.IsGenericParameter) continue;
                        if (pp[i].ParameterType != (Type)_params[i].type)
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        var targetop = t.MakeGenericMethod(gtypes);
                        if (cacheT == null)
                            cacheT = new Dictionary<int, System.Reflection.MethodInfo>();
                        cacheT[hashcode] = targetop;
                        return targetop;
                    }
                }
            }
            //targetop = type.GetMethod(tfunc, types.ToArray());
            //var pp =targetop.GetParameters();
            //targetop = targetop.MakeGenericMethod(gtypes);
            return null;
        }
        public virtual CLS_Content.Value MemberCall(CLS_Content environment, object object_this, string function, IList<CLS_Content.Value> _params, MethodCache cache = null)
        {
            bool needConvert = false;
            List<Type> types = new List<Type>();
            List<object> _oparams = new List<object>();
            bool bEm = false;
            foreach (var p in _params)
            {
                {
                    _oparams.Add(p.value);
                }
                if ((SType)p.type != null)
                {
                    types.Add(typeof(object));
                }
                else
                {
                    if (p.type == null)
                    {
                        bEm = true;
                    }
                    types.Add(p.type);
                }
            }

            System.Reflection.MethodInfo targetop = null;
            if (!bEm)
            {
                targetop = type.GetMethod(function, types.ToArray());
            }
            CLS_Content.Value v = new CLS_Content.Value();
            if (targetop == null)
            {
                if (function[function.Length - 1] == '>')//这是一个临时的模板函数调用
                {
                    int sppos = function.IndexOf('<', 0);
                    string tfunc = function.Substring(0, sppos);
                    string strparam = function.Substring(sppos + 1, function.Length - sppos - 2);
                    string[] sf = strparam.Split(',');
                    //string tfunc = sf[0];
                    Type[] gtypes = new Type[sf.Length];
                    for (int i = 0; i < sf.Length; i++)
                    {
                        gtypes[i] = environment.environment.GetTypeByKeyword(sf[i]).type;
                    }
                    targetop = FindTMethod(type, tfunc, _params, gtypes);

                }
                else
                {
                    if (!bEm)
                    {
                        foreach (var s in type.GetInterfaces())
                        {
                            targetop = s.GetMethod(function, types.ToArray());
                            if (targetop != null) break;
                        }
                    }
                    if (targetop == null)
                    {//因为有cache的存在，可以更慢更多的查找啦，哈哈哈哈
                        targetop = GetMethodSlow(environment, false, function, types, _oparams);
                        needConvert = true;
                    }
                    if (targetop == null)
                    {
                        throw new Exception("函数不存在function:" + type.ToString() + "." + function);
                    }
                }
            }
            if (cache != null)
            {
                cache.info = targetop;
                cache.slow = needConvert;
            }

            if (targetop == null)
            {
                throw new Exception("函数不存在function:" + type.ToString() + "." + function);
            }
            v.value = targetop.Invoke(object_this, _oparams.ToArray());
            v.type = targetop.ReturnType;
            return v;
        }

        Dictionary<string, IList<System.Reflection.MethodInfo>> slowCache = null;

        System.Reflection.MethodInfo GetMethodSlow(CSLE.CLS_Content content, bool bStatic, string funcname, IList<Type> types, IList<object> _params)
        {
            List<object> myparams = new List<object>(_params);
            if (slowCache == null)
            {
                System.Reflection.MethodInfo[] ms = this.type.GetMethods();
                slowCache = new Dictionary<string, IList<System.Reflection.MethodInfo>>();
                foreach (var m in ms)
                {
                    string name = m.IsStatic ? "s=" + m.Name : m.Name;
                    if (slowCache.ContainsKey(name) == false)
                    {
                        slowCache[name] = new List<System.Reflection.MethodInfo>();
                    }
                    slowCache[name].Add(m);
                }
            }
            IList<System.Reflection.MethodInfo> minfo = null;

            if (slowCache.TryGetValue(bStatic ? "s=" + funcname : funcname, out minfo) == false)
                return null;

            foreach (var m in minfo)
            {
                bool match = true;
                var pp = m.GetParameters();
                if (pp.Length < types.Count)//参数多出来，不匹配
                {
                    match = false;
                    continue;
                }
                for (int i = 0; i < pp.Length; i++)
                {
                    if (i >= types.Count)//参数多出来
                    {
                        if (!pp[i].IsOptional)
                        {

                            match = false;
                            break;
                        }
                        else
                        {
                            myparams.Add(pp[i].DefaultValue);
                        }
                    }
                    else
                    {
                        if (pp[i].ParameterType == types[i]) continue;

                        try
                        {
                            if (types[i] == null && !pp[i].ParameterType.IsValueType)
                            {
                                continue;
                            }
                            myparams[i] = content.environment.GetType(types[i]).ConvertTo(content, _params[i], pp[i].ParameterType);
                            if (myparams[i] == null)
                            {
                                match = false;
                                break;
                            }
                        }
                        catch
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                        break;
                }
                if (!match)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < myparams.Count; i++)
                    {
                        if (i < _params.Count)
                        {
                            _params[i] = myparams[i];
                        }
                        else
                        {
                            _params.Add(myparams[i]);
                        }
                    }
                    return m;
                }

            }

            if (minfo.Count == 1)
                return minfo[0];

            return null;

        }
        public virtual CLS_Content.Value MemberCallCache(CLS_Content content, object object_this, IList<CLS_Content.Value> _params, MethodCache cache)
        {
            List<Type> types = new List<Type>();
            List<object> _oparams = new List<object>();
            foreach (var p in _params)
            {
                {
                    _oparams.Add(p.value);
                }
                if ((SType)p.type != null)
                {
                    types.Add(typeof(object));
                }
                else
                {
                    types.Add(p.type);
                }
            }

            var targetop = cache.info;
            CLS_Content.Value v = new CLS_Content.Value();
            if (cache.slow)
            {
                var pp = targetop.GetParameters();
                for (int i = 0; i < pp.Length; i++)
                {
                    if (i >= _params.Count)
                    {
                        _oparams.Add(pp[i].DefaultValue);
                    }
                    else
                    {
                        if (pp[i].ParameterType != (Type)_params[i].type)
                        {
                            _oparams[i] = content.environment.GetType(_params[i].type).ConvertTo(content, _oparams[i], pp[i].ParameterType);
                        }
                    }
                }
            }
            v.value = targetop.Invoke(object_this, _oparams.ToArray());
            v.type = targetop.ReturnType;
            return v;
        }

        class MemberValueCache
        {
            public int type = 0;
            public System.Reflection.FieldInfo finfo;
            public System.Reflection.MethodInfo minfo;
            public System.Reflection.EventInfo einfo;

        }
        Dictionary<string, MemberValueCache> memberValuegetCaches = new Dictionary<string, MemberValueCache>();
        public virtual CLS_Content.Value MemberValueGet(CLS_Content environment, object object_this, string valuename)
        {

            MemberValueCache c;

            if (!memberValuegetCaches.TryGetValue(valuename, out c))
            {
                c = new MemberValueCache();
                memberValuegetCaches[valuename] = c;
                c.finfo = type.GetField(valuename);
                if (c.finfo == null)
                {
                    c.minfo = type.GetMethod("get_" + valuename);
                    if (c.minfo == null)
                    {
                        c.einfo = type.GetEvent(valuename);
                        if (c.einfo == null)
                        {
                            c.type = -1;
                            return null;
                        }
                        else
                        {
                            c.type = 3;
                        }
                    }
                    else
                    {
                        c.type = 2;
                    }
                }
                else
                {
                    c.type = 1;
                }
            }

            if (c.type < 0) return null;
            CLS_Content.Value v = new CLS_Content.Value();
            switch (c.type)
            {
                case 1:

                    v.value = c.finfo.GetValue(object_this);
                    v.type = c.finfo.FieldType;
                    break;
                case 2:

                    v.value = c.minfo.Invoke(object_this, null);
                    v.type = c.minfo.ReturnType;
                    break;
                case 3:
                    v.value = new DeleEvent(object_this, c.einfo);
                    v.type = c.einfo.EventHandlerType;
                    break;

            }
            return v;

            //var targetf = type.GetField(valuename);
            //if (targetf != null)
            //{
            //    CLS_Content.Value v = new CLS_Content.Value();
            //    v.value = targetf.GetValue(object_this);
            //    v.type = targetf.FieldType;
            //    return v;
            //}

            //var targetp = type.GetProperty(valuename);
            //if (targetp != null)
            //{
            //    CLS_Content.Value v = new CLS_Content.Value();
            //    v.value = targetp.GetValue(object_this, null);
            //    v.type = targetp.PropertyType;
            //    return v;
            //}
            //else
            //{//用get set 方法替代属性操作，AOT环境属性操作有问题
            //    var methodf = type.GetMethod("get_" + valuename);
            //    if (methodf != null)
            //    {
            //        CLS_Content.Value v = new CLS_Content.Value();
            //        v.value = methodf.Invoke(object_this, null);
            //        v.type = methodf.ReturnType;
            //        return v;
            //    }
            //var targetf = type.GetField(valuename);
            //if (targetf != null)
            //{
            //    CLS_Content.Value v = new CLS_Content.Value();
            //    v.value = targetf.GetValue(object_this);
            //    v.type = targetf.FieldType;
            //    return v;
            //}
            //    else
            //    {
            //        System.Reflection.EventInfo targete = type.GetEvent(valuename);
            //        if (targete != null)
            //        {
            //            CLS_Content.Value v = new CLS_Content.Value();
            //            v.value = new DeleEvent(object_this, targete);
            //            v.type = targete.EventHandlerType;
            //            return v;
            //        }
            //    }
            //}

            //return null;
        }

        Dictionary<string, MemberValueCache> memberValuesetCaches = new Dictionary<string, MemberValueCache>();

        public virtual bool MemberValueSet(CLS_Content content, object object_this, string valuename, object value)
        {
            MemberValueCache c;

            if (!memberValuesetCaches.TryGetValue(valuename, out c))
            {
                c = new MemberValueCache();
                memberValuesetCaches[valuename] = c;
                c.finfo = type.GetField(valuename);
                if (c.finfo == null)
                {
                    c.minfo = type.GetMethod("set_" + valuename);
                    if (c.minfo == null)
                    {
                        c.type = -1;
                        return false;
                    }
                    else
                    {
                        c.type = 2;
                    }
                }
                else
                {
                    c.type = 1;
                }
            }

            if (c.type < 0)
                return false;

            if (c.type == 1)
            {
                if (value != null && value.GetType() != c.finfo.FieldType)
                {

                    value = content.environment.GetType(value.GetType()).ConvertTo(content, value, c.finfo.FieldType);
                }
                c.finfo.SetValue(object_this, value);
            }
            else
            {
                var ptype = c.minfo.GetParameters()[0].ParameterType;
                if (value != null && value.GetType() != ptype)
                {

                    value = content.environment.GetType(value.GetType()).ConvertTo(content, value, ptype);
                }
                c.minfo.Invoke(object_this, new object[] { value });
            }
            return true;
            ////先操作File
            //var targetf = type.GetField(valuename);
            //if (targetf != null)
            //{
            //    if (value != null && value.GetType() != targetf.FieldType)
            //    {

            //        value = content.environment.GetType(value.GetType()).ConvertTo(content, value, targetf.FieldType);
            //    }
            //    targetf.SetValue(object_this, value);
            //    return;
            //}
            //else
            //{
            //    var methodf = type.GetMethod("set_" + valuename);
            //    if (methodf != null)
            //    {
            //        var ptype = methodf.GetParameters()[0].ParameterType;
            //        if (value != null && value.GetType() != ptype)
            //        {

            //            value = content.environment.GetType(value.GetType()).ConvertTo(content, value, ptype);
            //        }
            //        methodf.Invoke(object_this, new object[] { value });

            //        return;
            //    }
            //}



            //throw new NotImplementedException();
        }



        System.Reflection.MethodInfo indexGetCache = null;
        Type indexGetCacheType = null;
        public virtual CLS_Content.Value IndexGet(CLS_Content environment, object object_this, object key)
        {
            //var m = type.GetMembers();
            if (indexGetCache == null)
            {
                indexGetCache = type.GetMethod("get_Item");
                if (indexGetCache != null)
                {
                    indexGetCacheType = indexGetCache.ReturnType;
                }
                //    CLS_Content.Value v = new CLS_Content.Value();
                //    v.type = targetop.ReturnType;
                //    v.value = targetop.Invoke(object_this, new object[] { key });
                //    return v;
                //}
                if (indexGetCache == null)
                {
                    indexGetCache = type.GetMethod("GetValue", new Type[] { typeof(int) });
                    if (indexGetCache != null)
                    {
                        indexGetCacheType = type.GetElementType();
                    }
                }
                if (indexGetCache != null)
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.type = indexGetCacheType;
                    v.value = indexGetCache.Invoke(object_this, new object[] { key });
                    return v;
                }
                //{
                //    targetop = type.GetMethod("GetValue", new Type[] { typeof(int) });
                //    if (targetop != null)
                //    {
                //        //targetop = type.GetMethod("Get");

                //        CLS_Content.Value v = new CLS_Content.Value();
                //        v.type = type.GetElementType();
                //        v.value = targetop.Invoke(object_this, new object[] { key });
                //        return v;
                //    }
                //}
            }
            else
            {
                CLS_Content.Value v = new CLS_Content.Value();
                v.type = indexGetCacheType;
                v.value = indexGetCache.Invoke(object_this, new object[] { key });
                return v;
            }
            throw new NotImplementedException();

        }

        System.Reflection.MethodInfo indexSetCache = null;
        bool indexSetCachekeyfirst = false;
        public virtual void IndexSet(CLS_Content environment, object object_this, object key, object value)
        {
            if (indexSetCache == null)
            {
                indexSetCache = type.GetMethod("set_Item");
                indexSetCachekeyfirst = true;
                if (indexSetCache == null)
                {
                    indexSetCache = type.GetMethod("SetValue", new Type[] { typeof(object), typeof(int) });
                    indexSetCachekeyfirst = false;
                }

            }
            //else
            if (indexSetCachekeyfirst)
            {
                indexSetCache.Invoke(object_this, new object[] { key, value });
            }
            else
            {
                indexSetCache.Invoke(object_this, new object[] { value, key });
            }
            //var m = type.GetMethods();
            //var targetop = type.GetMethod("set_Item");
            //if (targetop == null)
            //{
            //    //targetop = type.GetMethod("Set");
            //    targetop = type.GetMethod("SetValue", new Type[] { typeof(object), typeof(int) });
            //    targetop.Invoke(object_this, new object[] { value, key });
            //    return;
            //}
            //targetop.Invoke(object_this, new object[] { key, value });
        }
    }

    public class RegHelper_Type : ICLS_Type
    {

        public RegHelper_Type(Type type, string setkeyword = null)
        {
            if(type.IsSubclassOf(typeof(Delegate)))
            {
                throw new Exception("你想注册的Type是一个Delegate，需要用特别的注册方法");
            }
            function = new RegHelper_TypeFunction(type);
            if (setkeyword != null)
            {
                keyword = setkeyword.Replace(" ", "");
            }
            else
            {
                keyword = type.Name;
            }
            this.type = type;
            this._type = type;
        }
        protected RegHelper_Type(Type type, string setkeyword,bool dele)
        {

            function = new RegHelper_TypeFunction(type);
            if (setkeyword != null)
            {
                keyword = setkeyword.Replace(" ", "");
            }
            else
            {
                keyword = type.Name;
            }
            this.type = type;
            this._type = type;
        }
        public string keyword
        {
            get;
            protected set;
        }
        public string _namespace
        {
            get { return type.NameSpace; }
        }
        public CLType type
        {
            get;
            protected set;
        }
        public Type _type;

        public virtual ICLS_Value MakeValue(object value) //这个方法可能存在AOT陷阱
        {
            //这个方法可能存在AOT陷阱
            //Type target = typeof(CLS_Value_Value<>).MakeGenericType(new Type[] { type }); 
            //return target.GetConstructor(new Type[] { }).Invoke(new object[0]) as ICLS_Value;

            CLS_Value_Object rvalue = new CLS_Value_Object(type);
            rvalue.value_value = value;
            return rvalue;
        }

        public virtual object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if (this._type == (Type)targetType) return src;

            //type.get

            if (_type.IsEnum)
            {


                if ((Type)targetType == typeof(int))
                    return System.Convert.ToInt32(src);
                else if ((Type)targetType == typeof(uint))
                    return System.Convert.ToUInt32(src);
                else if ((Type)targetType == typeof(short))
                    return System.Convert.ToInt16(src);
                else if ((Type)targetType == typeof(ushort))
                    return System.Convert.ToUInt16(src);
                else
                {
                    return System.Convert.ToInt32(src);
                }
            }
            var ms = _type.GetMethods();
            foreach (var m in ms)
            {
                if ((m.Name == "op_Implicit" || m.Name == "op_Explicit") && m.ReturnType == (Type)targetType)
                {
                    return m.Invoke(null, new object[] { src });
                }
            }
            if ((Type)targetType != null)
            {
                if (((Type)targetType).IsAssignableFrom(_type))
                    return src;
                if (src != null && ((Type)targetType).IsInstanceOfType(src))
                    return src;
            }
            else
            {
                return src;
            }

            return null;
        }

        public virtual object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = type;
            System.Reflection.MethodInfo call = null;
            var m = ((Type)type).GetMembers();
            if (code == '+')
            {
                if ((Type)right.type == typeof(string))
                {
                    returntype = typeof(string);
                    return left.ToString() + right.value as string;
                }
                call = _type.GetMethod("op_Addition", new Type[] { this.type, right.type });
            }
            else if (code == '-')//base = {CLScriptExt.Vector3 op_Subtraction(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_Subtraction", new Type[] { this.type, right.type });
            else if (code == '*')//[2] = {CLScriptExt.Vector3 op_Multiply(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_Multiply", new Type[] { this.type, right.type });
            else if (code == '/')//[3] = {CLScriptExt.Vector3 op_Division(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_Division", new Type[] { this.type, right.type });
            else if (code == '%')//[4] = {CLScriptExt.Vector3 op_Modulus(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_Modulus", new Type[] { this.type, right.type });


            var obj = call.Invoke(null, new object[] { left, right.value });
            //function.StaticCall(env,"op_Addtion",new List<ICL>{})
            return obj;
        }

        public virtual bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            System.Reflection.MethodInfo call = null;

            //var m = _type.GetMethods();
            if (code == logictoken.more)//[2] = {Boolean op_GreaterThan(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_GreaterThan");
            else if (code == logictoken.less)//[4] = {Boolean op_LessThan(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_LessThan");
            else if (code == logictoken.more_equal)//[3] = {Boolean op_GreaterThanOrEqual(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_GreaterThanOrEqual");
            else if (code == logictoken.less_equal)//[5] = {Boolean op_LessThanOrEqual(CLScriptExt.Vector3, CLScriptExt.Vector3)}
                call = _type.GetMethod("op_LessThanOrEqual");
            else if (code == logictoken.equal)//[6] = {Boolean op_Equality(CLScriptExt.Vector3, CLScriptExt.Vector3)}
            {
                if (left == null || right.type == null)
                {
                    return left == right.value;
                }



                call = _type.GetMethod("op_Equality");
                if (call == null)
                {
                    return left.Equals(right.value);
                }
            }
            else if (code == logictoken.not_equal)//[7] = {Boolean op_Inequality(CLScriptExt.Vector3, CLScriptExt.Vector3)}
            {
                if (left == null || right.type == null)
                {
                    return left != right.value;
                }
                call = _type.GetMethod("op_Inequality");
                if (call == null)
                {
                    return !left.Equals(right.value);
                }
            }
            var obj = call.Invoke(null, new object[] { left, right.value });
            return (bool)obj;
        }

        public ICLS_TypeFunction function
        {
            get;
            protected set;
        }


        public virtual object DefValue
        {
            get { return null; }
        }
    }
}
