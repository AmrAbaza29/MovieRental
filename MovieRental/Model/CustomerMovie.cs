using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Model
{
    public class CustomerMovie
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  
        public DateTime DateRented { get; set; }
        public DateTime DueDate { get; set; }
    }
}
