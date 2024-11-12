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

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(MAX)");

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

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"),
                            CategoryName = "Dog Food",
                            Status = "None"
                        },
                        new
                        {
                            CategoryId = new Guid("ec3e268c-46bd-49ab-b161-55651e4475b0"),
                            CategoryName = "Cat Food",
                            Status = "None"
                        },
                        new
                        {
                            CategoryId = new Guid("019a0482-0f12-4114-857a-76cbb24e2818"),
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
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("ContractPer")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("FacilityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("FacilityId");

                    b.ToTable("Facilities");

                    b.HasData(
                        new
                        {
                            FacilityId = new Guid("2a831e1f-4fb8-4b55-827b-7ff9b07c83c4"),
                            Address = "Floor 3, Room 201",
                            ContractPer = "Doctor Joe",
                            FacilityName = "Room Vet A1",
                            Image = "1.jpg",
                            IsDeleted = false,
                            PhoneNumber = "0342231123"
                        });
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
                        .ValueGeneratedOnAdd()
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

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PayerId")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("TimeCharge")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeGenerated")
                        .HasColumnType("int");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CaseId");

                    b.HasIndex("OrderId");

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

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit");

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

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<float?>("UnitPrice")
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
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(Max)");

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

                    b.Property<bool?>("IsCofirmed")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(MAX)");

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
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(Max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

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
                            ProductId = new Guid("f7ea46c9-f5e5-40ab-b0c5-49af957899e0"),
                            CategoryId = new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"),
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
                            ProductId = new Guid("50ab2adf-ac46-4490-8b94-9e9dc7af08f9"),
                            CategoryId = new Guid("ec3e268c-46bd-49ab-b161-55651e4475b0"),
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
                            ProductId = new Guid("1441651e-06eb-4c78-b4a4-8f9fc91e8a95"),
                            CategoryId = new Guid("019a0482-0f12-4114-857a-76cbb24e2818"),
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
                            ProductId = new Guid("539133a7-d2df-4748-8058-c289cf8dcc4c"),
                            CategoryId = new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"),
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
                            ProductId = new Guid("77228607-c331-45f9-8ca3-d6a61da48523"),
                            CategoryId = new Guid("019a0482-0f12-4114-857a-76cbb24e2818"),
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
                            ProductId = new Guid("ae7e1aeb-c278-46b5-bd7d-c70534a6d223"),
                            CategoryId = new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"),
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
                            RoleId = new Guid("f0463cb3-d4fc-4c68-8eca-bce793c8cbc0"),
                            RoleName = "Admin",
                            Status = "None"
                        },
                        new
                        {
                            RoleId = new Guid("7fb2f782-d814-4299-8a89-05dd3848b05a"),
                            RoleName = "Member",
                            Status = "None"
                        },
                        new
                        {
                            RoleId = new Guid("12d976c3-3591-43f3-a4cf-250185abfdd6"),
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
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

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

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SpeciesId");

                    b.ToTable("_Species");

                    b.HasData(
                        new
                        {
                            SpeciesId = new Guid("f1393ebe-6458-446e-b216-d53cd02eea02"),
                            SpeciesName = "Dog"
                        },
                        new
                        {
                            SpeciesId = new Guid("7595f6d0-d560-49d1-8857-dfcb7104e58f"),
                            SpeciesName = "Cat"
                        },
                        new
                        {
                            SpeciesId = new Guid("3068c789-140c-4b25-94fb-602efd40eec2"),
                            SpeciesName = "Parrot"
                        },
                        new
                        {
                            SpeciesId = new Guid("10d7d48c-d817-49a4-a2b9-9ad4b36e7713"),
                            SpeciesName = "Ferret"
                        });
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
                            UserId = new Guid("116483a4-a64c-483c-9c76-489a872fdec4"),
                            Addess = "123 ABc",
                            Email = "manh123@gmail.com",
                            Gender = "Male",
                            Password = "nsZXnLisYMRi5raBLsXJFWnp0G/cOmcXIe5wNwLRrJk=",
                            PhoneNumber = "0123456789",
                            RoleId = new Guid("f0463cb3-d4fc-4c68-8eca-bce793c8cbc0"),
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
                        .HasForeignKey("CaseId");

                    b.HasOne("DataObject.Order", "Order")
                        .WithMany("Invoices")
                        .HasForeignKey("OrderId");

                    b.HasOne("DataObject.Service", "Service")
                        .WithMany("_Invoices")
                        .HasForeignKey("ServiceId");

                    b.Navigation("Case");

                    b.Navigation("Order");

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
                        .HasForeignKey("CategoryId");

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
                    b.Navigation("Invoices");

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
