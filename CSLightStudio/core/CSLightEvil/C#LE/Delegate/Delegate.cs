using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    //改为DeleEvent
    public class DeleEvent //指向系统中的事件委托
    {
        public DeleEvent(object source, System.Reflection.EventInfo _event)
        {
            this.source = source;
            this._event = _event;
        }
        //public DeleEvent(Delegate instance, CSLE.CLS_Content content)
        //{
        //    deleInstance = instance;
        //    deleContent = content;
        //}
        //public object Call(CSLE.CLS_Content parentContent, IList<CSLE.CLS_Content.Value> _params)
        //{
        //    object[] plist = new object[_params.Count];
        //    for (int i = 0; i < _params.Count; i++)
        //    {
        //        plist[i] = _params[i].value;
        //    }
        //    if (deleInstance != null)
        //    {
        //        if (parentContent != null)
        //            parentContent.InStack(deleContent);
        //        object returnv = deleInstance.Method.Invoke(deleInstance.Target, plist);
        //        if (parentContent != null)
        //            parentContent.OutStack(deleContent);
        //        return returnv;
        //    }
        //    else
        //    {
        //        throw new Exception("事件不能調用");
        //    }
        //    return null;
        //}
        //如果是事件有这两个参数
        public object source;
        public System.Reflection.EventInfo _event;


        //如果是委托实例有这两个参数
        //public Delegate deleInstance;
        //public CLS_Content deleContent;
    }

    /// <summary>
    /// 接口： IDeleBase.
    /// 仅仅是作为 DeleFunction 和 DeleLambda 的基类而已.
    /// 1. 从设计逻辑上强调：DeleFunction 和 DeleLambda 有概念上的相同之处。
    /// 2. GetKey() 方法返回字符串类型的Key，作为维持映射关系的字典Key。
    ///    至于为何不用其他形式的Key，比如用DeleFunction的对象引用来做Key，那就说来话长了。
    /// </summary>
    public interface IDeleBase {
        string GetKey();
    }

    public class DeleFunction : IDeleBase //指向脚本中的函数
    {
        public DeleFunction(SType stype, SInstance _this, string function)
        {
            this.calltype = stype;
            this.callthis = _this;
            this.function = function;
        }

        public string GetKey() {
            return calltype.Name + "_" + function;
        }

        public SType calltype;
        public SInstance callthis;
        public string function;
    }

    public class DeleLambda : IDeleBase //指向Lambda表达式
    {
        public DeleLambda(CLS_Content content,IList<ICLS_Expression> param,ICLS_Expression func)
        {
            this.content = content.Clone();
            this.expr_func = func;
            foreach(var p in param)
            {
                CLS_Expression_GetValue v1 = p as CLS_Expression_GetValue;
                CLS_Expression_Define v2 = p as CLS_Expression_Define;
                if (v1 != null)
                {
                    paramTypes.Add(null);
                    paramNames.Add(v1.value_name);
                }
                else if (v2 != null)
                {
                    paramTypes.Add(v2.value_type);
                    paramNames.Add(v2.value_name);
                }
                else
                {
                    throw new Exception("DeleLambda 参数不正确");
                }
            }
        }

        public string GetKey() {
            string key = "";
            foreach (string item in paramNames) {
                key += item + "_";
            }
            key += expr_func.tokenBegin + "_" + expr_func.tokenEnd + "_" + expr_func.lineBegin + "_" + expr_func.lineEnd;

            return key;
        }

        public List<Type> paramTypes = new List<Type>();
        public List<string> paramNames = new List<string>(); 
        public CLS_Content content;
        public ICLS_Expression expr_func;
    }

    /// <summary>
    /// 一个维持 [DeleFunction or DeleLambda] 和 [Delegate] 之间映射关系的类.
    /// </summary>
    public class Dele_Map_Delegate {
        /// <summary>
        /// Dictonary:
        /// Key   => string.
        /// Value => Delegate Object.
        /// </summary>
        private static Dictionary<string, Delegate> m_Dict = new Dictionary<string, Delegate>();

        /// <summary>
        /// 进行映射操作。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Map(IDeleBase key, Delegate value) {
            if (key == null) {
                throw new Exception("[Dele_Map_Delegate::GetDelegate()] key == null.");
            }
            string strKey = key.GetKey();
            m_Dict[strKey] = value;
        }

        /// <summary>
        /// 获取 Delegate.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Delegate GetDelegate(IDeleBase key) {
            if (key == null) {
                throw new Exception("[Dele_Map_Delegate::GetDelegate()] key == null.");
            }
            string strKey = key.GetKey();
            if (!m_Dict.ContainsKey(strKey)) {
                //throw new Exception("[Dele_Map_Delegate::GetDelegate()] Count not find the key => " + key);
                return null;
            }
            return m_Dict[strKey];
        }

        /// <summary>
        /// 销毁单个映射关系.
        /// </summary>
        public static void Destroy(IDeleBase key) {
            if (key == null) {
                throw new Exception("[Dele_Map_Delegate::Destriy()] key == null.");
            }
            string strKey = key.GetKey();
            if (m_Dict.ContainsKey(strKey)) {
                m_Dict.Remove(strKey);
            }
        }

        /// <summary>
        /// 销毁全部映射关系.
        /// </summary>
        public static void Destroy() {
            m_Dict.Clear();
        }
    }
}
