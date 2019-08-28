using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCarlender.Models
{
    [PetaPoco.TableName("Show")]
    [PetaPoco.PrimaryKey("idShow")]
    public class Show
    {
        public long idShow { get; set; }
        public DateTime Datum { get; set; }
        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public string Ort { get; set; }
        public string Plz { get; set; }
        public string Straße { get; set; }
        public int Image { get; set; }
        public string Facebook { get; set; }
    }
}