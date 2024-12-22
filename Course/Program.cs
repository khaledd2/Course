using AutoMapper;
using Course.BLL;
using Course.BLL.Interfaces;
using Course.BLL.Services;
using Course.DAL;
using Course.Shared.DTOs;
using Course.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Course.Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

/*=================================================== Services ========================================*/
// Auth services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorization();

// Controller
builder.Services.AddControllers().ConfigureApiBehaviorOptions
    (options => { options.InvalidModelStateResponseFactory = context =>
        { var errorMessages = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new BaseResponse<object>(null, Messages.Validation, errorMessages, false);
            return new BadRequestObjectResult(response); 
        };
    }); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
}).CreateMapper());

// Custom services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IPostService, PostService>();

// ÅÖÇÝÉ ÎÏãÇÊ CORS
builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowSpecificOrigin",
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

/*=================================================== App ===============================================*/
var app = builder.Build();

//app.UseCors("AllowSpecificOrigin"); // áãÇ äÑÝÚ ÇáÝÑæäÊ Ú ÇÓÊÖÇÝÉ Èíßæä ßÐÇ
app.UseCors("AllowAllOrigins");

// Enable serving of static files from the wwwroot folder
app.UseStaticFiles(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGroup("/identity").MapIdentityApi<IdentityUser>();
app.MapControllers();
app.Run();
