using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _5_CancellationToken
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //正常请求
            //await DownloadAsync("https://www.baidu.com",10000);

            //请求设置中断，设置如果10秒没有处理完，就中断
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(10000);
            //方法一：IsCancellationRequested，判断是否中断，可以方便用户操作
            await DownloadAsync("https://www.baidu.com", 10000, cts.Token);
            //方法二：ThrowIfCancellationRequested()，强制中断，并返回一个Throw错误
            await DownloadAsyncV2("https://www.baidu.com", 10000, cts.Token);
            //方法三：程序支持CancellationToken中断功能
            await DownloadAsyncV3("https://www.baidu.com", 10000, cts.Token);
            //显然方法一更好一点

            Console.WriteLine("Hello World!");
        }
        /// <summary>
        /// 将一个链接的HTML下载N次，
        /// </summary>
        /// <param name="url"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static async Task DownloadAsync(string url,int n)
        {
            using(HttpClient httpClient=new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine(html);
                }
            }
        }
        static async Task DownloadAsync(string url, int n,CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine(html);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("中断");
                        break;
                    }
                }
            }
        }
        static async Task DownloadAsyncV2(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine(html);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
        static async Task DownloadAsyncV3(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var res = await httpClient.GetAsync(url, cancellationToken);
                    string html = await res.Content.ReadAsStringAsync();
                    Console.WriteLine(html);
                }
            }
        }
    }
}
