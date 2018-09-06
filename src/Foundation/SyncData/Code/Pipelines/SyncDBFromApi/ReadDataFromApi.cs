using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Pipelines.SyncDBFromApi
{
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.SyncItems.Models;
    using System.IO;
    using Newtonsoft.Json;

    public class ReadDataFromApi
    {
        public void Process(ProductDataArgs productArgs)
        {
            if (string.IsNullOrEmpty(productArgs.FilePath))
                productArgs.AbortPipeline();

            string fileText = File.ReadAllText(productArgs.FilePath);
            try
            {
                productArgs.ProductData.ProductDataObj = JsonConvert.DeserializeObject<ProductModel>(fileText);
            }
            catch (JsonException je)
            {
                Log.Error(je.Message, je, typeof(ProductDataArgs));
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e, typeof(ProductDataArgs));
            }
        }
    }
}