using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Foundation.SyncItems.DataModels;

namespace Sitecore.Foundation.SyncItems.Models
{
    public class SitecoreProductData : ISitecoreProductData
    {
        public SitecoreProductModel SitecoreProductModel { get; set; }
        public ProductDataContext ProductContext { get; set; }
    }
}