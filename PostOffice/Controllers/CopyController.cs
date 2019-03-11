    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostOffice.Shared.Models;
using PostOffice.ViewModels;

namespace PostOffice.Controllers
{
    public class CopyController : Controller
    {
        private readonly PostOfficeDbContext _context;

        public CopyController(PostOfficeDbContext context)
        {
            _context = context;
        }

        // GET: Copy
        public async Task<IActionResult> Index()
        {
            List<Copy> copies = await _context.Copy.ToListAsync();
            foreach(Copy c in copies)
            {
                c.PostItem = await _context.PostItems.FirstOrDefaultAsync(post => post.Id == c.PostItemId);
            }
            return View(copies);
        }

        // GET: Copy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copy = await _context.Copy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (copy == null)
            {
                return NotFound();
            }

            return View(copy);
        }

        // GET: Copy/Create
        public IActionResult Create()
        {
            var posts = _context.PostItems.Select(p => p.Url);
            var viewModel = new CopyViewModel
            {
                Urls = new SelectList(posts)
            };
            return View(viewModel);
        }

        // POST: Copy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CopyViewModel copyViewModel)
        {
            var postItem =  _context.PostItems.FirstOrDefault(p => p.Url == copyViewModel.Url);
            Copy copy = new Copy
            {
                Text = copyViewModel.Text.Trim().Replace("  "," "),
                PostItem = postItem
            };
            if (ModelState.IsValid)
            {
                _context.Add(copy);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(copy);
        }

        // POST: Copy/5/PostOnAccount/7
        [HttpPost]
        public async Task<IActionResult> MarkPosted(int? copyId, int? accountId, bool checkedState)
        {
            if (copyId == null || accountId == null)
            {
                return NotFound();
            }

            var copy = _context.Copy.FirstOrDefault(c => c.Id == copyId);
            var account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);

            AccountActivity accountActivity = new AccountActivity();
            accountActivity.AccountId = account.Id;
            accountActivity.Account = account;
            accountActivity.CopyId = copy.Id;
            accountActivity.Copy = copy;

            if (ModelState.IsValid)
            {
                AccountActivitiesController accountActivityController = new AccountActivitiesController(_context);
                if(checkedState)
                {
                    return await accountActivityController.Create(accountActivity);
                }
                else
                {
                    var existingActivity = _context.AccountActivity.FirstOrDefault(activity => activity.AccountId == account.Id && activity.CopyId == copy.Id);
                    return await accountActivityController.DeleteConfirmed(existingActivity.Id);
                }
            }
            return Json("model state invalid: copyController#markPosted");
        }

        // GET: Copy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copy = await _context.Copy.FindAsync(id);
            if (copy == null)
            {
                return NotFound();
            }
            return View(copy);
        }

        // POST: Copy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text")] Copy copy)
        {
            if (id != copy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(copy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CopyExists(copy.Id))
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
            return View(copy);
        }

        // GET: Copy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copy = await _context.Copy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (copy == null)
            {
                return NotFound();
            }

            return View(copy);
        }

        // POST: Copy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var copy = await _context.Copy.FindAsync(id);
            _context.Copy.Remove(copy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CopyExists(int id)
        {
            return _context.Copy.Any(e => e.Id == id);
        }
    }
}
