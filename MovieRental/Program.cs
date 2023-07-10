using Microsoft.EntityFrameworkCore;
using MovieRental.Context;
using MovieRental.Model;
using System.Security.Cryptography.X509Certificates;

namespace MovieRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MovieContext movieContext = new MovieContext();


            
            MovieContext db = new MovieContext();
            #region add data
            var producers = new List<Producer>
            {
                new Producer{CompanyName="Coppola Company", Country="USA"},//the godfather
                new Producer{CompanyName="The Ladda Company", Country="USA"},//once upon a time in America
                new Producer{CompanyName="Chernin Entertainment", Country="USA"}//heat
            };
            var customers = new List<Customer>
            {
                new Customer{
                    FirstName="John",
                    LastName="Doe",
                    Address="USA, Virginia",
                    BirthDate=new DateTime(1996,4,1),
                    PhoneNumber=125489631
                },
                new Customer{
                    FirstName="Michael",
                    LastName="Johnas",
                    Address="Italy, Rome",
                    BirthDate=new DateTime(1993,6,20),
                    PhoneNumber=123456789
                },
                new Customer{
                    FirstName="Leonardo",
                    LastName="Davinci",
                    Address="Australia, Australia",
                    BirthDate=new DateTime(1995,6,19),
                    PhoneNumber=479631486
                },
            };
            var movies = new List<Movie>
            {
                new Movie{ProducerId=1, Duration=180, Rating='A', Title="The Godfather"},
                new Movie{ProducerId=2, Duration=170, Rating='B', Title="Once Upon a time in America"},
                new Movie{ProducerId=3, Duration=120, Rating='A', Title="Heat"},
                new Movie{ProducerId=1, Duration=180, Rating='B', Title="The Godfather II"},
                new Movie{ProducerId=1, Duration=140, Rating='A', Title="Fight Club"},
                new Movie{ProducerId=2, Duration=120, Rating='C', Title="The Intern"},
                new Movie{ProducerId=3, Duration=160, Rating='A', Title="Dark Knight Rise"}
            };
            var customerMovies = new List<CustomerMovie>
            {
                new CustomerMovie{CustomerId=1, MovieId=3, DateRented=new DateTime(2023,6,1), DueDate=new DateTime(2023, 6,8)},
                new CustomerMovie{CustomerId=2, MovieId=4, DateRented=new DateTime(2023,6,2), DueDate=new DateTime(2023, 6,9)},
                new CustomerMovie{CustomerId=3, MovieId=5, DateRented=new DateTime(2023,6,3), DueDate=new DateTime(2023, 6,10)},
                new CustomerMovie{CustomerId=1, MovieId=6, DateRented=new DateTime(2023,6,4), DueDate=new DateTime(2023, 6,11)},
                new CustomerMovie{CustomerId=2, MovieId=7, DateRented=new DateTime(2023,6,5), DueDate=new DateTime(2023, 6,12)},
                new CustomerMovie{CustomerId=3, MovieId=8, DateRented=new DateTime(2023,6,6), DueDate=new DateTime(2023, 6,13)},
                new CustomerMovie{CustomerId=1, MovieId=9, DateRented=new DateTime(2023,6,7), DueDate=new DateTime(2023, 6,14)},
                new CustomerMovie{CustomerId=2, MovieId=3, DateRented=new DateTime(2023,6,8), DueDate=new DateTime(2023, 6,15)},
                new CustomerMovie{CustomerId=3, MovieId=4, DateRented=new DateTime(2023,6,9), DueDate=new DateTime(2023, 6,16)},
                new CustomerMovie{CustomerId=1, MovieId=5, DateRented=new DateTime(2023,6,10), DueDate=new DateTime(2023, 6,17)},
                new CustomerMovie{CustomerId=2, MovieId=6, DateRented=new DateTime(2023,6,11), DueDate=new DateTime(2023, 6,18)},
                new CustomerMovie{CustomerId=3, MovieId=7, DateRented=new DateTime(2023,6,12), DueDate=new DateTime(2023, 6,19)},
            };
            movieContext.AddRange(producers);
            movieContext.AddRange(customers);
            movieContext.AddRange(movies);
            movieContext.AddRange(customerMovies);
            movieContext.SaveChanges();
            #endregion

            var query = (from movie in movieContext.Movies
                         join rental in movieContext.CustomersMovie on movie.Id equals rental.MovieId
                         group movie by movie.Title into movieGroup
                         orderby movieGroup.Count() descending
                         select new
                         {
                             Number = movieGroup.Count(),
                             Title = movieGroup.Key
                         }).Take(3);
            Console.WriteLine("Top 3 Rented Movies\n-----------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"{c.Title} ========> {c.Number} times");
            }
            Console.WriteLine("=========================================================");
            Console.WriteLine("Top Producer\n-----------------------------------");

            var query1 = from movie in movieContext.Movies
                         join producer in movieContext.Producers on movie.ProducerId equals producer.Id
                         group movie by producer.CompanyName into producerGroup
                         orderby producerGroup.Count() descending
                         select new
                         {
                             Number = producerGroup.Count(),
                             CompanyName = producerGroup.Key
                         };
            foreach (var c in query1)
            {
                Console.WriteLine($"Top Producer=> {c.CompanyName}, with=> {c.Number} movies");
            }
            var query2 = from customer in movieContext.Customers
                         join rentalCount in movieContext.CustomersMovie on customer.Id equals rentalCount.CustomerId
                         group customer by customer.FirstName into movieGroup
                         orderby movieGroup.Count() descending
                         select new
                         {
                             Number = movieGroup.Count(),
                             Name = movieGroup.Key
                         };
            Console.WriteLine("=========================================================");
            Console.WriteLine("Customers ordered by rental count: \n-------------------------------");
            foreach (var c in query2)
            {
                Console.WriteLine($"Name: {c.Name}, Rental Times:{c.Number}");
            }
            List<Customer> c1 = db.Customers.Include(x => x.Movies).ThenInclude(x => x.Movie)
                .ThenInclude(x => x.Producer).ToList();
            Console.WriteLine("=========================================================");
            Console.WriteLine("All Details\n-------------------------------");
            foreach (var customer in c1)
            {
                Console.WriteLine($"Customer Name: {customer.FirstName} {customer.LastName}");
                foreach (var c in customer.Movies)
                {
                    int daysRemaining = DateTime.Now.Subtract(c.DueDate).Days;
                    string days = daysRemaining >= 0 ? $"{daysRemaining}" : $"{Math.Abs(daysRemaining)} Days Late";
                    Console.WriteLine("His Movies: \n-------------------------------");
                    Console.WriteLine($"Movie Title: {c.Movie.Title}");
                    Console.WriteLine($"Date Rented: {c.DateRented}");
                    Console.WriteLine($"Days Remaining: {days}");
                    Console.WriteLine($"Producer Company: {c.Movie.Producer.CompanyName}");
                }
                Console.WriteLine("=========================================================");
            }
            //Overdue rent time 
            foreach (var customer in c1)
            {
                foreach (var c in customer.Movies)
                {
                    int daysRemaining = DateTime.Now.Subtract(c.DueDate).Days;
                    if (daysRemaining < 0)
                    {
                        Console.WriteLine($"{customer.FirstName} {customer.LastName} | " +
                            $"{Math.Abs(daysRemaining)} Days Late | {c.Movie.Title} |" +
                            $"{c.DateRented} | {c.Movie.Producer.CompanyName}");
                    }
                    
                }
            }
            Console.WriteLine("********************************************************");
        }
    }
}