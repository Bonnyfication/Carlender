using NetWebkonzepteUmbreco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace NetWebkonzepteUmbreco.Controllers
{
    [PluginController("shopSection")]
    public class ArtikelApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<Artikel> GetAll()
        {
            List<Artikel> liste = new List<Artikel>();
            var db = new PetaPoco.Database("umbShop");

            // Show all articles
            foreach (Artikel a in db.Query<Artikel>("SELECT * FROM Artikel"))
            {
                liste.Add(a);
            }
            return liste;
        }

        public Artikel GetById(int id)
        {
            var db = new PetaPoco.Database("umbShop");
            return db.SingleOrDefault<Artikel>("SELECT * FROM Artikel WHERE idArtikel=@0", id);
        }

        public Artikel PostSave(Artikel artikel)
        {
            var db = new PetaPoco.Database("umbShop");
            if (artikel.idArtikel > 0)
            { db.Update(artikel); }
            else
            { db.Insert(artikel); }

            return artikel;
        }

        public int DeleteById(int id)
        {
            var db = new PetaPoco.Database("umbShop");
            return db.Delete<Artikel>("WHERE idArtikel=@0", id);
        }

    }

}