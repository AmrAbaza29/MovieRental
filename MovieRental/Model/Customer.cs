using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } 
        public DateTime BirthDate { get; set; }
        public int PhoneNumber { get; set; }
        public List<CustomerMovie> Movies { get; set; } = new();
    }
}
