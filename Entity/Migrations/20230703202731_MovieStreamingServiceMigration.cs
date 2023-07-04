using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class MovieStreamingServiceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    genere = table.Column<string>(type: "text", nullable: false),
                    director = table.Column<string>(type: "text", nullable: false),
                    actor = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<decimal>(type: "numeric", nullable: false),
                    subscription_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.id);
                    table.ForeignKey(
                        name: "FK_movie_subscription_subscription_id",
                        column: x => x.subscription_id,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    card_number = table.Column<string>(type: "text", nullable: false),
                    card_holder_name = table.Column<string>(type: "text", nullable: false),
                    expire_date = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_payment_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    subscription_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_profile_subscription_subscription_id",
                        column: x => x.subscription_id,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profile_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "upi_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    upi_id = table.Column<string>(type: "text", nullable: false),
                    upi_app = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upi_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_upi_payment_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "subscription",
                columns: new[] { "id", "created_by", "created_on", "description", "is_active", "key", "updated_by", "updated_on" },
                values: new object[,]
                {
                    { new Guid("7ec4efca-0a55-4bc1-827d-a7692a293e4a"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3286), "This plan offers standard definition (SD) streaming on one device at a time. It's a cost-effective option for individuals or budget-conscious users", true, "BASIC", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3286) },
                    { new Guid("c486ca23-b288-478c-973a-de75595d6b37"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3289), "This plan provides high definition (HD) streaming on up to two devices simultaneously. It is suitable for users who prefer better video quality and want to share their account with family members", true, "STANDARD", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3289) },
                    { new Guid("c5d19446-4c0f-450b-936a-b998c8c4e438"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3292), "The free plan offers limited access to the streaming site's content library", true, "FREE", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3292) },
                    { new Guid("e88f6a6d-7a3d-434e-b4ba-d61f6c38b52c"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3291), "This plan offers Ultra HD (4K) streaming on up to four devices at the same time. It is ideal for users with large households or those who desire the best video quality available", true, "PREMIUM", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3291) }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_by", "created_on", "is_active", "password", "role", "updated_by", "updated_on", "user_name" },
                values: new object[,]
                {
                    { new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3174), true, "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=", "Admin", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3176), "AdminUser" },
                    { new Guid("391c8da9-8465-49b2-a5ee-68990aa49f62"), new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3183), true, "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=", "Admin", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3183), "Propel" }
                });

            migrationBuilder.InsertData(
                table: "movie",
                columns: new[] { "id", "actor", "created_by", "created_on", "director", "genere", "is_active", "rating", "subscription_id", "title", "updated_by", "updated_on" },
                values: new object[,]
                {
                    { new Guid("03ba24d5-8bb0-44b5-bb9b-1ec7749023ca"), "Humphrey Bogart", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3328), "Michael Curtiz", "Drama", true, 4.4m, new Guid("e88f6a6d-7a3d-434e-b4ba-d61f6c38b52c"), "Casablanca", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3329) },
                    { new Guid("221398e0-9e2d-4322-bc5d-daba7765c9cd"), "Michael Keaton", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3334), "Alejandro González Iñárritu", "Drama", true, 4.2m, new Guid("e88f6a6d-7a3d-434e-b4ba-d61f6c38b52c"), "Birdman", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3335) },
                    { new Guid("518e04b1-043c-4829-b823-8efcbe347c69"), "Sally Hawkins", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3304), "Guillermo del Toro", "Fantasy", true, 4.2m, new Guid("7ec4efca-0a55-4bc1-827d-a7692a293e4a"), "The Shape of Water", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3304) },
                    { new Guid("5c87f473-a7c4-4eee-bc17-e6048d7fafff"), "Mahershala Ali", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3310), "Barry Jenkins", "Drama", true, 4.1m, new Guid("7ec4efca-0a55-4bc1-827d-a7692a293e4a"), "Moonlight", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3310) },
                    { new Guid("67a4dcd3-8f12-4759-b7ff-5237dacf1760"), "Leonardo DiCaprio", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3330), "Martin Scorsese", "Crime", true, 4.5m, new Guid("e88f6a6d-7a3d-434e-b4ba-d61f6c38b52c"), "The Departed", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3330) },
                    { new Guid("69cab019-baf0-4d0d-a4d8-b48b0ca2f50d"), "Colin Firth", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3342), "Tom Hooper", "Drama", true, 4.1m, new Guid("c5d19446-4c0f-450b-936a-b998c8c4e438"), "The King's Speech", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3343) },
                    { new Guid("6cfbfeb6-6361-40cb-9af2-d149849c675a"), "Javier Bardem", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3341), "Joel Coen, Ethan Coen", "Thriller", true, 4.5m, new Guid("c5d19446-4c0f-450b-936a-b998c8c4e438"), "No Country for Old Men", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3341) },
                    { new Guid("78d99698-ea37-4e51-a176-4355082fcff8"), "Anthony Hopkins", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3332), "Jonathan Demme", "Thriller", true, 4.4m, new Guid("e88f6a6d-7a3d-434e-b4ba-d61f6c38b52c"), "The Silence of the Lambs", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3332) },
                    { new Guid("9ac4c728-ff33-4a52-8a8e-d835e4217e21"), "Marlon Brando", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3322), "Francis Ford Coppola", "Crime", true, 4.7m, new Guid("c486ca23-b288-478c-973a-de75595d6b37"), "The Godfather", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3322) },
                    { new Guid("a61f7287-91d2-4c93-b4d3-115cb2a0f6bd"), "Russell Crowe", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3344), "Ron Howard", "Biography", true, 4.3m, new Guid("c5d19446-4c0f-450b-936a-b998c8c4e438"), "A Beautiful Mind", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3344) },
                    { new Guid("ac26b8aa-f0de-4ca6-859a-62b9b07be0d9"), "Song Kang Ho", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3314), "Bong Joon Ho", "Thriller", true, 4.3m, new Guid("7ec4efca-0a55-4bc1-827d-a7692a293e4a"), "Parasite", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3315) },
                    { new Guid("acf2ce5e-b799-4fe0-9c1a-d67bcb1e15d4"), "Liam Neeson", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3326), "Steven Spielberg", "Biography", true, 4.6m, new Guid("c486ca23-b288-478c-973a-de75595d6b37"), "Schindler's List", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3326) },
                    { new Guid("beb3361c-43b4-4611-875e-fa2296a78ed8"), "Michael Keaton", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3312), "Tom McCarthy", "Drama", true, 4.4m, new Guid("7ec4efca-0a55-4bc1-827d-a7692a293e4a"), "Spotlight", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3312) },
                    { new Guid("d461743d-4664-41c4-80c3-239a276f851d"), "Elijah Wood", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3320), "Peter Jackson", "Fantasy", true, 4.5m, new Guid("c486ca23-b288-478c-973a-de75595d6b37"), "The Lord of the Rings: The Return of the King", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3320) },
                    { new Guid("e16371c4-0184-4b41-b740-de95248c5960"), "Chiwetel Ejiofor", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3339), "Steve McQueen", "Drama", true, 4.3m, new Guid("c5d19446-4c0f-450b-936a-b998c8c4e438"), "12 Years a Slave", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3339) },
                    { new Guid("fca2786d-d18a-4e35-9259-a7f5e85f6bbc"), "Clark Gable", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3324), "Victor Fleming", "Drama", true, 4.2m, new Guid("c486ca23-b288-478c-973a-de75595d6b37"), "Gone with the Wind", new Guid("135b327b-497f-4a2a-a6dc-6c70dcf8d6dc"), new DateTime(2023, 7, 3, 20, 27, 31, 328, DateTimeKind.Utc).AddTicks(3324) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_card_payment_user_id",
                table: "card_payment",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movie_subscription_id",
                table: "movie",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_profile_subscription_id",
                table: "profile",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_profile_user_id",
                table: "profile",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_upi_payment_user_id",
                table: "upi_payment",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_payment");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "upi_payment");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
