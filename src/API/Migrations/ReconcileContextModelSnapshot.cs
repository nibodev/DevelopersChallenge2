﻿// <auto-generated />
using System;
using API.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(ReconcileContext))]
    partial class ReconcileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Domain.BankAccount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<string>("BanckId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("API.Domain.ImportedFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankAccountId");

                    b.Property<string>("FileContent");

                    b.Property<string>("FileName");

                    b.Property<DateTime>("ImportDate");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.ToTable("ImportedFiles");
                });

            modelBuilder.Entity("API.Domain.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Ammount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<Guid?>("FileId");

                    b.Property<bool>("Reconciled");

                    b.Property<short>("Type");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("API.Domain.ImportedFile", b =>
                {
                    b.HasOne("API.Domain.BankAccount", "BankAccount")
                        .WithMany("Files")
                        .HasForeignKey("BankAccountId");
                });

            modelBuilder.Entity("API.Domain.Transaction", b =>
                {
                    b.HasOne("API.Domain.ImportedFile", "File")
                        .WithMany("Transactions")
                        .HasForeignKey("FileId");
                });
#pragma warning restore 612, 618
        }
    }
}
