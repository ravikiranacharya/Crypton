using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.Arbitrage
{
    public class Exchange
    {
        public int exchangeId { get; set; }

        [Required]
        [Display(Name = "Exchange Name")]
        [Column(TypeName = "varchar(100)")]
        public string exchangeName { get; set; }

        [Required]
        [Display(Name = "Active Status")]
        [Column(TypeName = "bit")]
        public bool activeStatus { get; set; }
    }
}
