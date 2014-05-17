using OdeToFoodGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFoodGit.Tests.Fakes
{
    class FakeOdeToFoodDb : IOdeToFoodDb
    {
        IQueryable<T> IOdeToFoodDb.Query<T>()
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }

        public void Dispose() {}

        public void AddSet<T>(IQueryable<T> objects)
        {
            Sets.Add(typeof(T), objects);
        }

        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
    }
}
