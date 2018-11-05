using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobTrack.Data;
using JobTrack.ViewModels;
using JobTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JobTrack.Controllers
{
    public class JobsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Stores private reference to Identity Framework user manager
        



        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Jobs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.Job.Include(j => j.ApplicationUser).Include(j => j.Company).Include(j => j.Status).Where(j => j.ApplicationUserId == user.Id);
            return View(await applicationDbContext.ToListAsync());

           

        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ApplicationUser)
                .Include(j => j.Company)
                .Include(j => j.Status)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            JobDetailsViewModel JobDetails = new JobDetailsViewModel();
            JobDetails.Job = job;
            return View(JobDetails);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");

            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location");

            JobCreateViewModel JobCreateViewModel = new JobCreateViewModel(_context);
            return View(JobCreateViewModel);

            //ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "Name");
            //return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,Name,Position,DateApplied,StatusId,CompanyId")] Job job)
        {
            ModelState.Remove("ApplicationUserId");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
               
                job.ApplicationUser= user;
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", job.ApplicationUserId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location", job.CompanyId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "Name", job.StatusId);

            JobCreateViewModel JobCreateViewModel = new JobCreateViewModel(_context);
            return View(JobCreateViewModel);
            
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Job = await _context.Job.FindAsync(id);
            if (Job == null)
            {
                return NotFound();
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", Job.ApplicationUserId);

            JobEditViewModel JobEditViewModel = new JobEditViewModel(_context);
            JobEditViewModel.Job = Job;
            return View(JobEditViewModel);

            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location", job.CompanyId);
            //ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "Name", job.StatusId);
            //return View(JobCreateViewModel);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,Name,Position,DateApplied,StatusId,ApplicationUserId,CompanyId")] Job job)
        {
           
            var user = await GetCurrentUserAsync();

            job.ApplicationUser = user;
            _context.Update(job);
            await _context.SaveChangesAsync();


            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            //JobEditViewModel JobEditViewModel = new JobEditViewModel(_context);
            //return base.View(JobEditViewModel);

            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", job.ApplicationUserId);
            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Location", job.CompanyId);
            //ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "Name", job.StatusId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ApplicationUser)
                .Include(j => j.Company)
                .Include(j => j.Status)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }
    }
}
