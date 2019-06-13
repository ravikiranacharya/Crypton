using Crypton.Models.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.Arbitrage
{
    public class Market
    {
        [Key]
        public int marketId { get; set; }

        [Required]
        [Display(Name = "Exchange")]
        public int exchangeId { get; set; }

        [Required]
        [Display(Name = "Source Currency")]
        public int sourceCurrencyId { get; set; }

        [Required]
        [Display(Name = "Target Currency")]
        public int targetCurrencyId { get; set; }

        [Required]
        [Display(Name = "Active Status")]
        [Column(TypeName = "bit")]
        public bool activeStatus { get; set; }

        [ForeignKey("exchangeId")]
        public virtual Exchange exchange { get; set; }

        [ForeignKey("sourceCurrencyId")]
        public virtual Currency sourceCurrency { get; set; }

        [ForeignKey("targetCurrencyId")]
        public virtual Currency targetCurrency { get; set; }
    }
}
