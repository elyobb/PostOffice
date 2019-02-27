using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostOffice.Shared.Models;

namespace PostOffice.Controllers
{
    public class AccountActivitiesController : Controller
    {
        private readonly PostOfficeDbContext _context;

        public AccountActivitiesController(PostOfficeDbContext context)
        {
            _context = context;
        }

        // GET: AccountActivities
        public async Task<IActionResult> Index()
        {
            var postOfficeDbContext = _context.AccountActivity.Include(a => a.Account).Include(a => a.Copy);
            return View(await postOfficeDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> GetAccountActivityList(int accountId)
        {
            return Json(await _context.AccountActivity.Where(a => a.AccountId == accountId).ToListAsync());
        }

        // GET: AccountActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity
                .Include(a => a.Account)
                .Include(a => a.Copy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountActivity == null)
            {
                return NotFound();
            }

            return View(accountActivity);
        }

        // GET: AccountActivities/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            ViewData["CopyId"] = new SelectList(_context.Copy, "Id", "Id");
            return View();
        }

        // POST: AccountActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,CopyId")] AccountActivity accountActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", accountActivity.AccountId);
            ViewData["CopyId"] = new SelectList(_context.Copy, "Id", "Id", accountActivity.CopyId);
            return View(accountActivity);
        }

        // GET: AccountActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity.FindAsync(id);
            if (accountActivity == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", accountActivity.AccountId);
            ViewData["CopyId"] = new SelectList(_context.Copy, "Id", "Id", accountActivity.CopyId);
            return View(accountActivity);
        }

        // POST: AccountActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,CopyId")] AccountActivity accountActivity)
        {
            if (id != accountActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountActivityExists(accountActivity.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", accountActivity.AccountId);
            ViewData["CopyId"] = new SelectList(_context.Copy, "Id", "Id", accountActivity.CopyId);
            return View(accountActivity);
        }

        // GET: AccountActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity
                .Include(a => a.Account)
                .Include(a => a.Copy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountActivity == null)
            {
                return NotFound();
            }

            return View(accountActivity);
        }

        // POST: AccountActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountActivity = await _context.AccountActivity.FindAsync(id);
            _context.AccountActivity.Remove(accountActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountActivityExists(int id)
        {
            return _context.AccountActivity.Any(e => e.Id == id);
        }
    }
}
