C#Light/Evil 是一组pure C#写成的脚本语言
项目主页http://www.cltri.com/ 或者http://lightszero.github.io/
能够对Unity等的逻辑热更新提供莫大的帮助
你可以在
具体信息可移步http://crazylights.cnblogs.com/
有问题可以加QQ群研讨：223823428

2014-08-27 0.48Beta
泛型类型可以如此注册一次，编译时自动注册实做类型
Type t =Type.GetType("System.Collections.Generic.List`1");
scriptService.RegType(new CSLE.RegHelper_Type(t, "List"));

0.47Beta
脚本类型可丢容器 List<object>

0.46Beta
模板函数调用
如GameObject.AddComponent<T>();

2014-08-25 0.45Beta
修正了lambda表达式 显示声明类型的语法
修正了lambda表达式空函数和脚本空函数作为delegate的情况

2014-08-22 0.44Beta
修正了int[] 作为成员变量的问题
修正了模板类型的空格兼容性，现在随便多个空格少个空格,不敏感

0.43Beta
bool型oo漏了，补上

0.42Beta
加入了新的基本类型(还在测试中) 
见C#LightTestor _3002_ 测试

20140813 0.41Beta
调整委托和事件的实现方式，使用更简便

20140811 0.40Beta
测试后提为Beta版 

0.39.4Alpha 
修改了一处关于属性调用的机制,怀疑可以让IOS平台调用更便捷，明日待测

20140806 0.39.3Alpha 匿名函数支持
目前的C#LightEvil对比C#已经没有特性的明显差异
C#LightEvil核心已经没有功能好增加
进入例子编写和稳定性测试阶段

0.39.2 修改了回调机制
脚本调用回调上下文可以调试，并为开发匿名函数支持预留了设计

0.39.1Alpha
unity提供了Framework例子，两种驱动模式
一种程序管理状态机，脚本实现状态
另一种脚本管理状态机，程序提供接口

20140805 0.39Alpha
支持了 while表达式与do while表达式
加入了unity的一些例子

0.38Alpha
完整支持了数组

0.37Alpha
加入了try catch throw机制，用法同c#

0.36.3Alpha
加入interface继承机制
2014-08-03  0.36.2 Alpha
修改一处 脚本类不能==null的问题
2014-08-03  0.36.1 Alpha 版本发布
修改了一处bug
for(int i=0;i<10;i++)
{
  int j=0;
}
当for循环中只有一行时作用域有bug，已修正

2014-08-03  0.36Alpha 版本发布
取消了暂时不用的namespace开关，免得造成误用
delegate的机制做了详尽的测试修改

C#EvilTestor改成一个单元测试的模式，方便追加测试



2014-08-02  0.35Alpha 版本发布
修正了大量调试方面的问题
并且建立了一套Unity3D使用的例子


2014-08-01  0.30Alpha 版本发布
C#Light/Evil
的功能体系已经固定，接口也已经稳定下来
进入测试修改Bug与制作例子的阶段
先确定为0.30Alpha版本
现在已支持在脚本中定义类型
支持this关键字,支持用脚本向程序委托注册回调
支持使用namespace


2014-07-31
C#Light/C#Evil决定合并，有bug两边改挺麻烦的
请大家多关注
C#Light/Evil




因为googlecode被封的太厉害，很多同学提意见，于是把CSLightStudio迁移到GitHub
这是第一次提交2014-7-8

2014-07-?
开发C#Evil的想法产生

2014-06-11
C#Light 经过商业项目检测后0.2正式版发布

2014-03-11
C#Light 0.01版提交，只完成了数值四则运算计算