using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.CurrencyViewModels
{
    /// <summary>
    /// Model for Currency
    /// </summary>
    public class Currency
    {
        public int currencyID { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "Currency ID")]
        [Column(TypeName = "varchar(10)")]
        public string providerCurrencyID { get; set; }
        /* Provider's ID for this Currency. Ideally, this should be currencyCode but there can be cases where multiple currencies have the same short code. */

        [Required]
        [Display(Name = "Currency's Name")]
        [Column(TypeName = "varchar(50)")]
        public string currencyName { get; set; }
        /* Name of Currency */

        [Required]
        [Display(Name = "Currency Code")]
        [Column(TypeName = "varchar(10)")]
        public string currencyCode { get; set; }
        /* Currency's short code */

        [Display(Name = "Description")]
        [Column(TypeName = "varchar(250)")]
        public string description { get; set; }
        /* Description about the Currency */

        [Display(Name = "Total Volume")]
        public double totalVolume { get; set; }
        /* Total volume of the currency */

        [Display(Name = "Current Volume")]
        public double currentVolume { get; set; }
        /* Current volume of the currency */

        [Display(Name = "Current Market Cap")]
        public double currentMarketCap { get; set; }
        /* Current market cap of the Currency */

        [Display(Name = "Official Website")]
        [Column(TypeName = "varchar(50)")]
        public string officialWebsite { get; set; }
        /* Currency's official website */

        [Display(Name = "Rank")]
        public int marketRank { get; set; }
        /* Currency's current rank in the market with respect to market cap*/

        [Display(Name = "Last Updated On")]
        public DateTime lastUpdated { get; set; }
        /* Time of last updation of data */

        public bool isEnabled { get; set; }
        /* Configure if this Currency is enabled or disabled */

        [Column(TypeName = "varchar(50)")]
        public string logoPath { get; set; }
        /* Local path to the currency's logo */

        [Required]
        [Display(Name = "Provider")]
        public int providerID { get; set; }
        /* Provider from whom the data is fetched */

        [ForeignKey("providerID")]
        public virtual Provider provider { get; set; }
        /* Reference to the Provider model */
    }
}
