using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class blogCategoryrevize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tag_Blog_BlogID",
                table: "Many_Blog_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tag_Tags_TagID",
                table: "Many_Blog_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Product_ProductID",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Product_ProductID",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "Many_Blog_RecomendedTour");

            migrationBuilder.DropTable(
                name: "Many_Product_AdditionalService");

            migrationBuilder.DropTable(
                name: "Many_Product_Tag");

            migrationBuilder.DropTable(
                name: "Many_Tour_Destination");

            migrationBuilder.DropTable(
                name: "Many_Tour_TourCategory");

            migrationBuilder.DropTable(
                name: "Many_TripadvisorComment_TourGuide");

            migrationBuilder.DropTable(
                name: "ProductFaqLanguageItem");

            migrationBuilder.DropTable(
                name: "ProductImageLanguageItem");

            migrationBuilder.DropTable(
                name: "ProductLanguageItem");

            migrationBuilder.DropTable(
                name: "ProductSellLimit");

            migrationBuilder.DropTable(
                name: "ServiceLanguageItem");

            migrationBuilder.DropTable(
                name: "TourLanguageItem");

            migrationBuilder.DropTable(
                name: "TourProgramLanguageItem");

            migrationBuilder.DropTable(
                name: "TripadvisorComment");

            migrationBuilder.DropTable(
                name: "ProductFaq");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "TourProgram");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Many_Blog_Tag",
                table: "Many_Blog_Tag");

            migrationBuilder.RenameTable(
                name: "Many_Blog_Tag",
                newName: "Many_Blog_Tags");

            migrationBuilder.RenameIndex(
                name: "IX_Many_Blog_Tag_TagID",
                table: "Many_Blog_Tags",
                newName: "IX_Many_Blog_Tags_TagID");

            migrationBuilder.RenameIndex(
                name: "IX_Many_Blog_Tag_BlogID",
                table: "Many_Blog_Tags",
                newName: "IX_Many_Blog_Tags_BlogID");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogID",
                table: "Many_Blog_Tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Many_Blog_Tags",
                table: "Many_Blog_Tags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tags_Blog_BlogID",
                table: "Many_Blog_Tags",
                column: "BlogID",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tags_Tags_TagID",
                table: "Many_Blog_Tags",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tags_Blog_BlogID",
                table: "Many_Blog_Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tags_Tags_TagID",
                table: "Many_Blog_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Many_Blog_Tags",
                table: "Many_Blog_Tags");

            migrationBuilder.RenameTable(
                name: "Many_Blog_Tags",
                newName: "Many_Blog_Tag");

            migrationBuilder.RenameIndex(
                name: "IX_Many_Blog_Tags_TagID",
                table: "Many_Blog_Tag",
                newName: "IX_Many_Blog_Tag_TagID");

            migrationBuilder.RenameIndex(
                name: "IX_Many_Blog_Tags_BlogID",
                table: "Many_Blog_Tag",
                newName: "IX_Many_Blog_Tag_BlogID");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogID",
                table: "Many_Blog_Tag",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Many_Blog_Tag",
                table: "Many_Blog_Tag",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Many_Blog_RecomendedTour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Blog_RecomendedTour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Blog_RecomendedTour_Blog_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_AdditionalService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComissionRateAbility = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_AdditionalService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_AdditionalService_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_Tag_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_Destination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_Destination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Destination_Destinations_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_TourCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_TourCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_TourCategory_TourCategories_TourCategoryID",
                        column: x => x.TourCategoryID,
                        principalTable: "TourCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_TripadvisorComment_TourGuide",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripadvisorCommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_TripadvisorComment_TourGuide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_TripadvisorComment_TourGuide_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CancellationPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgentDeposito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BannerImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerDeposito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CutoffHour = table.Column<int>(type: "int", nullable: false),
                    IsChildPolicyActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsTour = table.Column<bool>(type: "bit", nullable: false),
                    MinimumPayoutPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowOnHomePage = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_CancellationPolicies_CancellationPolicyID",
                        column: x => x.CancellationPolicyID,
                        principalTable: "CancellationPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFaq",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFaq", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFaq_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductLanguageItem_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSellLimit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SellLimit = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSellLimit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSellLimit_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ExclusionIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InclusionIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPerDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegmentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SightToSeeIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartCityID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTimeIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SuggestedStartTimeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourTypeID = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tour_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripadvisorComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripadvisorComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripadvisorComment_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TripadvisorComment_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductFaqLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductFaqID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFaqLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFaqLanguageItem_ProductFaq_ProductFaqID",
                        column: x => x.ProductFaqID,
                        principalTable: "ProductFaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImageLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImageLanguageItem_ProductImage_ProductImageID",
                        column: x => x.ProductImageID,
                        principalTable: "ProductImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceLanguageItem_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TourEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourStartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourLanguageItem_Tour_TourID",
                        column: x => x.TourID,
                        principalTable: "Tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourProgram_Tour_TourID",
                        column: x => x.TourID,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourProgramLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TourProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourProgramLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourProgramLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourProgramLanguageItem_TourProgram_TourProgramID",
                        column: x => x.TourProgramID,
                        principalTable: "TourProgram",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTour_BlogID",
                table: "Many_Blog_RecomendedTour",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTour_ProductID",
                table: "Many_Blog_RecomendedTour",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalService_AdditionalServiceID",
                table: "Many_Product_AdditionalService",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalService_ProductID",
                table: "Many_Product_AdditionalService",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_Tag_ProductID",
                table: "Many_Product_Tag",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_Tag_TagID",
                table: "Many_Product_Tag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Destination_DestinationID",
                table: "Many_Tour_Destination",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Destination_TourID",
                table: "Many_Tour_Destination",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_TourCategory_TourCategoryID",
                table: "Many_Tour_TourCategory",
                column: "TourCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_TourCategory_TourID",
                table: "Many_Tour_TourCategory",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_TripadvisorComment_TourGuide_TripadvisorCommentID",
                table: "Many_TripadvisorComment_TourGuide",
                column: "TripadvisorCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_TripadvisorComment_TourGuide_UserID",
                table: "Many_TripadvisorComment_TourGuide",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CancellationPolicyID",
                table: "Product",
                column: "CancellationPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ServiceID",
                table: "Product",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TourID",
                table: "Product",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaq_ProductID",
                table: "ProductFaq",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaqLanguageItem_ProductFaqID",
                table: "ProductFaqLanguageItem",
                column: "ProductFaqID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductID",
                table: "ProductImage",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageLanguageItem_ProductImageID",
                table: "ProductImageLanguageItem",
                column: "ProductImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguageItem_LangaugeID",
                table: "ProductLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguageItem_ProductID",
                table: "ProductLanguageItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellLimit_ProductID",
                table: "ProductSellLimit",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ProductID",
                table: "Service",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLanguageItem_LangaugeID",
                table: "ServiceLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLanguageItem_ServiceID",
                table: "ServiceLanguageItem",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ProductID",
                table: "Tour",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TourLanguageItem_LangaugeID",
                table: "TourLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_TourLanguageItem_TourID",
                table: "TourLanguageItem",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourProgram_TourID",
                table: "TourProgram",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourProgramLanguageItem_LangaugeID",
                table: "TourProgramLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_TourProgramLanguageItem_TourProgramID",
                table: "TourProgramLanguageItem",
                column: "TourProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_TripadvisorComment_ProductID",
                table: "TripadvisorComment",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TripadvisorComment_UserID",
                table: "TripadvisorComment",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tag_Blog_BlogID",
                table: "Many_Blog_Tag",
                column: "BlogID",
                principalTable: "Blog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tag_Tags_TagID",
                table: "Many_Blog_Tag",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_RecomendedTour_Product_ProductID",
                table: "Many_Blog_RecomendedTour",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Product_AdditionalService_Product_ProductID",
                table: "Many_Product_AdditionalService",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Product_Tag_Product_ProductID",
                table: "Many_Product_Tag",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Tour_Destination_Tour_TourID",
                table: "Many_Tour_Destination",
                column: "TourID",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Tour_TourCategory_Tour_TourID",
                table: "Many_Tour_TourCategory",
                column: "TourID",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_TripadvisorComment_TourGuide_TripadvisorComment_TripadvisorCommentID",
                table: "Many_TripadvisorComment_TourGuide",
                column: "TripadvisorCommentID",
                principalTable: "TripadvisorComment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Service_ServiceID",
                table: "Product",
                column: "ServiceID",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tour_TourID",
                table: "Product",
                column: "TourID",
                principalTable: "Tour",
                principalColumn: "Id");
        }
    }
}
