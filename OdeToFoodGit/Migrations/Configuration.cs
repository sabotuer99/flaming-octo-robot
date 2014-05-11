namespace OdeToFoodGit.Migrations
{
    using OdeToFoodGit.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
        }
    }
}
