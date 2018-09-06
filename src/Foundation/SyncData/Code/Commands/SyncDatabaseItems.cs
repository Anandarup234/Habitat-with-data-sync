using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Data;
using System.IO;

namespace Sitecore.Foundation.SyncItems.Commands
{
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.SyncItems.Models;
    using Sitecore.Pipelines;
    using System.Threading;

    public class SyncDatabaseItems : Command
    {
        public override void Execute(CommandContext context)
        {
            var contextItem = context.Items.FirstOrDefault();
            if (contextItem != null)
            {
                //Context.ClientPage.ClientResponse.Alert("Testing my button 1");
                var parameters = GetCommandObjects(contextItem);
               
                Shell.Applications.Dialogs.ProgressBoxes.ProgressBox.Execute("DB Sync", "Running Database Sync Job", RunSyncDBPipeline, parameters);
            }
        }
        public object[] GetCommandObjects(Item contextItem)
        {
            Item settingsRoot = contextItem.Database.GetItem(new ID(Configuration.Settings.GetSetting("SyncSettingsFolder")));
            ID settingTemplateID = new ID(Configuration.Settings.GetSetting("LinkTemplateId"));
            string localFilePath = string.Empty, apiUrl = string.Empty;
            if (settingsRoot != null)
            {
                Item apiLinkItem = settingsRoot.Children.Where(x => x.IsDerived(settingTemplateID)).FirstOrDefault();
                if (apiLinkItem != null && apiLinkItem.Fields.Count > 0)
                {
                    if (apiLinkItem.Fields["IsLocalFile"].IsChecked())
                    {
                        localFilePath = apiLinkItem.Fields["LocalFilePath"].Value;
                    }
                    else
                    {
                        apiUrl = apiLinkItem.LinkFieldUrl("Url");
                    }
                }
            }
            return new object[] {localFilePath, apiUrl,contextItem };
        }

        public void RunSyncDBPipeline(params object[] parameters)
        {
            string filePath, apiUrl;
            if (parameters == null)
                return;
            Thread.Sleep(3000);
            if(!string.IsNullOrEmpty(parameters[0].ToString()))
            {
                filePath = parameters[0].ToString();
                if (File.Exists(filePath))
                {
                    ProductDataArgs productArgs = new ProductDataArgs(new ProductData(), filePath, (Item)parameters[2]);
                    CorePipeline.Run("SyncData.SyncDBFromApi", productArgs);
                }
                else
                {
                    Context.ClientPage.ClientResponse.Alert("No File found at the path. Check sitecore item.");
                }
            }
        }
    }
}