﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiOfficecom.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240730131300_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonPhone", b =>
                {
                    b.Property<int>("BusinessEntityID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("PhoneNumberTypeID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("BusinessEntityID", "PhoneNumberTypeID");

                    b.HasIndex("PhoneNumberTypeID");

                    b.ToTable("PersonPhones");
                });

            modelBuilder.Entity("PhoneNumberType", b =>
                {
                    b.Property<int>("PhoneNumberTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneNumberTypeID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhoneNumberTypeID");

                    b.ToTable("PhoneNumberTypes");
                });

            modelBuilder.Entity("PersonPhone", b =>
                {
                    b.HasOne("PhoneNumberType", "PhoneNumberType")
                        .WithMany("PersonPhones")
                        .HasForeignKey("PhoneNumberTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhoneNumberType");
                });

            modelBuilder.Entity("PhoneNumberType", b =>
                {
                    b.Navigation("PersonPhones");
                });
#pragma warning restore 612, 618
        }
    }
}
