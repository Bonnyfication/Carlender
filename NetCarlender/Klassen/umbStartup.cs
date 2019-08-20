using NetWebkonzepteUmbreco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace NetWebkonzepteUmbreco.Klassen
{
    public class umbStartup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            try
            {
                // Abonniere Publish Event
                ContentService.Published += ContentService_Published;

                // Init Test Data
                insertTestData();

                // Umbraco startet
                // Check if Knoten "Hello World" exists
                // If not create knoten
                var contentService = ApplicationContext.Current.Services.ContentService;
                
                //contentService.GetRootContent()
                var homeNode = contentService.GetRootContent().FirstOrDefault(x => x.Name == "Home");
                if (homeNode != null)
                {
                    if (contentService.HasChildren(homeNode.Id))
                    {
                        // Does Hello World exist?
                        if (contentService.GetChildren(homeNode.Id).FirstOrDefault(x => x.Name == "Hello World") == null)
                        {
                            // Knoten does not exist -> create
                            var newPage = contentService.CreateContent("Hello World", homeNode.Id, "unterseite", 0);
                            contentService.SaveAndPublishWithStatus(newPage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Short Error Log: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Long Error: ");
                Console.WriteLine(ex);
            }
        }

        private void ContentService_Published(Umbraco.Core.Publishing.IPublishingStrategy sender, Umbraco.Core.Events.PublishEventArgs<Umbraco.Core.Models.IContent> e)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            const string docType = "unterseite";

            var rootHome = contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias.ToLower() == "home");

            if (rootHome != null)
            {
                // Fuer jede gespeicherte Seite vom Typ Unterseite
                foreach (var node in e.PublishedEntities.Where(x => x.ContentType.Alias.ToLower() == docType))
                {
                    // Für alle gepseicherten Unterseiten behandele nur solche, die neu sind, deren elternelement das root element ist und
                    // deren Validierung erfolgreich war
                    if (node.WasPropertyDirty("Id") && node.ParentId == rootHome.Id && node.IsValid())
                    {
                        // Lege automatisch 2 Unterseiten für die neue Seite an
                        for (int i = 1; i < 3; i++)
                        {
                            var newSubPage = contentService.CreateContent("Unterseite " + i, node.Id, docType, 0);
                            contentService.SaveAndPublishWithStatus(newSubPage);
                        }
                    }

                }
            }
        }

        private void ContentService_Saved(IContentService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IContent> e)
        {
            //var contentService = ApplicationContext.Current.Services.ContentService;
            //const string docType = "unterseite";

            //var rootHome = contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias.ToLower() == "home");

            //if (rootHome != null)
            //{
            //    // Fuer jede gespeicherte Seite vom Typ Unterseite
            //    foreach (var node in e.SavedEntities.Where(x => x.ContentType.Alias.ToLower() == docType))
            //    {
            //        // Für alle gepseicherten Unterseiten behandele nur solche, die neu sind, deren elternelement das root element ist und
            //        // deren Validierung erfolgreich war
            //        if (node.WasPropertyDirty("Id") && node.ParentId == rootHome.Id && node.IsValid())
            //        {
            //            // Lege automatisch 2 Unterseiten für die neue Seite an
            //            for (int i = 1; i < 3; i++)
            //            {
            //                var newSubPage = contentService.CreateContent("Unterseite " + i, node.Id, docType, 0);
            //                contentService.SaveAndPublishWithStatus(newSubPage);
            //            }
            //        }

            //    }
            //}


        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {         
        }


        public void insertTestData()
        {
            Shop s = new Shop();

            // INIT if empty
            if (s.getAllArtikel().Count() == 0)
            {
                var db = new PetaPoco.Database("umbShop");
                for (int i = 0; i < 5; i++)
                {
                    var a = new Artikel();

                    a.Bezeichnung = "Artikel " + i;

                    a.Beschreibung = "Bechreibung Artikel " + i;

                    a.Preis = 10 * i;

                    db.Insert(a);
                }
            }

        }

    }
}