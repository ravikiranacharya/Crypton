using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.PriceViewModels
{
    /// <summary>
    /// Model for Variation
    /// </summary>
    public class Variation
    {
        public int variationID { get; set; }
        /* Identity Key*/

        [Required]
        [Display(Name = "Variation Type")]
        [Column(TypeName = "varchar(50)")]
        public string variationType { get; set; } 
        /* Upward trend in percentage, Downward trend in percentage, Upward trend in price, Downward trend in price */
        
    }
}
