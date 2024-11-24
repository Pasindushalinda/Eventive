﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventive.Modules.Attendance.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_consumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_message_consumers",
                schema: "attendance",
                columns: table => new
                {
                    outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_message_consumers",
                schema: "attendance");
        }
    }
}
