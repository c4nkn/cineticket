using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    interface IMovie
    {
        string? name { get; }
        string? genre { get; }
        DateTime releaseDate { get; }
        int time { get; }
        float? imdbRating { get; }
        string? ageRating { get; }
        (string[], string[], DateTime[], int[], float?[], string[]) movieInfos();
        void getMovieInfo(int movieNumber);
    }

    public class Movie : IMovie
    {
        public Session? session { get; set; }
        public string? name { get; set; }
        public string? genre { get; set; }
        public DateTime releaseDate { get; set; }
        public int time { get; set; }
        public float? imdbRating { get; set; }
        public string? ageRating { get; set; }

        // Film bilgilerini return methoduyla baska methodlarda kullanmak uzere donduruyor.
        public (string[], string[], DateTime[], int[], float?[], string[]) movieInfos()
        {
            Movie movie1 = new Movie();
            movie1.name = "Guardians of the Galaxy Vol.3";
            movie1.genre = "Action, Sci-Fi, Adventure, Comedy";
            movie1.releaseDate = new DateTime(2022, 4, 22, 0, 0, 0); //April 22, 2023
            movie1.time = 150;
            movie1.imdbRating = 8.4F;
            movie1.ageRating = "13+";

            Movie movie2 = new Movie();
            movie2.name = "John Wick 4";
            movie2.genre = "Action, Crime, Thriller";
            movie2.releaseDate = new DateTime(2023, 03, 22, 0, 0, 0); //March 22, 2023
            movie2.time = 169;
            movie2.imdbRating = 8.2F;
            movie2.ageRating = "R";

            Movie movie3 = new Movie();
            movie3.name = "Thor: Love and Thunder";
            movie3.genre = "Action, Adventure, Comedy";
            movie3.releaseDate = new DateTime(2022, 07, 06, 0, 0, 0); //July 6, 2022
            movie3.time = 118;
            movie3.imdbRating = 6.2F;
            movie3.ageRating = "13+";

            Movie movie4 = new Movie();
            movie4.name = "The Menu";
            movie4.genre = "Comedy, Horror, Thriller";
            movie4.releaseDate = new DateTime(2022, 11, 18, 0, 0, 0); //November 18, 2022
            movie4.time = 107;
            movie4.imdbRating = 7.2F;
            movie4.ageRating = "16+";

            Movie movie5 = new Movie();
            movie5.name = "Babylon";
            movie5.genre = "Comedy, Drama, History";
            movie5.releaseDate = new DateTime(2023, 01, 20, 0, 0, 0); //January 20, 2023
            movie5.time = 189;
            movie5.imdbRating = 7.2F;
            movie5.ageRating = "18+";

            string[] movieNames = { movie1.name, movie2.name, movie3.name, movie4.name, movie5.name };
            string[] movieGenres = { movie1.genre, movie2.genre, movie3.genre, movie4.genre, movie5.genre };
            DateTime[] movieRelDate = { movie1.releaseDate, movie2.releaseDate, movie3.releaseDate, movie4.releaseDate, movie5.releaseDate };
            int[] movieTime = { movie1.time, movie2.time, movie3.time, movie4.time, movie5.time };
            float?[] movieRate = { movie1.imdbRating, movie2.imdbRating, movie3.imdbRating, movie4.imdbRating, movie5.imdbRating };
            string[] movieAgeRate = { movie1.ageRating, movie2.ageRating, movie3.ageRating, movie4.ageRating, movie5.ageRating };

            return (movieNames, movieGenres, movieRelDate, movieTime, movieRate, movieAgeRate);
        }

        // Filmleri listeleyecegim zaman kullandigim film bilgileri.
        public void getMovieInfo(int movieNumber)
        {
            var (movieNames, movieGenres, movieRelDate, movieTime, movieRate, movieAgeRate) = movieInfos();

            // Filmlerin tum bilgileri array olarak donduruldugunden dolayi her birine ayni info metni hazirladim.
            string movie1Info = $"  Name: {movieNames[0]}\n  Genre: {movieGenres[0]}\n  Release Date: {movieRelDate[0].ToShortDateString()}\n  Time: {movieTime[0]} minutes\n  Rating (IMDb): {movieRate[0]}\n  Age Rating: {movieAgeRate[0]}";
            string movie2Info = $"  Name: {movieNames[1]}\n  Genre: {movieGenres[1]}\n  Release Date: {movieRelDate[1].ToShortDateString()}\n  Time: {movieTime[1]} minutes\n  Rating (IMDb): {movieRate[1]}\n  Age Rating: {movieAgeRate[1]}";
            string movie3Info = $"  Name: {movieNames[2]}\n  Genre: {movieGenres[2]}\n  Release Date: {movieRelDate[2].ToShortDateString()}\n  Time: {movieTime[2]} minutes\n  Rating (IMDb): {movieRate[2]}\n  Age Rating: {movieAgeRate[2]}";
            string movie4Info = $"  Name: {movieNames[3]}\n  Genre: {movieGenres[3]}\n  Release Date: {movieRelDate[3].ToShortDateString()}\n  Time: {movieTime[3]} minutes\n  Rating (IMDb): {movieRate[3]}\n  Age Rating: {movieAgeRate[3]}";
            string movie5Info = $"  Name: {movieNames[4]}\n  Genre: {movieGenres[4]}\n  Release Date: {movieRelDate[4].ToShortDateString()}\n  Time: {movieTime[4]} minutes\n  Rating (IMDb): {movieRate[4]}\n  Age Rating: {movieAgeRate[4]}";

            // movieNumber'a gore listeliyor.
            switch (movieNumber)
            {
                case 1:
                    Utils.newPage(movieNames[0]);
                    Utils.writeLine("Movie Info");
                    Utils.straightLine(50);
                    Console.WriteLine(movie1Info);
                    break;
                case 2:
                    Utils.newPage(movieNames[1]);
                    Utils.writeLine("Movie Info");
                    Utils.straightLine(50);
                    Console.WriteLine(movie2Info);
                    break;
                case 3:
                    Utils.newPage(movieNames[2]);
                    Utils.writeLine("Movie Info");
                    Utils.straightLine(50);
                    Console.WriteLine(movie3Info);
                    break;
                case 4:
                    Utils.newPage(movieNames[3]);
                    Utils.writeLine("Movie Info");
                    Utils.straightLine(50);
                    Console.WriteLine(movie4Info);
                    break;
                case 5:
                    Utils.newPage(movieNames[4]);
                    Utils.writeLine("Movie Info");
                    Utils.straightLine(50);
                    Console.WriteLine(movie5Info);
                    break;
            }
        }
    }
}
