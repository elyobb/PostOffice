using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostOffice.Shared.Models
{
    public class Copy
    {
        public int Id { get; set; }
        public String Text { get; set; }

        public int PostItemId {get;set;}
        [Display(Name = "Post URL")]
        public PostItem PostItem { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
