using System;
using System.Collections.Generic;

namespace _8_Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 1, 2, 3 ,111,4,555,11,77,9};
            IEnumerable<int> intlist = IWhere(ints, i => i > 10);
            foreach (int i in intlist)
            {
                Console.WriteLine(i);
            }
        }
        /// <summary>
        /// 普通的Linq语句
        /// </summary>
        static IEnumerable<int> IWhere(IEnumerable<int> items,Func<int,bool> f)
        {
            List<int>  list = new List<int>();
            foreach (var item in items)
            {
                if (f(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// yield Linq语句，该方法可以流水化，一边判断，一边执行
        /// </summary>
        static IEnumerable<int> IWhere2(IEnumerable<int> items, Func<int, bool> f)
        {
            foreach (var item in items)
            {
                if (f(item))
                {
                    yield return item;
                }
            }
        }
    }
}
