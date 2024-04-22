using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace _1_异步
{
    internal class Program
    {
        /// <summary>
        /// 异步方法 
        /// 关键字：async，调用时关键字： await
        /// 优点：可以同时处理多个请求
        /// 优先级：同样的功能，既有同步方法又有异步方法，推荐使用异步方法
        /// 无返回值：直接定义Task
        static async Task Main(string[] args)
        {
            //await DownloadHtmlAsync("https://www.baidu.com",@"e:\1.txt");
            //Console.WriteLine("Hello World!");

            Console.WriteLine("Operation completed");
            Console.ReadKey();
        }

        /// <summary>
        /// 异步：将网页内容写到文件中
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        static async Task DownloadHtmlAsync(string url, string filename)
        {
            //HttpClient因为 继承了IDisposable  所以需要回收
            using (HttpClient httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync(url);
                await File.WriteAllTextAsync(filename, html);
            }
        }
        /// <summary>
        /// 同步：同步调异步方法
        /// 问题：这种写法 有死锁的风险
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        static void DownloadHtml(string url, string filename)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<string> html = httpClient.GetStringAsync(url);
                File.WriteAllTextAsync(filename, html.Result).Wait();
            }
        }
        /// <summary>
        /// 异步委托
        /// </summary>
        static void Thread()
        {
            ///正常使用
            ThreadPool.QueueUserWorkItem((obj) => {
                while (true)
                {
                    Console.WriteLine("AAAAAAA");
                }
            });
            //使用异步方法
            ThreadPool.QueueUserWorkItem(async (obj) => {
                while (true)
                {
                    await File.WriteAllTextAsync(@"e:\1.txt", "asdrfasdf");
                    Console.WriteLine("AAAAAAA");
                }
            });
        }
    }
}
