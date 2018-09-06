using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Repositories
{
    using Sitecore.Foundation.SyncItems.DataModels;
    using Sitecore.Foundation.SyncItems.Models;
    using FoodType = DataModels.FoodType;
    using Product = DataModels.Product;
    using Tax = DataModels.Tax;

    public class CopyToDBRepository
    {
        public IProductData productData;
        public ProductDataContext dataContext;
        public CopyToDBRepository(IProductData productData, ProductDataContext dataContext)
        {
            this.productData = productData;
            this.dataContext = dataContext;
        }

        public IEnumerable<FoodType> GetFoodTypes()
        {
            List<FoodType> foodTypes = new List<FoodType>();
            FoodType foodTypeObj = null;
            foreach (Models.FoodType foodType in productData.ProductDataObj?.FoodTypes)
            {
                if (dataContext.FoodTypes.Any(x => x.FoodTypeValue == foodType.FoodTypeValue))
                {
                    var tempFoodType = dataContext.FoodTypes.Where(x => x.FoodTypeValue == foodType.FoodTypeValue).FirstOrDefault();
                    dataContext.FoodTypes.Remove(tempFoodType);
                    dataContext.SaveChanges();
                }
                foodTypeObj = new FoodType();
                PropertyCopier<Models.FoodType, FoodType>.Copy(foodType, foodTypeObj);
                //foodTypeObj.FoodTypeGuid = new Guid();
                foodTypes.Add(foodTypeObj);
            }
            return foodTypes;
        }

        public IEnumerable<Tax> GetTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            Tax taxObj = null;
            foreach (Models.Tax tax in productData.ProductDataObj?.TaxTable)
            {
                if (dataContext.Taxes.Any(x => x.TaxTypeId == tax.TaxTypeId))
                {
                    var temptaxType = dataContext.Taxes.Where(x => x.TaxTypeId == tax.TaxTypeId).FirstOrDefault();
                    dataContext.Taxes.Remove(temptaxType);
                    dataContext.SaveChanges();
                }
                taxObj = new Tax();
                PropertyCopier<Models.Tax, Tax>.Copy(tax, taxObj);
                //taxObj.TaxGuid = new Guid();
                taxes.Add(taxObj);

            }
            return taxes;
        }

        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            Category categoryObj = null;
            foreach (Models.CategoryList category in productData.ProductDataObj?.CategoryList)
            {
                if (dataContext.Categories.Any(x => x.CategoryId == x.CategoryId))
                {
                    var tempCategory = dataContext.Categories.Where(x => x.CategoryId == x.CategoryId).FirstOrDefault();
                    dataContext.Categories.Remove(tempCategory);
                    dataContext.SaveChanges();
                }
                categoryObj = new Category();
                PropertyCopier<Models.CategoryList, Category>.Copy(category, categoryObj);
                //categoryObj.CategoryGUID = new Guid();
                categories.Add(categoryObj);
            }
            return categories;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Product productObj = null;
            foreach (Models.Product product in productData.ProductDataObj?.ProductList)
            {
                if (dataContext.Products.Any(x => x.ProductId == x.ProductId))
                {
                    var tempProduct = dataContext.Products.Where(x => x.ProductId == x.ProductId).FirstOrDefault();
                    dataContext.Products.Remove(tempProduct);
                    dataContext.SaveChanges();
                }
                productObj = new Product();
                PropertyCopier<Models.Product, Product>.Copy(product, productObj);
                productObj.ProductGuid = Guid.NewGuid();
                products.Add(productObj);
            }
            return products;
        }
    }



    public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
    {
        public static void Copy(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();
            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.PropertyType == childProperty.PropertyType && parentProperty.Name.Equals(childProperty.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
    }

}