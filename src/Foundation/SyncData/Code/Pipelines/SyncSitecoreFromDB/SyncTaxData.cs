using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data;
namespace Sitecore.Foundation.SyncItems.Pipelines.SyncSitecoreFromDB
{
    using Sitecore.Foundation.SyncItems.DataModels;
    using Sitecore.Foundation.SyncItems.Models;
    using Sitecore.Foundation.SyncItems.Repositories;

    public class SyncTaxData
    {
        public void Process(SitecoreProductDataArgs sitecoreProductDataArgs)
        {
            ProductDataContext productContext = new ProductDataContext();
            if (productContext.Taxes == null)
                sitecoreProductDataArgs.AbortPipeline();
            if (sitecoreProductDataArgs.SyncRoot == null)
                sitecoreProductDataArgs.AbortPipeline();
           
            CopyToSitecoreRepository sitecoreRepo = new CopyToSitecoreRepository(productContext, sitecoreProductDataArgs.SitecoreProductData);

            var taxFolder = sitecoreProductDataArgs.SyncRoot.Children.Where(x => x.ID == new ID(Configuration.Settings.GetSetting("TaxTypesFolder"))).FirstOrDefault();
            if (taxFolder == null)
                sitecoreProductDataArgs.AbortPipeline();

            sitecoreRepo.SyncTaxItemsIntoSitecore(taxFolder);
        }
    }
}