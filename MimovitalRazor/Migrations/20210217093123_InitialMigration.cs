using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MimovitalRazor.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CSVDataMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    levelBodyMotionL = table.Column<long>(type: "bigint", nullable: false),
                    levelHeartL = table.Column<long>(type: "bigint", nullable: false),
                    levelRespirationL = table.Column<long>(type: "bigint", nullable: false),
                    levelBodyMotionR = table.Column<long>(type: "bigint", nullable: false),
                    levelHeartR = table.Column<long>(type: "bigint", nullable: false),
                    levelRespirationR = table.Column<long>(type: "bigint", nullable: false),
                    occurDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    babyID = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    staffID = table.Column<string>(type: "text", nullable: true),
                    roomID = table.Column<string>(type: "text", nullable: true),
                    sensorID = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedBodyMotionL = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedHeartL = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedRespirationL = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedBodyMotionR = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedHeartR = table.Column<long>(type: "bigint", nullable: false),
                    levelLearnedRespirationR = table.Column<long>(type: "bigint", nullable: false),
                    averagingPeriodBodyMotion = table.Column<double>(type: "double precision", nullable: false),
                    averagingPeriodHeart = table.Column<double>(type: "double precision", nullable: false),
                    averagingPeriodRespiration = table.Column<double>(type: "double precision", nullable: false),
                    inputOffsetL = table.Column<double>(type: "double precision", nullable: false),
                    inputGainL = table.Column<double>(type: "double precision", nullable: false),
                    outputGainHeartL = table.Column<double>(type: "double precision", nullable: false),
                    outputGainRespirationL = table.Column<double>(type: "double precision", nullable: false),
                    inputOffsetR = table.Column<double>(type: "double precision", nullable: false),
                    inputGainR = table.Column<double>(type: "double precision", nullable: false),
                    outputGainHeartR = table.Column<double>(type: "double precision", nullable: false),
                    outputGainRespirationR = table.Column<double>(type: "double precision", nullable: false),
                    heartRate = table.Column<long>(type: "bigint", nullable: false),
                    respirationRate = table.Column<long>(type: "bigint", nullable: false),
                    minBodyMotion = table.Column<long>(type: "bigint", nullable: false),
                    minlHeart = table.Column<long>(type: "bigint", nullable: false),
                    minRespiration = table.Column<long>(type: "bigint", nullable: false),
                    maxBodyMotion = table.Column<long>(type: "bigint", nullable: false),
                    maxlHeart = table.Column<long>(type: "bigint", nullable: false),
                    maxRespiration = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSVDataMaster", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CSVDataMaster");
        }
    }
}
