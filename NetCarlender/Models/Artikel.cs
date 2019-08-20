using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetWebkonzepteUmbreco.Models
{
    [PetaPoco.TableName("Artikel")]
    [PetaPoco.PrimaryKey("idArtikel")]
    public class Artikel
    {
        public long idArtikel { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public decimal Preis { get; set; }
    }
}