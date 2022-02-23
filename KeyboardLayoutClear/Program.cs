using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardLayoutClear
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");
            var klw = new KeyboardLayoutWatcher();

            klw.KeyboardLayoutChanged += (o, n) =>
            {
                Console.WriteLine($"{o} -> {n}"); // old and new KB layout
            };

            

            Console.ReadKey();
        }
    }
}
