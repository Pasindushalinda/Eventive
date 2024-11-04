﻿// <auto-generated />
using System;
using Eventive.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eventive.Modules.Ticketing.Infrastructure.Database.Migrations
{
    [DbContext(typeof(TicketingDbContext))]
    partial class TicketingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ticketing")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Events.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Canceled")
                        .HasColumnType("boolean")
                        .HasColumnName("canceled");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndsAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ends_at_utc");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<DateTime>("StartsAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("starts_at_utc");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.ToTable("events", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Events.TicketType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("AvailableQuantity")
                        .HasColumnType("numeric")
                        .HasColumnName("available_quantity");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_ticket_types");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_ticket_types_event_id");

                    b.ToTable("ticket_types", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<bool>("TicketsIssued")
                        .HasColumnType("boolean")
                        .HasColumnName("tickets_issued");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("total_price");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_orders_customer_id");

                    b.ToTable("orders", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric")
                        .HasColumnName("quantity");

                    b.Property<Guid>("TicketTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("ticket_type_id");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_order_items");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_items_order_id");

                    b.HasIndex("TicketTypeId")
                        .HasDatabaseName("ix_order_items_ticket_type_id");

                    b.ToTable("order_items", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<decimal?>("AmountRefunded")
                        .HasColumnType("numeric")
                        .HasColumnName("amount_refunded");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<DateTime?>("RefundedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refunded_at_utc");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("transaction_id");

                    b.HasKey("Id")
                        .HasName("pk_payments");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_payments_order_id");

                    b.HasIndex("TransactionId")
                        .IsUnique()
                        .HasDatabaseName("ix_payments_transaction_id");

                    b.ToTable("payments", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Tickets.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean")
                        .HasColumnName("archived");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("TicketTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("ticket_type_id");

                    b.HasKey("Id")
                        .HasName("pk_tickets");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_tickets_code");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_tickets_customer_id");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_tickets_event_id");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_tickets_order_id");

                    b.HasIndex("TicketTypeId")
                        .HasDatabaseName("ix_tickets_ticket_type_id");

                    b.ToTable("tickets", "ticketing");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Events.TicketType", b =>
                {
                    b.HasOne("Eventive.Modules.Ticketing.Domain.Events.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_ticket_types_events_event_id");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Orders.Order", b =>
                {
                    b.HasOne("Eventive.Modules.Ticketing.Domain.Customers.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_customers_customer_id");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Orders.OrderItem", b =>
                {
                    b.HasOne("Eventive.Modules.Ticketing.Domain.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_orders_order_id");

                    b.HasOne("Eventive.Modules.Ticketing.Domain.Events.TicketType", null)
                        .WithMany()
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_ticket_types_ticket_type_id");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Payments.Payment", b =>
                {
                    b.HasOne("Eventive.Modules.Ticketing.Domain.Orders.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_payments_orders_order_id");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Tickets.Ticket", b =>
                {
                    b.HasOne("Eventive.Modules.Ticketing.Domain.Customers.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_customers_customer_id");

                    b.HasOne("Eventive.Modules.Ticketing.Domain.Events.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_events_event_id");

                    b.HasOne("Eventive.Modules.Ticketing.Domain.Orders.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_orders_order_id");

                    b.HasOne("Eventive.Modules.Ticketing.Domain.Events.TicketType", null)
                        .WithMany()
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tickets_ticket_types_ticket_type_id");
                });

            modelBuilder.Entity("Eventive.Modules.Ticketing.Domain.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
