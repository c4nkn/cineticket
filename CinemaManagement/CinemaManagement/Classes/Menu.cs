using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    // Kullandigim methodlar statik oldugundan dolayi ayrı bir interface olusturmadim.

    public class Menu
    {
        // Ana menuyu yukler. Tekrar kullanimda kolaylik olsun diye method yaptim.
        public static void loadMainMenu()
        {
            Utils.newPage("Main Menu");

            Utils.writeLine($"Welcome!");
            Utils.writeLine($"Use {(char)8593}{(char)8595} and Enter keys for select what you want.");
            Utils.straightLine(52);

            // Options ve menu olusturma
            string[] options = { "Purchase ticket", "List movies", "Exit" };
            int selectedOpt = Utils.createInteractiveMenu(options, 12);

            // Secilen option'a gore yonlendirmeler
            switch (selectedOpt)
            {
                case 0:
                    Reservation reservation = new Reservation();
                    reservation.purchaseTicket();
                    break;
                case 1:
                    listMovies();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Environment.Exit(0);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        // Film bilgilerini gösteren menuyu yukler.
        public static void movieInfoMenu()
        {
            string[] opts = { "Show available sessions", "Back" };
            int selected = Utils.createInteractiveMenu(opts, 18);

            DateTime today = DateTime.Today;

            if (selected == 0)
            {
                Utils.newPage("Available Sessions");

                Reservation reservation = new Reservation();
                reservation.purchaseTicket();
            }
            else if (selected == 1)
            {
                listMovies();
            }
        }

        // Filmleri listeler.
        public static void listMovies()
        {
            Utils.newPage("List movies");
            Utils.writeLine("You can see movie infos by pressing Enter.");
            Utils.straightLine(50);

            // movieInfos statik bir method olmadigindan dolayi tanimlama gerekli.
            Movie movie = new Movie();

            var movieNames = movie.movieInfos().Item1;
            string[] options = { movieNames[0], movieNames[1], movieNames[2], movieNames[3], movieNames[4], "Back" };
            int selectedMovie = Utils.createInteractiveMenu(options, 11);

            // Secilen filme gore film bilgilerini gosterme.
            switch (selectedMovie)
            {
                case 0:
                    movie.getMovieInfo(1);
                    Utils.straightLine(50);
                    movieInfoMenu();
                    break;
                case 1:
                    movie.getMovieInfo(2);
                    Utils.straightLine(50);
                    movieInfoMenu();
                    break;
                case 2:
                    movie.getMovieInfo(3);
                    Utils.straightLine(50);
                    movieInfoMenu();
                    break;
                case 3:
                    movie.getMovieInfo(4);
                    Utils.straightLine(50);
                    movieInfoMenu();
                    break;
                case 4:
                    movie.getMovieInfo(5);
                    Utils.straightLine(50);
                    movieInfoMenu();
                    break;
                case 5:
                    loadMainMenu();
                    break;
                default:
                    // Exception olusturuyoruz, herhangi bir hata vermesini istemeyiz.
                    throw new NotImplementedException();
            }
        }
    }
}
