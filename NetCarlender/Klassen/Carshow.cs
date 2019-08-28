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

    }
}