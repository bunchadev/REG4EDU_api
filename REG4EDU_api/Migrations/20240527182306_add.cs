using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REG4EDU_api.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorsCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorsId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfCredits = table.Column<int>(type: "int", nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCredits = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "MajorsId");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RolePermission_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Permission_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermission_Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_Permission_Id",
                        column: x => x.Permission_Id,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MajorsSubjects",
                columns: table => new
                {
                    MajorsSubject_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MajorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorsSubjects", x => x.MajorsSubject_Id);
                    table.ForeignKey(
                        name: "FK_MajorsSubjects_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "MajorsId");
                    table.ForeignKey(
                        name: "FK_MajorsSubjects_Subjects_FK_SubjectId",
                        column: x => x.FK_SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateTable(
                name: "SemesterSubjects",
                columns: table => new
                {
                    SemesterSubject_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubjects", x => x.SemesterSubject_Id);
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "SemesterId");
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Subjects_FK_SubjectId",
                        column: x => x.FK_SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateTable(
                name: "StudentsSubjects",
                columns: table => new
                {
                    StudentSubject_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scores = table.Column<double>(type: "float", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsSubjects", x => x.StudentSubject_Id);
                    table.ForeignKey(
                        name: "FK_StudentsSubjects_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "SemestersClass",
                columns: table => new
                {
                    SemesterClass_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassNumber = table.Column<int>(type: "int", nullable: false),
                    Classroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekDay = table.Column<int>(type: "int", nullable: false),
                    OnShift = table.Column<int>(type: "int", nullable: false),
                    EndShift = table.Column<int>(type: "int", nullable: false),
                    NumberStudent = table.Column<int>(type: "int", nullable: false),
                    NumStudent = table.Column<int>(type: "int", nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemestersClass", x => x.SemesterClass_Id);
                    table.ForeignKey(
                        name: "FK_SemestersClass_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "SemesterId");
                    table.ForeignKey(
                        name: "FK_SemestersClass_Subjects_FK_SubjectId",
                        column: x => x.FK_SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_SemestersClass_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_SemestersClass_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserSubjects",
                columns: table => new
                {
                    UserSubject_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubjects", x => x.UserSubject_Id);
                    table.ForeignKey(
                        name: "FK_UserSubjects_Subjects_FK_SubjectId",
                        column: x => x.FK_SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_UserSubjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    StudentClass_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterClass_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => x.StudentClass_Id);
                    table.ForeignKey(
                        name: "FK_StudentClasses_SemestersClass_SemesterClass_Id",
                        column: x => x.SemesterClass_Id,
                        principalTable: "SemestersClass",
                        principalColumn: "SemesterClass_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentCode", "Name" },
                values: new object[,]
                {
                    { new Guid("0ab25235-7295-4cb4-abcf-7c8f66afdb10"), "KKT", "Khoa kinh tế" },
                    { new Guid("5feed829-1d72-4aa8-8446-4658853d85f9"), "KNN", "Khoa ngôn ngữ" },
                    { new Guid("9295a976-e816-49c9-aed0-6267320eaf88"), "KTNDS", "Khoa tài năng và đời sống" },
                    { new Guid("c1f5223f-a8b7-4c1f-a7e8-e6b845df9a09"), "KTDTT", "Khoa thể dục và thể thao" },
                    { new Guid("eaff2b81-dc92-4f4a-8d1a-b4953305f68e"), "KTT", "Khoa toán tin" },
                    { new Guid("f91c3e64-d3c1-47f1-95de-999484203077"), "KTH", "Khoa toán học" }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorsId", "MajorsCode", "MajorsName" },
                values: new object[,]
                {
                    { new Guid("5b4b810b-1408-432e-b180-30cde7173c12"), "NNH", "Ngôn ngữ hàn" },
                    { new Guid("896892a9-3dcc-4f71-be85-7daa974647b9"), "TI", "Khoa học và máy tính" },
                    { new Guid("deecf99e-e0b5-46f6-94b2-ae94b22283db"), "KTQT", "Kinh tế quốc tế" },
                    { new Guid("ec57c574-163d-4688-b98c-413e45a34684"), "CNTT", "Công nghệ thông tin" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ApiEndpoint", "Description", "Group", "PermissionName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0f840597-f075-446e-9694-b5001747ba4b"), "/api/Subject/class/student", "Xem thông tin trong lớp", 4, "ViewStudentWithClass", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3969) },
                    { new Guid("207f57b0-5039-4f58-9e7f-6a937ab71630"), "/api/User/class", "Xem các lớp mà giảng viên đã đăng ký", 3, "ViewUserClass", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3966) },
                    { new Guid("2f15d374-23a4-4726-af76-7d76a00d0a57"), "/api/User/update/class", "Giảng viên lập lịch dạy", 3, "ScheduleUser", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3968) },
                    { new Guid("3f6293b5-b56c-4332-a48e-ec9812b6878f"), "/api/Subject/create", "Tạo mới môn học", 1, "CreateSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3886) },
                    { new Guid("4e2b5a4b-ee6c-4b63-a95d-dc6d13b7e58c"), "/api/Subject/update", "Sửa môn học", 1, "UpdateSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3889) },
                    { new Guid("5d80e45a-8b80-4878-b8dc-3e83c6623453"), "/api/Student/delete", "Xóa sinh viên", 2, "DeleteStudent", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3898) },
                    { new Guid("6ad04b4d-bb5f-42da-a52f-b0a8aa7af5be"), "/api/Student/pagination", "Xem danh sách sinh viên", 2, "ViewStudent", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3950) },
                    { new Guid("6b1c2f7a-38c2-45f6-a9b7-9d93a5196a7c"), "/api/Subject/delete", "Xóa môn học", 1, "DeleteSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3892) },
                    { new Guid("73f4d492-8cdd-4285-8bc9-bb0412837257"), "/api/Student/subject_1", "Xem thời khóa biểu sinh viên", 2, "ViewStudentWithSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3951) },
                    { new Guid("8056df16-f36c-49d1-b6bb-f80b16069ce8"), "/api/Student/update", "Cập nhật thông tin sinh viên", 2, "UpdateStudent", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3900) },
                    { new Guid("86bdc666-2a59-4cc4-834f-ebd331065db2"), "/api/User/subject", "Xem các môn của giảng viên đã đăng ký dạy", 3, "ViewUserSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3964) },
                    { new Guid("a7e7f1c4-8e88-47a3-98f9-b4f0f9879cde"), "/api/Subject/pagination", "Xem danh sách môn học", 1, "ViewSubject", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3893) },
                    { new Guid("d91446a0-8897-45fd-b387-d70cb4c7c676"), "/api/Student/create", "Tạo mới sinh viên", 2, "CreateStudent", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3896) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatorName", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c"), "trungnguyen", "admin", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3846) },
                    { new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "trungnguyen", "user_0", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3849) },
                    { new Guid("15f257e4-9674-4a1a-acb7-076b115c1cf9"), "trungnguyen", "user_2", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3852) },
                    { new Guid("6b82d83c-834d-46a6-b182-7f33012aaece"), "trungnguyen", "user_3", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3854) },
                    { new Guid("7967c1aa-b77e-4599-9266-85c9b008f6bb"), "trungnguyen", "user_1", new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(3851) }
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "SemesterId", "Level", "Name", "SemesterName", "Status" },
                values: new object[,]
                {
                    { new Guid("07f835f2-a17e-4933-a8b6-de6bad0a0ff1"), 2, "K2N2", "Kì 2 nhóm 2", false },
                    { new Guid("875673eb-48b4-4201-bc62-65c7aad23625"), 0, "K3N3", "Kì 3 nhóm 3", false },
                    { new Guid("93dc7a54-d63f-4d8f-819d-297b941df7fc"), 6, "K3N1", "Kì 3 nhóm 1", false },
                    { new Guid("cadb635d-80dc-42d3-8271-e155591c89e5"), 5, "K2N1", "Kì 2 nhóm 1", false },
                    { new Guid("dc1f9223-ffcb-4334-bd1e-1192c42a8695"), 3, "K3N2", "Kì 3 nhóm 2", false },
                    { new Guid("dd2c3ba3-5f32-4847-8d24-9eb1b0e799a5"), 4, "K1N1", "Kì 1 nhóm 1", false },
                    { new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), 1, "K1N2", "Kì 1 nhóm 2", true }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreatedAt", "MajorName", "NumberOfCredits", "ParentId", "SubjectCode", "SubjectName" },
                values: new object[,]
                {
                    { new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4093), null, 2, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ABCDEF", "Tiếng anh trung cấp 2" },
                    { new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4122), null, 2, null, "ABCDEF", "Bóng rổ" },
                    { new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4100), null, 3, null, "ABCDEF", "Hội họa cơ bản" },
                    { new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4125), null, 3, null, "ABCDEF", "Thể dục cơ bản" },
                    { new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4104), null, 2, null, "ABCDEF", "Tiếng nhật 1" },
                    { new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4112), null, 3, null, "ABCDEF", "Công nghệ web" },
                    { new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4116), null, 2, null, "ABCDEF", "Công nghệ phần mềm" },
                    { new Guid("7e8ef101-3183-458b-9302-432c00a096da"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4107), null, 2, null, "ABCDEF", "Tiếng trung 1" },
                    { new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4120), null, 2, null, "ABCDEF", "Nhập môn khoa học dữ liệu" },
                    { new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4115), null, 3, null, "ABCDEF", "C#" },
                    { new Guid("943e6618-cee5-460f-b003-b53c5014619e"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4109), null, 2, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "ABCDEF", "Tiếng nhật 2" },
                    { new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4097), null, 2, null, "ABCDEF", "Bóng bàn" },
                    { new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4124), null, 2, null, "ABCDEF", "Bóng chuyền" },
                    { new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4106), null, 2, null, "ABCDEF", "Tiếng hàn 1" },
                    { new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4111), null, 3, null, "ABCDEF", "An toàn thông tin" },
                    { new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4098), null, 3, null, "ABCDEF", "Kỹ năng sống" },
                    { new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4121), null, 2, null, "ABCDEF", "Dữ liệu lớn" },
                    { new Guid("ba805446-367c-4929-b746-fc797388e8ef"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4118), null, 2, null, "ABCDEF", "Học máy" },
                    { new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4101), null, 4, null, "ABCDEF", "Bóng đá" },
                    { new Guid("dade835c-ca38-45bf-a591-57ca00ba232f"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4095), null, 2, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ABCDEF", "Tiếng anh trung cấp 3" },
                    { new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4103), null, 3, null, "ABCDEF", "Giải tích 1" },
                    { new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4090), null, 2, null, "ABCDEF", "Tiếng anh trung cấp 1" },
                    { new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4113), null, 3, null, "ABCDEF", "Khai phá dữ liệu" }
                });

            migrationBuilder.InsertData(
                table: "MajorsSubjects",
                columns: new[] { "MajorsSubject_Id", "Category", "FK_SubjectId", "IsActive", "MajorsId" },
                values: new object[,]
                {
                    { new Guid("15bfd834-6611-4e03-a495-d452a296b98c"), "TA", new Guid("dade835c-ca38-45bf-a591-57ca00ba232f"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("16e3b576-4223-4916-b13a-84e9883cf786"), "NN", new Guid("943e6618-cee5-460f-b003-b53c5014619e"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("1a479e44-8338-4450-9b9e-fb557c93fd24"), "CN", new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("2435e635-404d-40d3-9ef1-085fc8dffdd0"), "CN", new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("25d21f4f-dc47-4d00-afbe-cddf4e7b1812"), "NN", new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("3184a360-bb38-4431-9a9e-27fb5f4cb07b"), "CN_TD", new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("3d7502c9-ac57-4656-8bc6-7507484843b7"), "TA", new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("616fddea-a71d-419c-85d7-65c0b2fd5d07"), "TT", new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("63a09c5b-c22a-46e8-8b7d-75ff958e2186"), "CN", new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("6d0203c6-1048-45f2-a581-9d5f392f7730"), "TT", new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("70027c20-672a-4a61-aff5-a05ce63254de"), "NN", new Guid("7e8ef101-3183-458b-9302-432c00a096da"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("7c528e7d-8f00-4b73-bb5f-a284f5b43afb"), "CN", new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("7f1dc90a-2730-4ee8-95b3-41042f8e9ef6"), "TT", new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("8510c0d9-8814-4faf-b98c-e54950a440c1"), "TA", new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("86916439-86d9-4c8d-b60a-7c158f797638"), "TD", new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("8dce1140-cfec-4d2f-b214-55ad4ae30c33"), "TT", new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("98677670-dcdf-4e19-a84d-fb258013b757"), "CN", new Guid("ba805446-367c-4929-b746-fc797388e8ef"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("ad18a679-9ff0-4334-9d5c-d171f81f6333"), "NN", new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("be423fe9-9e3f-48ad-9a58-cec77acd747a"), "CN_TD", new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("c6903263-ef8e-4ad4-a64c-ffce4c7c0e29"), "TD", new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("ed7a8363-a932-4ea2-a0da-f134fa719d1e"), "CN", new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("f948ba24-33a3-4fb0-aa0a-7c16372aaba5"), "CN", new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") },
                    { new Guid("ffe3d618-b797-4387-91d8-ad3f8f0f612b"), "TD", new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), true, new Guid("896892a9-3dcc-4f71-be85-7daa974647b9") }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RolePermission_Id", "PermissionId", "Permission_Id", "RoleId", "Role_Id" },
                values: new object[,]
                {
                    { new Guid("28139e73-2dde-48d5-bc03-5c3ba6f1287f"), null, new Guid("a7e7f1c4-8e88-47a3-98f9-b4f0f9879cde"), null, new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c") },
                    { new Guid("369edb08-f881-4cc3-8a9d-16a7c45c3c13"), null, new Guid("3f6293b5-b56c-4332-a48e-ec9812b6878f"), null, new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c") },
                    { new Guid("37129f77-4ff4-4a7a-8189-ca16b3ea4a3f"), null, new Guid("a7e7f1c4-8e88-47a3-98f9-b4f0f9879cde"), null, new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b") },
                    { new Guid("725fc4cc-474f-487a-8637-639ed0b246e9"), null, new Guid("6b1c2f7a-38c2-45f6-a9b7-9d93a5196a7c"), null, new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c") },
                    { new Guid("e218a658-7fc0-406d-9994-7d33ded6b034"), null, new Guid("4e2b5a4b-ee6c-4b63-a95d-dc6d13b7e58c"), null, new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c") }
                });

            migrationBuilder.InsertData(
                table: "SemesterSubjects",
                columns: new[] { "SemesterSubject_Id", "FK_SubjectId", "SemesterId" },
                values: new object[,]
                {
                    { new Guid("18c1a9aa-08a8-4192-acfe-a24b38c9d74e"), new Guid("943e6618-cee5-460f-b003-b53c5014619e"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("2176d1fd-bdb8-40fa-9460-d0e5a4bda394"), new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("22417cc0-aa74-4d83-b883-d63a9c1ef47c"), new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("31c8284b-ae71-46bf-9294-ffe6ecf0ca26"), new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("39049821-d320-462b-89d3-ba29a21631f3"), new Guid("7e8ef101-3183-458b-9302-432c00a096da"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("48247252-061f-412f-861f-7e2b02c462e2"), new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("59b79cfe-8e78-42ef-8d4f-78e01b9fc71f"), new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("677b57a4-418e-4099-a461-50d72eeea447"), new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("768d3b99-0105-4cfe-8501-2d93119c22c5"), new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("87fe9b31-48ef-4175-a8ab-9ffea74d139a"), new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("897fd7c8-1f08-43da-a8a9-2f4bb29d0887"), new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("907b2327-7f8c-48ee-b4b7-2e5d70d730c6"), new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("988f3a61-1a65-4b0f-94ff-56e0a09ec042"), new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("99980519-de4d-4b74-a248-32023c2dac97"), new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("a4ce5e08-8010-4ce2-bc1a-92abe1086e58"), new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("a85e5407-5e57-4ee0-b9c6-aa393d449964"), new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("a9787aa0-1ae1-4c6c-8d9e-793b5b26cd90"), new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("ae7a2831-83c8-400f-87ea-b07a8be6fa09"), new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("aef7a699-033f-4e80-aef2-87c61e90bda9"), new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("b03c3285-f47a-426e-836b-c73d3493400e"), new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("e2554f05-2365-49f3-82b2-8edb8e17e051"), new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("e2d0d0d3-e9a8-4e49-983c-c13ff8472952"), new Guid("dade835c-ca38-45bf-a591-57ca00ba232f"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") },
                    { new Guid("e3e30662-96c4-4651-97e3-949a369935b6"), new Guid("ba805446-367c-4929-b746-fc797388e8ef"), new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140") }
                });

            migrationBuilder.InsertData(
                table: "SemestersClass",
                columns: new[] { "SemesterClass_Id", "ClassNumber", "Classroom", "Describe", "EndShift", "FK_SubjectId", "Name", "NumStudent", "NumberStudent", "OnShift", "SemesterId", "SubjectId", "UserId", "WeekDay" },
                values: new object[,]
                {
                    { new Guid("009391c4-fb67-418a-b1c8-0355814d33cb"), 1, "B403", null, 5, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("049fff7a-ad70-4ec5-a4a3-0b21287cf60a"), 3, "B603", null, 7, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("062e2361-fda9-4f58-b466-46dcee5513d7"), 2, "B509", null, 3, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ANHTC2", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("06f65383-fb31-4a24-a7a5-de9ceddeb319"), 2, "B519", "LT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("0a1ab7ff-8220-4407-9fe3-54b2a822f9b3"), 2, "B409", null, 10, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("100ef7b0-7d18-42f7-94c0-d0c2361bac23"), 1, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("12284727-7767-47b2-ab89-3a94f5e6c71c"), 1, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("139c1ea5-2128-4153-beca-262a0724c24d"), 1, "B408", null, 7, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("17874ef8-5463-46b6-b029-2aa4521b3cf1"), 1, "B608", null, 2, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("17aecae1-4a32-4a0c-a9e5-47007a3d605d"), 2, "B608", null, 5, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("180f16cc-52fd-4f9c-90ea-d16c5f41da7d"), 2, "B407", null, 2, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("18e16370-b3a0-4f70-a39a-2343421b40e8"), 1, "A710", null, 5, new Guid("ba805446-367c-4929-b746-fc797388e8ef"), "HOCMAY", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("1b8181a9-fb8c-420b-9326-84379081975d"), 2, "B604", "LT", 3, new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), "HOIHOACB", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("1f5f007e-7b0a-4c6a-8833-cc7cbcb0e365"), 4, "NHATC", null, 10, new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), "BONGRO", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("22739e10-143a-4b53-943c-add6744c2668"), 1, "NHATC", null, 5, new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), "BONGCHUYEN", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("29cd6ef7-d8bf-4c58-bc13-e55e9c08c966"), 2, "A704", null, 10, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("2ad5a328-8334-4bd2-bd32-6dec0b955507"), 1, "B520", "BT", 7, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("2bc66f0b-a2c7-4542-b190-698ba9f36f50"), 2, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("2f53b3b5-215c-44f3-97df-1ff1aabe658e"), 1, "B609", null, 2, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("30505417-aa51-4003-ae1c-3728e411ca3d"), 2, "B502", null, 2, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("3921ad3b-5844-42a8-9914-7838600a9de3"), 2, "B502", null, 2, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("3a4bdd8b-ad5e-43a3-958b-a78fdc6f7167"), 3, "B404", null, 10, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("3cbf310d-1dd3-43c0-8893-afe182aa73a6"), 1, "B406", null, 5, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("3d1db7b9-e48c-4fd7-872c-6985ae48d9c8"), 4, "NHATC", null, 5, new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), "BONGBAN", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("3d9fe0a3-101d-44c0-bb53-a155406ac492"), 1, "B607", null, 2, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("40747ed2-4817-4c14-9a39-56ab38e657b6"), 1, "B512", null, 2, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ANHTC2", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("42345d6d-b593-4a20-82f1-25e5c5a1ab68"), 3, "B518", "LT", 8, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("468f8a54-3cad-421e-ac47-d49d5b864ec2"), 3, "B502", null, 7, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("4af823a6-d03a-4160-856f-74d3b23bcd2f"), 2, "B604", null, 5, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("4b1ec90d-5417-4e3c-87b7-8591f513bbfc"), 2, "B407", null, 2, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("525ec3e6-2f59-4967-938b-af27431681ba"), 1, "A705", null, 7, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("535c87f3-5133-4fe4-9c1f-e9f3b99fd784"), 3, "B518", "BT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("5a1023ee-6d24-45fa-8154-9005075a5063"), 1, "B511", null, 2, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ANHTC2", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("5b2a624a-5a2a-45fd-b3c8-c9fe289fe7d4"), 2, "B407", null, 2, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("5bc136e3-cda6-4a67-a51d-6ed96eb9dce2"), 3, "B607", null, 5, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("61c15b0c-a7e7-4e0d-850f-381e932d2dda"), 2, "A710", null, 2, new Guid("ba805446-367c-4929-b746-fc797388e8ef"), "HOCMAY", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("61d63c54-5481-42e5-bb3d-a26a785cf74f"), 1, "B520", "LT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("632d9fc0-808d-4d64-914d-4a829a0c099c"), 3, "A703", null, 10, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 8, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("6379dd1a-d948-4294-a910-ae8129e82b0d"), 3, "B401", null, 3, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("6496a8f6-5e2f-46fc-8a44-86ec8cbea03a"), 3, "A705", null, 5, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("669febde-1bfe-497e-b895-a9a7e36bc794"), 3, "B502", null, 7, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("66f5d719-c567-4ac4-b72b-54234724155c"), 1, "A703", null, 10, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("68ce2e3a-3e08-4262-97b0-10aa22f009f2"), 2, "A711", null, 7, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("69a34517-c170-4030-9154-89c3cf5c530c"), 1, "B403", null, 10, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 8, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("6a44f194-5ae2-4dd8-8acc-379b76508f22"), 1, "B406", null, 5, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("6eafc8f4-f4b7-455d-8b31-6d94682efbe8"), 1, "B503", null, 5, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("6f46769f-81c2-4732-8623-93d166fb56af"), 2, "B502", null, 2, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("70f1df1f-4344-4349-b4a1-ce31e9ddc48a"), 2, "NHATC", null, 7, new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), "BONGBAN", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("734cdbf5-34d1-412d-8f97-3767c0b2f6b4"), 2, "B605", null, 5, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("7390b863-0f9c-480d-bb2b-bb743bb7ee11"), 1, "A705", null, 3, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("7487a539-8bce-4fbe-b5d2-aaabf028e15b"), 2, "B405", null, 7, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("766f2282-0fd8-4685-8626-90f00f98d48a"), 1, "B408", null, 7, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("7860a33a-99d4-412d-93c3-7410c38f01a5"), 3, "A705", null, 7, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("7be3d0e9-0756-4820-9df2-70a44fc3e471"), 1, "B501", null, 2, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("7d22a304-a118-4ccc-af13-af124c77ca86"), 2, "B510", null, 3, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ANHTC2", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("7e528378-c34d-422a-aee5-83286b1e887b"), 3, "B601", null, 7, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("7e85df67-6251-42cc-8f85-d164845e9d37"), 1, "A710", null, 8, new Guid("ba805446-367c-4929-b746-fc797388e8ef"), "HOCMAY", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("8145d5ca-f532-4405-84c9-ef0e630f2ea7"), 1, "NHATC", null, 7, new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), "BONGBAN", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("8155afcc-5ea2-4390-9e44-5d785ca85163"), 2, "A711", null, 8, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("821eee35-039b-4db3-91fb-ea00ae515857"), 1, "A703", null, 5, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("83c143a6-e663-44f8-b0b6-24ebaa712f60"), 1, "A712", null, 5, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("840bad6e-1f9d-4075-a2a5-18cf40144845"), 3, "NHATC", null, 5, new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), "BONGRO", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("8560ba39-54f0-455b-b518-f7ecdaa23b5e"), 1, "NHATC", null, 2, new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), "THEDUCCB", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("85e68cb7-cb0c-4ba8-98c0-cb48e57603f4"), 2, "A703", null, 10, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("86632368-9ea2-4038-96bb-b845107393cf"), 1, "A708", null, 2, new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), "NHAPMONKHDL", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("8a30d195-4663-4c9b-ac50-5e5de1530d1e"), 1, "NHATC", null, 2, new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), "BONGRO", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("91ace230-48d2-45c7-b007-ee024d3ae698"), 2, "B519", "BT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("930eba70-be56-4fa2-8a82-394479e023d4"), 2, "B606", null, 5, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("97cd0e61-9bbf-4adf-9767-b002a75e47de"), 3, "A710", null, 5, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("9a3462ba-2831-4d65-b933-7a08e8c2cf45"), 3, "B404", null, 10, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("9c48f9aa-e98d-4bc9-9ec6-8db6fd9d1cd2"), 2, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("9cb5aec7-10c6-4f22-a34a-8096c8cdd065"), 2, "A709", null, 8, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("9d0469ef-6f54-4b03-99a0-d61e1b1d99d8"), 1, "B609", null, 5, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("9d8a42eb-a97f-4190-8030-a4a10cacb27b"), 2, "B409", null, 10, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("9eb7e532-812d-4972-811b-cb440dc9db34"), 1, "B503", null, 5, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("9ecb7923-0392-47ad-94f5-b69b2e5ac9c5"), 2, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("9f6bfe06-6eea-4493-95b6-53b90268ae9b"), 1, "B605", "BT", 8, new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), "HOIHOACB", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 8 },
                    { new Guid("a10b35dd-7038-42ac-b3c1-d3fe9488687b"), 1, "B501", null, 2, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("a3a8bf0c-e745-4784-a7f3-2d25de33282e"), 1, "B406", null, 5, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("a52a7029-e74e-4f47-957e-54678eaef3ad"), 1, "A705", null, 5, new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), "DULIEULON", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("a6fdb7e8-54eb-48a5-9a89-0af108cac15c"), 1, "A708", null, 5, new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), "NHAPMONKHDL", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("a72621ad-b25d-4e7c-971e-99cd17cc152e"), 1, "A709", null, 10, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("aa60c46b-18f8-493c-af97-6f04cb3ed3db"), 2, "A705", null, 10, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 8, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("adf15b9f-92c7-4fe3-afb8-f78bcd5e366c"), 1, "B606", "LT", 8, new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), "HOIHOACB", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("b107224a-6978-4461-9a15-509898348801"), 2, "B409", null, 10, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("b52fa8e6-2f34-4727-90dd-86752984e334"), 1, "B520", "BT", 7, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("b5f472ee-f7e7-4a53-9eb2-37cba72512b6"), 2, "B604", "BT", 3, new Guid("19018876-bd0e-4271-aae6-38831dd7cb20"), "HOIHOACB", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 8 },
                    { new Guid("b99c502e-5d1f-4787-94b7-0c95bb471562"), 3, "B602", null, 7, new Guid("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"), "ANHTC1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("ba44aab7-39d8-4ab9-ba76-d684233e0e7c"), 4, "NHATC", null, 2, new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), "BONGCHUYEN", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("baf103c1-57f2-41f6-8026-0718d8a7feea"), 3, "B607", null, 8, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("bc6c6a6b-5cfe-411a-a833-9463be8de3a1"), 3, "A703", null, 5, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("bfc39851-0b04-44af-b661-9545f5217d07"), 2, "NHATC", null, 7, new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), "BONGCHUYEN", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("c18803c6-7230-4956-a980-f11a7b88750a"), 1, "B503", null, 5, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("c1e398b7-3289-40aa-a2f5-43091c6f74db"), 3, "B401", null, 7, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("c24aa759-079e-4674-bff8-41d797c74cc4"), 2, "A703", null, 5, new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), "CNWEB", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("c4b10683-bb36-40cb-8f94-b5130264126c"), 2, "A708", null, 5, new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), "NHAPMONKHDL", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("ca3fc095-00f5-4c6f-8f3c-bb9b125b564d"), 1, "B513", null, 2, new Guid("0c3ad6be-0f5e-4f65-8808-ff35bed77acb"), "ANHTC2", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("ccbb375a-788c-429e-abd5-36ca46815dcb"), 1, "CBN", null, 2, new Guid("bd56cf9b-45b9-42ce-9c84-60b32350424c"), "BONGDA", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("ccda95ba-fe85-49be-bafd-dc55cd2cb063"), 3, "A704", null, 5, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("ccebad90-998d-4aab-a589-73ce19907263"), 1, "B408", null, 7, new Guid("7e8ef101-3183-458b-9302-432c00a096da"), "TIENGTRUNG1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("ce0c58cf-5bcb-4c10-92fc-bb23abb128e7"), 1, "A712", null, 7, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("cf9125b4-36c0-46f2-bde8-8674a00a2732"), 1, "B501", null, 2, new Guid("a9ff86b6-3fe8-4980-b2da-01aef0c73e36"), "TIENGHAN1", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("d0a48c0d-b2a3-4ea8-9924-440c090f5907"), 2, "A705", null, 5, new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), "C#", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("d19b80cf-2a7d-4dbd-acf3-e9a95f123349"), 1, "B609", null, 8, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("d1c6b378-2ff6-4539-9ad1-42e0cb025e8c"), 1, "A705", null, 10, new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), "DULIEULON", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("d349a01e-6b15-4405-ba98-e4970646bae6"), 2, "B405", null, 7, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("d3a131c5-a26c-41db-b32a-81842751a4f3"), 2, "A710", null, 5, new Guid("ba805446-367c-4929-b746-fc797388e8ef"), "HOCMAY", 0, 30, 3, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("dbb3912e-8bdf-411e-a73d-b09ca8f98c9d"), 3, "B518", "BT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("e472ea01-b9b2-4ac4-934e-2010b2b916ec"), 2, "B519", "BT", 5, new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), "GIAITICH1", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("e79123ed-86fb-48a5-a749-8fd7ae31920e"), 3, "A709", null, 5, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("e7da4abb-7804-403f-8f68-cc83fd4bd627"), 3, "B502", null, 7, new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c"), "TIENGNHAT1", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 3 },
                    { new Guid("ecd939bb-8ba6-4f65-b6fc-791d6c18b335"), 1, "A704", null, 8, new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), "KHAIPHADL", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("f122996d-3742-45f7-9036-ebf0475a539b"), 2, "B608", null, 8, new Guid("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"), "KNS", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("f63d4404-3855-48ce-a98d-a928f8777a45"), 3, "NHATC", null, 10, new Guid("a7e2f789-34fc-4b6e-b308-50b70bc68d43"), "BONGBAN", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("f7bf6059-5372-4af3-8001-87ba9a5bd052"), 3, "NHATC", null, 10, new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), "THEDUCCB", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("f81aca96-d847-4228-999b-279c98894697"), 3, "B404", null, 10, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("f863c7b3-ac52-47d2-8068-3d38da8e8098"), 2, "B402", null, 10, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 8, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("f88fdd16-b928-4ab8-86d1-18d306d7d66c"), 4, "NHATC", null, 2, new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), "THEDUCCB", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("f91bc021-ff75-454a-af58-3808238f5933"), 2, "NHATC", null, 10, new Guid("6349c24f-8e64-4512-9aa7-8c887f16fa92"), "THEDUCCB", 0, 30, 9, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 },
                    { new Guid("fbcd6572-e216-49cb-a1a4-f81cdb57732d"), 2, "B405", null, 7, new Guid("943e6618-cee5-460f-b003-b53c5014619e"), "TIENGNHAT2", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 7 },
                    { new Guid("fcf0581c-ad66-4d83-9dbd-67c8af2db768"), 3, "NHATC", null, 2, new Guid("a907092b-4f8f-4efa-8468-429b2a3801d4"), "BONGCHUYEN", 0, 30, 1, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 6 },
                    { new Guid("fd8eaa84-fbff-4109-9689-7d782365ba7c"), 3, "A710", null, 8, new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), "CNPHANMEM", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 4 },
                    { new Guid("fe3d1212-7f43-4430-a6ca-77061d3b0439"), 2, "B402", null, 5, new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), "ANTOANTTIN", 0, 30, 4, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("ff0df8ff-503c-4151-846c-ae914a9db92f"), 2, "NHATC", null, 7, new Guid("0d20c2f0-c1d7-41fc-af0c-f6f12e8b74d3"), "BONGRO", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 2 },
                    { new Guid("ffe835d0-499e-47e0-a02e-52773b0640d4"), 2, "A708", null, 8, new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), "NHAPMONKHDL", 0, 30, 6, new Guid("de7fbe40-99f1-4cb0-a85a-0241c4f89140"), null, null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Email", "MajorsId", "NumberOfCredits", "Password", "Status", "UserName" },
                values: new object[,]
                {
                    { new Guid("561d6341-41ce-43a3-85f1-e1cda2d58336"), "chun23@gmail.com", new Guid("896892a9-3dcc-4f71-be85-7daa974647b9"), 0, "1232003", "BT", "trung23" },
                    { new Guid("b3d9d4be-0f56-4921-8895-e60f7eb3052e"), "trung2@gmail.com", new Guid("896892a9-3dcc-4f71-be85-7daa974647b9"), 0, "1232003", "BT", "trung2" },
                    { new Guid("ca8ab9a6-7ced-4b2b-b907-7ebf17a8bedd"), "trung1@gmail.com", new Guid("896892a9-3dcc-4f71-be85-7daa974647b9"), 0, "1232003", "BT", "trung1" },
                    { new Guid("d7a42f7a-1aae-4f4a-bb64-2c1c63d9b328"), "chun23@gmail.com", new Guid("896892a9-3dcc-4f71-be85-7daa974647b9"), 0, "1232003", "BT", "chun23" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DepartmentId", "Email", "Name", "Password", "RoleId", "UserName" },
                values: new object[,]
                {
                    { new Guid("40a200bf-c6e6-4fd7-b791-5d6728f6c5ac"), new Guid("c1f5223f-a8b7-4c1f-a7e8-e6b845df9a09"), "giangnguyen2003@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "giangnguyen" },
                    { new Guid("88abd326-4b9c-45ac-9029-632a8674f01a"), new Guid("9295a976-e816-49c9-aed0-6267320eaf88"), "lienpham2003@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "lienpham" },
                    { new Guid("8af6cd07-6e3a-4cb2-8a0b-9cf92c432e9d"), null, "chun2003@gmail.com", null, "1232003", new Guid("044c9907-d2de-4e1e-ae33-8a9565caa44c"), "xiaochun" },
                    { new Guid("aca3e7bf-9999-4ee7-91b1-961be8d1da0c"), new Guid("0ab25235-7295-4cb4-abcf-7c8f66afdb10"), "ayng2005@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "yang" },
                    { new Guid("c652bcb2-0e43-4f7d-9217-8b4b0cf423f8"), new Guid("5feed829-1d72-4aa8-8446-4658853d85f9"), "mimi2003@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "mimi" },
                    { new Guid("e1f0503a-75f5-4062-af33-ecb7c591c198"), new Guid("eaff2b81-dc92-4f4a-8d1a-b4953305f68e"), "giangnguyen2003@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "giangnguyen" },
                    { new Guid("fd1d94a0-c819-4649-938a-2544281bc301"), new Guid("eaff2b81-dc92-4f4a-8d1a-b4953305f68e"), "trungnguyen2003@gmail.com", null, "1232003", new Guid("0b3e6d46-4e19-48ab-8998-7cf4186acd9b"), "trungnguyen" }
                });

            migrationBuilder.InsertData(
                table: "StudentsSubjects",
                columns: new[] { "StudentSubject_Id", "Complete", "CreateAt", "Scores", "Status", "StudentId", "SubjectId" },
                values: new object[] { new Guid("d832a0a4-1c0d-4bf0-ac73-f15962165f95"), false, new DateTime(2024, 5, 28, 1, 23, 4, 962, DateTimeKind.Local).AddTicks(4909), null, "ĐH", new Guid("561d6341-41ce-43a3-85f1-e1cda2d58336"), new Guid("67fe6d4a-8985-4a45-94cb-9a078d01d38c") });

            migrationBuilder.InsertData(
                table: "UserSubjects",
                columns: new[] { "UserSubject_Id", "FK_SubjectId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0495f94e-69d0-4495-8956-07dc4ebff33b"), new Guid("e9d3b36f-45cf-4d86-8743-aceca695d7fd"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("2b57533c-c0d2-4423-b22e-78140f25d380"), new Guid("69c422c1-e837-49c0-8c02-84568b4dce1d"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("35c36f09-0fd2-48fd-a9c2-bc1e309ace74"), new Guid("b86d8635-f095-415f-a4d8-2b415d836abb"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("36036d08-c4d3-43c2-9eec-8441a61eabcc"), new Guid("865a4ee2-4882-4757-a1d1-811f03415e32"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("40cc0d43-4ef8-494d-a4aa-ddcb973416d2"), new Guid("857d8adb-74a1-403d-925d-f9a133a5b095"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("6301f0d6-c2eb-465b-99fc-ff87b4747655"), new Guid("f98283c6-66b5-4e62-b3b0-04a000b8b0b2"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("77349ddd-737e-462a-a2fa-37e0993077c8"), new Guid("7945d608-0166-4dee-a119-940f7c8df13d"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("87ca1c85-2de3-4fd8-8d3b-6859d7877b99"), new Guid("ba805446-367c-4929-b746-fc797388e8ef"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") },
                    { new Guid("f65f09e3-aa44-424b-a479-bcdbf736eed0"), new Guid("b158e77a-e8f4-43a1-abc7-65d1896ac005"), new Guid("fd1d94a0-c819-4649-938a-2544281bc301") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MajorsSubjects_FK_SubjectId",
                table: "MajorsSubjects",
                column: "FK_SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorsSubjects_MajorsId",
                table: "MajorsSubjects",
                column: "MajorsId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_Permission_Id",
                table: "RolePermissions",
                column: "Permission_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_Role_Id",
                table: "RolePermissions",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersClass_FK_SubjectId",
                table: "SemestersClass",
                column: "FK_SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersClass_SemesterId",
                table: "SemestersClass",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersClass_SubjectId",
                table: "SemestersClass",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersClass_UserId",
                table: "SemestersClass",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubjects_FK_SubjectId",
                table: "SemesterSubjects",
                column: "FK_SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubjects_SemesterId",
                table: "SemesterSubjects",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_SemesterClass_Id",
                table: "StudentClasses",
                column: "SemesterClass_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_StudentId",
                table: "StudentClasses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MajorsId",
                table: "Students",
                column: "MajorsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjects_StudentId",
                table: "StudentsSubjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubjects_FK_SubjectId",
                table: "UserSubjects",
                column: "FK_SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubjects_UserId",
                table: "UserSubjects",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MajorsSubjects");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SemesterSubjects");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropTable(
                name: "StudentsSubjects");

            migrationBuilder.DropTable(
                name: "UserSubjects");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "SemestersClass");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
