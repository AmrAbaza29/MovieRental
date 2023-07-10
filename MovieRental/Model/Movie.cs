using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public int Duration { get; set; }   
        public char Rating { get; set; }
        public List<CustomerMovie> Customers { get; set; } = new();
        public int ProducerId { get; set; }
        public Producer Producer { get; set; } 
    }
}
