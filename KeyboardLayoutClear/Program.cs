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
            long targetLayout = 134809609;

            Console.WriteLine("Try to remove: " + targetLayout);

            var klm = new KeyboardLayoutManager();

            var layouts = klm.GetAllKeyboardLayout();
            Console.WriteLine(string.Join("\n", layouts.Select(l=>$"{l.Id} Lang:{l.LanguageName} Kboard:{l.KeyboardName}")));
            while (layouts.Any(l=>l.Id == targetLayout))
            {
                if (klm.TryToUnloadLayout(targetLayout))
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Failure");
                }
            }

            Console.WriteLine("Complete");
            Console.ReadKey();
        }
    }
}
