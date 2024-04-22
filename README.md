# .Net Core学习计划
## 1、异步的特性
异步方法使用async关键字进行定义，并通过await关键字来等待异步操作完成。
<br><br>
异步方法通常返回Task或Task\<T\>，其中Task用于返回具有特定类型的值的异步操作。Task表示一个没有返回值的任务，而Task\<T\>则是一个带有泛型类型参数的任务，该参数表示返回值的类型。

### 1.1 async、await背后的原理

从编译器中可以看出，异步方法 是将代码 切割成多个块
<br><br>
在程序执行过程中，看似等待 实则没有等待，是不停的
<br><br>
### 1.2 程序池与异步
```
using System;

class Program
{
  static async Task Main(string[] args)
  {
      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);//线程池id：1
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < 10000; i++)
      {
        sb.Append("AAAAAAAAAAA");
      }
      await File.AppendAllTextAsync(@"e:\1.txt",sb.ToString());
      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);//线程池id：4
  }
}
```
由此可见，可以理解为以await 为模块，如果执行时间较长，线程池会被释放处理其他事务。
<br><br>
等处理完后，会换新的程序池处理
___
