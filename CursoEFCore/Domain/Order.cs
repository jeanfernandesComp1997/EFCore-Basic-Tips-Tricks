using CursoEFCore.ValueObjects;
using System;
using System.Collections.Generic;

namespace CursoEFCore.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public FreightType FreightType { get; set; }
        public OrderStatus Status { get; set; }
        public string Observation { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}