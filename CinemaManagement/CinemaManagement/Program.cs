using System;
using System.Net.NetworkInformation;
using CinemaManagement.Classes;

namespace CinemaManagement
{
    class Program
    {
        // Kodu parcaladigim, class ve methodlara boldugum icin burada bisi yok.
        public static void Main(string[] args)
        {
            Console.Title = "CineTicket";
            Utils.writeLogo();
            Menu.loadMainMenu();
        }
    }
}
