using System.Collections.Generic;

namespace EFSamples
{
    public class Visitor
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public ICollection<Book> TakenBooks { get; set; }
    }
}