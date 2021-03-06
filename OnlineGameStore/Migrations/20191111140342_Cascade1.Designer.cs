﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineGameStore.Data.Data;

namespace OnlineGameStore.Api.Migrations
{
    [DbContext(typeof(OnlineGameContext))]
    [Migration("20191111140342_Cascade1")]
    partial class Cascade1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<Guid?>("GameId");

                    b.Property<Guid?>("GameId1");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GameId1");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("PublisherId");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.GameGenre", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("GenreId");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.GamePlatformType", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<Guid>("PlatformTypeId");

                    b.HasKey("GameId", "PlatformTypeId");

                    b.HasIndex("PlatformTypeId");

                    b.ToTable("GamePlatformType");
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("add83210-313f-47f4-ae78-ebe248c88e70"),
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("3d175368-0858-497f-baab-e118ed96b581"),
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("dde9588c-891a-4a45-bb4d-97c73876db16"),
                            Name = "Misc"
                        },
                        new
                        {
                            Id = new Guid("79417700-8164-4235-a59e-fabac36f0630"),
                            Name = "PuzzleSkill"
                        },
                        new
                        {
                            Id = new Guid("1c7650ec-4f9f-4999-b78d-2079d2b4e9fb"),
                            Name = "RPG"
                        },
                        new
                        {
                            Id = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433"),
                            Name = "Races"
                        },
                        new
                        {
                            Id = new Guid("02553495-4059-455f-8554-e6be30a5228e"),
                            Name = "Sports"
                        },
                        new
                        {
                            Id = new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b"),
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("28efabea-38b8-4166-8bea-e6954716683c"),
                            Name = "FPS",
                            ParentId = new Guid("add83210-313f-47f4-ae78-ebe248c88e70")
                        },
                        new
                        {
                            Id = new Guid("dbb44617-ec8d-412b-bbbf-ee25eb0e91ac"),
                            Name = "TPS",
                            ParentId = new Guid("add83210-313f-47f4-ae78-ebe248c88e70")
                        },
                        new
                        {
                            Id = new Guid("3e331a5f-88e4-434c-b820-dd3ee3299d2d"),
                            Name = "rally",
                            ParentId = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433")
                        },
                        new
                        {
                            Id = new Guid("0387d3ee-1c21-4620-9a0f-02e1ee5a7f96"),
                            Name = "arcade",
                            ParentId = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433")
                        },
                        new
                        {
                            Id = new Guid("a9d17356-1c52-458e-a0bb-51740b3e1688"),
                            Name = "formula",
                            ParentId = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433")
                        },
                        new
                        {
                            Id = new Guid("4cd13094-ee40-4a31-b781-c9fca7196bc1"),
                            Name = "off-road",
                            ParentId = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433")
                        },
                        new
                        {
                            Id = new Guid("de521ddc-8aa1-41f5-978e-5bea90970ee6"),
                            Name = "RTS",
                            ParentId = new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b")
                        },
                        new
                        {
                            Id = new Guid("0713dfb5-c361-400b-a7ff-d1f9ef0c7d80"),
                            Name = "TBS",
                            ParentId = new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b")
                        });
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.PlatformType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique()
                        .HasFilter("[Type] IS NOT NULL");

                    b.ToTable("PlatformTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1c7650ec-4f9f-4999-b78d-2079d2b4e9fb"),
                            Type = "Browser"
                        },
                        new
                        {
                            Id = new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433"),
                            Type = "Console"
                        },
                        new
                        {
                            Id = new Guid("02553495-4059-455f-8554-e6be30a5228e"),
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b"),
                            Type = "Mobile"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Comment", b =>
                {
                    b.HasOne("OnlineGameStore.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineGameStore.Domain.Entities.Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId1");

                    b.HasOne("OnlineGameStore.Domain.Entities.Comment", "ParentComment")
                        .WithMany("Answers")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Game", b =>
                {
                    b.HasOne("OnlineGameStore.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.GameGenre", b =>
                {
                    b.HasOne("OnlineGameStore.Domain.Entities.Game", "Game")
                        .WithMany("GameGenre")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineGameStore.Domain.Entities.Genre", "Genre")
                        .WithMany("GameGenre")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.GamePlatformType", b =>
                {
                    b.HasOne("OnlineGameStore.Domain.Entities.Game", "Game")
                        .WithMany("GamePlatformType")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineGameStore.Domain.Entities.PlatformType", "PlatformType")
                        .WithMany("Games")
                        .HasForeignKey("PlatformTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineGameStore.Domain.Entities.Genre", b =>
                {
                    b.HasOne("OnlineGameStore.Domain.Entities.Genre", "ParentGenre")
                        .WithMany("SubGenres")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
