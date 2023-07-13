using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveGallery.Infrastructure.Migrations;
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.id);
                    table.ForeignKey(
                        name: "FK_Gallery_Artist",
                        column: x => x.InitiatorId,
                        principalTable: "Artists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Artwork_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Artwork_Category",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Artwork_Gallery",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "GalleriesArtist",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    GalleryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleriesArtist", x => x.id);
                    table.ForeignKey(
                        name: "FK_GalleryArtist_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_GalleryArtist_Gallery",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ArtistId",
                table: "Artworks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_CategoryId",
                table: "Artworks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_GalleryId",
                table: "Artworks",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_InitiatorId",
                table: "Galleries",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleriesArtist_ArtistId",
                table: "GalleriesArtist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleriesArtist_GalleryId",
                table: "GalleriesArtist",
                column: "GalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "GalleriesArtist");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }

