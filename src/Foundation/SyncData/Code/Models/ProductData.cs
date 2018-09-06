using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Sitecore.Foundation.SyncItems.Models
{
    public class ProductData : IProductData
    {
        public Item ContextItem { get; set; }
        public ProductModel ProductDataObj { get; set; }
    }
}