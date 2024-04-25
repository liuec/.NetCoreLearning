using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_线程池和异步
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);//线程池id：1
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append("AAAAAAAAAAA");
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await File.AppendAllTextAsync(@"e:\1.txt",sb.ToString());//线程池id：4，也有一个原因 是File方法内置开辟一个线程池
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
        //由此可见，可以理解为以await 为模块，如果执行时间较长，线程池会被释放处理其他事务。
        //等处理完后，会换新的程序池处理，线程切换 对于系统来说是一个耗费资源的动作
      
    }
}
