﻿// <auto-generated />
using System;
using Backend_Repositor.io.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230608154716_profilePic")]
    partial class profilePic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Backend_Repositor.io.Models.Archivo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime?>("FechaSubida")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filepath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RepositorioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RepositorioId");

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Relacion", b =>
                {
                    b.Property<long>("SeguidorId")
                        .HasColumnType("bigint");

                    b.Property<long>("SeguidoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.HasKey("SeguidorId", "SeguidoId");

                    b.HasIndex("SeguidoId");

                    b.ToTable("Relaciones");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Repositorio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaMod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Repositorios");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Archivo", b =>
                {
                    b.HasOne("Backend_Repositor.io.Models.Repositorio", "Repositorio")
                        .WithMany("Archivos")
                        .HasForeignKey("RepositorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repositorio");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Relacion", b =>
                {
                    b.HasOne("Backend_Repositor.io.Models.User", "Seguidor")
                        .WithMany("Seguido")
                        .HasForeignKey("SeguidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Backend_Repositor.io.Models.User", "Seguido")
                        .WithMany("Seguidor")
                        .HasForeignKey("SeguidorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Seguido");

                    b.Navigation("Seguidor");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Repositorio", b =>
                {
                    b.HasOne("Backend_Repositor.io.Models.User", "Usuario")
                        .WithMany("Repositorios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.Repositorio", b =>
                {
                    b.Navigation("Archivos");
                });

            modelBuilder.Entity("Backend_Repositor.io.Models.User", b =>
                {
                    b.Navigation("Repositorios");

                    b.Navigation("Seguido");

                    b.Navigation("Seguidor");
                });
#pragma warning restore 612, 618
        }
    }
}
