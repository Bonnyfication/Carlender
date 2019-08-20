using NetWebkonzepteUmbreco.Klassen;
using NetWebkonzepteUmbreco.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetWebkonzepteUmbreco.Handler
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für getArtikel
    /// </summary>
    public class getArtikel : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "JSON";
            Shop shop = new Shop();
            List<Artikel> artikel = shop.getAllArtikel();
            var json = JsonConvert.SerializeObject(artikel);
            context.Response.Write(json.ToString());
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}