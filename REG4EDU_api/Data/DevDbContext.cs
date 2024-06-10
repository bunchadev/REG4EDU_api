using Microsoft.EntityFrameworkCore;
using REG4EDU_api.Model.Domain;

namespace REG4EDU_api.Data
{
    public class DevDbContext : DbContext
    {
        public DevDbContext(DbContextOptions<DevDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SemesterClass> SemestersClass { get; set; }
        public DbSet<SemesterSubject> SemesterSubjects { get; set; }
        public DbSet<StudentSubject> StudentsSubjects { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Majors> Majors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MajorsSubject> MajorsSubjects { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RolePermission>()
                .HasOne(r => r.Role)
                .WithMany()
                .HasForeignKey(r => r.Role_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RolePermission>()
                .HasOne(r => r.Permission)
                .WithMany()
                .HasForeignKey(r => r.Permission_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SemesterClass>()
                .HasOne(s => s.Semester)
                .WithMany()
                .HasForeignKey(s => s.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SemesterClass>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SemesterClass>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.FK_SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StudentClass>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<StudentClass>()
                .HasOne(s => s.SemesterClass)
                .WithMany()
                .HasForeignKey(s => s.SemesterClass_Id);
            modelBuilder.Entity<Student>()
                .HasOne(m => m.Majors)
                .WithMany()
                .HasForeignKey(m => m.MajorsId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasOne(d => d.Department)
                .WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.FK_SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(s => s.Semester)
                .WithMany()
                .HasForeignKey(s => s.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MajorsSubject>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.FK_SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MajorsSubject>()
                .HasOne(m => m.Majors)
                .WithMany()
                .HasForeignKey(m => m.MajorsId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserSubject>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.FK_SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserSubject>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            var departments = new List<Department>()
            {
                new Department
                {
                    DepartmentId = Guid.Parse("EAFF2B81-DC92-4F4A-8D1A-B4953305F68E"),
                    Name = "Khoa toán tin",
                    DepartmentCode = "KTT"
                },
                new Department
                {
                    DepartmentId = Guid.Parse("0AB25235-7295-4CB4-ABCF-7C8F66AFDB10"),
                    Name = "Khoa kinh tế",
                    DepartmentCode = "KKT"
                },
                new Department
                {
                    DepartmentId = Guid.Parse("5FEED829-1D72-4AA8-8446-4658853D85F9"),
                    Name = "Khoa ngôn ngữ",
                    DepartmentCode = "KNN"
                },
                new Department
                {
                    DepartmentId = Guid.Parse("9295A976-E816-49C9-AED0-6267320EAF88"),
                    Name = "Khoa tài năng và đời sống",
                    DepartmentCode = "KTNDS"
                },
                new Department
                {
                    DepartmentId = Guid.Parse("C1F5223F-A8B7-4C1F-A7E8-E6B845DF9A09"),
                    Name = "Khoa thể dục và thể thao",
                    DepartmentCode = "KTDTT"
                },
                new Department
                {
                    DepartmentId = Guid.Parse("F91C3E64-D3C1-47F1-95DE-999484203077"),
                    Name = "Khoa toán học",
                    DepartmentCode = "KTH"
                }
            };
            modelBuilder.Entity<Department>().HasData(departments);
            var majors = new List<Majors>()
            {
                new Majors
                {
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    MajorsName = "Khoa học và máy tính",
                    MajorsCode = "TI"
                },
                new Majors
                {
                    MajorsId = Guid.Parse("EC57C574-163D-4688-B98C-413E45A34684"),
                    MajorsName = "Công nghệ thông tin",
                    MajorsCode = "CNTT"
                },
                new Majors
                {
                    MajorsId = Guid.Parse("DEECF99E-E0B5-46F6-94B2-AE94B22283DB"),
                    MajorsName = "Kinh tế quốc tế",
                    MajorsCode = "KTQT"
                },
                new Majors
                {
                    MajorsId = Guid.Parse("5B4B810B-1408-432E-B180-30CDE7173C12"),
                    MajorsName = "Ngôn ngữ hàn",
                    MajorsCode = "NNH"
                }
            };
            modelBuilder.Entity<Majors>().HasData(majors);
            var students = new List<Student>()
            {
                new Student
                {
                    StudentId = Guid.Parse("d7a42f7a-1aae-4f4a-bb64-2c1c63d9b328"),
                    UserName = "chun23",
                    Email = "chun23@gmail.com",
                    Password = "1232003",
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9")
                },
                new Student
                {
                    StudentId = Guid.Parse("561D6341-41CE-43A3-85F1-E1CDA2D58336"),
                    UserName = "trung23",
                    Email = "chun23@gmail.com",
                    Password = "1232003",
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9")
                },
                new Student
                {
                    StudentId = Guid.NewGuid(),
                    UserName = "trung1",
                    Email = "trung1@gmail.com",
                    Password = "1232003",
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9")
                },
                new Student
                {
                    StudentId = Guid.NewGuid(),
                    UserName = "trung2",
                    Email = "trung2@gmail.com",
                    Password = "1232003",
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9")
                }
            };
            modelBuilder.Entity<Student>().HasData(students);
            var semester = new List<Semester>()
            {
                new Semester
                {
                    SemesterId = Guid.NewGuid(),
                    Name = "K3N3",
                    SemesterName = "Kì 3 nhóm 3",
                    Level = 0,
                    Status = false
                },
                new Semester
                {
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    Name = "K1N2",
                    SemesterName = "Kì 1 nhóm 2",
                    Level = 1,
                    Status = true
                },
                new Semester
                {
                    SemesterId = Guid.Parse("07F835F2-A17E-4933-A8B6-DE6BAD0A0FF1"),
                    Name = "K2N2",
                    SemesterName = "Kì 2 nhóm 2",
                    Level = 2
                },
                new Semester
                {
                    SemesterId = Guid.Parse("DC1F9223-FFCB-4334-BD1E-1192C42A8695"),
                    Name = "K3N2",
                    SemesterName = "Kì 3 nhóm 2",
                    Level = 3
                },
                new Semester
                {
                    SemesterId = Guid.Parse("DD2C3BA3-5F32-4847-8D24-9EB1B0E799A5"),
                    Name = "K1N1",
                    SemesterName = "Kì 1 nhóm 1",
                    Level = 4
                },
                new Semester
                {
                    SemesterId = Guid.Parse("CADB635D-80DC-42D3-8271-E155591C89E5"),
                    Name = "K2N1",
                    SemesterName = "Kì 2 nhóm 1",
                    Level = 5
                },
                new Semester
                {
                    SemesterId = Guid.Parse("93DC7A54-D63F-4D8F-819D-297B941DF7FC"),
                    Name = "K3N1",
                    SemesterName = "Kì 3 nhóm 1",
                    Level = 6
                },
            };
            modelBuilder.Entity<Semester>().HasData(semester);
            var roles = new List<Role>()
            {
                new Role
                {
                    Id = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                    RoleName = "admin",
                    CreatorName = "trungnguyen",
                    UpdatedAt = DateTime.Now,
                },
                new Role
                {
                    Id = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    RoleName= "user_0",
                    CreatorName = "trungnguyen",
                    UpdatedAt = DateTime.Now,
                },
                new Role
                {
                    Id = Guid.Parse("7967C1AA-B77E-4599-9266-85C9B008F6BB"),
                    RoleName = "user_1",
                    CreatorName= "trungnguyen",
                    UpdatedAt = DateTime.Now,
                },
                new Role
                {
                    Id = Guid.Parse("15F257E4-9674-4A1A-ACB7-076B115C1CF9"),
                    RoleName = "user_2",
                    CreatorName= "trungnguyen",
                    UpdatedAt = DateTime.Now,
                },
                new Role
                {
                    Id = Guid.Parse("6B82D83C-834D-46A6-B182-7F33012AAECE"),
                    RoleName = "user_3",
                    CreatorName= "trungnguyen",
                    UpdatedAt = DateTime.Now,
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
            var permissions = new List<Permission>()
            {
                new Permission
                {
                    Id = Guid.Parse("3F6293B5-B56C-4332-A48E-EC9812B6878F"),
                    PermissionName = "CreateSubject",
                    ApiEndpoint = "/api/Subject/create",
                    Group = 1,
                    Description = "Tạo mới môn học"
                },
                new Permission
                {
                    Id = Guid.Parse("4E2B5A4B-EE6C-4B63-A95D-DC6D13B7E58C"),
                    PermissionName = "UpdateSubject",
                    ApiEndpoint = "/api/Subject/update",
                    Group = 1,
                    Description = "Sửa môn học"
                },
                new Permission
                {
                    Id = Guid.Parse("6B1C2F7A-38C2-45F6-A9B7-9D93A5196A7C"),
                    PermissionName = "DeleteSubject",
                    ApiEndpoint = "/api/Subject/delete",
                    Group = 1,
                    Description = "Xóa môn học"
                },
                new Permission
                {
                    Id = Guid.Parse("A7E7F1C4-8E88-47A3-98F9-B4F0F9879CDE"),
                    PermissionName = "ViewSubject",
                    ApiEndpoint = "/api/Subject/pagination",
                    Group = 1,
                    Description = "Xem danh sách môn học"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "CreateStudent",
                    ApiEndpoint = "/api/Student/create",
                    Group = 2,
                    Description = "Tạo mới sinh viên"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "DeleteStudent",
                    ApiEndpoint = "/api/Student/delete",
                    Group = 2,
                    Description = "Xóa sinh viên"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "UpdateStudent",
                    ApiEndpoint = "/api/Student/update",
                    Group = 2,
                    Description = "Cập nhật thông tin sinh viên"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ViewStudent",
                    ApiEndpoint = "/api/Student/pagination",
                    Group = 2,
                    Description = "Xem danh sách sinh viên"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ViewStudentWithSubject",
                    ApiEndpoint = "/api/Student/subject_1",
                    Group = 2,
                    Description = "Xem thời khóa biểu sinh viên"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ViewUserSubject",
                    ApiEndpoint = "/api/User/subject",
                    Group = 3,
                    Description = "Xem các môn của giảng viên đã đăng ký dạy"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ViewUserClass",
                    ApiEndpoint = "/api/User/class",
                    Group = 3,
                    Description = "Xem các lớp mà giảng viên đã đăng ký"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ScheduleUser",
                    ApiEndpoint = "/api/User/update/class",
                    Group = 3,
                    Description = "Giảng viên lập lịch dạy"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    PermissionName = "ViewStudentWithClass",
                    ApiEndpoint = "/api/Subject/class/student",
                    Group = 4,
                    Description = "Xem thông tin trong lớp"
                }
            };
            modelBuilder.Entity<Permission>().HasData(permissions);
            var rolepermissions = new List<RolePermission>
            {
                  new RolePermission
                  {
                      RolePermission_Id = Guid.NewGuid(),
                      Role_Id = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                      Permission_Id = Guid.Parse("3F6293B5-B56C-4332-A48E-EC9812B6878F")
                  },
                  new RolePermission
                  {
                      RolePermission_Id = Guid.NewGuid(),
                      Role_Id = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                      Permission_Id = Guid.Parse("4E2B5A4B-EE6C-4B63-A95D-DC6D13B7E58C")
                  },
                  new RolePermission
                  {
                      RolePermission_Id = Guid.NewGuid(),
                      Role_Id = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                      Permission_Id = Guid.Parse("6B1C2F7A-38C2-45F6-A9B7-9D93A5196A7C")
                  },
                  new RolePermission
                  {
                      RolePermission_Id = Guid.NewGuid(),
                      Role_Id = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                      Permission_Id = Guid.Parse("A7E7F1C4-8E88-47A3-98F9-B4F0F9879CDE")
                  },
                  new RolePermission
                  {
                      RolePermission_Id = Guid.NewGuid(),
                      Role_Id = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                      Permission_Id = Guid.Parse("A7E7F1C4-8E88-47A3-98F9-B4F0F9879CDE")
                  },
            };
            modelBuilder.Entity<RolePermission>().HasData(rolepermissions);
            var users = new List<User>()
            {
                new User
                {
                    UserId = Guid.Parse("8af6cd07-6e3a-4cb2-8a0b-9cf92c432e9d"),
                    UserName = "xiaochun",
                    Email = "chun2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("044C9907-D2DE-4E1E-AE33-8A9565CAA44C"),
                },
                new User
                {
                    UserId = Guid.Parse("c652bcb2-0e43-4f7d-9217-8b4b0cf423f8"),
                    UserName = "mimi",
                    Email = "mimi2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("5FEED829-1D72-4AA8-8446-4658853D85F9")
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "yang",
                    Email = "ayng2005@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("0AB25235-7295-4CB4-ABCF-7C8F66AFDB10")
                },
                new User
                {
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    UserName = "trungnguyen",
                    Email = "trungnguyen2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("EAFF2B81-DC92-4F4A-8D1A-B4953305F68E")
                },
                new User
                {
                    UserId = Guid.Parse("E1F0503A-75F5-4062-AF33-ECB7C591C198"),
                    UserName = "giangnguyen",
                    Email = "giangnguyen2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("EAFF2B81-DC92-4F4A-8D1A-B4953305F68E")
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "lienpham",
                    Email = "lienpham2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("9295A976-E816-49C9-AED0-6267320EAF88")
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "giangnguyen",
                    Email = "giangnguyen2003@gmail.com",
                    Password = "1232003",
                    RoleId = Guid.Parse("0B3E6D46-4E19-48AB-8998-7CF4186ACD9B"),
                    DepartmentId = Guid.Parse("C1F5223F-A8B7-4C1F-A7E8-E6B845DF9A09")
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            var subjects = new List<Subject>()
            {
                new Subject
                {
                    SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                    SubjectName = "Tiếng anh trung cấp 1",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                    SubjectName = "Tiếng anh trung cấp 2",
                    ParentId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("DADE835C-CA38-45BF-A591-57CA00BA232F"),
                    SubjectName = "Tiếng anh trung cấp 3",
                    ParentId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                    SubjectName = "Bóng bàn",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                    SubjectName = "Kỹ năng sống",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                    SubjectName = "Hội họa cơ bản",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                    SubjectName = "Bóng đá",
                    NumberOfCredits = 4,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                    SubjectName = "Giải tích 1",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                    SubjectName = "Tiếng nhật 1",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                    SubjectName = "Tiếng hàn 1",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                    SubjectName = "Tiếng trung 1",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                    ParentId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                    SubjectName = "Tiếng nhật 2",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                    SubjectName = "An toàn thông tin",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                    SubjectName = "Công nghệ web",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                    SubjectName = "Khai phá dữ liệu",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                    SubjectName = "C#",
                    NumberOfCredits = 3,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                    SubjectName = "Công nghệ phần mềm",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                    SubjectName = "Học máy",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                    SubjectName = "Nhập môn khoa học dữ liệu",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB"),
                    SubjectName = "Dữ liệu lớn",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                    SubjectName = "Bóng rổ",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                    SubjectName = "Bóng chuyền",
                    NumberOfCredits = 2,
                },
                new Subject
                {
                    SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                    SubjectName = "Thể dục cơ bản",
                    NumberOfCredits = 3
                },
            };
            modelBuilder.Entity<Subject>().HasData(subjects);
            var UserSubjects = new List<UserSubject>
            {
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB")
                },
                new UserSubject
                {
                    UserSubject_Id = Guid.NewGuid(),
                    UserId = Guid.Parse("FD1D94A0-C819-4649-938A-2544281BC301"),
                    FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd")
                }
            };
            modelBuilder.Entity<UserSubject>().HasData(UserSubjects);
            var SemesterSubjects = new List<SemesterSubject>
            {
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("DADE835C-CA38-45BF-A591-57CA00BA232F"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                },
                new SemesterSubject
                {
                    SemesterSubject_Id = Guid.NewGuid(),
                    SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                    FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                },
            };
            modelBuilder.Entity<SemesterSubject>().HasData(SemesterSubjects);
            var MajorsSubjects = new List<MajorsSubject>()
            {
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TA"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TA"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("DADE835C-CA38-45BF-A591-57CA00BA232F"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TA"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TT"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TD"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TD"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TD"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "NN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "NN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "NN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "NN",
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN_TD"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN_TD"

                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "CN"

                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TT"

                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TT"
                },
                new MajorsSubject
                {
                    MajorsSubject_Id = Guid.NewGuid(),
                    FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                    MajorsId = Guid.Parse("896892A9-3DCC-4F71-BE85-7DAA974647B9"),
                    Category = "TT"
                },
            };
            modelBuilder.Entity<MajorsSubject>().HasData(MajorsSubjects);
            var classes = new List<SemesterClass>
             {
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B609",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B608",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B607",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B606",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B605",
                     ClassNumber = 2,
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B604",
                     ClassNumber = 2,
                     WeekDay = 6,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B603",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B602",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC1",
                     Classroom = "B601",
                     ClassNumber = 3,
                     WeekDay = 7,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("f53d9b26-1c27-4c79-bd44-7e1e6bfc0c16"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC2",
                     Classroom = "B513",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC2",
                     Classroom = "B512",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC2",
                     Classroom = "B511",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC2",
                     Classroom = "B510",
                     ClassNumber = 2,
                     WeekDay = 3,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANHTC2",
                     Classroom = "B509",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("0C3AD6BE-0F5E-4F65-8808-FF35BED77ACB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGBAN",
                     Classroom = "NHATC",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGBAN",
                     Classroom = "NHATC",
                     ClassNumber = 2,
                     WeekDay = 4,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGBAN",
                     Classroom = "NHATC",
                     ClassNumber = 3,
                     WeekDay = 6,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGBAN",
                     Classroom = "NHATC",
                     ClassNumber = 4,
                     WeekDay = 3,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("a7e2f789-34fc-4b6e-b308-50b70bc68d43"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B609",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B609",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B608",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B608",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B607",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KNS",
                     Classroom = "B607",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("b45a58e2-6e85-4d6a-9dc7-d52553b3ae19"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOIHOACB",
                     Classroom = "B606",
                     Describe = "LT",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOIHOACB",
                     Classroom = "B605",
                     Describe = "BT",
                     ClassNumber = 1,
                     WeekDay = 8,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOIHOACB",
                     Classroom = "B604",
                     Describe = "LT",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOIHOACB",
                     Classroom = "B604",
                     Describe = "BT",
                     ClassNumber = 2,
                     WeekDay = 8,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("19018876-BD0E-4271-AAE6-38831DD7CB20"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 2,
                     WeekDay = 3,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGDA",
                     Classroom = "CBN",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BD56CF9B-45B9-42CE-9C84-60B32350424C"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B520",
                     ClassNumber = 1,
                     Describe = "LT",
                     WeekDay = 4,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B520",
                     ClassNumber = 1,
                     Describe = "BT",
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B520",
                     ClassNumber = 1,
                     Describe = "BT",
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B519",
                     ClassNumber = 2,
                     Describe = "LT",
                     WeekDay = 3,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B519",
                     ClassNumber = 2,
                     Describe = "BT",
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B519",
                     ClassNumber = 2,
                     Describe = "BT",
                     WeekDay = 6,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B518",
                     ClassNumber = 3,
                     Describe = "LT",
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B518",
                     ClassNumber = 3,
                     Describe = "BT",
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "GIAITICH1",
                     Classroom = "B518",
                     ClassNumber = 3,
                     Describe = "BT",
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("e9d3b36f-45cf-4d86-8743-aceca695d7fd"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B503",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B503",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B503",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 2,
                     WeekDay = 4,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 2,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT1",
                     Classroom = "B502",
                     ClassNumber = 3,
                     WeekDay = 7,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B501",
                     ClassNumber = 1,
                     WeekDay = 3,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B501",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B501",
                     ClassNumber = 1,
                     WeekDay = 7,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B409",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B409",
                     ClassNumber = 2,
                     WeekDay = 4,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGHAN1",
                     Classroom = "B409",
                     ClassNumber = 2,
                     WeekDay = 6,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("A9FF86B6-3FE8-4980-B2DA-01AEF0C73E36"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B408",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B408",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B408",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B407",
                     ClassNumber = 2,
                     WeekDay = 3,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B407",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGTRUNG1",
                     Classroom = "B407",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("7E8EF101-3183-458B-9302-432C00A096DA"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B406",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B406",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B406",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B405",
                     ClassNumber = 2,
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B405",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B405",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B404",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B404",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "TIENGNHAT2",
                     Classroom = "B404",
                     ClassNumber = 3,
                     WeekDay = 7,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("943E6618-CEE5-460F-B003-B53C5014619E"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B403",
                     ClassNumber = 1,
                     WeekDay = 3,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B403",
                     ClassNumber = 1,
                     WeekDay = 3,
                     OnShift = 8,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B402",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B402",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 8,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B401",
                     ClassNumber = 3,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "ANTOANTTIN",
                     Classroom = "B401",
                     ClassNumber = 3,
                     WeekDay = 3,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("b158e77a-e8f4-43a1-abc7-65d1896ac005"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNWEB",
                     Classroom = "A703",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 8,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("69C422C1-E837-49C0-8C02-84568B4DCE1D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A709",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A704",
                     ClassNumber = 1,
                     WeekDay = 7,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A709",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A704",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A709",
                     ClassNumber = 3,
                     WeekDay = 2,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "KHAIPHADL",
                     Classroom = "A704",
                     ClassNumber = 3,
                     WeekDay = 5,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("F98283C6-66B5-4E62-B3B0-04A000B8B0B2"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 3,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 2,
                     WeekDay = 6,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 2,
                     WeekDay = 6,
                     OnShift = 8,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 3,
                     WeekDay = 4,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "C#",
                     Classroom = "A705",
                     ClassNumber = 3,
                     WeekDay = 4,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("865A4EE2-4882-4757-A1D1-811F03415E32"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A712",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A712",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A711",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A711",
                     ClassNumber = 2,
                     WeekDay = 3,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A710",
                     ClassNumber = 3,
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "CNPHANMEM",
                     Classroom = "A710",
                     ClassNumber = 3,
                     WeekDay = 4,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("7945D608-0166-4DEE-A119-940F7C8DF13D"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOCMAY",
                     Classroom = "A710",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOCMAY",
                     Classroom = "A710",
                     ClassNumber = 1,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOCMAY",
                     Classroom = "A710",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "HOCMAY",
                     Classroom = "A710",
                     ClassNumber = 2,
                     WeekDay = 7,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("BA805446-367C-4929-B746-FC797388E8EF"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "NHAPMONKHDL",
                     Classroom = "A708",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "NHAPMONKHDL",
                     Classroom = "A708",
                     ClassNumber = 1,
                     WeekDay = 6,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "NHAPMONKHDL",
                     Classroom = "A708",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "NHAPMONKHDL",
                     Classroom = "A708",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 8,
                     FK_SubjectId = Guid.Parse("857D8ADB-74A1-403D-925D-F9A133A5B095"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "DULIEULON",
                     Classroom = "A705",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 3,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "DULIEULON",
                     Classroom = "A705",
                     ClassNumber = 1,
                     WeekDay = 3,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("B86D8635-F095-415F-A4D8-2B415D836ABB"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGRO",
                     Classroom = "NHATC",
                     ClassNumber = 1,
                     WeekDay = 2,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGRO",
                     Classroom = "NHATC",
                     ClassNumber = 2,
                     WeekDay = 2,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGRO",
                     Classroom = "NHATC",
                     ClassNumber = 3,
                     WeekDay = 4,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGRO",
                     Classroom = "NHATC",
                     ClassNumber = 4,
                     WeekDay = 6,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("0D20C2F0-C1D7-41FC-AF0C-F6F12E8B74D3"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGCHUYEN",
                     Classroom = "NHATC",
                     ClassNumber = 1,
                     WeekDay = 3,
                     OnShift = 4,
                     EndShift = 5,
                     FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGCHUYEN",
                     Classroom = "NHATC",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 6,
                     EndShift = 7,
                     FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGCHUYEN",
                     Classroom = "NHATC",
                     ClassNumber = 3,
                     WeekDay = 6,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "BONGCHUYEN",
                     Classroom = "NHATC",
                     ClassNumber = 4,
                     WeekDay = 7,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("A907092B-4F8F-4EFA-8468-429B2A3801D4"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "THEDUCCB",
                     Classroom = "NHATC",
                     ClassNumber = 1,
                     WeekDay = 4,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "THEDUCCB",
                     Classroom = "NHATC",
                     ClassNumber = 2,
                     WeekDay = 5,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "THEDUCCB",
                     Classroom = "NHATC",
                     ClassNumber = 3,
                     WeekDay = 7,
                     OnShift = 9,
                     EndShift = 10,
                     FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
                 new SemesterClass
                 {
                     SemesterClass_Id = Guid.NewGuid(),
                     Name = "THEDUCCB",
                     Classroom = "NHATC",
                     ClassNumber = 4,
                     WeekDay = 7,
                     OnShift = 1,
                     EndShift = 2,
                     FK_SubjectId = Guid.Parse("6349C24F-8E64-4512-9AA7-8C887F16FA92"),
                     SemesterId = Guid.Parse("DE7FBE40-99F1-4CB0-A85A-0241C4F89140"),
                 },
             };
            modelBuilder.Entity<SemesterClass>().HasData(classes);
            var studentSubject = new List<StudentSubject>()
            {
                new StudentSubject()
                {
                    StudentSubject_Id = Guid.NewGuid(),
                    StudentId = Guid.Parse("561D6341-41CE-43A3-85F1-E1CDA2D58336"),
                    SubjectId = Guid.Parse("67fe6d4a-8985-4a45-94cb-9a078d01d38c")
                }
            };
            modelBuilder.Entity<StudentSubject>().HasData(studentSubject);
        }
    }
}
