using Microsoft.EntityFrameworkCore.Migrations;

namespace IRunes.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Tracks");

            migrationBuilder.CreateTable(
                name: "TracksAlbums",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TrackId = table.Column<string>(nullable: true),
                    AlbumId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TracksAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TracksAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TracksAlbums_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TracksAlbums_AlbumId",
                table: "TracksAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_TracksAlbums_TrackId",
                table: "TracksAlbums",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TracksAlbums");

            migrationBuilder.AddColumn<string>(
                name: "AlbumId",
                table: "Tracks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
