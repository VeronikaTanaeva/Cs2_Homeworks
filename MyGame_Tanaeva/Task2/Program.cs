using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {

        static Dictionary<T, int> ElementsCount<T>(List<T> list)
        {
            Dictionary<T, int> elements = new Dictionary<T, int>();
            foreach(var e in list)
            {
                int i = 0;
                foreach (var g in list)
                {
                    if (g.Equals(e))
                        i++;
                }
                if (elements.ContainsKey(e)) continue;
                elements.Add(e, i);
            }
            return elements;
        }

        static Dictionary<T, int> ElementsCountLinq<T>(List<T> list)
        {
            Dictionary<T, int> elements = new Dictionary<T, int>();
            var elems = from n in list
                        where list.Contains(n) && !elements.ContainsKey(n)
                        select n;
            foreach (var e in elems)
            {
                if(elements.ContainsKey(e)) continue;
                elements.Add(e, elems.Count());
            }                    

            return elements;
        }

        static void PrintResult<T>(Dictionary<T, int> elements)
        {
            foreach (var e in elements)
                Console.WriteLine("Элемент {0} встречается {1} раз", e.Key, e.Value);
        }

        static void Main(string[] args)
        {
            List<int> MyList = new List<int> { 0, 2, 0, 1, 2, 2, 3 };
            Dictionary<int, int> MyDict = ElementsCount<int>(MyList);
            Console.WriteLine("Через foreach:\n");
            PrintResult(MyDict);
            Console.WriteLine("\nЧерез LINQ:\n");
            Dictionary<int, int> MyDict2 = ElementsCountLinq<int>(MyList);
            PrintResult(MyDict2);
            Console.ReadKey();
        }
    }
}
