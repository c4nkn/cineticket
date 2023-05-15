using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    // Bu class icin interface ekleme geregi duymadim.
    // Tamamen ise yarayacak olan methodlari buraya yazdim.
    // Static oldugundan ve cok kullanildigindan dolayi interface kullanmadim.
    public class Utils
    {
        public static void newPage(string title)
        {
            Console.Clear();
            Console.Title = "CineTicket - " + title;
            writeLogo();
        }

        public static void straightLine(int width)
        {
            Console.WriteLine(new string('─', width));
        }

        public static void writeLine(string text)
        {
            Console.WriteLine("  " + text);
        }

        public static void writeLogo()
        {
            string logo = @"
                     _   ver1.0   _   _      _        _   
                 ___(_)_ __   ___| |_(_) ___| | _____| |_ 
                / __| | '_ \ / _ \ __| |/ __| |/ / _ \ __|
               | (__| | | | |  __/ |_| | (__|   <  __/ |_ 
                \___|_|_| |_|\___|\__|_|\___|_|\_\___|\__|
              
                                      developed by c4nkn
            ";

            Console.WriteLine(logo);
        }

        public static int createInteractiveMenu(string[] options, int cursorPositionY)
        {
            bool isSelected = false;
            int selectedOpt = 0;
            string prefix;

            do
            {
                // Belirli satirdan baslatiyoruz ki duzgun yazdirsin.
                Console.SetCursorPosition(0, cursorPositionY);

                for (int i = 0; i < options.Length; i++)
                {
                    string currentOpt = options[i];
                    
                    // Renk belirleme
                    if (i == selectedOpt)
                    {
                        if (options[i] == "Exit" || options[i] == "Back") // Ozel option
                        {
                            prefix = "<";
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            prefix = ">";
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                    }
                    else
                    {
                        prefix = " ";
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    // Yazdirma
                    if (options[i] == "Exit" || options[i] == "Back")
                    {
                        Console.WriteLine($"\n{prefix} {currentOpt}");
                    }
                    else
                    {
                        Console.WriteLine($"{prefix} {currentOpt}");
                    }

                    Console.ResetColor();
                }

                var pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    selectedOpt--;

                    // Limitlendirme (eger en bastaysa yukariya tekrar bastiginda en sona yolla, binevi loop)
                    if (selectedOpt == -1)
                    {
                        selectedOpt = options.Length - 1;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    selectedOpt++;

                    // Limitlendirme (eger sonuncuya geldiyse en basa yolla tekrar bastiginda)
                    if (selectedOpt == options.Length)
                    {
                        selectedOpt = 0;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    isSelected = true;
                }
            } while (!isSelected);

            return selectedOpt;
        }

        public static int ticketSelection()
        {
            int numberOfTickets = 0;
            bool isSelected = false;

            do
            {
                Console.SetCursorPosition(0, 12);
                Console.Write($"  Number of tickets: {numberOfTickets:D2}\n");

                var pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.UpArrow)
                { 
                    Console.SetCursorPosition(30, 12);
                    Console.Write(new string(' ', "(Choose minimum 1 ticket!)".Length));
                    Console.SetCursorPosition(0, 12);


                    if (numberOfTickets == 35)
                    {
                        numberOfTickets = 0;
                    }
                    else
                    {
                        numberOfTickets++;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(30, 12);
                    Console.Write(new string(' ', "(Choose minimum 1 ticket!)".Length));
                    Console.SetCursorPosition(0, 12);

                    if (numberOfTickets == 0)
                    {
                        numberOfTickets = 35;
                    }
                    else
                    {
                        numberOfTickets--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    if (numberOfTickets == 0)
                    {
                        Console.SetCursorPosition(30, 12);
                        Console.Write("(Choose minimum 1 ticket!)");
                    }
                    else
                    {
                        isSelected = true;
                    }
                }
            } while (!isSelected);

            return numberOfTickets;
        }
    }
}
