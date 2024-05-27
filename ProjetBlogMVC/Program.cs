using CONST;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using Services;
using System.Text.Json.Serialization;

namespace ProjetBlogMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            AppSettings.BaseURL = builder.Configuration.GetValue("BaseURL", "");

            builder.Services.AddDbContext<ProjetBlogContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ProjetBlogContext>();

            builder.Services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProjetBlogContext>();

            builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
                jsonOptions.JsonSerializerOptions.ReferenceHandler =
                ReferenceHandler.IgnoreCycles
            );
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.MapRazorPages();

            app.Run();
        }
    }
}
