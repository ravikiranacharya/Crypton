using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.CurrencyViewModels
{
    /// <summary>
    /// Model for Providers
    /// </summary>
    public class Provider
    {
        public int providerID { get; set; }
        /* Identity key */

        [Required]
        [Display(Name = "Provider's Name")]
        [Column(TypeName = "varchar(50)")]
        public string providerName { get; set; }
        /* Provider's name. For example: CoinMarketCap */

        [Required]
        [Display(Name = "Provider's Website")]
        [Column(TypeName = "varchar(50)")]
        public string providerWebsite { get; set; }
        /* Full address of Provider's website. For example: https://coinmarketcap.com */

        [Required]
        [Display(Name = "API URL")]
        [Column(TypeName = "varchar(50)")]
        public string apiUrl { get; set; }
        /* Full address to Provider's API. For example: https://api.coinmarketcap.com/v2/ */

        [Display(Name = "API Key")]
        [Column(TypeName = "varchar(250)")]
        public string apiKey { get; set; }
        /* API key */

        [Display(Name = "Enabled?")]
        public bool isEnabled { get; set; }
        /* Configure if this Provider is enabled or disabled */
    }
}
