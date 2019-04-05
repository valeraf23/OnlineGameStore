using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Genres_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlatformTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatformType",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    PlatformTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatformType", x => new { x.GameId, x.PlatformTypeId });
                    table.ForeignKey(
                        name: "FK_GamePlatformType_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatformType_PlatformTypes_PlatformTypeId",
                        column: x => x.PlatformTypeId,
                        principalTable: "PlatformTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("add83210-313f-47f4-ae78-ebe248c88e70"), "Action", null },
                    { new Guid("3d175368-0858-497f-baab-e118ed96b581"), "Adventure", null },
                    { new Guid("dde9588c-891a-4a45-bb4d-97c73876db16"), "Misc", null },
                    { new Guid("79417700-8164-4235-a59e-fabac36f0630"), "PuzzleSkill", null },
                    { new Guid("1c7650ec-4f9f-4999-b78d-2079d2b4e9fb"), "RPG", null },
                    { new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433"), "Races", null },
                    { new Guid("02553495-4059-455f-8554-e6be30a5228e"), "Sports", null },
                    { new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b"), "Strategy", null }
                });

            migrationBuilder.InsertData(
                table: "PlatformTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("1c7650ec-4f9f-4999-b78d-2079d2b4e9fb"), "Browser" },
                    { new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433"), "Console" },
                    { new Guid("02553495-4059-455f-8554-e6be30a5228e"), "Desktop" },
                    { new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b"), "Mobile" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("28efabea-38b8-4166-8bea-e6954716683c"), "FPS", new Guid("add83210-313f-47f4-ae78-ebe248c88e70") },
                    { new Guid("dbb44617-ec8d-412b-bbbf-ee25eb0e91ac"), "TPS", new Guid("add83210-313f-47f4-ae78-ebe248c88e70") },
                    { new Guid("3e331a5f-88e4-434c-b820-dd3ee3299d2d"), "rally", new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433") },
                    { new Guid("0387d3ee-1c21-4620-9a0f-02e1ee5a7f96"), "arcade", new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433") },
                    { new Guid("a9d17356-1c52-458e-a0bb-51740b3e1688"), "formula", new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433") },
                    { new Guid("4cd13094-ee40-4a31-b781-c9fca7196bc1"), "off-road", new Guid("7cd7d02c-eb2c-4050-bda3-44791b159433") },
                    { new Guid("de521ddc-8aa1-41f5-978e-5bea90970ee6"), "RTS", new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b") },
                    { new Guid("0713dfb5-c361-400b-a7ff-d1f9ef0c7d80"), "TBS", new Guid("4b6f4c7f-eb42-4ce1-9ece-882b63fc662b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatformType_PlatformTypeId",
                table: "GamePlatformType",
                column: "PlatformTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ParentId",
                table: "Genres",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformTypes_Type",
                table: "PlatformTypes",
                column: "Type",
                unique: true,
                filter: "[Type] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatformType");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PlatformTypes");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
