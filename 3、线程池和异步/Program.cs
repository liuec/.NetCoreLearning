using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_线程池和异步
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append("AAAAAAAAAAA");
            }
            await File.AppendAllTextAsync(@"e:\1.txt",sb.ToString());
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
    }
}
