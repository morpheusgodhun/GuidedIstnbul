using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class additionalService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "FaqLangaugeItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "FaqCategoryLanguageItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdditionalServiceInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    InputTypeID = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerPerson = table.Column<bool>(type: "bit", nullable: false),
                    IsPerDay = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialNumber = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBaseLanguage = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceInputLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceInputID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceInputLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceInputLanguageItems_AdditionalServiceInputs_AdditionalServiceInputID",
                        column: x => x.AdditionalServiceInputID,
                        principalTable: "AdditionalServiceInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceLanguageItems_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptions_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BannerImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_BlogCategoryID",
                        column: x => x.BlogCategoryID,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategoryLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategoryLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategoryLanguageItem_BlogCategory_BlogCategoryID",
                        column: x => x.BlogCategoryID,
                        principalTable: "BlogCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogCategoryLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionLanguageItems_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_PersonPolicies_PersonPolicyID",
                        column: x => x.PersonPolicyID,
                        principalTable: "PersonPolicies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceInputID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_AdditionalServiceOption_AdditionalServiceInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceInputs_AdditionalServiceInputID",
                        column: x => x.AdditionalServiceInputID,
                        principalTable: "AdditionalServiceInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogLanguageItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogLanguageItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogLanguageItem_Blog_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogLanguageItem_Language_LangaugeID",
                        column: x => x.LangaugeID,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Many_Blog_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Blog_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Blog_Tag_Blog_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Many_Blog_Tag_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Blog_RecomendedTour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    ComissionRateAbility = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_AdditionalService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_AdditionalService_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DestinationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TourCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripadvisorCommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    IsTour = table.Column<bool>(type: "bit", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BannerImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CutoffHour = table.Column<int>(type: "int", nullable: false),
                    CustomerDeposito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgentDeposito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<int>(type: "int", nullable: false),
                    MinimumPayoutPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShowOnHomePage = table.Column<bool>(type: "bit", nullable: false),
                    IsChildPolicyActive = table.Column<bool>(type: "bit", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CancellationPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellLimit = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    TourTypeID = table.Column<int>(type: "int", nullable: false),
                    SegmentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartCityID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTimeIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestedStartTimeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    InclusionIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExclusionIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SightToSeeIDs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductFaqID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourStartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LangaugeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_FaqLangaugeItems_LanguageId",
                table: "FaqLangaugeItems",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqCategoryLanguageItems_LanguageId",
                table: "FaqCategoryLanguageItems",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceInputLanguageItems_AdditionalServiceInputID",
                table: "AdditionalServiceInputLanguageItems",
                column: "AdditionalServiceInputID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceLanguageItems_AdditionalServiceID",
                table: "AdditionalServiceLanguageItems",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionLanguageItems_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionLanguageItems",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPrices",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_PersonPolicyID",
                table: "AdditionalServiceOptionPrices",
                column: "PersonPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptions_AdditionalServiceID",
                table: "AdditionalServiceOptions",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategoryID",
                table: "Blog",
                column: "BlogCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryLanguageItem_BlogCategoryID",
                table: "BlogCategoryLanguageItem",
                column: "BlogCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryLanguageItem_LangaugeID",
                table: "BlogCategoryLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogLanguageItem_BlogID",
                table: "BlogLanguageItem",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogLanguageItem_LangaugeID",
                table: "BlogLanguageItem",
                column: "LangaugeID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceInputID",
                table: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                column: "AdditionalServiceInputID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceOptionID",
                table: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTour_BlogID",
                table: "Many_Blog_RecomendedTour",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTour_ProductID",
                table: "Many_Blog_RecomendedTour",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_Tag_BlogID",
                table: "Many_Blog_Tag",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_Tag_TagID",
                table: "Many_Blog_Tag",
                column: "TagID");

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
                name: "FK_FaqCategoryLanguageItems_Language_LanguageId",
                table: "FaqCategoryLanguageItems",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqLangaugeItems_Language_LanguageId",
                table: "FaqLangaugeItems",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaqCategoryLanguageItems_Language_LanguageId",
                table: "FaqCategoryLanguageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FaqLangaugeItems_Language_LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Product_ProductID",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Product_ProductID",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "AdditionalServiceInputLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionPrices");

            migrationBuilder.DropTable(
                name: "BlogCategoryLanguageItem");

            migrationBuilder.DropTable(
                name: "BlogLanguageItem");

            migrationBuilder.DropTable(
                name: "Many_AdditionalServiceOption_AdditionalServiceInputs");

            migrationBuilder.DropTable(
                name: "Many_Blog_RecomendedTour");

            migrationBuilder.DropTable(
                name: "Many_Blog_Tag");

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
                name: "AdditionalServiceInputs");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptions");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "TripadvisorComment");

            migrationBuilder.DropTable(
                name: "ProductFaq");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "TourProgram");

            migrationBuilder.DropTable(
                name: "AdditionalServices");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_FaqLangaugeItems_LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropIndex(
                name: "IX_FaqCategoryLanguageItems_LanguageId",
                table: "FaqCategoryLanguageItems");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "FaqCategoryLanguageItems");
        }
    }
}
