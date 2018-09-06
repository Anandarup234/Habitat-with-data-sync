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

    public class SyncCategories
    {
        public void Process(SitecoreProductDataArgs sitecoreProductDataArgs)
        {
            ProductDataContext productContext = new ProductDataContext();
            if (productContext.Categories == null)
                sitecoreProductDataArgs.AbortPipeline();
            if (sitecoreProductDataArgs.SyncRoot == null)
                sitecoreProductDataArgs.AbortPipeline();

            CopyToSitecoreRepository sitecoreRepo = new CopyToSitecoreRepository(productContext, sitecoreProductDataArgs.SitecoreProductData);

            var categoriesFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("CategoryTypesFolder"))).FirstOrDefault();
            var taxFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("TaxTypesFolder"))).FirstOrDefault();
            if (categoriesFolder == null)
                sitecoreProductDataArgs.AbortPipeline();

            sitecoreRepo.SyncCategoriesIntoSitecore(categoriesFolder, taxFolder);
        }
    }
}