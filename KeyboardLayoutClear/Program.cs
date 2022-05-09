using System;
using System.Linq;
using System.Text;

namespace KeyboardLayoutClear
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 ||
                args[0] == "-?" )
            {
                WriteHelp();
                return;
            }

            var klm = new KeyboardLayoutManager();
            switch (args[0])
            {
                case "-s":
                    ShowAllLayouts(klm);
                    break;
                case "-r":
                    if (args.Length > 1)
                    {
                        foreach (var idString in args.Skip(1))
                        {
                            if (long.TryParse(idString, out var id))
                            {
                                RemoveLayout(klm, id);
                            }
                        }
                    }
                    else
                        WriteHelp();
                    break;
                default:
                    WriteHelp();
                    break;
            }

            return;
        }

        private static void RemoveLayout(KeyboardLayoutManager klm, long targetLayout)
        {
            Console.Write("Try to remove: " + targetLayout + " ..... ");
            while (klm.GetAllKeyboardLayout().Any(l => l.Id == targetLayout))
            {
                if (klm.TryToUnloadLayout(targetLayout))
                    Console.WriteLine("Success");
                else
                    Console.WriteLine("Failure");
            }
        }

        private static void ShowAllLayouts(KeyboardLayoutManager klm)
        {
            Console.WriteLine($"Id \t\t Language \t\t Keyboard");

            foreach (var l in klm.GetAllKeyboardLayout())
            {
                Console.WriteLine($"{l.Id} \t {l.LanguageName} \t {l.KeyboardName}");
            }
        }

        private static void WriteHelp()
        {
            var builder = new StringBuilder()
                .AppendLine("Use one of arguments below:")
                .AppendLine("\t-s - show all layouts")
                .AppendLine("\t-r <id> - remove layouts with id")
                .AppendLine("\t-? or other - show this help");
            
                
            Console.WriteLine(builder);
        }
    }
}
