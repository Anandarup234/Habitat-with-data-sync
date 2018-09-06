using Sitecore.Foundation.SyncItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Pipelines.SyncSitecoreFromDB
{
    using Sitecore.Data;
    using Sitecore.Foundation.SyncItems.DataModels;
    using Sitecore.Foundation.SyncItems.Models;
    using Sitecore.Foundation.SyncItems.Repositories;
    public class SyncProducts
    {

        public void Process(SitecoreProductDataArgs sitecoreProductDataArgs)
        {
            ProductDataContext productContext = new ProductDataContext();
            if (productContext.Products == null)
                sitecoreProductDataArgs.AbortPipeline();
            if (sitecoreProductDataArgs.SyncRoot == null)
                sitecoreProductDataArgs.AbortPipeline();

            CopyToSitecoreRepository sitecoreRepo = new CopyToSitecoreRepository(productContext, sitecoreProductDataArgs.SitecoreProductData);

            var productsFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("ProductsFolder"))).FirstOrDefault();
            var categoriesFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("CategoryTypesFolder"))).FirstOrDefault();
            var foodTypesFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("FoodTypesFolder"))).FirstOrDefault();

            if (categoriesFolder == null)
                sitecoreProductDataArgs.AbortPipeline();

            sitecoreRepo.SyncProductsIntoSitecore(productsFolder, categoriesFolder, foodTypesFolder);
        }
    }
}