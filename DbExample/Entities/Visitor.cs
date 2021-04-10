using System;
using System.Collections.Generic;
using System.Text;

namespace DbExample.Entities
{
    public class Visitor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Rating { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
