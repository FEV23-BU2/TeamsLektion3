﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamsLektion3;

#nullable disable

namespace TeamsLektion3.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240220090933_AddSuperPower")]
    partial class AddSuperPower
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TeamsLektion3.SuperHero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SuperHeroes");
                });

            modelBuilder.Entity("TeamsLektion3.SuperPower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PowerLevel")
                        .HasColumnType("integer");

                    b.Property<int>("SuperHeroId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SuperHeroId");

                    b.ToTable("SuperPowers");
                });

            modelBuilder.Entity("TeamsLektion3.SuperPower", b =>
                {
                    b.HasOne("TeamsLektion3.SuperHero", "SuperHero")
                        .WithMany("SuperPowers")
                        .HasForeignKey("SuperHeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SuperHero");
                });

            modelBuilder.Entity("TeamsLektion3.SuperHero", b =>
                {
                    b.Navigation("SuperPowers");
                });
#pragma warning restore 612, 618
        }
    }
}