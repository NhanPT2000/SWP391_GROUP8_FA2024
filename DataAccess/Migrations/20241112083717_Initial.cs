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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsCofirmed = table.Column<bool>(type: "bit", nullable: true)
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
                    { new Guid("019a0482-0f12-4114-857a-76cbb24e2818"), "Bird Food", null, "None" },
                    { new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"), "Dog Food", null, "None" },
                    { new Guid("ec3e268c-46bd-49ab-b161-55651e4475b0"), "Cat Food", null, "None" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacilityId", "Address", "ContractPer", "FacilityName", "Image", "IsDeleted", "PhoneNumber" },
                values: new object[] { new Guid("2a831e1f-4fb8-4b55-827b-7ff9b07c83c4"), "Floor 3, Room 201", "Doctor Joe", "Room Vet A1", "1.jpg", false, "0342231123" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("12d976c3-3591-43f3-a4cf-250185abfdd6"), "Staff", "None" },
                    { new Guid("7fb2f782-d814-4299-8a89-05dd3848b05a"), "Member", "None" },
                    { new Guid("f0463cb3-d4fc-4c68-8eca-bce793c8cbc0"), "Admin", "None" }
                });

            migrationBuilder.InsertData(
                table: "_Species",
                columns: new[] { "SpeciesId", "IsDelete", "SpeciesName" },
                values: new object[,]
                {
                    { new Guid("10d7d48c-d817-49a4-a2b9-9ad4b36e7713"), null, "Ferret" },
                    { new Guid("3068c789-140c-4b25-94fb-602efd40eec2"), null, "Parrot" },
                    { new Guid("7595f6d0-d560-49d1-8857-dfcb7104e58f"), null, "Cat" },
                    { new Guid("f1393ebe-6458-446e-b216-d53cd02eea02"), null, "Dog" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "UserId", "Addess", "ConfirmedEmail", "Email", "Gender", "OnlineTime", "Password", "PhoneNumber", "PhoneNumber2", "Profile", "RoleId", "UserName" },
                values: new object[] { new Guid("116483a4-a64c-483c-9c76-489a872fdec4"), "123 ABc", null, "manh123@gmail.com", "Male", null, "nsZXnLisYMRi5raBLsXJFWnp0G/cOmcXIe5wNwLRrJk=", "0123456789", null, null, new Guid("f0463cb3-d4fc-4c68-8eca-bce793c8cbc0"), "Manh" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Image", "IsDeleted", "Origin", "ProductDescription", "ProductName", "UnitPrice", "UnitsInStock", "Weight" },
                values: new object[,]
                {
                    { new Guid("1441651e-06eb-4c78-b4a4-8f9fc91e8a95"), new Guid("019a0482-0f12-4114-857a-76cbb24e2818"), "product_03.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Cat Food", 10000m, 0, 0.76m },
                    { new Guid("50ab2adf-ac46-4490-8b94-9e9dc7af08f9"), new Guid("ec3e268c-46bd-49ab-b161-55651e4475b0"), "product_02.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m },
                    { new Guid("539133a7-d2df-4748-8058-c289cf8dcc4c"), new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"), "product_04.jpg", null, "England", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Bird Food", 10000m, 10, 12m },
                    { new Guid("77228607-c331-45f9-8ca3-d6a61da48523"), new Guid("019a0482-0f12-4114-857a-76cbb24e2818"), "product_05.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 13m },
                    { new Guid("ae7e1aeb-c278-46b5-bd7d-c70534a6d223"), new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"), "product_06.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m },
                    { new Guid("f7ea46c9-f5e5-40ab-b0c5-49af957899e0"), new Guid("8afd8cc2-5acb-48e6-a394-07749d5470d3"), "product_01.jpg", null, "Viet Nam", "High-quality ingredients, Rich in vitamins and minerals, Supports healthy growth, Suitable for all breeds", "Premium Dog Food", 10000m, 1, 12m }
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
