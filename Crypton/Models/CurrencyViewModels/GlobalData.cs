using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.CurrencyViewModels
{
    /// <summary>
    /// Model for Global Market Data
    /// </summary>
    public class GlobalData
    {
        public int id { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "Active Currencies")]
        public int activeCurrencies { get; set; }
        /* Number of Active Currencies */

        [Required]
        [Display(Name = "Active Markets")]
        public int activeMarkets { get; set; }
        /* Number of active Markets */

        [Required]
        [Display(Name = "Bitcoin Dominance")]
        [Column(TypeName = "numeric(18,4)")]
        public double bitcoinDominance { get; set; }
        /* Percentage of market owned by Bitcoin */

        [Required]
        [Display(Name = "Total Marketcap")]
        [Column(TypeName = "numeric(18,4)")]
        public double totalMarketCap { get; set; }
        /* Total Marketcap in USD */

        [Required]
        [Display(Name = "Last Updated On")]
        public DateTime lastUpdated { get; set; }
        /* Date on which the data was last updated on */

        [Required]
        [Display(Name = "Provider")]
        public int providerId { get; set; }
        /* Provider from whom the data is fetched */

        [ForeignKey("providerId")]
        public virtual Provider provider { get; set; }
        /* Reference to the Provider model */
    }
}
