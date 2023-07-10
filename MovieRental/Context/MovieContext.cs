using Microsoft.EntityFrameworkCore;
using MovieRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Context
{
    public class MovieContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Movie> Movies { get; set; }    
        public DbSet<Producer> Producers { get; set; }
        public DbSet<CustomerMovie> CustomersMovie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Movies;Trusted_Connection=true;Encrypt=false");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Many-To-Many
            modelBuilder.Entity<CustomerMovie>().HasKey(k => new { k.MovieId, k.CustomerId });
            modelBuilder.Entity<CustomerMovie>().HasOne(m => m.Movie).WithMany(c => c.Customers)
                .HasForeignKey(fk => fk.MovieId);
            modelBuilder.Entity<CustomerMovie>().HasOne(c => c.Customer).WithMany(m => m.Movies)
                .HasForeignKey(fk => fk.CustomerId);
            //table Customer
            modelBuilder.Entity<Customer>().HasKey(pk => pk.Id);
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(c=>c.LastName).IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).IsRequired()
               .HasMaxLength(10);
            modelBuilder.Entity<Customer>().Property(c => c.Address).IsRequired()
               .HasMaxLength(255);
            //table Movie 
            modelBuilder.Entity<Movie>().HasKey(pk=>pk.Id);
            modelBuilder.Entity<Movie>().Property(t => t.Title).IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Movie>().Property(r => r.Rating).IsRequired()
                .HasMaxLength(1);
            modelBuilder.Entity<Movie>().Property(d=>d.Duration).IsRequired()
                .HasMaxLength (10);
 
            //table Producer 
            modelBuilder.Entity<Producer>().HasKey(pk=>pk.Id);
            modelBuilder.Entity<Producer>().Property(c => c.CompanyName).IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Producer>().Property(c=>c.Country).IsRequired()
                .HasMaxLength(255);
            //table CustomerMovie
            modelBuilder.Entity<CustomerMovie>().Property(d => d.DateRented).IsRequired();
            modelBuilder.Entity<CustomerMovie>().Property(d => d.DateRented).IsRequired();
            //relations 
            modelBuilder.Entity<Movie>().HasOne(m => m.Producer).WithMany(p => p.Movies)
                .HasForeignKey(fk => fk.ProducerId);
            //modelBuilder.Entity<Movie>().HasOne(p => p.Producer).WithMany(c => c.Movies)
            //    .HasForeignKey(fk=>fk.ProducerId);
        }
    }
}
