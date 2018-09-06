using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Models
{
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SyncItems.DataModels;
    public class SitecoreProductDataArgs : PipelineArgs
    {
        public ISitecoreProductData SitecoreProductData { get; }
        public Item ContextItem { get; }
        public Item SyncRoot { get; }

        public SitecoreProductDataArgs(ISitecoreProductData sitecoreProductData, Item contextItem, Item syncRoot)
        {
            this.SitecoreProductData = sitecoreProductData;
            this.ContextItem = contextItem;
            this.SyncRoot = syncRoot;
        }
    }
}