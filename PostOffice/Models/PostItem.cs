using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.Shared.Models
{
    public class PostItem
    {
        public int Id { get; set; }

        public String Url { get; set; }

        public virtual List<Copy> Copy { get; set; }
   
    }
}
