﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostOffice.Shared.Models;

namespace PostOffice.Migrations
{
    [DbContext(typeof(PostOfficeDbContext))]
    partial class PostOfficeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PostOffice.Shared.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique()
                        .HasFilter("[Label] IS NOT NULL");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PostOffice.Shared.Models.Copy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PostAccountId");

                    b.Property<int>("PostItemId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("PostAccountId");

                    b.HasIndex("PostItemId");

                    b.ToTable("Copy");
                });

            modelBuilder.Entity("PostOffice.Shared.Models.PostItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique()
                        .HasFilter("[Url] IS NOT NULL");

                    b.ToTable("PostItems");
                });

            modelBuilder.Entity("PostOffice.Shared.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CopyId");

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.HasIndex("CopyId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PostOffice.Shared.Models.Copy", b =>
                {
                    b.HasOne("PostOffice.Shared.Models.Account", "PostAccount")
                        .WithMany()
                        .HasForeignKey("PostAccountId");

                    b.HasOne("PostOffice.Shared.Models.PostItem", "PostItem")
                        .WithMany("Copy")
                        .HasForeignKey("PostItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PostOffice.Shared.Models.Tag", b =>
                {
                    b.HasOne("PostOffice.Shared.Models.Copy", "Copy")
                        .WithMany("Tags")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
