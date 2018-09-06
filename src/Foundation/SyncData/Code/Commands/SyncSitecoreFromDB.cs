using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Foundation.SyncItems.Commands
{
    using Sitecore.Data;
    using System.Threading;
    using Sitecore.Foundation.SyncItems.Models;
    using Sitecore.Pipelines;

    public class SyncSitecoreFromDB : Command
    {
        public override void Execute(CommandContext context)
        {
            var contextItem = context.Items.FirstOrDefault();
            if (contextItem != null)
            {
                var syncRoot = contextItem.Database.GetItem(new ID(Configuration.Settings.GetSetting("SyncedItemsRoot")));
                Shell.Applications.Dialogs.ProgressBoxes.ProgressBox.Execute("Sitecore Sync", "Running Sitecore Sync Job", RunSyncSitecorePipeline, new[] { contextItem, syncRoot });
            }
        }

        private void RunSyncSitecorePipeline(object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                if(parameters[1] is Item)
                {
                    Thread.Sleep(3000);
                    SitecoreProductDataArgs pipelineArgs = new SitecoreProductDataArgs(new SitecoreProductData(), (Item)parameters[0], (Item)parameters[1]);
                    CorePipeline.Run("syncData.SyncSitecoreFromDB", pipelineArgs);
                }
            }
        }
    }
}