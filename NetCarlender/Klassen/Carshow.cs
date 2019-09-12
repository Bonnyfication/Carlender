using NetCarlender.Models;
using System;
using System.Collections.Generic;
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

            result = shows.Where(x => x.Titel.ToLower().Contains(query.Trim().ToLower()) || x.Beschreibung.ToLower().Contains(query.Trim().ToLower()) || x.Zusammenfassung.ToLower().Contains(query.Trim().ToLower())).ToList();

            return result;
        }
    }
}