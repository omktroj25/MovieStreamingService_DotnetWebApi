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
                    { new Guid("4b554483-edaa-4b49-9ea7-31ec22c7a4e7"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5521), "This plan offers Ultra HD (4K) streaming on up to four devices at the same time. It is ideal for users with large households or those who desire the best video quality available", true, "PREMIUM", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5523) },
                    { new Guid("6b61a787-b20d-48ad-aa97-39d8b2551f3c"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5242), "This plan offers standard definition (SD) streaming on one device at a time. It's a cost-effective option for individuals or budget-conscious users", true, "BASIC", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5243) },
                    { new Guid("a99388f0-7111-4d76-babb-e572cd200c37"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5525), "The free plan offers limited access to the streaming site's content library", true, "FREE", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5526) },
                    { new Guid("eae8a2ee-2c30-471a-8f08-310d1400573b"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5246), "This plan provides high definition (HD) streaming on up to two devices simultaneously. It is suitable for users who prefer better video quality and want to share their account with family members", true, "STANDARD", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5246) }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_by", "created_on", "is_active", "password", "role", "updated_by", "updated_on", "user_name" },
                values: new object[,]
                {
                    { new Guid("6a451953-a671-40fd-8ff6-537e824b7615"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5065), true, "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=", "Admin", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5065), "Propel" },
                    { new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5051), true, "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=", "Admin", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5055), "AdminUser" }
                });

            migrationBuilder.InsertData(
                table: "movie",
                columns: new[] { "id", "actor", "created_by", "created_on", "director", "genere", "is_active", "rating", "subscription_id", "title", "updated_by", "updated_on" },
                values: new object[,]
                {
                    { new Guid("0634fa71-593f-44fc-8ed3-6c48546a5fc3"), "Michael Keaton", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5618), "Tom McCarthy", "Drama", true, 4.4m, new Guid("6b61a787-b20d-48ad-aa97-39d8b2551f3c"), "Spotlight", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5618) },
                    { new Guid("136ffdc7-70a6-4fb1-8ee3-8c695c0e803b"), "Russell Crowe", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5660), "Ron Howard", "Biography", true, 4.3m, new Guid("a99388f0-7111-4d76-babb-e572cd200c37"), "A Beautiful Mind", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5660) },
                    { new Guid("16159777-2ce0-4fd0-b85d-b364538c60f4"), "Marlon Brando", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5633), "Francis Ford Coppola", "Crime", true, 4.7m, new Guid("eae8a2ee-2c30-471a-8f08-310d1400573b"), "The Godfather", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5634) },
                    { new Guid("25c82962-a3cf-436e-9a2c-f08f88f44280"), "Humphrey Bogart", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5641), "Michael Curtiz", "Drama", true, 4.4m, new Guid("4b554483-edaa-4b49-9ea7-31ec22c7a4e7"), "Casablanca", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5641) },
                    { new Guid("26678ae1-0948-4566-9861-310cab588897"), "Mahershala Ali", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5615), "Barry Jenkins", "Drama", true, 4.1m, new Guid("6b61a787-b20d-48ad-aa97-39d8b2551f3c"), "Moonlight", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5616) },
                    { new Guid("3401c340-c1af-4387-a520-f389bf489623"), "Clark Gable", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5636), "Victor Fleming", "Drama", true, 4.2m, new Guid("eae8a2ee-2c30-471a-8f08-310d1400573b"), "Gone with the Wind", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5636) },
                    { new Guid("4a66d7c0-1617-4363-9b9d-3e27c03d3a12"), "Elijah Wood", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5631), "Peter Jackson", "Fantasy", true, 4.5m, new Guid("eae8a2ee-2c30-471a-8f08-310d1400573b"), "The Lord of the Rings: The Return of the King", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5631) },
                    { new Guid("6198bdca-56fa-4d28-9af8-913c4c098b38"), "Liam Neeson", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5638), "Steven Spielberg", "Biography", true, 4.6m, new Guid("eae8a2ee-2c30-471a-8f08-310d1400573b"), "Schindler's List", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5639) },
                    { new Guid("73378ae5-c944-43c1-9790-969cba68e9dc"), "Javier Bardem", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5655), "Joel Coen, Ethan Coen", "Thriller", true, 4.5m, new Guid("a99388f0-7111-4d76-babb-e572cd200c37"), "No Country for Old Men", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5656) },
                    { new Guid("7c951dd5-48b6-4d18-8d7e-a3054afad587"), "Anthony Hopkins", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5645), "Jonathan Demme", "Thriller", true, 4.4m, new Guid("4b554483-edaa-4b49-9ea7-31ec22c7a4e7"), "The Silence of the Lambs", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5646) },
                    { new Guid("95ff5d50-f49d-4988-aa18-b0a9e079b3fd"), "Chiwetel Ejiofor", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5653), "Steve McQueen", "Drama", true, 4.3m, new Guid("a99388f0-7111-4d76-babb-e572cd200c37"), "12 Years a Slave", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5653) },
                    { new Guid("c38622fd-37bd-4af4-80eb-9f42814f9192"), "Sally Hawkins", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5598), "Guillermo del Toro", "Fantasy", true, 4.2m, new Guid("6b61a787-b20d-48ad-aa97-39d8b2551f3c"), "The Shape of Water", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5598) },
                    { new Guid("c6d61e65-ad4e-4c04-93f3-9cdd2547da1c"), "Colin Firth", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5658), "Tom Hooper", "Drama", true, 4.1m, new Guid("a99388f0-7111-4d76-babb-e572cd200c37"), "The King's Speech", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5658) },
                    { new Guid("d2ace484-c584-440d-9980-145cbd405a11"), "Song Kang Ho", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5628), "Bong Joon Ho", "Thriller", true, 4.3m, new Guid("6b61a787-b20d-48ad-aa97-39d8b2551f3c"), "Parasite", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5629) },
                    { new Guid("edc05d1f-10ea-4512-b899-7b32429ed6a8"), "Michael Keaton", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5651), "Alejandro González Iñárritu", "Drama", true, 4.2m, new Guid("4b554483-edaa-4b49-9ea7-31ec22c7a4e7"), "Birdman", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5651) },
                    { new Guid("fc3af630-2577-4588-b5a0-b17f56d85e8c"), "Leonardo DiCaprio", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5643), "Martin Scorsese", "Crime", true, 4.5m, new Guid("4b554483-edaa-4b49-9ea7-31ec22c7a4e7"), "The Departed", new Guid("ed87a888-ca9c-493d-9b50-e18bf61da782"), new DateTime(2023, 5, 26, 11, 3, 38, 914, DateTimeKind.Utc).AddTicks(5643) }
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
