using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies
{
    public class Movie
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string MovieName { get; set; }
        public Category Category { get; set; }
    }
}
