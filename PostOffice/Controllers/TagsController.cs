using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PostOffice.Shared.Models;
using PostOffice.ViewModels;

namespace PostOffice.Controllers
{
    public class TagsController : Controller
    {
        private readonly PostOfficeDbContext _context;

        public TagsController(PostOfficeDbContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            List<Tag> tags = await _context.Tags.ToListAsync();
            foreach(Tag t in tags)
            {
                t.Copy = await _context.Copy.FirstOrDefaultAsync(copy => copy.Id == t.CopyId);
            }
            return View(tags);
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            var copyList = _context.Copy.Select(c => c.Text);
            var viewModel = new TagViewModel
            {
                CopyList = new SelectList(copyList)
            };
            return View(viewModel);
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel tagViewModel)
        {
            var copyItem = _context.Copy.FirstOrDefault(c => c.Text.Equals(tagViewModel.Copy.Trim().Replace("  ", " ")));
            Tag tag = new Tag
            {
                Label = tagViewModel.Label,
                Copy = copyItem
            };
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(int? copyId, string label)
        {
            if (copyId == null || label == null)
            {
                return NotFound();
            }
            var copyItem = _context.Copy.FirstOrDefault(c => c.Id.Equals(copyId));
            Tag tag = new Tag
            {
                Label = label,
                Copy = copyItem
            };
            if (ModelState.IsValid)
            {
                 _context.Add(tag);
                await _context.SaveChangesAsync();
                var returnData = new
                {
                    tagId = tag.Id,
                    label = tag.Label
                };
                return Json(returnData);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Label")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
