using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Model
{
    public class Producer
    {
        public int Id { get; set; } 
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public List<Movie> Movies { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Company Name: {CompanyName}, Country: {Country}";
        }

    }
}
