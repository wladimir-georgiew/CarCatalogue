using CarCatalogue.Data.Entities;
using CarCatalogue.Extensions;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using System.Text.Json;

using CarCatalogue.Common.Constants;

namespace CarCatalogue.Data
{

    public static class DbInitializer
    {
        /// <summary>
        /// Ensures the database is updated and seeds entities.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        public static async void Initialize(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            context.Database.EnsureCreated();

            await SeedCars(context);

            await SeedRolesAsync(context,
                roles: new string[]
                {
                    Roles.ADMIN,
                });

            await SeedUsersAndAssignToRolesAsync(context, userManager, new UserRoleDto[]
            {
                new UserRoleDto("admin@abv.bg", Roles.ADMIN)
            });
        }

        private static async Task SeedCars(ApplicationDbContext context)
        {
            if (context.Cars.Any())
            {
                return;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "cars.json");
            var json = File.ReadAllText(path);
            var cars = JsonSerializer.Deserialize<IList<CarJsonModel>>(json);

            foreach (var car in cars!)
            {
                var make = car.Name!
                    .Split(" ")[0];
                var model = car.Name
                    .Split(" ")
                    .Skip(1);

                context.Cars.Add(
                    new Car
                    {
                        Make = make.ToCamelCase(),
                        Model = string.Join(" ", model.Select(x => x.ToCamelCase())),
                        Year = DateTime.Parse(car.Year ?? "1999/01/01"),
                        Acceleration = car.Acceleration ?? default,
                        Horsepower = car.Horsepower ?? default,
                        Weight = (int)Math.Ceiling((double)(car.Weight_in_lbs! * 0.45359237))
                    });
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedRolesAsync(ApplicationDbContext context, string[] roles)
        {
            foreach (string roleName in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    await roleStore.CreateAsync(new IdentityRole(roleName) { NormalizedName = roleName.ToUpper() });
                }
            }
        }

        private static async Task SeedUsersAndAssignToRolesAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, UserRoleDto[] data)
        {
            foreach (var userRoleDto in data)
            {
                var user = new IdentityUser
                {
                    UserName = userRoleDto.UserEmail,
                    Email = userRoleDto.UserEmail,
                    NormalizedEmail = userRoleDto.UserEmail.ToUpper(),
                    NormalizedUserName = userRoleDto.UserEmail.ToUpper(),
                    PhoneNumber = "",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user, "123123123");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<IdentityUser>(context);

                    await userStore.CreateAsync(user);

                    var dbUser = await userManager.FindByEmailAsync(userRoleDto.UserEmail);
                    await userManager.AddToRoleAsync(dbUser!, userRoleDto.RoleName);
                }
            }
        }

        private class CarJsonModel
        {
            public string? Name { get; set; }
            public double? Miles_per_Gallon { get; set; }
            public int? Cylinders { get; set; }
            public double? Displacement { get; set; }
            public int? Horsepower { get; set; }
            public int? Weight_in_lbs { get; set; }
            public double? Acceleration { get; set; }
            public string? Year { get; set; }
            public string? Origin { get; set; }
        }
        private class UserRoleDto
        {
            public UserRoleDto(string userEmail, string roleName)
            {
                this.UserEmail = userEmail;
                this.RoleName = roleName;
            }

            public string UserEmail { get; set; }
            public string RoleName { get; set; }
        }
    }
}
