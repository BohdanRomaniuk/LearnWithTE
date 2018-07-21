﻿// <auto-generated />
using EFC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCore.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20180721154702_Customers")]
    partial class Customers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFC.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("EFC.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EFC.Customer", b =>
                {
                    b.OwnsOne("EFC.Address", "PhysicalAddress", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("CityOrTown");

                            b1.Property<string>("CountryName");

                            b1.Property<string>("LineOne");

                            b1.Property<string>("LineTwo");

                            b1.Property<string>("PostalOrZipCode");

                            b1.Property<string>("StateOrProvince");

                            b1.ToTable("Customers");

                            b1.HasOne("EFC.Customer")
                                .WithOne("PhysicalAddress")
                                .HasForeignKey("EFC.Address", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("EFC.Address", "WorkAddress", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("CityOrTown");

                            b1.Property<string>("CountryName");

                            b1.Property<string>("LineOne");

                            b1.Property<string>("LineTwo");

                            b1.Property<string>("PostalOrZipCode");

                            b1.Property<string>("StateOrProvince");

                            b1.ToTable("Customers");

                            b1.HasOne("EFC.Customer")
                                .WithOne("WorkAddress")
                                .HasForeignKey("EFC.Address", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}