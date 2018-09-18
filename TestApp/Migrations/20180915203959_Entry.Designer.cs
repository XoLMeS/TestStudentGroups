﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp.Models;

namespace TestApp.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20180915203959_Entry")]
    partial class Entry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestApp.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<int>("Maxcap");

                    b.Property<int>("Number");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TestApp.Models.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Average_score");

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("Exp");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
