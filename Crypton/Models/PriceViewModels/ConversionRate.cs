using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.PriceViewModels
{
    /// <summary>
    /// Model for Conversion Rate
    /// </summary>
    public class ConversionRate
    {
        [Key]
        public int conversionID { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "Source Currency")]
        public int? sourceCurrencyID { get; set; }
        /* Source Currency */

        [Required]
        [Display(Name = "Target Currency")]
        public int? targetCurrencyID { get; set; }
        /* Target Currency */

        [Required]
        [Display(Name = "Conversion Rate")]
        public double conversionRate { get; set; }
        /* Type of Conversion */

        [Required]
        [Display(Name = "Date")]
        [Column(TypeName = "datetime")]
        public DateTime conversionDate { get; set; }
        /* Date */

        [ForeignKey("sourceCurrencyID")]
        public virtual CurrencyViewModels.Currency sourceCurrency { get; set; }
        /* Foreign key for Source Currency */

        [ForeignKey("targetCurrencyID")]        
        public virtual CurrencyViewModels.Currency targetCurrency { get; set; }
        /* Foreign key for Target Currency */
    }
}
