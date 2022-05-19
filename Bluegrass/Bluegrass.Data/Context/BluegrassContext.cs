using System;
using Bluegrass.Data.Authentication;
using Bluegrass.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bluegrass.Data.Context
{
    public class BluegrassContext : IdentityDbContext<ApplicationUser>
    {
        #region Members
        
        // TODO: Add Members for Encryption
        
        #endregion
        
        #region Constructors
        
        public BluegrassContext(DbContextOptions<BluegrassContext> options) : base(options)
        {
            // TODO: Add Encryption Option to the constructors
        }

        #endregion
        
        #region On Model Creation

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exclude tables from Migrations
            // LOL. I am a bit confused at the moment.
            // modelBuilder.Entity<IdentityUser>(entity => {
            //     entity.ToTable("AspNetRoles", t => t.ExcludeFromMigrations());
            // });

            // TODO: Encryption on Database data
            
            // TODO: Set the Identity Columns
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Avatar>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            // TODO: Pre-Populate the database with information
            
            modelBuilder.Entity<Person>(p =>
            {
                p.HasData(new Person()
                {
                    Id = 1,
                    Name = "Jason",
                    Surname = "De Jesuz",
                    Gender = "Male",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                    // TODO: Add the Profile Picture
                });
                // p.OwnsOne(a => a.Address).HasData(new
                // {
                //     Id = 1,
                //     City = "Pretoria",
                //     Country = "South Africa",
                //     PersonId = 1,
                //     DateCreated = DateTime.Now,
                //     DateModified = DateTime.Now
                // });
                // p.OwnsOne(c => c.Contact).HasData(new
                // {
                //     Id = 1,
                //     Mobile = "0823405879",
                //     Email = "jason@lyemac.co.za",
                //     PersonId = 1,
                //     DateCreated = DateTime.Now,
                //     DateModified = DateTime.Now
                // });
            });

            #region Dropdown Values
            modelBuilder.Entity<DropdownCountry>()
            .HasData(
                new DropdownCountry { Id = 1, Country = "Select" },
                new DropdownCountry { Id=2, Country = "South Africa" },
                new DropdownCountry { Id=3, Country = "United States of America" },
                new DropdownCountry { Id=4, Country = "Singapore" }
            );

            modelBuilder.Entity<DropdownGender>()
            .HasData(
                new DropdownGender { Id=1, Gender="Select" },
                new DropdownGender { Id=2, Gender = "Male"},
                new DropdownGender { Id=3, Gender = "Female"}
            );
            #endregion

            modelBuilder = LinkModels(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        
        #endregion
        
        #region Database Models Relationships

        /// <summary>
        /// Configure the database relationships for entity navigation
        /// </summary>
        private static ModelBuilder LinkModels(ModelBuilder modelBuilder)
        {
            // Contact details of person
            modelBuilder.Entity<Person>().HasOne<Contact>(i => i.Contact).WithOne(i => i.Person)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Address of person
            modelBuilder.Entity<Person>().HasOne<Address>(i => i.Address).WithOne(i => i.Person)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Profile picture of person
            modelBuilder.Entity<Person>().HasOne<Avatar>(i => i.Avatar).WithOne(i => i.Person)
                .OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
        
        #endregion
        
        #region Database Models
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        
        #endregion

        #region Dropdown Data

        public DbSet<DropdownCountry> DropdownCountries { get; set; }
        public DbSet<DropdownGender> DropdownGenders { get; set; }

        #endregion
    }
}