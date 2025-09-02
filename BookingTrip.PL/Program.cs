using BookingTrip.BLL.Interfaces;
using BookingTrip.BLL.Interfaces.Repositories;
using BookingTrip.BLL.Interfaces.Services;
using BookingTrip.BLL.Repositories;
using BookingTrip.BLL.Services;
using BookingTrip.BLL.UnitOfWork;
using BookingTrip.DAL.Data.Context;
using BookingTrip.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTrip.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Unit of Work and Repositories
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            // Add BLL Services
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IRiderService, RiderService>();
            builder.Services.AddScoped<IFareCalculatorService, FareCalculatorService>();
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Add Identity for authentication (simplified for now, full Identity setup would be more complex)
            //builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
