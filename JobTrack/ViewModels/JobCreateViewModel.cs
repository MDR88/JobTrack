using JobTrack.Data;
using JobTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.ViewModels
{
    public class JobCreateViewModel
    {
        private ApplicationDbContext _context;

        public JobCreateViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Job Job { get; set; }

     
    }
}
