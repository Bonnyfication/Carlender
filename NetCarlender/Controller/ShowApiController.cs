using NetCarlender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace NetWebkonzepteUmbreco.Controller
{
    [PluginController("showSection")]
    public class ShowApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<Show> GetAll()
        {
            List<Show> liste = new List<Show>();
            var db = new PetaPoco.Database("umbShow");

            // Show all articles
            foreach (Show a in db.Query<Show>("SELECT * FROM Show"))
            {
                liste.Add(a);
            }
            return liste;
        }

        public Show GetById(int id)
        {
            var db = new PetaPoco.Database("umbShow");
            return db.SingleOrDefault<Show>("SELECT * FROM Show WHERE idShow=@0", id);
        }

        public Show PostSave(Show show)
        {
            var db = new PetaPoco.Database("umbShow");
            if (show.idShow > 0)
            { db.Update(show); }
            else
            { db.Insert(show); }

            return show;
        }

        public int DeleteById(int id)
        {
            var db = new PetaPoco.Database("umbShow");
            return db.Delete<Show>("WHERE idShow=@0", id);
        }

    }
}
