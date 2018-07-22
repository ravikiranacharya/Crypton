using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Models.AlertViewModels
{
    /// <summary>
    /// Model for Alert Logs
    /// </summary>
    public class Log
    {
        public int logID { get; set; }
        /* Identity Key */
        
        [Display(Name = "Status")]
        [Column(TypeName = "varchar(50)")]
        public string status { get; set; }
        /* Status of Alert */

        [Display(Name = "Description")]
        [Column(TypeName = "varchar(250)")]
        public string description { get; set; }
        /* Log description */

        [Display(Name = "Log Date")]
        [Column(TypeName = "datetime")]
        public DateTime logDate { get; set; }
        /* Creation time of Log */

        [Required]
        [Display(Name = "Alert")]
        public int alertID { get; set; }
        /* Alert*/

        [Required]
        [Display(Name = "Alert Type")]
        public int alertTypeID { get; set; }
        /* Alert Type */

        [ForeignKey("alertID")]
        public virtual AlertViewModels.Alert alert { get; set; }
        /* Foreign key for Alert */

        [ForeignKey("alertTypeID")]
        public virtual AlertViewModels.AlertType alertType { get; set; }
        /* Foreign key for Alert Type */
    }
}
