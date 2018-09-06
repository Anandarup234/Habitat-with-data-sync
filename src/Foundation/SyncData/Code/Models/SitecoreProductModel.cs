using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Models
{
    public class SitecoreProductModel
    {
        public List<CategoryModel> CategoryList { get; set; }
        public List<FoodTypeModel> FoodTypes { get; set; }
        public List<ProductsModel> ProductList { get; set; }
        public List<TaxModel> TaxTable { get; set; }
    }

    public class TaxModel
    {
        public int TaxTypeId { get; set; }
        public int TaxPercentage { get; set; }
        public string TaxTypeName { get; set; }
        public Guid TaxTypeGuid { get; set; }
    }

    public class ProductsModel
    {
        public Guid ProductGuid { get; set; }
        public Guid Category { get; set; }
       
        public Guid FoodType{ get; set; }
 
        public int ProductId { get; set; }
 
        public string ProductDescription { get; set; }

        public string ProductImgUrl { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
    }

    public class FoodTypeModel
    {
        public string FoodTypeName { get; set; }
        public int FoodTypeValue { get; set; }
        public Guid FoodTypeGuid { get; set; }
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid TaxTypeId { get; set; }
        public Guid CategoryGuid { get; set; }
    }
}