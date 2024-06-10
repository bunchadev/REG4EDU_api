using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using REG4EDU_api.Authentication;
using REG4EDU_api.Data;
using REG4EDU_api.Middlewares;
using REG4EDU_api.Repositories;
using REG4EDU_api.Services;
using REG4EDU_api.Requirement;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DevDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("myApiConnectionStrings")));
//
builder.Services.AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>();
var permissions = new List<string>
{ "CreateSubject", "UpdateSubject", "DeleteSubject",
  "ViewSubject","ViewStudent","CreateStudent","DeleteStudent",
  "UpdateStudent","ViewStudentWithSubject","ViewUserClass","ScheduleUser","ViewUserSubject",};
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in permissions)
    {
        options.AddPolicy($"{permission}Policy", policy =>
            policy.Requirements.Add(new PermissionRequirement(permission)));
    }
    options.AddPolicy("AdminOrStudentPolicy", policy =>
           policy.RequireRole("admin", "student"));
});
//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    });
builder.Services.AddScoped<IJwtToken, JwtToken>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISemesterClassRepository, SemesterClassRepository>();
builder.Services.AddScoped<ISemesterClassService, SemesterClassService>();
builder.Services.AddScoped<ISemesterClassRepository, SemesterClassRepository>();
builder.Services.AddScoped<ISemesterClassService, SemesterClassService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("2k3",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                          });
});
builder.Services.AddMemoryCache();
builder.Services.AddExceptionHandler<ExceptionHandlerMiddlewares>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(_ => { });
app.UseCustomStatusCodePages();
app.UseRouting();
app.UseCors("2k3");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
