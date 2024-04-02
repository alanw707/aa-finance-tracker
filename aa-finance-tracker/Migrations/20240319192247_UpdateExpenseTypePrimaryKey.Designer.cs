﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aa_finance_tracker.Data;

#nullable disable

namespace aa_finance_tracker.Migrations
{
    [DbContext(typeof(FinanceTrackerDbContext))]
    [Migration("20240319192247_UpdateExpenseTypePrimaryKey")]
    partial class UpdateExpenseTypePrimaryKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("aa_finance_tracker.Data.ExpenseType", b =>
                {
                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ExpenseTypes");
                });

            modelBuilder.Entity("aa_finance_tracker.Domains.ExpenseCategory", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ColourHex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("ExpensesCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
