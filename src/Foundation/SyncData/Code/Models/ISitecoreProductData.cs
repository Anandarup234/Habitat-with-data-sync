using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.SyncItems.Models
{
    using Sitecore.Foundation.SyncItems.DataModels;
    public interface ISitecoreProductData
    {
        SitecoreProductModel SitecoreProductModel { get; set; }
        ProductDataContext ProductContext { get; set; }
    }
}
