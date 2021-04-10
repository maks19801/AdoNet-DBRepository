using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFSamples
{
    public class Price
    {
        public int Id { get; set; }
        
        [Required]
        public Book Book { get; set; }
        public decimal BookPrice { get; set; }
    }
}
