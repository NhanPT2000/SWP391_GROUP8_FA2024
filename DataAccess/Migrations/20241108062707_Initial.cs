using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Species",
                columns: table => new
                {
                    SpeciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeciesName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Species", x => x.SpeciesId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ContractPer = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(900)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    HasLimit = table.Column<bool>(type: "bit", nullable: true),
                    CostPerUnity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.WorkScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(900)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(Max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Addess = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    OnlineTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedEmail = table.Column<bool>(type: "bit", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Members_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTitle = table.Column<string>(type: "nvarchar(900)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Members_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Members",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Freight = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SpeciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(Max)", nullable: true),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Pets__Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "_Species",
                        principalColumn: "SpeciesId");
                });

            migrationBuilder.CreateTable(
                name: "StaffDetails",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DegreeImage = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    WorkScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffDetails", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_StaffDetails_Members_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffDetails_WorkSchedules_StaffId",
                        column: x => x.StaffId,
                        principalTable: "WorkSchedules",
                        principalColumn: "WorkScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<float>(type: "real", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK__OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Cases_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId");
                    table.ForeignKey(
                        name: "FK_Cases_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceCode = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeGenerated = table.Column<int>(type: "int", nullable: false),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TimeCharge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountCharge = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PayerId = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Invoices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "PlannedServices",
                columns: table => new
                {
                    PlannedServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedUnits = table.Column<int>(type: "int", nullable: false),
                    CostPerUnit = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedServices", x => x.PlannedServiceId);
                    table.ForeignKey(
                        name: "FK_PlannedServices_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                    table.ForeignKey(
                        name: "FK_PlannedServices_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_PlannedServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Voucherid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VourcherTitle = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VourcherDescription = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    PlannedServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Voucherid);
                    table.ForeignKey(
                        name: "FK_Vouchers_Members_UserId",
                        column: x => x.UserId,
                        principalTable: "Members",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Vouchers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Vouchers_PlannedServices_PlannedServiceId",
                        column: x => x.PlannedServiceId,
                        principalTable: "PlannedServices",
                        principalColumn: "PlannedServiceId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "IsDeleted", "Status" },
                values: new object[,]
                {
                    { new Guid("2e1b4c3f-2be3-4947-b320-a085cc7b47bf"), "Cat Food", null, "None" },
                    { new Guid("e031d5e4-76cb-4443-9c4b-41834e1abbba"), "Bird Food", null, "None" },
                    { new Guid("f90ecec0-f3fc-4bf0-8e6f-57caa29e857a"), "Dog Food", null, "None" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacilityId", "Address", "ContractPer", "FacilityName", "Image", "IsDeleted", "PhoneNumber" },
                values: new object[] { new Guid("84f782b1-c3b8-42f8-9d19-c075edcd567b"), "Floor 3, Room 201", "Doctor Joe", "Room Vet A1", "1.jpg", false, "0342231123" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("b2b46689-a9d5-44f1-9397-ca3b588268db"), "Admin", "None" },
                    { new Guid("ebc6b7cf-a0be-474b-9477-267ee0f72b9b"), "Staff", "None" },
                    { new Guid("f79cd537-47bd-46f3-a79f-cd7999aaa364"), "Member", "None" }
                });

            migrationBuilder.InsertData(
                table: "_Species",
                columns: new[] { "SpeciesId", "IsDelete", "SpeciesName" },
                values: new object[,]
                {
                    { new Guid("088482ad-40c1-4523-a8da-bf2ecf07fe6e"), null, "Dog" },
                    { new Guid("6fb70da5-6704-4344-90ce-6d8412bc7ee1"), null, "Parrot" },
                    { new Guid("c90876f5-c3a4-42c4-ada9-b48142537111"), null, "Cat" },
                    { new Guid("dee3c2ff-fab6-42f4-9ed0-afeeb3f53922"), null, "Ferret" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "UserId", "Addess", "ConfirmedEmail", "Email", "Gender", "OnlineTime", "Password", "PhoneNumber", "PhoneNumber2", "Profile", "RoleId", "UserName" },
                values: new object[] { new Guid("96999c36-824d-43fa-9a63-5d45a33ce255"), "123 ABc", null, "manh123@gmail.com", "Male", null, "nsZXnLisYMRi5raBLsXJFWnp0G/cOmcXIe5wNwLRrJk=", "0123456789", null, null, new Guid("b2b46689-a9d5-44f1-9397-ca3b588268db"), "Manh" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Image", "IsDeleted", "Origin", "ProductDescription", "ProductName", "UnitPrice", "UnitsInStock", "Weight" },
                values: new object[,]
                {
                    { new Guid("32e0f124-3c52-4832-87fe-2ba46122dece"), new Guid("e031d5e4-76cb-4443-9c4b-41834e1abbba"), "product_03.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Cat Food", 10000m, 0, 0.76m },
                    { new Guid("59a6b2d9-4d26-4145-80f1-6e2d6c69ea23"), new Guid("f90ecec0-f3fc-4bf0-8e6f-57caa29e857a"), "product_04.jpg", null, "England", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Bird Food", 10000m, 10, 12m },
                    { new Guid("60fe3de1-e5cd-4631-89e8-f01dc1900bf7"), new Guid("f90ecec0-f3fc-4bf0-8e6f-57caa29e857a"), "product_01.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m },
                    { new Guid("7555066d-9a1d-4e88-aa83-1f8e38664f0a"), new Guid("f90ecec0-f3fc-4bf0-8e6f-57caa29e857a"), "product_06.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m },
                    { new Guid("8f84818e-4519-4ecb-bd9a-05914ba056dc"), new Guid("2e1b4c3f-2be3-4947-b320-a085cc7b47bf"), "product_02.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m },
                    { new Guid("d92d3171-7364-4fef-b089-a3a96af2ef7c"), new Guid("e031d5e4-76cb-4443-9c4b-41834e1abbba"), "product_05.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 13m }
                });

            migrationBuilder.CreateIndex(
                name: "IX__OrderDetails_ProductId",
                table: "_OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FacilityId",
                table: "Cases",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PetId",
                table: "Cases",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminId",
                table: "Events",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_MemberId",
                table: "Feedbacks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProductId",
                table: "Feedbacks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ServiceId",
                table: "Feedbacks",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CaseId",
                table: "Invoices",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_RoleId",
                table: "Members",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MemberId",
                table: "Orders",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_MemberId",
                table: "Pets",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedServices_CaseId",
                table: "PlannedServices",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedServices_MemberId",
                table: "PlannedServices",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedServices_ServiceId",
                table: "PlannedServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_OrderId",
                table: "Vouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_PlannedServiceId",
                table: "Vouchers",
                column: "PlannedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_UserId",
                table: "Vouchers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_OrderDetails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "StaffDetails");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PlannedServices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "_Species");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
