﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SinglePage.Data;

#nullable disable

namespace SinglePage.Data.Migrations
{
    [DbContext(typeof(SinglePageContext))]
    partial class SinglePageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SinglePage.Data.Models.VirtualServer", b =>
                {
                    b.Property<int>("VirtualServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VirtualServerId"), 1L, 1);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RemoveDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("VirtualServerId");

                    b.ToTable("VirtualServers");
                });
#pragma warning restore 612, 618
        }
    }
}
