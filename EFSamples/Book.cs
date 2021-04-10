using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFSamples
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }

        [Required, StringLength(255)]
        public string Author { get; set; }

        public int PagesCount { get; set; }
    }
}
