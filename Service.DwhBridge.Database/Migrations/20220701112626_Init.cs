using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhBridge.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bridge");

            migrationBuilder.CreateTable(
                name: "EarnDashboardAssets",
                schema: "bridge",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnDashboardAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EarnDashboardAssetsByDay",
                schema: "bridge",
                columns: table => new
                {
                    AssetSymbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClientInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_PayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnDashboardAssetsByDay", x => new { x.TimeStamp, x.AssetSymbol });
                });

            migrationBuilder.CreateTable(
                name: "EarnDashboardTotal",
                schema: "bridge",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnDashboardTotal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EarnDashboardTotalByDay",
                schema: "bridge",
                columns: table => new
                {
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClientInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SimpleInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_AmountUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_Apy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetInfo_InterestPayoutUsd = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnDashboardTotalByDay", x => x.TimeStamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EarnDashboardAssets",
                schema: "bridge");

            migrationBuilder.DropTable(
                name: "EarnDashboardAssetsByDay",
                schema: "bridge");

            migrationBuilder.DropTable(
                name: "EarnDashboardTotal",
                schema: "bridge");

            migrationBuilder.DropTable(
                name: "EarnDashboardTotalByDay",
                schema: "bridge");
        }
    }
}
