using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PostOffice.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        [Display(Name = "Which Copy")]
        public string Copy { get; set; }
        public IEnumerable<SelectListItem> CopyList { get; set; }
    }
}
