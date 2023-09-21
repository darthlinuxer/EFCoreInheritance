﻿// <auto-generated />
using System;
using Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TPT.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Models.Article", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Models.Person", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Models.Comment", b =>
                {
                    b.HasBaseType("Models.Article");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Models.PostModel", b =>
                {
                    b.HasBaseType("Models.Article");

                    b.Property<int?>("AuthorId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasIndex("AuthorId");

                    b.ToTable("PostModel");
                });

            modelBuilder.Entity("Models.Author", b =>
                {
                    b.HasBaseType("Models.Person");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("Models.PublicUser", b =>
                {
                    b.HasBaseType("Models.Person");

                    b.ToTable("PublicUser");
                });

            modelBuilder.Entity("Models.Comment", b =>
                {
                    b.HasOne("Models.Article", null)
                        .WithOne()
                        .HasForeignKey("Models.Comment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Comment", "ParentComment")
                        .WithMany("Comments")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Person", "Person")
                        .WithMany("Comments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.PostModel", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentComment");

                    b.Navigation("Person");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Models.PostModel", b =>
                {
                    b.HasOne("Models.Author", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Article", null)
                        .WithOne()
                        .HasForeignKey("Models.PostModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Models.Author", b =>
                {
                    b.HasOne("Models.Person", null)
                        .WithOne()
                        .HasForeignKey("Models.Author", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.PublicUser", b =>
                {
                    b.HasOne("Models.Person", null)
                        .WithOne()
                        .HasForeignKey("Models.PublicUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Person", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Models.Comment", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Models.PostModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Models.Author", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
