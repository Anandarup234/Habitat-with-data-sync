using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sitecore.Foundation.SyncItems.DataModels
{
    public class Tax
    {
        public int TaxPercentage { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaxTypeId { get; set; }
        //public Guid TaxGuid { get; set; }
        public string TaxTypeName { get; set; }

        //public virtual Category Categories { get; set; }
    }
}