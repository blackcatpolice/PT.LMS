using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PageTechsLMS.DataCore.Data.Mysql.Migrations
{
    public partial class InitMysqlDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    Name = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Cover = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                columns: table => new
                {
                    DeviceCode = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    UserCode = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    SubjectId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    SessionId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    ClientId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Data = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "FilebaseInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    PhysicPath = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    RelativePath = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Ext = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Size = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilebaseInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberBinds",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberBinds", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "MemberCoursePayLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCoursePayLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberInfos",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Avatart = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberInfos", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    SubjectId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    SessionId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    ClientId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Data = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CoverImg = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Cover = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Icon = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Video = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    CommentNum = table.Column<int>(type: "int", nullable: false),
                    Favorite = table.Column<int>(type: "int", nullable: false),
                    ViewNumb = table.Column<int>(type: "int", nullable: false),
                    StartNum = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsHot = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    MemberInfoId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    MemberBindId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256) CHARACTER SET utf8", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MemberBinds_MemberBindId",
                        column: x => x.MemberBindId,
                        principalTable: "MemberBinds",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MemberInfos_MemberInfoId",
                        column: x => x.MemberInfoId,
                        principalTable: "MemberInfos",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messageboxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    FromeMemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messageboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messageboxes_MemberInfos_FromeMemberId",
                        column: x => x.FromeMemberId,
                        principalTable: "MemberInfos",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messageboxes_MemberInfos_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberInfos",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TopicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    LikeNum = table.Column<int>(type: "int", nullable: false),
                    FavoriteNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_MemberInfos_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberInfos",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: false),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    MemberAccountId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_MemberAccountId",
                        column: x => x.MemberAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberCourseLearnLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SectionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SectionItemId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    Remaining = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCourseLearnLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCourseLearnLogs_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberFeedbackLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true),
                    StartNum = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFavorite = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFeedbackLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberFeedbackLogs_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paylogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paylogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paylogs_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CourseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SectionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Video = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Duration = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Level = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    IsTrailer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFree = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionItems_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MemberBindId",
                table: "AspNetUsers",
                column: "MemberBindId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MemberInfoId",
                table: "AspNetUsers",
                column: "MemberInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemberAccountId",
                table: "Comments",
                column: "MemberAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCourseLearnLogs_MemberId",
                table: "MemberCourseLearnLogs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFeedbackLogs_MemberId",
                table: "MemberFeedbackLogs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Messageboxes_FromeMemberId",
                table: "Messageboxes",
                column: "FromeMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Messageboxes_MemberId",
                table: "Messageboxes",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Paylogs_MemberId",
                table: "Paylogs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MemberId",
                table: "Posts",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicId",
                table: "Posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionItems_SectionId",
                table: "SectionItems",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_LessonId",
                table: "Sections",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CourseId",
                table: "Tags",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CourseOrder");

            migrationBuilder.DropTable(
                name: "DeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "FilebaseInfo");

            migrationBuilder.DropTable(
                name: "MemberCourseLearnLogs");

            migrationBuilder.DropTable(
                name: "MemberCoursePayLogs");

            migrationBuilder.DropTable(
                name: "MemberFeedbackLogs");

            migrationBuilder.DropTable(
                name: "Messageboxes");

            migrationBuilder.DropTable(
                name: "Paylogs");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "SectionItems");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "MemberBinds");

            migrationBuilder.DropTable(
                name: "MemberInfos");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
