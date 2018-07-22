using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Crypton.Models.AlertViewModels
{
    /// <summary>
    /// Model for Alerts
    /// </summary>
    public class Alert
    {
        public int alertID { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "User")]
        public int userID { get; set; }
        /* User's ID */

        [Required]
        [Display(Name = "Limit")]
        public double limit { get; set; }
        /* Selected limit on price */

        [Display(Name = "Created On")]
        public DateTime createdOn { get; set; }
        /* Creation date for alert */

        [Display(Name = "Fulfilled?")]
        public bool isFulfilled { get; set;  }
        /* Flag to check if the alert is fulfilled */

        [Display(Name = "Fulfilled On")]
        [Column(TypeName = "datetime")]
        public DateTime fulfilledOn { get; set; }
        /* Time at which the alert is fulfilled */

        [Required]
        [Display(Name = "Conversion Type")]
        public int conversionID { get; set; }
        /* Conversion Type. Examples: Price-based (or) Percentage-based */

        [Required]
        [Display(Name = "Variation Type")]
        public int variationID { get; set; }
        /* Variation Type. Example: Uppward trend (or) Downward trend */

        [Required]
        [Display(Name = "Alert Type")]
        public int alertTypeID { get; set; }
        /* Alert Type */

        [ForeignKey("conversionID")]
        public virtual PriceViewModels.ConversionRate conversionRate { get; set; }
        /* Foreign key for Conversion rate */

        [ForeignKey("variationID")]
        public virtual PriceViewModels.Variation variation { get; set; }
        /* Foreign key for variation type */

        [ForeignKey("alertTypeID")]
        public virtual AlertViewModels.AlertType alertType { get; set; }
        /* Foreign key for alert type */

    }
}
