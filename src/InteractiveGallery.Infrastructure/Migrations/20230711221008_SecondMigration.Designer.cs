﻿// <auto-generated />
using InteractiveGallery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InteractiveGallery.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230711221008_SecondMigration")]
    partial class SecondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("biography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Artwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("GalleryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GalleryId");

                    b.ToTable("Artworks");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.GalleryArtist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("GalleryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GalleryId");

                    b.ToTable("GalleriesArtist");
                });

            modelBuilder.Entity("InteractiveGallery.Core.GalleryAggregate.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InitiatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Theme")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("theme");

                    b.HasKey("Id");

                    b.HasIndex("InitiatorId");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Artwork", b =>
                {
                    b.HasOne("InteractiveGallery.Core.ArtistAggregate.Artist", "Artist")
                        .WithMany("Artworks")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_Artwork_Artist");

                    b.HasOne("InteractiveGallery.Core.ArtistAggregate.Category", "Category")
                        .WithMany("Artworks")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Artwork_Category");

                    b.HasOne("InteractiveGallery.Core.GalleryAggregate.Gallery", "Gallery")
                        .WithMany("Artworks")
                        .HasForeignKey("GalleryId")
                        .IsRequired()
                        .HasConstraintName("FK_Artwork_Gallery");

                    b.Navigation("Artist");

                    b.Navigation("Category");

                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.GalleryArtist", b =>
                {
                    b.HasOne("InteractiveGallery.Core.ArtistAggregate.Artist", "Artist")
                        .WithMany("JoinedGalleries")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_GalleryArtist_Artist");

                    b.HasOne("InteractiveGallery.Core.GalleryAggregate.Gallery", "Gallery")
                        .WithMany("Artists")
                        .HasForeignKey("GalleryId")
                        .IsRequired()
                        .HasConstraintName("FK_GalleryArtist_Gallery");

                    b.Navigation("Artist");

                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("InteractiveGallery.Core.GalleryAggregate.Gallery", b =>
                {
                    b.HasOne("InteractiveGallery.Core.ArtistAggregate.Artist", "InitiatorArtist")
                        .WithMany("Galleries")
                        .HasForeignKey("InitiatorId")
                        .IsRequired()
                        .HasConstraintName("FK_Gallery_Artist");

                    b.Navigation("InitiatorArtist");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Artist", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Galleries");

                    b.Navigation("JoinedGalleries");
                });

            modelBuilder.Entity("InteractiveGallery.Core.ArtistAggregate.Category", b =>
                {
                    b.Navigation("Artworks");
                });

            modelBuilder.Entity("InteractiveGallery.Core.GalleryAggregate.Gallery", b =>
                {
                    b.Navigation("Artists");

                    b.Navigation("Artworks");
                });
#pragma warning restore 612, 618
        }
    }
}
