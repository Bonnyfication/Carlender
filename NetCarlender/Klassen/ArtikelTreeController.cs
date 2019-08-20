using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using Umbraco.Core;
using System.Web.Http.ModelBinding;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;
using umbraco.BusinessLogic.Actions;
using umbraco;

namespace NetWebkonzepteUmbreco.Klassen
{
    [Tree("shopSection", "artikelTree", "Artikel")]
    [PluginController("shopSection")]
    public class ArtikelTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {

            var nodes = new TreeNodeCollection();
            var shop = new Shop();

            foreach (var artikel in shop.getAllArtikel())
            {
                var node = CreateTreeNode(artikel.idArtikel.ToString(), "-1", queryStrings, artikel.Bezeichnung);
                
                nodes.Add(node);
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            //var items = new MenuItemCollection();
            //if (id == "-1")
            //{
            //    MenuItem createMenue = new MenuItem();
            //    createMenue = new MenuItem("create", "Artikel hinzufügen");
            //    //createMenue.NavigateToRoute("/shopSection/artikelTree/edit/-1");
            //    createMenue.Icon = "add";
            //    items.Items.Add(createMenue);

            //    return items;
            //}
            //else
            //{
            //    MenuItem deleteMenue = new MenuItem();
            //    deleteMenue = new MenuItem("delete", "Artikel entfernen");
            //    deleteMenue.Icon = "remove";
            //    items.Items.Add(deleteMenue);
            //    return items;
            //}





            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));

                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
            }

            else
            {
                menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));
            }

            return menu;


        }
    }
}