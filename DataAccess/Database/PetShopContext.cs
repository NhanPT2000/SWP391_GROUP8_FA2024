using DataObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public class PetShopContext : DbContext
    {
        public PetShopContext() { }
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options)
        { }
        public DbSet<User> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StaffDetails> StaffDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DataObject.Service> Services { get; set; }
        public DbSet<PlannedService> PlannedServices { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Species> _Species { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> _OrderDetails { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PetShopDB"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            /*Member-Pet*/
            //
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Member)
                .WithMany(m=> m._Pets)
                .HasForeignKey(p => p.MemberId);
            //
            /*Product-Category*/
            //
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(m => m._Products)
                .HasForeignKey(p => p.CategoryId);
            //
            /*Order-Member*/
            //
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Member)
                .WithMany(m => m._Orders)
                .HasForeignKey(p => p.MemberId);
            //
            /*OrderDetails-CompositeKey*/
            //
            modelBuilder.Entity<OrderDetails>()
                .HasKey(o => new { o.OrderId, o.ProductId });
            //
            /*OrderDetails-Order*/
            //
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            //
            /*OrderDetails-Product*/
            //
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p._OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            //
            /*Staff-WorkSchedule*/
            //
            modelBuilder.Entity<StaffDetails>()
                .HasOne(p => p.WorkSchedule)
                .WithMany(m => m.Staffs)
                .HasForeignKey(p => p.StaffId);
            //
            /*Member-Role*/
            //
            modelBuilder.Entity<User>()
                .HasOne(p => p._Role)
                .WithMany(m => m._Members)
                .HasForeignKey(p => p.RoleId);
            //
            /*Species-Pet*/
            //
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Species)
                .WithMany(m => m.Pets)
                .HasForeignKey(p => p.SpeciesId);
            //
            /*Member-Pet*/
            //
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Member)
                .WithMany(m => m._Pets)
                .HasForeignKey(p => p.MemberId);
            //
            /*Facility-Pet*/
            //
            modelBuilder.Entity<Case>()
                .HasOne(p => p.Facility)
                .WithMany(m => m._Cases)
                .HasForeignKey(p => p.FacilityId);
            modelBuilder.Entity<Case>()
                .HasOne(p => p.Pet)
                .WithMany(m => m._Cases)
                .HasForeignKey(p => p.PetId);
            //
            /*Service-Cases-Planned Services*/
            //
            modelBuilder.Entity<PlannedService>()
                .HasOne(p => p.Service)
                .WithMany(m => m._PlannedServices)
                .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<PlannedService>()
                .HasOne(p => p.Case)
                .WithMany(m => m._PlannedServices)
                .HasForeignKey(p => p.CaseId);
            //
            /*Service-Cases-Invoice*/
            //
            modelBuilder.Entity<Invoice>()
                .HasOne(p => p.Service)
                .WithMany(m => m._Invoices)
                .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<Invoice>()
                .HasOne(p => p.Case)
                .WithMany(m => m.Invoices)
                .HasForeignKey(p => p.CaseId);
            modelBuilder.Entity<Invoice>()
                .HasOne(p => p.Order)
                .WithMany(m => m.Invoices)
                .HasForeignKey(p => p.OrderId);
            //
            /*User-Staff*/
            //
            modelBuilder.Entity<StaffDetails>()
                .HasOne(s => s.Member)
                .WithOne(m => m._StaffDetails)
                .HasForeignKey<StaffDetails>(s => s.StaffId);
            //
            /*Voucher-User*/
            //
            modelBuilder.Entity<Voucher>()
                .HasOne(s => s.User)
                .WithMany(m => m._Vouchers)
                .HasForeignKey(s => s.UserId);
            //
            /*Voucher-PlannedService*/
            //
            modelBuilder.Entity<Voucher>()
                .HasOne(s => s.PlannedService)
                .WithMany(m => m._Vouchers)
                .HasForeignKey(s => s.PlannedServiceId);
            //
            /*Voucher-Order*/
            //
            modelBuilder.Entity<Voucher>()
                .HasOne(s => s.Order)
                .WithMany(m => m._Vouchers)
                .HasForeignKey(s => s.OrderId);
            //
            /*Admin-Event*/
            //
            modelBuilder.Entity<Event>()
                .HasOne(s => s.Admin)
                .WithMany(m => m._Events)
                .HasForeignKey(s => s.AdminId);
            //
            /*Member-PlannedService*/
            //
            modelBuilder.Entity<PlannedService>()
                .HasOne(s => s._Member)
                .WithMany(m => m._PlannedServices)
                .HasForeignKey(s => s.MemberId);
            //
            /*Member-Feedback*/
            //
            modelBuilder.Entity<Feedback>()
                .HasOne(s => s.Member)
                .WithMany(m => m._Feedbacks)
                .HasForeignKey(s => s.MemberId);
            //
            /*Product-Feedback*/
            //
            modelBuilder.Entity<Feedback>()
                .HasOne(s => s.Product)
                .WithMany(m => m._Feedbacks)
                .HasForeignKey(s => s.ProductId);
            //
            /*Service-Feedback*/
            //
            modelBuilder.Entity<Feedback>()
                .HasOne(s => s.Service)
                .WithMany(m => m._Feedbacks)
                .HasForeignKey(s => s.ServiceId);
            //
            /*Role-Seeding*/
            //
            var AdminId = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                new { RoleId = AdminId, RoleName = "Admin", Status ="None" },
                new { RoleId = Guid.NewGuid(), RoleName = "Member", Status = "None" },
                new { RoleId = Guid.NewGuid(), RoleName = "Staff", Status = "None" });
            //
            /*Admin-Seeding*/
            //
            modelBuilder.Entity<User>().HasData(
                new {
                    UserId = Guid.NewGuid(),
                    UserName = "Manh",
                    Password = HashPassword("manh123"),
                    Email = "manh123@gmail.com",
                    Gender = "Male",
                    PhoneNumber = "0123456789",
                    Addess = "123 ABc",
                    RoleId = AdminId
                }
                );
            //
            /*Category-Seeding*/
            //
            var dogFoodCategoryId = Guid.NewGuid();
            var catFoodCategoryId = Guid.NewGuid();
            var birdFoodCategoryId = Guid.NewGuid();
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = dogFoodCategoryId, CategoryName = "Dog Food", Status = "None"},
                new { CategoryId = catFoodCategoryId, CategoryName = "Cat Food", Status = "None"},
                new { CategoryId = birdFoodCategoryId, CategoryName = "Bird Food", Status = "None"}
                );
            //
            /*Products-Sedding*/
            //
            modelBuilder.Entity<Product>().HasData(
                new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Dog Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "Viet Nam",
                    Weight = (float)12,
                    UnitPrice = (float)10000,
                    UnitsInStock = 1,
                    Image = "product_01.jpg",
                    CategoryId = dogFoodCategoryId
                },
                new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Dog Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "Viet Nam",
                    Weight = (float)12,
                    UnitPrice = (float)10000,
                    UnitsInStock = 1,
                    Image = "product_02.jpg",
                    CategoryId = catFoodCategoryId
                }, new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Cat Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "Viet Nam",
                    Weight = (float)0.76,
                    UnitPrice = (float)10000,
                    UnitsInStock = 0,
                    Image = "product_03.jpg",
                    CategoryId = birdFoodCategoryId
                },
                new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Bird Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "England",
                    Weight = (float)12,
                    UnitPrice = (float)10000,
                    UnitsInStock = 10,
                    Image = "product_04.jpg",
                    CategoryId = dogFoodCategoryId
                },
                new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Dog Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "Viet Nam",
                    Weight = (float)13,
                    UnitPrice = (float)10000,
                    UnitsInStock = 1,
                    Image = "product_05.jpg",
                    CategoryId = birdFoodCategoryId
                },
                new
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Premium Dog Food",
                    ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                    Origin = "Viet Nam",
                    Weight = (float)12,
                    UnitPrice = (float)10000,
                    UnitsInStock = 1,
                    Image = "product_06.jpg",
                    CategoryId = dogFoodCategoryId
                }
                );
            modelBuilder.Entity<Facility>().HasData(
                new
                {
                    FacilityId = Guid.NewGuid(),
                    FacilityName = "Room Vet A1",
                    Address = "Floor 3, Room 201",
                    PhoneNumber = "0342231123",
                    ContractPer = "Doctor Joe",
                    Image = "1.jpg",
                    IsDeleted = false
                }
                );
            modelBuilder.Entity<Species>().HasData(
                new
                {
                    SpeciesId = Guid.NewGuid(),
                    SpeciesName = "Dog",
                    IsDeleted = false
                },
                new
                {
                    SpeciesId = Guid.NewGuid(),
                    SpeciesName = "Cat",
                    IsDeleted = false

                },
                new
                {
                    SpeciesId = Guid.NewGuid(),
                    SpeciesName = "Parrot",
                    IsDeleted = false

                },
                new
                {
                    SpeciesId = Guid.NewGuid(),
                    SpeciesName = "Ferret",
                    IsDeleted = false

                }
                );
        }
        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var HashPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(HashPassword);
        }

    }
}
