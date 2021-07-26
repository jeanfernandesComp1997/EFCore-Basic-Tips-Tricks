using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertData();
            InsertBatch();
        }

        private static void InsertData()
        {
            var product = new Product()
            {
                Description = "Test product",
                BarCode = "123456789",
                Value = 10m,
                ProductType = ProductType.ProductForSale,
                Active = true,
            };

            using var db = new ApplicationContext();

            /* Opções mais indicadas */
            //db.Products.Add(product);
            db.Set<Product>().Add(product);

            /* Outras opções */
            //db.Entry(product).State = EntityState.Added;
            //db.Add(product);

            /* Save registers */
            var registers = db.SaveChanges();

            Console.WriteLine($"Total register(s): {registers}");
        }

        private static void InsertBatch()
        {
            var product = new Product()
            {
                Description = "Test product",
                BarCode = "123456789",
                Value = 10m,
                ProductType = ProductType.ProductForSale,
                Active = true,
            };

            var client = new Client()
            {
                Name = "John Doe",
                Phone = "11999999999",
                State = "SP",
                CEP = "12345000",
                City = "São Paulo"
            };

            var listClients = new[]
            {
                new Client()
                {
                    Name = "John Doe",
                    Phone = "11999999999",
                    State = "SP",
                    CEP = "12345000",
                    City = "São Paulo"
                },
                new Client()
                {
                    Name = "John Doe 2",
                    Phone = "11777777777",
                    State = "SP",
                    CEP = "12345000",
                    City = "São Paulo"
                }
            };

            using var db = new ApplicationContext();

            //db.AddRange(product, client);
            db.AddRange(listClients);

            var registers = db.SaveChanges();

            Console.WriteLine($"Total register(s): {registers}");
        }
    }
}