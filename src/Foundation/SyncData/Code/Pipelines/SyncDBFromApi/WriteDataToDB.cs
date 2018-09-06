using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Pipelines.SyncDBFromApi
{
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.SyncItems.Models;
    using Sitecore.Foundation.SyncItems.DataModels;
    using Sitecore.Foundation.SyncItems.Repositories;

    public class WriteDataToDB
    {
        public void Process(ProductDataArgs productArgs)
        {
            ProductDataContext productContext = new ProductDataContext();
            if (productArgs.ProductData.ProductDataObj == null)
                productArgs.AbortPipeline();
            var productObj = productArgs.ProductData;

            CopyToDBRepository dbRepo = new CopyToDBRepository(productObj, productContext);

            productContext.FoodTypes.AddRange(dbRepo.GetFoodTypes());
            productContext.SaveChanges();

            productContext.Taxes.AddRange(dbRepo.GetTaxes());
            productContext.SaveChanges();

            productContext.Categories.AddRange(dbRepo.GetCategories());
            productContext.SaveChanges();

            productContext.Products.AddRange(dbRepo.GetProducts());
            productContext.SaveChanges();
        }
    }
}