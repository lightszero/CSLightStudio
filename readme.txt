C#Light/Evil 是一组pure C#写成的脚本语言
能够对Unity等的逻辑热更新提供莫大的帮助
具体信息可移步http://crazylights.cnblogs.com/
有问题可以加QQ群研讨：119706192

针对Alpha的问题说明
C#Light已经是正式版，在商业项目中检验过
C#Light/Evil是在C#Light的基础上增加面向对象部分
虽然Alpha但是稳定性也很高，如果不使用面向对象就是C#Light

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