using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Model
{
    public class View
    {
        public static void Menu()
        {
            Console.WriteLine("Welcome To Movie Rental System\n--------------------------------");
            Console.WriteLine("Select What You Want To Do");
            Console.WriteLine("1- Add Movie\n2- Add Producer\n3- Add Customer\n4- Rent Movie");
        }
    }
}
