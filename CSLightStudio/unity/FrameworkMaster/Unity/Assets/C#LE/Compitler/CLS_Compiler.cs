using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {
        ICLS_Logger logger;
        public CLS_Expression_Compiler(ICLS_Logger logger)
        {
            this.logger = logger;
        }
        public ICLS_Expression Compiler(IList<Token> tlist, ICLS_Environment content)
        {
            ICLS_Expression value;

            int expbegin = 0;
            int expend = FindCodeBlock(tlist, expbegin);
            if (expend != tlist.Count - 1)
            {
                LogError(tlist, "CodeBlock 识别问题,异常结尾", expbegin, expend);
                return null;
            }
            bool succ = Compiler_Expression_Block(tlist, content, expbegin, expend, out value);
            if (succ)
            {
                if (value == null)
                {
                    logger.Log_Warn("编译为null:");
                }
                return value;

            }
            else
            {
                LogError(tlist, "编译失败:", expbegin, expend);
                return null;
            }



        }

        public ICLS_Expression Compiler_NoBlock(IList<Token> tlist, ICLS_Environment content)
        {
            ICLS_Expression value;
            int expbegin = 0;
            int expend = tlist.Count - 1;
            bool succ = Compiler_Expression(tlist, content, expbegin, expend, out value);
            if (succ)
            {
                if (value == null)
                {
                    logger.Log_Warn("编译为null:");
                }
                return value;

            }
            else
            {
                LogError(tlist, "编译失败:", expbegin, expend);
                return null;
            }


        }
        public ICLS_Expression Optimize(ICLS_Expression value, ICLS_Environment env)
        {
            ICLS_Expression expr = value as ICLS_Expression;
            if (expr == null) return value;
            else return OptimizeDepth(expr, new CLS_Content(env));
        }
        ICLS_Expression OptimizeDepth(ICLS_Expression expr, CLS_Content content)
        {
            //先进行深入优化
            if (expr.listParam != null)
            {
                for (int i = 0; i < expr.listParam.Count; i++)
                {
                    ICLS_Expression subexpr = expr.listParam[i] as ICLS_Expression;
                    if (subexpr != null)
                    {
                        expr.listParam[i] = OptimizeDepth(subexpr, content);
                    }
                }
            }


            return OptimizeSingle(expr, content);

        }
        ICLS_Expression OptimizeSingle(ICLS_Expression expr, CLS_Content content)
        {

            if (expr is CLS_Expression_Math2Value || expr is CLS_Expression_Math2ValueAndOr || expr is CLS_Expression_Math2ValueLogic)
            {

                if (expr.listParam[0] is ICLS_Value &&
                expr.listParam[1] is ICLS_Value)
                {
                    CLS_Content.Value result = expr.ComputeValue(content);
                    if ((Type)result.type == typeof(bool))
                    {
                        CLS_Value_Value<bool> value = new CLS_Value_Value<bool>();
                        value.value_value = (bool)result.value;
                        value.tokenBegin = expr.listParam[0].tokenBegin;
                        value.tokenEnd = expr.listParam[1].tokenEnd;
                        value.lineBegin = expr.listParam[0].lineBegin;
                        value.lineEnd = expr.listParam[1].lineEnd;
                        return value;
                    }
                    else
                    {
                        ICLS_Type v = content.environment.GetType(result.type);
                        ICLS_Value value = v.MakeValue(result.value);
                        value.tokenBegin = expr.listParam[0].tokenBegin;
                        value.tokenEnd = expr.listParam[1].tokenEnd;
                        value.lineBegin = expr.listParam[0].lineBegin;
                        value.lineEnd = expr.listParam[1].lineEnd;
                        return value;
                    }


                }
            }
            if (expr is CLS_Expression_Math3Value)
            {
                CLS_Content.Value result = expr.listParam[0].ComputeValue(content);
                if ((Type)result.type == typeof(bool))
                {
                    bool bv = (bool)result.value;
                    if (bv)
                        return expr.listParam[1];
                    else
                        return expr.listParam[2];
                }
            }

            return expr;
        }


        public IList<ICLS_Type> FileCompiler(ICLS_Environment env,string filename,IList<Token> tlist, bool embDebugToken)
        {
            return _FileCompiler(filename, tlist, embDebugToken, env, false);
        }
        public IList<ICLS_Type> FilePreCompiler(ICLS_Environment env, string filename, IList<Token> tlist)
        {
            return _FileCompiler(filename, tlist, false, env, true);
        }
    }
}