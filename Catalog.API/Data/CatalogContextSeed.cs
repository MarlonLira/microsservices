using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var existProduct = productCollection.Find(x => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "546f745887734d94ba6203eeb1b5365e",
                    Name = "Tv",
                    Description = "Lorem ipsum bibendum semper sit phasellus tellus fringilla porta arcu, ut adipiscing",
                    Image = "tv_24.jpg",
                    Price = 1.100M,
                    Category = "eletrodomésticos"
                },
                new Product()
                {
                    Id = "66414a2f8ccd433b9b4b551440d69589",
                    Name = "Samsung S8",
                    Description = "Lorem ipsum vivamus porta risus curabitur habitasse dapibus at sem felis taciti",
                    Image = "s8.jpg",
                    Price = 2.800M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "a1dd5715e49e4bc3aa6215ac9cc576db",
                    Name = "Samsung S10",
                    Description = "Lorem ipsum ac odio vel netus cursus quam lacus sed",
                    Image = "s10.jpg",
                    Price = 3.800M,
                    Category = "Smart Phone"
                },
            };
        }
    }
}
