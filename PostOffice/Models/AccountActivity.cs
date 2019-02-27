using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.Shared.Models
{
    public class AccountActivity
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int CopyId { get; set; }
        public virtual Copy Copy { get; set; }
    }
}
