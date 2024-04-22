using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreLearning
{
    internal class Program
    {
        /// <summary>
        /// 异步方法 关键字：async，调用时关键字： await
        /// 异步方法的优点是：可以同时处理多个请求
        /// 同样的功能，既有同步方法又有异步方法，推荐使用异步方法
        static async Task Main(string[] args)
        {
            await DownloadHtmlAsync("https://www.baidu.com",@"e:\1.txt");
            Console.WriteLine("Hello World!");
        }
        static async Task DownloadHtmlAsync(string url,string filename)
        {
            //HttpClient因为 继承了IDisposable  所以需要回收
            using (HttpClient httpClient=new HttpClient())
            {
                string html=await httpClient.GetStringAsync(url);
                await File.WriteAllTextAsync(filename,html);
            }
        }
    }
}
