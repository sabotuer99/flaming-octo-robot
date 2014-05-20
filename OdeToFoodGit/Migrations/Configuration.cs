namespace OdeToFoodGit.Migrations
{
    using OdeToFoodGit.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFoodGit.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFoodGit.Models.OdeToFoodDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
            new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
            new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
            new Restaurant
            {
                Name = "Smaka",
                City = "Gothenburg",
                Country = "Sweden",
                Reviews = new List<RestaurantReview> {
                new RestaurantReview{Rating = 9, Body = "Great food!", ReviewerName = "Scott"}
                }
            });

            for (int i=0; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant {Name = i.ToString(), City = "NoWhere", Country = "USA"});
            }

            SeedMembership();
        }

        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if(!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("sallen", false) == null)
            {
                membership.CreateUserAndAccount("sallen", "imalittleteapot");
            }
            if (!roles.GetRolesForUser("sallen").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "sallen" }, new[] { "admin" });
            }
        }

        
    }
}
