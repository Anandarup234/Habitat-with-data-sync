using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Sitecore.Foundation.SyncItems.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FoodType
    {
        public FoodType()
        {
            this.Products = new HashSet<Product>();
        }
        [MaxLength(50)]
        [Required]
        public string FoodTypeName { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodTypeValue { get; set; }
        //public Guid FoodTypeGuid { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}