using NetCarlender.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.ModelBinding;
using umbraco.businesslogic;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using umbraco;

namespace NetWebkonzepteUmbreco.Trees
{
    [umbraco.businesslogic.Tree("showSection", "showTree", "Show")]
    [PluginController("showSection")]
    public class ShowTreeController : TreeController
    {

        protected override MenuItemCollection GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if(id == Constants.System.Root.ToInvariantString())
            {
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias));
            }
            else
            {
                menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));
            }

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            if(id == Constants.System.Root.ToInvariantString())
            {
                var nodes = new TreeNodeCollection();
                var carshow = new Carshow();

                foreach (var show in carshow.getShows())
                {
                    var node = CreateTreeNode(show.idShow.ToString(), "-1", queryStrings, show.Titel);

                    nodes.Add(node);
                }

                return nodes;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}