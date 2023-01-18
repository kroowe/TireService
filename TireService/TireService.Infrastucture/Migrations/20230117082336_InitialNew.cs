using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TireService.Infrastructure.Migrations
{
    public partial class InitialNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:ltree", ",,");

            migrationBuilder.CreateTable(
                name: "AppSettingConstants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettingConstants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CategoryPath = table.Column<string>(type: "ltree", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    CarNumber = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseNomenclatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Article = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseNomenclatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    IsDismissed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    FirstVersionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogItems_FirstVersionId",
                        column: x => x.FirstVersionId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CatalogItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseItemHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseNomenclatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WarehouseItemHistoryType = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<double>(type: "double precision", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseItemHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseItemHistory_WarehouseNomenclatures_WarehouseNomenc~",
                        column: x => x.WarehouseNomenclatureId,
                        principalTable: "WarehouseNomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SalaryType = table.Column<int>(type: "integer", nullable: false),
                    SumBySalaryType = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRules_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPaymentsToWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPaymentsToWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryPaymentsToWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftWorkWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShiftWorkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftWorkWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftWorkWorkers_ShiftWorks_ShiftWorkId",
                        column: x => x.ShiftWorkId,
                        principalTable: "ShiftWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftWorkWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerBalances_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShiftWorkWorkerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    ClientComment = table.Column<string>(type: "text", nullable: true),
                    CloseComment = table.Column<string>(type: "text", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShiftWorkWorkers_ShiftWorkWorkerId",
                        column: x => x.ShiftWorkWorkerId,
                        principalTable: "ShiftWorkWorkers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CatalogItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskOrders_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CategoryId",
                table: "CatalogItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_FirstVersionId",
                table: "CatalogItems",
                column: "FirstVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShiftWorkWorkerId",
                table: "Orders",
                column: "ShiftWorkWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRules_WorkerId",
                table: "PaymentRules",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPaymentsToWorkers_WorkerId",
                table: "SalaryPaymentsToWorkers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftWorkWorkers_ShiftWorkId",
                table: "ShiftWorkWorkers",
                column: "ShiftWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftWorkWorkers_WorkerId_ShiftWorkId",
                table: "ShiftWorkWorkers",
                columns: new[] { "WorkerId", "ShiftWorkId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskOrders_CatalogItemId",
                table: "TaskOrders",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskOrders_OrderId",
                table: "TaskOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItemHistory_WarehouseNomenclatureId",
                table: "WarehouseItemHistory",
                column: "WarehouseNomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerBalances_WorkerId",
                table: "WorkerBalances",
                column: "WorkerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettingConstants");

            migrationBuilder.DropTable(
                name: "PaymentRules");

            migrationBuilder.DropTable(
                name: "SalaryPaymentsToWorkers");

            migrationBuilder.DropTable(
                name: "TaskOrders");

            migrationBuilder.DropTable(
                name: "WarehouseItemHistory");

            migrationBuilder.DropTable(
                name: "WorkerBalances");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WarehouseNomenclatures");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ShiftWorkWorkers");

            migrationBuilder.DropTable(
                name: "ShiftWorks");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
