﻿// <auto-generated />
using System;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PetShopContext))]
    partial class PetShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataObject.Case", b =>
                {
                    b.Property<Guid>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FacilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("CaseId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("PetId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("DataObject.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("7153d563-8bb7-47ac-934f-fbfb353bf8ab"),
                            CategoryName = "Dog Food",
                            Status = "None"
                        },
                        new
                        {
                            CategoryId = new Guid("4c0d5274-44a8-4203-8d2a-812daf366702"),
                            CategoryName = "Cat Food",
                            Status = "None"
                        },
                        new
                        {
                            CategoryId = new Guid("be87443f-bc63-4479-836b-e0bd5bf3af71"),
                            CategoryName = "Bird Food",
                            Status = "None"
                        });
                });

            modelBuilder.Entity("DataObject.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("EventTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.HasIndex("AdminId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DataObject.Facility", b =>
                {
                    b.Property<Guid>("FacilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContractPer")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FacilityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FacilityId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("DataObject.Feedback", b =>
                {
                    b.Property<Guid>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FeedbackId");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("DataObject.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("AmountCharge")
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid?>("CaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("InvoiceAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("InvoiceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeCharge")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeGenerated")
                        .HasColumnType("int");

                    b.HasKey("InvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("DataObject.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Freight")
                        .HasColumnType("real");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequiredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("MemberId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataObject.OrderDetails", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("_OrderDetails");
                });

            modelBuilder.Entity("DataObject.Pet", b =>
                {
                    b.Property<Guid>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("SpeciesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PetId");

                    b.HasIndex("MemberId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("DataObject.PlannedService", b =>
                {
                    b.Property<Guid>("PlannedServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CostPerUnit")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PlannedUnits")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PlannedServiceId");

                    b.HasIndex("CaseId");

                    b.HasIndex("MemberId");

                    b.HasIndex("ServiceId");

                    b.ToTable("PlannedServices");
                });

            modelBuilder.Entity("DataObject.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("f2bb30ec-0133-4963-a596-74626552d5ef"),
                            CategoryId = new Guid("7153d563-8bb7-47ac-934f-fbfb353bf8ab"),
                            Image = "product_01.jpg",
                            Origin = "Viet Nam",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Dog Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 1,
                            Weight = 12m
                        },
                        new
                        {
                            ProductId = new Guid("7214221c-5668-4ac1-9075-213144f68b7e"),
                            CategoryId = new Guid("4c0d5274-44a8-4203-8d2a-812daf366702"),
                            Image = "product_02.jpg",
                            Origin = "Viet Nam",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Dog Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 1,
                            Weight = 12m
                        },
                        new
                        {
                            ProductId = new Guid("94beae78-c9d7-4830-aa59-94567c562cd4"),
                            CategoryId = new Guid("be87443f-bc63-4479-836b-e0bd5bf3af71"),
                            Image = "product_03.jpg",
                            Origin = "Viet Nam",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Cat Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 0,
                            Weight = 0.76m
                        },
                        new
                        {
                            ProductId = new Guid("898cd542-ada3-4b78-8f21-de5957994b48"),
                            CategoryId = new Guid("7153d563-8bb7-47ac-934f-fbfb353bf8ab"),
                            Image = "product_04.jpg",
                            Origin = "England",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Bird Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 10,
                            Weight = 12m
                        },
                        new
                        {
                            ProductId = new Guid("aba453a0-2819-4794-b13a-e2bf8a3712bd"),
                            CategoryId = new Guid("be87443f-bc63-4479-836b-e0bd5bf3af71"),
                            Image = "product_05.jpg",
                            Origin = "Viet Nam",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Dog Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 1,
                            Weight = 13m
                        },
                        new
                        {
                            ProductId = new Guid("91a2eba5-26d4-49f0-966a-977bfa5ee5e4"),
                            CategoryId = new Guid("7153d563-8bb7-47ac-934f-fbfb353bf8ab"),
                            Image = "product_06.jpg",
                            Origin = "Viet Nam",
                            ProductDescription = "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds",
                            ProductName = "Premium Dog Food",
                            UnitPrice = 10000m,
                            UnitsInStock = 1,
                            Weight = 12m
                        });
                });

            modelBuilder.Entity("DataObject.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("c00d5d18-058b-4dad-90c7-44e11c8989e3"),
                            RoleName = "Admin",
                            Status = "None"
                        },
                        new
                        {
                            RoleId = new Guid("22fd5e77-262c-4a38-9a43-26066501f1c8"),
                            RoleName = "Member",
                            Status = "None"
                        },
                        new
                        {
                            RoleId = new Guid("d7e9e6e7-9050-4630-8fa6-88ba0bb280c1"),
                            RoleName = "Staff",
                            Status = "None"
                        });
                });

            modelBuilder.Entity("DataObject.Service", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CostPerUnity")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool?>("HasLimit")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(900)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DataObject.Species", b =>
                {
                    b.Property<Guid>("SpeciesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SpeciesId");

                    b.ToTable("_Species");
                });

            modelBuilder.Entity("DataObject.StaffDetails", b =>
                {
                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DegreeImage")
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("WorkScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StaffId");

                    b.ToTable("StaffDetails");
                });

            modelBuilder.Entity("DataObject.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Addess")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("ConfirmedEmail")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("OnlineTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Profile")
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("55241cb9-3c1f-4dd6-840a-bddb1303042b"),
                            Addess = "123 ABc",
                            Email = "manh123@gmail.com",
                            Gender = "Male",
                            Password = "nsZXnLisYMRi5raBLsXJFWnp0G/cOmcXIe5wNwLRrJk=",
                            PhoneNumber = "0123456789",
                            RoleId = new Guid("c00d5d18-058b-4dad-90c7-44e11c8989e3"),
                            UserName = "Manh"
                        });
                });

            modelBuilder.Entity("DataObject.Voucher", b =>
                {
                    b.Property<Guid>("Voucherid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PlannedServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VourcherDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VourcherTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Voucherid");

                    b.HasIndex("OrderId");

                    b.HasIndex("PlannedServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("DataObject.WorkSchedule", b =>
                {
                    b.Property<Guid>("WorkScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkScheduleId");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("DataObject.Case", b =>
                {
                    b.HasOne("DataObject.Facility", "Facility")
                        .WithMany("_Cases")
                        .HasForeignKey("FacilityId");

                    b.HasOne("DataObject.Pet", "Pet")
                        .WithMany("_Cases")
                        .HasForeignKey("PetId");

                    b.Navigation("Facility");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("DataObject.Event", b =>
                {
                    b.HasOne("DataObject.User", "Admin")
                        .WithMany("_Events")
                        .HasForeignKey("AdminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("DataObject.Feedback", b =>
                {
                    b.HasOne("DataObject.User", "Member")
                        .WithMany("_Feedbacks")
                        .HasForeignKey("MemberId");

                    b.HasOne("DataObject.Product", "Product")
                        .WithMany("_Feedbacks")
                        .HasForeignKey("ProductId");

                    b.HasOne("DataObject.Service", "Service")
                        .WithMany("_Feedbacks")
                        .HasForeignKey("ServiceId");

                    b.Navigation("Member");

                    b.Navigation("Product");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("DataObject.Invoice", b =>
                {
                    b.HasOne("DataObject.Case", "Case")
                        .WithMany("Invoices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataObject.Service", "Service")
                        .WithMany("_Invoices")
                        .HasForeignKey("ServiceId");

                    b.Navigation("Case");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("DataObject.Order", b =>
                {
                    b.HasOne("DataObject.User", "Member")
                        .WithMany("_Orders")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("DataObject.OrderDetails", b =>
                {
                    b.HasOne("DataObject.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataObject.Product", "Product")
                        .WithMany("_OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataObject.Pet", b =>
                {
                    b.HasOne("DataObject.User", "Member")
                        .WithMany("_Pets")
                        .HasForeignKey("MemberId");

                    b.HasOne("DataObject.Species", "Species")
                        .WithMany("Pets")
                        .HasForeignKey("SpeciesId");

                    b.Navigation("Member");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("DataObject.PlannedService", b =>
                {
                    b.HasOne("DataObject.Case", "Case")
                        .WithMany("_PlannedServices")
                        .HasForeignKey("CaseId");

                    b.HasOne("DataObject.User", "_Member")
                        .WithMany("_PlannedServices")
                        .HasForeignKey("MemberId");

                    b.HasOne("DataObject.Service", "Service")
                        .WithMany("_PlannedServices")
                        .HasForeignKey("ServiceId");

                    b.Navigation("Case");

                    b.Navigation("Service");

                    b.Navigation("_Member");
                });

            modelBuilder.Entity("DataObject.Product", b =>
                {
                    b.HasOne("DataObject.Category", "Category")
                        .WithMany("_Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataObject.StaffDetails", b =>
                {
                    b.HasOne("DataObject.User", "Member")
                        .WithOne("_StaffDetails")
                        .HasForeignKey("DataObject.StaffDetails", "StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataObject.WorkSchedule", "WorkSchedule")
                        .WithMany("Staffs")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("WorkSchedule");
                });

            modelBuilder.Entity("DataObject.User", b =>
                {
                    b.HasOne("DataObject.Role", "_Role")
                        .WithMany("_Members")
                        .HasForeignKey("RoleId");

                    b.Navigation("_Role");
                });

            modelBuilder.Entity("DataObject.Voucher", b =>
                {
                    b.HasOne("DataObject.Order", "Order")
                        .WithMany("_Vouchers")
                        .HasForeignKey("OrderId");

                    b.HasOne("DataObject.PlannedService", "PlannedService")
                        .WithMany("_Vouchers")
                        .HasForeignKey("PlannedServiceId");

                    b.HasOne("DataObject.User", "User")
                        .WithMany("_Vouchers")
                        .HasForeignKey("UserId");

                    b.Navigation("Order");

                    b.Navigation("PlannedService");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataObject.Case", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("_PlannedServices");
                });

            modelBuilder.Entity("DataObject.Category", b =>
                {
                    b.Navigation("_Products");
                });

            modelBuilder.Entity("DataObject.Facility", b =>
                {
                    b.Navigation("_Cases");
                });

            modelBuilder.Entity("DataObject.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("_Vouchers");
                });

            modelBuilder.Entity("DataObject.Pet", b =>
                {
                    b.Navigation("_Cases");
                });

            modelBuilder.Entity("DataObject.PlannedService", b =>
                {
                    b.Navigation("_Vouchers");
                });

            modelBuilder.Entity("DataObject.Product", b =>
                {
                    b.Navigation("_Feedbacks");

                    b.Navigation("_OrderDetails");
                });

            modelBuilder.Entity("DataObject.Role", b =>
                {
                    b.Navigation("_Members");
                });

            modelBuilder.Entity("DataObject.Service", b =>
                {
                    b.Navigation("_Feedbacks");

                    b.Navigation("_Invoices");

                    b.Navigation("_PlannedServices");
                });

            modelBuilder.Entity("DataObject.Species", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("DataObject.User", b =>
                {
                    b.Navigation("_Events");

                    b.Navigation("_Feedbacks");

                    b.Navigation("_Orders");

                    b.Navigation("_Pets");

                    b.Navigation("_PlannedServices");

                    b.Navigation("_StaffDetails");

                    b.Navigation("_Vouchers");
                });

            modelBuilder.Entity("DataObject.WorkSchedule", b =>
                {
                    b.Navigation("Staffs");
                });
#pragma warning restore 612, 618
        }
    }
}
