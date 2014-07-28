using System;
using System.Collections.Generic;
using System.Text;
namespace CSEvil
{
    //值


    //类型
    public interface ICLS_Value : ICLS_Expression
    {
        Type type
        {
            get;
        }
        object value
        {
            get;
        }
        int tokenBegin
        {
            get;
            set;
        }
        int tokenEnd
        {
            get;
            set;
        }
    }
    //表达式是一个值
    public interface ICLS_Expression
    {

        List<ICLS_Expression> listParam
        {
            get;
        }
        CLS_Content.Value ComputeValue(CLS_Content content);

        int tokenBegin
        {
            get;
        }
        int tokenEnd
        {
            get;
        }
    }
    public interface ICLS_Environment
    {
        void RegType(ICLS_Type type);
        ICLS_Type GetType(Type type);

        ICLS_Type GetTypeByKeyword(string keyword);


        void RegFunction(ICLS_Function func);
        ICLS_Function GetFunction(string name);

        ICLS_Logger logger
        {
            get;
        }
        //public ICLS_Debugger debugger;
        ICLS_TokenParser tokenParser
        {
            get;
        }
    }
    public interface ICLS_Environment_Compiler
    {

    }

    public interface ICLS_Expression_Compiler
    {
        ICLS_Expression Compiler(IList<Token> tlist, ICLS_Environment content);//语句
        ICLS_Expression Compiler_NoBlock(IList<Token> tlist, ICLS_Environment content);//表达式，一条语句
        ICLS_Expression Optimize(ICLS_Expression value, ICLS_Environment content);

        IList<ICLS_Type> FileCompiler(IList<Token> tlist, ICLS_Environment env);
    }

}