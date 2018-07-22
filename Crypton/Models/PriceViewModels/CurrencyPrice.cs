using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.PriceViewModels
{
    /// <summary>
    /// Model fro Currency's Price
    /// </summary>
    public class CurrencyPrice
    {
        [Key]
        public int currencyPriceID { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "Currency")]
        public int currencyID { get; set; }
        /* Currency */

        [Required]
        [Display(Name = "Provider")]
        public int providerID { get; set; }
        /* Provider */

        [Required]
        [Display(Name = "Date")]
        [Column(TypeName = "datetime")]
        public DateTime priceDate { get; set; }
        /* Date */

        [Required]
        [Display(Name = "Price USD")]
        public double priceUSD { get; set; }
        /* Price in USD */

        [Required]
        [Display(Name = "Price Euro")]
        public double priceEuro { get; set; }
        /* Price in Euro */

        [Required]
        [Display(Name = "Price BTC")]
        public double priceBTC { get; set; }
        /* Price in BTC */

        [ForeignKey("currencyID")]
        public virtual CurrencyViewModels.Currency currency { get; set; }
        /* Foreign key for Currency */

        [ForeignKey("providerID")]
        public virtual CurrencyViewModels.Provider provider { get; set; }
        /* Foreign key for Provider */
    }
}
