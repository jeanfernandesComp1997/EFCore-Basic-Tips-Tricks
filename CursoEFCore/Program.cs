using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertData();
            //InsertBatch();
            //QueryData();
            //InsertOrder();
            QueryWithEarlyLoading();
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

        private static void QueryData()
        {
            using var db = new ApplicationContext();

            //var queryForSintax = (from c in db.Clients where c.Id > 0 select c).ToList();
            //var queryMethod = db.Clients.Where(p => p.Id > 0).ToList();
            //var queryMethod = db.Clients.AsNoTracking().Where(p => p.Id > 0).ToList();
            var queryMethod = db.Clients
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var client in queryMethod)
            {
                Console.WriteLine($"Query client: {client.Id}");

                /* Apenas o método Find faz buscas em memória */
                //db.Clients.Find(client.Id);

                db.Clients.FirstOrDefault(c => c.Id == client.Id);
            }
        }

        private static void InsertOrder()
        {
            using var db = new ApplicationContext();

            var client = db.Clients.FirstOrDefault();
            var product = db.Clients.FirstOrDefault();

            var order = new Order()
            {
                ClientId = client.Id,
                CreatedAt = DateTime.Now,
                FinishedAt = DateTime.Now,
                Observation = "Test order",
                Status = OrderStatus.Analysis,
                FreightType = FreightType.WithoutFreight,
                Items = new List<OrderItem>
                {
                    new OrderItem()
                    {
                        ProductId = product.Id,
                        Discount = 0,
                        Quantity = 1,
                        Value = 10
                    }
                }
            };

            db.Orders.Add(order);

            db.SaveChanges();
        }

        private static void QueryWithEarlyLoading()
        {
            using var db = new ApplicationContext();

            var orders = db.Orders
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .ToList();

            Console.WriteLine(orders.Count);
        }
    }
}