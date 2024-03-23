using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Many_Blog_RecomendedTours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Blog_RecomendedTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Blog_RecomendedTours_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_AdditionalServiceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Many_Product_AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_AdditionalServiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_AdditionalServiceOptions_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Many_Product_AdditionalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_AdditionalServices_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Product_Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_Tags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_Destinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Destinations_Destinations_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_TourCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_TourCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_TourCategories_TourCategories_TourCategoryID",
                        column: x => x.TourCategoryID,
                        principalTable: "TourCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFaqLanguageItems",
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
                    table.PrimaryKey("PK_ProductFaqLanguageItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductFaqs",
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
                    table.PrimaryKey("PK_ProductFaqs", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
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
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLanguageItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
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
                    IsPersonLimited = table.Column<bool>(type: "bit", nullable: false),
                    PersonLimit = table.Column<int>(type: "int", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CancellationPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CancellationPolicies_CancellationPolicyID",
                        column: x => x.CancellationPolicyID,
                        principalTable: "CancellationPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSellLimits",
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
                    table.PrimaryKey("PK_ProductSellLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSellLimits_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
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
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceLanguageItems_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourStartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourLanguageItems_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPrices_PersonPolicies_PersonPolicyID",
                        column: x => x.PersonPolicyID,
                        principalTable: "PersonPolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourPrices_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TourPrograms",
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
                    table.PrimaryKey("PK_TourPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPrograms_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourProgramLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    TourProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourProgramLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourProgramLanguageItems_TourPrograms_TourProgramID",
                        column: x => x.TourProgramID,
                        principalTable: "TourPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTours_BlogID",
                table: "Many_Blog_RecomendedTours",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_RecomendedTours_ProductID",
                table: "Many_Blog_RecomendedTours",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalServiceOptions_AdditionalServiceOptionID",
                table: "Many_Product_AdditionalServiceOptions",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalServiceOptions_Many_Product_AdditionalServiceID",
                table: "Many_Product_AdditionalServiceOptions",
                column: "Many_Product_AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalServices_AdditionalServiceID",
                table: "Many_Product_AdditionalServices",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_AdditionalServices_ProductID",
                table: "Many_Product_AdditionalServices",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_Tags_ProductID",
                table: "Many_Product_Tags",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_Tags_TagID",
                table: "Many_Product_Tags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Destinations_DestinationID",
                table: "Many_Tour_Destinations",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Destinations_TourID",
                table: "Many_Tour_Destinations",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_TourCategories_TourCategoryID",
                table: "Many_Tour_TourCategories",
                column: "TourCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_TourCategories_TourID",
                table: "Many_Tour_TourCategories",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaqLanguageItems_ProductFaqID",
                table: "ProductFaqLanguageItems",
                column: "ProductFaqID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaqs_ProductID",
                table: "ProductFaqs",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageLanguageItem_ProductImageID",
                table: "ProductImageLanguageItem",
                column: "ProductImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductID",
                table: "ProductImages",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguageItems_ProductID",
                table: "ProductLanguageItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CancellationPolicyID",
                table: "Products",
                column: "CancellationPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ServiceID",
                table: "Products",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TourID",
                table: "Products",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellLimits_ProductID",
                table: "ProductSellLimits",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLanguageItems_ServiceID",
                table: "ServiceLanguageItems",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProductID",
                table: "Services",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TourLanguageItems_TourID",
                table: "TourLanguageItems",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrices_PersonPolicyID",
                table: "TourPrices",
                column: "PersonPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrices_TourID",
                table: "TourPrices",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourProgramLanguageItems_TourProgramID",
                table: "TourProgramLanguageItems",
                column: "TourProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrograms_TourID",
                table: "TourPrograms",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ProductID",
                table: "Tours",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_RecomendedTours_Products_ProductID",
                table: "Many_Blog_RecomendedTours",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Product_AdditionalServiceOptions_Many_Product_AdditionalServices_Many_Product_AdditionalServiceID",
                table: "Many_Product_AdditionalServiceOptions",
                column: "Many_Product_AdditionalServiceID",
                principalTable: "Many_Product_AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Product_AdditionalServices_Products_ProductID",
                table: "Many_Product_AdditionalServices",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Product_Tags_Products_ProductID",
                table: "Many_Product_Tags",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Tour_Destinations_Tours_TourID",
                table: "Many_Tour_Destinations",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Tour_TourCategories_Tours_TourID",
                table: "Many_Tour_TourCategories",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFaqLanguageItems_ProductFaqs_ProductFaqID",
                table: "ProductFaqLanguageItems",
                column: "ProductFaqID",
                principalTable: "ProductFaqs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFaqs_Products_ProductID",
                table: "ProductFaqs",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageLanguageItem_ProductImages_ProductImageID",
                table: "ProductImageLanguageItem",
                column: "ProductImageID",
                principalTable: "ProductImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductID",
                table: "ProductImages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguageItems_Products_ProductID",
                table: "ProductLanguageItems",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Services_ServiceID",
                table: "Products",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Tours_TourID",
                table: "Products",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Products_ProductID",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Products_ProductID",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Many_Blog_RecomendedTours");

            migrationBuilder.DropTable(
                name: "Many_Product_AdditionalServiceOptions");

            migrationBuilder.DropTable(
                name: "Many_Product_Tags");

            migrationBuilder.DropTable(
                name: "Many_Tour_Destinations");

            migrationBuilder.DropTable(
                name: "Many_Tour_TourCategories");

            migrationBuilder.DropTable(
                name: "ProductFaqLanguageItems");

            migrationBuilder.DropTable(
                name: "ProductImageLanguageItem");

            migrationBuilder.DropTable(
                name: "ProductLanguageItems");

            migrationBuilder.DropTable(
                name: "ProductSellLimits");

            migrationBuilder.DropTable(
                name: "ServiceLanguageItems");

            migrationBuilder.DropTable(
                name: "TourLanguageItems");

            migrationBuilder.DropTable(
                name: "TourPrices");

            migrationBuilder.DropTable(
                name: "TourProgramLanguageItems");

            migrationBuilder.DropTable(
                name: "Many_Product_AdditionalServices");

            migrationBuilder.DropTable(
                name: "ProductFaqs");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "TourPrograms");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
