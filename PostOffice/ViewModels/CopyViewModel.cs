using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.ViewModels
{
    public class CopyViewModel
    {
        public int Id { get; set; }

        [Required]
        public String Text { get; set; }

        [Required]
        [Display(Name = "Post URL")]
        public string Url { get; set; }
        public IEnumerable<SelectListItem> Urls { get; set; }
    }
}
