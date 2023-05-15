using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    // Interface disarisinda 'static' methodlari birbirleriyle eslestiremedigimden
    // surekli Reservation reservation = new Reservation() yapmamak icin
    // butun methodlari Interface icerisine aldim.
    interface IReservation
    {
        int ticketPrice { get; }
        int numberOfTickets { get; }
        string? seatNo { get; }

        void purchaseTicket();

        // Kullanicidan bilet sayisi ve isim soy isim bilgilerini alir.
        static void ticketInfo()
        {
            Utils.newPage("Enter Information");
            Utils.writeLine($"Select number of tickets by using {(char)8593}{(char)8595} and press Enter");
            Utils.writeLine("Then fill 'Full name' section and press Enter again.");
            Utils.straightLine(57);

            var numberOfTickets = Utils.ticketSelection();
            var personNames = new List<string>();

            for (int i = 1; i < numberOfTickets + 1; i++)
            {
                Console.Write($"  Full name (ticket {i}): ");
                string? enteredName = Console.ReadLine();
                
                // personName null dondurunce uyari vermesini istemedim.
                if (string.IsNullOrEmpty(enteredName))
                {
                    personNames.Add($"Person {i}");
                } else
                {
                    personNames.Add(enteredName);
                }
            }

            movieSelection(numberOfTickets, personNames);
        }

        // Kullanicidan film secmesini ister.
        static void movieSelection(int numberOfTickets, List<string> personNames)
        {
            Utils.newPage("Select Movie");
            Utils.writeLine("Choose what movie you want to go.");
            Utils.straightLine(45);

            Movie movie = new Movie();

            var movieNames = movie.movieInfos().Item1;
            string[] options = { movieNames[0], movieNames[1], movieNames[2], movieNames[3], movieNames[4], "Back" };
            int selectedMovie = Utils.createInteractiveMenu(options, 11);

            switch (selectedMovie)
            {
                case int n when n <= 4:
                    showAvailableSessions(selectedMovie, numberOfTickets, personNames);
                    break;
                case int n when n > 4:
                    Menu.loadMainMenu();
                    break;
            }
        }

        // Her filme ozel sessionları listeler, kullaniciya sectirir.
        static void showAvailableSessions(int selectedMovie, int numberOfTickets, List<string> personNames)
        {
            Utils.newPage("Available Sessions");
            Utils.writeLine("Choose available session for you.");
            Utils.writeLine($"You can select date by using {(char)8593}{(char)8595} keys.");
            Utils.writeLine("For cancel press 'Escape'.");
            Utils.straightLine(50);

            bool isSelected = false;

            DateTime date = DateTime.Today; // Bugunden baslatiyorum tarihi.

            do
            {
                Console.SetCursorPosition(0, 13);
                Console.Write("  Date: {0}", date.ToShortDateString());

                var pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    date = date.AddDays(1);
                }
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (date != DateTime.Today) // Eger secili tarih bugune esitse geriye donemez.
                    {
                        date = date.AddDays(-1);
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    // Secilen zamana gore sessionlari listeler.
                    (int selectedSession, int sessionsCount) = ISession.createSession(selectedMovie, date);

                    switch (selectedSession)
                    {
                        case int n when n < sessionsCount: 
                            showAvailableSeats(selectedMovie, numberOfTickets, personNames);
                            break;
                        case int n when n >= sessionsCount: // Geri donus
                            showAvailableSessions(selectedMovie, numberOfTickets, personNames);
                            break;
                    }

                    isSelected = true;
                }
                else if (pressedKey.Key == ConsoleKey.Escape) // Geri donus
                {
                    Console.WriteLine("Returning.."); // Konsoldaki hata sebebiyle boyle bir yol denedim calisti. 
                    movieSelection(numberOfTickets, personNames);
                }
            } while (!isSelected);
        }

        // O seansa ait koltuklari gosterir.
        static void showAvailableSeats(int selectedMovie, int numberOfTickets, List<string> personNames)
        {
            Utils.newPage("Select Seat");
            Utils.writeLine("Please, choose your seat.");
            Utils.writeLine($"You can use {(char)8596}{(char)8597} (arrow keys) for selecting seats.");
            Utils.straightLine(60);

            Theater theater = new Theater();
            theater.showSeats(55, numberOfTickets, selectedMovie, personNames);
        }

        // Theater class'indan verileri daha duzgun bir sekilde cekebilmeyi saglar.
        static void seatsSelected(int selectedMovie, List<string> selectedSeats, int numberOfTickets, string theaterName, List<string> personNames)
        {
            showSummary(selectedMovie, selectedSeats, numberOfTickets, theaterName, personNames);
        }

        // Bilet bilgilerini gosterir.
        static void showSummary(int selectedMovie, List<string> selectedSeats, int numberOfTickets, string theaterName, List<string> personNames)
        {
            Utils.newPage("Receipt");

            Movie movie = new Movie();

            var movieNames = movie.movieInfos().Item1;
            var movieGenres = movie.movieInfos().Item2;
            var movieRelDates = movie.movieInfos().Item3;
            var movieTimes = movie.movieInfos().Item4;
            var movieRates = movie.movieInfos().Item5;
            var movieAgeRates = movie.movieInfos().Item6;

            Utils.writeLine("Ticket receipt:");
            Utils.straightLine(60);
            Utils.writeLine("Selected movie: " + movieNames[selectedMovie]);
            Utils.writeLine($"Time: {movieTimes[selectedMovie].ToString()} minutes");
            Utils.writeLine($"Theater: {theaterName}");

            // Koltuklar bir liste icerisinde oldugundan dolayi her birini ayri ayri almak gerekiyor.
            Utils.writeLine($"Selected seat(s): ");
            for (int i = 0; i < selectedSeats.Count; i++)
            {
                Utils.writeLine($"- [{selectedSeats[i]}]({personNames[i]})");
            }

            Utils.straightLine(60);
            confirmation(numberOfTickets);
        }

        // Bileti onaylayip onaylamadigini sorar, onayliyorsa programi execute eder.
        // Onaylamiyorsa biraz bekletip ana menuye dondurur. 
        static void confirmation(int numberOfTickets)
        {
            Console.SetCursorPosition(0, 16 + numberOfTickets);
            Console.Write("  Do u want to confirm? (Y/n):");
            var answer = Console.ReadLine();

            if (answer != null)
            {
                if (answer == "y" || answer == "Y")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Utils.writeLine("Ticket successfully created. Please, save seat number(s). Ticket number: 23986.");
                    Console.ResetColor();
                    Utils.writeLine("Returning main menu... (20s)");
                    Thread.Sleep(20000);
                    Menu.loadMainMenu();
                }
                else if (answer == "n" || answer == "N")
                {
                    Console.WriteLine();
                    Utils.writeLine("Returning main menu. Please wait...");
                    Thread.Sleep(3000);
                    Menu.loadMainMenu();
                }
            }
            else
            {
                Console.SetCursorPosition(0, 16 + numberOfTickets);
                Console.Write("  Do u want to confirm? (Y/n):");
                answer = Console.ReadLine();
            }
        }
    }

    public class Reservation : IReservation
    {
        public Session? session { get; set; }
        public Movie? movie { get; set; }
        public Theater? theatre { get; set; }
        public int ticketPrice { get; set; }
        public int numberOfTickets { get; set; }
        public string? seatNo { get; set; }

        // Bunu ekleme geregi duymadim.
        public void purchaseTicket()
        {
            Utils.newPage("Processing...");
            IReservation.ticketInfo();
        }
    }
}
