using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    interface ITheater
    {
        string? theaterName { get; }
        int capacity { get; }

        void createTheaters();
        void showSeats(int capacity, int numberOfTickets, int selectedMovie, List<string> personNames);
    }

    public class Theater : ITheater
    {
        public string? theaterName { get; set; }
        public int capacity { get; set; }

        public void createTheaters()
        {
            Theater theater = new Theater();
            theater.theaterName = "Theater";
            theater.capacity = 55;
        }

        public void showSeats(int capacity, int numberOfTickets, int selectedMovie, List<string> personNames)
        {
            string screen = @"       ████████████████████████████████████████████████████";
            string doors = @"[ <Door                                                    Door> ]";
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };

            int rowSize = 5;
            int columnSize = 11;
            string theaterName = "";

            Random random = new Random();

            bool isSelected = false;
            var selectedSeats = new List<bool[,]>();
            int seatsCount = numberOfTickets;

            bool[,] seats = new bool[rowSize, columnSize];

            List<string> finalSeats = new List<string>();

            int selectedRow = 0, selectedColumn = 0;

            if (capacity == 55)
            {
                theaterName = "Theater " + letters[random.Next(0, letters.Length)];

                for (int i = 0; i < random.Next(6, 24); i++)
                {
                    seats[random.Next(0, rowSize), random.Next(0, columnSize)] = true;
                }

                Console.WriteLine($"{screen}\n\n{doors}");

                do
                {
                    for (int i = 0; i < rowSize; i++)
                    {
                        Console.WriteLine("\n");
                        Console.SetCursorPosition(11, 18 + i);

                        for (int j = 0; j < columnSize; j++)
                        {
                            if (i == selectedRow && j == selectedColumn)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("[x] ");
                            }
                            else if (seats[i, j])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("[▬] ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("[_] ");
                            }
                        }

                        Console.ResetColor();
                    }

                    var pressedKey = Console.ReadKey();

                    if (pressedKey.Key == ConsoleKey.UpArrow && selectedRow > 0)
                    {
                        selectedRow--;
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow && selectedRow < rowSize - 1)
                    {
                        selectedRow++;
                    }
                    else if (pressedKey.Key == ConsoleKey.LeftArrow && selectedColumn > 0)
                    {
                        selectedColumn--;
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow && selectedColumn < columnSize - 1)
                    {
                        selectedColumn++;
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        if (seats[selectedRow, selectedColumn])
                        {
                            Console.SetCursorPosition(0, 20 + rowSize);
                            Console.WriteLine("Selected seat is taken. Please choose a new empty seat.");
                            Console.ReadKey();
                        }
                        else
                        {
                            selectedSeats.Add(new bool[,] { { seats[selectedRow, selectedColumn] } });
                            seats[selectedRow, selectedColumn] = true;
                            seatsCount--;
                            finalSeats.Add($"{letters[selectedRow]}{selectedColumn + 1}");

                            if (seatsCount == 0)
                            {
                                isSelected = true;
                            }
                        }
                    }
                } while (!isSelected);

                IReservation.seatsSelected(selectedMovie, finalSeats, numberOfTickets, theaterName, personNames);
            } 
        }
    }
}
