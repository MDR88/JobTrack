﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

    }
}
