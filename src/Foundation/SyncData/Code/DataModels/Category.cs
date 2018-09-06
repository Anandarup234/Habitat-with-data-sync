using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sitecore.Foundation.SyncItems.DataModels
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        //public Guid CategoryGUID { get; set; }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [ForeignKey("Tax")]
        public int TaxTypeId { get; set; }
        //[Required]
        public virtual Tax Tax { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}