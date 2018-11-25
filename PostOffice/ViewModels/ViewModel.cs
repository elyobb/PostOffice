using PostOffice.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.ViewModels
{
    public class ViewModel
    {
        public List<PostItem> postItems { get; set; }
        public List<Account> accounts { get; set; }
        public List<Tag> tags { get; set; }
    }
}
