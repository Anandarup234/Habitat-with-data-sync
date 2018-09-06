using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Sitecore.Foundation.SyncItems.Models
{
    public class ProductModel
    {
        public List<CategoryList> CategoryList { get; set; }
        [JsonProperty("FoodType")]
        public List<FoodType> FoodTypes { get; set; }
        [JsonProperty("ListofProducts")]
        public List<Product> ProductList { get; set; }
        public List<Tax> TaxTable { get; set; }
    }
    public class Tax
    {
        public int TaxTypeId { get; set; }
        public int TaxPercentage { get; set; }
        public string TaxTypeName { get; set; }
    }
    public class Product
    {
        public int CategoryId { get; set; }
        [JsonProperty("foodType")]
        public int FoodTypeValue { get; set; }
        [JsonProperty("id")]
        public int ProductId { get; set; }
        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }
        [JsonProperty("productimgurl")]
        public string ProductImgUrl { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
    }

    public class FoodType
    {
        public string FoodTypeName { get; set; }
        public int FoodTypeValue { get; set; }
    }

    public class CategoryList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TaxTypeId { get; set; }
    }
}