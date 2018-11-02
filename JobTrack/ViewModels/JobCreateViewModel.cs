using JobTrack.Data;
using JobTrack.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.ViewModels
{
    public class JobCreateViewModel
    {

        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Company { get; set; }

        public JobCreateViewModel(ApplicationDbContext context)
        {
            Company = context.Company.Select(Company =>
           new SelectListItem { Text = Company.Name, Value = Company.CompanyId.ToString() }).ToList();


            Status = context.Status.Select(Status =>
           new SelectListItem { Text = Status.Name, Value = Status.StatusId.ToString() }).ToList();

        }

        public Job Job { get; set; }

     
    }
}
