
using IServices;
using IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Repositories;
using Services;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IArticleReactionService, ArticleReactionService>();
builder.Services.AddScoped<IArticleReactionRepository, ArticleReactionRepository>();

builder.Services.AddScoped<IArticleReadService, ArticleReadService>();
builder.Services.AddScoped<IArticleReadRepository, ArticleReadRepository>();

builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<IDisLikeService, DisLikeService>();
builder.Services.AddScoped<IDisLikeRepository, DisLikeRepository>();

builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<ISupportRepository, SupportRepository>();



builder.Services.AddDbContext<ProjetBlogContext>(dbContextBuilderOptions =>
    dbContextBuilderOptions.UseSqlServer(builder.Configuration.GetConnectionString("ContextCS"))

);


string? MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});



builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProjetBlogContext>(); 


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
    jsonOptions.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles
);

builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetBlog", Version = "V1" });
    string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swaggerOptions.IncludeXmlComments(xmlPath);

    swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    swaggerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
});


WebApplication? app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
