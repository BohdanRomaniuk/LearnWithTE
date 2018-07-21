using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                var blog = new Blog { Name = "Bohdans Blog" };
                blog.SetUrl("http://bohdanromaniuk.com");
                db.Blogs.Add(blog);
                db.SaveChanges();
            }

            using (var db = new BloggingContext())
            {
                var blog = db.Blogs.Single();
                Console.WriteLine($"{blog.Name}: {db.Entry(blog).Property("Url").CurrentValue}");
            }
            Console.ReadKey();
        }
    }

    public class BloggingContext: DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyBlog;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent configuration of owned types.
            modelBuilder.Entity<Blog>()
                .Property<string>("Url")
                .HasField("_url");

            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.WorkAddress);

            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.PhysicalAddress)
                .ToTable("PhysicalsAddresses");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        private string _url;

        public Blog()
        {
        }

        public void SetUrl(string url)
        {
            _url = url;
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public Address WorkAddress { get; set; }
        public Address PhysicalAddress { get; set; }
    }

    public class Address
    {
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string PostalOrZipCode { get; set; }
        public string StateOrProvince { get; set; }
        public string CityOrTown { get; internal set; }
        public string CountryName { get; set; }
    }
}
