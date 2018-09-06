using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Models
{

    public class ProductDataArgs : Sitecore.Pipelines.PipelineArgs
    {
        public IProductData ProductData { get; }
        public Item Item { get; }
        public string FilePath { get; }

        public ProductDataArgs(IProductData productData,string filePath ,Item item)
        {
            Item = item;
            ProductData = productData;
            FilePath = filePath;
        }
    }
}