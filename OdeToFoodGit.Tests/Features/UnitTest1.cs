using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeToFoodGit.Models;
using System.Collections.Generic;
using System.Linq;

// 
// A restaurant's overall rating can be calculated using various methods.
// For this application we'll want to try different methods over time, 
// but for starters we'll allow an adminstrator to toggle between two
// different techniques.
//
// 1. Simple mean of the "rating" value for the most recent n reviews
//    (the admin can configure the value of n)
//
// 2. Weighted mean of the last n reviews.  The most recent n/2 reviews
//    will be weighted twice as much as the oldest n/2 reviews
//
// Overall rating should be a whole number
// 

namespace OdeToFoodGit.Tests.Features
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Computes_Result_For_One_Review()
        {
            var data = BuildRestaurantAndReviews(ratings: 4);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeRating(10);

            Assert.AreEqual(4, result.Rating);

        }

        [TestMethod]
        public void Computes_Result_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(ratings: new[] { 4, 8 });

            var rater = new RestaurantRater(data);
            var result = rater.ComputeRating(10);

            Assert.AreEqual(6, result.Rating);
        }

        [TestMethod]
        public void Weighted_Average_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(ratings: new[] { 3, 9 });

            var rater = new RestaurantRater(data);
            var result = rater.ComputeWeightedRate(10);

            Assert.AreEqual(5, result.Rating);
        }

        private Restaurant BuildRestaurantAndReviews(params int[] ratings)
        {
            var restaurant = new Restaurant();
            // need a using statement for System.Linq at the top of the file
            restaurant.Reviews =
                ratings.Select(r => new RestaurantReview { Rating = r })
                .ToList();

            return restaurant;
        }

    }
}
