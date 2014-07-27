using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
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


	public interface ICLS_Expression_Compiler
    {
        ICLS_Expression Compiler(IList<Token> tlist, CLS_Content content);//语句
        ICLS_Expression Compiler_NoBlock(IList<Token> tlist, CLS_Content content);//表达式，一条语句
        ICLS_Expression Optimize(ICLS_Expression value,CLS_Content content);
    }

}