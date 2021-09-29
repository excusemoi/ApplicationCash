using System;
using System.ComponentModel;
using ApplicationCashImp;


namespace ApplicationCash
{
    class Program
    {
        static void Main(string[] args)
        {
            int key = 0, size = 0, intlifetime = 0;
            string k = "", data = "";
            TimeSpan lifetime;
            try
            {
                Console.WriteLine("Enter lifetime of notes (seconds)");
                int.TryParse(Console.ReadLine(), out intlifetime);    
                lifetime = TimeSpan.FromSeconds(intlifetime);
                Console.WriteLine("Enter max size of cash (amount of notes)");
                int.TryParse(Console.ReadLine(), out size);    
                AppCash<string> app = new AppCash<string>(lifetime, size);
                if ((lifetime == TimeSpan.Zero) || (size <= 0))
                {
                    Console.WriteLine("Incorrect input");
                    return;
                }
                    printInstruction();
                while (key != 5)
                {
                    int.TryParse(Console.ReadLine(), out key);    
                    switch (key)
                    {
                        case 1:
                        {
                            Console.WriteLine("Enter <key>");
                            k = Console.ReadLine();
                            Console.WriteLine("Enter <data>");
                            data = Console.ReadLine();
                            app.Save(k, data);
                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("Enter <data>");
                            data = Console.ReadLine();
                            app.Get(data);
                            break;
                        }
                        case 3:
                        {
                            app.printData();
                            break;
                        }
                        case 4:
                        {
                            printInstruction();
                            break;
                        }
                        case 5:
                        {
                            break;
                        }
                        default: break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void printInstruction()
        {
            Console.WriteLine("1.Save data by command: <key> <data>");
            Console.WriteLine("2.Get data by command: <key>");
            Console.WriteLine("3.View table");
            Console.WriteLine("4.Repeat instruction");
            Console.WriteLine("5.Exit");
        }
        
    }
}