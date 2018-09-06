using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SyncItems.Utilities
{
    public static class ItemExtension
    {
        public static Item PopulateItemFieldValue(this Item currentItem, object obj)
        {
            var objProperties = obj.GetType().GetProperties();
            if (objProperties != null && objProperties.Any())
            {
                foreach (var property in objProperties)
                {
                    Guid guid;
                    if(Guid.TryParse(property.GetValue(obj).ToString(), out guid))
                    {
                        currentItem.Fields[property.Name].Value = guid.ToString("B").ToUpper();
                    }
                    else
                    {
                        currentItem.Fields[property.Name].Value = property.GetValue(obj).ToString();
                    }
                }
            }
            return currentItem;
        }
    }
}