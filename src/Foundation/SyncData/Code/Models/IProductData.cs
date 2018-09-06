using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.SyncItems.Models
{
    public interface IProductData
    {
        Item ContextItem { get; set; }
        ProductModel ProductDataObj { get; set; }
    }
}
