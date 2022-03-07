﻿// <auto-generated />
using System;
using DotNet6Mediator.InfrastructureLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNet6Mediator.InfrastructureLayer.Migrations.SqlServerMigrations
{
    [DbContext(typeof(DatabaseCtxSqlServer))]
    partial class DatabaseCtxModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DotNet6Mediator.DomainLayer.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("RoleName");

                    b.HasKey("Id");

                    b.ToTable("RoleTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("DotNet6Mediator.DomainLayer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("BirthDate");

                    b.Property<int>("FK_Role")
                        .HasMaxLength(30)
                        .HasColumnType("integer")
                        .HasColumnName("FK_Role");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Password");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Surname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("FK_Role");

                    b.ToTable("UserTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1996, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FK_Role = 2,
                            Name = "Tommaso",
                            Password = "tomPwd",
                            Surname = "Zazzaretti",
                            Username = "Tom96"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1996, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FK_Role = 1,
                            Name = "Giorgio",
                            Password = "gioPwd",
                            Surname = "Zazzaretti",
                            Username = "Gio96"
                        });
                });

            modelBuilder.Entity("DotNet6Mediator.DomainLayer.Entities.User", b =>
                {
                    b.HasOne("DotNet6Mediator.DomainLayer.Entities.Role", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("FK_Role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DotNet6Mediator.DomainLayer.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
