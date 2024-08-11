using System;

namespace _7_从委托到Lamdba
{
    class Program
    {
        static void Main(string[] args)
        {
            //Linq的推演过程：委托>Lambda>linq
            //委托：将方法当作变量传递，委托指向方法
            D1 d = F1;
            d();
            D2 d2 = F2;
            Console.WriteLine("计算结果："+d2(1,2));


            //泛型委托Action 无返回值
            Action action = F1;
            action();
            //泛型委托Func 有返回值
            Func<int,int ,int> func = F2;
            Console.WriteLine("计算结果：" + func(1, 2));

            //匿名委托
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

            //匿名委托简化：写成lambda表达式
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
            Console.WriteLine($"i+j的和：" + func3(100, 12));
            //第三步：省略值类型
            Func<int, int, int> func4 = (i, j) => i + j;
            Console.WriteLine($"i+j的和：" + func3(100, 12));
        }
        static void F1()
        {
            Console.WriteLine("Hello, World!");
        }
        static int F2(int i,int j)
        {
            return i + j;
        }
        delegate void D1();
        delegate int D2(int i, int j);
    }
}
