using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSetUp_Ver01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "AC00000001"),
                    UserName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__349DA586A61F0662", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "CardProvider",
                columns: table => new
                {
                    CardProviderName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CPFullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CardProv__3B8DEBCC9CD99C33", x => x.CardProviderName);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    PCateID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "PC00000001"),
                    PCateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PCateDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PCateStatus = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductC__5DF9FF092873E01E", x => x.PCateID);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    WishID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "W000000001"),
                    ProductID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Wishlist__64BA6541F2011F05", x => x.WishID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWallet",
                columns: table => new
                {
                    WalletID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "CW00000001"),
                    AccountID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__84D4F92E55E9A551", x => x.WalletID);
                    table.ForeignKey(
                        name: "customerwallet_accountid_foreign",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Account1Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Account2Id = table.Column<string>(type: "varchar(255)", nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<long>(type: "bigint", nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    DepositId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Account_Account1Id",
                        column: x => x.Account1Id,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Account_Account2Id",
                        column: x => x.Account2Id,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayoutHistories",
                columns: table => new
                {
                    PayoutId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PayoutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayoutHistories", x => x.PayoutId);
                    table.ForeignKey(
                        name: "FK_PayoutHistories_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "C000000001"),
                    AccountID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CardName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CardNumber = table.Column<long>(type: "bigint", nullable: true),
                    CardProviderName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TaxNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8B16F035F", x => x.CustomerID);
                    table.ForeignKey(
                        name: "customer_accountid_foreign",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "customer_cardprovider_foreign",
                        column: x => x.CardProviderName,
                        principalTable: "CardProvider",
                        principalColumn: "CardProviderName");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "P000000001"),
                    AccountID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PCateID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    StockQuantity = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PurchaseType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PercentageOfDamage = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    DamageDetail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "[]"),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__B40CC6EDEFCE57C4", x => x.ProductID);
                    table.ForeignKey(
                        name: "product_accountid_foreign",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "product_pcateid_foreign",
                        column: x => x.PCateID,
                        principalTable: "ProductCategory",
                        principalColumn: "PCateID");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<string>(type: "varchar(255)", nullable: false),
                    IsGoodReview = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    PaidPrice = table.Column<double>(type: "float", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", nullable: false),
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Reports_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositInformations",
                columns: table => new
                {
                    DepositId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositInformations", x => x.DepositId);
                    table.ForeignKey(
                        name: "FK_DepositInformations_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepositId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.CheckConstraint("CHK_DepositId_For_Barter", "(TransactionType ='Buy' AND DepositId IS NULL) OR (TransactionType = 'Barter' AND DepositId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Transactions_DepositInformations_DepositId",
                        column: x => x.DepositId,
                        principalTable: "DepositInformations",
                        principalColumn: "DepositId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Address", "Birthday", "Email", "FullName", "Gender", "Password", "Phone", "Role", "Status", "UserName" },
                values: new object[,]
                {
                    { "AC00000001", "Ho Chi Minh", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duchao696@gmail.com", "Nguyễn Văn A", "Nam", "yK8/gC+JYpNvFdXRYr8nXhR60lKyKJxlbNCnZA4YfTk=", 84901234567L, "Admin", "Active", "user01" },
                    { "AC00000002", "Hanoi", new DateTime(1990, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "nhattulam12102003@gmail.com", "Trần Thị B", "Nữ", "yK8/gC+JYpNvFdXRYr8nXhR60lKyKJxlbNCnZA4YfTk=", 84901234568L, "Seller", "Active", "user02" },
                    { "AC00000003", "Da Nang", new DateTime(1988, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "user03@example.com", "Lê Văn C", "Nam", "Pass@123", 84901234569L, "Customer", "Inactive", "user03" },
                    { "AC00000004", "Can Tho", new DateTime(1992, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "user04@example.com", "Phạm Thị D", "Nữ", "Pass@123", 84901234570L, "Seller", "Active", "user04" },
                    { "AC00000005", "Nha Trang", new DateTime(1985, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "user05@example.com", "Bùi Văn E", "Nam", "Pass@123", 84901234571L, "Customer", "Active", "user05" },
                    { "AC00000006", "Vinh", new DateTime(1993, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "user06@example.com", "Đặng Thị F", "Nữ", "Pass@123", 84901234572L, "Seller", "Inactive", "user06" },
                    { "AC00000007", "Hue", new DateTime(1987, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user07@example.com", "Ngô Văn G", "Nam", "Pass@123", 84901234573L, "Customer", "Active", "user07" },
                    { "AC00000008", "Bac Giang", new DateTime(1994, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "user08@example.com", "Tô Thị H", "Nữ", "Pass@123", 84901234574L, "Seller", "Active", "user08" },
                    { "AC00000009", "Quang Ninh", new DateTime(1986, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user09@example.com", "Hoàng Văn I", "Nam", "Pass@123", 84901234575L, "Customer", "Inactive", "user09" },
                    { "AC00000010", "Tay Ninh", new DateTime(1991, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "user10@example.com", "Vũ Thị J", "Nữ", "Pass@123", 84901234576L, "Seller", "Active", "user10" }
                });

            migrationBuilder.InsertData(
                table: "CardProvider",
                columns: new[] { "CardProviderName", "CPFullName" },
                values: new object[,]
                {
                    { "ABBANK", "Ngân hàng TMCP An Bình" },
                    { "ACB", "Ngân hàng TMCP Á Châu" },
                    { "Agribank", "Ngân hàng Nông nghiệp và Phát triển Nông thôn Việt Nam" },
                    { "BacABank", "Ngân hàng TMCP Bắc Á" },
                    { "BaoVietBank", "Ngân hàng TMCP Bảo Việt" },
                    { "BIDV", "Ngân hàng TMCP Đầu tư và Phát triển Việt Nam" },
                    { "CAKE", "TMCP Việt Nam Thịnh Vượng - Ngân hàng số CAKE by VPBank" },
                    { "CBBank", "Ngân hàng Thương mại TNHH MTV Xây dựng Việt Nam" },
                    { "CIMB", "Ngân hàng TNHH MTV CIMB Việt Nam" },
                    { "Citibank", "Ngân hàng Citibank, N.A. - Chi nhánh Hà Nội" },
                    { "COOPBANK", "Ngân hàng Hợp tác xã Việt Nam" },
                    { "DBSBank", "DBS Bank Ltd - Chi nhánh Thành phố Hồ Chí Minh" },
                    { "DongABank", "Ngân hàng TMCP Đông Á" },
                    { "Eximbank", "Ngân hàng TMCP Xuất Nhập khẩu Việt Nam" },
                    { "GPBank", "Ngân hàng Thương mại TNHH MTV Dầu Khí Toàn Cầu" },
                    { "HDBank", "Ngân hàng TMCP Phát triển Thành phố Hồ Chí Minh" },
                    { "HongLeong", "Ngân hàng TNHH MTV Hong Leong Việt Nam" },
                    { "HSBC", "Ngân hàng TNHH MTV HSBC (Việt Nam)" },
                    { "IBKHCM", "Ngân hàng Công nghiệp Hàn Quốc - Chi nhánh TP. Hồ Chí Minh" },
                    { "IBKHN", "Ngân hàng Công nghiệp Hàn Quốc - Chi nhánh Hà Nội" },
                    { "IndovinaBank", "Ngân hàng TNHH Indovina" },
                    { "KBank", "Ngân hàng Đại chúng TNHH Kasikornbank" },
                    { "KEBHanaHCM", "Ngân hàng KEB Hana – Chi nhánh Thành phố Hồ Chí Minh" },
                    { "KEBHANAHN", "Công ty Tài chính TNHH MTV Mirae Asset (Việt Nam)" },
                    { "KienLongBank", "Ngân hàng TMCP Kiên Long" },
                    { "KookminHCM", "Ngân hàng Kookmin - Chi nhánh Thành phố Hồ Chí Minh" },
                    { "KookminHN", "Ngân hàng Kookmin - Chi nhánh Hà Nội" },
                    { "LienVietPostBank", "Ngân hàng TMCP Bưu Điện Liên Việt" },
                    { "MBBank", "Ngân hàng TMCP Quân đội" },
                    { "MSB", "Ngân hàng TMCP Hàng Hải" },
                    { "NamABank", "Ngân hàng TMCP Nam Á" },
                    { "NCB", "Ngân hàng TMCP Quốc Dân" },
                    { "Nonghyup", "Ngân hàng Nonghyup - Chi nhánh Hà Nội" },
                    { "OCB", "Ngân hàng TMCP Phương Đông" },
                    { "Oceanbank", "Ngân hàng Thương mại TNHH MTV Đại Dương" },
                    { "PGBank", "Ngân hàng TMCP Xăng dầu Petrolimex" },
                    { "PublicBank", "Ngân hàng TNHH MTV Public Việt Nam" },
                    { "PVcomBank", "Ngân hàng TMCP Đại Chúng Việt Nam" },
                    { "Sacombank", "Ngân hàng TMCP Sài Gòn Thương Tín" },
                    { "SaigonBank", "NgânNgân hàng TMCP Sài Gòn Công Thương" },
                    { "SCB", "Ngân hàng TMCP Sài Gòn" },
                    { "SeABank", "Ngân hàng TMCP Đông Nam Á" },
                    { "SHB", "Ngân hàng TMCP Sài Gòn - Hà Nội" },
                    { "ShinhanBank", "Ngân hàng TNHH MTV Shinhan Việt Nam" },
                    { "StandardChartered", "Ngân hàng TNHH MTV Standard Chartered Bank Việt Nam" },
                    { "Techcombank", "Ngân hàng TMCP Kỹ thương Việt Nam" },
                    { "Timo", "Ngân hàng số Timo by Ban Viet Bank (Timo by Ban Viet Bank)" },
                    { "TPBank", "Ngân hàng TMCP Tiên Phong" },
                    { "Ubank", "NgânTMCP Việt Nam Thịnh Vượng - Ngân hàng số Ubank by VPBank" },
                    { "UnitedOverseas", "Ngân hàng United Overseas - Chi nhánh TP. Hồ Chí Minh" },
                    { "VBSP", "Ngân hàng Chính sách Xã hội" },
                    { "VIB", "Ngân hàng TMCP Quốc tế Việt Nam" },
                    { "VietABank", "Ngân hàng TMCP Việt Á" },
                    { "VietBank", "Ngân hàng TMCP Việt Nam Thương Tín" },
                    { "VietCapitalBank", "Ngân hàng TMCP Bản Việt" },
                    { "Vietcombank", "Ngân hàng TMCP Ngoại Thương Việt Nam" },
                    { "VietinBank", "Ngân hàng TMCP Công thương Việt Nam" },
                    { "ViettelMoney", "Tổng Công ty Dịch vụ số Viettel - Chi nhánh tập đoàn công nghiệp viễn thông Quân Đội" },
                    { "VNPTMoney", "VNPT Money" },
                    { "VPBank", "Ngân hàng TMCP Việt Nam Thịnh Vượng" },
                    { "VRB", "Ngân hàng Liên doanh Việt - Nga" },
                    { "Woori", "Ngân hàng TNHH MTV Woori Việt Nam" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "PCateID", "PCateDesc", "PCateName", "PCateStatus" },
                values: new object[,]
                {
                    { "PC00000001", "Các loại điện thoại di động", "Điện thoại", "Active" },
                    { "PC00000002", "Các loại máy tính xách tay", "Laptop", "Active" },
                    { "PC00000003", "Quần áo, giày dép nam", "Thời trang nam", "Active" },
                    { "PC00000004", "Quần áo, giày dép nữ", "Thời trang nữ", "Active" },
                    { "PC00000005", "Sản phẩm cho gia đình", "Đồ gia dụng", "Active" },
                    { "PC00000006", "Các thiết bị điện tử", "Đồ điện tử", "Active" },
                    { "PC00000007", "Các loại sách", "Sách", "Active" },
                    { "PC00000008", "Đồ chơi trẻ em", "Đồ chơi", "Active" },
                    { "PC00000009", "Tai nghe, bàn phím, chuột", "Phụ kiện công nghệ", "Active" },
                    { "PC00000010", "Các loại xe đạp, xe máy", "Xe cộ", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerID", "AccountID", "Avatar", "CardName", "CardNumber", "CardProviderName", "TaxNumber" },
                values: new object[,]
                {
                    { "C000000001", "AC00000001", null, "Visa Platinum", 1234567890123456L, "Vietcombank", 123456789L },
                    { "C000000002", "AC00000003", null, "MasterCard Gold", 9876543210987654L, "BIDV", 987654321L },
                    { "C000000003", "AC00000005", null, "JCB Standard", 4561237894561237L, "Techcombank", 456123789L },
                    { "C000000004", "AC00000007", null, "Visa Infinite", 7418529638527418L, "Sacombank", 741852963L },
                    { "C000000005", "AC00000009", null, "MasterCard Standard", 3692581473692581L, "MBBank", 369258147L }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "AccountID", "CreatedAt", "CreatedBy", "DamageDetail", "DeletedAt", "DeletedBy", "Discount", "Location", "PCateID", "PercentageOfDamage", "ProductDesc", "ProductName", "ProductPrice", "PurchaseDate", "PurchaseType", "Status", "StockQuantity", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { "P000000001", "AC00000002", new DateOnly(1, 1, 1), "AC00000002", null, null, null, 10.00m, "Ho Chi Minh", "PC00000001", 10.00m, "Điện thoại Apple iPhone 14 mới", "iPhone 14", 20000000.00m, new DateOnly(2024, 1, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000002", "AC00000004", new DateOnly(1, 1, 1), "AC00000004", null, null, null, 5.00m, "Hanoi", "PC00000002", 20.00m, "Laptop Dell XPS 13 2023", "Dell XPS 13", 35000000.00m, new DateOnly(2024, 2, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000003", "AC00000006", new DateOnly(1, 1, 1), "AC00000006", null, null, null, 20.00m, "Can Tho", "PC00000003", 15.00m, "Giày thể thao Nike Air Jordan", "Giày Nike Air", 5000000.00m, new DateOnly(2024, 3, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000004", "AC00000008", new DateOnly(1, 1, 1), "AC00000008", null, null, null, 15.00m, "Da Nang", "PC00000004", 5.00m, "Áo thun nữ cotton", "Áo thun nữ", 300000.00m, new DateOnly(2024, 4, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000005", "AC00000010", new DateOnly(1, 1, 1), "AC00000010", null, null, null, 10.00m, "Hue", "PC00000005", 40.00m, "Nồi cơm điện Toshiba", "Nồi cơm điện", 2000000.00m, new DateOnly(2024, 5, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000006", "AC00000002", new DateOnly(1, 1, 1), "AC00000002", null, null, null, 5.00m, "Quang Ninh", "PC00000006", 50.00m, "Tai nghe Sony WH-1000XM4", "Tai nghe Sony", 8000000.00m, new DateOnly(2024, 6, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000007", "AC00000004", new DateOnly(1, 1, 1), "AC00000004", null, null, null, 0.00m, "Nha Trang", "PC00000007", 12.00m, "Sách Python cho người mới", "Sách lập trình", 250000.00m, new DateOnly(2024, 7, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000008", "AC00000006", new DateOnly(1, 1, 1), "AC00000006", null, null, null, 10.00m, "Vinh", "PC00000008", 35.00m, "Bộ xếp hình lego 1000 mảnh", "Bộ lego", 1200000.00m, new DateOnly(2024, 8, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000009", "AC00000008", new DateOnly(1, 1, 1), "AC00000008", null, null, null, 5.00m, "Hanoi", "PC00000009", 44.00m, "Bàn phím cơ RGB", "Bàn phím cơ", 1500000.00m, new DateOnly(2024, 9, 1), "Mua ngay", "Active", 1.0, null, null },
                    { "P000000010", "AC00000010", new DateOnly(1, 1, 1), "AC00000010", null, null, null, 10.00m, "Tay Ninh", "PC00000010", 10.00m, "Xe đạp địa hình", "Xe đạp thể thao", 5000000.00m, new DateOnly(2024, 10, 1), "Mua ngay", "Active", 1.0, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AccountID",
                table: "Customer",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CardProviderName",
                table: "Customer",
                column: "CardProviderName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWallet_AccountID",
                table: "CustomerWallet",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositInformations_TransactionId",
                table: "DepositInformations",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositInformations_UserId",
                table: "DepositInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AccountId",
                table: "Feedbacks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProductID",
                table: "Feedbacks",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Account1Id",
                table: "Orders",
                column: "Account1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Account2Id",
                table: "Orders",
                column: "Account2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutHistories_AccountId",
                table: "PayoutHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AccountID",
                table: "Product",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PCateID",
                table: "Product",
                column: "PCateID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AccountId",
                table: "Reports",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProductId",
                table: "Reports",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DepositId",
                table: "Transactions",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositInformations_Transactions_TransactionId",
                table: "DepositInformations",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepositInformations_Account_UserId",
                table: "DepositInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Account_Account1Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Account_Account2Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_DepositInformations_Transactions_TransactionId",
                table: "DepositInformations");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerWallet");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PayoutHistories");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "CardProvider");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "DepositInformations");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
