using NetWebkonzepteUmbreco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetWebkonzepteUmbreco.Klassen
{
    public class Shop
    {
        public List<Artikel> getAllArtikel()
        {
            PetaPoco.Database db = new PetaPoco.Database("umbShop");
            return db.Query<Artikel>("SELECT * FROM Artikel").ToList();
        }

        public Artikel getArtikelById(string id)
        {
            var db = new PetaPoco.Database("umbShop");
            return db.SingleOrDefault<Artikel>("SELECT * FROM Artikel WHERE idArtikel=@0", id);
        }
    }
}