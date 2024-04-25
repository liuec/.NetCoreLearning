using System;
using System.IO;
using System.Threading.Tasks;

namespace _4_async为什么非必须_
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string a =await  ReaadAsync(0);
            Console.WriteLine(a);
            string b = await ReaadAsync2(1);
            Console.WriteLine(b);
        }
        static async Task<string> ReaadAsync(int num)
        {
            if (num == 0)
            {
                string a = await File.ReadAllTextAsync(@"e:\1.txt");
                return a;
            }

            else if (num == 1)
            {
                return await File.ReadAllTextAsync(@"e:\1.txt");
            }
            else
            {
                throw new ArgumentException();
            }
        }
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
    }
}
