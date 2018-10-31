using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.Models
{
    public class Job
    {

        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(25), Display (Name ="Company Name")]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateApplied{ get; set; }

        [Required]
        public int StatusId { get; set; }

        public Status Status { get; set; }

      
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
