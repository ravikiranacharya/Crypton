using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.AlertViewModels
{
    /// <summary>
    /// Model for Alert types
    /// </summary>
    public class AlertType
    {
        public int alertTypeID { get; set; }
        /* Identity Key */

        [Required]
        [Display(Name = "Alert Type")]
        [Column(TypeName = "varchar(50)")]
        public string alertType { get; set; }
        /* Can be email alerts, sms alerts, push notifications, browser notifications etc., */

        [Required]
        [Display(Name = "Enabled?")]
        public bool isEnabled { get; set; }
        /* Configure if this alert type is enabled or disabled */
    }
}
