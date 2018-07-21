using System;
using System.Collections.Generic;
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
                var blog = new Blog { Name = "Modest Blog" };
                blog.SetUrl("http://bohdanromaniuk.com");
                blog.Posts = new List<Post>();
                blog.Posts.Add(new Post() { Title = "My life", Content = "Some story", IsDeleted = false });
                blog.Posts.Add(new Post() { Title = "My life2", Content = "Some story2", IsDeleted = false });
                db.Blogs.Add(blog);
                db.SaveChanges();
            }

            using (var db = new BloggingContext())
            {
                Console.WriteLine("SingleBlog");
                var blog = db.Blogs.FirstOrDefault();
                Console.WriteLine($"{blog.Name}: {db.Entry(blog).Property("Url").CurrentValue}");

                Console.WriteLine("Blogs:");
                var blogs = db.Blogs.FromSql("SELECT * FROM dbo.Blogs")
                    .OrderBy(b => b.Name)
                    .Select(b => b.Name)
                    .ToList();
                foreach(var blogName in blogs)
                {
                    Console.WriteLine(blogName);
                }

                Console.WriteLine("Blogs with posts");
                var blogsWithPosts = db.Blogs.Include(b => b.Posts).OrderBy(b => b.BlogId);
                foreach(var blogWithPost in blogsWithPosts)
                {
                    Console.WriteLine($"Name: {blogWithPost.Name} Id: {blogWithPost.BlogId}");
                    Console.WriteLine($"Blog Posts(Count: {blogWithPost.Posts.Count})#########");
                    foreach(var post in blogWithPost.Posts)
                    {
                        Console.WriteLine($"\tTitle: {post.Title,-15} Content: {post.Content,-15}  Deleted: {post.IsDeleted}");
                    }
                }
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

        public List<Post> Posts { get; set; }

        public Blog()
        {
        }

        public void SetUrl(string url)
        {
            _url = url;
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
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
