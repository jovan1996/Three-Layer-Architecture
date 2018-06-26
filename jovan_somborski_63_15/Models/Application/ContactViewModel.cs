using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace jovan_somborski_63_15.Models.Application
{
    public class ContactViewModel
    {
        [Required]
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Message")]
        public string Description { get; set; }

    }
}