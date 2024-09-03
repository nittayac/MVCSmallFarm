using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCSmallFarm.Migrations
{
    /// <inheritdoc />
    public partial class InitIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastCommentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Category",
            //    columns: table => new
            //    {
            //        CategoryId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Category", x => x.CategoryId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ECommerceSummary",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductNet = table.Column<int>(type: "int", nullable: false),
            //        MemberNet = table.Column<int>(type: "int", nullable: false),
            //        LimitToComment = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ECommerceSummary", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Order",
            //    columns: table => new
            //    {
            //        OrderId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
            //        UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        OrderNo = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        ReceiveDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        PaidDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        NetDC = table.Column<double>(type: "float", nullable: true),
            //        VATRate = table.Column<double>(type: "float", nullable: true),
            //        NetVAT = table.Column<double>(type: "float", nullable: true),
            //        Net = table.Column<double>(type: "float", nullable: true),
            //        IsReceived = table.Column<bool>(type: "bit", nullable: false),
            //        IsPaid = table.Column<bool>(type: "bit", nullable: false),
            //        IsNormal = table.Column<bool>(type: "bit", nullable: false),
            //        UserIp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
            //        UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Order", x => x.OrderId);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        ProductId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        ImageUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        InStock = table.Column<int>(type: "int", nullable: true),
            //        SoldTotals = table.Column<int>(type: "int", nullable: true),
            //        ViewTotals = table.Column<int>(type: "int", nullable: true),
            //        ShipDateDuration = table.Column<int>(type: "int", nullable: true),
            //        OnePoint = table.Column<int>(type: "int", nullable: true),
            //        TwoPoint = table.Column<int>(type: "int", nullable: true),
            //        ThreePoint = table.Column<int>(type: "int", nullable: true),
            //        FourPoint = table.Column<int>(type: "int", nullable: true),
            //        FivePoint = table.Column<int>(type: "int", nullable: true),
            //        PointTotals = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
            //        CommentTotals = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
            //        AveragePoint = table.Column<double>(type: "float", nullable: true),
            //        IsNormal = table.Column<bool>(type: "bit", nullable: false),
            //        IsPromotion = table.Column<bool>(type: "bit", nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
            //        UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.ProductId);
            //        table.ForeignKey(
            //            name: "FK_Product_Category",
            //            column: x => x.CategoryId,
            //            principalTable: "Category",
            //            principalColumn: "CategoryId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderDetail",
            //    columns: table => new
            //    {
            //        OrderId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
            //        DiscountPerUnit = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
            //        TotalDiscount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
            //        Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderDetail", x => new { x.OrderId, x.ProductId });
            //        table.ForeignKey(
            //            name: "FK_OrderDetail_Order",
            //            column: x => x.OrderId,
            //            principalTable: "Order",
            //            principalColumn: "OrderId");
            //        table.ForeignKey(
            //            name: "FK_OrderDetail_Product",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductWithComment",
            //    columns: table => new
            //    {
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        RunningNumber = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        CommentDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        UserIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        IsShow = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductWithComment_1", x => new { x.ProductId, x.RunningNumber });
            //        table.ForeignKey(
            //            name: "FK_ProductWithComment_Product",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ShoppingCartItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        ShoppingCartId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShoppingCartItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ShoppingCartItem_Product",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserWithPoint",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        PointDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Point = table.Column<int>(type: "int", nullable: false),
            //        UserIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserWithPoint", x => new { x.UserId, x.ProductId });
            //        table.ForeignKey(
            //            name: "FK_UserWithPoint_Product",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId");
            //    });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a87cf669-47df-4f83-80b6-916fc3fec1e6", null, "Admin", "ADMIN" },
                    { "d9513d6f-c65d-4e6f-897f-5bcb89651e13", null, "Member", "MEMBER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetail_ProductId",
            //    table: "OrderDetail",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_CategoryId",
            //    table: "Product",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShoppingCartItem_ProductId",
            //    table: "ShoppingCartItem",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserWithPoint_ProductId",
            //    table: "UserWithPoint",
            //    column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "ECommerceSummary");

            //migrationBuilder.DropTable(
            //    name: "OrderDetail");

            //migrationBuilder.DropTable(
            //    name: "ProductWithComment");

            //migrationBuilder.DropTable(
            //    name: "ShoppingCartItem");

            //migrationBuilder.DropTable(
            //    name: "UserWithPoint");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "Order");

            //migrationBuilder.DropTable(
            //    name: "Product");

            //migrationBuilder.DropTable(
            //    name: "Category");
        }
    }
}
