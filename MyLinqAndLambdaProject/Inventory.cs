using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyLinqAndLambdaProject
{
    class Inventory
    {
        private List<Product> products;
        public void Initialize()
        {
            products = new List<Product>()
            {
                new Product { Description = "Avocado",     UnitPrice = 29,     InStock = 63,     Likes = 498 },
                new Product { Description = "Banana",      UnitPrice = 5,      InStock = 154,    Likes = 120 },
                new Product { Description = "Orange",      UnitPrice = 9,      InStock = 98,     Likes = 233 },
                new Product { Description = "Mango",       UnitPrice = 25,     InStock = 20,     Likes = 513 },
                new Product { Description = "Red Onion",   UnitPrice = 3,      InStock = 44,     Likes = 78  },
                new Product { Description = "Salad",       UnitPrice = 15,     InStock = 12,     Likes = 434 },
                new Product { Description = "Carrot",      UnitPrice = 4,      InStock = 38,     Likes = 131 },
                new Product { Description = "Broccoli",    UnitPrice = 14,     InStock = 16,     Likes = 59  },
                new Product { Description = "Apple",       UnitPrice = 6,      InStock = 103,    Likes = 470 },
            };

            DisplayAllProducts(products);
            DisplayPopularProducts(products);
            DisplayPopularProductsLambda(products);
            OrderByPrice(products);
            OrderByPriceLambda(products);
            CalculateTotalValueOfEachProduct(products);
            CalculateTotalValueOfEachProductLambda(products);
            RankProductsByPotentialProfitLambda(products);
        }

        public void DisplayAllProducts(List<Product> products)
        {
            foreach (var p in products)
            {
                Console.WriteLine($"Description: { p.Description}\tUnit price: { p.UnitPrice}\tIn stock: {p.InStock}\tLikes: { p.Likes}");
            }
        }

        public void DisplayPopularProducts(List<Product> products)
        {
            IEnumerable<string> result = from p in products
                                         where p.Likes > 300
                                         orderby p.Likes descending
                                         select p.Description;
            PresentResult(result, "Most popular products");
        }

        public void DisplayPopularProductsLambda(List<Product> products)
        {
            IEnumerable<string> result = products.Where(p => p.Likes > 300)
                                                 .OrderByDescending(p => p.Likes)
                                                 .Select(p => p.Description);

            PresentResult(result, "Most popular products - Using lambda");
        }

        public void OrderByPrice(List<Product> products)
        {
            IEnumerable<string> result = from p in products
                                         orderby p.UnitPrice descending
                                         select p.Description;
            PresentResult(result, "Ordered by unit price");
        }

        public void OrderByPriceLambda(List<Product> products)
        {
            IEnumerable<string> result = products.OrderByDescending(p => p.UnitPrice)
                                                 .Select(p => p.Description);

            PresentResult(result, "Ordered by unit price - Using lambda");
        }

        public void CalculateTotalValueOfEachProduct(List<Product> products)
        {
            var result = from p in products
                         orderby p.Description
                         select new { Desc = p.Description, Value = p.InStock * p.UnitPrice };

            foreach (var p in result)
            {
                Console.WriteLine($"The value of all {p.Desc}(s)\tin stock is: {p.Value} kr");
            }
        }

        public void CalculateTotalValueOfEachProductLambda(List<Product> products)
        {
            var result = products.OrderBy(p => p.Description)
                                 .Select(p => new { Desc = p.Description, Value = p.InStock * p.UnitPrice });

            foreach (var p in result)
            {
                Console.WriteLine($"The value of all {p.Desc}(s)\tin stock is: {p.Value} kr");
            }
        }

        public void RankProductsByPotentialProfitLambda(List<Product> products)
        {
            foreach (var p in products)
            {
                double likesPerKr = Math.Round((p.Likes / p.UnitPrice), 2);
                Console.WriteLine($"The products with most likes compared to the price is {p.Description}, {likesPerKr}");
            }
        }

        private static void PresentResult(IEnumerable<string> result, string description)
        {
            Console.WriteLine($"\n{description}");
            foreach (var p in result)
            {
                Console.WriteLine(p);
            }
        }
    }
}
