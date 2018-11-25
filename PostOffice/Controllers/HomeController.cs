using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models;
using PostOffice.Shared.Models;
using PostOffice.ViewModels;

namespace PostOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostOfficeDbContext _context;

        public HomeController(PostOfficeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewModel viewModel = new ViewModel();
            viewModel.accounts = await _context.Accounts.ToListAsync();
            List<PostItem> postItems = await _context.PostItems.ToListAsync();
            foreach (PostItem p in postItems)
            {
                List<Copy> copy = await _context.Copy.Where(c => c.PostItemId == p.Id).ToListAsync();
                p.Copy = copy;
                foreach (Copy c in copy)
                {
                    List<Tag> tags = await _context.Tags.Where(t => t.CopyId == c.Id).ToListAsync();
                    c.Tags = tags;
                }
            }
            viewModel.postItems = postItems;

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
