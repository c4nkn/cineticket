using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Classes
{
    // Yine ayni sekilde statik olan methodlari buraya ekledim.
    interface ISession
    {
        string? duration { get; }
        DateTime time { get; }
        string? format { get; }

        static (int, int) createSession(int selectedMovie, DateTime sessionDate)
        {
            Movie movie = new Movie();
            var formats = new List<string> { "Subtitled", "Dubbed" };
            var movieTime = movie.movieInfos().Item4;

            var (sessionTimes, sessionsCount) = adjustSessionTime(movieTime, selectedMovie, sessionDate);
            var sessions = new List<string>();

            // Olabilecek max. session sayisi kadar session olusturur.
            for (int i = 0; i < sessionsCount; i++)
            {
                var session = new Session();
                session.duration = movieTime[selectedMovie].ToString();
                session.format = formats[new Random().Next(0, formats.Count)];
                session.time = sessionTimes[i];

                var timeString = session.time.ToString();

                sessions.Add($"{timeString} ({session.format})");
            }

            // Back optionu ekledim manuel olarak.
            sessions.Add("Back");

            int selectedSession = Utils.createInteractiveMenu(sessions.ToArray(), 14);
            return (selectedSession, sessionsCount);
        }
        static (List<DateTime>, int sessionsCount) adjustSessionTime(int[] movieTime, int selectedMovie, DateTime sessionDate)
        {
            var dateYear = sessionDate.Year;
            var dateMonth = sessionDate.Month;
            var dateDay = sessionDate.Day;

            var sessionTimes = new List<DateTime>();

            var startTime = new DateTime(dateYear, dateMonth, dateDay, 11, 0, 0);
            var endTime = new DateTime(dateYear, dateMonth, dateDay, 23, 50, 0);
            var duration = TimeSpan.FromMinutes(movieTime[selectedMovie] + 20);

            var currentTime = startTime;

            while (currentTime <= endTime)
            {
                sessionTimes.Add(currentTime);
                currentTime = currentTime.Add(duration);
            }

            var sessionsCount = sessionTimes.Count;

            return (sessionTimes, sessionsCount);
        }
    }
    public class Session : ISession
    {
        public Movie? movie { get; set; }
        public Theater? theatre { get; set; }
        public string? duration { get; set; }
        public DateTime time { get; set; }
        public string? format { get; set; }
    }
}
