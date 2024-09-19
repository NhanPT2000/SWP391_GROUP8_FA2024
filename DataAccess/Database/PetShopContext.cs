using DataObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public class PetShopContext : DbContext
    {
        public PetShopContext() { }
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options)
        { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Staff> Staffs { get; set; }
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
            modelBuilder.Entity<Staff>()
                .HasOne(p => p.WorkSchedule)
                .WithMany(m => m.Staffs)
                .HasForeignKey(p => p.StaffId);
            //
            /*Member-Role*/
            //
            modelBuilder.Entity<Member>()
                .HasOne(p => p._Role)
                .WithMany(m => m._Members)
                .HasForeignKey(p => p.MemberId);
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
                .HasForeignKey(p => p.InvoiceId);
            //
            /*Member-Staff*/
            //
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Member)
                .WithOne(m => m._Staff)
                .HasForeignKey<Staff>(s => s.StaffId);
            //
            /*Member-Admin*/
            //
            modelBuilder.Entity<Admin>()
                .HasOne(s => s.Member)
                .WithOne(m => m._Admin)
                .HasForeignKey<Admin>(s => s.AdminId);
            //
            /*Voucher-Staff*/
            //
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Member)
                .WithOne(m => m._Staff)
                .HasForeignKey<Staff>(s => s.StaffId);
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
        }

    }
}
