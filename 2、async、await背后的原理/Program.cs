using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace async_await背后的原理
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //从编译器中可以看出，微软将 异步方法 切割成多个块
            //块1
            using (HttpClient httpClient=new HttpClient())
            {
                string htmk = await httpClient.GetStringAsync("http://www.taobao.com");
                Console.WriteLine(htmk);
            }
            //块2
            string txt = "hello lyc";
            string filename = @"e:\1.txt";
            await File.WriteAllTextAsync(filename, txt);
            Console.WriteLine("写入成功");
            //块3
            string s = await File.ReadAllTextAsync(filename);
            Console.WriteLine("Hello World!");
            //在程序执行过程中，看似等待 实则没有等待，是不停的
        }
    }
}
