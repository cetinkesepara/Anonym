﻿// <auto-generated />
using System;
using Core.DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Anonym.DataAccess.Migrations
{
    [DbContext(typeof(AnonymContext))]
    [Migration("20200803070743_Add_Category_ChatMessage_ChatRoom_Tables")]
    partial class Add_Category_ChatMessage_ChatRoom_Tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Anonym.Entities.Concrete.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnName("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories","dbo");
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.ChatMessage", b =>
                {
                    b.Property<string>("ChatMessageId")
                        .HasColumnName("ChatMessageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChatRoomId")
                        .IsRequired()
                        .HasColumnName("ChatRoomId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayUserName")
                        .HasColumnName("DisplayUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublisherMessage")
                        .HasColumnName("IsPublisherMessage")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnName("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatMessages","dbo");
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.ChatRoom", b =>
                {
                    b.Property<string>("ChatRoomId")
                        .HasColumnName("ChatRoomId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnName("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PublisherCommented")
                        .HasColumnName("PublisherCommented")
                        .HasColumnType("bit");

                    b.Property<string>("PublisherName")
                        .HasColumnName("PublisherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewerName")
                        .HasColumnName("ReviewerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewerUserId")
                        .HasColumnName("ReviewerUserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatRoomId");

                    b.HasIndex("PostId");

                    b.ToTable("ChatRooms","dbo");
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.Post", b =>
                {
                    b.Property<string>("PostId")
                        .HasColumnName("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnName("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatePostDate")
                        .HasColumnName("CreatePostDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnName("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnName("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts","dbo");
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.ChatMessage", b =>
                {
                    b.HasOne("Anonym.Entities.Concrete.ChatRoom", "ChatRoom")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.ChatRoom", b =>
                {
                    b.HasOne("Anonym.Entities.Concrete.Post", "Post")
                        .WithMany("ChatRooms")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Anonym.Entities.Concrete.Post", b =>
                {
                    b.HasOne("Anonym.Entities.Concrete.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
