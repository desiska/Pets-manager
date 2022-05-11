using DI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DI.Data
{
    public static class Seeder
    {
        public static void Seeding()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if(db.Categories.Count() == 0)
            {
                string administratorRoleID = Guid.NewGuid().ToString();
                db.Roles.Add(new IdentityRole()
                {
                    Id = administratorRoleID,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

                db.Roles.Add(new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Housekeeper",
                    NormalizedName = "HOUSEKEEPER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

                db.Roles.Add(new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Client",
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

                db.Categories.Add(new Category()
                {
                    ID = Guid.NewGuid().ToString(),
                    Text = "Cleaning and desinfection"
                });

                db.Categories.Add(new Category()
                {
                    ID = Guid.NewGuid().ToString(),
                    Text = "Pet and plant care"
                });

                db.Categories.Add(new Category()
                {
                    ID = Guid.NewGuid().ToString(),
                    Text = "Child care"
                });

                db.Categories.Add(new Category()
                {
                    ID = Guid.NewGuid().ToString(),
                    Text = "Aduld care"
                });

                db.Statuses.Add(new Status()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Waiting"
                });

                db.Statuses.Add(new Status()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Appointed for a domestic helper"
                });

                db.Statuses.Add(new Status()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "For review"
                });

                db.Statuses.Add(new Status()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Done"
                });

                db.Statuses.Add(new Status()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Refused"
                });

                string userID = Guid.NewGuid().ToString();

                User user = new User()
                {
                    Id = userID,
                    UserName = "administrator",
                    NormalizedUserName = "ADMINISTRATOR"
                };

                IPasswordHasher<User> hasher = new PasswordHasher<User>();
                string hash = hasher.HashPassword(user, "Admin123!");

                user.PasswordHash = hash;

                db.Users.Add(user);

                db.UserRoles.Add(new IdentityUserRole<string>()
                {
                    RoleId = administratorRoleID,
                    UserId = userID
                });

                db.SaveChangesAsync();
            }
        }

    }
}
