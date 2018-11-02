using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.Models
{
    public class Status
    {

        [Key]
        public int StatusId { get; set; }


        [Required, Display(Name = "Job Status")]
        public string Name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
