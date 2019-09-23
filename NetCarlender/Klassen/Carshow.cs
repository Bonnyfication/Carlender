using NetCarlender.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NetCarlender.Klassen
{
    public class Carshow
    {
        /// <summary>
        /// returns all carshows
        /// </summary>
        /// <returns></returns>
        public List<Show> getShows()
        {
            List<Show> liste = new List<Show>();
            var db = new PetaPoco.Database("umbShow");

            // Load all shows
            foreach (Show show in db.Query<Show>("SELECT * FROM Show"))
            {
                liste.Add(show);
            }

            return liste;
        }

        /// <summary>
        /// returns all carshows that are comming soon
        /// </summary>
        /// <returns></returns>
        public List<Show> getShowsCurrent()
        {
            List<Show> liste = new List<Show>();
            var db = new PetaPoco.Database("umbShow");

            // Load all shows
            foreach (Show show in db.Query<Show>("SELECT * FROM Show WHERE Datum >= GETDATE()"))
            {
                liste.Add(show);
            }

            return liste.OrderBy(x => x.Datum).ToList();
        }

        /// <summary>
        /// returns all carshows that already took place
        /// </summary>
        /// <returns></returns>
        public List<Show> getShowsArchiv()
        {
            List<Show> liste = new List<Show>();
            var db = new PetaPoco.Database("umbShow");

            // Load all shows
            foreach (Show show in db.Query<Show>("SELECT * FROM Show WHERE Datum < GETDATE()"))
            {
                liste.Add(show);
            }

            return liste.OrderByDescending(x => x.Datum).ToList();
        }

        /// <summary>
        /// returns the show by id
        /// </summary>
        /// <returns></returns>
        public Show getShowById(string id)
        {
            int check = 0;
            int showId = 0;
            if (Int32.TryParse(id, out check))
            {
                showId = Int32.Parse(id);
            }
            else
            {
                return null;
            }

            List<Show> liste = new List<Show>();
            var db = new PetaPoco.Database("umbShow");

            // Load all shows
            foreach (Show show in db.Query<Show>("SELECT * FROM Show"))
            {
                liste.Add(show);
            }

            return liste.FirstOrDefault(x => x.idShow == showId);
        }

        /// <summary>
        /// Gibt alle shows zurück bei denen titel oder beschreibung auf die suchanfrage passen
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Show> getShowsBySearch(string query)
        {
            List<Show> result = new List<Show>();
            List<Show> shows = getShows();

            // Liste der einzelnen search queries
            List<string> splittedquery = query.Split(' ').ToList();

            foreach (string request in splittedquery)
            {
                DateTime O_DATE;
                
                if (DateTime.TryParseExact(request, 
                    "dd.MM.yyyy", 
                    CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out O_DATE))
                {
                    shows.ForEach(x =>
                    {
                        Console.WriteLine(x.Datum.ToString("dd.MM.yyyy"));
                        if (x.Datum.ToString("dd.MM.yyyy") == request)
                        {
                            if (result.FirstOrDefault(y => y.idShow == x.idShow) == null)
                            {
                                result.Add(x);
                            }
                        }
                    });
                    continue;
                }
                

                // Postleizahl
                System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(@"^\d{5}$");
                if (pattern.IsMatch(request))
                {
                    shows.ForEach(x =>
                    {
                        if (x.Plz == request)
                        {
                            if (result.FirstOrDefault(y => y.idShow == x.idShow) == null)
                            {
                                result.Add(x);
                            }
                        }
                    });
                    continue;
                }

                // Prüfe gegen rest
                shows.ForEach(x =>
                {
                    if (x.Titel.ToLower().Contains(request.Trim().ToLower()) || x.Beschreibung.ToLower().Contains(request.Trim().ToLower()) || x.Zusammenfassung.ToLower().Contains(request.Trim().ToLower()) || x.Ort.ToLower().Equals(request.Trim().ToLower()) || x.Strasse.ToLower().Equals(request.Trim().ToLower()))
                    {
                        if (result.FirstOrDefault(y => y.idShow == x.idShow) == null)
                        {
                            result.Add(x);
                        }
                    }
                });
            }

            return result;
        }
    }
}