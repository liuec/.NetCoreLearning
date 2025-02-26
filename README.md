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
### 1.3 async、await关键字免写
```
//async 和 await 是组合关键字，如果方法返回值本身 是Task<T>,可以去掉关键字
static Task<string> ReaadAsync2(int num)
{
    if (num == 1)
    {
        return  File.ReadAllTextAsync(@"e:\1.txt");
    }
    else
    {
        throw new ArgumentException();
    }
}
```
**这种写法可以避免重复的 拆箱装箱的动作，执行更高效**
### 1.4 CancellationToken取消执行方法
#### 1.4.1 CancellationToken方法的普通使用
用户在访问请求，可能还没有等到结果就离开，可以用CancellationToken取消执行
```
CancellationTokenSource cts = new CancellationTokenSource();//初始化
cts.CancelAfter(10000);//设定程序执行10秒后，中断
```
方法一：IsCancellationRequested，判断是否中断，可以方便用户操作
<br>
await DownloadAsync("https://www.baidu.com", cts.Token);
<br>
方法二：ThrowIfCancellationRequested()，强制中断，并返回一个Throw错误
<br>
await DownloadAsyncV2("https://www.baidu.com", cts.Token);
<br>
方法三：程序支持CancellationToken中断功能，这种防止在调用方法时超时，执行到CancellationToken时已经远超定义时间
<br>
await DownloadAsyncV3("https://www.baidu.com", cts.Token);
<br>
第一、第二种 和第三种方法可以组合使用
#### 1.4.2在Web中使用CancellationToken方法
```
public async Task<IActionResult> Index(CancellationToken cancellationToken)
{
    await ConsoleTxt("https://www.youzack.com",100000, cancellationToken);
    return View();
}
```
用户在离开网页后，会停止运算

## 2、Linq基础知识
### 2.1 Linq的演变：委托——>Lambda——>linq
#### 2.1.1 委托：指向方法，将方法当作变量传递
```
static void F1()
{
    Console.WriteLine("Hello, World!");
}
delegate void D1();//定义委托，无参
static void Main(string[] args)
{
    D1 d = F1;//将委托指向F1方法
    d();//执行委托=执行方法
}
```
```
static int F2(int i,int j)
{
    return i + j;
}
delegate int D2(int i, int j);//定义委托，有参
static void Main(string[] args)
{
    D2 d2 = F2;/将委托指向F2方法
    Console.WriteLine("计算结果："+d2(1,2));//执行委托=执行方法
}
```
#### 2.1.2 泛型委托Action:无返回值，替代delegate void D1()
```
    Action action = F1;
    action();
```
#### 2.1.3 泛型委托Func：有返回值，替代delegate int D2(int i, int j)
```
    Func<int,int ,int> func = F2;//Func，最后一个参数是返回值类型
    Console.WriteLine("计算结果：" + func(1, 2));
```
#### 2.1.4 匿名委托：将方法直接通过delegate，代替定义方法
```
   Action action1 = delegate ()
   {
       Console.WriteLine("Hello, 匿名委托!");
   };
   action1();

   Action<int, int> action2 = delegate (int n,int i)
   {
       Console.WriteLine($"n={n},i={i}");
   };
   action2(1,2);

   Func<int, int, int> func1 = delegate (int i, int j)
   {
       return i + j;
   };
   Console.WriteLine($"i+j的和："+func1(100,10));
```
#### 2.1.5 匿名方法：写成lambda表达式
```
   //第一步：省略delegate
   Func<int, int, int> func2 = (int i, int j)=>
   {
       return i + j;
   };
   Console.WriteLine($"i+j的和：" + func2(100, 11));
   //第二步：省略值类型
   Func<int, int, int> func3 = (i,j) =>
   {
       return i + j;
   };
   //第二步：只有一行 省略括号和Return
   Func<int, int, int> func3 = (i,j) =>i + j;
```
