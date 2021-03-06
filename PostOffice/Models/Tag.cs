﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PostOffice.Shared.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public String Label { get; set; }

        public int CopyId { get; set; }
        public virtual Copy Copy { get; set; }
    }
}
