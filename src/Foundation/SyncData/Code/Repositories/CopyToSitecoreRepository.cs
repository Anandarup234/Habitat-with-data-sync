using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SyncItems.DataModels;
using Sitecore.Foundation.SyncItems.Models;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Repositories
{
    using Sitecore.Foundation.SyncItems.Utilities;
    public class CopyToSitecoreRepository
    {
        ProductDataContext productDataContext;
        ISitecoreProductData sitecoreProductData;
        Item currentItem = null;
        TemplateItem template = null;
        public CopyToSitecoreRepository(ProductDataContext productDataContext, ISitecoreProductData sitecoreProductData)
        {
            this.productDataContext = productDataContext;
            this.sitecoreProductData = sitecoreProductData;
            sitecoreProductData.SitecoreProductModel = new SitecoreProductModel();
        }

        public void SyncTaxItemsIntoSitecore(Item taxFolderItem)
        {
            sitecoreProductData.SitecoreProductModel.TaxTable = productDataContext.Taxes.
                                                                                   Select(x => new TaxModel
                                                                                   {
                                                                                       TaxTypeGuid = Guid.NewGuid(),
                                                                                       TaxPercentage = x.TaxPercentage,
                                                                                       TaxTypeId = x.TaxTypeId,
                                                                                       TaxTypeName = x.TaxTypeName
                                                                                   }).ToList();
            using (new SecurityDisabler())
            {
                template = GetTemplateItem("Tax");
                foreach (var taxItem in sitecoreProductData.SitecoreProductModel.TaxTable)
                {
                    if (taxFolderItem.Children.Any(x => x.Fields["TaxTypeId"].Value.Equals(taxItem.TaxTypeId.ToString())))
                    {
                        currentItem = taxFolderItem.Children.Where(x => x.Fields["TaxTypeId"].Value.Equals(taxItem.TaxTypeId.ToString())).FirstOrDefault();
                    }
                    else
                    {
                        currentItem = taxFolderItem.Add(taxItem.TaxTypeName, template);
                    }

                    currentItem.Editing.BeginEdit();
                    try
                    {
                        currentItem.PopulateItemFieldValue(taxItem);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message, e);
                    }
                    finally
                    {
                        currentItem.Editing.EndEdit();
                    }
                }
            }
        }

        public void SyncFoodTypesIntoSitecore(Item foodTypesFolderItem)
        {
            sitecoreProductData.SitecoreProductModel.FoodTypes = productDataContext.FoodTypes.Select(x => new FoodTypeModel
            {
                FoodTypeName = x.FoodTypeName,
                FoodTypeValue = x.FoodTypeValue,
                FoodTypeGuid = Guid.NewGuid()
            }).ToList();

            using (new SecurityDisabler())
            {
                template = GetTemplateItem("FoodType");
                foreach (var foodType in sitecoreProductData.SitecoreProductModel.FoodTypes)
                {
                    if (foodTypesFolderItem.Children.Any(x => x.Fields["FoodTypeValue"].Value.Equals(foodType.FoodTypeValue.ToString())))
                    {
                        currentItem = foodTypesFolderItem.Children.Where(x => x.Fields["FoodTypeValue"].Value.Equals(foodType.FoodTypeValue.ToString())).FirstOrDefault();
                    }
                    else
                    {
                        currentItem = foodTypesFolderItem.Add(foodType.FoodTypeName, template);
                    }

                    currentItem.Editing.BeginEdit();
                    try
                    {
                        currentItem.PopulateItemFieldValue(foodType);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message, e);
                    }
                    finally
                    {
                        currentItem.Editing.EndEdit();
                    }
                }
            }
        }

        public void SyncCategoriesIntoSitecore(Item categoriesFolderItem, Item taxFolderItem)
        {
            
            sitecoreProductData.SitecoreProductModel.CategoryList = productDataContext.Categories.ToList().Select(x => new CategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                TaxTypeId = taxFolderItem.Children.Where(y => y.Fields["TaxTypeId"].Value.Equals(x.TaxTypeId.ToString())).FirstOrDefault().ID.Guid,
                CategoryGuid = Guid.NewGuid()
            }).ToList();

            using (new SecurityDisabler())
            {
                template = GetTemplateItem("Category");
                foreach (var category in sitecoreProductData.SitecoreProductModel.CategoryList)
                {
                    if (categoriesFolderItem.Children.Any(x => x.Fields["CategoryId"].Value.Equals(category.CategoryId.ToString())))
                    {
                        currentItem = categoriesFolderItem.Children.Where(x => x.Fields["CategoryId"].Value.Equals(category.CategoryId.ToString())).FirstOrDefault();
                    }
                    else
                    {
                        currentItem = categoriesFolderItem.Add(category.CategoryName, template);
                    }

                    currentItem.Editing.BeginEdit();
                    try
                    {
                        currentItem.PopulateItemFieldValue(category);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message, e);
                    }
                    finally
                    {
                        currentItem.Editing.EndEdit();
                    }
                }
            }
        }

        public void SyncProductsIntoSitecore(Item productsFolderItem, Item categoriesFolderItem, Item foodTypesFolderItem)
        {

            sitecoreProductData.SitecoreProductModel.ProductList = productDataContext.Products.ToList().Select(x => new ProductsModel
            {
                ProductId = x.ProductId,
                ProductGuid = x.ProductGuid,
                ProductDescription = x.ProductDescription,
                ProductImgUrl = x.ProductImgUrl,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                Category = categoriesFolderItem.Children.Where(y=> y.Fields["CategoryId"].Value.Equals(x.CategoryId.ToString())).FirstOrDefault().ID.Guid,
                FoodType = foodTypesFolderItem.Children.Where(y=> y.Fields["FoodTypeValue"].Value.Equals(x.FoodTypeValue.ToString())).FirstOrDefault().ID.Guid
            }).ToList();

            using (new SecurityDisabler())
            {
                template = GetTemplateItem("Product");
                foreach (var product in sitecoreProductData.SitecoreProductModel.ProductList)
                {
                    if (productsFolderItem.Children.Any(x => x.Fields["ProductId"].Value.Equals(product.ProductId.ToString())))
                    {
                        currentItem = productsFolderItem.Children.Where(x => x.Fields["ProductId"].Value.Equals(product.ProductId.ToString())).FirstOrDefault();
                    }
                    else
                    {
                        currentItem = productsFolderItem.Add(product.ProductName, template);
                    }

                    currentItem.Editing.BeginEdit();
                    try
                    {
                        currentItem.PopulateItemFieldValue(product);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message, e);
                    }
                    finally
                    {
                        currentItem.Editing.EndEdit();
                    }
                }
            }
        }
        public TemplateItem GetTemplateItem(string type)
        {
            Database masterDb = Configuration.Factory.GetDatabase("master");
            TemplateItem template = null;
            switch (type)
            {
                case "Tax":
                    template = masterDb.GetTemplate(new ID(Configuration.Settings.GetSetting("TaxTemplate")));
                    break;
                case "FoodType":
                    template = masterDb.GetTemplate(new ID(Configuration.Settings.GetSetting("FoodTypeTemplate")));
                    break;
                case "Category":
                    template = masterDb.GetTemplate(new ID(Configuration.Settings.GetSetting("CategoryTemplate")));
                    break;
                case "Product":
                    template = masterDb.GetTemplate(new ID(Configuration.Settings.GetSetting("ProductTemplate")));
                    break;
                default:
                    template = null;
                    break;
            }
            return template;
        }
    }
}