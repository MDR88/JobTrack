using JobTrack.Data;
using JobTrack.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTrack.ViewModels
{
    public class ContactCreateViewModel
    {

        public List<SelectListItem> Company { get; set; }

        public ContactCreateViewModel(ApplicationDbContext context)
        {
            Company = context.Company.Select(Company =>
           new SelectListItem { Text = Company.Name, Value = Company.CompanyId.ToString() }).ToList();


          

        }
        public Contact Contact { get; set; }

    }
}
