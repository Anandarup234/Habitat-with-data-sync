using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sitecore.Foundation.SyncItems.DataModels
{
    public class Product
    {
        [Key]
        [Column(Order = 1)]
        public Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImgUrl { get; set; }
        public int ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public int FoodTypeValue { get; set; }

        public virtual FoodType FoodType { get; set; }
        public Category Categories { get; set; }
    }
}